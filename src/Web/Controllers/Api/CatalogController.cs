using Microsoft.eShopWeb.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.eShopWeb.Web.ViewModels;
using System;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{

    public class CatalogController : BaseApiController
    {
        private readonly ICatalogViewModelService _catalogViewModelService;

        public CatalogController(ICatalogViewModelService catalogViewModelService) => _catalogViewModelService = catalogViewModelService;

        [HttpGet]
        public async Task<ActionResult<CatalogIndexViewModel>> List(
            int? brandFilterApplied, int? typesFilterApplied, int? page,
            string searchText = null)
        {
            var itemsPage = 10;           
            var catalogModel = await _catalogViewModelService.GetCatalogItems(
                page ?? 0, itemsPage, searchText, 
                brandFilterApplied, typesFilterApplied, true, HttpContext.RequestAborted);
            return Ok(catalogModel);
        }

        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<CatalogItemViewModel>> GetById(int id) {
            try  {
                var catalogItem = await _catalogViewModelService.GetItemById(id);
                return Ok(catalogItem);
            } catch (ModelNotFoundException) {
                return NotFound();
            }
        }

   

    }
}
