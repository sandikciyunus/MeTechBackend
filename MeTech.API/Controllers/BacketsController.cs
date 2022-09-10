using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MeTech.Model.Backet;
using MeTech.ResponseRequest.Backet;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeTech.API.Controllers
{
    [Route("api/[controller]")]
    public class BacketsController : Controller
    {
        private readonly IMediator mediatr;
        public BacketsController(IMediator mediatr)
        {
            this.mediatr = mediatr;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BacketAddModel backet)
        {
            var request = new BacketAddRequest
            {
                Backet = backet
            };
            var response=await mediatr.Send(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BacketUpdateModel backet)
        {
            var request = new BacketUpdateRequest
            {
                Backet = backet
            };
            var response = await mediatr.Send(request);
            return Ok(response);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var request = new BacketGetRequest
            {
                Id = id
            };
            var response = await mediatr.Send(request);
            return Ok(response);
        }

    }
}

