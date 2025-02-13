using System.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repo
{
    public interface IBond
    {
        Task<IEnumerable<BondBond>> GetAllBonds();
        Task<BondBond> GetBondById(int id);
        Task<BondBond> GetBondByName(string securityName);
        Task<bool> UpdateBond(string securityName, BondBond bondBond);
        Task<BondBond> CreateBond(BondBond bondBond);
        Task<bool> DeleteBondByName(string securityName);
        Task<bool> PatchHasPosition(string securityName, JsonPatchDocument<BondBond> patchDoc);
        public Task<bool> BondBondExistsByName(string securityName);
        public Task<List<Dictionary<string, object>>> GetTabWise(string tabName);
        public Task<bool> BondBondExists(int id);
    }
}
