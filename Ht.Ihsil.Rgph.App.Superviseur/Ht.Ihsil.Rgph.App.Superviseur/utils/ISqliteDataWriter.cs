using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.utils
{
   public interface ISqliteDataWriter
    {
       bool syncroBatimentToServeur(BatimentModel bat);
       bool insertBatiment(tbl_batiment batiment);
       bool updateBatiment(tbl_batiment batiment);
       bool insertLogement(tbl_logement logement);
       bool updateLogement(tbl_logement logement);
       bool insertMenage(tbl_menage men);
       bool updateMenage(tbl_menage men);
       bool insertDeces(DecesModel dec);
       bool updateDeces(DecesModel dec);
       bool insertEmigre(EmigreModel em);
       bool updateEmigre(EmigreModel em);
       bool insertIndividu(IndividuModel ind);
       bool updateIndividu(IndividuModel ind);
       bool validate<T>(T obj,string sdeId);
       bool verified<T>(T obj, string sdeId);
       bool changeStatus<T>(T obj, string sdeId);
       bool contreEnqueteMade<T>(T obj, string sdeId);
       bool savePersonnel(tbl_personnel person);
       bool ifPersonExist(tbl_personnel person);
       bool changeToVerified<T>(T obj, string sdeId,string path);
       bool deleteBatiment(tbl_batiment bat);
       bool deleteMenage(tbl_menage menage);
       bool deleteLogement(tbl_logement logement);
       bool deleteEmigre(tbl_emigre emigre);
       bool deleteDeces(tbl_deces deces);
       bool deleteIndividu(tbl_individu individu);
       bool insertQuestion(string question);
       bool insertQuestionReponse(string questionReponse);
       bool deleteQuestion(string codeQuestion);
       bool deleteQuestionReponse(string codeQuestion);

    }
}
