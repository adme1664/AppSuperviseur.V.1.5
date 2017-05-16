using Ht.Ihsi.Rgph.DataAccess.Dao;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.services
{
    public class QuestionReponseService:IQuestionReponseService
    {
        private DaoQuestionReponse daoQuestion;
        private Logger log;

        public QuestionReponseService()
        {
            string path = Utilities.getConnectionString(Users.users.SupDatabasePath);
            daoQuestion = new DaoQuestionReponse(path);
            log = new Logger();
        }
        public List<Models.QuestionsModel> searchQuestion(string nomObjet)
        {
            try
            {
                return ModelMapper.MapTolistOfQuestion(daoQuestion.searchQuestion(nomObjet));
            }
            catch (Exception ex)
            {
                log.Info("<>=============================QuestionReponseService/searchQuestion:"+ex.Message);
            }
            return null;
        }


        public Models.QuestionsModel getQuestion(string codeQuestion)
        {
            try
            {
                return ModelMapper.MaptoQuestionModel(daoQuestion.getQuestion(codeQuestion));
            }
            catch (Exception ex)
            {
                log.Info("<>=============================QuestionReponseService/getQuestion:" + ex.Message);
            }
            return null;
        }


        public List<Models.ReponseModel> searchReponse(string codeReponse)
        {
            try
            {
                return ModelMapper.MapToListReponseModel(daoQuestion.searchReponse(codeReponse));
            }
            catch (Exception ex)
            {
                log.Info("<>=============================QuestionReponseService/searchReponse:" + ex.Message);
            }
            return null;
        }

        public Models.ReponseModel getReponse(string codeUnique)
        {
            try
            {
                return ModelMapper.MapToReponseModel(daoQuestion.getReponse(codeUnique));
            }
            catch (Exception ex)
            {
                log.Info("<>=============================QuestionReponseService/getReponse:" + ex.Message);
            }
            return null;
        }


        public List<Models.QuestionReponseModel> searchQuestionReponse(string codeQuestion)
        {
            try
            {
                return ModelMapper.MapToListQuestionReponseModel(daoQuestion.searchQuestionReponse(codeQuestion));
            }
            catch (Exception ex)
            {
                log.Info("<>=============================QuestionReponseService/searchQuestionReponse:" + ex.Message);
            }
            return null;
        }


        public Models.CategorieQuestionModel getCategorieQuestion(string codeCategorie)
        {
            try
            {
                return ModelMapper.MapToCategorieQuestionModel(daoQuestion.getCategorieQuestion(codeCategorie));
            }
            catch (Exception ex)
            {
                log.Info("<>=============================QuestionReponseService/getCategorieQuestion:" + ex.Message);
            }
            return null;
        }


        public Models.QuestionReponseModel getQuestionReponse(string codeUnique)
        {
            try
            {
                return ModelMapper.MapToQuestionReponseModel(daoQuestion.getQuestionReponse(codeUnique));
            }
            catch (Exception ex)
            {
                log.Info("<>=============================QuestionReponseService/getQuestionReponse:" + ex.Message);
            }
            return null;
        }


        public Models.QuestionsModel getQuestionByNomChamps(string nomChamps)
        {
            try
            {
                try
                {
                    return ModelMapper.MaptoQuestionModel(daoQuestion.getQuestionByNomChamps(nomChamps));
                }
                catch (Exception ex)
                {
                    log.Info("<>=============================QuestionReponseService/getQuestionByNomChamps:" + ex.Message);
                }
                
            }
            catch (Exception)
            {

            }
            return null;
        }


        public List<UtilisationModel> searchUtilisation(string codeCategorie)
        {
            try
            {
                return ModelMapper.MapToListUtilisationModel(daoQuestion.searchtUtilisation(codeCategorie));
            }
            catch (Exception)
            {

            }
            return null;
        }


        public UtilisationModel getUtilisation(string code)
        {
            try
            {
                return ModelMapper.MapToUtilisationModel(daoQuestion.getUtilisation(code));
            }
            catch (Exception)
            {

            }
            return null;
        }
    }
}
