using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeiculosFagron.Repository
{
    public interface IModeloRepository
    {
        public Task<List<Modelo>> GetModelos();

    }
}
