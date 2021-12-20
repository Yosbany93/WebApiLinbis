using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTOs;
using Services.Entidades;
using Services.Services;

namespace WebAPILinbis.Controllers
{

    [Route("api/developers")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly DevelopersService developersService;
        private readonly IMapper mapper;

        public DevelopersController(DevelopersService developersService, IMapper mapper)
        {
            this.developersService = developersService;
            this.mapper = mapper;
        }
        [HttpGet]
        public ActionResult<List<Developer>> Get()
        {
            try
            {                
                return developersService.ShowAll();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("projects/{projectId:int}")]        
        public ActionResult<List<Developer>> Get(int projectId)
        {

            try
            {              
                return developersService.ShowDevelopersByProject(projectId);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        [HttpGet("{name}")]
        public ActionResult<List<Developer>> Get(string name)
        {

            try
            {
                return developersService.ShowDevelopersByName(name);
            }
            catch (Exception)
            {
                throw;
            }
        }
        

        [HttpPost("projects/{projectId:int}")]
        [HttpPost("/api/projects/{projectId:int}/developers")]
        public ActionResult Post(int projectId, DeveloperCreationDTO developerCreationDTO)
        {
            try
            {
                var developer = mapper.Map<Developer>(developerCreationDTO);
                return developersService.CreateDeveloper(projectId, developer);

            }
            catch (Exception)
            {
                throw;
            }  
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, DeveloperCreationDTO developerCreationDTO)
        {
            try
            {
                var developer = mapper.Map<Developer>(developerCreationDTO);
                return developersService.UpdateDeveloper(id, developer);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            try
            {    
                return developersService.DeleteDeveloper(id);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
