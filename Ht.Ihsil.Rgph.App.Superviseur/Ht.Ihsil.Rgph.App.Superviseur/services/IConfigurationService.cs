using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.services
{
    interface IConfigurationService
    {
        #region GESTION DES SDES
        SdeModel getSdeDetails(string sdeId);
        SdeModel getSdeByAgent(long agentId);
        bool saveSdeDetails(Tbl_Sde sde);
        bool updateSdeDetails(Tbl_Sde sde);
        List<SdeModel> searchAllSdes();
        #endregion

        #region GESTIONS DES AGENTS
        void insertAgentSde(AgentModel agent);
        List<AgentModel> searchAllAgents();
        List<AgentModel> searchAllAgentsToDisplay();
        void updateAgentSde(AgentModel agent);
        AgentModel findAgentSderById(long agentId);
        void deleteAgentSde(long agentId);
        bool isAgentExist(int agentId);
        #endregion

        #region GESTION DS MATERIELS
        bool saveMateriels(Tbl_Materiels materiels);
        bool updateMateriels(Tbl_Materiels materiels);
        Tbl_Materiels getMateriels(string serial);
        Tbl_Materiels getMaterielByAgent(int agentId);
        bool deleteMateriel(int id);
        List<Tbl_Materiels> SearchMateriels();
        bool isMaterielExist(string serial);
        bool isMaterielConfigure(string serial);
        #endregion

        #region GESTION DES RETOURS
        bool saveRetour(RetourModel retour);
        List<RetourModel> searchAllRetours();

        List<RetourModel> searchAllRetourBySde(string sdeId);
        RetourModel getRetour(long id);
        #endregion

        #region GESTION DES PROBLEMES POUR LA VERIFICATION
        bool saveProbleme(ProblemeModel probleme);
        bool updateProbleme(ProblemeModel probleme);
        bool deleteProbleme(int problemeId);
        List<ProblemeModel> searchAllProblemes();
        List<ProblemeModel> searchAllProblemesBySdeId(string sdeID);
        ProblemeModel getProblemeByCodeQuestionAndBatiment(string codeQuestion, long batimentId);
        ProblemeModel getProbleme(int problemeId);
        #endregion

        bool savePersonne(tbl_personnel person);
        bool ifPersonExist(tbl_personnel person);
    }
}
