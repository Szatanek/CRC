using CRC.Repository.Models;

namespace CRC.Services.Services.StatusStrategy
{
    public interface IRequestStatusStrategy
    {
        void Claim(Request request, string reason);
        void Approve(Request request);
        void Reject(Request request, string reason);
    }
}