using System.Globalization;
using API.DTOs.FixQty;
using API.Services.FixQtyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace API.Controllers;

// Indicates that this class is an API controller
[ApiController]
// Sets the base route for the controller
[Route("[controller]")]
public class FixQtyController: ControllerBase
{
    private readonly IFixQtyService _fixQtyService;

    public FixQtyController(IFixQtyService fixQtyService)
    {
        _fixQtyService = fixQtyService;
    }

    // GET: /FixQty
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GetFixQtyDTO>>>> Get()
    {
        // Return a response with the list of FixQty objects
        return Ok(await _fixQtyService.Get());
    }
    [HttpGet("getBetween/{startDateStr}/{endDateStr}")]
    public async Task<ActionResult<ServiceResponse<List<GetTempFixQtyDTO>>>> GetBetween(string startDateStr, string endDateStr)
    {
        string format = "yyyy-MM-dd";
        CultureInfo provider = CultureInfo.InvariantCulture;
        DateTime startDate;
        DateTime endDate;
        DateTime.TryParseExact(startDateStr, format, provider, DateTimeStyles.None, out startDate);
        DateTime.TryParseExact(endDateStr, format, provider, DateTimeStyles.None, out endDate);

        // Return a response with the list of FixQty objects
        return Ok(await _fixQtyService.GetBetween(startDate, endDate));
    }

    // GET: /FixQty/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetFixQtyDTO>>> GetById(int id)
    {
        // Return a response with the FixQty object for the given id
        return Ok(await _fixQtyService.GetById(id));
    }

    // POST: /FixQty
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetFixQtyDTO>>>> AddFixQty(AddFixQtyDTO fixQty)
    {
        // Return a response after adding the FixQty object
        return Ok(await _fixQtyService.AddFixQty());
    }

    // PUT: /FixQty
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<List<GetFixQtyDTO>>>> UpdateFixQty(UpdateFixQtyDTO fixQty)
    {
        // Update the FixQty object and return a response
        var response = await _fixQtyService.UpdateFixQty(fixQty);
        if(response.Data is null) return NotFound(response);
        return Ok(response);
    }

    // DELETE: /FixQty/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<GetFixQtyDTO>>> Delete(int id)
    {
        // Delete the FixQty object and return a response
        var response = await _fixQtyService.DeleteFixQty(id);
        if(response.Data is null) return NotFound(response);
        return Ok(response);
    }
}
