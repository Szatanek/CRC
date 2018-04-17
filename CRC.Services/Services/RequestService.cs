using AutoMapper;
using CRC.Repository.Abstract;
using CRC.Repository.Models;
using CRC.Services.Abstract;
using CRC.Services.ViewModels;
using System.Collections.Generic;
using System.Linq;
using CRC.Repository.Enums;
using CRC.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CRC.Services.Services
{
    public class RequestService : IRequestService
    {
        private readonly IGenericRepository<Request> _requestRepository;
        private readonly IGenericRepository<ProvisionedPermission> _permissionRepository;

        public RequestService(IGenericRepository<Request> requestRepository, IGenericRepository<ProvisionedPermission> permissionRepository)
        {
            _requestRepository = requestRepository;
            _permissionRepository = permissionRepository;
        }
        public IEnumerable<ReadRequestViewModel> GetAllIRequests()
        {
            var requests = _requestRepository.GetAll().Include(x => x.User);
            return Mapper.Map<IEnumerable<Request>, IEnumerable<ReadRequestViewModel>>(requests);
        }

        public IEnumerable<ReadRequestViewModel> GetMyRequests(int userId)
        {
            var requests = _requestRepository.GetAll().Where(r => r.UserId == userId);
            return Mapper.Map<IEnumerable<Request>, IEnumerable<ReadRequestViewModel>>(requests);
        }      

        public int CreateNewRequest(CreateRequestViewModel requestVm)
        {
            var request = Mapper.Map<Request>(requestVm);
            request.Status = StatusEnum.InProgress; 
            return _requestRepository.Add(request);
        }

        public void Approve(int id)
        {
            var request = _requestRepository.GetById(id);
            request.Status = StatusEnum.Approved;
            _requestRepository.Edit(request);

            //create permission object
            var provisionedPermission = Mapper.Map<ProvisionedPermission>(request);
            _permissionRepository.Add(provisionedPermission);
        }

        public void Reject(int id, string reason)
        {
            if (string.IsNullOrEmpty(reason))
            {
                throw new ReasonRequiredWhenRejectException();
            }

            var request = _requestRepository.GetById(id);
            request.Status = StatusEnum.Rejected;
            request.Reason = reason;
            _requestRepository.Edit(request);           
        }

        public void Delete(int id)
        {
            var request = _requestRepository.GetById(id);
            _requestRepository.Delete(request);
        }
    }
}
