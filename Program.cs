using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyMcpServer.Tools;

var builder = Host.CreateApplicationBuilder(args);

// Configure all logs to go to stderr (stdout is used for the MCP protocol messages).
builder.Logging.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Trace);

builder.Services.AddSingleton<EchoTool>();
builder.Services.AddSingleton<GetTimeTool>();
builder.Services.AddSingleton<GenerateGuidTool>();
builder.Services.AddSingleton<RandomNumberTools>();
builder.Services.AddSingleton<ReadFileTool>();
builder.Services.AddSingleton<SchemaTools>();
builder.Services.AddSingleton<JsonValidateTool>();
builder.Services.AddSingleton<SearchLogsTool>();
builder.Services.AddSingleton<RunShellTool>();

// Add the MCP services: the transport to use (stdio) and the tools to register.
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<EchoTool>()
    .WithTools<GetTimeTool>()
    .WithTools<GenerateGuidTool>()
    .WithTools<RandomNumberTools>()
    .WithTools<ReadFileTool>()
    .WithTools<SchemaTools>()
    .WithTools<JsonValidateTool>()
    .WithTools<SearchLogsTool>()
    .WithTools<RunShellTool>();

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var sp = scope.ServiceProvider;

    var echo = sp.GetService<EchoTool>();
    var getTime = sp.GetService<GetTimeTool>();
    var gen = sp.GetService<GenerateGuidTool>();
    var rnd = sp.GetService<RandomNumberTools>();

    //if (echo != null)
    //{
    //    var resp = await echo.InvokeAsync(new
    //    //    {
    //        text = "hello MCP"
    //    //    });
    //    Console.Error.WriteLine($"[LocalTest] Echo: {System.Text.Json.JsonSerializer.Serialize(resp)}");
    //}
    //else
    //{
    //    Console.Error.WriteLine("[LocaTest] EchoTool not registered in DI.");
    //}
    //if(getTime != null)
    //{
    //    var resp = await getTime.InvokeAsync(null);
    //    Console.Error.WriteLine($"[LocalTest] GetTime: {System.Text.Json.JsonSerializer.Serialize(resp)}");
    //}
    //if(gen != null)
    //{
    //    var resp = await gen.InvokeAsync(null);
    //    Console.Error.WriteLine($"[LocalTest] GenerateGuid: {System.Text.Json.JsonSerializer.Serialize(resp)}");
    //}
    //if(rnd != null)
    //{
    //    var resp = rnd.GetRandomNumber();
    //    Console.Error.WriteLine($"[LocalTest] RandomNumberTools.GetRandomNumber(): {resp}");
    //}
}
await host.RunAsync();
