using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Delobytes.NetCore.DependencyInjection;

/// <summary>
/// Расширения коллекции сервисов.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <inheritdoc cref="AddServicesFromAssembly(IServiceCollection,Assembly)"/>
	public static void AddServicesFromAssemblies(this IServiceCollection services, IEnumerable<string> assemblyNames)
    {
        foreach (string assemblyName in assemblyNames)
        {
            services.AddServicesFromAssembly(assemblyName);
        }
    }

    /// <inheritdoc cref="AddServicesFromAssembly(IServiceCollection,Assembly)"/>
    public static void AddServicesFromAssemblies(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        foreach (Assembly assembly in assemblies)
        {
            services.AddServicesFromAssembly(assembly);
        }
    }

    /// <inheritdoc cref="AddServicesFromAssembly(IServiceCollection,Assembly)"/>
    public static void AddServicesFromCurrentAssembly(this IServiceCollection services)
    {
        services.AddServicesFromAssembly(Assembly.GetCallingAssembly());
    }

    /// <inheritdoc cref="AddServicesFromAssembly(IServiceCollection,Assembly)"/>
    public static void AddServicesFromAssembly(this IServiceCollection services, string assemblyName)
    {
        services.AddServicesFromAssembly(Assembly.Load(assemblyName));
    }

    /// <summary>Регистрирует все сервисы, помеченные атрибутом <see cref="InjectableAttribute"/> в контейнере.</summary>
	/// <remarks><b>Все имплементации должны быть атрибутированы <see cref="InjectableAttribute"/>.</b></remarks>
	/// <param name="services"><see cref="IServiceCollection" /> для добавления сервисов.</param>
	/// <param name="assembly">Библиотека с имплементациями.</param>
	/// <exception cref="ArgumentOutOfRangeException">Если предоставлен невалидный тип имплементации.</exception>
	public static void AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        IEnumerable<Type> serviceTypes = assembly.GetTypes();

        foreach (Type implType in serviceTypes)
        {
            InjectableAttribute? attr = implType.GetAttribute<InjectableAttribute>();

            if (attr is null)
            {
                continue;
            }

            switch (attr.InjectionType)
            {
                case InjectionType.Auto:
                {
                    if (implType.GetInterfaces().Length > 0)
                    {
                        goto case InjectionType.Interface;
                    }

                    if (implType.BaseType is not { })
                    {
                        goto case InjectionType.BaseClass;
                    }

                    goto case InjectionType.Self;
                }
                case InjectionType.Interface:
                {
                    attr.ServiceType ??= implType.GetInterfaces().First();
                    services.Add(new ServiceDescriptor(attr.ServiceType, implType, attr.Lifetime));
                    break;
                }
                case InjectionType.Self:
                {
                    services.Add(new ServiceDescriptor(implType, implType, attr.Lifetime));
                    break;
                }
                case InjectionType.BaseClass:
                {
                    services.Add(new ServiceDescriptor(implType.BaseType!, implType, attr.Lifetime));
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(attr.InjectionType), "Invalid injection type.");
            }
        }
    }
}
