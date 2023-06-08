using JGRFoundation.API.Data;
using JGRFoundation.API.Helpers.AbstractFactory;
using JGRFoundation.API.Helpers.AbstractFactory.factory;
using JGRFoundation.API.Helpers.AbstractFactory.factory.HighConsume;
using JGRFoundation.API.Helpers.AbstractFactory.factory.LowConsume;
using Microsoft.AspNetCore.Mvc;

namespace JGRFoundation.API.Controller
{
    [ApiController]
    [Route("/api/PhotovoltaicEquipment")]
    public class PhotovoltaicEquipmentController : ControllerBase
    {
        private readonly DataContext _context;

        public PhotovoltaicEquipmentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetPhotovoltaicEquipmentHighConsume")]
        public async Task<ActionResult> GetPhotovoltaicEquipmentHighConsumeAsync()
        {
            try
            {
                IConsumeAbstractFactory Caf = new HighConsumeFactory();
                Client client = new Client(Caf);
                var photovoltaicEquipmentDTO = client.Operation();
                if (photovoltaicEquipmentDTO is null)
                {
                    return NotFound();
                }
                return Ok(photovoltaicEquipmentDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPhotovoltaicEquipmentLowConsume")]
        public async Task<ActionResult> GetPhotovoltaicEquipmentLowConsumeAsync()
        {
            try
            {
                IConsumeAbstractFactory Caf = new LowConsumeFactory();
                Client client = new Client(Caf);
                var photovoltaicEquipmentDTO = client.Operation();
                if (photovoltaicEquipmentDTO is null)
                {
                    return NotFound();
                }
                return Ok(photovoltaicEquipmentDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
