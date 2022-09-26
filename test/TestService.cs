namespace Delobytes.NetCore.DependencyInjection.Tests;

[Injectable]
public class TestService : ITestService
{
    public string Prop1 { get; set; } = string.Empty;
}
