using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.SchemaTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ht.Ihsil.Rgph.App.Superviseur.Json;

namespace Ht.Ihsil.Rgph.App.Superviseur.utils
{
    public interface ISqliteReader
    {
        #region BATIMENTS
        BatimentType ReadBatimentType(long batimentId);
        List<BatimentType> GetAllBatimentType();
        List<BatimentType> GetAllBatimentMalRempli();
        List<BatimentType> GetAllBatimentPasFini();
        List<BatimentModel> GetAllBatimentNotFinished();
        List<BatimentType> GetAllBatimentTermine();
        List<BatimentModel> GetAllBatimentModelTermine();
        List<BatimentModel> GetAllBatimentModelValide();
        List<BatimentModel> GetAllBatimentModel();
        BatimentModel GetBatimentbyId(long batimentId);
        List<BatimentModel> GetAllBatimentWithLogVide();
        List<BatimentModel> GetAllBatimentWithLogInd();
        List<BatimentModel> GetAllBatimentFinishedWithLogInd();
        List<BatimentModel> GetABatimentWithLogC();
        List<BatimentModel> GetAllBatimentVide();
        List<BatimentModel> GetBatLogMenWithDeces();
        List<BatimentJson> GetAllBatimentsInJson();
        Ht.Ihsil.Rgph.App.Superviseur.Models.BatimentDataMobile GetAllBatiments();
        //Retourne les batiments inobservables(modalite =5)
        List<BatimentModel> GetAllBatimentsInobservables();
        //Retourne les batiments ayant des objets vides (Pas du tout rempli)
        List<BatimentModel> GetAllBatimentsWithAtLeastOneBlankObject();
        List<BatimentModel> GetAllBatimentVerifies();
        List<BatimentModel> GetAllBatimentNonVerifies();
        //

        #endregion

        #region DECES
        DecesModel GetDecesById(long decesId);
        List<MenageDetailsModel> GetDecesByMenageDetails(long menageId);
        List<DecesModel> GetDecesByMenage(long menageId);
        List<DecesModel> GetAllDeces();
        #endregion

        #region EMIGRE
        EmigreModel GetEmigreById(int emigreId);
        List<MenageDetailsModel> GetEmigreByMenageDetails(long menageId);
        List<EmigreModel> GetEmigrebyMenage(long menageId);
        List<EmigreModel> GetAllEmigres();
        #endregion

        #region LOGEMENTS
        List<LogementModel> GetAllLogements();
        List<LogementModel> GetAllLogementsIndVerifies();
        List<LogementModel> GetAllLogementsIndNonVerifies();
        List<LogementModel> GetAllLogementsIndTermines();
        List<LogementModel> GetAllLogementsIndValides();
        List<LogementModel> GetAllLogementsColVerifies();
        List<LogementModel> GetAllLogementsColNonVerifies();
        List<LogementModel> GetAllLogementsColTermines();
        List<LogementModel> GetAllLogementsColNonTermines();
        List<LogementModel> GetAllLogementsColValides();
        List<LogementModel> GetAllLogementsCollectifs();
        List<LogementModel> GetAllLogementsIndividuels();
        List<LogementModel> GetAllLogementsByBatiment(long batimentId);
        List<LogementModel> GetLogementCByBatiment(long batimentId);
        List<LogementModel> GetLogementIByBatiment(long batimentId);
        List<LogementModel> GetLogementIFiniByBatiment(long batimentId);
        List<LogementModel> GetAllLogementIndNonTermines();
        List<LogementModel> GetAllLogementOccupantAbsent();
        List<LogementModel> GetAllLogementOccupeOccasionnellement();
        List<LogementModel> GetAllLogementVide();
        LogementModel GetLogementById(long logId);
        #endregion

        #region MENAGES
        List<MenageModel> GetMenageByLogement(long logId);
        List<MenageDetailsModel> GetRapportFinal(long menageId);
        List<MenageModel> GetMenageFiniByLogement(long logId);
        List<MenageModel> GetAllMenageNonTermine();
        List<MenageModel> GetAllMenageTermine();
        List<MenageModel> GetAllMenageValides();
        MenageModel GetMenageById(long menageId);
        List<MenageModel> GetAllMenages();
        List<MenageModel> GetAllMenagesVerifies();
        List<MenageModel> GetAllMenagesNonVerifies();
        List<MenageModel> GetAllMenage_1_Personne();
        List<MenageModel> GetAllMenage_2_3_Personnes();
        List<MenageModel> GetAllMenage_4_5_Personnes();
        List<MenageModel> GetAllMenage_6_Personnes();
        Flag compteurFlagParMenages(List<MenageModel> menages);
        #endregion

