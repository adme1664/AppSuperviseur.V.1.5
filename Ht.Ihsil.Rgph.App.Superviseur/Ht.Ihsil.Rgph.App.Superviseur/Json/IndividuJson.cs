using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Json
{
    public class IndividuJson
    {
        public long IndividuId { get; set; }
        public long MenageId { get; set; }
        public long LogeId { get; set; }
        public long BatimentId { get; set; }
        public string SdeId { get; set; }
        public short Q1NoOrdre { get; set; }
        public string Qp2APrenom { get; set; }
        public string Qp2BNom { get; set; }
        public short Qp3LienDeParente { get; set; }
        public short Qp3HabiteDansMenage { get; set; }
        public short Qp4Sexe { get; set; }
        public short Qp5DateNaissanceJour { get; set; }
        public short Qp5DateNaissanceMois { get; set; }
        public int Qp5DateNaissanceAnnee { get; set; }
        public short Qp5bAge { get; set; }
        public short Qp6religion { get; set; }
        public string Qp6AutreReligion { get; set; }
        public short Qp7Nationalite { get; set; }
        public string Qp7PaysNationalite { get; set; }
        public short Qp8MereEncoreVivante { get; set; }
        public short Qp9EstPlusAge { get; set; }
        public short Qp10LieuNaissance { get; set; }
        public string Qp10CommuneNaissance { get; set; }
        public string Qp10VqseNaissance { get; set; }
        public string Qp10PaysNaissance { get; set; }
        public short Qp11PeriodeResidence { get; set; }
        public short Qp12DomicileAvantRecensement { get; set; }
        public string Qp12CommuneDomicileAvantRecensement { get; set; }
        public string Qp12VqseDomicileAvantRecensement { get; set; }
        public string Qp12PaysDomicileAvantRecensement { get; set; }
        public short Qe1EstAlphabetise { get; set; }
        public short Qe2FreqentationScolaireOuUniv { get; set; }
        public short Qe3typeEcoleOuUniv { get; set; }
        public short Qe4aNiveauEtude { get; set; }
        public string Qe4bDerniereClasseOUAneEtude { get; set; }
        public short Qe5DiplomeUniversitaire { get; set; }
        public string Qe6DomaineEtudeUniversitaire { get; set; }
        public short Qaf1HandicapVoir { get; set; }
        public short Qaf2HandicapEntendre { get; set; }
        public short Qaf3HandicapMarcher { get; set; }
        public short Qaf4HandicapSouvenir { get; set; }
        public short Qaf5HandicapPourSeSoigner { get; set; }
        public short Qaf6HandicapCommuniquer { get; set; }
        public short Qt1PossessionTelCellulaire { get; set; }
        public short Qt2UtilisationInternet { get; set; }
        public short Qem1DejaVivreAutrePays { get; set; }
        public string Qem1AutrePays { get; set; }
        public short Qem2MoisRetour { get; set; }
        public int Qem2AnneeRetour { get; set; }
        public short Qsm1StatutMatrimonial { get; set; }
        public short Qa1ActEconomiqueDerniereSemaine { get; set; }
        public short Qa2ActAvoirDemele1 { get; set; }
        public short Qa2ActDomestique2 { get; set; }
        public short Qa2ActCultivateur3 { get; set; }
        public short Qa2ActAiderParent4 { get; set; }
        public short Qa2ActAutre5 { get; set; }
        public short Qa3StatutEmploie { get; set; }
        public short Qa4SecteurInstitutionnel { get; set; }
        public string Qa5TypeBienProduitParEntreprise { get; set; }
        public string Qa5PreciserTypeBienProduitParEntreprise { get; set; }
        public short Qa6LieuActDerniereSemaine { get; set; }
        public short Qa7FoncTravail { get; set; }
        public short Qa8EntreprendreDemarcheTravail { get; set; }
        public short Qa9VouloirTravailler { get; set; }
        public short Qa10DisponibilitePourTravail { get; set; }
        public short Qa11RecevoirTransfertArgent { get; set; }
        public int Qf1aNbreEnfantNeVivantM { get; set; }
        public int Qf1bNbreEnfantNeVivantF { get; set; }
        public int Qf2aNbreEnfantVivantM { get; set; }
        public int Qf2bNbreEnfantVivantF { get; set; }
        public short Qf3DernierEnfantJour { get; set; }
        public short Qf3DernierEnfantMois { get; set; }
        public int Qf3DernierEnfantAnnee { get; set; }
        public short Qf4DENeVivantVit { get; set; }
        public short Statut { get; set; }
        public bool IsFieldAllFilled { get; set; }
        public string DateDebutCollecte { get; set; }
        public string DateFinCollecte { get; set; }
        public int DureeSaisie { get; set; }
        public bool IsContreEnqueteMade { get; set; }
        public string CodeAgentRecenceur { get; set; }
    }
}
