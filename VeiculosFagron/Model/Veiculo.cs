using System;

namespace VeiculosFagron
{
    public class Veiculo
    {
        public int id_veiculo { get; set; }
        public DateTime data_cadastro { get; set; }
        public int id_placa { get; set; }
        public int id_cor { get; set; }
        public float km { get; set; }
        public int id_modelo { get; set; }
    }
}
