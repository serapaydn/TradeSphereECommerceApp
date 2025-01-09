using System.Web.Mvc;

namespace TradeSphereECommerceApp.Areas.SellerPanel
{
    public class SellerPanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SellerPanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SellerPanel_default",
                "SellerPanel/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }, 
                namespaces: new[] { "TradeSphereECommerceApp.Areas.SellerPanel.Controllers" }
            );
        }
    }
}