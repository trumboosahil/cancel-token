using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CancelAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CancelController : ControllerBase
    {
        private readonly ILogger<CancelController> _logger;

       

        public CancelController(ILogger<CancelController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get(CancellationToken token)
        {
            try
            {
                DataAccess dataAccess = new DataAccess();
                var result = await dataAccess.GetData(token);
                return Ok(result);
            }
            catch (OperationCanceledException  ex)
            {
                return StatusCode(420, "Cancel by user");
            }

        }
    }
}
