using CRC.Repository.Enums;
using CRC.Repository.Models;
using CRC.Services.Exceptions;

namespace CRC.Services.Services.StatusStrategy
{
    public sealed class RequestStatusStrategy : IRequestStatusStrategy
    {
        public void Claim(Request request, string reason)
        {
            if (request.Status != StatusEnum.Rejected)
            {
                throw new InvalidRequestStatusWhenClaimException();
            }

            if (string.IsNullOrEmpty(reason))
            {
                throw new ReasonRequiredWhenClaimException();
            }

            request.Status = StatusEnum.InProgress;
            request.Reason = reason;
        }

        public void Approve(Request request)
        {
            if (request.Status != StatusEnum.InProgress)
            {
                throw new InvalidRequestStatusWhenApproveException();
            }

            request.Status = StatusEnum.Approved;
        }

        public void Reject(Request request, string reason)
        {
            if (string.IsNullOrEmpty(reason))
            {
                throw new ReasonRequiredWhenRejectException();
            }

            if (request.Status != StatusEnum.InProgress)
            {
                throw new InvalidRequestStatusWhenRejectException();
            }

            request.Status = StatusEnum.Rejected;
            request.Reason = reason;
        }
    }
}