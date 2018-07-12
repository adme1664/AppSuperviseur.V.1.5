using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsi.Rgph.DataAccess.Dao
{
   public interface IDaoSettings
    {
        #region GESTION DES TABLETTES
        bool saveMateriels(Tbl_Materiels materiels);
        bool updateMateriels(Tbl_Materiels materiels);
        Tbl_Materiels getMateriels(string serial);
        List<Tbl_Materiels> searchMateriels();
        #endregion

        #region GESTION DES SDES
        Tbl_Sde getSdeDetails(string sdeId);
        Tbl_Sde getSdeByAgent(long agentId);
        bool saveSdeDetails(Tbl_Sde sde);
        bool updateSdeDetails(Tbl_Sde sde);
        List<Tbl_Sde> searchAllSdes();
        #endregion

        #region GESTION DES AGENTS
        Tbl_Agent insertAgent(Tbl_Agent agent);
        void updateAgent(Tbl_Agent agent);
        Tbl_Agent findAgentById(long agentId);
        Tbl_Agent findAgentByUsername(string username);
        void deleteAgent(long agentId);
        Tbl_Agent findAgent(string sdeId);
        List<Tbl_Agent> searchAllAgents();
        bool isAgentGotDevice(int agentId);
        #endregion

        #region GESTION DES RETOURS
        bool saveRetour(Tbl_Retour retour);
        bool updateRetour(Tbl_Retour retour);
        List<Tbl_Retour> searchAllRetours();

        List<Tbl_Retour> searchAllRetourBySde(string sdeId);
        Tbl_Retour getRetour(long id);

        #endregion

        #region GESTION DES PROBLEMES POUR LA VERIFICATION
        bool saveProbleme(Tbl_Probleme probleme);
        bool updateProbleme(Tbl_Probleme probleme);
        bool deleteProbleme(int problemeId);
        List<Tbl_Probleme> searchAllProblemes();
        Tbl_Probleme getProblemeByCodeQuestionAndBatiment(string codeQuestion, long batimentId);
        List<Tbl_Probleme> searchAllProblemesBySdeId(string sdeID);
        Tbl_Probleme getProbleme(int problemeId);
        #endregion


        MainRepository getRepository();
    }
}
