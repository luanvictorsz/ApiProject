using System.Collections.Generic;
using ApiProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ToDoController : ControllerBase
    {
        [HttpGet]
        public List<Todo> Get()
        {
            return new List<Todo>();
        }
    }
}
