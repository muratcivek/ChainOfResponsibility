using ChainOfResponsibility.ChainOfResponsibilityPattern;
using ChainOfResponsibility.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChainOfResponsibility.Controllers
{
    public class DefaultController : Controller
    {
        private readonly Treasurer _treasurer;
        private readonly AssistantManager _assistantManager;
        private readonly Manager _manager;
        private readonly RegionalManager _regionalManager;

        public DefaultController(RegionalManager regionalManager, Manager manager, AssistantManager assistantManager, Treasurer treasurer)
        {
            _regionalManager = regionalManager;
            _manager = manager;
            _assistantManager = assistantManager;
            _treasurer = treasurer;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CustomerProcessViewModel model)
        {
            _treasurer.SetNextApprover(_assistantManager);
            _assistantManager.SetNextApprover(_manager);
            _manager.SetNextApprover(_regionalManager);

            _treasurer.ProcessRequest(model);    

            return NoContent();
        }
    }
}
