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
    
    public partial class tbl_menage
    {
        public long menageId { get; set; }
        public Nullable<long> logeId { get; set; }
        public Nullable<long> batimentId { get; set; }
        public string sdeId { get; set; }
        public Nullable<long> qm1NoOrdre { get; set; }
        public Nullable<long> qm2ModeJouissance { get; set; }
        public Nullable<long> qm3ModeObtentionLoge { get; set; }
        public Nullable<long> qm4_1ModeAprovEauABoire { get; set; }
        public Nullable<long> qm4_2ModeAprovEauAUsageCourant { get; set; }
        public Nullable<long> qm5SrcEnergieCuisson1 { get; set; }
        public Nullable<long> qm5SrcEnergieCuisson2 { get; set; }
        public Nullable<long> qm6TypeEclairage { get; set; }
        public Nullable<long> qm7ModeEvacDechet { get; set; }
        public Nullable<long> qm8EndroitBesoinPhysiologique { get; set; }
        public Nullable<long> qm9NbreRadio1 { get; set; }
        public Nullable<long> qm9NbreTelevision2 { get; set; }
        public Nullable<long> qm9NbreRefrigerateur3 { get; set; }
        public Nullable<long> qm9NbreFouElectrique4 { get; set; }
        public Nullable<long> qm9NbreOrdinateur5 { get; set; }
        public Nullable<long> qm9NbreMotoBicyclette6 { get; set; }
        public Nullable<long> qm9NbreVoitureMachine7 { get; set; }
        public Nullable<long> qm9NbreBateau8 { get; set; }
        public Nullable<long> qm9NbrePanneauGeneratrice9 { get; set; }
        public Nullable<long> qm9NbreMilletChevalBourique10 { get; set; }
        public Nullable<long> qm9NbreBoeufVache11 { get; set; }
        public Nullable<long> qm9NbreCochonCabrit12 { get; set; }
        public Nullable<long> qm9NbreBeteVolaille13 { get; set; }
        public Nullable<long> qm10AvoirPersDomestique { get; set; }
        public Nullable<long> qm10TotalDomestiqueFille { get; set; }
        public Nullable<long> qm10TotalDomestiqueGarcon { get; set; }
        public Nullable<long> qm11TotalIndividuVivant { get; set; }
        public Nullable<long> qn1Emigration { get; set; }
        public Nullable<long> qn1NbreEmigre { get; set; }
        public Nullable<long> qd1Deces { get; set; }
        public Nullable<long> qd1NbreDecede { get; set; }
        public Nullable<long> statut { get; set; }
        public Nullable<long> isValidated { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public Nullable<long> dureeSaisie { get; set; }
        public Nullable<long> isFieldAllFilled { get; set; }
        public Nullable<long> isContreEnqueteMade { get; set; }
        public string codeAgentRecenceur { get; set; }
    }
}