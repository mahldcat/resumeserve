var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
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
    
});

var app = builder.Build();
app.UseCors("AllowAnyLocalhost");
app.UseCors("AllowHyperioSubdomains");
app.UseCors("AllowHyperioRoot");

app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//kill this for now....
//app.UseHttpsRedirection();
app.Run();