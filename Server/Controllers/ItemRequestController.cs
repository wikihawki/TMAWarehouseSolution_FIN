﻿using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemRequestController(IGenericRepositoryInterface<ItemRequest> genericRepositoryInterface)
        : GenericController<ItemRequest>(genericRepositoryInterface)
    {
    }
}

