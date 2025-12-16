using System.Diagnostics;
using System.Text;
using System.Text.Json;
using McpClient;

class Program
{
    static async Task Main(string[] args)
    {
        using var client = new McpClient.McpClient("dotnet", "run --project ../MyMcpServer/MyMcpServer.csproj");

        await client.InitializeAsync();

        Console.WriteLine("\n--- Tools ---");
        var tools = await client.ListToolsAsync();
        Console.WriteLine(tools);

        Console.WriteLine("\n--- Echo ---");
        var echo = await client.CallToolAsync<object>("echo", new { text = "Hello from MCP Client"});

        Console.WriteLine(echo);

        Console.WriteLine("\n--- Get Time ---");
        var time = await client.CallToolAsync<object>("get-time", new { });

        Console.WriteLine(time);

        Console.WriteLine("Press ENTER to exit");
        Console.ReadLine();
    }
}
