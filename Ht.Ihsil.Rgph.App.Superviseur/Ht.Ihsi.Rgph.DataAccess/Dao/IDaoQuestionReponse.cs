using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsi.Rgph.DataAccess.Dao
{
   public interface IDaoQuestionReponse
    {
       List<Tbl_Questions> searchQuestion(string NomObjet);
       Tbl_Questions getQuestion(string codeQuestion);
       Tbl_Questions getQuestionByNomChamps(string nomChamps);
       Tbl_Reponses getReponse(string codeUniqueReponse);
       List<Tbl_Reponses> searchReponse(string codeReponse);
       List<Tbl_Questions_Reponses> searchQuestionReponse(string codeQuestion);
       Tbl_Questions_Reponses getQuestionReponse(string codeUnique);
       Tbl_CategorieQuestion getCategorieQuestion(string catId);
       List<Tbl_Utilisation> searchtUtilisation(string codeCategorie);
       Tbl_Utilisation getUtilisation(string code);

    }
}
