//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_rapportrar
    {
        public long rapportId { get; set; }
        public Nullable<long> batimentId { get; set; }
        public Nullable<long> logeId { get; set; }
        public Nullable<long> menageId { get; set; }
        public Nullable<long> emigreId { get; set; }
        public Nullable<long> decesId { get; set; }
        public Nullable<long> individuId { get; set; }
        public string rapportModuleName { get; set; }
        public string codeQuestionStop { get; set; }
        public Nullable<long> visiteNumber { get; set; }
        public Nullable<long> raisonActionId { get; set; }
        public string autreRaisonAction { get; set; }
        public Nullable<long> isFieldAllFilled { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public Nullable<long> dureeSaisie { get; set; }
        public Nullable<long> isContreEnqueteMade { get; set; }
        public string codeAgentRecenceur { get; set; }
    }
}
