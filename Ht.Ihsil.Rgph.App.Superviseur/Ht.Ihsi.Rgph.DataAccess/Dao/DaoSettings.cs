using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.DataAccess.Exceptions;
using Ht.Ihsi.Rgph.DataAccess.Repositories;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsi.Rgph.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsi.Rgph.DataAccess.Dao
{
    public class DaoSettings : IDaoSettings
    {
        #region DECLARATIONS
        MainRepository repository;
        Logger log;
        #endregion

        #region CONSTRUCTORS
        public DaoSettings()
        {
            repository = new MainRepository();
            log = new Logger();
        }
        public DaoSettings(string connectionString)
        {
            repository = new MainRepository(connectionString, true);
            log = new Logger();
        }
        #endregion

        #region GESTION DES MATERIELS
        public bool saveMateriels(Tbl_Materiels materiels)
        {
            try
            {
                repository.MaterielsRepository.Insert(materiels);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("<>===========Error:" + ex.Message);
            }
            return false;
        }

        public bool updateMateriels(Tbl_Materiels materiels)
        {
            try
            {
                Tbl_Materiels mat = repository.MaterielsRepository.Find(m => m.Imei == materiels.Imei).FirstOrDefault();
                mat.LastSynchronisation = materiels.LastSynchronisation;
                mat.AgentId = materiels.AgentId;
                mat.Version = materiels.Version;
                //mat.IsConfigured = materiels.IsConfigured;
                repository.MaterielsRepository.Update(mat);
                repository.Save();
                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public Tbl_Materiels getMateriels(string serial)
        {
            return repository.MaterielsRepository.Find(m => m.Serial == serial).FirstOrDefault();
        }
        #endregion

        #region GESTION DES SDES
        public Tbl_Sde getSdeDetails(string sdeId)
        {
            return repository.SdeRepository.FindOne(sdeId);
        }

        public bool saveSdeDetails(Tbl_Sde sde)
        {
            try
            {
                repository.SdeRepository.Update(sde);
                repository.Save();
                return true;
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
                Tbl_Sde entity = repository.SdeRepository.Find(s => s.SdeId == sde.SdeId).FirstOrDefault();
                entity.SdeId = sde.SdeId;
                entity.NoOrdre = sde.NoOrdre;
                entity.StatutContreEnquete = sde.StatutContreEnquete.GetValueOrDefault();
                entity.StatutCollecte = sde.StatutCollecte.GetValueOrDefault();
                entity.RaisonCouverture = sde.RaisonCouverture.GetValueOrDefault();
                entity.TotalBatCartographie = sde.TotalBatCartographie.GetValueOrDefault();
                entity.TotalBatRecense = sde.TotalBatRecense.GetValueOrDefault();
                entity.TotalLogeCRecense = sde.TotalLogeCRecense.GetValueOrDefault();
                entity.TotalLogeIRecense = sde.TotalLogeIRecense.GetValueOrDefault();
                entity.TotalMenageRecense = sde.TotalMenageRecense.GetValueOrDefault();
                entity.TotalIndRecense = sde.TotalIndRecense.GetValueOrDefault();
                entity.TotalEmigreRecense = sde.TotalEmigreRecense.GetValueOrDefault();
                entity.TotalDecesRecense = sde.TotalDecesRecense.GetValueOrDefault();
                entity.TotalLogeIRecense = sde.TotalLogeIRecense.GetValueOrDefault();
                repository.SdeRepository.Update(entity);
                repository.Save();
                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }


        public List<Tbl_Sde> searchAllSdes()
        {
            return repository.SdeRepository.Find().ToList();
        }
        #endregion

        #region GESTION DES AGENTS
        public void insertAgent(Tbl_Agent agent)
        {
            try
            {
                if (Utils.IsNotNull(agent))
                {
                    repository.AgentRepository.Insert(agent);
                    repository.Save();
                }
            }
            catch (Exception)
            {

            }
        }

        public void updateAgent(Tbl_Agent agent)
        {
            try
            {
                if (Utils.IsNotNull(agent))
                {
                    Tbl_Agent a = findAgentById(agent.AgentId);
                    a.Cin = a.Cin;
                    a.CodeUtilisateur = agent.CodeUtilisateur;
                    a.Email = agent.Email;
                    a.MotDePasse = agent.MotDePasse;
                    a.Nif = agent.Nif;
                    a.Nom = agent.Nom;
                    a.Prenom = agent.Prenom;
                    a.Sexe = agent.Sexe;
                    a.Telephone = agent.Telephone;
                    repository.AgentRepository.Update(a);
                    repository.Save();
                }
            }
            catch (Exception)
            {

            }
        }

        public Tbl_Agent findAgentById(long agentId)
        {
            return repository.AgentRepository.FindOne(agentId);
        }

        public void deleteAgent(long agentId)
        {
            try
            {
                Tbl_Agent agent = findAgentById(agentId);
                if (Utils.IsNotNull(agent))
                {
                    repository.AgentRepository.Delete(agent);
                }
            }
            catch (Exception)
            {

            }
        }


        public Tbl_Agent findAgent(string sdeId)
        {
            return null;//return repository.AgentRepository.Find(s => s.NumSde == sdeId).FirstOrDefault();
        }


        public Tbl_Sde getSdeByAgent(long agentId)
        {
            try
            {
                return repository.SdeRepository.Find(s => s.AgentId == agentId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Info("Erreur:" + ex.Message);
            }
            return null;
        }


        public List<Tbl_Agent> searchAllAgents()
        {
            try
            {
                return repository.AgentRepository.Find().ToList();
            }
            catch (Exception)
            {

            }
            return null;
        }
        public bool isAgentGotDevice(int agentId)
        {
            try
            {
                Tbl_Materiels matForAgent = repository.MaterielsRepository.Find(m => m.AgentId == agentId).FirstOrDefault();
                if (matForAgent != null)
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        #endregion

        #region GESTION DES RETOURS
        /// <summary>
        /// Enregistrer un retour
        /// </summary>
        /// <param name="retour"></param>
        /// <returns>Tbl_Retour</returns>
        public bool saveRetour(Tbl_Retour retour)
        {
            try
            {
                repository.RetourRepository.Insert(retour);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("==================<> Erreur:DaoSettings/saveRetour:" + ex.Message);
                throw new MessageException(ex.Message);
            }
        }

        public List<Tbl_Retour> searchAllRetours()
        {
            try
            {
                return repository.RetourRepository.Find().ToList();
            }
            catch (Exception ex)
            {
                log.Info("==================<> Erreur:DaoSettings/searchAllRetours:" + ex.Message);
                throw new NotFoundException("PAS DE DONNEES");
            }
        }

        public List<Tbl_Retour> searchAllRetourBySde(string sdeId)
        {
            try
            {
                return repository.RetourRepository.Find(r => r.SdeId == sdeId).ToList();
            }
            catch (Exception ex)
            {
                log.Info("==================<> Erreur:DaoSettings/searchAllRetourBySde:" + ex.Message);
                throw new NotFoundException("PAS DE DONNEES POUR CETTE SDE");
            }
        }

        public Tbl_Retour getRetour(long id)
        {
            try
            {
                return repository.RetourRepository.Find(r => r.RetourId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Info("==================<> Erreur:DaoSettings/getRetour:" + ex.Message);
                throw new NotFoundException("PAS DE DONNEES POUR CETTE SDE");
            }
        }
        #endregion

        #region GESTION DES PROBLEMES POUR LA VERIFICATION

        /// <summary>
        /// Enregistrer un probleme rencontre par l'agent recenseur pour la verification
        /// </summary>
        /// <param name="probleme"><Tbl_Probleme/param>
        /// <returns></returns>
        public bool saveProbleme(Tbl_Probleme probleme)
        {
            try
            {
                repository.ProblemeRepository.Insert(probleme);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                throw new MessageException(ex.Message);
            }
        }

        public bool updateProbleme(Tbl_Probleme probleme)
        {
            try
            {
                Tbl_Probleme probToUpdate = getProblemeByCodeQuestionAndBatiment(probleme.CodeQuestion, probleme.BatimentId.GetValueOrDefault());
                bool result = false;
                if (probToUpdate != null)
                {
                    probToUpdate.Objet = probleme.Objet;
                    probToUpdate.SdeId = probleme.SdeId;
                    probToUpdate.Nature = probleme.Nature;
                    probToUpdate.Statut = probleme.Statut;
                    probToUpdate.CodeQuestion = probleme.CodeQuestion;
                    probToUpdate.Domaine = probleme.Domaine;
                    probToUpdate.BatimentId = probleme.BatimentId;
                    repository.ProblemeRepository.Update(probToUpdate);
                    repository.Save();
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MessageException(ex.Message);
            }
        }

        public bool deleteProbleme(int problemeId)
        {
            try
            {
                Tbl_Probleme probleme = repository.ProblemeRepository.Find(p => p.ProblemeId == problemeId).FirstOrDefault();
                bool result = false;
                if (probleme != null)
                {
                    repository.ProblemeRepository.Delete(probleme);
                    repository.Save();
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new MessageException(ex.Message);
            }
        }

        public List<Tbl_Probleme> searchAllProblemes()
        {
            try
            {
                return repository.ProblemeRepository.Find().ToList();
            }
            catch (Exception ex)
            {
                throw new MessageException(ex.Message);
            }
        }

        public List<Tbl_Probleme> searchAllProblemesBySdeId(string sdeID)
        {
            try
            {
                return repository.ProblemeRepository.Find(p => p.SdeId == sdeID).ToList();
            }
            catch (Exception ex)
            {
                throw new MessageException(ex.Message);
            }
        }

        public Tbl_Probleme getProbleme(int problemeId)
        {
            try
            {
                return repository.ProblemeRepository.Find(p => p.ProblemeId == problemeId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new MessageException(ex.Message);
            }
        }


        public Tbl_Probleme getProblemeByCodeQuestionAndBatiment(string codeQuestion, long batimentId)
        {

            try
            {
                return repository.ProblemeRepository.Find(p => p.CodeQuestion == codeQuestion && p.BatimentId == batimentId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new MessageException(ex.Message);
            }
        }
        #endregion

        public MainRepository getRepository()
        {
            if (repository == null)
            {
                repository = new MainRepository();
                log = new Logger();
            }
            return repository;
        }


        public List<Tbl_Materiels> searchMateriels()
        {
            try
            {
                return repository.MaterielsRepository.Find().ToList();
            }
            catch (Exception)
            {

            }
            return new List<Tbl_Materiels>();
        }
    }
}
