//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ht.Ihsil.Rgph.App.Superviseur.entites
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_emigre
    {
        public long emigreId { get; set; }
        public Nullable<long> menageId { get; set; }
        public Nullable<long> logeId { get; set; }
        public Nullable<long> batimentId { get; set; }
        public string sdeId { get; set; }
        public Nullable<long> qn1numeroOrdre { get; set; }
        public string qn2aNomComplet { get; set; }
        public Nullable<long> qn2bResidenceActuelle { get; set; }
        public Nullable<long> qn2cSexe { get; set; }
        public Nullable<long> qn2dAgeAuMomentDepart { get; set; }
        public Nullable<long> qn2eNiveauEtudeAuMomentDepart { get; set; }
        public Nullable<long> qn2fDernierPaysResidence { get; set; }
        public Nullable<long> statut { get; set; }
        public Nullable<long> isFieldAllFilled { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public Nullable<long> dureeSaisie { get; set; }
    }
}
