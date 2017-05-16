using Ht.Ihsil.Rgph.App.Superviseur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.services
{
    public interface IQuestionReponseService
    {
        List<QuestionsModel> searchQuestion(string nomObjet);
        QuestionsModel getQuestion(string codeQuestion);
        QuestionsModel getQuestionByNomChamps(string nomChamps);
        List<ReponseModel> searchReponse(string codeReponse);
        ReponseModel getReponse(string codeUnique);
        List<QuestionReponseModel> searchQuestionReponse(string codeQuestion);
        QuestionReponseModel getQuestionReponse(string codeUnique);
        CategorieQuestionModel getCategorieQuestion(string codeCategorie);
        List<UtilisationModel> searchUtilisation(string codeCategorie);
        UtilisationModel getUtilisation(string code);

    }
}
