using Host.Models.Coffees;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Dtos.Coffee;
using Services.Dtos.Files;
using System.Data;

namespace Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "MODERATOR")]
    public class CoffeeController : Controller
    {
        readonly ICoffeeService _coffeeService;
        readonly IFileService _fileService;
        public CoffeeController(ICoffeeService coffeeService, IFileService fileService) 
        {
            _coffeeService = coffeeService;
            _fileService = fileService;
        }

        [HttpGet("list")]
        [AllowAnonymous]        
        public IEnumerable<CoffeeDto> GetCoffees() => _coffeeService.GetCoffees();

        
        [AllowAnonymous]
        [HttpGet]
        public async  Task<IActionResult> GetCoffee(int id)
        {
            var coffee = await _coffeeService.GetCoffeeDetails(id);
            


            return Ok(coffee);
        }

        [HttpPut]
        public async Task<IActionResult> EditCoffee([FromForm]EditCoffeeModel coffeeModel)
        {
            await WriteFiles(coffeeModel.Photos);

            var coffeeDetails = await _coffeeService.EditCoffee(coffeeModel.Adapt<EditCoffeeDto>());
            
            return Ok(coffeeDetails);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoffee([FromForm]CreateCoffeeModel coffeeModel)
        {
            await WriteFiles(coffeeModel.Photos);

            var coffeeDetails = await _coffeeService.CreateCoffee(coffeeModel.Adapt<CreateCoffeeDto>());

            return Ok(coffeeDetails);
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteCoffee([FromQuery]int coffeeId)
        {
            await _coffeeService.DeleteCoffee(coffeeId);

            return Ok();
        }

        async Task WriteFiles(IEnumerable<IFormFile> files)
        {
            var filesData = files.Select(s => new WriteFileDto {FileData = s.OpenReadStream(), Name = s.FileName });
            var filesNames = files.Select(s => s.Name);

            await _fileService.WriteFiles(filesData);
        }
    }
}
