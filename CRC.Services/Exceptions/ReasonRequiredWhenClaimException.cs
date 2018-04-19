using System;

namespace CRC.Services.Exceptions
{
    public sealed class ReasonRequiredWhenClaimException : Exception
    {
        private const string DomainMessage = "Reason is required when claiming a request.";

        public ReasonRequiredWhenClaimException()
            : base(DomainMessage)
        {
        }
    }
}