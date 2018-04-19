using CRC.Repository.Enums;
using CRC.Repository.Models;
using CRC.Services.Exceptions;
using CRC.Services.Services.StatusStrategy;
using NUnit.Framework;

namespace CRC.Services.Tests
{
    /// <summary>
    /// Cwiczenie 3.
    /// Aplikacja zagościła w naszej firmie na dobre, użytkownicy często z niej korzystają.
    /// Niestety coraz częściej użytkownicy narzekają na odrzucone Requesty, ponieważ muszą tworzyć nowy Request.
    /// 
    /// Product Owner zaproponował dodanie nowej akcji - Claim. Akcja ta będzie zmieniać status Requestu na InProgress.
    /// Akcja Claim ma wymagane pole Reason, dokładnie tak jak akcja Reject.
    /// Akcja Claim może zostać wykonana tylko na statusie Rejected.
    /// 
    /// Padła propozycja, aby logikę zmiany statusu Requestu przenieść do osobnej klasy, aby nie zaciemniać roli klasy RequestService.
    /// W tej klasie przetestujemy również pozostałe akcje - Approve oraz Reject.
    /// </summary>
    [TestFixture]
    public class RequestStatusStrategyTests
    {
        private RequestStatusStrategy _requestStatusStrategy;

        [SetUp]
        public void Setup()
        {
            _requestStatusStrategy = new RequestStatusStrategy();
        }

        /// <summary>
        /// W tym teście zweryfikujemy czy akcja Claim zmienia status obiektu Request na InProgress oraz czy ustawia pole reason.
        /// 
        /// W sekcji Given należy uzupełnić kod utworzenia obiektu RequestStatusStrategy. 
        /// W sekcji When należy uzupełnić kod wywołania akcji.
        /// </summary>
        [Test]
        public void ShouldSetStatusToInProgress()
        {
            // Given
            const StatusEnum expectedStatus = StatusEnum.InProgress;
            const string reason = "I need to do Production Release.";
            var request = new Request
            {
                Status = StatusEnum.Rejected
            };

            // When
            _requestStatusStrategy.Claim(request, reason);

            // Then
            Assert.That(request.Status, Is.EqualTo(expectedStatus));
            Assert.That(request.Reason, Is.EqualTo(reason));
        }

        /// <summary>
        /// W tym teście zweryfikujemy czy akcja Approve zmienia status obiektu Request na Approved.
        /// </summary>
        [Test]
        public void ShouldSetStatusToApproved()
        {
            // Given
            const StatusEnum expectedStatus = StatusEnum.Approved;
            var request = new Request
            {
                Status = StatusEnum.InProgress
            };

            // When
            _requestStatusStrategy.Approve(request);

            // Then
            Assert.That(request.Status, Is.EqualTo(expectedStatus));
        }

        /// <summary>
        /// W tym teście zweryfikujemy czy akcja Reject zmienia status obiektu Request na Rejected oraz czy ustawia pole reason.
        /// </summary>
        [Test]
        public void ShouldSetStatusToRejected()
        {
            // Given
            const StatusEnum expectedStatus = StatusEnum.Rejected;
            const string reason = "Too much priviledges on Production Environment";
            var request = new Request
            {
                Status = StatusEnum.InProgress
            };

            // When
            _requestStatusStrategy.Reject(request, reason);

            // Then
            Assert.That(request.Status, Is.EqualTo(expectedStatus));
        }

        /// <summary>
        /// W tym teście zweryfikujemy, czy akcja Claim wyrzuci wyjątek InvalidRequestStatusWhenClaim.
        /// Wyjątek powinien być wyrzucony, gdy Claim jest w statusie InProgress oraz Approved.
        /// 
        /// W sekcji When należy napisać lokalną metodę wywołującą akcję Claim
        /// W sekcji Then należy zweryfikować, czy lokalna metoda wyrzuci odpowiedni wyjątek
        /// </summary>
        [TestCase(StatusEnum.InProgress)]
        [TestCase(StatusEnum.Approved)]
        public void ClaimShouldThrowExceptionWhenStatusIsNotRejected(StatusEnum status)
        {
            // Given
            const string reason = "I need to do Production Release.";
            var request = new Request
            {
                Status = status
            };

            // When
            void Claim()
            {
                _requestStatusStrategy.Claim(request, reason);
            }

            // Then
            Assert.That(Claim, Throws.InstanceOf<InvalidRequestStatusWhenClaimException>());
        }

