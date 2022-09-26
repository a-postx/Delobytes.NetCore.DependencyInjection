using System;
using Microsoft.Extensions.DependencyInjection;

namespace Delobytes.NetCore.DependencyInjection;

/// <summary> Атрибут для предоставления ссылки на сервис для контейнера зависимостей.</summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class InjectableAttribute : Attribute
{
    /// <summary> Указывает время жизни сервиса. </summary>
    public ServiceLifetime Lifetime { get; init; } = ServiceLifetime.Transient;

    /// <summary> Указывает тип инъекции.</summary>
    public InjectionType InjectionType { get; init; } = InjectionType.Auto;

    /// <summary> Указывает базовый тип инъекции.</summary>
    public Type? ServiceType { get; set; }
}
