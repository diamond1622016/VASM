using System.Web.Mvc;

namespace ScientistVA.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default_router",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", Controller = "Scientist", id = UrlParameter.Optional },
                new[] { "ScientistVA.Areas.Admin.Controllers" }
            );
        }
    }
}