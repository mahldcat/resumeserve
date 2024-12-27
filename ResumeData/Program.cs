using DataAccess;
using Microsoft.OpenApi.Models;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.BuildPaginatedDataConnection()

.Services.AddTransient<IPaginatedData,PaginatedData>()
.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Resume API", Version = "v1" });
    })
.AddCors(options =>
{
    options.AddPolicy("AllowAnyLocalhost", policy =>
    {
        policy.SetIsOriginAllowed(origin => 
                origin.StartsWith("http://localhost:")) // Allows any localhost port
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
    options.AddPolicy("AllowHyperioSubdomains", policy =>
    {
        policy.SetIsOriginAllowed(origin => 
                Uri.TryCreate(origin, UriKind.Absolute, out var uri) &&
                uri.Host.EndsWith(".hyperio.us"))
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
    options.AddPolicy("AllowHyperioRoot", policy =>
    {
        policy.SetIsOriginAllowed(origin => 
                Uri.TryCreate(origin, UriKind.Absolute, out var uri) &&
                uri.Host.Equals("hyperio.us"))
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
})
.AddControllers();

var app = builder.Build();
app.UseCors("AllowAnyLocalhost").UseCors("AllowHyperioSubdomains").UseCors("AllowHyperioRoot");

app.MapControllers();

app.UseSwagger(c =>
{
    c.RouteTemplate = "v1/resume/{documentName}/swagger.json"; // Custom route for Swagger JSON
}).UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/v1/resume/v1/swagger.json", "Resume API v1");
    c.RoutePrefix = "v1/resume"; // Serve Swagger UI at /v1/resume
});


//kill this for now....
//app.UseHttpsRedirection();
app.Run();