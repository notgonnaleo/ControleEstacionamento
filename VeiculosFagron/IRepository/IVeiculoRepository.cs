using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeiculosFagron.Repository
{
    public interface IVeiculoRepository
    {
        public Task<List<Veiculo>> GetVeiculos();
        public Task<Veiculo> GetVeiculo(int id_veiculo);
        public Task<bool> CreateVeiculo(Veiculo model);
        public Task<bool> UpdateVeiculo(Veiculo model);
        public Task<bool> DeleteVeiculo(Veiculo model);

    }
}
