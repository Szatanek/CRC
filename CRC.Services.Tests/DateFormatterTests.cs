using System;
using CRC.Services.Formatters;
using NUnit.Framework;

namespace CRC.Services.Tests
{
    /// <summary>
    /// Cwiczenie 1.
    /// Pojawiło się nowe wymaganie w aplikacji: Request oraz Permission mają mieć daty utworzenia. 
    /// Daty te mają zostać sformatowane w postaci 'dd-MM-yyyy HH:mm' podczas zwracania z API.
    /// </summary>
    [TestFixture]
    public class DateTimeFormatterTests
    {
        /// <summary>
        /// W tym teście zweryfikujemy czy podana data zostanie odpowiednio sformatowana.
        /// 
        /// W sekcjach When oraz Then należy wpisać poprawny kod walidujący formatowanie daty.
        /// W efekcie powinniśmy uzyskać nową klasę DateFormatter z metodą ToShortDateTimeString(DateTime dateToFormat).
        /// </summary>
        [Test]
        public void ShouldFormatToShortDateAndTime()
        {
            // Given
            const string expectedDateTimeFormatted = "01-01-2018 14:37";
            var dateToFormat = new DateTime(2018, 1, 1, 14, 37, 25);

            // When
            var result = DateFormatter.ToShortDateTimeString(dateToFormat);

            // Then
            Assert.That(result, Is.EqualTo(expectedDateTimeFormatted));
        }
    }
}
