

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 

builder.Services.AddDependencies();
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
