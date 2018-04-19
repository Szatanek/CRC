using AutoMapper;
using CRC.Repository.Abstract;
using CRC.Repository.Models;
using CRC.Services.Abstract;
using CRC.Services.ViewModels;
using System.Collections.Generic;
using System.Linq;
using CRC.Repository.Enums;
using CRC.Services.Exceptions;
using CRC.Services.Services.StatusStrategy;
using Microsoft.EntityFrameworkCore;

namespace CRC.Services.Services
{
    public class RequestService : IRequestService
    {
        private readonly IGenericRepository<Request> _requestRepository;
        private readonly IGenericRepository<ProvisionedPermission> _permissionRepository;
        private readonly IRequestStatusStrategy _requestStatusStrategy;

        public RequestService(
            IGenericRepository<Request> requestRepository,
            IGenericRepository<ProvisionedPermission> permissionRepository,
            IRequestStatusStrategy requestStatusStrategy)
        {
            _requestRepository = requestRepository;
            _permissionRepository = permissionRepository;
            _requestStatusStrategy = requestStatusStrategy;
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
            _requestStatusStrategy.Approve(request);
            _requestRepository.Edit(request);

            //create permission object
            var provisionedPermission = Mapper.Map<ProvisionedPermission>(request);
            _permissionRepository.Add(provisionedPermission);
        }

        public void Reject(int id, string reason)
        {
            var request = _requestRepository.GetById(id);
            _requestStatusStrategy.Reject(request, reason);
            _requestRepository.Edit(request);           
        }

        public void Delete(int id)
        {
            var request = _requestRepository.GetById(id);
            _requestRepository.Delete(request);
        }

        public void Claim(int id, string reason)
        {
            var request = _requestRepository.GetById(id);
            _requestStatusStrategy.Claim(request, reason);
            _requestRepository.Edit(request);
        }
    }
}
