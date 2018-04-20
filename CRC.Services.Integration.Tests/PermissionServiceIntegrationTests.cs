using System;
using System.Linq;
using CRC.Repository.Enums;
using CRC.Repository.Models;
using CRC.Repository.Repository;
using CRC.Services.Integration.Tests.Base;
using CRC.Services.Services;
using NUnit.Framework;

namespace CRC.Services.Integration.Tests
{
    /// <summary>
    /// Cwiczenie 4
    /// Aktualnie w aplikacji mamy przetestowaną warstwę logiki.
    /// Dobrze jest napisać kilka testów weryfikujących poprawne zaciąganie danych z bazy.
    /// 
    /// Przygotowaliśmy już infrastrukturę do tworzenia pustej bazy danych.
    /// Należy napisać kod inicializujący dane w bazie potrzebne do przeprowadzenia testu.
    /// Po wykonaniu testu infrastruktura testu powinna wyczyścić dane.
    /// </summary>
    [TestFixture]
    public class PermissionServiceIntegrationTests : BaseIntegrationTest
    {
        private PermissionService _permissionService;

        [SetUp]
        public void Setup()
        {
            var permissionRepository = new GenericRepository<ProvisionedPermission>(Context);
            _permissionService = new PermissionService(permissionRepository);
        }

        /// <summary>
        /// W tym teście sprawdzimy czy dla istniejącego użytkownika zwrócone zostaną jego uprawnienia.
        /// 
        /// W sekcji Given należy wpisać kod inicjalizujący dane w bazie danych.
        /// 
        /// W sekcji Then należy wpisać kod sprawdzający czy liczba uprawnień jest równa oczekiwanej liczbie.
        /// </summary>
        [Test]
        public void ShouldReturnShouldReturnPermissionsForExistingUser()
        {
            // Given
            const int expectedPermissionsCount = 3;
            const int myTestUserId = 8;
            Context.Users.Add(CreateTestUser(myTestUserId));
            Context.ProvisionedPermissions.Add(CreatePermissionForUser(myTestUserId, PermissionsEnum.Admin, ServersEnum.Dev));
            Context.ProvisionedPermissions.Add(CreatePermissionForUser(myTestUserId, PermissionsEnum.Hpa, ServersEnum.Acc));
            Context.ProvisionedPermissions.Add(CreatePermissionForUser(myTestUserId, PermissionsEnum.ReadOnly, ServersEnum.Prd));
            Context.SaveChanges();

            // When
            var requests = _permissionService.GetMyPermissions(myTestUserId);

            // Then
            Assert.That(requests, Is.Not.Null);
            Assert.That(requests, Is.Not.Empty);
            Assert.That(requests.Count(), Is.EqualTo(expectedPermissionsCount));
        }

        /// <summary>
        /// W tym teście sprawdzimy, czy dla istniejącego użytkownika bez uprawnień zostanie zwrócona pusta kolekcja uprawnień.
        /// 
        /// W sekcji Given należy wpisać kod inicjalizujący dane w bazie.
        /// 
        /// W sekcji When należy wywołać metodę GetMyPermissions dla Id myTestUserId.
        /// 
        /// W sekcji Then należy zweryfikować, czy kolekcja zwróconych uprawnień nie jest nullem, ale jest pusta.
        /// </summary>
        [Test]
        public void ShouldReturnNoPermissionsWhenUserHasNoPermissions()
        {
            // Given
            const int myTestUserId = 3;
            const int notMyUserId = 2;
            Context.Users.Add(CreateTestUser(myTestUserId));
            Context.Users.Add(CreateTestUser(notMyUserId));
            Context.ProvisionedPermissions.Add(CreatePermissionForUser(notMyUserId, PermissionsEnum.Admin, ServersEnum.Dev));

            // When
            var requests = _permissionService.GetMyPermissions(myTestUserId);

            // Then
            Assert.That(requests, Is.Not.Null);
            Assert.That(requests, Is.Empty);
        }

        private User CreateTestUser(int userId)
        {
            return new User
            {
                Id = userId,
                IsLogin = true,
                Login = "Pawcio123",
                Name = "Henryk Maślanka",
                Password = "Test123"
            };
        }

        private ProvisionedPermission CreatePermissionForUser(int userId, PermissionsEnum permission, ServersEnum server)
        {
            return new ProvisionedPermission
            {
                UserId = userId,
                ApprovedAt = DateTime.Now,
                Permission = permission,
                ServerName = server,
                ServerAddress = "Test"
            };
        }
    }
}
