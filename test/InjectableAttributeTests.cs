using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Delobytes.NetCore.DependencyInjection.Tests;

public class InjectableAttributeTests
{
    [Fact]
    public void ServicesFromCurrentAssembly_AddedSuccessfully()
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();

        Action configureOptions = () =>
        {
            builder.Services.AddServicesFromCurrentAssembly();
        };

        Exception ex = Record.Exception(configureOptions);

        ex.Should().BeNull();
    }

    [Fact]
    public void AttributedService_ResolvedSuccessfully()
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder.Services.AddServicesFromCurrentAssembly();
        WebApplication app = builder.Build();

        ITestService? testService = app.Services.GetService<ITestService>();

        testService.Should().NotBeNull();
        testService.Should().BeOfType<TestService>();
    }
}
