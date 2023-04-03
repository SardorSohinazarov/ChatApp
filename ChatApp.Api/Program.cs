using ChatApp.Api.Hubs;
using ChatApp.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseInMemoryDatabase("ChatApp"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>();

builder.Services.AddCors(cors =>
    cors.AddDefaultPolicy(corsPolicy =>
        corsPolicy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()));

builder.Services.AddSingleton<MessagesService>();


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
