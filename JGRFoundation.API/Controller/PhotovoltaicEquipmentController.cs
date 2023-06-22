using JGRFoundation.API.Data;
using JGRFoundation.API.Helpers.AbstractFactory;
using JGRFoundation.API.Helpers.AbstractFactory.factory;
using JGRFoundation.API.Helpers.AbstractFactory.factory.HighConsume;
using JGRFoundation.API.Helpers.AbstractFactory.factory.LowConsume;
using JGRFoundation.API.Helpers.Strategy;
using JGRFoundation.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JGRFoundation.API.Controller
{
    [ApiController]
    [Route("/api/PhotovoltaicEquipment")]
    public class PhotovoltaicEquipmentController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly CalculatorInstallationContext _calculatorInstallationContext;

        public PhotovoltaicEquipmentController(DataContext context)
        {
            _context = context;
            _calculatorInstallationContext = new CalculatorInstallationContext();
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


        [HttpPost("GetCalculateInstallationPanel")]
        public async Task<ActionResult> GetCalculateInstallationPanel([FromBody]InstallationPanelDTO installationPanelDTO)
        {
            try
            {
                IPanelInstallationStrategy strategy;
                if (installationPanelDTO.installationStrategy == "Paralle")
                {
                    strategy = new ParallelPanelStrategy();
                }
                else if (installationPanelDTO.installationStrategy == "Series")
                {
                    strategy = new SeriesPanelStrategy();
                }
                else
                {
                    return BadRequest("Estrategia de envío no válida.");
                }
                _calculatorInstallationContext.SetPanelInstallationStrategy(strategy);
                int QPanel = _calculatorInstallationContext.executeStrategy(installationPanelDTO);

                return Ok(QPanel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
