using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeiculosFagron.Repository
{
    public interface IBloqueioTransitoRepository
    {
        public Task<List<BloqueioTransito>> GetBloqueios();
        public Task<BloqueioTransito> GetBloqueio(int id_bloqueio);
        public Task<bool> CreateBloqueio(BloqueioTransito model);
        public Task<bool> UpdateBloqueio(BloqueioTransito model);
        public Task<bool> DeleteBloqueio(BloqueioTransito model);
    }
}
