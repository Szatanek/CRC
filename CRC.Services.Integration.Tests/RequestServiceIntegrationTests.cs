using System;
using System.Linq;
using CRC.Repository.Enums;
using CRC.Repository.Models;
using CRC.Repository.Repository;
using CRC.Services.Integration.Tests.Base;
using CRC.Services.Services;
using CRC.Services.Services.StatusStrategy;
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
    public class RequestServiceIntegrationTests : BaseIntegrationTest
    {
        private RequestService _requestService;

        [SetUp]
        public void Setup()
        {
            var requestRepository = new GenericRepository<Request>(Context);
            var permissionRepository = new GenericRepository<ProvisionedPermission>(Context);
            var requestStatusStrategy = new RequestStatusStrategy();
            _requestService = new RequestService(requestRepository, permissionRepository, requestStatusStrategy);
        }
        
        [Test]
        public void ShouldApproveRequestAndCreatePermission()
        {
            // Given
            const int requestId = 3;
            const int myTestUserId = 8;
            const PermissionsEnum expectedPermission = PermissionsEnum.Hpa;
            const ServersEnum expectedServer = ServersEnum.Dev;
            const StatusEnum expectedStatus = StatusEnum.Approved;
            Context.Users.Add(CreateTestUser(myTestUserId));
            Context.Requests.Add(CreateTestRequest(requestId, myTestUserId, expectedPermission, expectedServer));
            Context.SaveChanges();

            // When
            _requestService.Approve(requestId);

            // Then
            var request = Context.Requests.FirstOrDefault(r => r.Id == requestId);
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Status, Is.EqualTo(expectedStatus));
            Assert.That(request.ServerName, Is.EqualTo(expectedServer));

            var permissions = Context.ProvisionedPermissions.Where(p => p.UserId == myTestUserId).ToList();
            Assert.That(permissions, Is.Not.Empty);

            var myExpectedPermission = permissions.FirstOrDefault(p => p.ServerName == expectedServer && p.Permission == expectedPermission);
            Assert.That(myExpectedPermission, Is.Not.Null);
        }

        [Test]
        public void ShouldRejectRequestAndNotCreatePermission()
        {
            // Given
            const int requestId = 1;
            const int myTestUserId = 7;
            const StatusEnum expectedStatus = StatusEnum.Rejected;
            Context.Users.Add(CreateTestUser(myTestUserId));
            Context.Requests.Add(CreateTestRequest(requestId, myTestUserId));
            Context.SaveChanges();

            const string reason = "Too much priviledges on Production Environment";

            // When
            _requestService.Reject(requestId, reason);

            // Then
            var request = Context.Requests.FirstOrDefault(r => r.Id == requestId);
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Status, Is.EqualTo(expectedStatus));
            Assert.That(request.Reason, Is.EqualTo(reason));

            var permissions = Context.ProvisionedPermissions.Where(p => p.UserId == myTestUserId).ToList();
            Assert.That(permissions, Is.Empty);
        }

        [Test]
        public void ShouldClaimRequestAndSetStatusToInProgress()
        {
            // Given
            const int requestId = 12;
            const int myTestUserId = 9;
            const StatusEnum expectedStatus = StatusEnum.InProgress;

            Context.Users.Add(CreateTestUser(myTestUserId));
            Context.Requests.Add(CreateRejectedRequest(requestId, myTestUserId));
            Context.SaveChanges();

            const string reason = "Too much priviledges on Production Environment";

            // When
            _requestService.Claim(requestId, reason);

            // Then
            var request = Context.Requests.FirstOrDefault(r => r.Id == requestId);
            Assert.That(request, Is.Not.Null);
            Assert.That(request.Status, Is.EqualTo(expectedStatus));
            Assert.That(request.Reason, Is.EqualTo(reason));
        }

        private static User CreateTestUser(int userId)
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

        private static Request CreateTestRequest(
            int requestId,
            int userId,
            PermissionsEnum permissionType = PermissionsEnum.Admin,
            ServersEnum serverType = ServersEnum.Prd,
            StatusEnum initialStatus = StatusEnum.InProgress)
        {
            return new Request
            {
                Id = requestId,
                AdditionalInfo = "Test info",
                Permission = permissionType,
                Status = initialStatus,
                RequestedAt = DateTime.Now,
                UserId = userId,
                ServerName = serverType,
                ServerAddress = "Test server address"
            };
        }

        private static Request CreateRejectedRequest(
            int requestId,
            int userId,
            PermissionsEnum permission = PermissionsEnum.Admin,
            ServersEnum serverType = ServersEnum.Prd)
        {
            return CreateTestRequest(requestId, userId, permission, serverType, StatusEnum.Rejected);
        }
    }
}
