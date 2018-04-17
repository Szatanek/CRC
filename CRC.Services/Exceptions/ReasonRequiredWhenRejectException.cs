using System;

namespace CRC.Services.Exceptions
{
    public sealed class ReasonRequiredWhenRejectException : Exception
    {
        private const string DomainMessage = "Reason is required when rejecting request.";

        public ReasonRequiredWhenRejectException() 
            : base(DomainMessage)
        {
        }
    }
}