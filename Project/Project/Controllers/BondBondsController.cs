using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Configuration;
using Project.Repo;


namespace Project.Controllers
{
    [Route("sm/[controller]")]
    [ApiController]
    public class BondBondsController : ControllerBase
    {
        private readonly IBond _ibond;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BondBondsController> _logger;
        public BondBondsController(IBond ibond, IConfiguration configuration, ILogger<BondBondsController> logger)
        {
            _ibond = ibond;
            _configuration = configuration;
            _logger = logger;
        }

        // GET: sm/BondBonds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BondBond>>> GetBondBonds()
        {
            _logger.LogInformation("Fetching all bonds");

            var bonds = await _ibond.GetAllBonds();

            if (bonds == null || !bonds.Any())
            {
                _logger.LogWarning("No bond records found.");
                return NotFound("No bond records found.");
            }

            return Ok(bonds);
        }


        // GET: sm/BondBonds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BondBond>> GetBondBond(int id)
        {
            _logger.LogInformation("Fetching bond with ID: {Id}", id);

            var bondBond = await _ibond.GetBondById(id);
            if (bondBond == null)
            {
                _logger.LogWarning("Bond with ID {Id} not found.", id);
                return NotFound();
            }

            return Ok(bondBond);
        }


        // GET: sm/BondBonds/GetByName/{securityName}
        [HttpGet("GetByName/{securityName}")]
        public async Task<ActionResult<BondBond>> GetBondBondByName(string securityName)
        {
            _logger.LogInformation("Fetching bond with Security Name: {SecurityName}", securityName);

            var bondBond = await _ibond.GetBondByName(securityName);
            if (bondBond == null)
            {
                _logger.LogWarning("Bond with Security Name '{SecurityName}' not found.", securityName);
                return NotFound();
            }

            return Ok(bondBond);
        }


        // POST: sm/BondBonds
        [HttpPost]
        public async Task<IActionResult> PostBondBond([FromBody] BondBond bondBond)
        {
            var existingBond = await _ibond.GetBondByName(bondBond.SecurityName);

            if (existingBond != null)
            {
                _logger.LogWarning("Bond creation failed. A bond with the name '{SecurityName}' already exists.", bondBond.SecurityName);
                return BadRequest(new { message = "A bond with this name already exists. Please choose a different name." });
            }

            var createdBond = await _ibond.CreateBond(bondBond);

            if (createdBond == null)
            {
                _logger.LogError("Failed to create bond. An unexpected error occurred.");
                return StatusCode(500, new { message = "An error occurred while creating the bond." });
            }

            _logger.LogInformation("Bond created successfully with Security ID: {SecurityId}", createdBond.SecurityId);
            return CreatedAtAction(nameof(GetBondBond), new { id = createdBond.SecurityId }, createdBond);
        }


        // PUT: sm/BondBonds/PutByName/{securityName}
        [HttpPut("PutByName/{securityName}")]
        public async Task<IActionResult> PutBondByName(string securityName, [FromBody] BondBond bondBond)
        {
            _logger.LogInformation("Updating bond with Security Name: {SecurityName}", securityName);

            var result = await _ibond.UpdateBond(securityName, bondBond);

            if (!result)
            {
                _logger.LogWarning("Bond with Security Name {SecurityName} not found.", securityName);
                return NotFound();
            }

            _logger.LogInformation("Bond with Security Name {SecurityName} updated successfully.", securityName);
            return NoContent();
        }

        [HttpGet("exists/{securityName}")]
        public async Task<IActionResult> BondBondExistsByName(string securityName)
        {
            _logger.LogInformation("Checking existence of bond with Security Name: {SecurityName}", securityName);

            var exists = await _ibond.BondBondExistsByName(securityName);

            if (!exists)
            {
                _logger.LogWarning("Bond with Security Name {SecurityName} does not exist.", securityName);
            }

            return Ok(exists);
        }


        // DELETE: api/BondBonds/DeleteByName/{securityName}
        [HttpDelete("DeleteByName/{securityName}")]
        public async Task<IActionResult> DeleteBondBondByName([FromRoute] string securityName)
        {
            _logger.LogInformation("Attempting to delete bond with Security Name: {SecurityName}", securityName);

            var result = await _ibond.DeleteBondByName(securityName);

            if (!result)
            {
                _logger.LogWarning("Bond with Security Name {SecurityName} not found for deletion.", securityName);
                return NotFound();
            }

            _logger.LogInformation("Successfully deleted bond with Security Name: {SecurityName}", securityName);
            return NoContent();
        }

        [HttpGet("GetTabWiseBond/{tabName}")]
        public IActionResult GetTabWise(string tabName)
        {
            _logger.LogInformation("Fetching tab-wise data for TabName: {TabName}", tabName);

            try
            {
                string connectionString = _configuration.GetConnectionString("MyDBConn");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetTabWiseBond", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter sqlOutput = new SqlParameter("@sql", SqlDbType.NVarChar, -1)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(sqlOutput);
                        cmd.Parameters.AddWithValue("@TabName", tabName);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        string generatedSql = sqlOutput.Value.ToString();

                        using (SqlCommand dynamicCmd = new SqlCommand(generatedSql, conn))
                        {
                            using (SqlDataReader reader = dynamicCmd.ExecuteReader())
                            {
                                List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

                                while (reader.Read())
                                {
                                    Dictionary<string, object> row = new Dictionary<string, object>();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        row[reader.GetName(i)] = reader[i] is DBNull ? null : reader[i];
                                    }
                                    results.Add(row);
                                }

                                if (results.Count == 0)
                                {
                                    _logger.LogWarning("No records found for TabName: {TabName}", tabName);
                                    return NotFound("No records found.");
                                }

                                return Ok(results);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching tab-wise data for TabName: {TabName}", tabName);
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
