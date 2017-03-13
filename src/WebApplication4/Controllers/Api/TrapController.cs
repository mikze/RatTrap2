using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Controllers.Api
{
    [Route("api/traps")]
    public class TrapController : Controller
    {
        private ILogger<TrapController> _logger;
        private ITrapRepo _repo;

        public TrapController(ITrapRepo repo,ILogger<TrapController> logger)
        {
            _logger = logger;
            _repo = repo;
        }
        [HttpGet("")]
        public JsonResult Get()
        {
            var results = _repo.GetAllTraps();

            return Json(results);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TrapViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //dodawanie do bazy
                    var newTrap = Mapper.Map<Trap>(vm);
                    _logger.LogInformation("Trying to save new trap");
                    _repo.AddTrap(newTrap);

                    if (_repo.SaveAll())
                    { 
                    Response.StatusCode = (int)HttpStatusCode.Created;
                    return Json(Mapper.Map<TrapViewModel>(newTrap));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to sve new trap",ex);

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message, ModelState = ModelState });
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Fail xD", ModelState = ModelState });
        }
    }
}
