//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRUDGame
{
    using System;
    using System.Collections.Generic;
    
    public partial class Personagen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personagen()
        {
            this.Personagem_Habilidade = new HashSet<Personagem_Habilidade>();
        }
    
        public int IdPersonagem { get; set; }
        public int Forca { get; set; }
        public int Destreza { get; set; }
        public int Sabedoria { get; set; }
        public int Constituicao { get; set; }
        public int Inteligencia { get; set; }
        public int Carisma { get; set; }
        public decimal Peso { get; set; }
        public decimal Altura { get; set; }
        public int CorCabelo { get; set; }
        public string EstiloCabelo { get; set; }
        public int CorOlho { get; set; }
        public int CorPele { get; set; }
        public System.DateTime DataNasc { get; set; }
        public int Nivel { get; set; }
        public string NomePersonagem { get; set; }
        public string Sexo { get; set; }
        public int RacaID { get; set; }
        public int SubclasseID { get; set; }
    
        public virtual Core Core { get; set; }
        public virtual Core Core1 { get; set; }
        public virtual Core Core2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Personagem_Habilidade> Personagem_Habilidade { get; set; }
        public virtual Raca Raca { get; set; }
        public virtual Subclass Subclass { get; set; }
    }
}
