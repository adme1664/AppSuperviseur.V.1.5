using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class EvaluationModel
    {
        public long BatimentId { get; set; }
        public long LogeId { get; set; }
        public long MenageId { get; set; }
        public string SdeId { get; set; }
        public Nullable<byte> Qa1StatutQuestionnaire { get; set; }
        public Nullable<byte> Qa1RaisonStatut { get; set; }
        public Nullable<int> Qb1RepondantNoOrdre { get; set; }
        public Nullable<int> Qb1RepondantRChefMenage { get; set; }
        public Nullable<int> Qb1RepondantSexe { get; set; }
        public Nullable<int> Qb1RepondantAge { get; set; }
        public Nullable<int> Qb1RepondantNiveauEtude { get; set; }
        public Nullable<int> Qc1MembreMenage { get; set; }
        public Nullable<int> Qc2MembreMenage { get; set; }
        public Nullable<int> Qc3MembreMenageNoOrdre { get; set; }
        public string Qc3MembreMenageNom { get; set; }
        public Nullable<int> Qc1Mortalite { get; set; }
        public Nullable<int> Qc2Mortalite { get; set; }
        public Nullable<int> Qc3MortaliteNoOrdre { get; set; }
        public string Qc3MortaliteNom { get; set; }
        public Nullable<int> Qc1Education { get; set; }
        public Nullable<int> Qc2Education { get; set; }
        public Nullable<int> Qc3EducationNoOrdre { get; set; }
        public string Qc3EducationNom { get; set; }
        public Nullable<int> Qc1Fonctionnement { get; set; }
        public Nullable<int> Qc2Fonctionnement { get; set; }
        public Nullable<int> Qc3FonctionnementNoOrdre { get; set; }
        public string Qc3FonctionnementNom { get; set; }
        public Nullable<int> Qc1Economique { get; set; }
        public Nullable<int> Qc2Economique { get; set; }
        public Nullable<int> Qc3EconomiqueNoOrdre { get; set; }
        public string Qc3EconomiqueNom { get; set; }
        public Nullable<int> Qc1Fecondite { get; set; }
        public Nullable<int> Qc2Fecondite { get; set; }
        public Nullable<int> Qc3FeconditeNoOrdre { get; set; }
        public string Qc3FeconditeNom { get; set; }
        public Nullable<int> Qd11NbrePerVivant { get; set; }
        public Nullable<int> Qd12NbrePerVivantG { get; set; }
        public Nullable<int> Qd13NbrePerVivantF { get; set; }
        public Nullable<int> Qd21NbrePerRecense { get; set; }
        public Nullable<int> Qd22NbrePerRecenseG { get; set; }
        public Nullable<int> Qd23NbrePerRecenseF { get; set; }
        public Nullable<int> Qd31NbrePerUneAnnee { get; set; }
        public Nullable<int> Qd32NbrePerUneAnneeG { get; set; }
        public Nullable<int> Qd33NbrePerUneAnneeF { get; set; }
        public Nullable<int> Qd41NbrePerCinqAnnee { get; set; }
        public Nullable<int> Qd42NbrePerCinqAnneeG { get; set; }
        public Nullable<int> Qd43NbrePerCinqAnneeF { get; set; }
        public Nullable<int> Qd5nbreFilleTreizeAnnee { get; set; }
        public Nullable<int> Qe1StatutFinal { get; set; }
        public Nullable<int> Qe1RaisonStatutFinal { get; set; }
        public string NomSuperviseur { get; set; }
        public Nullable<System.DateTime> DateContreEnquete { get; set; }
        public Nullable<System.DateTime> DureEntrevue { get; set; }
        public string NomResponsableCom { get; set; }
        public Nullable<System.DateTime> DateVerification { get; set; }
    }
}
