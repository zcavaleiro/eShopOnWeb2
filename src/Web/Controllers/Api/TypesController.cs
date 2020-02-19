using Microsoft.eShopWeb.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{
    public class TypesController : BaseApiController
    {
        private readonly IAsyncRepository<CatalogType> _typeRepository;

        private readonly ILogger<TypesController> _logger;

        public TypesController(IAsyncRepository<CatalogType> typeRepository, ILogger<TypesController> logger)
        {
            _typeRepository = typeRepository;
            _logger = logger;
        }

        /// <summary>
        /// return catalog types list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CatalogType>>> List()
        {
            var types = await _typeRepository.ListAllAsync();
            _logger.LogInformation("################################");
            _logger.LogInformation("Get all Catalog types");
            _logger.LogInformation("################################");
            return Ok(types);

        }

        /// <summary>
        /// Get catalog type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<CatalogType>> GetById(int id)
        {
            _logger.LogInformation("################################");
            _logger.LogInformation("Get Catalog types by id");
            _logger.LogInformation("################################");
            try
            {
                var type = await _typeRepository.GetByIdAsync(id);
                return Ok(type);
            }
            catch (ModelNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get Catalog Type by type name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<CatalogType>> GetByType(string name)
        {
            _logger.LogInformation("################################");
            _logger.LogInformation("GET Catalog types by name");
            _logger.LogInformation("################################");
            try
            {
                var types = await _typeRepository.ListAllAsync();
                var typeFindeds = types.Where(x => string.Compare(x.Type, name, StringComparison.InvariantCultureIgnoreCase) == 0);
                return Ok(typeFindeds);
            }
            catch (ModelNotFoundException)
            {
                return NotFound();
            }
        }

        // Post: api/Types/
        /// <summary>
        /// Insert new catalog type
        /// </summary>
        /// <param name="catalogType"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IReadOnlyList<CatalogType>>> Post(CatalogType catalogType)
        {
            _logger.LogInformation("################################");
            _logger.LogInformation("POST Catalog types");
            _logger.LogInformation("################################");
            try
            {
                var types = await _typeRepository.ListAllAsync();
                var typeFindeds = types
                        .Where(x => string.Compare(x.Type, catalogType.Type, StringComparison.InvariantCultureIgnoreCase) == 0);
                if (typeFindeds != null) { return Conflict(); }

                await _typeRepository.AddAsync(catalogType);
                return Ok();
            }
            catch (ModelNotFoundException error)
            {
                _logger.LogError("O erro é: ", error);
                return NotFound();
            }
        }

        /// <summary>
        /// Update Catalog type
        /// api/Types/
        /// </summary>
        /// <param name="catalogType"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<IReadOnlyList<CatalogType>>> Put(CatalogType catalogType)
        {
            _logger.LogInformation("################################");
            _logger.LogInformation("Update Catalog types");
            _logger.LogInformation("################################");
            try
            {
                var types = await _typeRepository.ListAllAsync();
                // return catalog types list where de type name is the same and id not iqual
                var typeFindeds = types
                        .Where(x => string.Compare(x.Type, catalogType.Type, StringComparison.InvariantCultureIgnoreCase) == 0)
                        .Where(x => x.Id != catalogType.Id);
                if (typeFindeds != null) { return Conflict(); }

                await _typeRepository.UpdateAsync(catalogType);
                return Ok();
            }
            catch (ModelNotFoundException error)
            {
                _logger.LogError("O erro é: ", error);
                return NotFound();
            }
        }

        /// <summary>
        /// Delete Catalog Type By id
        /// api/Types/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<IReadOnlyList<CatalogType>>> Delete(int id)
        {
            _logger.LogInformation("################################");
            _logger.LogInformation("DELETE Catalog types");
            _logger.LogInformation("################################");
            try
            {
                var type = await _typeRepository.GetByIdAsync(id);
                if (type == null) { return NotFound(); }

                await _typeRepository.DeleteAsync(type);
                return Ok(type);
            }
            catch (ModelNotFoundException error)
            {
                _logger.LogError("O erro é: ", error);
                return NotFound();
            }
        }
    }
}