        #region INDIVIDUS
        List<IndividuModel> GetIndividuByMenage(long menageId);
        List<IndividuModel> GetAllIndividuNonTermine();
        List<IndividuModel> GetAllIndividuNonVerifie();
        List<IndividuModel> GetAllIndividuVerifie();
        List<IndividuModel> GetAllIndividuTermine();
        List<IndividuModel> GetAllIndividuValide();
        IndividuModel GetIndividuById(long indId);
        List<MenageDetailsModel> GetIndividuByMenageDetails(long menageId);
        List<IndividuModel> GetIndividuByLoge(long logeId);
        List<IndividuModel> GetAllIndividus();
        List<IndividuModel> GetAllIndividusVerifies();
        List<IndividuModel> GetAllIndividusNonVerifies();
        //Nombre d'individus dans les menages
        List<IndividuModel> GetAllIndividusInMenage();
        //Nombre d'individus sans informations sur l'Age et sur la date de naissance;
        List<IndividuModel> GetAllIndividusWithoutAgeAndBirthDay();

        //Nombre d'individus sans info sur la date de naissance
        List<IndividuModel> GetAllIndividusWithoutBirthDay();

        //Nonbre individus sans age
        List<IndividuModel> GetAllIndividusWithoutAge();

        //Nombre individus de 3 ans sans info sur le niveau d'etude
        List<IndividuModel> GetAllIndividus3ansWithoutNiveauEtude();

        //Nombre individus femmes de 13 ans sans info sur le nbre de garcons et de filles nes vivants
        List<IndividuModel> GetAllIndividusFemmes13ansSansFGNesVivants();

        Codification getInformationForCodification();
        Flag getIndividuWithP10();
        Flag getIndividuWithP12();
        Flag getIndividuWithA5();
        Flag getIndividuWithA7();
        Flag CountTotalFlag(List<IndividuModel> individus);
        Flag Count2FlagAgeDateNaissance();
        Flag CountFlagFecondite();
        Flag CountFlagEmploi();

        /// <summary>
        /// Return location of an individu (Batiment/Logement/Menage/NoOrdre)
        /// </summary>
        /// <param name="individus"></param>
        /// <returns></returns>
        string locateIndividu(IndividuModel individu);
        #endregion

        #region SDES
        Tbl_Sde getSdeDetailsFromSqliteFile();
        int getTotalBatiment();
        int getTotalLogements();
        int getTotalLogementCs();
        int getTotalLogementInds();
        int getTotalMenages();
        int getTotalEmigres();
        int getTotalEmigresFemmes();
        int getTotalEmigresHommes();
        int getTotalDeces();
        int getTotalDecesFemmes();
        int getTotalDecesHommes();
        int getTotalIndividus();
        int getTotalIndividusFemmes();
        int getTotalIndividusHommes();
        int getTotalBatimentCartographies(string sdeId);
        #endregion

        #region QUESTIONS
        tbl_question getQuestion(string codeQuestion);
        tbl_question getQuestionByNomChamps(string nomChamps);
        string getReponse(string codeQuestion, string codeReponse);
        string getLibelleCategorie(string codeCategorie);

        List<tbl_question> searchQuestionByCategorie(string codeCategorie);

        tbl_categorie_question getCategorie(string codeCategorie);

        List<tbl_question_module> listOfQuestionModule(string codeModule);
        #endregion

        #region GESTION
        int getTotalBatRecenseV();
        int getTotalBatRecenseNV();
        int getTotalLogeCRecenseV();
        int getTotalLogeCRecenseNV();
        int getTotalLogeIRecenseV();
        int getTotalLogeIRecenseNV();
        int getTotalMenageRecenseV();
        int getTotalMenageRecenseNV();
        int getTotalIndRecenseV();
        int getTotalIndRecenseNV();
        int getTotalLogeIOccupeRecenseNV();
        int getTotalLogeIOccupeRecenseV();
        int getTotalLogeIVideRecenseNV();
        int getTotalLogeIVideRecenseV();
        int getTotalLogeIUsageTemporelRecenseNV();
        int getTotalLogeIUsageTemporelRecenseV();
        int getTotalLogeIOccupeRecense();
        int getTotalLogeIVideRecense();
        int getTotalLogeIUsageTemporelRecense();
        #endregion

