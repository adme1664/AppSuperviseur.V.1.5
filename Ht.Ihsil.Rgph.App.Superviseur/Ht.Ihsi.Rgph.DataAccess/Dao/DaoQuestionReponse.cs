using Ht.Ihsi.Rgph.DataAccess.Repositories;
using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ht.Ihsi.Rgph.Logging.Logs;

namespace Ht.Ihsi.Rgph.DataAccess.Dao
{
   public class DaoQuestionReponse: IDaoQuestionReponse
    {
       private MainRepository repository;
       Logger log;

       #region CONSTRUCTORS
       public DaoQuestionReponse()
       {
           repository = new MainRepository();
       }
       public DaoQuestionReponse(string connectionString)
        {
            repository = new MainRepository(connectionString, true);
            log = new Logger();
        }
       #endregion


       #region METHODS IMPLEMENTATIONS FOR QUESTIONS
       /// <summary>
       /// Rechercher les questions par nom d'objet
       /// </summary>
       /// <param name="NomObjet"></param>
       /// <returns></returns>
       public List<Tbl_Questions> searchQuestion(string NomObjet)
        {
            try
            {
                return repository.QuestionRepository.Find(s => s.NomObjet == NomObjet).ToList();
            }
            catch (Exception ex )
            {
                log.Info("searchQuestion/Error======================<>:" + ex.Message);
            }
            return null;
            
        }


       /// <summary>
       /// Rechercher une question a partir de son code
       /// </summary>
       /// <param name="codeQuestion"></param>
       /// <returns>Tbl_Questions</returns>
        public Tbl_Questions getQuestion(string codeQuestion)
        {
            return repository.QuestionRepository.Find(q => q.CodeQuestion == codeQuestion).FirstOrDefault();
        }


        public Tbl_Reponses getReponse(string codeUniqueReponse)
        {
            return repository.ReponseRepository.FindOne(codeUniqueReponse);
        }



        public List<Tbl_Reponses> searchReponse(string codeReponse)
        {
            return repository.ReponseRepository.Find(r =>r.CodeReponse  == codeReponse).ToList();
        }


        public List<Tbl_Questions_Reponses> searchQuestionReponse(string codeQuestion)
        {
            return repository.QuestionReponseRepository.Find(q => q.CodeQuestion == codeQuestion).ToList();
        }


        public Tbl_CategorieQuestion getCategorieQuestion(string catId)
        {
            return repository.CategorieRepository.Find(s => s.codeCategorie == catId).FirstOrDefault();
        }


        public Tbl_Questions_Reponses getQuestionReponse(string codeUnique)
        {
            return repository.QuestionReponseRepository.Find(r => r.CodeUniqueReponse == codeUnique).FirstOrDefault();
        }


        public Tbl_Questions getQuestionByNomChamps(string nomChamps)
        {
            return repository.QuestionRepository.Find(q => q.NomChamps == nomChamps).FirstOrDefault();
        }


        //public List<Tbl_Utilisation> getUtilisation(string codeCategorie)
        //{
        //    return repository.UtilisationRepository.Find(c => c.CodeCategorie == codeCategorie).ToList();
        //}
        //public Tbl_Utilisation getUtilisation(string code)
        //{
        //    return repository.UtilisationRepository.Find(u => u.CodeUtilisation == code).FirstOrDefault();
        //}


        public List<Tbl_Utilisation> searchtUtilisation(string codeCategorie)
        {
            return repository.UtilisationRepository.Find(c => c.CodeCategorie == codeCategorie).ToList();
        }

        public Tbl_Utilisation getUtilisation(string code)
        {
            return repository.UtilisationRepository.Find(u => u.CodeUtilisation == code).FirstOrDefault();
        }
       #endregion
    }
}