        /// <summary>
        /// W tym teście zweryfikujemy, czy akcja Claim wyrzuci wyjątek ReasonRequiredWhenClaimException.
        /// Wyjątek powinien być wyrzucony dla parametrów null oraz pusty string.
        /// 
        /// W sekcji When należy napisać lokalną metodę wywołującą akcję Claim
        /// W sekcji Then należy zweryfikować, czy lokalna metoda wyrzuci odpowiedni wyjątek
        /// </summary>
        [TestCase(null)]
        [TestCase("")]
        public void ClaimShouldThrowExceptionWhenReasonIsNullOrEmpty(string reason)
        {
            // Given
            var request = new Request
            {
                Status = StatusEnum.Rejected,
                Reason = reason
            };

            // When
            void Claim()
            {
                _requestStatusStrategy.Claim(request, reason);
            }

            // Then
            Assert.That(Claim, Throws.InstanceOf<ReasonRequiredWhenClaimException>());
        }

        /// <summary>
        /// W tym teście zweryfikujemy, czy akcja Reject wyrzuci wyjątek ReasonRequiredWhenRejectException.
        /// Test należy uzupełnić analogicznie do poprzedniego testu.
        /// </summary>
        [TestCase(null)]
        [TestCase("")]
        public void RejectShouldThrowExceptionWhenReasonIsNullOrEmpty(string reason)
        {
            // Given
            var request = new Request
            {
                Status = StatusEnum.InProgress,
                Reason = reason
            };

            // When
            void Claim()
            {
                _requestStatusStrategy.Reject(request, reason);
            }

            // Then
            Assert.That(Claim, Throws.InstanceOf<ReasonRequiredWhenRejectException>());
        }

        /// <summary>
        /// W tym teście zweryfikujemy, czy akcja Reject wyrzuci wyjątek InvalidRequestStatusWhenReject.
        /// Wyjątek powinien być wyrzucony, gdy Request jest w statusie Approved oraz Rejected.
        /// 
        /// W sekcji When należy napisać lokalną metodę wywołującą akcję Reject
        /// W sekcji Then należy zweryfikować, czy lokalna metoda wyrzuci odpowiedni wyjątek
        /// </summary>
        [TestCase(StatusEnum.Approved)]
        [TestCase(StatusEnum.Rejected)]
        public void RejectShouldThrowExceptionWhenStatusIsNotInProgress(StatusEnum status)
        {
            // Given
            const string reason = "Too much priviledges on Production Environment";
            var request = new Request
            {
                Status = status
            };

            // When
            void Claim()
            {
                _requestStatusStrategy.Reject(request, reason);
            }

            // Then
            Assert.That(Claim, Throws.InstanceOf<InvalidRequestStatusWhenRejectException>());
        }

        /// <summary>
        /// W tym teście zweryfikujemy, czy akcja Approve wyrzuci wyjątek InvalidRequestStatusWhenApprove.
        /// Wyjątek powinien być wyrzucony, gdy Request jest w statusie Approved oraz Rejected.
        /// 
        /// W sekcji When należy napisać lokalną metodę wywołującą akcję Approve
        /// W sekcji Then należy zweryfikować, czy lokalna metoda wyrzuci odpowiedni wyjątek
        /// </summary>
        [TestCase(StatusEnum.Approved)]
        [TestCase(StatusEnum.Rejected)]
        public void ApproveShouldThrowExceptionWhenStatusIsNotInProgress(StatusEnum status)
        {
            // Given
            var request = new Request
            {
                Status = status
            };

            // When
            void Approve()
            {
                _requestStatusStrategy.Approve(request);
            }

            // Then
            Assert.That(Approve, Throws.InstanceOf<InvalidRequestStatusWhenApproveException>());
        }
    }
}
