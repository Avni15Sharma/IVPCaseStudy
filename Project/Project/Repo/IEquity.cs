using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repo
{
    public interface IEquity
    {
        public Task<List<EquityEquity>> GetEquities();
        public Task<EquityEquity?> GetEquityByName(string securityName);
        public Task<bool> UpdateEquityByName(string securityName, EquityEquity equityEquity);
        public Task<EquityEquity?> CreateEquity(EquityEquity equityEquity);
        public Task<EquityEquity?> GetEquityById(int id);
        public Task<bool> DeleteEquityByName(string securityName);
        public Task<List<Dictionary<string, object>>> GetTabWise(string tabName);
        public Task<bool> EquityEquityExists(int id);
    }

}
