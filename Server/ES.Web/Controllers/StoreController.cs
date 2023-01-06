using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ES.Web.Contracts.V1;

namespace ES.Web.Controllers
{
    [Route(ApiRoutes.Root)]
    [ApiController]
    public class StoreController : ControllerBase
    {
        [HttpGet(ApiRoutes.Store.GetStore)]
        public async Task<IActionResult> GetMaterialStore()
        {
            throw new NotImplementedException();
        }

        [HttpGet(ApiRoutes.Store.GetStoreList)]
        public async Task<IActionResult> GetMaterialListStore()
        {
            throw new NotImplementedException();
        }
    }
}
