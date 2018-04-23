using AutoMapper;
using CRC.Repository.Context;
using CRC.Repository.Enums;
using CRC.Repository.Models;
using CRC.Services.Integration.Tests.Extensions;
using CRC.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CRC.Services.Integration.Tests.Base
{
    public abstract class BaseIntegrationTest
    {
        private AppContext _context;

        protected AppContext Context => _context;

        [OneTimeSetUp]
        public void SetupInitDatabase()
        {
            var options = new DbContextOptionsBuilder<AppContext>()
                .UseInMemoryDatabase(databaseName: "CrcTests")
                .Options;
            _context = new AppContext(options);

            ConfigureMapper();
        }

        [TearDown]
        public void CleanupDatabase()
        {
            _context.ProvisionedPermissions.Clear();
            _context.Requests.Clear();
            _context.RolesSimplies.Clear();
            _context.Roles.Clear();
            _context.Users.Clear();
            _context.SaveChanges();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }

        }

        private void ConfigureMapper()
        {
            Mapper.Reset();
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
    }
}
