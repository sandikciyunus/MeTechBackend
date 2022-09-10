using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MeTech.ResponseRequest.Product;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeTech.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IMediator mediatr;
        public ProductsController(IMediator mediatr)
        {
            this.mediatr = mediatr;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var request = new ProductListRequest();
            return Ok(await mediatr.Send(request));
        }

      
    }
}

