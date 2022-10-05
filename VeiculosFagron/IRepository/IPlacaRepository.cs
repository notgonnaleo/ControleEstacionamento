using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeiculosFagron.Repository
{
    public interface IPlacaRepository
    {
        public Task<List<Placa>> GetPlacas();
        public Task<Placa> GetPlaca(int id_placa);
        public Task<bool> CreatePlaca(Placa model);
        public Task<bool> UpdatePlaca(Placa model);
        public Task<bool> DeletePlaca(Placa model);
    }
}
