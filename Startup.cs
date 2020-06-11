using System.Text;
using API.Mail;
using API.Models;
using API.Repositories.Implement;
using API.Repositories.Interface;
using API.Repository.Interface;
using API.Security.Jwt;
using API.Security.Mail;
using API.Security.ServerName;
using API.Security.UserAccessor;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var emailConfig = Configuration
                                            .GetSection("EmailConfiguration")
                                            .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            services.AddControllers();

            var key = Encoding.UTF8.GetBytes(Configuration["TokenKey"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            // DI automapper
            services.AddAutoMapper(typeof(Startup));

            // DI của dbContext
            services.AddDbContext<DataContext>(
                opt =>
                {
                    opt.UseLazyLoadingProxies();
                    opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
                });


            // DI automapper
            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IHdxRepo, HdxRepo>();

            services.AddTransient<INhanVienRepo, NhanVienRepo>();

            services.AddTransient<IBaoHanhRepo, BaoHanhRepo>();

            services.AddTransient<IHdnRepo, HdnRepo>();

            services.AddTransient<IKhachHangRepo, KhachHangRepo>();

            services.AddTransient<IKhoRepo, KhoRepo>();

            services.AddTransient<ILoaiXeRepo, LoaiXeRepo>();

            services.AddTransient<INccRepo, NccRepo>();

            services.AddTransient<IXeRepo, XeRepo>();

            services.AddTransient<IUserRepo, UserRepo>();

            services.AddScoped<IJwtgenerator, Jwtgenerator>();

            services.AddScoped<IUserAccessor, UserAccessor>();

            services.AddScoped<IServerName, ServerName>();

            services.AddScoped<IDoanhThu, DoanhThuRepo>();

            services.AddScoped<IEmailSender, EmailSender>();

            services.AddHttpContextAccessor();


            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:3000", "http://192.168.1.6:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });


            services.AddMvc();


            services.AddMvc(opt =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
