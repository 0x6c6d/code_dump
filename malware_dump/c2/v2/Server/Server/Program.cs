using Server;
using Server.Common;

var builder = Host.CreateApplicationBuilder(args);
builder.ConfigureServices();

var host = builder.Build();
host.Run();