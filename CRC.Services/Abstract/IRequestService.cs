using CRC.Services.ViewModels;
using System.Collections.Generic;

namespace CRC.Services.Abstract
{
    public interface IRequestService
    {
        IEnumerable<ReadRequestViewModel> GetAllIRequests();
        IEnumerable<ReadRequestViewModel> GetMyRequests(int userId);       
        int CreateNewRequest(CreateRequestViewModel request);
        void Approve(int id);
        void Reject(int id, string reaseon);
        void Delete(int id);
        void Claim(int requestId, string reason);
    }
}
