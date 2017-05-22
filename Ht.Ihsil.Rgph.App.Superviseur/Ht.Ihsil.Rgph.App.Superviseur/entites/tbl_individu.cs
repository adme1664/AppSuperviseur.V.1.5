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
    
    public partial class tbl_individu
    {
        public long individuId { get; set; }
        public Nullable<long> menageId { get; set; }
        public Nullable<long> logeId { get; set; }
        public Nullable<long> batimentId { get; set; }
        public string sdeId { get; set; }
        public Nullable<long> qp1NoOrdre { get; set; }
        public string Q2Nom { get; set; }
        public string Q3Prenom { get; set; }
        public Nullable<long> Q6LienDeParente { get; set; }
        public Nullable<long> Q4Sexe { get; set; }
        public Nullable<long> Q5bAge { get; set; }
        public Nullable<long> qp6religion { get; set; }
        public string qp6AutreReligion { get; set; }
        public Nullable<long> qp7Nationalite { get; set; }
        public string qp7PaysNationalite { get; set; }
        public Nullable<long> qp8MereEncoreVivante { get; set; }
        public Nullable<long> qp9EstPlusAge { get; set; }
        public Nullable<long> qp10LieuNaissance { get; set; }
        public string qp10CommuneNaissance { get; set; }
        public string qp10LieuNaissanceVqse { get; set; }
        public string qp10PaysNaissance { get; set; }
        public Nullable<long> qp11PeriodeResidence { get; set; }
        public Nullable<long> qp12DomicileAvantRecensement { get; set; }
        public string qp12DomicileAvantRecensementComm { get; set; }
        public string qp12DomicileAvantRecensementVqse { get; set; }
        public string qp12DomicileAvantRecensementPays { get; set; }
        public Nullable<long> qe1EstAlphabetise { get; set; }
        public Nullable<long> qe2FreqentationScolaire { get; set; }
        public Nullable<long> qe3typeEtablissement { get; set; }
        public Nullable<long> qe4aNiveauEtude { get; set; }
        public Nullable<long> qe4bDerniereClasseEtude { get; set; }
        public Nullable<long> qe5DiplomeUniversitaire { get; set; }
        public string qe6DomaineEtudeUniversitaire { get; set; }
        public Nullable<long> qe7FormationProf { get; set; }
        public string qe8DomaineFormationProf { get; set; }
        public Nullable<long> qaf1aHandicapVoir { get; set; }
        public Nullable<long> qaf1bUtiliserAppareilVoir { get; set; }
        public Nullable<long> qaf2aHandicapEntendre { get; set; }
        public Nullable<long> qaf2bUtiliserAppareilEntendre { get; set; }
        public Nullable<long> qaf3aHandicapMarcher { get; set; }
        public Nullable<long> qaf3bUtiliserAppareilMarcher { get; set; }
        public Nullable<long> qaf4aHandicapSouvenir { get; set; }
        public Nullable<long> qaf5aHandicapPourSeSoigner { get; set; }
        public Nullable<long> qaf6aHandicapCommuniquer { get; set; }
        public Nullable<long> qaf6bUtiliserAppareilCommuniquer { get; set; }
        public Nullable<long> qt1PossessionTelCellulaire { get; set; }
        public Nullable<long> qt2UtilisationInternet { get; set; }
        public Nullable<long> qem1DejaVivreAutrePays { get; set; }
        public string qem1AutrePays { get; set; }
        public Nullable<long> qem2MoisRetour { get; set; }
        public Nullable<long> qem2AnneeRetour { get; set; }
        public Nullable<long> qsm1StatutMatrimonial { get; set; }
        public Nullable<long> qa1ActEconomiqueDerniereSemaine { get; set; }
        public Nullable<long> qa2ActAvoirDemele1 { get; set; }
        public Nullable<long> qa2ActDomestique2 { get; set; }
        public Nullable<long> qa2ActCultivateur3 { get; set; }
        public Nullable<long> qa2ActAiderParent4 { get; set; }
        public Nullable<long> qa2ActAutre5 { get; set; }
        public Nullable<long> qa3StatutEmploie { get; set; }
        public Nullable<long> qa4SecteurInstitutionnel { get; set; }
        public string qa5TypeBienProduitParEntreprise { get; set; }
        public Nullable<long> qa6LieuActDerniereSemaine { get; set; }
        public string qa7FoncTravail { get; set; }
        public Nullable<long> qa8EntreprendreDemarcheTravail { get; set; }
        public Nullable<long> qa9VouloirTravailler { get; set; }
        public Nullable<long> qa10DisponibilitePourTravail { get; set; }
        public Nullable<long> qa11RecevoirTransfertArgent { get; set; }
        public Nullable<long> qf1aNbreEnfantNeVivantM { get; set; }
        public Nullable<long> qf2bNbreEnfantNeVivantF { get; set; }
        public Nullable<long> qf2aNbreEnfantVivantM { get; set; }
        public Nullable<long> qf2bNbreEnfantVivantF { get; set; }
        public Nullable<long> qf3aNbreEnfantVivantMenageM { get; set; }
        public Nullable<long> qf3bNbreEnfantVivantMenageF { get; set; }
        public Nullable<long> qf3DernierEnfantJour { get; set; }
        public Nullable<long> qf3DernierEnfantMois { get; set; }
        public Nullable<long> qf3DernierEnfantAnnee { get; set; }
        public Nullable<long> qf4SexeDernierVivant { get; set; }
        public Nullable<long> qf5DENeVivantVit { get; set; }
        public Nullable<long> statut { get; set; }
        public Nullable<long> isFieldAllFilled { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public Nullable<long> dureeSaisie { get; set; }
        public Nullable<long> isContreEnqueteMade { get; set; }
    }
}