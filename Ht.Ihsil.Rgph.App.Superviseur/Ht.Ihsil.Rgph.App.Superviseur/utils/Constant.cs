using Ht.Ihsil.Rgph.App.Superviseur.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.utils
{
    public class Constant
    {

        public static int STATUS_SENT = 1;
        public static int STATUS_TO_RESEND = 2;
        public static int STATUS_VALIDATED_1 = 1;
        public static int STATUS_NOT_VALIDATED_0 = 0;

        public static short STATUT_MODULE_KI_FINI_1 = 1;
        public static short STATUT_MODULE_KI_MAL_RANPLI_2 = 2;
        public static short STATUT_MODULE_KI_PA_FINI_3 = 3;
        public static string SDE_ZONE_RURAL = "2";
        public static string SDE_ZONE_URBAINE = "1";

        public static string STATUT_EFFECTUE = "Effectue";
        public static string STATUT_NON_EFFECTUE = "Non Effectue";
        public enum StatutModule : short
        {
            Fini = 1,
            MalRempli = 2,
            PasFini = 3
        }
        public enum StatutVerifie : short
        {
            Verifie = 1,
            PasVerifie = 0
        }
        public enum StatutValide : short
        {
            Valide = 1,
            PasValide = 0
        }

        public enum EtatBatiment : int
        {
            Inobservable = 5
        }

        #region VARIABLES
        public static string RESPONSE_HEADER_SUCCESS = "SUCCESS";
        public static string RESPONSE_HEADER_FAILED = "FAILED";

        public static string LOJMAN_KOLEKTIF = "Lojman kolektif";
        public static string LOJMAN_ENVIDIVIDYEL = "Lojman Endividyel";

        public static string STR_TYPE_DECES = "Dèsè";
        public static string STR_TYPE_EMIGRE = "Emigre";
        public static string STR_TYPE_ENVDIVIDI = "Endividi";

        public static int CODE_TYPE_DECES = 1;
        public static int CODE_TYPE_EMIGRE = 2;
        public static int CODE_TYPE_ENVDIVIDI = 3;
        public static int CODE_TYPE_RAPO = 0;

        public static string DATACONTEXT_BATIMENTVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.BatimentViewModel";
        public static string DATACONTEXT_SDEVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.SdeViewModel";
        public static string DATACONTEXT_LOGEMENTVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.LogementViewModel";
        public static string DATACONTEXT_MENAGEDETAILSVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.MenageDetailsViewModel";
        public static string DATACONTEXT_MENAGEVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.MenageViewModel";
        public static string DATACONTEXT_INDIVIDUVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.IndividuViewModel";

        public static string DATACONTEXT_BATIMENTCEVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.ContreEnquete.BatimentCEViewModel";
        public static string DATACONTEXT_SDECEVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.ContreEnquete.SdeCEViewModel";
        public static string DATACONTEXT_LOGEMENTCEVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.ContreEnquete.LogementCEViewModel";
        public static string DATACONTEXT_CONTREENQUETECEVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.ContreEnquete.ContreEnqueteViewModel";
        public static string DATACONTEXT_MENAGEDETAILSCEVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.ContreEnquete.MenageDetailsViewModel";
        public static string DATACONTEXT_MENAGECEVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.ContreEnquete.MenageCEViewModel";



        //public static string DATACONTEXT_BATIMENTCEVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.Contre_enquete.BatimentCEViewModel";
        public static string DATACONTEXT_BATIMENTLCEVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.Contre_enquete.BatimentLCViewModel";
        public static string DATACONTEXT_BATIMENT_LOG_IND_VIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.Contre_enquete.BatimentLogIndViewModel";
        public static string DATACONTEXT_BATIMENT_LOG_VIDE_VIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.Contre_enquete.BatimentLogVideViewModel";

        //public static string DATACONTEXT_SDECEVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.Contre_enquete.SdeCEViewModel";
        public static string DATACONTEXT_SDELCEVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.Contre_enquete.SdeLCViewModel";
        //public static string DATACONTEXT_LOGEMENTCEVIEWMODEL = "Ht.Ihsil.Rgph.App.Superviseur.viewModels.Contre_enquete.LogementCEViewModel";

        public static string OBJET_BATIMENT = "Batiment";
        public static string OBJET_LOGEMENT = "Logement";
        public static string OBJET_LOGEMENT_COLLECTIF = "LogementCollectif";
        public static string OBJET_EMIGRE = "Emigre";
        public static string OBJET_MENAGE = "Menage";
        public static string OBJET_INDIVIDU = "Individu";
        public static string OBJET_DECES = "Deces";
        public static string OBJET_EVALUATION = "Evaluation";

        public static string AF1 = "AF1";
        public static string AF2 = "AF2";
        public static string AF3 = "AF3";
        public static string AF4 = "AF4";
        public static string AF5 = "AF5";
        public static string AF6 = "AF6";
        public static string AF7 = "AF7";
        public static string AF8 = "AF8";
        public static string AF9 = "AF9";
        public static string BAT = "BAT";
        public static string CP2 = "CP2";
        public static string EV = "EV";
        public static string LIN = "LIN";
        public static string MEN = "MEN";
        public static string MIGRA = "MIGRA";
        public static string MOURI = "MOURI";
        public static string CP1 = "CP1";
        #endregion

        #region ENUMERATIONS
        public enum TypeQuestionMobile : int
        {
            Choix = 17,
            Saisie = 2,
            Automatique = 3,
            Departement = 4,
            Commune = 7,
            Vqse = 6,
            Pays = 18,
            Grid = 8,
            Utilisation = 9
        }

        public enum TypeContrEnquete : int
        {
            BatimentVide = 1,
            LogementCollectif = 2,
            LogementInvididuelVide = 3,
            LogementIndividuelMenage = 4,
            LogementOccupantAbsent = 5
        }

        public enum StatutContreEnquete : int
        {
            Selectionee = 1,
            Effectuee = 2,
            Non_Effectue = 3
        }
        public enum Contrainte : int
        {
            Numerique = 1,
            Lettrte = 2,
            Alphanumerique = 3
        }

        public enum StatutOccupation : int
        {
            Okipe_Toutan = 1,
            Okipe_Moun_Pa_la = 2,
            Okipe_le_konsa = 3,
            Pa_okipe = 4
        }

        public enum TypeLogement : int
        {
            Kolektif = 0,
            Endividyel = 1
        }
        public enum ImagePath : int
        {
            [StringValue("/images/malrempli.png")]
            MalRempli = 1,
            [StringValue("/images/notfinish.png")]
            PasFini = 2,
            [StringValue("/images/check.png")]
            Fini = 3,
            [StringValue("/images/validate_check.png")]
            Valide = 4,
            [StringValue("/images/ce.png")]
            ContreEnquete = 5
        }

        public enum ToolTipMessage : int
        {
            [StringValue("Pa byen ranpli")]
            MalRempli = 1,
            [StringValue("Poko fini")]
            PasFini = 2,
            [StringValue("Fini")]
            Fini = 3,
            [StringValue("Kont ankèt sa a fèt deja")]
            Kont_anket_fet = 4,
            [StringValue("Valide deja")]
            Valide_deja = 5,
            [StringValue("Kont ankèt sa a poko fèt")]
            Kont_Anket_Not_Made = 6,
            [StringValue("Nan Kont ankèt")]
            Kont_Anket_IN = 7
        }

        public enum TypeTreeView : int
        {
            Contre_enquete = 1,
            Batiment = 2,
            Logement = 3,
            Menage = 4,
            Deces = 5,
            Individu = 6
        }
        #endregion

        #region MODELS
        public static string OBJET_MODEL_LOGEMENTCE = "Ht.Ihsil.Rgph.App.Superviseur.Models.LogementCEModel";
        public static string OBJET_MODEL_BATIMENTCE = "Ht.Ihsil.Rgph.App.Superviseur.Models.BatimentCEModel";
        public static string OBJET_MODEL_MENAGECE = "Ht.Ihsil.Rgph.App.Superviseur.Models.MenageCEModel";
        public static string OBJET_MODEL_INDIVIDUCE = "Ht.Ihsil.Rgph.App.Superviseur.Models.IndividuCEModel";
        public static string OBJET_MODEL_DECESCE = "Ht.Ihsil.Rgph.App.Superviseur.Models.DecesCEModel";

        public static string OBJET_MODEL_LOGEMENT = "Ht.Ihsil.Rgph.App.Superviseur.Models.LogementModel";
        public static string OBJET_MODEL_BATIMENT = "Ht.Ihsil.Rgph.App.Superviseur.Models.BatimentModel";
        public static string OBJET_MODEL_MENAGE = "Ht.Ihsil.Rgph.App.Superviseur.Models.MenageModel";
        public static string OBJET_MODEL_INDIVIDU = "Ht.Ihsil.Rgph.App.Superviseur.Models.IndividuModel";
        public static string OBJET_MODEL_RAPO_FINAL = "Ht.Ihsil.Rgph.App.Superviseur.Models.RapportFinalModel";
        public static string OBJET_MODEL_DECES = "Ht.Ihsil.Rgph.App.Superviseur.Models.DecesModel";
        public static string OBJET_MODEL_EMIGRE = "Ht.Ihsil.Rgph.App.Superviseur.Models.EmigreModel";
        public static string OBJET_MODEL_MENAGE_DETAILS = "Ht.Ihsil.Rgph.App.Superviseur.Models.MenageDetailsModel";
        #endregion

        #region BATIMENT CONTRE-ENQUETE
        public static string BatimentId = "BatimentId";
        public static string SdeId = "SdeId";
        public static string Qhabitation = "Qhabitation";
        public static string Qrec = "Qrec";
        public static string Qrgph = "Qrgph";
        public static string Qadresse = "Qadresse";
        public static string Qlocalite = "Qlocalite";
        public static string Qb1Etat = "Qb1Etat";
        public static string Qb2Type = "Qb2Type";
        public static string Qb3NombreEtage = "Qb3NombreEtage";
        public static string Qb4MateriauMur = "Qb4MateriauMur";
        public static string Qb5MateriauToit = "Qb5MateriauToit";
        public static string Qb6StatutOccupation = "Qb6StatutOccupation";
        public static string Qb7Utilisation1 = "Qb7Utilisation1";
        public static string Qb7Utilisation2 = "Qb7Utilisation2";
        public static string Qb7GrandeUtilisation2 = "Qb7GrandeUtilisation2";
        public static string Qb7GrandeUtilisation1 = "Qb7GrandeUtilisation1";
        public static string Qb8NbreLogeCollectif = "Qb8NbreLogeCollectif";
        public static string Qb8NbreLogeIndividuel = "Qb8NbreLogeIndividuel";
        public static string Statut = "Statut";
        public static string IsValidated = "IsValidated";
        public static string IsSynchroToAppSup = "IsSynchroToAppSup";
        public static string IsSynchroToCentrale = "IsSynchroToCentrale";
        public static string DateDebutCollecte = "DateDebutCollecte";
        public static string DateFinCollecte = "DateFinCollecte";
        public static string DureeSaisie = "DureeSaisie";
        public static string IsFieldAllFilled = "IsFieldAllFilled";
        public static string IsContreEnqueteMade = "IsContreEnqueteMade";
        public static string Latitude = "Latitude";
        public static string Longitude = "Longitude";
        #endregion

        #region LOGEMENT CONTRE-ENQUETE
        public static string LogeId = "LogeId";
        public static string QlCategLogement = "QlCategLogement";
        public static string QlcTypeLogement = "QlcTypeLogement";
        public static string Qllc2bTotalGarcon = "Qllc2bTotalGarcon";
        public static string Qlc2bTotalFille = "Qlc2bTotalFille";
        public static string QlcTotalIndividus = "QlcTotalIndividus";
        public static string Qlin1NumeroOrdre = "Qlin1NumeroOrdre";
        public static string Qlin2StatutOccupation = "Qlin2StatutOccupation";
        public static string Qlin4TypeLogement = "Qlin4TypeLogement";
        public static string Qlin5MateriauSol = "Qlin5MateriauSol";
        public static string Qlin6NombrePieceETChambreACoucher = "Qlin6NombrePiece";
        public static string Qlin7NbreChambreACoucher = "Qlin7NbreChambreACoucher";
        public static string Qlin8NbreIndividuDepense = "Qlin8NbreIndividuDepense";
        public static string Qlin9NbreTotalMenage = "Qlin9NbreTotalMenage";


        #endregion

        #region MENAGE CONTRE-ENQUETE
        public static string MenageId = "MenageId";
        public static string Qm1NoOrdre = "Qm1NoOrdre";
        public static string Qm2ModeJouissance = "Qm2ModeJouissance";
        public static string Qm5SrcEnergieCuisson1 = "Qm5SrcEnergieCuisson1";
        public static string Qm5SrcEnergieCuisson2 = "Qm5SrcEnergieCuisson2";
        public static string Qm8EndroitBesoinPhysiologique = "Qm8EndroitBesoinPhysiologique";
        public static string Qm11TotalIndividuVivant = "Qm11TotalIndividuVivant";


        #endregion

        #region DECES CONTRE-ENQUETE
        public static string DecesId = "DecesId";
        public static string Qd2NoOrdre = "Qd2NoOrdre";
        public static string Qd1Deces = "Qd1Deces";
        public static string Qd1NbreDecedeFille = "Qd1NbreDecedeFille";
        public static string Qd1NbreDecedeGarcon = "Qd1NbreDecedeGarcon";
        #endregion

        #region INDIVIDU CONTRE-ENQUETE
        public static string IndividuId = "IndividuId";
        public static string Qp1NoOrdre = "Qp1NoOrdre";
        public static string Q2Nom = "Q2Nom";
        public static string Q3Prenom = "Q3Prenom";
        public static string Q3LienDeParente = "Q3LienDeParente";
        public static string Q3aRaisonChefMenage = "Q3aRaisonChefMenage";
        public static string Q4Sexe = "Q4Sexe";
        public static string Q5bAge = "Q5bAge";
        public static string Qp7Nationalite = "Qp7Nationalite";
        public static string Qp7PaysNationalite = "Qp7PaysNationalite";
        public static string Qp9EstPlusAge = "Qp9EstPlusAge";
        public static string Qp10LieuNaissance = "Qp10LieuNaissance";
        public static string Qp10CommuneNaissance = "Qp10CommuneNaissance";
        public static string Qp10LieuNaissanceVqse = "Qp10LieuNaissanceVqse";
        public static string Qp10PaysNaissance = "Qp10PaysNaissance";
        public static string Q7DateNaissanceJour = "Q7DateNaissanceJour";
        public static string Q7DateNaissanceMois = "Q7DateNaissanceMois";
        public static string Q7DateNaissanceAnnee = "Q7DateNaissanceAnnee";
        public static string Qp11PeriodeResidence = "Qp11PeriodeResidence";
        public static string Qe2FreqentationScolaireOuUniv = "Qe2FreqentationScolaireOuUniv";
        public static string Qe3typeEtablissement = "Qe3typeEtablissement";
        public static string Qe4aNiveauEtude = "Qe4aNiveauEtude";
        public static string Qe4bDerniereClasseOUAneEtude = "Qe4bDerniereClasseOUAneEtude";

        public static string Qsm1StatutMatrimonial = "Qsm1StatutMatrimonial";
        public static string Qa1ActEconomiqueDerniereSemaine = "Qa1ActEconomiqueDerniereSemaine";
        public static string Qa2ActAvoirDemele1 = "Qa2ActAvoirDemele1";
        public static string Qa2ActDomestique2 = "Qa2ActDomestique2";
        public static string Qa2ActCultivateur3 = "Qa2ActCultivateur3";
        public static string Qa2ActAiderParent4 = "Qa2ActAiderParent4";
        public static string Qa2ActAutre5 = "Qa2ActAutre5";

        public static string Qa8EntreprendreDemarcheTravail = "Qa8EntreprendreDemarcheTravail";
        public static string Qf1aNbreEnfantNeVivantM = "Qf1aNbreEnfantNeVivantM";
        public static string Qf2bNbreEnfantNeVivantF = "Qf2bNbreEnfantNeVivantF";
        public static string Qf2aNbreEnfantVivantM = "Qf2aNbreEnfantVivantM";
        public static string Qf2bNbreEnfantVivantF = "Qf2bNbreEnfantVivantF";
        public static string Qf3aNbreEnfantVivantMenageM = "Qf3aNbreEnfantVivantMenageM";
        public static string Qf3bNbreEnfantVivantMenageF = "Qf3bNbreEnfantVivantMenageF";
        public static string Qf3DernierEnfantJour = "Qf3DernierEnfantJour";
        public static string Qf3DernierEnfantMois = "Qf3DernierEnfantMois";
        public static string Qf3DernierEnfantAnnee = "Qf3DernierEnfantAnnee";
        public static string Qf4DENeVivantVit = "Qf4DENeVivantVit";


        #endregion

        #region EMIGRE
        public static string EmigreId = "EmigreId";
        public static string Qn1numeroOrdre = "Qn1numeroOrdre";
        public static string Qn2aNomComplet = "Qn2aNomComplet";
        public static string Qn2bResidenceActuelle = "Qn2bResidenceActuelle";
        public static string Qn2cSexe = "Qn2cSexe";
        public static string Qn2dAgeAuMomentDepart = "Qn2dAgeAuMomentDepart";
        public static string Qn2eNiveauEtudeAuMomentDepart = "Qn2eNiveauEtudeAuMomentDepart";
        public static string Qn2fDernierPaysResidence = "Qn2fDernierPaysResidence";

        #endregion

        #region CODE MODULE
        public static string MODULE_INDIVIDU = "FRM-IND";
        public static string MODULE_BATIMENT = "FRM-BAT";
        public static string MODULE_LOGEMENT = "FRM-LIN";
        public static string MODULE_MENAGE = "FRM-MEN";
        public static string MODULE_EMIGRE = "FRM-EMI";
        public static string MODULE_DECES = "FRM-DEC";
        #endregion

        #region MESSAGE

        public enum ValidateNotMade : int
        {
            [StringValue("Ou dwe valide batiman a anvan.")]
            Batiment = 1,
            [StringValue("Ou dwe valide lojman sa a anvan.")]
            Logement = 2,
            [StringValue("Ou dwe valide menaj sa a anvan.")]
            Menage = 3,
            [StringValue("Ou dwe valide desè sa a anvan.")]
            Deces = 4,
            [StringValue("Ou dwe valide endividi sa a anvan.")]
            Individu = 5

        }

        public enum Sexe : int
        {
            Gason = 1,
            Fi = 2
        }
        public enum ContreEnqueteNotMade : int
        {
            [StringValue("Ou dwe fè kont ankèt sou batiman  sa a.")]
            Batiment = 1,
            [StringValue("Ou dwe fè kont ankèt sou lojman sa a.")]
            Logement = 2,
            [StringValue("Ou dwe fè kont ankèt sou menaj sa a.")]
            Menage = 3,
            [StringValue("Ou dwe fè kont ankèt sou desè ")]
            Deces = 4,
            [StringValue("Ou dwe fè kont ankèt sou endividi ")]
            Individu = 5
        }

        public enum ValidateConfirm : int
        {
            [StringValue("Batiman sa valide avek siksè.")]
            Batiment = 1,
            [StringValue("Lojman sa valide avek siksè.")]
            Logement = 2,
            [StringValue("Menaj sa valide avek siksè.")]
            Menage = 3,
            [StringValue("Desè sa valide avek siksè.")]
            Deces = 4,
            [StringValue("Endvidi sa valide avek siksè.")]
            Individu = 5

        }

        public static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.ToString());
            StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }
            return output;
        }

        public static string MESSAGE12EXCEPTION = "Fomilè a fini pou moun sa a. Anregistre epi pran yon lot moun.";
        public static string MSG_CHEF_MENAGE = "Verifye laj moun sa a paske laj chef menaj la ak laj pal pi piti ke 13 an.";
        public static string MSG_AGE = "Moun sa a pa gen laj poul fè tout timoun sa yo.";
        public static string MSG_NIVEAU_AGE_ETUDE = "Moun sa a pa ka ap fè klas sa a ak laj sa a.";
        public static string MSG_NIVEAU_ETUDE = "Moun sa a pa ka gen nivo sa oubyen li pa ka ap fè klas sa.";
        public static string MSG_MODULE_KI_MAL_RANPLI = "Fè ajan an retounen pran enfomasyon yo ankò.";
        public static string MSG_TABLET_PAS_CONFIGURE = "Tablèt sa a poko konfigire.";
        public static string MSG_TRANSFERT_TERMINE = "Transfè a fèt avèk siksè.";
        public static string MSG_TABLET_PAS_CONNECTE = "Pa gen tablèt ki konekte.";
        public static string MSG_FICHIER_PAS_COPIE = "Fichye yo pa kopye. Eseye ankò";
        public static string MSG_VALIDATION = "Validasyon an fèt avèk siksè.";
        public static string MSG_NOT_VALIDATE = "Batiman an dwe fini avan ou valide li.";
        public static string MSG_RAPO_SAVE = "Rapò a anregistre avèk siksè.";
        public static string MSG_RAPO_UPDATE = "Rapò a modifye avèk siksè.";
        public static string MSG_NOT_CLICK = "Ou dwe klike sou yon objè.";
        public static string WINDOW_TITLE = "IHSI-RGPH[2018]";
        public static string MSG_KE_IS_MADE = "Kont ankèt sa a fèt deja.";
        #endregion

        #region TYOE CONTRE-ENQEUTE
        public static int CE_TYPE_BATIMENT_VIDE = 1;
        public static int CE_TYPE_BATIMENT_LOG_VIDE = 3;
        public static int CE_TYPE_BATIMENT_LOG_COL = 2;
        public static int CE_TYPE_BATIMENT_LOG_IND = 4;
        #endregion

        #region TRANSFERT CONSTANT
        public static string TOPIC_COLLECT_DATA = "VirtualTopic/Rgph/CD";
        public static string TOPIC_CONTRE_ENQUETE_DATA = "VirtualTopic/Rgph/CED";
        public static string TOPIC_RAPPORT_DATA = "VirtualTopic/Rgph/Rapport";
        public static string TOPIC_RAPPORT_DEROULEMENTCOLLECTE = "VirtualTopic/Rgph/RDC";
        public static string TOPIC_RAPPORT_SUPERVISION_DIRECTE = "VirtualTopic/Rgph/RSD";
        public static string TOPIC_PROBLEME_RENCONTREE = "VirtualTopic/Rgph/PR";
        public static string TOPIC_ALERTDATA = "VirtualTopic/Rgph/Alert";
        #endregion

        #region ANDROID ADB COMMAND
        public static string CMD_IMEI = "/c adb shell dumpsys iphonesubinfo";
        public static string CMD_SERIAL = "/c adb shell getprop ro.boot.serialno";
        public static string CMD_MODEL = "/c adb shell getprop ro.product.model";
        public static string CMD_VERSION = "/c adb shell getprop ro.build.version.release";
        public static string DB_NAME = "rgph_data-db";

        public static string DEVICE_DIRECTORY_DATA = "sdcard/Data/rgph_db/";
        public static string CMD_PULL_DB = "/c adb pull sdcard/Data/rgph_db/";
        public static string CMD_PUSH_DB = "/c adb push";
        public static string CMD_ADB_VERSION = "/c adb version";
        #endregion

        #region STATUT LOJMAN
        public static int LOJMAN_OKIPE_TOUTAN = 1;
        public static int LOJMAN_OKIPE_YON_LE_KONSA = 2;
        public static int LOJMAN_VID = 3;
        public static int TYPE_LOJMAN_ENDIVIDYEL = 1;
        public static int TYPE_LOJMAN_KOLEKTIF = 0;
        #endregion

        #region PROFIL UTILISATEUR
        public static int PROFIL_AGENT_RECENSEUR_MOBILE = 8;
        public static int PROFIL_SUPERVISEUR_SUPERVISION_SG = 8;
        public static int PROFIL_SUPERVISEUR_SUPERVISION_MOBILE = 7;
        public static int PROFIL_ASTIC = 6;
        public static int EST_ACTIF = 1;
        public static int PAS_ACTIF = 0;
        #endregion

        #region RAPPORT PERSONNEL
        public static string q1 = "Est-ce que l´agent recenseur s´est orienté correctement à l´aide de la carte de la SDE ?";
        public static string q2 = "Est-ce que l´AR s´est présenté correctement au ménage ?";
        public static string q3 = "Est-ce que l´AR a correctement identifié le répondant selon les instructions ?";
        public static string q4 = "Est-ce que l´AR a formulé les questions conformément aux instructions ?";
        public static string q5 = "Est-ce que toutes les question clé ont été couvertes pour les caractéristiques de la personne ?";
        public static string q6 = "Est-ce que l´AR a bien géré l´administration du module activités et fonctionnements";
        public static string q7 = "Est-ce que l´AR a bien géré l´administration du module activité économique et en particulier les branches et professions exercées ?";
        public static string q8 = "Est-ce que l´AR a influencé la réponse du répondant ?";
        public static string q9 = "Est-ce que l´AR interprète correctement les informations qui lui sont données ? ";
        public static string q10 = "EstEst-ce que l´AR est en mesure de déceler les incohérences ou d´apprécier le degré de précision des réponses obtenues ?";
        public static string q11 = "Est-ce que l´AR a révisé l´ensemble du questionnaire de manière à éviter toute omission ?";
        public static string q12 = "Est-ce que l´AR a précisé le résultat de la visite à la fin du questionnaire ?";
        public static string q13 = "Faire la liste des problèmes à résoudre avec l´AR. Sélectionner le type de problème, puis en faire la description librement.";
        public static string q14 = "Commentaires généraux sur le comportement de l´agent recenseur (à rédiger)";
        public static string q15 = "Alerte? ";

        #endregion

        #region CHOIX RAPPORT
        public static List<ReponseModel> listOf4Choix()
        {
            List<ReponseModel> listOf = new List<ReponseModel>();
            listOf.Add(new ReponseModel("1", "1.Oui"));
            listOf.Add(new ReponseModel("2", "2.Non"));
            listOf.Add(new ReponseModel("3", "3.Moyennement "));
            listOf.Add(new ReponseModel("4", "4.Hors observation"));
            return listOf;
        }
        public static List<ReponseModel> listOf3Choix()
        {
            List<ReponseModel> listOf = new List<ReponseModel>();
            listOf.Add(new ReponseModel("1", "1.Oui"));
            listOf.Add(new ReponseModel("2", "2.Non"));
            listOf.Add(new ReponseModel("3", "3.Moyennement "));
            return listOf;
        }
        public static List<ReponseModel> listOf8Choix()
        {
            List<ReponseModel> listOf = new List<ReponseModel>();
            listOf.Add(new ReponseModel("1", "1.Une fois"));
            listOf.Add(new ReponseModel("2", "2.Au moins 2 fois"));
            listOf.Add(new ReponseModel("3", "3.Non "));
            return listOf;
        }
        public static List<ReponseModel> listOfChoixQ13()
        {
            List<ReponseModel> listOf = new List<ReponseModel>();
            listOf.Add(new ReponseModel("1", "1.Non maîtrise de certains concepts clé"));
            listOf.Add(new ReponseModel("2", "2.Du mal à gérer l´interview"));
            listOf.Add(new ReponseModel("3", "3.Attitude peu respectueuse vis-à-vis du répondant"));
            listOf.Add(new ReponseModel("4", "4.Ne prend pas le temps de bien gérer l´entrevue"));
            return listOf;
        }
        public static List<ReponseModel> listOfChoixQ15()
        {
            List<ReponseModel> listOf = new List<ReponseModel>();
            listOf.Add(new ReponseModel("1", "1. Oui, Agent problématique "));
            listOf.Add(new ReponseModel("2", "2. Agent à suivre pour renforcement"));
            listOf.Add(new ReponseModel("3", "3. Non, aucune alerte"));
            return listOf;
        }
        #endregion

        #region CHOIX CONTRE ENQUETE

        public static List<ReponseModel> ChoixStatutContreEnquete()
        {
            List<ReponseModel> list = new List<ReponseModel>();
            list.Add(new ReponseModel("1", "1.Terminée"));
            list.Add(new ReponseModel("2", "2.Non Terminée"));
            list.Add(new ReponseModel("3", "3.Validée"));
            return list;
        }

        public static List<ReponseModel> ChoixModelTirage()
        {
            List<ReponseModel> list = new List<ReponseModel>();
            list.Add(new ReponseModel("1", "1.Aléatoire"));
            list.Add(new ReponseModel("2", "2.Non Aléatoire"));
            return list;
        }
        public static List<ReponseModel> ChoixRaisonContreEnquete()
        {
            List<ReponseModel> list = new List<ReponseModel>();
            list.Add(new ReponseModel("1", "1. Pas le temps "));
            list.Add(new ReponseModel("2", "2. Ménage absent"));
            list.Add(new ReponseModel("3", "Répondant ou chef de ménage absent"));
            return list;
        }


        #endregion

        #region RAPPORT DEROULEMENT COLLECTE


        public static List<SousDomaineProbleme> ListOfDomaineProbleme()
        {
            List<SousDomaineProbleme> listOf = new List<SousDomaineProbleme>();
            //listOf.Add(new SousDomaineProbleme(SousDomaineTerrain1, Prob_TerrainCart1_0));
            //listOf.Add(new SousDomaineProbleme(SousDomaineTerrain1, Prob_TerrainCart1_1));
            //listOf.Add(new SousDomaineProbleme(SousDomaineTerrain1, Prob_TerrainCart1_2));
            //listOf.Add(new SousDomaineProbleme(SousDomaineTerrain1, Prob_TerrainCart1_3));
            //listOf.Add(new SousDomaineProbleme(SousDomaineTerrain1, Prob_TerrainCart1_4));
            //listOf.Add(new SousDomaineProbleme(SousDomaineTerrain1, Prob_TerrainCart1_5));
            //listOf.Add(new SousDomaineProbleme(SousDomaineTerrain1, Prob_TerrainCart1_6));

            //listOf.Add(new SousDomaineProbleme(SousDomaineTerrain2, Prob_TerrainCart2_1));
            //listOf.Add(new SousDomaineProbleme(SousDomaineTerrain2, Prob_TerrainCart2_2));
            //listOf.Add(new SousDomaineProbleme(SousDomaineTerrain2, Prob_TerrainCart2_3));
            //listOf.Add(new SousDomaineProbleme(SousDomaineTerrain2, Prob_TerrainCart2_4));
            return listOf;
        }
        public static ObservableCollection<DetailsRapportDeroulement> RapportsDeroulementCollecte()
        {
            ObservableCollection<DetailsRapportDeroulement> rapports = new ObservableCollection<DetailsRapportDeroulement>();

            #region cartographie 1
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_0, Sol_TerrainCart1_1, "", "", Suivi_TerrainCart1_1, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_0, Sol_TerrainCart1_2, "", "", Suivi_TerrainCart1_2, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_0, Sol_TerrainCart1_3, "", "", Suivi_TerrainCart1_3, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_0, Sol_TerrainCart1_4, "", "", Suivi_TerrainCart1_4, ""));

            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_1, Sol_TerrainCart1_1, "", "", Suivi_TerrainCart1_1, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_1, Sol_TerrainCart1_2, "", "", Suivi_TerrainCart1_2, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_1, Sol_TerrainCart1_3, "", "", Suivi_TerrainCart1_3, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_1, Sol_TerrainCart1_4, "", "", Suivi_TerrainCart1_4, ""));

            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_2, Sol_TerrainCart1_1, "", "", Suivi_TerrainCart1_1, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_2, Sol_TerrainCart1_2, "", "", Suivi_TerrainCart1_2, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_2, Sol_TerrainCart1_3, "", "", Suivi_TerrainCart1_3, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_2, Sol_TerrainCart1_4, "", "", Suivi_TerrainCart1_4, ""));

            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_3, Sol_TerrainCart1_1, "", "", Suivi_TerrainCart1_1, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_3, Sol_TerrainCart1_2, "", "", Suivi_TerrainCart1_2, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_3, Sol_TerrainCart1_3, "", "", Suivi_TerrainCart1_3, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_3, Sol_TerrainCart1_4, "", "", Suivi_TerrainCart1_4, ""));

            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_4, Sol_TerrainCart1_1, "", "", Suivi_TerrainCart1_1, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_4, Sol_TerrainCart1_2, "", "", Suivi_TerrainCart1_2, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_4, Sol_TerrainCart1_3, "", "", Suivi_TerrainCart1_3, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_4, Sol_TerrainCart1_4, "", "", Suivi_TerrainCart1_4, ""));

            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_5, Sol_TerrainCart1_1, "", "", Suivi_TerrainCart1_1, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_5, Sol_TerrainCart1_2, "", "", Suivi_TerrainCart1_2, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_5, Sol_TerrainCart1_3, "", "", Suivi_TerrainCart1_3, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_5, Sol_TerrainCart1_4, "", "", Suivi_TerrainCart1_4, ""));

            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_1, Sol_TerrainCart1_1, "", "", Suivi_TerrainCart1_1, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_1, Sol_TerrainCart1_2, "", "", Suivi_TerrainCart1_2, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_1, Sol_TerrainCart1_3, "", "", Suivi_TerrainCart1_3, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain1, Prob_TerrainCart1_1, Sol_TerrainCart1_4, "", "", Suivi_TerrainCart1_4, ""));

            #endregion

            #region cartographie 2
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain2, Prob_TerrainCart2_1, Sol_TerrainCart2_1, "", "", Suivi_TerrainCart1_1, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain2, Prob_TerrainCart2_1, Sol_TerrainCart2_2, "", "", Suivi_TerrainCart1_2, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain2, Prob_TerrainCart2_1, Sol_TerrainCart2_3, "", "", Suivi_TerrainCart1_3, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain2, Prob_TerrainCart2_1, Sol_TerrainCart2_4, "", "", Suivi_TerrainCart1_4, ""));

            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain2, Prob_TerrainCart2_2, Sol_TerrainCart2_1, "", "", Suivi_TerrainCart1_1, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain2, Prob_TerrainCart2_2, Sol_TerrainCart2_2, "", "", Suivi_TerrainCart1_2, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain2, Prob_TerrainCart2_2, Sol_TerrainCart2_3, "", "", Suivi_TerrainCart1_3, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain2, Prob_TerrainCart2_2, Sol_TerrainCart2_4, "", "", Suivi_TerrainCart1_4, ""));

            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain2, Prob_TerrainCart2_3, Sol_TerrainCart2_1, "", "", Suivi_TerrainCart1_1, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain2, Prob_TerrainCart2_3, Sol_TerrainCart2_2, "", "", Suivi_TerrainCart1_2, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain2, Prob_TerrainCart2_3, Sol_TerrainCart2_3, "", "", Suivi_TerrainCart1_3, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain2, Prob_TerrainCart2_3, Sol_TerrainCart2_4, "", "", Suivi_TerrainCart1_4, ""));

            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain2, Prob_TerrainCart2_4, Sol_TerrainCart2_1, "", "", Suivi_TerrainCart1_1, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain2, Prob_TerrainCart2_4, Sol_TerrainCart2_2, "", "", Suivi_TerrainCart1_2, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain2, Prob_TerrainCart2_4, Sol_TerrainCart2_3, "", "", Suivi_TerrainCart1_3, ""));
            //rapports.Add(new DetailsRapportDeroulement("Terrain", SousDomaineTerrain2, Prob_TerrainCart2_4, Sol_TerrainCart2_4, "", "", Suivi_TerrainCart1_4, ""));
            #endregion

            return rapports;
        }

        #endregion

        #region PROBLEME ET SOLUTION TERRAIN

        #region CARTOGRAPHIE 1
        public static KeyValue Prob_TerrainCart1_0 = new KeyValue(0, "0. Aucun problème ");
        public static KeyValue Prob_TerrainCart1_1 = new KeyValue(1, "1. Carte incomplète  ");
        public static KeyValue Prob_TerrainCart1_2 = new KeyValue(2, "2. Délimitation erronée");
        public static KeyValue Prob_TerrainCart1_3 = new KeyValue(3, "3. Aires avec habitat supplémentaire");
        public static KeyValue Prob_TerrainCart1_4 = new KeyValue(4, "4. Aires avec habitat détruit/inoccupé ");
        public static KeyValue Prob_TerrainCart1_5 = new KeyValue(5, "5. Indications erronées (hors délimitation)");
        public static KeyValue Prob_TerrainCart1_6 = new KeyValue(6, "6. Autre ");

        public static KeyValue Sol_TerrainCart1_1 = new KeyValue(1, "1. Requête d´une carte pertinente");
        public static KeyValue Sol_TerrainCart1_2 = new KeyValue(2, "2. Recherche d´informations complémentaires sur le terrain");
        public static KeyValue Sol_TerrainCart1_3 = new KeyValue(3, "3. Recours au responsable communal");
        public static KeyValue Sol_TerrainCart1_4 = new KeyValue(4, "4. Autre");

        public static KeyValue Suivi_TerrainCart1_1 = new KeyValue(1, "1. Superviseur de district");
        public static KeyValue Suivi_TerrainCart1_2 = new KeyValue(2, "2. Responsable communal");
        public static KeyValue Suivi_TerrainCart1_3 = new KeyValue(3, "3. Responsable départemental");
        public static KeyValue Suivi_TerrainCart1_4 = new KeyValue(4, "4. Bureau central");
        #endregion

        # region CARTOGRAPHIE 2
        public static KeyValue Prob_TerrainCart2_1 = new KeyValue(1, "1. Omission constatée ");
        public static KeyValue Prob_TerrainCart2_2 = new KeyValue(2, "2. Doublon possible");
        public static KeyValue Prob_TerrainCart2_3 = new KeyValue(3, "3. Doublon constaté");
        public static KeyValue Prob_TerrainCart2_4 = new KeyValue(4, "4. Autre");

        public static KeyValue Sol_TerrainCart2_1 = new KeyValue(1, "1. Mobilisation AR sur l´aire omise et contre-enquête");
        public static KeyValue Sol_TerrainCart2_2 = new KeyValue(2, "2. Vérification avec le responsable communal et les autres superviseurs / Doublon possible");
        public static KeyValue Sol_TerrainCart2_3 = new KeyValue(3, "3. Alerte au Responsable communal pour signalement doublon");
        public static KeyValue Sol_TerrainCart2_4 = new KeyValue(4, "4. Autre");
        #endregion

        #endregion

        #region DOMAINE ET SOUS-DOMAINE
        public static KeyValue SousDomaineTerrain1 = new KeyValue(1, "CARTOGRAPHIE 1 - Reconnaissance des lieux pré-collecte");
        public static KeyValue SousDomaineTerrain2 = new KeyValue(2, "CARTOGRAPHIE  2 - Couverture collecte");
        public static KeyValue SousDomaineTerrain3 = new KeyValue(3, "GESTION TERRAIN");

        public static KeyValue Domaine1 = new KeyValue(1, "TERRAIN");
        public static KeyValue Domaine2 = new KeyValue(2, "RÉALISATION ENTREVUES");
        public static KeyValue Domaine3 = new KeyValue(3, "GESTION DU PERSONNEL");
        public static KeyValue Domaine4 = new KeyValue(4, "TERRAIN");
        public static KeyValue Domaine5 = new KeyValue(5, "GESTION DES ÉQUIPEMENTS ET ACCESSOIRES");
        #endregion

        public static int CHEF_MENAGE = 1;
        public static int MASCULIN = 2;
        public static int FEMININ = 1;
        public static string SUPDATABASE_FILE_NAME = "rgph_sup-db.sqlite";
        public static string XML_ELEMENT_ADR_SERVER = "adrServer";
        public static string XML_ELEMENT_VARIABLE = "variable";
        public static string PA_KONNEN_KOMIN_OU_PEYI = "9999";
        public static string PA_KONNEN_SEKSYON_KOMINAL = "9999-99";
        public static int TRANSFERT_MOBILE = 1;
        public static int TRANSFERT_PC = 2;
        public static int AGE_PA_KONNEN = 999;

        #region OTHERS STUFF
        public enum RapportTypeQuestion : int
        {
            question_4 = 1,
            question_8 = 2
        }

        public static List<KeyValue> ListOfRaisons()
        {
            List<KeyValue> listOf = new List<KeyValue>();
            listOf.Add(new KeyValue(1, "Ranpli nèt / depi nan batiman rive nan manb menaj la"));
            listOf.Add(new KeyValue(2, "Ranpli nèt / Batiman vid"));
            listOf.Add(new KeyValue(3, "Ranpli nèt / Lojman vid"));
            listOf.Add(new KeyValue(4, "Ranpli nèt depi nan premye entèvyou a"));
            listOf.Add(new KeyValue(5, "Refus converti"));
            listOf.Add(new KeyValue(6, "Randevou ou retou pwograme/fèt/fini"));
            //Menage
            listOf.Add(new KeyValue(7, "Moun ki tap reponn nan pa vle kontinye"));
            listOf.Add(new KeyValue(8, "Moun ki tap reponn nan kanpe, men gen randevou"));
            listOf.Add(new KeyValue(9, "Lojman an okipe, men moun yo pa la"));
            listOf.Add(new KeyValue(10, "Lòt. Presize poukisa..."));
            //Menage
            listOf.Add(new KeyValue(11, "\"Refus non converti\" / Kesyonè a pa fini (NRP)"));
            listOf.Add(new KeyValue(12, "Moun ki pou reponn nan pa janm la/disponib pou AR la fin ranpli kesyonè a"));
            listOf.Add(new KeyValue(13, "Lojman an okipe, men moun yo pa la"));
            listOf.Add(new KeyValue(14, "Lòt. Presize poukisa..."));
            listOf.Add(new KeyValue(15, "Pa gen mwayen obsève batiman an / pa gen enfòmasyon sou lojman an"));
            listOf.Add(new KeyValue(16, "Moun yo refize reponn nètalkole"));
            listOf.Add(new KeyValue(17, "Moun ki pou reponn nan pa la/ pa disponib men gen yon randevou"));
            listOf.Add(new KeyValue(18, "Moun ki pou reponn nan pa la oubyen li pa disponib"));
            listOf.Add(new KeyValue(19, "Lòt. Presize poukisa..."));
            listOf.Add(new KeyValue(20, "Pa janm gen mwayen obsève batiman an"));
            listOf.Add(new KeyValue(21, "\"Refus non converti\" / Non-réponse totale (NRT)"));
            listOf.Add(new KeyValue(22, "Moun ki pou reponn nan pa janm la/disponib pou AR la ranpli kesyonè a"));
            listOf.Add(new KeyValue(23, "Lòt. Presize poukisa..."));
            return listOf;
        }
        public static KeyValue getRaison(int raisonId)
        {
            return ListOfRaisons().Find(r => r.Key == raisonId);
        }
        public static List<NameValue> ListOfSections()
        {
            List<NameValue> listOf = new List<NameValue>();
            listOf.Add(new NameValue("BAT", "KARAKTERISTIK KAY LA", "Batiment"));
            listOf.Add(new NameValue("LCOL", "KARAKTERISTIK LOJMAN KOLEKTIF", "Logement Collectif"));
            listOf.Add(new NameValue("LIN", "KARAKTERISTIK LOJMAN ENDIVIDYÈL", "Logement Individuel"));
            listOf.Add(new NameValue("MEN", "MENAGE", "Menage"));
            listOf.Add(new NameValue("MIGRA", "MIGRASYON", "Menage"));
            listOf.Add(new NameValue("MOURI", "MOUN KI MOURI NAN MENAJ LA", "Menage"));
            listOf.Add(new NameValue("KMN", "KARAKTERISTIK DEMOGRAFIK", "Individu"));
            listOf.Add(new NameValue("KMNEDIK_1", "EDIKASYON", "Individu"));
            listOf.Add(new NameValue("KMNWE_2", "AKTIVITE AK FONKSYÒNMAN MOUN NAN", "Individu"));
            listOf.Add(new NameValue("KMNPHON_8", "POSESYON TELEFON SELILE", "Individu"));
            listOf.Add(new NameValue("KMNENTEN_9", "ITILIZASYON  ENTENET AK AKSE ", "Individu"));
            listOf.Add(new NameValue("KMNMIGRA_10", "MIGRASYON : RETOUNEN VIN VIV AN AYITI", "Individu"));
            listOf.Add(new NameValue("KMNESTATI_1", "ESTATI MATRIMONYAL", "Individu"));
            listOf.Add(new NameValue("AKTEKONO", "AKTIVITE EKONOMIK", "Individu"));
            listOf.Add(new NameValue("KMFEKOND", "FEGONDITE", "Individu"));
            return listOf;

        }
        public static NameValue getSection(string sectionId)
        {
            return ListOfSections().Find(s => s.Name == sectionId);
        }
        public static List<NameValue> searchSectionByObjet(string objet)
        {
            return ListOfSections().FindAll(s => s.Objet == objet);
        }
        public static List<NameValue> ListOfDifficultes()
        {
            List<NameValue> listOf = new List<NameValue>();
            listOf.Add(new NameValue("B3.17", "B3.17. Fournir des indications sur tout feedback reçu concernant les difficultés observées dans la réponse aux questions individuelles."));
            listOf.Add(new NameValue("B3.18", "B3.18. Existe-t-il des domaines sensibles pour lesquels les réponses pourraient être peu ou non fiables ?"));
            return listOf;
        }

        public static string REPONDAN_PRENSIPAL = "Repondan Prensipal";
        public static string ED_REPONDAN_PRENSIPAL = "Ed Repondan Prensipal la";
        public static string EDE_AKTIVITE_EKONOMIK_1 = "Eske gen moun ki ede l?";
        public static string EDE_AKTIVITE_EKONOMIK_2 = "Si wi, eske moun sa a viv nan menaj la?";
        public static string EDE_AKTIVITE_EKONOMIK_3 = "Si wi, kiyès ki ede repondan an?";

        public static string EDE_AKTIVITE_FEKONDITE_1 = "Eske gen moun ki ede l?";
        public static string EDE_AKTIVITE_FEKONDITE_2 = "Si wi, eske moun sa a viv nan menaj la?";
        public static string EDE_AKTIVITE_FEKONDITE_3 = "Si wi, kiyès ki ede repondan an?";

        public static List<KeyValue> REPONS_EDE_AKTIVITE_EKONOMIK_1()
        {
            List<KeyValue> repons = new List<KeyValue>();
            repons.Add(new KeyValue(1, "Wi"));
            repons.Add(new KeyValue(2, "Non"));
            return repons;
        }
        public static KeyValue getKeyValue(int key)
        {
            foreach (KeyValue k in REPONS_EDE_AKTIVITE_EKONOMIK_1())
            {
                if (k.Key == key)
                    return k;
                //break;
            }
            return new KeyValue();
        }
        #endregion

        public static List<string> QuestionsReponsesLogements()
        {
            List<string> q = new List<string>();
            
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('LIN2', 'LIN2-1',0,0 , '', '', 'LIN4');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('LIN2', 'LIN2-2',0,0 , '', '', 'FIN');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('LIN2', 'LIN2-3',0,0 , '', '', 'FIN');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('LIN2', 'LIN2-4',0,0 , '', '', 'FIN');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('LIN4', 'LIN4-1',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('LIN4', 'LIN4-2',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('LIN4', 'LIN4-3',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('LIN5', 'LIN5-1',0,0 , '', '', 'LIN6');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('LIN5', 'LIN5-2',0,0 , '', '', 'LIN6');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('LIN5', 'LIN5-3',0,0 , '', '', 'LIN6');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('LIN5', 'LIN5-4',0,0 , '', '', 'LIN6');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('LIN5', 'LIN5-5',0,0 , '', '', 'LIN6');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('LIN5', 'LIN5-6',0,0 , '', '', 'LIN6');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('LIN8', 'LIN8-1',0,0 , '', '', 'LIN9');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('LIN8', 'LIN8-2',0,0 , '', '', 'FIN');");
            return q;
        }
        public static List<string> Reponses()
        {
            List<string> r = new List<string>();
            
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('LIN2-1', '1', '1-Okipe toutan epi moun yo la');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('LIN2-2', '2', '2-Okipe toutan, men moun yo pa la');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('LIN2-3', '3', '3-Vid yon lè konsa');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('LIN2-4', '4', '4-Toujou vid');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('LIN4-1', '1', '1-Apatman');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('LIN4-2', '2', '2-Pyès (pyès kay)');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('LIN4-3', '3', '3-Lòt');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('LIN5-1', '1', '1-Tè');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('LIN5-2', '2', '2-Bwa/Planch/Playwoud');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('LIN5-3', '3', '3-Siman');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('LIN5-4', '4', '4-Mozayik');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('LIN5-5', '5', '5-Seramik/Terazo/Mab');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('LIN5-6', '6', '6-Lòt materyo');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('LIN8-1', '1', '1-Wi');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('LIN8-2', '2', '2-Non');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('M5_1-1', '1', '1-Bwa / Pay');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('M5_1-2', '2', '2-Gaz (Bonbòn)');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('M5_1-3', '3', '3-Elektrisite (Kouran)');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('M5_1-4', '4', '4-Chabon bwa');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('M5_1-5', '5', '5-Chabon Brikèt');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('M5_1-6', '6', '6-Kewozè n');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('M5_1-7', '7', '7-Enèji solèy');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('M5_1-8', '8', '8-Lòt');");
            r.Add("INSERT INTO Tbl_Reponses (CodeUniqueReponse,CodeReponse , LibelleReponse) VALUES ('M5_1-9', '9', '9-Pa konsène');");

            return r;
        }
        public static List<string> Questions()
        {
            List<string> q = new List<string>();
            q.Add("INSERT INTO Tbl_Questions (CodeQuestion, NomObjet,Libelle, DetailsQuestion, CodeCategorie,NomChamps , TypeQuestion, ContrainteQuestion,ValeurMaxParChiffre,NbreCaratereMaximal, EstSautReponse, Qprecedent, Qsuivant  ) VALUES ('LIN2','Logement', 'LIN2. - ESKE LOJMAN SA A…?     ', '','2', 'Qlin2StatutOccupation' , 17, 0,-1,-1, 1, 'LIN1', 'LIN4');");
            q.Add("INSERT INTO Tbl_Questions (CodeQuestion, NomObjet,Libelle, DetailsQuestion, CodeCategorie,NomChamps , TypeQuestion, ContrainteQuestion,ValeurMaxParChiffre,NbreCaratereMaximal, EstSautReponse, Qprecedent, Qsuivant  ) VALUES ('LIN4','Logement', 'LIN4. – ESKE LOJMAN SA A SE YON ? ', '','2', 'Qlin4TypeLogement' , 17, 0,-1,-1, 0, 'LIN2', 'LIN5');");
            q.Add("INSERT INTO Tbl_Questions (CodeQuestion, NomObjet,Libelle, DetailsQuestion, CodeCategorie,NomChamps , TypeQuestion, ContrainteQuestion,ValeurMaxParChiffre,NbreCaratereMaximal, EstSautReponse, Qprecedent, Qsuivant  ) VALUES ('LIN5','Logement', 'LIN5.- AK KI MATERYO ATÈ LOJMAN AN PI PLIS FÈT ? ', '','2', 'Qlin5MateriauSol' , 17, 0,-1,-1, 0, 'LIN4', 'LIN6');");
            q.Add("INSERT INTO Tbl_Questions (CodeQuestion, NomObjet,Libelle, DetailsQuestion, CodeCategorie,NomChamps , TypeQuestion, ContrainteQuestion,ValeurMaxParChiffre,NbreCaratereMaximal, EstSautReponse, Qprecedent, Qsuivant  ) VALUES ('LIN6','Logement', 'LIN6.- KONBYEN PYÈS LOJMAN SA A GENYEN(Watè, koulwa ak galeri pa ladann)?', '','2', 'Qlin6NombrePiece' , 2, 1,-1,-1, 0, 'LIN5', 'LIN7');");
            q.Add("INSERT INTO Tbl_Questions (CodeQuestion, NomObjet,Libelle, DetailsQuestion, CodeCategorie,NomChamps , TypeQuestion, ContrainteQuestion,ValeurMaxParChiffre,NbreCaratereMaximal, EstSautReponse, Qprecedent, Qsuivant  ) VALUES ('LIN7','Logement', 'LIN7.- NAN PYES SA YO KONBYEN KI SE CHANM POU DOMI SELMAN ?', '','2', 'Qlin7NbreChambreACoucher' , 2, 1,-1,-1, 0, 'LIN6', 'LIN8');");
            q.Add("INSERT INTO Tbl_Questions (CodeQuestion, NomObjet,Libelle, DetailsQuestion, CodeCategorie,NomChamps , TypeQuestion, ContrainteQuestion,ValeurMaxParChiffre,NbreCaratereMaximal, EstSautReponse, Qprecedent, Qsuivant  ) VALUES ('LIN8','Logement', 'LIN8.- NAN TOUT MOUN KI TOUJOU AP VIV NAN LOJMAN SA A, ÈSKE GEN YOUN NAN YO OSWA YON GWOUP NAN YO KI METE TET YO ANSANM POU FÈ DEPANS APA POU MANJE YO, POU YO BWÈ EPI POU LÒT BAGAY KI KONSÈNE YO ?  ', '','2', 'Qlin8NbreIndividuDepense' , 2, 0,-1,-1, 1, 'LIN7', 'LIN9');");
            q.Add("INSERT INTO Tbl_Questions (CodeQuestion, NomObjet,Libelle, DetailsQuestion, CodeCategorie,NomChamps , TypeQuestion, ContrainteQuestion,ValeurMaxParChiffre,NbreCaratereMaximal, EstSautReponse, Qprecedent, Qsuivant  ) VALUES ('LIN9','Logement', 'LIN9.- KONBYEN MENAJ* ANTOU KAP VIV NAN LOJMAN SA A Si W METE MENAJ PA W LA LADAN N ?', '','2', 'Qlin9NbreTotalMenage' , 2, 1,-1,-1, 0, 'LIN8', 'FIN');");
            q.Add("INSERT INTO Tbl_Questions (CodeQuestion, NomObjet,Libelle, DetailsQuestion, CodeCategorie,NomChamps , TypeQuestion, ContrainteQuestion,ValeurMaxParChiffre,NbreCaratereMaximal, EstSautReponse, Qprecedent, Qsuivant  ) VALUES ('M2','Menage', 'M2.- AK KI TIT MENAJ LA RETE NAN LOJMAN AN?', '','3', 'Qm2ModeJouissance' , 17, 0,-1,-1, 0, 'M2', 'M5');");
            q.Add("INSERT INTO Tbl_Questions (CodeQuestion, NomObjet,Libelle, DetailsQuestion, CodeCategorie,NomChamps , TypeQuestion, ContrainteQuestion,ValeurMaxParChiffre,NbreCaratereMaximal, EstSautReponse, Qprecedent, Qsuivant  ) VALUES ('M5','Menage', 'M5.- AK KI DE (2) PRINSIPAL SOUS ENERJI MENAJ LA PLIS SÈVI POU KWIT MANJE ? (Komanse ak sa ki pi enpotan an)?', '','3', 'Qm5SrcEnergieCuisson1' , 17, 0,-1,-1, 0, 'M2', 'M5_1');");
            q.Add("INSERT INTO Tbl_Questions (CodeQuestion, NomObjet,Libelle, DetailsQuestion, CodeCategorie,NomChamps , TypeQuestion, ContrainteQuestion,ValeurMaxParChiffre,NbreCaratereMaximal, EstSautReponse, Qprecedent, Qsuivant  ) VALUES ('M5_1','Menage', 'M5-BAY DEZYEM PRINSIPAL SOUS ENERJI MENAJ LA PLIS SÈVI POU KWIT MANJE?', '','3', 'Qm5SrcEnergieCuisson2' , 17, 0,-1,-1, 0, 'M5', 'M8');");
            q.Add("INSERT INTO Tbl_Questions (CodeQuestion, NomObjet,Libelle, DetailsQuestion, CodeCategorie,NomChamps , TypeQuestion, ContrainteQuestion,ValeurMaxParChiffre,NbreCaratereMaximal, EstSautReponse, Qprecedent, Qsuivant  ) VALUES ('M8','Menage', 'M8. - KI KOTE MOUN KI NAN MENAJ LA FÈ BEZWEN YO ?', '','3', 'Qm8EndroitBesoinPhysiologique' , 17, 0,-1,-1, 0, 'M5.1', 'M11');");
            q.Add("INSERT INTO Tbl_Questions (CodeQuestion, NomObjet,Libelle, DetailsQuestion, CodeCategorie,NomChamps , TypeQuestion, ContrainteQuestion,ValeurMaxParChiffre,NbreCaratereMaximal, EstSautReponse, Qprecedent, Qsuivant  ) VALUES ('M11','Menage', 'M11.- ANTOU, KONBYEN MOUN KI TOUJOU AP VIV  NAN MENAJ (LOJMAN KOLEKTIF) SA A ?', '','3', 'Qm11TotalIndividuVivant' , 2, 1,-1,-1, 0, 'M8', 'M12');");
            q.Add("INSERT INTO Tbl_Questions (CodeQuestion, NomObjet,Libelle, DetailsQuestion, CodeCategorie,NomChamps , TypeQuestion, ContrainteQuestion,ValeurMaxParChiffre,NbreCaratereMaximal, EstSautReponse, Qprecedent, Qsuivant  ) VALUES ('M12','Menage', 'M12.- BAN M NON, SEKS, DAT NESANS AK LAJ TOUT MOUN SA YO  ? (Pa bliye banm ti bebe yo, timoun piti yo ak sak pi granmoun yo tou).', '','3', '' , 8, 0,-1,-1, 0, '', 'FIN');");
            return q;
        }

        public static List<string> QuestionsToDelete()
        {
            List<string> q = new List<string>();
            q.Add("LIN4");
            q.Add("LIN5");
            q.Add("LIN6");
            q.Add("LIN7");
            q.Add("LIN8");
            q.Add("LIN9");
            return q;
        }
        public static List<string> DeleteQuestions()
        {
            List<string> Q = new List<string>();
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='LIN2';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='LIN4';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='LIN5';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='LIN6';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='LIN7';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='LIN8';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='LIN9';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='M3';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='M2';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='M4';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='M4.1';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='M4.2';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='M5';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='M5.1';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='M5_1';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='M5.2';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='M6';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='M8';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='M11';");
            Q.Add("DELETE FROM Tbl_Questions WHERE CodeQuestion='M12';");
            return Q;
        }
        public static List<string> DeleteQuestionsReponses()
        {
            List<string> Q = new List<string>();
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='LIN2';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='LIN4';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='LIN5';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='LIN6';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='LIN7';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='LIN8';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='LIN9';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='M3';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='M2';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='M4';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='M4.1';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='M4.2';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='M5';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='M5.1';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='M5_1';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='M5.2';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='M6';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='M8';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='M11';");
            Q.Add("DELETE FROM Tbl_Questions_Reponses WHERE CodeQuestion='M12';");
            return Q;
        }

        public static List<string> DeleteReponses()
        {
            List<string> r = new List<string>();
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='LIN2-1'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='LIN2-2'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='LIN2-3'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='LIN2-4'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='LIN4-1'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='LIN4-2'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='LIN4-3'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='LIN5-1'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='LIN5-2'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='LIN5-3'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='LIN5-4'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='LIN5-5'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='LIN5-6'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='LIN8-1'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='LIN8-2'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5.1-1'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5.1-2'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5.1-3'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5.1-4'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5.1-5'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5.1-6'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5.1-7'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5.1-8'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5.1-9'");

            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5_1-1'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5_1-2'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5_1-3'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5_1-4'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5_1-5'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5_1-6'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5_1-7'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5_1-8'");
            r.Add("DELETE FROM Tbl_Reponses WHERE CodeUniqueReponse='M5_1-9'");
            return r;
        }

        public static List<string> QuestionsReponsesMenages()
        {
            List<string> q = new List<string>();
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M2', 'M2-1',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M2', 'M2-2',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M2', 'M2-3',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M2', 'M2-4',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M2', 'M2-5',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M2', 'M2-6',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M2', 'M2-7',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5', 'M5-1',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5', 'M5-2',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5', 'M5-3',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5', 'M5-4',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5', 'M5-5',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5', 'M5-6',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5', 'M5-7',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5', 'M5-8',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5', 'M5-9',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5_1', 'M5_1-1',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5_1', 'M5_1-2',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5_1', 'M5_1-3',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5_1', 'M5_1-4',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5_1', 'M5_1-5',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5_1', 'M5_1-6',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5_1', 'M5_1-7',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5_1', 'M5_1-8',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M5_1', 'M5_1-9',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M8', 'M8-1',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M8', 'M8-2',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M8', 'M8-3',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M8', 'M8-4',0,0 , '', '', '');");
            q.Add("INSERT INTO Tbl_Questions_Reponses (CodeQuestion, CodeUniqueReponse,EstEnfant,AvoirEnfant , CodeParent, Qprecedent, Qsuivant) VALUES ('M8', 'M8-5',0,0 , '', '', '');");
            return q;
        }



    }

}
