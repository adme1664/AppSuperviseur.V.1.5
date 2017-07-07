using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsi.Rgph.DataAccess.Dao
{
   public interface IDaoContreEnquete
   {

        #region CONTREENQUETE
       void saveContreEnquete(Tbl_ContreEnquete ce);
       bool updateContreEnquete(Tbl_ContreEnquete ce);
       Tbl_ContreEnquete getContreEnquete(int batimentId, string sdeId);
       Tbl_ContreEnquete getContreEnquete(long id, string sdeId);
       List<Tbl_ContreEnquete> searchContreEnquete(string sdeId);
       List<Tbl_ContreEnquete> searchContreEnquete(string sdeId, int typeContreEnquete);
       #endregion

        #region BATIMENT
        bool saveBatimentCE(Tbl_BatimentCE bat);
        bool updateBatimentCE(Tbl_BatimentCE bat);
        Tbl_BatimentCE getBatiment(long batId, string sdeId);
        List<Tbl_BatimentCE> searchBatimentWithLogement(string sdeId);
        #endregion
       
        #region LOGEMENT
        bool saveLogement(Tbl_LogementCE log);
        bool updateLogement(Tbl_LogementCE log);
        bool isLogementExistInBatiment(int batimentId, string sdeId);
        bool isLogementCollectifExistInBatiment(int batimentId, string sdeId);
        List<Tbl_LogementCE> searchLogementbyBatiment(int batimentId, string sdeId);
        List<Tbl_LogementCE> searchLogByBatimentAndTypeLog(long batimentId, string sdeId,long typeLogement);
        List<Tbl_LogementCE> searchLogement(string sdeId);
        Tbl_LogementCE getLogementCE(int batimentId, string sdeId, int logId);
        #endregion

        #region DECES
        bool saveDecesCE(Tbl_DecesCE _deces);
        bool updateDecesCE(Tbl_DecesCE _deces);
        Tbl_DecesCE getDecesCEModel(long decesId, string sdeId);
        Tbl_DecesCE getDecesCEModel(long batimentId, long logId, long menageId,long decesId, string sdeId);
        #endregion

        #region EMIGRE
        bool saveEmigre(Tbl_EmigreCE _emigre);
        bool updateEmigre(Tbl_EmigreCE _emigre);
        List<Tbl_EmigreCE> searchAllEmigres(long batimentId, long logId, long menageId, string sdeId);
        Tbl_EmigreCE getEmigreCEModel(long emigreId, string sdeId);
        Tbl_EmigreCE getEmigreCEModel(long batimentId, long logId, long menageId, long emigreId, string sdeId);
        #endregion

        #region MENAGE
        List<Tbl_MenageCE> searchAllMenageCE(long batimentId, long logeId, string sdeId);
        List<Tbl_IndividusCE> searchAllIndividuCE(long batimentId, long logeId, string sdeId, long menageId);
        List<Tbl_DecesCE> searchAllDecesCE(long batimentId, long logeId, string sdeId, long menageId);
        bool saveMenage(Tbl_MenageCE _men);
        bool updateMenage(Tbl_MenageCE _men);
        Tbl_MenageCE getMenageCE(long batimentId, long logeId, string sdeId, long menageId);
        Tbl_MenageCE getMenageCE(long menageId);
        #endregion

        #region INDIVIDU
        bool saveIndividuCE(Tbl_IndividusCE _ind);
        bool upateIndividuCE(Tbl_IndividusCE _ind);
        Tbl_IndividusCE getIndividuCEModel(long individuId, string sdeId);
        Tbl_IndividusCE getIndividuCEModel(long batimentId,long logeId,long menageId,long individuId, string sdeId);
        List<Tbl_IndividusCE> searchAllIndividuCE(long logId);

        #endregion

        #region RAPPORT PERSONNEL
        bool saveRptPersonnel(Tbl_RapportPersonnel rpt);
        bool updateteRptPersonnel(Tbl_RapportPersonnel rpt);
        bool deleteRptPersonnel(int rptId);
        Tbl_RapportPersonnel getRptPersonnel(int rptId);
        List<Tbl_RapportPersonnel> searchRptPersonnelbyAgent(int persId);
        List<Tbl_RapportPersonnel> searchRptPersonnel();
        
        #endregion

        #region GEOLOCALISATION
        List<Tbl_Departement> searchAllDepartement();
        List<Tbl_Commune> searchAllCommuneByDept(string deptId);
        Tbl_Commune getCommune(string idCommune);
        Tbl_Departement getDepartement(string idDepartement);
        Tbl_Pays getPays(string idPays);
        Tbl_VilleQuartierSectionCommunale getVqse(string vqseId);
        List<Tbl_VilleQuartierSectionCommunale> searchAllVqsebyCom(string comId);
        List<Tbl_Pays> searchAllPays();
        
        #endregion

        #region RAPPORT SUR LE DEROULEMENT DE LA COLLECTE
        long saveRptDeroulement(Tbl_RprtDeroulement rptDeroulement);
        bool updateRptDeroulement(Tbl_RprtDeroulement rptDeroulement);
        List<Tbl_RprtDeroulement> searchRptDeroulment();
        Tbl_RprtDeroulement getRptDeroulement(string codeDistrict);

        bool saveDetailsDeroulement(Tbl_DetailsRapport details);
        bool updateDetailsDeroulement(Tbl_DetailsRapport details);
        List<Tbl_DetailsRapport> searchDetailsReport(Tbl_RprtDeroulement rptDeroulment);
       #endregion

   }
}
