using System;
using NUnit.Framework;

namespace CRC.Services.Tests
{
    [TestFixture]
    public class DateTimeFormatterTests
    {
        /// <summary>
        /// Cwiczenie 1.
        /// Pojawiło się nowe wymaganie w aplikacji: Request oraz Permission mają mieć daty utworzenia. 
        /// Daty te mają zostać sformatowane w postaci 'DD-MM-YYYY HH:mm' podczas zwracania z API.
        ///  
        /// W sekcjach When oraz Then należy wpisać poprawny kod walidujący formatowanie daty.
        /// W efekcie powinniśmy uzyskać nową klasę DateFormatter z metodą ToShortDateTimeString(DateTime dateToFormat).
        /// </summary>
        [Test]
        public void ShouldFormatToShortDateAndTime()
        {
            // Given
            const string expectedDateTimeFormatted = "01-01-2018";
            var dateToFormat = new DateTime(2018, 1, 1);

            // TODO: Execute ToShortDateTimeString Method on DateFormatter class.
            // When

            // TODO: Assert That result is equal to expectedDateTimeFormatted.
            // Then
        }
    }
}
