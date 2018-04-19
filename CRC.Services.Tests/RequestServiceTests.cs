using System.Linq.Expressions;
using CRC.Repository.Abstract;
using CRC.Repository.Enums;
using CRC.Repository.Models;
using CRC.Services.Exceptions;
using CRC.Services.Services;
using CRC.Services.Services.StatusStrategy;
using Moq;
using NUnit.Framework;

namespace CRC.Services.Tests
{
    /// <summary>
    /// Cwiczenie 2.
    /// Aplikacja została uruchomiona Live, użytkownicy zaczynają z niej korzystać.
    /// Po pewnym czasie użytkownicy zaczynają narzekać, ponieważ często dostają Reject na ich Requestach.
    /// Aplikacja nie zapewnia żadnej informacji zwrotnej czemu Request został odwołany.
    /// 
    /// Product Owner aplikacji poprosił nas, żebyśmy dodali pole Reason w obiekcie Request.
    /// Pole Reason jest obowiązkowe podczas akcji Reject.
    /// Aplikacja ma wyrzucać stosowny wyjątek, jeśli pole Reason jest nie uzupełnione przy akcji Reject.
    /// </summary>
    [TestFixture]
    public class RequestServiceTests
    {
        private Mock<IGenericRepository<Request>> _requestRepositoryMock;
        private RequestService _requestService;

        [SetUp]
        public void Setup()
        {
            var strategy = new RequestStatusStrategy();
            _requestRepositoryMock = new Mock<IGenericRepository<Request>>();
            _requestService = new RequestService(_requestRepositoryMock.Object, null, strategy);
        }

        /// <summary>
        /// W tym teście sprawdzimy, czy zapisywany Request ma wypełniony status Reason.
        /// 
        /// W sekcji Given należy przygotować requestRepositoryMock tak, aby dla podanego requestId zwrócił nam obiekt Reuquest.
        /// 
        /// W sekcji Then należy dodać weryfikację w obiekcie requestRepositoryMock.
        /// Weryfikacja powinna się odbyć na metodzie Edit. 
        /// Weryfikacja ma sprawdzać, czy metoda została wykonana z obiektem Request zawierającym uzupełnione pole Reason
        /// </summary>
        [Test]
        public void ShouldSetReasonToRequest()
        {
            // Given
            const int requestId = 5;
            const string reason = "Too much priviledges on Production Environment";
            var testRequest = new Request
            {
                Id = requestId
            };

            _requestRepositoryMock.Setup(r => r.GetById(requestId))
                .Returns(testRequest);

            // When
            _requestService.Reject(requestId, reason);

            // Then
            _requestRepositoryMock.Verify(
                repository => repository.Edit(
                    It.Is<Request>(request => VerifyEditRequest(request, requestId, reason))),
                Times.Once);
        }

        /// <summary>
        /// W tym teście sprawdzimy, czy aplikacja rzuci odpowiednim wyjątkiem, jeśli pole reason jest nieprawidłowe.
        /// 
        /// Należy przygotować test tak, aby był wykonywany zarówno z parametrem 'string.Empty', jak i 'null'.
        /// 
        /// W sekcji Then należy zweryfikować, czy lokalna metoda Reject wyrzuci wyjątek typu 'ReasonRequiredWhenReject'.
        /// </summary>
        /// <param name="reason"></param>
        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenReasonIsNullOrEmpty(string reason)
        {
            // Given
            const int requestId = 5;

            // When
            void Reject()
            {
                _requestService.Reject(requestId, reason);
            }

            // Then
            Assert.That(Reject, Throws.InstanceOf<ReasonRequiredWhenRejectException>());
        }

        /// <summary>
        /// W tym teście sprawdzimy, czy zapisywany Request ma wypełniony status Reason.
        /// 
        /// W sekcji Given należy przygotować requestRepositoryMock tak, aby dla podanego requestId zwrócił nam obiekt Reuquest.
        /// 
        /// W sekcji Then należy dodać weryfikację w obiekcie requestRepositoryMock.
        /// Weryfikacja powinna się odbyć na metodzie Edit. 
        /// Weryfikacja ma sprawdzać, czy metoda została wykonana z obiektem Request zawierającym uzupełnione pole Reason
        /// </summary>
        [Test]
        public void ShouldSetReasonToRequestWhenClaim()
        {
            // Given
            const int requestId = 8;
            const string reason = "I need to do Production Release.";
            var requestToClaim = new Request
            {
                Id = requestId,
                Status = StatusEnum.Rejected
            };

            _requestRepositoryMock.Setup(repository => repository.GetById(requestId))
                .Returns(requestToClaim);

            // When
            _requestService.Claim(requestId, reason);

            // Then
            Assert.That(requestToClaim.Reason, Is.EqualTo(reason));
            _requestRepositoryMock
                .Verify(
                    repository => repository.Edit(
                        It.Is<Request>(request => VerifyEditRequest(request, requestId, reason))),
                    Times.Once);
        }

        private static bool VerifyEditRequest(Request request, int requestId, string reason)
        {
            return request.Id == requestId && request.Reason == reason;
        }
    }
}
