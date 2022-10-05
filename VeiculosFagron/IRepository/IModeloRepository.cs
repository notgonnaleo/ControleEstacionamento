using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeiculosFagron.Repository
{
    public interface IModeloRepository
    {
        public Task<List<Modelo>> GetModelos();
        public Task<Modelo> GetModelo(int id_modelo);
        public Task<bool> CreateModelo(Modelo model);
        public Task<bool> UpdateModelo(Modelo model);
        public Task<bool> DeleteModelo(Modelo model);
    }
}
