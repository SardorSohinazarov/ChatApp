using ChatApp.Api;
using ChatApp.Api.Data;
using ChatApp.Api.Hubs;
using ChatApp.Api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    options.TokenValidationParameters = TokenService.GetTokenValidationParameters(builder.Configuration));

builder.Services.AddTransient<TokenService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IMessageRepository,MessageRepository>();
builder.Services.AddScoped<IChatRepository,ChatRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();

builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.ConfigureSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Chat.Api",
        Version = "v1"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "MyAllowSpecificOrigins",
        configurePolicy: configurePolicy =>
        {
            configurePolicy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyAllowSpecificOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("/chathub");
app.Run();
