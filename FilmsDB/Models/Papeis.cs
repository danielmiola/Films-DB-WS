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
    
    public partial class Papeis
    {
        public int FilmeID { get; set; }
        public int AtorID { get; set; }
        public string NomePersonagem { get; set; }

        [JsonIgnore]
        public virtual Atores Atores { get; set; }
        [JsonIgnore]
        public virtual Filmes Filmes { get; set; }
    }
}
