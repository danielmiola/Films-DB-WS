//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FilmsDB.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class Generos
    {
        public Generos()
        {
            this.Filmes = new HashSet<Filmes>();
        }
    
        public int GeneroID { get; set; }
        public string Descricao { get; set; }

        [JsonIgnore]
        public virtual ICollection<Filmes> Filmes { get; set; }
    }
}
