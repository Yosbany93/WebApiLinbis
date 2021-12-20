using Microsoft.AspNetCore.Mvc;
using Services.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Services.Services
{
    public class ProjectsService : ControllerBase
    {
        JsonContext<Project> proj = new JsonContext<Project>("proj.txt");
        JsonContext<Developer> dev = new JsonContext<Developer>("dev.txt");

        public ActionResult<List<Project>> ShowAll()
        {
            proj.Load();
            return proj.values;
        }
        public ActionResult<Project> ShowProject(int id)
        {
            proj.Load();
            dev.Load();
            var existProject = ExistProject(id);
            if (!existProject)
            {
                return NotFound(404);
            }
            var project = proj.values.FirstOrDefault(x => x.Id == id);
            var develop = dev.values.Where(x => x.ProjectId == id).ToList();
            foreach (var item in develop)
            {
                var devCost = item.CostByDay * project.EffortRequireInDays;
                project.DevelopmentCost = project.DevelopmentCost + devCost;
            }
            project.developers = develop;
            return project;
        }
        public ActionResult<Project> ShowProjectByName(string name)
        {
            proj.Load();
            dev.Load();
            var existProjectByName = ExistProjectByName(name);
            if (!existProjectByName)
            {
                return NotFound(404);
            }
            var project = proj.values.FirstOrDefault(x => x.Name.Contains(name));
            int id = project.Id;
            var develop = dev.values.Where(x => x.ProjectId == id).ToList();
            foreach (var item in develop)
            {
                var devCost = item.CostByDay * project.EffortRequireInDays;
                project.DevelopmentCost = project.DevelopmentCost + devCost;
            }
            project.developers = develop;
            return project;
        }
        public void CreateProject(Project project)
        {
            proj.Load();
            if (proj.values == null)
            {
                proj.values = new List<Project>();
                project.Id = 1;
            }
            else
            {
                project.Id = proj.values.LastOrDefault().Id + 1;
            }
            project.AddedDate = DateTimeOffset.UtcNow;
            project.developers = new List<Developer>();
            proj.Insert(project);
        }
        public ActionResult UpdateProject(int id, Project project)
        {
            proj.Load();
            var existProject = ExistProject(id);
            if (!existProject)
            {
                return NotFound(404);
            }
            project.Id = id;            
            proj.Update(x => x.Id == id, project);
            proj.Save();
            return Ok(200);
        }
        public ActionResult DeleteProject(int id)
        {
            proj.Load();
            var existProject = ExistProject(id);
            if (!existProject)
            {
                return NotFound(404);
            }
            proj.Delete(x => x.Id == id);
            proj.Save();
            return Ok(204);
        }

        public bool ExistProject(int id)
        {
            proj.Load();
            return proj.values.Any(x => x.Id == id);
        }
        public bool ExistProjectByName(string name)
        {
            proj.Load();
            return proj.values.Any(x => x.Name.Contains(name));
        }
        public Project GetProject(int id)
        {
            proj.Load();
            return proj.values.FirstOrDefault(x => x.Id == id);
        }
    }
}
