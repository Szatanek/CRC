using System.Collections.Generic;
using CRC.Filters;
using Microsoft.AspNetCore.Mvc;
using CRC.Services.Abstract;
using CRC.Services.ViewModels;

namespace CRC.Controllers
{
    [Route("api/request/")]   
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }
      
        [HttpGet("getAllRequests")]
        public IEnumerable<ReadRequestViewModel> GetAllRequests()
        {
            return _requestService.GetAllIRequests();
        }

        [HttpGet("getMyRequests/{id}")]
        public IEnumerable<ReadRequestViewModel> GetMyRequests(int id)
        {
            return _requestService.GetMyRequests(id);
        }       
      
        [HttpPost]
        [ValidationFilter]
        public IActionResult CreateRequest([FromBody]CreateRequestViewModel request) 
        {           
            _requestService.CreateNewRequest(request);
            return Ok();
        }

        [HttpPut("approve/{id}")]
        public IActionResult Approve(int id) 
        {           
            _requestService.Approve(id);
            return Ok();
        }

        [HttpPut("reject/{id}")]
        public IActionResult Reject(int id)
        {
            _requestService.Reject(id);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _requestService.Delete(id);
            return Ok();
        }
    }
}