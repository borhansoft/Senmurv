using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pm.Bus.BusHelpers;
using pm.Data;
using pm.Data.Models;

namespace pm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public IRoleBus _roleBus;
        public RoleController(IRoleBus roleBus)
        {
            _roleBus = roleBus;
        }

        [HttpPost("create")]
        public ActionResult Create(Role role)
        {
            try
            {
                if (_roleBus.IsUniqueRole(role.Name))
                {
                    _roleBus.Create(role);
                    return Ok(StatusCode(201));
                }
                else
                {
                    return Ok(StatusCode(401));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost("getall")]
        public ActionResult GetAll()
        {

            var RoleList =_roleBus.GetAll();
            return Ok(RoleList);

        }


    }
}