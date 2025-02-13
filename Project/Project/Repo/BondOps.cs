using System.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repo
{
    public class BondOps: IBond
    {
        private readonly ProjectContext _context;
        private readonly IConfiguration _configuration;
        public BondOps(ProjectContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<BondBond>> GetAllBonds()
        {
            return await _context.BondBonds.ToListAsync();
        }

        public async Task<BondBond> GetBondById(int id)
        {
            return await _context.BondBonds.FindAsync(id);
        }

        public async Task<BondBond> GetBondByName(string securityName)
        {
            return await _context.BondBonds.FirstOrDefaultAsync(e => e.SecurityName.ToLower() == securityName.ToLower());
        }

        public async Task<bool> UpdateBond(string securityName, BondBond bondBond)
        {
            var existingBond = await _context.BondBonds
                                             .FirstOrDefaultAsync(e => e.SecurityName == securityName);

            if (existingBond == null)
            {
                return false; 
            }

    
            existingBond.SecurityDescription = bondBond.SecurityDescription;
            existingBond.AssetType = bondBond.AssetType;
            existingBond.InvestmentType = bondBond.InvestmentType;
            existingBond.TradingFactor = bondBond.TradingFactor;
            existingBond.PricingFactor = bondBond.PricingFactor;
            existingBond.Cusip = bondBond.Cusip;
            existingBond.Isin = bondBond.Isin;
            existingBond.Sedol = bondBond.Sedol;
            existingBond.BloombergTicker = bondBond.BloombergTicker;
            existingBond.BloombergUniqueId = bondBond.BloombergUniqueId;
            existingBond.FirstCouponDate = bondBond.FirstCouponDate;
            existingBond.CouponCap = bondBond.CouponCap;
            existingBond.CouponFloor = bondBond.CouponFloor;
            existingBond.CouponFrequency = bondBond.CouponFrequency;
            existingBond.CouponRate = bondBond.CouponRate;
            existingBond.CouponType = bondBond.CouponType;
            existingBond.FloatSpread = bondBond.FloatSpread;
            existingBond.IsCallable = bondBond.IsCallable;
            existingBond.IsFixToFloat = bondBond.IsFixToFloat;
            existingBond.IsPutable = bondBond.IsPutable;
            existingBond.IssueDate = bondBond.IssueDate;
            existingBond.LastResetDate = bondBond.LastResetDate;
            existingBond.MaturityDate = bondBond.MaturityDate;
            existingBond.MaximumCallNoticeDays = bondBond.MaximumCallNoticeDays;
            existingBond.MaximumPutNoticeDays = bondBond.MaximumPutNoticeDays;
            existingBond.PenultimateCouponDate = bondBond.PenultimateCouponDate;
            existingBond.ResetFrequency = bondBond.ResetFrequency;
            existingBond.HasPosition = bondBond.HasPosition;
            existingBond.Duration = bondBond.Duration;
            existingBond.Volatility30d = bondBond.Volatility30d;
            existingBond.Volatility90d = bondBond.Volatility90d;
            existingBond.Convexity = bondBond.Convexity;
            existingBond.AverageVolume30d = bondBond.AverageVolume30d;
            existingBond.FormPfAssetClass = bondBond.FormPfAssetClass;
            existingBond.FormPfCountry = bondBond.FormPfCountry;
            existingBond.FormPfCreditRating = bondBond.FormPfCreditRating;
            existingBond.FormPfCurrency = bondBond.FormPfCurrency;
            existingBond.FormPfInstrument = bondBond.FormPfInstrument;
            existingBond.FormPfLiquidityProfile = bondBond.FormPfLiquidityProfile;
            existingBond.FormPfMaturity = bondBond.FormPfMaturity;
            existingBond.FormPfNaicsCode = bondBond.FormPfNaicsCode;
            existingBond.FormPfRegion = bondBond.FormPfRegion;
            existingBond.FormPfSector = bondBond.FormPfSector;
            existingBond.FormPfSubAssetClass = bondBond.FormPfSubAssetClass;
            existingBond.BloombergIndustryGroup = bondBond.BloombergIndustryGroup;
            existingBond.BloombergIndustrySubGroup = bondBond.BloombergIndustrySubGroup;
            existingBond.BloombergSector = bondBond.BloombergSector;
            existingBond.IssueCountry = bondBond.IssueCountry;
            existingBond.IssueCurrency = bondBond.IssueCurrency;
            existingBond.Issuer = bondBond.Issuer;
            existingBond.RiskCurrency = bondBond.RiskCurrency;
            existingBond.PutDate = bondBond.PutDate;
            existingBond.PutPrice = bondBond.PutPrice;
            existingBond.CallDate = bondBond.CallDate;
            existingBond.CallPrice = bondBond.CallPrice;
            existingBond.AskPrice = bondBond.AskPrice;
            existingBond.HighPrice = bondBond.HighPrice;
            existingBond.LowPrice = bondBond.LowPrice;
            existingBond.OpenPrice = bondBond.OpenPrice;
            existingBond.Volume = bondBond.Volume;
            existingBond.BidPrice = bondBond.BidPrice;
            existingBond.LastPrice = bondBond.LastPrice;

            _context.BondBonds.Update(existingBond);
            await _context.SaveChangesAsync();

            return true; 
        }

        public async Task<BondBond> CreateBond(BondBond bondBond)
        {
            try
            {
                _context.BondBonds.Add(bondBond);
                await _context.SaveChangesAsync();

                var savedEntity = await _context.BondBonds
                                .FirstOrDefaultAsync(e => e.SecurityName == bondBond.SecurityName);

                return savedEntity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");
                return null;
            }
        }


        public async Task<bool> DeleteBondByName(string securityName)
        {
            var bondBond = await _context.BondBonds.FirstOrDefaultAsync(b => b.SecurityName == securityName);
            if (bondBond == null)
                return false;

            _context.BondBonds.Remove(bondBond);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PatchHasPosition(string securityName, JsonPatchDocument<BondBond> patchDoc)
        {
            var bondBond = await _context.BondBonds.FirstOrDefaultAsync(b => b.SecurityName == securityName);
            if (bondBond == null)
                return false;

            patchDoc.ApplyTo(bondBond);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Dictionary<string, object>>> GetTabWise(string tabName)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("MyDBConn");
                List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("GetTabWiseBond", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter sqlOutput = new SqlParameter("@sql", SqlDbType.NVarChar, -1)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(sqlOutput);
                        cmd.Parameters.AddWithValue("@TabName", tabName);

                        await cmd.ExecuteNonQueryAsync();

                        string generatedSql = sqlOutput.Value?.ToString();
                        if (string.IsNullOrEmpty(generatedSql))
                        {
                            throw new Exception("Generated SQL is null or empty.");
                        }

                        using (SqlCommand dynamicCmd = new SqlCommand(generatedSql, conn))
                        {
                            using (SqlDataReader reader = await dynamicCmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    Dictionary<string, object> row = new Dictionary<string, object>();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        row[reader.GetName(i)] = reader[i] is DBNull ? null : reader[i];
                                    }
                                    results.Add(row);
                                }
                            }
                        }
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: {ex.Message}");
            }
        }


        public async Task<bool> BondBondExists(int id)
        {
            return await _context.BondBonds.AnyAsync(e => e.SecurityId == id);
        }

        public async Task<bool> BondBondExistsByName(string securityName)
        {
            return await _context.BondBonds.AnyAsync(e => e.SecurityName == securityName);
        }
    }
}
