using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeiculosFagron.Repository
{
    public interface ITenantRepository
    {
        public Task<List<Tenant>> GetTenant();
       

    }
}
