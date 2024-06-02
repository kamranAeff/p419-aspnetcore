using Microsoft.AspNetCore.Mvc;
using WebIntroEmpty2.AppCode.LoggingConcept;

namespace WebIntroEmpty2.AppCode.ViewComponents
{
    public class ExchangeStatisticViewComponent : ViewComponent
    {
        private readonly IMyLogger logger;

        public ExchangeStatisticViewComponent(IMyLogger logger)
        {
            this.logger = logger;
        }
        public IViewComponentResult Invoke()
        {
            return Content(logger.InstanceId.ToString());
        }
    }
}
