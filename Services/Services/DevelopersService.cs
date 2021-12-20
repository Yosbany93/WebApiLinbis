using Microsoft.AspNetCore.Mvc;
using Services.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class DevelopersService : ControllerBase
    {
        JsonContext<Project> proj = new JsonContext<Project>("proj.txt");
        JsonContext<Developer> dev = new JsonContext<Developer>("dev.txt");

        public ActionResult<List<Developer>> ShowAll()
        {
            dev.Load();
            return dev.values;
        }
        public ActionResult<List<Developer>> ShowDevelopersByProject(int projectId)
        {
            dev.Load();
            var existProject = ExistProject(projectId);
            if (!existProject)
            {
                return NotFound(404);
            }
            var developers = dev.values.Where(x => x.ProjectId == projectId).ToList();
            return developers;
        }
        public ActionResult<List<Developer>> ShowDevelopersByName(string name)
        {
            dev.Load();
            var existDeveloperByName = ExistDeveloperByName(name);
            if (!existDeveloperByName)
            {
                return NotFound(404);
            }
            var developers = dev.values.Where(x => x.Name.Contains(name)).ToList();
            return developers;
        }
        public ActionResult CreateDeveloper(int projectId, Developer developer)
        {            
            dev.Load();
            if (projectId != developer.ProjectId)
            {
                return BadRequest("El projectId enviado desde el formulario no coincide con el enviado en la url");
            }
            var existProject = ExistProject(projectId);
            if (!existProject)
            {
                return NotFound(404);
            }
            if (dev.values == null)
            {
                dev.values = new List<Developer>();
                developer.Id = 1;
            }
            else
            {
                developer.Id = dev.values.LastOrDefault().Id + 1;
            }
            developer.AddedDate = DateTimeOffset.UtcNow;
            dev.Insert(developer);
            return Ok(201);
        }
        public ActionResult UpdateDeveloper(int id, Developer developer)
        {
            dev.Load();
            var existDeveloper = ExistDeveloper(id);
            var existProject = ExistProject(developer.ProjectId);
            if (!existProject || !existDeveloper)
            {
                return NotFound(404);
            }
            developer.Id = id;            
            dev.Update(x => x.Id == id, developer);
            dev.Save();
            return Ok(200);
        }
        public ActionResult DeleteDeveloper(int id)
        {
            dev.Load();
            var existDeveloper = ExistDeveloper(id);
            if (!existDeveloper)
            {
                return NotFound(404);
            }
            dev.Delete(x => x.Id == id);
            dev.Save();
            return Ok(204);
        }

        public bool ExistProject(int id)
        {
            proj.Load();
            return proj.values.Any(x => x.Id == id);
        }
        public bool ExistDeveloper(int id)
        {
            dev.Load();
            return dev.values.Any(x => x.Id == id);
        }
        public bool ExistDeveloperByName(string name)
        {
            dev.Load();
            return dev.values.Any(x => x.Name.Contains(name));
        }
    }
}
