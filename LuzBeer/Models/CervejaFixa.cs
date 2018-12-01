using System;

namespace LuzBeer.Models
{
    public class CervejaFixa
    {
        public CervejaFixa()
        {
            CervejaId = Guid.NewGuid();
        }
        public Guid CervejaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string DescricaoCompleta { get; set; }
        public string QuantidadeMl { get; set; }
        public string Temperatura { get; set; }
        public string Amargor { get; set; }
        public string Coloracao { get; set; }
        public string CaminhoFoto { get; set; }
        public bool Ativo { get; set; }
    }
}