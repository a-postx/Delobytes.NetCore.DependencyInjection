namespace Delobytes.NetCore.DependencyInjection;

/// <summary>
/// Способы внедрения класса в контейнер.
/// </summary>
public enum InjectionType
{
    /// <summary>
    /// Использует значение по-умолчанию, определённое при конфигурации.
    /// </summary>
    Default,

    /// <summary>
    /// Автоматическое внедрение.
    /// </summary>
    /// <remarks>
    ///   Как работает:
    ///   <list type="number">
    ///     <item>Если имплементирует любой интерфейс, будет внедрён как <see cref="Interface"/>.</item>
    ///     <item>Если расширяет любой класс, будет внедрён как <see cref="BaseClass"/>.</item>
    ///     <item>Иначе будет внедрён как <see cref="Self"/>.</item>
    ///   </list>
    /// </remarks>
    Auto,

    /// <summary>
    /// Внедряет как реализацию имплементирующего интерфейса.
    /// </summary>
    Interface,

    /// <summary>
    /// Внедряет самого себя.
    /// </summary>
    Self,

    /// <summary>
    /// Внедряет как родительский класс.
    /// </summary>
    BaseClass
}
