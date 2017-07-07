using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.Json;

namespace Ht.Ihsil.Rgph.App.Superviseur.services
{
    public interface IContreEnqueteService
    {
        #region BATIMENT
        List<BatimentModel> getBatimentVideInCE(string sdeId);
        List<BatimentModel> searchBatimentByCE(int contreEnqueteId, string sdeId);
        List<BatimentModel> getBatimentInCE(string sdeId);
        BatimentModel getBatimentWithLogementC();
        BatimentCEModel getBatiment(long batimentId, string sdeId);
        List<BatimentModel> searchBatimentWithLogementIndVide();
        BatimentModel getBatimentWithLogColCE(string sdeId);
        List<BatimentModel> getBatimentWithLogIndVideCE(string sdeId);
        List<BatimentModel> getBatimentWithLogIndCE(string sdeId);
        List<BatimentModel> getBatimentWithLogInd();
        List<BatimentModel> getBatimentVide();
        bool saveBatiment(BatimentCEModel bat);
        bool updateBatiment(BatimentCEModel bat);
        bool isBatimentExist(int batimentId, string sdeId);
        #endregion

        #region MENAGE
        bool saveMenageCE(MenageCEModel men);
        bool updateMenageCE(MenageCEModel men);
        List<MenageCEModel> searchAllMenageCE(LogementCEModel _log);
        List<MenageDetailsModel> searchAllInMenage(MenageCEModel _men, string type);
        MenageCEModel getMenageById(long batimentId, long logId, string sdeId, long id);
        #endregion

        #region LOGEMENT
        LogementModel getFirstLogementInd(BatimentModel _batiment, LogementTypeModel _logementType);
        LogementModel getFirstLogementCol(BatimentModel _batiment, LogementTypeModel _logementType);
        bool saveLogementCE(LogementCEModel log);
        bool updateLogement(LogementCEModel log);
        List<LogementCEModel> searchLogement(int batimentId, string sdeId);
        LogementCEModel getLogementCE(int batimentId, string sdeId, int logId);
        #endregion

        #region DECES
        bool saveDecesCE(DecesCEModel _dec);
        bool updateDecesCE(DecesCEModel _dec);
        DecesCEModel getDecesCEModel(long decesId, string sdeId);

        #endregion

        #region EMIGRE
        bool saveEmigre(EmigreCEModel _emigre);
        bool updateEmigre(EmigreCEModel _emigre);
        EmigreCEModel getEmigreCEModel(long emigreId, string sdeId);
        EmigreCEModel getEmigreCEModel(long batimentId, long logId, long menageId, long emigreId, string sdeId);
        #endregion

        #region INDIVIDU
        bool saveIndividuCE(IndividuCEModel _ind);
        bool updateIndividuCE(IndividuCEModel _ind);
        IndividuCEModel getIndividuCEModel(long individuId, string sdeId);
        IndividuCEModel getIndividuCEModel(long batimentId, long logeId, long menageId, long individuId, string sdeId);
        IndividuModel getFirstIndividu(MenageModel _menage);
        List<IndividuCEModel> searchAllIndividuCE(MenageCEModel _men);
        List<IndividuCEModel> searchAllIndividuCE(long _logId);
        #endregion

        #region DECES
        DecesModel getFirstDeces(MenageModel _menage);
        List<DecesCEModel> searchAllDecesCE(MenageCEModel _men);
        #endregion

        #region CONTRE-ENQUETE
        void saveContreEnquete(ContreEnqueteModel ce);
        bool updateContreEnquete(ContreEnqueteModel ce);
        List<ContreEnqueteModel> searchContreEnquete(int typeContreEnquete, string sdeId);
        List<ContreEnqueteModel> searchContreEnquete(string sdeId);
        ContreEnqueteModel getContreEnquete(long id, string sdeId);
        List<BatimentJson> getAllBatimentCEInJson(string sdeId);

        #endregion

        #region GEOLOCALISATION
        List<DepartementModel> searchAllDepartement();
        List<CommuneModel> searchAllCommuneByDept(string deptId);
        List<VqseModel> searchAllVqsebyCom(string comId);
        List<PaysModel> searchAllPays();
        CommuneModel getCommune(string idCommune);
        DepartementModel getDepartement(string idDepartement);
        PaysModel getPays(string idPays);
        VqseModel getVqse(string vqseId);
        #endregion

        #region RAPPORT PERSONNEL
        bool saveRptPersonnel(RapportPersonnelModel rpt);
        bool updateteRptPersonnel(RapportPersonnelModel rpt);
        bool deleteRptPersonnel(int rptId);
        RapportPersonnelModel getRptPersonnel(int rptId);
        List<RapportPersonnelModel> searchRptPersonnelByAgent(int persId);
        List<RapportPersonnelModel> searchRptPersonnel();
        #endregion

        #region RAPPORT DEROULEMENT COLLECTE
        long saveRptDeroulement(RapportDeroulementModel rptDeroulement);
        bool updateRptDeroulement(RapportDeroulementModel rptDeroulement);
        List<RapportDeroulementModel> searchRptDeroulment();
        bool saveDetailsDeroulement(DetailsRapportModel details);
        bool updateDetailsDeroulement(DetailsRapportModel details);
        DetailsRapportModel getDetailsRapportDeroulement(long id);
        List<DetailsRapportModel> searchDetailsReport(RapportDeroulementModel rptDeroulment);
        #endregion

        #region WRITE IN SQLITE FILE AFTER A CONTRE-ENQUETE
        bool updateBatimentInSqlite(BatimentCEModel bat);
        bool updateLogementInSqlite(LogementCEModel log);
        bool updateMenageInSqlite(MenageCEModel men);
        bool updateDecesInSqlite(DecesCEModel dec);
        bool updateIndividuInSqlite(IndividuCEModel ind);
        #endregion

        #region INDICATEUR SOCIO-DEMOGRAPHIQUE
        int getTotalPersonnesByMenage(MenageCEModel model);
        int getTotalFemmesByMenage(MenageCEModel model);
        int getTotalEnfantMoins5AnsByMenage(MenageCEModel model);
        int getTotalPersonnesMoin15AnsByMenage(MenageCEModel model);
        int getTotalDecesByMenage(MenageCEModel model);
        int getTotalHandicapVoirByMen(MenageCEModel model);
        int getTotalhandicapEntendrebyMen(MenageCEModel model);
        int getTotalHandicapMarcherByMen(MenageCEModel model);
        int getTotalhandicapSouvenirByMen(MenageCEModel model);
        int getTotalHandicapSoignerbyMen(MenageCEModel model);
        int getTotalHandicapCommuniquerbyMen(MenageCEModel model);
        int getTotalAnalphabetes15AnsbyMen(MenageCEModel model);
        int getTotalPersonneFrequentantEcoleByMen(MenageCEModel model);
        int getTotalPersonneNiveauSecondairebyMen(MenageCEModel model);
        int getTotalFormationProByMen(MenageCEModel model);
        #endregion

    }
}
