using System;

namespace LuzBeer.Models
{
    [Serializable]
    public class Admin
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}