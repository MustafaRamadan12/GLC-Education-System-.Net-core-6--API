using GLC.Core.ExtendUser;
using GLC.Core.IUnitOfWork;
using GLC.Core.MappingProfile;
using GLC.EF;
using GLC.EF.UnitOfWork;
using GLC_SAL.Hub;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<GLCDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(GLCDbContext).Assembly.FullName)
    )
);

// Registering Identity Users and roles
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
  //Default Password Setting
  options.User.RequireUniqueEmail = true;
  options.Password.RequireDigit = true;
  options.Password.RequireUppercase = true;
  options.Password.RequireLowercase = true;
  options.Password.RequireNonAlphanumeric = true;
  options.Password.RequiredLength = 6;
  options.Password.RequiredUniqueChars = 0;

}).AddEntityFrameworkStores<GLCDbContext>()
             .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
//Reset password token
builder.Services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(10));
// Adding Authentication

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
  options.SaveToken = true;
  options.RequireHttpsMetadata = false;
  options.TokenValidationParameters = new TokenValidationParameters()
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidAudience = builder.Configuration["JWT:ValidAudience"],
    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
  };
});

//Registering AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

//Registering UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Ignoring Looping. //Download Package: Microsoft.AspNetCore.Mvc.NewtonJson
builder.Services.AddControllers().AddNewtonsoftJson(
    x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSignalR();
builder.Services.AddCors(options => options.AddPolicy(name: "GLC_Origins",
    policy =>
    {
      policy.
      WithOrigins("http://localhost:4200").
      AllowAnyMethod().
      AllowAnyHeader().
      AllowCredentials().

      SetIsOriginAllowed((hosts) => true);
    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors("GLC_Origins");
app.UseRouting();
app.UseAuthorization();


app.UseHttpsRedirection();
app.UseEndpoints(endpoints =>
{
  endpoints.MapHub<MessageHub>("/chat");
  endpoints.MapControllers();
});

//app.MapControllers();
//app.MapHub<MessageHub>("/offers");



app.Run();
