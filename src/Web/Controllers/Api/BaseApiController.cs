using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.eShopWeb.ApplicationCore.Constants;

namespace Microsoft.eShopWeb.Web.Controllers.Api
    {
        [Route("api/[controller]/[action]")]
        [ApiController]
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
        public class BaseApiController : ControllerBase
        { }
    }