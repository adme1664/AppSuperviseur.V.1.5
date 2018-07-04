using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Json
{
    public class IndividuJson
    {
        public long individuId { get; set; }
        public long menageId { get; set; }
        public long logeId { get; set; }
        public long batimentId { get; set; }
        public string sdeId { get; set; }
        public short q1NoOrdre { get; set; }
        public string qp2APrenom { get; set; }
        public string qp2BNom { get; set; }
        public short qp3LienDeParente { get; set; }
        public short q3aRaisonChefMenage { get; set; }
        public short qp3HabiteDansMenage { get; set; }
        public short qp4Sexe { get; set; }
        public short qp5DateNaissanceJour { get; set; }
        public short qp5DateNaissanceMois { get; set; }
        public int qp5DateNaissanceAnnee { get; set; }
        public int qp5bAge { get; set; }
        public short qp6religion { get; set; }
        public string qp6AutreReligion { get; set; }
        public short qp7Nationalite { get; set; }
        public string qp7PaysNationalite { get; set; }
        public short qp8MereEncoreVivante { get; set; }
        public short qp9EstPlusAge { get; set; }
        public short qp10LieuNaissance { get; set; }
        public string qp10CommuneNaissance { get; set; }
        public string qp10VqseNaissance { get; set; }
        public string qp10PaysNaissance { get; set; }
        public short qp11PeriodeResidence { get; set; }
        public short qp12DomicileAvantRecensement { get; set; }
        public string qp12CommuneDomicileAvantRecensement { get; set; }
        public string qp12VqseDomicileAvantRecensement { get; set; }
        public string qp12PaysDomicileAvantRecensement { get; set; }
        public short qe1EstAlphabetise { get; set; }
        public short qe2FreqentationScolaireOuUniv { get; set; }
        public short qe3typeEcoleOuUniv { get; set; }
        public short qe4aNiveauEtude { get; set; }
        public string qe4bDerniereClasseOUAneEtude { get; set; }
        public short qe5DiplomeUniversitaire { get; set; }
        public string qe6DomaineEtudeUniversitaire { get; set; }
        public short qaf1HandicapVoir { get; set; }
        public short qaf2HandicapEntendre { get; set; }
        public short qaf3HandicapMarcher { get; set; }
        public short qaf4HandicapSouvenir { get; set; }
        public short qaf5HandicapPourSeSoigner { get; set; }
        public short qaf6HandicapCommuniquer { get; set; }
        public short qt1PossessionTelCellulaire { get; set; }
        public short qt2UtilisationInternet { get; set; }
        public short qem1DejaVivreAutrePays { get; set; }
        public string qem1AutrePays { get; set; }
        public short qem2MoisRetour { get; set; }
        public int qem2AnneeRetour { get; set; }
        public short qsm1StatutMatrimonial { get; set; }
        public short qa1ActEconomiqueDerniereSemaine { get; set; }
        public short qa2ActAvoirDemele1 { get; set; }
        public short qa2ActDomestique2 { get; set; }
        public short qa2ActCultivateur3 { get; set; }
        public short qa2ActAiderParent4 { get; set; }
        public short qa2ActAutre5 { get; set; }
        public short qa3StatutEmploie { get; set; }
        public short qa4SecteurInstitutionnel { get; set; }
        public string qa5TypeBienProduitParEntreprise { get; set; }
        public string qa5PreciserTypeBienProduitParEntreprise { get; set; }
        public short qa6LieuActDerniereSemaine { get; set; }
        public short qa7FoncTravail { get; set; }
        public short qa8EntreprendreDemarcheTravail { get; set; }
        public short qa9VouloirTravailler { get; set; }
        public short qa10DisponibilitePourTravail { get; set; }
        public short qa11RecevoirTransfertArgent { get; set; }
        public int qf1aNbreEnfantNeVivantM { get; set; }
        public int qf1bNbreEnfantNeVivantF { get; set; }
        public int qf2aNbreEnfantVivantM { get; set; }
        public int qf2bNbreEnfantVivantF { get; set; }
        public short qf3DernierEnfantJour { get; set; }
        public short qf3DernierEnfantMois { get; set; }
        public int qf3DernierEnfantAnnee { get; set; }
        public short qf4DENeVivantVit { get; set; }
        public short statut { get; set; }
        public bool isFieldAllFilled { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public int dureeSaisie { get; set; }
        public bool isContreEnqueteMade { get; set; }
        public string codeAgentRecenceur { get; set; }
        public bool verified { get; set; }

    }
}
