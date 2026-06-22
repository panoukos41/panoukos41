
using Profile.Models;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


var options = JsonSerializerOptions.Default;
var node = options.GetJsonSchemaAsNode(typeof(Resume));
var json = node.ToJsonString();


app.MapGet("/", () => "Hello World!");

app.Run();
