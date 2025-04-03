using Microsoft.AspNetCore.Mvc;
using TestBuilder.Models;

namespace TestBuilder.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpPost("AddRequest")]
    public IActionResult AddRequest(RequestDto model)
    {

        var pipline = new PiplineBuilder<RequestDto>()
                               .Add(new HasIdValidator())
                               .When(x => x.Id<1,
                                  () => new CheckIdNotZeroValidator(),
                                  () => new CheckIdNotZero2Validator()
                               )
                               .Add(new CheckTitleValidator())
                               .Build();


        var result = pipline.Validate(model);

        Console.WriteLine($"Validating: {model.Title}");
        Console.WriteLine($"Validation: isSuccess: {result.IsSuccess}");
        Console.WriteLine("--------------------------");

        return Ok(result);
    }
}
