using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeiculosFagron.Repository
{
    public interface ICorRepository
    {
        public Task<List<Cor>> GetCor();
   
    }
}
