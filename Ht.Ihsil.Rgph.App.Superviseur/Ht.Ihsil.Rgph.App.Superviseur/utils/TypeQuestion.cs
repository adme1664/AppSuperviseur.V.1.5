using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.utils
{
    public class TypeQuestion
    {
        public static string Batiment
        {
            get { return Constant.OBJET_BATIMENT; }
        }
        public static string Logement
        {
            get { return Constant.OBJET_LOGEMENT; }
        }
        public static string LogementCollectif
        {
            get { return Constant.OBJET_LOGEMENT; }
        }
        public static string Emigre
        {
            get { return Constant.OBJET_EMIGRE; }
        }
        public static string Menage
        {
            get { return Constant.OBJET_MENAGE; }
        }
        public static string Individu
        {
            get { return Constant.OBJET_INDIVIDU; }
        }
        public static string Deces
        {
            get { return Constant.OBJET_DECES; }
        }
        public static string Evaluation
        {
            get { return Constant.OBJET_EVALUATION; }
        }
    }
}
