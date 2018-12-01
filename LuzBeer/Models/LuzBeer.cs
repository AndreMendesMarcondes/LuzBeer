using System;
using System.Collections.Generic;


namespace LuzBeer.Models
{
    [Serializable]
    public class LuzBeer
    {
        public Admin Admin { get; set; }
        public List<Cerveja> ListaCervejas { get; set; }
        public Kit Kit { get; set; }
        public CervejaFixa CervejaFixa { get; set; }
    }
}