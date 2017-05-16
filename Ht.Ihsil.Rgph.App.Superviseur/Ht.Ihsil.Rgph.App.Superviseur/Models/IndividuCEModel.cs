using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class IndividuCEModel
    {
        public long IndividuId { get; set; }
        public long MenageId { get; set; }
        public long LogeId { get; set; }
        public long BatimentId { get; set; }
        public string SdeId { get; set; }
        public Nullable<long> Qp1NoOrdre { get; set; }
        public string Q2Nom { get; set; }
        public string Q3Prenom { get; set; }
        public Nullable<long> Q6LienDeParente { get; set; }
        public Nullable<long> Q4Sexe { get; set; }
        public Nullable<long> Q5bAge { get; set; }
        public Nullable<long> Q7DateNaissanceJour { get; set; }
        public Nullable<long> Q7DateNaissanceMois { get; set; }
        public Nullable<long> Q7DateNaissanceAnnee { get; set; }
        public Nullable<long> Qp7Nationalite { get; set; }
        public string Qp7PaysNationalite { get; set; }
        public Nullable<long> Qp10LieuNaissance { get; set; }
        public string Qp10CommuneNaissance { get; set; }
        public string Qp10LieuNaissanceVqse { get; set; }
        public string Qp10PaysNaissance { get; set; }
        public Nullable<long> Qp11PeriodeResidence { get; set; }
        public Nullable<long> Qe2FreqentationScolaireOuUniv { get; set; }
        public Nullable<long> Qe4aNiveauEtude { get; set; }
        public Nullable<long> Qe4bDerniereClasseOUAneEtude { get; set; }
        public Nullable<long> Qsm1StatutMatrimonial { get; set; }
        public Nullable<long> Qa1ActEconomiqueDerniereSemaine { get; set; }
        public Nullable<long> Qa2ActAvoirDemele1 { get; set; }
        public Nullable<long> Qa2ActDomestique2 { get; set; }
        public Nullable<long> Qa2ActCultivateur3 { get; set; }
        public Nullable<long> Qa2ActAiderParent4 { get; set; }
        public Nullable<long> Qa2ActAutre5 { get; set; }
        public Nullable<long> Qa8EntreprendreDemarcheTravail { get; set; }
        public Nullable<long> Qf1aNbreEnfantNeVivantM { get; set; }
        public Nullable<long> Qf2bNbreEnfantNeVivantF { get; set; }
        public Nullable<long> Qf2aNbreEnfantVivantM { get; set; }
        public Nullable<long> Qf2bNbreEnfantVivantF { get; set; }
        public Nullable<long> Qf3DernierEnfantJour { get; set; }
        public Nullable<long> Qf3DernierEnfantMois { get; set; }
        public Nullable<long> Qf3DernierEnfantAnnee { get; set; }
        public Nullable<long> Qf4DENeVivantVit { get; set; }
        public Nullable<long> Statut { get; set; }
        public Nullable<long> IsValidated { get; set; }
        public string DateDebutCollecte { get; set; }
        public string DateFinCollecte { get; set; }
        public Nullable<long> DureeSaisie { get; set; }
        public Nullable<long> IsContreEnqueteMade { get; set; }
    }
}
