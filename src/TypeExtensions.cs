using System;
using System.Reflection;

namespace Delobytes.NetCore.DependencyInjection;

/// <summary>
/// Расширения типов.
/// </summary>
public static class TypeExtensions
{
    /// <summary>Простой способ получить атрибут на типе.</summary>
    /// <param name="type">Тип с которого получить атрибут.</param>
    /// <returns><see cref="TAttribute"/> или <see langword="null"/>.</returns>
    public static TAttribute? GetAttribute<TAttribute>(this Type type)
    {
        Attribute? attr = type.GetCustomAttribute(typeof(TAttribute));
        return attr is null ? default : (TAttribute)(object)attr;
    }

    /// <summary>Простой способ получить атрибут на типе.</summary>
    /// <param name="type">Тип с которого получить атрибут.</param>
    /// <returns><see cref="TAttribute"/> или исключение <see cref="TypeLoadException"/>.</returns>
    /// <exception cref="TypeLoadException">Если TAttribute не найден на типе.</exception>
    public static TAttribute GetRequiredAttribute<TAttribute>(this Type type)
    {
        Attribute? attr = type.GetCustomAttribute(typeof(TAttribute));

        if (attr is null)
        {
            throw new TypeLoadException($"Attribute {typeof(TAttribute).Name} for type {type.Name} not found.");
        }

        return (TAttribute)(object)attr;
    }
}
