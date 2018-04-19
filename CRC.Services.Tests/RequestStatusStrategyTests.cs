using CRC.Repository.Enums;
using CRC.Repository.Models;
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
        /// <summary>
        /// W tym teście zweryfikujemy czy akcja Claim zmienia status obiektu Request na InProgress.
        /// 
        /// W sekcji Given należy uzupełnić kod utworzenia obiektu RequestStatusStrategy. 
        /// W sekcji When należy uzupełnić kod wywołania akcji.
        /// </summary>
        [Test]
        public void ShouldSetStatusToInProgress()
        {
            // Given
            const StatusEnum expectedStatus = StatusEnum.InProgress;
            var request = new Request();
            // TODO: utworzenie obiektu ServiceRequestStrategy.

            // When
            // TODO: wywołanie akcji Claim

            // Then
            Assert.That(request.Status, Is.EqualTo(expectedStatus));
        }

        /// <summary>
        /// W tym teście zweryfikujemy czy akcja Approve zmienia status obiektu Request na Approved.
        /// </summary>
        [Test]
        public void ShouldSetStatusToApproved()
        {
            // Given
            // TODO: Przygotowanie testu analogicznie do poprzedniego testu.

            // When
            // TODO: Wywołanie akcji

            // Then
            // TODO: Weryfikacja wykonania akcji
        }

        /// <summary>
        /// W tym teście zweryfikujemy czy akcja Reject zmienia status obiektu Request na Rejected.
        /// </summary>
        [Test]
        public void ShouldSetStatusToRejected()
        {
            // Given
            // TODO: Przygotowanie testu analogicznie do poprzedniego testu.

            // When
            // TODO: Wywołanie akcji

            // Then
            // TODO: Weryfikacja wykonania akcji
        }

        /// <summary>
        /// W tym teście zweryfikujemy, czy akcja Claim wyrzuci wyjątek ReasonRequiredWhenClaimException.
        /// Wyjątek powinien być wyrzucony dla parametrów null oraz pusty string.
        /// 
        /// W sekcji When należy napisać lokalną metodę wywołującą akcję Claim
        /// W sekcji Then należy zweryfikować, czy lokalna metoda wyrzuci odpowiedni wyjątek
        /// </summary>
        public void ClaimShouldThrowExceptionWhenReasonIsNullOrEmpty(string reason)
        {
            // Given
            var request = new Request
            {
                Reason = reason
            };

            // When
            // TODO: Napisanie lokalnej metody wywołującej akcję Claim

            // Then
            // TODO: Weryfikacja, czy akcja Claim wyrzuci wyjątek ReasonRequiredWhenClaimException
        }

        /// <summary>
        /// W tym teście zweryfikujemy, czy akcja Reject wyrzuci wyjątek ReasonRequiredWhenRejectException.
        /// Test należy uzupełnić analogicznie do poprzedniego testu.
        /// </summary>
        public void RejectShouldThrowExceptionWhenReasonIsNullOrEmpty(string reason)
        {
            // Given
            // TODO: Przygotowanie testu analogicznie do poprzedniego testu

            // When
            // TODO: Napisanie lokalnej metody wywołującej akcję Reject

            // Then
            // TODO: Weryfikacja, czy akcja Reject wyrzuci wyjątek ReasonRequiredWhenRejectException
        }
    }
}
