using System;
using CRC.Repository.Enums;
using CRC.Repository.Models;
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
            // TODO: Initialize PermissionService with Repository. Use AppContext from base class.
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
            const int myTestUser = 8;
            
            // TODO: Initialize database - create User and add Permissions to this User

            // When
            var requests = _permissionService.GetMyPermissions(myTestUser);

            // Then
            Assert.That(requests, Is.Not.Null);
            Assert.That(requests, Is.Not.Empty);
            // TODO: Verify if permission count is equal to expected.
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

            // TODO: Initialize Users with myTestUserId and notMyUserId. Add Permissions to notMyUserId.

            // When
            // TODO: Get permissions for MyTestUserId.

            // Then
            // TODO: Verify if myTestUserPermissions is not null, but is empty.
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
