using Ht.Ihsi.Rgph.DataAccess.Dao;
using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ht.Ihsi.Rgph.DataAccess.Exceptions;

namespace Ht.Ihsil.Rgph.App.Superviseur.services
{
    public class ConfigurationService : IConfigurationService
    {
        #region DECLARATIONS VARIABLES
        private DaoSettings daoSettings;
        Logger log;
        ISqliteDataWriter sdw;
        #endregion

        #region CONSTRUCTORS
        public ConfigurationService()
        {
            string path = Utilities.getConnectionString(Users.users.SupDatabasePath);
            daoSettings = new DaoSettings(path);
            log = new Logger();
        }
        #endregion

        #region SDE
        public Models.SdeModel getSdeDetails(string sdeId)
        {
            try
            {
                return ModelMapper.MapToSdeModel(daoSettings.getSdeDetails(sdeId));

            }
            catch (Exception)
            {

            }
            return null;
        }

        public bool saveSdeDetails(Tbl_Sde sde)
        {

            try
            {
                return daoSettings.saveSdeDetails(sde);
            }
            catch (Exception)
            {

            }
            return false;
        }

        public bool updateSdeDetails(Tbl_Sde sde)
        {
            try
            {
                return daoSettings.updateSdeDetails(sde);
            }
            catch (Exception)
            {

            }
            return false;

        }

        public List<Models.SdeModel> searchAllSdes()
        {
            try
            {
                return ModelMapper.MapToListSdeModel(daoSettings.searchAllSdes());
            }
            catch (Exception)
            {

            }
            return null;

        }
        #endregion

        #region AGENTS
        public AgentModel findAgentByUsername(string username)
        {
            try
            {
                return ModelMapper.MapToAgentModel(daoSettings.findAgentByUsername(username));
            }
            catch (Exception e)
            {

            }
            return new AgentModel();
        }

        public AgentModel insertAgentSde(Models.AgentModel agent)
        {
            try
            {
               return ModelMapper.MapToAgentModel( daoSettings.insertAgent(EntityMapper.MapMAgentInInEntity(agent)));
            }
            catch (Exception)
            {

            }
            return new AgentModel();
        }

        public void updateAgentSde(Models.AgentModel agent)
        {
            try
            {
                daoSettings.updateAgent(EntityMapper.MapMAgentInInEntity(agent));
            }
            catch (Exception)
            {

            }

        }

        public Models.AgentModel findAgentSderById(long agentId)
        {
            try
            {
                return ModelMapper.MapToAgentModel(daoSettings.findAgentById(agentId));
            }
            catch (Exception)
            {

            }
            return null;
        }

        public void deleteAgentSde(long agentId)
        {
            try
            {
                daoSettings.deleteAgent(agentId);
            }
            catch (Exception)
            {

            }
        }

        public Models.SdeModel getSdeByAgent(long agentId)
        {
            try
            {
                return ModelMapper.MapToSdeModel1(daoSettings.getSdeByAgent(agentId));
            }
            catch (Exception ex)
            {

            }
            return null;
        }


        public List<Models.AgentModel> searchAllAgents()
        {
            try
            {
                List<Tbl_Agent> list = daoSettings.searchAllAgents();
                List<AgentModel> listOfAgent = new List<AgentModel>();
                if (list != null)
                {
                    foreach (Tbl_Agent agent in list)
                    {
                        AgentModel model = ModelMapper.MapToAgentModel(agent);
                        listOfAgent.Add(model);
                    }
                }
                return listOfAgent;
            }
            catch (Exception ex)
            {
                log.Info("getSdeByAgent/Error:" + ex.Message);
            }
            return null;

        }
        public List<AgentModel> searchAllAgentsToDisplay()
        {
            try
            {
                List<Tbl_Agent> list = daoSettings.searchAllAgents();
                List<AgentModel> listOfAgent = new List<AgentModel>();
                if (list != null)
                {
                    foreach (Tbl_Agent agent in list)
                    {
                        AgentModel model = ModelMapper.MapToAgentModel(agent);
                        model.Username = "" + agent.Prenom + "(" + agent.CodeUtilisateur + ")";
                        listOfAgent.Add(model);
                    }
                }
                return listOfAgent;
            }
            catch (Exception ex)
            {
                log.Info("searchAllAgentsToDisplay/Error:" + ex.Message);
            }
            return null;
        }

        public bool savePersonne(tbl_personnel person)
        {
            try
            {
                if (person != null)
                {
                    sdw = new SqliteDataWriter(person.sdeId);
                    return sdw.savePersonnel(person);

                }
            }
            catch (Exception)
            {

            }
            return false;
        }

        public bool ifPersonExist(tbl_personnel person)
        {
            try
            {
                sdw = new SqliteDataWriter(person.sdeId);
                return sdw.ifPersonExist(person);
            }
            catch (Exception)
            {

            }
            return false;
        }


        public bool isAgentExist(int agentId)
        {
            try
            {
                return daoSettings.isAgentGotDevice(agentId);
            }
            catch (Exception)
            {

            }
            return false;

        }
        #endregion

        #region MATERIELS
        public Tbl_Materiels getMaterielByAgent(int agentId)
        {
            try
            {
                return daoSettings.getRepository().MaterielsRepository.Find(m => m.AgentId == agentId).FirstOrDefault();
            }
            catch (Exception)
            {

            }
            return new Tbl_Materiels();
        }

