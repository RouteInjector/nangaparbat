using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using NangaParbat.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddNangaParbat(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.EnableTryItOutByDefault();
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
