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
    
    public partial class Filmes
    {
        public Filmes()
        {
            this.Papeis = new HashSet<Papeis>();
            this.Reviews = new HashSet<Reviews>();
            this.Studios = new HashSet<Studios>();
            this.Generos = new HashSet<Generos>();
        }
    
        public int FilmeID { get; set; }
        public string Titulo { get; set; }
        public Nullable<int> Duracao { get; set; }
        public Nullable<int> AnoLancamento { get; set; }
    
        [JsonIgnore]
        public virtual ICollection<Papeis> Papeis { get; set; }
        [JsonIgnore]
        public virtual ICollection<Reviews> Reviews { get; set; }
        [JsonIgnore]
        public virtual ICollection<Studios> Studios { get; set; }
        [JsonIgnore]
        public virtual ICollection<Generos> Generos { get; set; }
    }
}