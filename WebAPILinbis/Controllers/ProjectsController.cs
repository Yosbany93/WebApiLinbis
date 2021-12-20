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
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        
        private readonly ProjectsService projectsService;
        private readonly IMapper mapper;

        public ProjectsController(ProjectsService projectsService, IMapper mapper)
        {
            this.projectsService = projectsService;
            this.mapper = mapper;
        }
        [HttpGet]
        public ActionResult<List<Project>> Get()
        {
            try
            {                
                return projectsService.ShowAll();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpGet("{id:int}")]
        [HttpGet("{id:int}/developers")]
        public ActionResult<Project> Get(int id)
        {
            try
            {                
                return projectsService.ShowProject(id);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        [HttpGet("{name}")]
        [HttpGet("{name}/developers")]
        public ActionResult<Project> Get(string name)
        {
            try
            {
                return projectsService.ShowProjectByName(name);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public ActionResult Post(ProjectCreationDTO projectCreationDTO)
        {
            try
            {
                var project = mapper.Map<Project>(projectCreationDTO);
                projectsService.CreateProject(project);
                return Ok(201);
            }
            catch (Exception)
            {
                throw;
            }     
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, ProjectCreationDTO projectCreationDTO)
        {
            try
            {
                var project = mapper.Map<Project>(projectCreationDTO);
                return projectsService.UpdateProject(id, project);                
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
                return projectsService.DeleteProject(id);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
