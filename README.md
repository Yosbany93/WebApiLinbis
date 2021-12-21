# WebApiLinbis
This project contains two controllers (Projetcs and Developers).
Next you will be given a description of the route and functionality of the endpoints of these controllers

ProjectsController:

* [Route("api/projects")](returns a list of existing projects)

  [HttpGet]  public ActionResult<List<Project>> Get()
  
* [Route("api/projects/{id:int}")]
  o
  [Route("api/projects/{id:int}/developers")](returns the details of the project that presents the id given in the url (including the developers linked to it))
  
  [HttpGet] public ActionResult<Project> Get(int id)
  
* [Route("api/projects/{name}")]
  o
  [Route("api/projects/{name}/developers")](returns the details of the project that presents the name given in the url (including the developers linked to it))
  
  [HttpGet] public ActionResult<Project> Get(string name)
  
* [Route("api/projects")](used to create a new project and add it to the database)
  
  [HttpPost] public ActionResult Post(ProjectCreationDTO projectCreationDTO)
  
* [Route("api/projects/{id:int}")](used to update an existing project)
  
  [HttpPut("{id}")]  public ActionResult Put(int id, ProjectCreationDTO projectCreationDTO)
  
* [Route("api/projects/{id:int}")](used to delete an existing project)
  
  [HttpDelete("{id}")] public ActionResult Delete(int id)

DevelopersController
  
* [Route("api/developers")](returns a list of existing developers)
  
  [HttpGet] public ActionResult<List<Developer>> Get()
  
* [Route("api/developers/projects/{projectId:int}")] (returns a list of developers associated with the project that presents the id passed as a parameter)
  
  [HttpGet("projects/{projectId:int}")] public ActionResult<List<Developer>> Get(int projectId)
  
* [Route("api/developers/{name}")(returns a list of developers that presents the name given in the url)
  
  [HttpGet("{name}")] public ActionResult<List<Developer>> Get(string name)
  
* [Route("api/developers/projects/{projectId:int}") o [Route("api/projects/{projectId:int}/developers")(used to create a new developer and add it to the database)
  
  public ActionResult Post(int projectId, DeveloperCreationDTO developerCreationDTO)
  
* [Route("api/developers/{id}")(used to update an existing developer)
  
  [HttpPut("{id}")] public ActionResult Put(int id, DeveloperCreationDTO developerCreationDTO)
  
* [Route("api/developers/{id}")(used to delete an existing developer)
  
  [HttpDelete("{id}")] public ActionResult Delete(int id)
