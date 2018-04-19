using System;

namespace CRC.Services.Exceptions
{
    public sealed class InvalidRequestStatusWhenClaimException : Exception
    {
        private const string DomainMessage = "Request can be claimed only in Rejected status.";

        public InvalidRequestStatusWhenClaimException()
            : base(DomainMessage)
        {
        }
    }
}