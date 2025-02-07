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


namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BondBondsController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly IConfiguration _configuration;
        public BondBondsController(ProjectContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/BondBonds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BondBond>>> GetBondBonds()
        {
            return await _context.BondBonds.ToListAsync();
        }

        // GET: api/BondBonds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BondBond>> GetBondBond(int id)
        {
            var bondBond = await _context.BondBonds.FindAsync(id);

            if (bondBond == null)
            {
                return NotFound();
            }

            return bondBond;
        }

        // GET: api/BondBonds/GetByName/{securityName}
        [HttpGet("GetByName/{securityName}")]
        public async Task<ActionResult<BondBond>> GetBondBondByName(string securityName)
        {
            var bondBond = await _context.BondBonds
                                          .Where(e => e.SecurityName.ToLower() == securityName.ToLower())
                                          .FirstOrDefaultAsync();
            if (bondBond == null)
            {
                return NotFound();
            }

            return bondBond;
        }


        // PUT: api/BondBonds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutById/{id}")]
        public async Task<IActionResult> PutBondBond([FromRoute] int id,[FromBody] BondBond bondBond)
        {
            if (id != bondBond.SecurityId)
            {
                return BadRequest();
            }

            _context.Entry(bondBond).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BondBondExists(id))
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



        // POST: api/BondBonds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BondBond>> PostBondBond([FromBody] BondBond bondBond)
        {
            _context.BondBonds.Add(bondBond);
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
            
            

            return CreatedAtAction("GetBondBond", new { id = bondBond.SecurityId }, bondBond);
        }

        // PUT: api/BondBonds/PutByName/{securityName}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutByName/{securityName}")]
        public async Task<IActionResult> PutBondBondByName([FromRoute] string securityName, [FromBody] BondBond bondBond)
        {
            // Check if the bond bond with the specified name exists
            var existingBondBond = await _context.BondBonds
                                                  .FirstOrDefaultAsync(b => b.SecurityName == securityName);

            if (existingBondBond == null)
            {
                return NotFound();
            }

            // Update all properties
            existingBondBond.SecurityDescription = bondBond.SecurityDescription;
            existingBondBond.AssetType = bondBond.AssetType;
            existingBondBond.InvestmentType = bondBond.InvestmentType;
            existingBondBond.TradingFactor = bondBond.TradingFactor;
            existingBondBond.PricingFactor = bondBond.PricingFactor;

            existingBondBond.Cusip = bondBond.Cusip;
            existingBondBond.Isin= bondBond.Isin;
            existingBondBond.Sedol = bondBond.Sedol;
            existingBondBond.BloombergTicker = bondBond.BloombergTicker;
            existingBondBond.BloombergUniqueId = bondBond.BloombergUniqueId;

            existingBondBond.FirstCouponDate = bondBond.FirstCouponDate;
            existingBondBond.CouponCap = bondBond.CouponCap;
            existingBondBond.CouponFloor = bondBond.CouponFloor;
            existingBondBond.CouponFrequency = bondBond.CouponFrequency;
            existingBondBond.CouponRate = bondBond.CouponRate;
            existingBondBond.CouponType = bondBond.CouponType;

            existingBondBond.FloatSpread = bondBond.FloatSpread;
            existingBondBond.IsCallable = bondBond.IsCallable;
            existingBondBond.IsFixToFloat = bondBond.IsFixToFloat;
            existingBondBond.IsPutable = bondBond.IsPutable;

            existingBondBond.IssueDate = bondBond.IssueDate;
            existingBondBond.LastResetDate = bondBond.LastResetDate;
            existingBondBond.MaturityDate = bondBond.MaturityDate;

            existingBondBond.MaximumCallNoticeDays = bondBond.MaximumCallNoticeDays;
            existingBondBond.MaximumPutNoticeDays = bondBond.MaximumPutNoticeDays;
            existingBondBond.PenultimateCouponDate = bondBond.PenultimateCouponDate;
            existingBondBond.ResetFrequency = bondBond.ResetFrequency;

            existingBondBond.HasPosition = bondBond.HasPosition;
            existingBondBond.Duration = bondBond.Duration;

            existingBondBond.Volatility30d = bondBond.Volatility30d;
            existingBondBond.Volatility90d = bondBond.Volatility90d;
            existingBondBond.Convexity = bondBond.Convexity;
            existingBondBond.AverageVolume30d = bondBond.AverageVolume30d;

            existingBondBond.FormPfAssetClass = bondBond.FormPfAssetClass;
            existingBondBond.FormPfCountry = bondBond.FormPfCountry;
            existingBondBond.FormPfCreditRating = bondBond.FormPfCreditRating;
            existingBondBond.FormPfCurrency = bondBond.FormPfCurrency;
            existingBondBond.FormPfInstrument = bondBond.FormPfInstrument;

            existingBondBond.FormPfLiquidityProfile = bondBond.FormPfLiquidityProfile;
            existingBondBond.FormPfMaturity = bondBond.FormPfMaturity;
            existingBondBond.FormPfNaicsCode = bondBond.FormPfNaicsCode;
            existingBondBond.FormPfRegion = bondBond.FormPfRegion;
            existingBondBond.FormPfSector = bondBond.FormPfSector;
            existingBondBond.FormPfSubAssetClass = bondBond.FormPfSubAssetClass;

            existingBondBond.BloombergIndustryGroup = bondBond.BloombergIndustryGroup;
            existingBondBond.BloombergIndustrySubGroup = bondBond.BloombergIndustrySubGroup;
            existingBondBond.BloombergSector = bondBond.BloombergSector;

            existingBondBond.IssueCountry = bondBond.IssueCountry;
            existingBondBond.IssueCurrency = bondBond.IssueCurrency;
            existingBondBond.Issuer = bondBond.Issuer;
            existingBondBond.RiskCurrency = bondBond.RiskCurrency;

            existingBondBond.PutDate = bondBond.PutDate;
            existingBondBond.PutPrice = bondBond.PutPrice;

            existingBondBond.CallDate = bondBond.CallDate;
            existingBondBond.CallPrice = bondBond.CallPrice;

            existingBondBond.AskPrice = bondBond.AskPrice;
            existingBondBond.HighPrice = bondBond.HighPrice;
            existingBondBond.LowPrice = bondBond.LowPrice;
            existingBondBond.OpenPrice = bondBond.OpenPrice;
            existingBondBond.Volume = bondBond.Volume;
            existingBondBond.BidPrice = bondBond.BidPrice;
            existingBondBond.LastPrice = bondBond.LastPrice;

            // Mark the entity as modified
            _context.Entry(existingBondBond).State = EntityState.Modified;

            try
            {
                // Save changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BondBondExistsByName(securityName))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Return a 204 No Content response indicating the update was successful
            return NoContent();
        }

        // Check if the bond bond exists by its name
        private bool BondBondExistsByName(string securityName)
        {
            return _context.BondBonds.Any(e => e.SecurityName == securityName);
        }


        // DELETE: api/BondBonds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBondBond(int id)
        {
            var bondBond = await _context.BondBonds.FindAsync(id);
            if (bondBond == null)
            {
                return NotFound();
            }

            _context.BondBonds.Remove(bondBond);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/BondBonds/DeleteByName/{securityName}
        [HttpDelete("DeleteByName/{securityName}")]
        public async Task<IActionResult> DeleteBondBondByName([FromRoute] string securityName)
        {
            // Find the bond by Security Name
            var bondBond = await _context.BondBonds
                                          .FirstOrDefaultAsync(b => b.SecurityName == securityName);

            if (bondBond == null)
            {
                return NotFound(); // Return 404 if bond is not found
            }

            // Remove the bond from the database
            _context.BondBonds.Remove(bondBond);
            await _context.SaveChangesAsync(); // Save the changes

            return NoContent(); // Return 204 No Content to indicate successful deletion
        }

        

    [HttpPatch("UpdateHasPositionByName/{securityName}")]
    public async Task<IActionResult> PatchHasPositionByName([FromRoute] string securityName,
    [FromBody] JsonPatchDocument<BondBond> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest("Invalid patch document.");
        }

        var bondBond = await _context.BondBonds.FirstOrDefaultAsync(b => b.SecurityName == securityName);

        if (bondBond == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(bondBond, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(500, "An error occurred while updating HasPosition.");
        }

        return Ok("HasPosition updated successfully.");
    }


        [HttpGet("GetTabWiseBond/{tabName}")]
        public IActionResult GetTabWise(string tabName)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("MyDBConn");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetTabWiseBond", conn))
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

        private bool BondBondExists(int id)
        {
            return _context.BondBonds.Any(e => e.SecurityId == id);
        }
    }
}
