using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeiculosFagron.Repository
{
    public interface ITenantRepository
    {
        public Task<List<Tenant>> GetTenant();
        public Task<Tenant> GetTenant(int id_tenant);
        public Task<bool> CreateTenant(Tenant model);
        public Task<bool> UpdateTenant(Tenant model);
    }
}
