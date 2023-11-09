using Microsoft.AspNetCore.Mvc;
using NMH_WebAPI.Helpers;
using NMH_WebAPI.Messaging;
using NMH_WebAPI.Models;

namespace NMH_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculationController : ControllerBase
    {
        private readonly ILogger<CalculationController> _logger;
        private readonly IProcessHelper _processHelper;
        private readonly IProducer _producer;

        public CalculationController(ILogger<CalculationController> logger, IProcessHelper processHelper, IProducer producer)
        {
            _logger = logger;
            _processHelper = processHelper;
            _producer = producer;
        }

        [HttpPost]
        [Route("/{key}")]
        public OutputModel ProcessKey(int key, InputModel inputModel)
        {
            var processedItem = _processHelper.ProcessKey(key, inputModel.Input);
            //_producer.SendMessage(processedItem);
            return processedItem;
        }
    }
}