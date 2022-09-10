using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MeTech.Model.Campaign;
using MeTech.ResponseRequest.Campaign;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeTech.API.Controllers
{
    [Route("api/[controller]")]
    public class CampaignsController : Controller
    {
        private readonly IMediator mediatr;
        public CampaignsController(IMediator mediatr)
        {
            this.mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var request = new CampaignListRequest();
            return Ok(await mediatr.Send(request));
        }
        [HttpPost]
        public async Task<IActionResult> ApplyCampaign([FromBody] CampaignApplyModel campaign)
        {
            var request = new CampaignApplyRequest
            {
                Campaign = campaign
            };
            return Ok(await mediatr.Send(request));
        }

      
    }
}

