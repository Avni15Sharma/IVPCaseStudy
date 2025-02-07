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

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquityEquitiesController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly IConfiguration _configuration;

        public EquityEquitiesController(ProjectContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        

        
        

        // GET: api/EquityEquities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquityEquity>>> GetEquityEquities()
        {
            return await _context.EquityEquities.ToListAsync();
        }

        // GET: api/EquityEquities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EquityEquity>> GetEquityEquity([FromRoute] int id)
        {
            var equityEquity = await _context.EquityEquities.FindAsync(id);

            if (equityEquity == null)
            {
                return NotFound();
            }

            return equityEquity;
        }

        [HttpGet]
        [Route("GetByName/{SecurityName}")]
        public async Task<IActionResult> GetEquityEquityByName([FromRoute] string SecurityName)
        {
            if (string.IsNullOrWhiteSpace(SecurityName))
            {
                return BadRequest("SecurityName cannot be empty.");
            }

            var equity = await _context.EquityEquities
                                       .Where(e => e.SecurityName == SecurityName)
                                       .FirstOrDefaultAsync();

            if (equity == null)
            {
                return NotFound($"No equity found with Security Name: {SecurityName}");
            }

            return Ok(equity);
        }


        // PUT: api/EquityEquities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquityEquity([FromRoute] int id,[FromBody] EquityEquity equityEquity)
        {
            if (id != equityEquity.SecurityId)
            {
                return BadRequest();
            }

            _context.Entry(equityEquity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquityEquityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPut("PutByName/{securityName}")]
        public async Task<IActionResult> PutEquityEquityByName([FromRoute] string securityName, [FromBody] EquityEquity equityEquity)
        {
            // Find the entity by SecurityName
            var existingEquity = await _context.EquityEquities
                                               .FirstOrDefaultAsync(e => e.SecurityName == securityName);

            if (existingEquity == null)
            {
                return NotFound(); // If not found, return 404
            }
            int id=existingEquity.SecurityId;
            // Update the properties of the existing entity with the values from the request body
            existingEquity.SecurityDescription = equityEquity.SecurityDescription;
            existingEquity.HasPosition = equityEquity.HasPosition;
            existingEquity.IsActive = equityEquity.IsActive;
            existingEquity.RoundLotSize = equityEquity.RoundLotSize;
            existingEquity.BloombergUniqueName = equityEquity.BloombergUniqueName;
            existingEquity.Cusip = equityEquity.Cusip;
            existingEquity.Isin = equityEquity.Isin;
            existingEquity.Sedol = equityEquity.Sedol;
            existingEquity.BloombergTicker = equityEquity.BloombergTicker;
            existingEquity.BloombergUniqueId = equityEquity.BloombergUniqueId;
            existingEquity.BloombergGlobalId = equityEquity.BloombergGlobalId;
            existingEquity.BloombergTickerAndExchange = equityEquity.BloombergTickerAndExchange;
            existingEquity.IsAdr = equityEquity.IsAdr;
            existingEquity.AdrUnderlyingTicker = equityEquity.AdrUnderlyingTicker;
            existingEquity.AdrUnderlyingCurrency = equityEquity.AdrUnderlyingCurrency;
            existingEquity.SharesPerAdr = equityEquity.SharesPerAdr;
            existingEquity.IpoDate = equityEquity.IpoDate;
            existingEquity.PriceCurrency = equityEquity.PriceCurrency;
            existingEquity.SettleDays = equityEquity.SettleDays;
            existingEquity.SharesOutstanding = equityEquity.SharesOutstanding;
            existingEquity.VotingRightsPerShare = equityEquity.VotingRightsPerShare;
            existingEquity._20DayAverageVolume = equityEquity._20DayAverageVolume;
            existingEquity.Beta = equityEquity.Beta;
            existingEquity.ShortInterest = equityEquity.ShortInterest;
            existingEquity.YtdReturn = equityEquity.YtdReturn;
            existingEquity._90DayPriceVolatility = equityEquity._90DayPriceVolatility;
            existingEquity.FormPfAssetClass = equityEquity.FormPfAssetClass;
            existingEquity.FormPfCountry = equityEquity.FormPfCountry;
            existingEquity.FormPfCreditRating = equityEquity.FormPfCreditRating;
            existingEquity.FormPfCurrency = equityEquity.FormPfCurrency;
            existingEquity.FormPfInstrument = equityEquity.FormPfInstrument;
            existingEquity.FormPfLiquidityProfile = equityEquity.FormPfLiquidityProfile;
            existingEquity.FormPfMaturity = equityEquity.FormPfMaturity;
            existingEquity.FormPfNaicsCode = equityEquity.FormPfNaicsCode;
            existingEquity.FormPfRegion = equityEquity.FormPfRegion;
            existingEquity.FormPfSector = equityEquity.FormPfSector;
            existingEquity.FormPfSubAssetClass = equityEquity.FormPfSubAssetClass;
            existingEquity.IssueCountry = equityEquity.IssueCountry;
            existingEquity.Exchange = equityEquity.Exchange;
            existingEquity.Issuer = equityEquity.Issuer;
            existingEquity.IssueCurrency = equityEquity.IssueCurrency;
            existingEquity.TradingCurrency = equityEquity.TradingCurrency;
            existingEquity.BloombergIndustrySubGroup = equityEquity.BloombergIndustrySubGroup;
            existingEquity.BloombergIndustryGroup = equityEquity.BloombergIndustryGroup;
            existingEquity.BloombergIndustrySector = equityEquity.BloombergIndustrySector;
            existingEquity.CountryOfIncorporation = equityEquity.CountryOfIncorporation;
            existingEquity.RiskCurrency = equityEquity.RiskCurrency;
            existingEquity.OpenPrice = equityEquity.OpenPrice;
            existingEquity.ClosePrice = equityEquity.ClosePrice;
            existingEquity.Volume = equityEquity.Volume;
            existingEquity.LastPrice = equityEquity.LastPrice;
            existingEquity.AskPrice = equityEquity.AskPrice;
            existingEquity.BidPrice = equityEquity.BidPrice;
            existingEquity.PeRatio = equityEquity.PeRatio;
            existingEquity.DeclaredDate = equityEquity.DeclaredDate;
            existingEquity.ExDate = equityEquity.ExDate;
            existingEquity.RecordDate = equityEquity.RecordDate;
            existingEquity.PayDate = equityEquity.PayDate;
            existingEquity.Amount = equityEquity.Amount;
            existingEquity.Frequency = equityEquity.Frequency;
            existingEquity.DividendType = equityEquity.DividendType;

            try
            {
                // Mark the entry as modified
                _context.Entry(existingEquity).State = EntityState.Modified;

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                // Handle concurrency exception if necessary
                if (!EquityEquityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/EquityEquities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<EquityEquity>> PostEquityEquity([FromBody] EquityEquity equityEquity)
        //{
        //    _context.EquityEquities.Add(equityEquity);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEquityEquity", new { id = equityEquity.SecurityId }, equityEquity);
        //}
        [HttpPost]
        public async Task<ActionResult<EquityEquity>> PostEquityEquity([FromBody] EquityEquity equityEquity)
        {
            _context.EquityEquities.Add(equityEquity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException as SqlException;
                Console.WriteLine($"Error: {innerException?.Message}");
                // Further log details
            }

            
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquityEquity", new { id = equityEquity.SecurityId }, equityEquity);
        }


        // DELETE: api/EquityEquities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquityEquity(int id)
        {
            var equityEquity = await _context.EquityEquities.FindAsync(id);
            if (equityEquity == null)
            {
                return NotFound();
            }

            _context.EquityEquities.Remove(equityEquity);
            await _context.SaveChangesAsync();

            return Ok("Successfully deleted"); 

        }

        // DELETE: api/EquityEquities/ByName
        [HttpDelete("ByName/{securityName}")]
        public async Task<IActionResult> DeleteEquityEquityByName(string securityName)
        {

            var equityEquity = await _context.EquityEquities
                                      .Where(e => e.SecurityName.ToLower() == securityName.ToLower())
                                      .FirstOrDefaultAsync();
            if (equityEquity == null)
            {
                return NotFound($"Equity with name '{securityName}' not found.");
            }

            // Remove the found equity entity.
            _context.EquityEquities.Remove(equityEquity);
            await _context.SaveChangesAsync();

            return Ok($"Successfully deleted the equity with name '{securityName}'.");
        }

        [HttpPatch("Update/{securityId}")]
        public async Task<IActionResult> PatchEquity(
    [FromRoute] int securityId,
    [FromBody] JsonPatchDocument<EquityEquity> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Patch document is null.");
            }

            // Fetch the entity from the database
            var equity = await _context.EquityEquities.FindAsync(securityId);
            if (equity == null)
            {
                return NotFound();
            }

            // Apply the patch with model state validation
            patchDoc.ApplyTo(equity, ModelState);

            // Check if the patch was valid
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage);
                return BadRequest(new { Errors = errors });
            }

            // Explicitly mark entity as modified
            _context.Entry(equity).State = EntityState.Modified;

            // Save changes and check if anything was updated
            var rowsAffected = await _context.SaveChangesAsync();
            if (rowsAffected == 0)
            {
                return BadRequest("No changes were made to the database.");
            }

            return NoContent(); // Success - 204 No Content
        }


        [HttpGet("GetTabWiseEquity/{tabName}")]
        public IActionResult GetTabWise(string tabName)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("MyDBConn");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetTabWiseEquity", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Define SQL output parameter
                        SqlParameter sqlOutput = new SqlParameter("@sql", SqlDbType.NVarChar, -1)
                        {
                            Direction = ParameterDirection.Output
                        };

                         cmd.Parameters.Add(sqlOutput); // Output parameter for SQL query
                //cmd.Parameters.AddWithValue("@name", securityName); // Security Name
                cmd.Parameters.AddWithValue("@TabName", tabName); // Tab Name

                conn.Open();
                cmd.ExecuteNonQuery();

                // Retrieve the dynamically generated SQL query
                string generatedSql = sqlOutput.Value.ToString();

                // Now execute the dynamically generated SQL query
                using (SqlCommand dynamicCmd = new SqlCommand(generatedSql, conn))
                {
                    //dynamicCmd.Parameters.AddWithValue("@name", securityName);

                    using (SqlDataReader reader = dynamicCmd.ExecuteReader())
                    {
                        // Read data dynamically into a list of dictionaries
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

                        return Ok(results);
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Error: " + ex.Message);
    }
}






        private bool EquityEquityExists(int id)
        {
            return _context.EquityEquities.Any(e => e.SecurityId == id);
        }
    }
}
