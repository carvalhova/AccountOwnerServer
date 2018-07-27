using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace AccountOwnerServer.Infra.Filters
{
    public class AddAuthorizeFiltersControllerConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            controller.Filters.Add(GetAuthorizeFilter(controller));
        }

        private static AuthorizeFilter GetAuthorizeFilter(ControllerModel controller)
        {
            //var authorizeFilter = controller.ControllerName.Contains("Api")
            //    ? new AuthorizeFilter("api-policy")
            //    : new AuthorizeFilter("default-policy");

            var authorizeFilter = new AuthorizeFilter("api-policy");

            return authorizeFilter;
        }
    }
}