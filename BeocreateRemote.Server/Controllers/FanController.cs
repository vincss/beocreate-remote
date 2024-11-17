using BeocreateRemote.Core;
using BeocreateRemote.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeocreateRemote.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FanController(FanInformation fanInformation) :  ControllerBase
{
    [HttpGet]
    public FanInformation Get()
    {
        return fanInformation;
    }
}


