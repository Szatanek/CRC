using System.Collections.Generic;
using AutoMapper;
using CRC.Repository.Abstract;
using CRC.Repository.Context;
using CRC.Repository.Enums;
using CRC.Repository.Models;
using CRC.Repository.Repository;
using CRC.Services.Abstract;
using CRC.Services.Services;
using CRC.Services.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace CRC
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
            services.AddDbContext<AppContext>(opt => opt.UseInMemoryDatabase("CRC"));
            services.AddMvc();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRequestService, RequestService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IRoleService, RoleService>();

            services.AddCors();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "CRC API", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRC API V1"); });

            app.UseCors(
                options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
            );
            app.UseMvc();

        ConfigureMapper();

            //generate data
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppContext>();
                GenerateTestData(context);
            }
        }

        private void ConfigureMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<Request, CreateRequestViewModel>();
                cfg.CreateMap<Request, ReadRequestViewModel>()
                    .ForMember(x => x.ServerName, opt => opt.MapFrom(src => src.ServerName.Description()))
                    .ForMember(x => x.Permission, opt => opt.MapFrom(src => src.Permission.Description()))
                    .ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status.Description()))
                    .ForMember(x => x.Name, opt => opt.MapFrom(src => src.User.Name));
                cfg.CreateMap<CreateRequestViewModel, Request>()
                    .ForMember(x => x.User, opt => opt.Ignore());
                cfg.CreateMap<Request, ProvisionedPermission>();                   
                cfg.CreateMap<ProvisionedPermission, ProvisionedPermissionViewModel>()
                    .ForMember(x => x.ServerName, opt => opt.MapFrom(src => src.ServerName.Description()))
                    .ForMember(x => x.Permission, opt => opt.MapFrom(src => src.Permission.Description()));
            });
        }

        private void GenerateTestData(AppContext context)
        {
             var rolesSimplies = new[]
            {
                new RoleSimply
                {
                    Id = 1,
                    Login = "henryk",
                    Roles = new List<Role>
                    {
                        new Role{ Name ="approver"},
                        new Role{ Name ="user"}
                    }
                },

                new RoleSimply
                {
                    Id = 2,
                    Login = "staszek",
                    Roles = new List<Role>
                    {
                        new Role{ Name ="approver"},
                        new Role{ Name ="user"}
                    }
                }
            };

            var users = new[]
            {
                new User
                {
                    Id = 1,
                    IsLogin = false,
                    Name = "Henryk Ciekawski",
                    Login = "henryk",
                    Password = "henryk"
                },

                new User
                {
                    Id = 2,
                    IsLogin = false,
                    Name = "Staszek Upierdliwy",
                    Login = "staszek",
                    Password = "staszek"
                },

                new User
                {
                    Id = 3,
                    IsLogin = true,
                    Name = "Admin",
                    Login = "admin",
                    Password = "admin"
                }
            };

            var requests = new[]
            {
                new Request
                {
                    Id = 1,
                    ServerName = ServersEnum.Acc,
                    ServerAddress = "kfejk.pl",
                    User = users[0],
                    UserId = users[0].Id,
                    Permission = PermissionsEnum.ReadOnly,
                    Status = StatusEnum.Approved
                },


                new Request
                {
                    Id = 2,
                    ServerName = ServersEnum.Prd,
                    ServerAddress = "yyy.pl",
                    User = users[1],
                    UserId = users[1].Id,
                    Permission = PermissionsEnum.ReadOnly,
                    Status = StatusEnum.Rejected
                }
            };

            var provinsioned = new[]
            {
                new ProvisionedPermission
                {
                    Id = 1,
                    User = users[0],
                    Permission = PermissionsEnum.ReadOnly,
                    ServerAddress = "adres",
                    ServerName = ServersEnum.Acc, 
                    UserId = users[0].Id,                  
                },

                new ProvisionedPermission
                {
                    Id = 2,
                    User = users[1],
                    Permission = PermissionsEnum.Hpa,
                    ServerAddress = "adresssss",
                    ServerName = ServersEnum.Tst,
                    UserId = users[1].Id,                    
                }
            };

            context.Users.AddRange(users);
            context.Requests.AddRange(requests);
            context.ProvisionedPermissions.AddRange(provinsioned);
            context.RolesSimplies.AddRange(rolesSimplies);

            context.SaveChanges();
        }
    }
}