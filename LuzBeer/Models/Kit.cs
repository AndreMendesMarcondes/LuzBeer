using System;

namespace LuzBeer.Models
{
    [Serializable]
    public class Kit
    {
        public string Mes { get; set; }
        public string PrimeiraCerveja { get; set; }
        public string PrimeiraDescricao { get; set; }
        public string SegundaCerveja { get; set; }
        public string SegundaDescricao { get; set; }
        public string CaminhoFotoKit { get; set; }
    }
}