        public bool deleteMateriel(int id)
        {
            try
            {
                daoSettings.getRepository().MaterielsRepository.Delete(id);
                daoSettings.getRepository().Save();
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public bool saveMateriels(Tbl_Materiels materiels)
        {
            try
            {
                return daoSettings.saveMateriels(materiels);
            }
            catch (Exception)
            {

            }
            return false;

        }

        public bool updateMateriels(Tbl_Materiels materiels)
        {
            try
            {
                return daoSettings.updateMateriels(materiels);
            }
            catch (Exception)
            {

            }
            return false;
        }

        public Tbl_Materiels getMateriels(string serial)
        {
            try
            {
                return daoSettings.getMateriels(serial);
            }
            catch (Exception ex)
            {
                log.Info("getMateriels/Error:" + ex.Message);
            }
            return null;
        }


        public bool isMaterielExist(string serial)
        {
            try
            {
                Tbl_Materiels mat = daoSettings.getMateriels(serial);
                if (mat != null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Info("isMaterielExist/Error:" + ex.Message);
            }
            return false;
        }
        public bool isMaterielConfigure(string serial)
        {
            try
            {
                Tbl_Materiels mat = daoSettings.getMateriels(serial);
                if (mat.IsConfigured.GetValueOrDefault() == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Info("isMaterielConfigure/Error:" + ex.Message);
            }
            return false;
        }
        public List<Tbl_Materiels> SearchMateriels()
        {
            try
            {
                return daoSettings.searchMateriels();
            }
            catch (Exception)
            {

            }
            return new List<Tbl_Materiels>();
        }
        #endregion

        #region GESTION DES RETOURS

        public bool saveRetour(RetourModel retour)
        {
            try
            {
                return daoSettings.saveRetour(ModelMapper.MapToTbl_Retour(retour));
            }
            catch (MessageException ex)
            {
                throw new MessageException(ex.Message);
            }
            catch (Exception)
            {

            }
            return false;
        }
        public bool updateRetour(RetourModel retour)
        {
            try
            {
                return daoSettings.updateRetour(ModelMapper.MapToTbl_Retour(retour));
            }
            catch (MessageException ex)
            {
                throw new MessageException(ex.Message);
            }
            catch (Exception)
            {

            }
            return false;
        }

        public List<RetourModel> searchAllRetours()
        {
            try
            {
                return ModelMapper.MapToListRetourModel(daoSettings.searchAllRetours());
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception)
            {

            }
            return new List<RetourModel>();
        }

        public List<RetourModel> searchAllRetourBySde(string sdeId)
        {
            try
            {
                return ModelMapper.MapToListRetourModel(daoSettings.searchAllRetourBySde(sdeId));
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception)
            {

            }
            return new List<RetourModel>();
        }

        public RetourModel getRetour(long id)
        {
            try
            {
                return ModelMapper.MapToRetourModel(daoSettings.getRetour(id));
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception)
            {

            }
            return new RetourModel();
        }
        #endregion

        #region GESTION DES PROBLEMES
        public bool saveProbleme(ProblemeModel probleme)
        {
            string method = "saveProbleme";
            try
            {
                return daoSettings.saveProbleme(ModelMapper.MapToTbl_Probleme(probleme));
            }
            catch (Exception ex)
            {
                log.Info("Erreur/" + method + "<>===========:" + ex.Message);
                throw new MessageException(ex.Message);
            }
        }

        public bool updateProbleme(ProblemeModel probleme)
        {
            string method = "updateProbleme";
            try
            {
                // ProblemeModel problemeUpdate = ModelMapper.MapToProblemeModel(daoSettings.getProblemeByCodeQuestionAndBatiment(probleme.CodeQuestion,probleme.BatimentId.GetValueOrDefault()));
                return daoSettings.updateProbleme(ModelMapper.MapToTbl_Probleme(probleme));
            }
            catch (Exception ex)
            {
                log.Info("Erreur/" + method + "<>===========:" + ex.Message);
                throw new MessageException(ex.Message);
            }
        }

        public bool deleteProbleme(int problemeId)
        {
            string method = "deleteProbleme";
            try
            {
                return daoSettings.deleteProbleme(problemeId);
            }
            catch (Exception ex)
            {
                log.Info("Erreur/" + method + "<>===========:" + ex.Message);
                throw new MessageException(ex.Message);
            }
        }

        public List<ProblemeModel> searchAllProblemes()
        {
            string method = "searchAllProblemes";
            try
            {
                return ModelMapper.MapToListProblemeModel(daoSettings.searchAllProblemes());
            }
            catch (Exception ex)
            {
                log.Info("Erreur/" + method + "<>===========:" + ex.Message);
                throw new MessageException(ex.Message);
            }
        }

        public List<ProblemeModel> searchAllProblemesBySdeId(string sdeID)
        {
            string method = "searchAllProblemesBySdeId";
            try
            {
                return ModelMapper.MapToListProblemeModel(daoSettings.searchAllProblemesBySdeId(sdeID));
            }
            catch (Exception ex)
            {
                log.Info("Erreur/" + method + "<>===========:" + ex.Message);
                throw new MessageException(ex.Message);
            }
        }

        public ProblemeModel getProbleme(int problemeId)
        {
            string method = "getProbleme";
            try
            {
                return ModelMapper.MapToProblemeModel(daoSettings.getProbleme(problemeId));
            }
            catch (Exception ex)
            {
                log.Info("Erreur/" + method + "<>===========:" + ex.Message);
                throw new MessageException(ex.Message);
            }
        }
        public ProblemeModel getProblemeByCodeQuestionAndBatiment(string codeQuestion, long batimentId)
        {
            string method = "getProblemeByCodeQuestionAndBatiment";
            try
            {
                return ModelMapper.MapToProblemeModel(daoSettings.getProblemeByCodeQuestionAndBatiment(codeQuestion, batimentId));
            }
            catch (Exception ex)
            {
                log.Info("Erreur/" + method + "<>===========:" + ex.Message);
                throw new MessageException(ex.Message);
            }
        }
        #endregion

    }
   
}
