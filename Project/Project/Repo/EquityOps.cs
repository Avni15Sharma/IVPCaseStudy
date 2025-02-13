using System.Configuration;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repo
{
    public class EquityOps : IEquity
    {
        private readonly ProjectContext _context;
        private readonly IConfiguration _configuration;
        public EquityOps(ProjectContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<EquityEquity>> GetEquities()
        {
            return await _context.EquityEquities.ToListAsync();
        }


        public async Task<EquityEquity?> GetEquityByName(string securityName)
        {
            return await _context.EquityEquities
                                 .FirstOrDefaultAsync(e => e.SecurityName.ToLower() == securityName.ToLower());
        }

        public async Task<bool> UpdateEquityByName(string securityName, EquityEquity equityEquity)
        {
            var existingEquity = await _context.EquityEquities
                                               .FirstOrDefaultAsync(e => e.SecurityName == securityName);

            if (existingEquity == null)
            {
                return false; 
            }
 
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

            _context.EquityEquities.Update(existingEquity);
            await _context.SaveChangesAsync();

            return true; 
        }

        public async Task<EquityEquity?> CreateEquity(EquityEquity equityEquity)
        {
            try
            {
                _context.EquityEquities.Add(equityEquity);
                await _context.SaveChangesAsync();

                var savedEntity = await _context.EquityEquities
                                .FirstOrDefaultAsync(e => e.SecurityName == equityEquity.SecurityName);

                return savedEntity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");
                return null; 
            }
        }


        public async Task<EquityEquity?> GetEquityById(int id)
        {
            return await _context.EquityEquities.FindAsync(id);
        }

        public async Task<bool> DeleteEquityByName(string securityName)
        {
            var equity = await _context.EquityEquities
                                       .FirstOrDefaultAsync(e => EF.Functions.Like(e.SecurityName, securityName));

            if (equity == null)
            {
                
                return false;
            }

            _context.EquityEquities.Remove(equity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Dictionary<string, object>>> GetTabWise(string tabName)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("MyDBConn");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetTabWiseEquity", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter sqlOutput = new SqlParameter("@sql", SqlDbType.NVarChar, -1)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(sqlOutput);
                        cmd.Parameters.AddWithValue("@TabName", tabName);

                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        string generatedSql = sqlOutput.Value.ToString();

                        using (SqlCommand dynamicCmd = new SqlCommand(generatedSql, conn))
                        {
                            using (SqlDataReader reader = await dynamicCmd.ExecuteReaderAsync())
                            {
                                List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

                                while (await reader.ReadAsync())
                                {
                                    Dictionary<string, object> row = new Dictionary<string, object>();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        row[reader.GetName(i)] = reader[i] is DBNull ? null : reader[i];
                                    }
                                    results.Add(row);
                                }

                                return results;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving tabwise data: " + ex.Message);
            }
        }

        public async Task<bool> EquityEquityExists(int id)
        {
            return await _context.EquityEquities.AnyAsync(e => e.SecurityId == id);
        }
    }
}