        #region PERFORMANCE
        double getTotalBatRecenseParJourV();
        int getTotalBatRecenseParJourNV();
        double getTotalLogeCRecenseParJourV();
        double getTotalLogeRecenseParJourV();
        int getTotalLogeCRecenseParJourNV();
        int getTotalLogeIRecenseParJourV();
        int getTotalLogeIRecenseParJourNV();
        double getTotalMenageRecenseParJourV();
        int getTotalMenageRecenseParJourNV();
        double getTotalIndRecenseParJourV();
        int getTotalIndRecenseParJourNV();
        #endregion

        #region DEMOGRAPHIQUE
        double tailleMoyenneMenage();
        float getIndiceMasculinite();
        int getTotalEnfantDeMoinsDe1Ans();
        int getTotalIndividu18AnsEtPlus();
        int getTotalIndividu10AnsEtPlus();
        int getTotalIndividu65AnsEtPlus();
        int getTotalMenageUnipersonnel();
        List<MenageModel> searchMenageUnipersonnel();
        List<MenageModel> searchMenage2Ou3Personne();
        List<MenageModel> searchMenage4Ou5Personne();
        List<MenageModel> searchMenage6PlusPersonne();
        int getTotalMenageDe6IndsEtPlus();
        int getTotalPersonnesByLogementCollectif();
        int getTotalPersonnesByLogementCollectifDeclare();

        int getTotalPersonnesByLimitation();
        int getTotalFemmeChefMenage();
        int getTotalHommeChefMenage();
        int getTotalPersonnesByMenage(long menageId);
        int getTotalFemmesByMenage(long menageId);
        int getTotalEnfantMoins5AnsByMenage(long menageId);
        int getTotalPersonnesMoin15AnsByMenage(long menageId);
        int getTotalDecesByMenage( long logeId);
        int getTotalHandicapVoirByMen(long menageId);
        int getTotalhandicapEntendrebyMen(long menageId);
        int getTotalHandicapMarcherByMen(long menageId);
        int getTotalhandicapSouvenirByMen(long menageId);
        int getTotalHandicapSoignerbyMen(long menageId);
        int getTotalHandicapCommuniquerbyMen(long menageId);
        int getTotalAnalphabetes15AnsbyMen(long menageId);
        int getTotalPersonneFrequentantEcoleByMen(long menageId);
        int getTotalPersonneNiveauSecondairebyMen(long menageId);
        int getTotalFormationProByMen(long menageId);
        #endregion

        #region GEOGRAPHIQUES
        tbl_pays getpays(string codePays);
        tbl_departement getDepartement(string deptId);
        tbl_commune getCommune(string comId);
        tbl_vqse getVqse(string vqse);

        #endregion

        #region DOMAINE ETUDE
        tbl_domaine_etude getDomaine(string domaineId);
        #endregion

        #region RAPPORT AGENT RECENSEUR
        List<RapportArModel> GetAllRptAgentRecenseur();
        List<RapportArModel> GetAllRptAgentRecenseurByBatiment(long batimentId);
        List<RapportArModel> GetAllRptAgentRecenseurByLogement(long logeId);
        List<RapportArModel> GetAllRptAgentRecenseurByMenage(long menageId);
        List<RapportArModel> GetAllRptAgentRecenseurByIndividu(long individuId);
        List<RapportArModel> GetAllRptAgentRecenseurForNotFinishedObject();
        #endregion

        #region RAPPORTFINAL
        RapportFinalModel GetRapportFinal(int menageId);
        RapportFinalModel GetRapportFinalModel(int menageId);
        List<RapportFinalModel> searchRapportFinal(int menageId);
        #endregion

        #region DUPLICATION
        List<BatimentModel> GetBatimentByRec(string qrec);
        List<LogementModel> GetLogementsByNoOrdre(long batiment,long numOrdre);
        List<MenageModel> GetMenagebyNumOrdre(long batiment, long logementId,long numOrdre);
        List<EmigreModel> GetEmigrebyNumOrdre(long batiment, long logementId, long menageId, long numOrdre);
        List<DecesModel> GetDecesbyNumOrdre(long batiment, long logementId, long menageId, long numOrdre);
        List<IndividuModel> GetIndividuByNumOrdre(long batiment, long logementId, long menageId, long numOrdre);
        
        #endregion
    }
}
