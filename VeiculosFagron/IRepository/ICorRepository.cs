using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeiculosFagron.Repository
{
    public interface ICorRepository
    {
        public Task<List<Cor>> GetCores();
        public Task<Cor> GetCor(int id_cor);
        public Task<bool> CreateCor(Cor model);
        public Task<bool> UpdateCor(Cor model);
        public Task<bool> DeleteCor(Cor model);
    }
}
