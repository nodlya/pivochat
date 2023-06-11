using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PivoChat;
using PivoChat.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ChatContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options => {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
   })
      .AddJwtBearer(options => {
      options.TokenValidationParameters = new TokenValidationParameters 
      {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ClockSkew = TimeSpan.Zero,
         ValidIssuer = "AuthOptions.ISSUER",
         ValidAudience = "AuthOptions.AUDIENCE",
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("very_secret_and_complex_key_12345"))
      };
   });
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
   options.AddPolicy("CorsPolicy",
      builder => builder
         .AllowAnyMethod()
         .AllowCredentials()
         .SetIsOriginAllowed((host) => true)
         .AllowAnyHeader());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");

app.UseRouting();

app.UseEndpoints(endpoints =>
{
   endpoints.MapControllers();
   endpoints.MapHub<ChatHub>("/socket");
});

app.UseHttpsRedirection();

using (var scope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ChatContext>();
      //  context.Database.Migrate();
}


app.Run();