using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.DataAccess.Repositories;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsi.Rgph.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsi.Rgph.DataAccess.Dao
{
    public class DaoUtilisateurs: IDaoUtilisateurs
    {
        private MainRepository repository;
        private Logger log;


        public DaoUtilisateurs()
        {
            repository = new MainRepository();
            log = new Logger();
        }
        public DaoUtilisateurs(string connectionString)
        {
            repository = new MainRepository(connectionString,true);
            log = new Logger();
        }


        public MainRepository getRepository()
        {
            return repository;
        }
         public void insertUser(Tbl_Utilisateur utilisateur)
        {

            try
            {
                if (Utils.IsNotNull(utilisateur))
                {
                    log.Info("<>======Info user:" + utilisateur.Nom);
                    repository.UtilisateurRepository.Insert(utilisateur);
                    repository.SupDatabaseContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Info("<>=================Insert user Error:" + ex.Message);
                log.Info("<>=================Insert user Error:" + ex.InnerException);
            }
        }

         public void updateUser(Tbl_Utilisateur utilisateur)
        {
            try
            {
                if (Utils.IsNotNull(utilisateur))
                {
                    Tbl_Utilisateur u = findUserById(utilisateur.UtilisateurId);
                    u.MotDePasse = utilisateur.MotDePasse;
                    u.Nom = utilisateur.Nom;
                    u.Prenom = utilisateur.Prenom;
                    u.ProfileId = utilisateur.ProfileId;
                    u.Statut = utilisateur.Statut;
                    u.UtilisateurId = utilisateur.UtilisateurId;
                    repository.UtilisateurRepository.Update(u);
                    repository.Save();
                }
            }
            catch (Exception)
            {

            }
        }

        public void deleteUser(long idUser)
        {
            throw new NotImplementedException();
        }


        public Tbl_Utilisateur findUserById(long idUser)
        {
            return repository.UtilisateurRepository.FindOne(idUser);
        }


        public Tbl_Utilisateur findUserByUsername(string username)
        {
            try
            {
                IQueryable<Tbl_Utilisateur> query = repository.UtilisateurRepository.supDatabaseContext.Tbl_Utilisateur;
                query = query.Where(u => u.CodeUtilisateur == username);
                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return null;
        }



        public List<Tbl_Utilisateur> findAllUser()
        {
            throw new NotImplementedException();
        }


        public List<Tbl_Agent> getAllAgentBySup(string supId)
        {
            IQueryable<Tbl_Agent> query = repository.AgentRepository.supDatabaseContext.Tbl_Agent;
            return query.ToList();
            
        }
    }
}
