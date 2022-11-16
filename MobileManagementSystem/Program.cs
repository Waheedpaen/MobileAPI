

using MobileManagementSystem.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 




builder.Services.AddDependencies();
builder.Services.AddIdentityServices(builder.Configuration);
 
 
// Add services to the container.
//builder.swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
//{
//    Name = "Authorization",
//    Type = SecuritySchemeType.ApiKey,
//    Scheme = "Bearer",
//    BearerFormat = "JWT",
//    In = ParameterLocation.Header,
//    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
 
 
 
 

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
