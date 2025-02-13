using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using Project.Repo;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Project.Controllers
{
    [Route("sm/[controller]")]
    [ApiController]
    public class EquityEquitiesController : ControllerBase
    {
        
        
        private IEquity _iequity;
        private readonly ILogger<EquityEquitiesController> _logger;
        public EquityEquitiesController( IEquity iequity, ILogger<EquityEquitiesController> logger)
        {
            
            _iequity= iequity;
            _logger= logger;
        }

        // GET: sm/EquityEquities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquityEquity>>> GetEquityEquities()
        {
            _logger.LogInformation("Fetching all equities.");

            var equities = await _iequity.GetEquities();

            if (equities == null || !equities.Any())
            {
                _logger.LogWarning("No equities found.");
            }

            return Ok(equities);
        }

        // GET :sm/EquityEquities/GetByName/xyz
        [HttpGet]
        [Route("GetByName/{SecurityName}")]
        public async Task<IActionResult> GetEquityEquityByName([FromRoute] string SecurityName)
        {
            if (string.IsNullOrWhiteSpace(SecurityName))
            {
                _logger.LogWarning("SecurityName is empty in GetByName request.");
                return BadRequest("SecurityName cannot be empty.");
            }

            _logger.LogInformation("Fetching equity details for SecurityName: {SecurityName}", SecurityName);

            var equity = await _iequity.GetEquityByName(SecurityName);

            if (equity == null)
            {
                _logger.LogWarning("No equity found for SecurityName: {SecurityName}", SecurityName);
                return NotFound($"No equity found with Security Name: {SecurityName}");
            }

            return Ok(equity);
        }



        //PUT: sm/EquityEquities/PutByName/xyz
        [HttpPut("PutByName/{securityName}")]
        public async Task<IActionResult> PutEquityEquityByName([FromRoute] string securityName, [FromBody] EquityEquity equityEquity)
        {
            if (string.IsNullOrWhiteSpace(securityName))
            {
                _logger.LogWarning("PutByName request received with an empty securityName.");
                return BadRequest(new { message = "Security name cannot be empty." });
            }

            _logger.LogInformation("Updating equity with SecurityName: {SecurityName}", securityName);

            var updated = await _iequity.UpdateEquityByName(securityName, equityEquity);

            if (!updated)
            {
                _logger.LogWarning("Equity not found for update - SecurityName: {SecurityName}", securityName);
                return NotFound(new { message = "Equity not found" });
            }

            _logger.LogInformation("Successfully updated equity - SecurityName: {SecurityName}", securityName);
            return NoContent(); // HTTP 204: Successfully updated but no content returned
        }


        //POST: sm/EquityEquities
        [HttpPost]
        public async Task<IActionResult> EquityEquities([FromBody] EquityEquity equityEquity)
        {
            var existingEquity = await _iequity.GetEquityByName(equityEquity.SecurityName);

            if (existingEquity != null)
            {
                _logger.LogWarning("Equity creation failed. An equity with the name '{SecurityName}' already exists.", equityEquity.SecurityName);
                return BadRequest(new { message = "An equity with this name already exists. Please choose a different name." });
            }

            var createdEquity = await _iequity.CreateEquity(equityEquity);

            if (createdEquity == null)
            {
                _logger.LogError("Failed to create equity. An unexpected error occurred.");
                return StatusCode(500, new { message = "An error occurred while saving the equity" });
            }

            _logger.LogInformation("Equity created successfully with ID: {SecurityId}", createdEquity.SecurityId);
            return CreatedAtAction(nameof(GetEquityEquityById), new { id = createdEquity.SecurityId }, createdEquity);
        }


        //GET: sm/EquityEquities/GetById/123
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetEquityEquityById(int id)
        {
            _logger.LogInformation("Fetching equity details for ID: {EquityId}", id);

            var equity = await _iequity.GetEquityById(id);

            if (equity == null)
            {
                _logger.LogWarning("Equity with ID {EquityId} not found.", id);
                return NotFound(new { message = $"Equity with ID {id} not found." });
            }

            _logger.LogInformation("Equity details retrieved successfully for ID: {EquityId}", id);
            return Ok(equity);
        }


        // DELETE: sm/EquityEquities/ByName
        [HttpDelete("ByName/{securityName}")]
        public async Task<IActionResult> DeleteEquityEquityByName(string securityName)
        {
            _logger.LogInformation("Attempting to delete equity with name: {SecurityName}", securityName);

            bool isDeleted = await _iequity.DeleteEquityByName(securityName);

            if (!isDeleted)
            {
                _logger.LogWarning("Equity with name '{SecurityName}' not found.", securityName);
                return NotFound(new { message = $"Equity with name '{securityName}' not found." });
            }

            _logger.LogInformation("Successfully deleted equity with name: {SecurityName}", securityName);
            return Ok(new { message = $"Successfully deleted the equity with name '{securityName}'." });
        }

        //GET: sm/EquityEquities/GetTabWiseEquity/abc
        [HttpGet("GetTabWiseEquity/{tabName}")]
        public async Task<IActionResult> GetTabWise(string tabName)
        {
            _logger.LogInformation("Fetching tab-wise equity data for tab: {TabName}", tabName);

            try
            {
                var result = await _iequity.GetTabWise(tabName);

                if (result == null || !result.Any())
                {
                    _logger.LogWarning("No data found for tab: {TabName}", tabName);
                    return NotFound(new { message = $"No data found for tab: {tabName}" });
                }

                _logger.LogInformation("Successfully fetched tab-wise data for tab: {TabName}", tabName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching tab-wise equity data for tab: {TabName}", tabName);
                return StatusCode(500, new { message = "An error occurred while fetching data.", error = ex.Message });
            }
        }

        [HttpGet("exists/{id}")]
        public async Task<IActionResult> CheckEquityExists(int id)
        {
            _logger.LogInformation("Checking if equity with ID {EquityId} exists.", id);

            bool exists = await _iequity.EquityEquityExists(id);

            if (!exists)
            {
                _logger.LogWarning("Equity with ID {EquityId} does not exist.", id);
            }
            else
            {
                _logger.LogInformation("Equity with ID {EquityId} exists.", id);
            }

            return Ok(new { exists });
        }

    }
}
