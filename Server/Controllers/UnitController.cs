using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController(IGenericRepositoryInterface<Unit> genericRepositoryInterface)
        : GenericController<Unit>(genericRepositoryInterface)
    {
    }
}
