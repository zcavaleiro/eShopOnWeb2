using Microsoft.eShopWeb.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Linq;
using System;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{
    public class TypesController : BaseApiController
    {
        private readonly IAsyncRepository<CatalogType> _typeRepository;

        public TypesController(IAsyncRepository<CatalogType> typeRepository) => _typeRepository = typeRepository;

        /// <summary>
        /// return catalog types list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CatalogType>>> List()
        {
            var types = await _typeRepository.ListAllAsync();
            return Ok(types);
        }

        /// <summary>
        /// Get catalog type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<CatalogType>> GetById(int id) {
            try  {
                var type = await _typeRepository.GetByIdAsync(id);
                return Ok(type);
            } catch (ModelNotFoundException) {
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
        public async Task<ActionResult<CatalogType>> GetByType(string name) {
            try  {
                var types = await _typeRepository.ListAllAsync();
                var typeFindeds = types.Where(x => string.Compare(x.Type, name, StringComparison.InvariantCultureIgnoreCase) == 0);
                return Ok(typeFindeds);
            } catch (ModelNotFoundException) {
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

            try  {
                var types = await _typeRepository.ListAllAsync();
                var typeFindeds = types
                        .Where(x => string.Compare(x.Type, catalogType.Type, StringComparison.InvariantCultureIgnoreCase) == 0);
                if(typeFindeds != null){ return Conflict();}

                await _typeRepository.AddAsync(catalogType);
                return Ok();
            } catch (ModelNotFoundException) {
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
            try  {
                var types = await _typeRepository.ListAllAsync();
                // return catalog types list where de type name is the same and id not iqual
                var typeFindeds = types
                        .Where(x => string.Compare(x.Type, catalogType.Type, StringComparison.InvariantCultureIgnoreCase) == 0)
                        .Where(x => x.Id != catalogType.Id);
                if(typeFindeds != null){ return Conflict();}

                await _typeRepository.UpdateAsync(catalogType);
                return Ok();
            } catch (ModelNotFoundException) {
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
            try  {
                var type = await _typeRepository.GetByIdAsync(id);
                if(type == null){ return NotFound();}

                await _typeRepository.DeleteAsync(type);
                return Ok(type);
            } catch (ModelNotFoundException) {
                return NotFound();
            }
        }
    }
}