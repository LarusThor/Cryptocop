using Cryptocop.Worker.Payments;
using Cryptocop.Worker.Payments;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
