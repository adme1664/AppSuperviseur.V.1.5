//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Utilisateur
    {
        public long UtilisateurId { get; set; }
        public string CodeUtilisateur { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string MotDePasse { get; set; }
        public Nullable<long> Statut { get; set; }
        public Nullable<long> ProfileId { get; set; }
    }
}
