using ChatApp.Api.Data;
using ChatApp.Api.Hubs;
using ChatApp.Api.Models;
using ChatApp.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddScoped<IMessageRepository,MessageRepository>();
builder.Services.AddScoped<IChatRepository,ChatRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();

builder.Services.AddDbContext<ChatDbContext>(options =>
    options
    //.UseLazyLoadingProxies()
    .UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentity<ChatUser, UserRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<ChatDbContext>();

builder.Services.AddCors(cors =>
    cors.AddDefaultPolicy(corsPolicy =>
        corsPolicy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("/chathub");
app.Run();
