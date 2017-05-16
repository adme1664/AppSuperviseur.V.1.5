using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Queries
{
    public class Query
    {
        //Query by BATIMENT
        //public static string COUNT_DECES = "SELECT count(*) as total FROM PERSONNEDECEDE ";
        //public static string COUNT_EMIGRE = "SELECT count(*) as total FROM EMIGRE ";
        //public static string COUNT_INDIVIDU = "SELECT count(*) as total FROM INDIVIDU ";
        public static string COUNT_B_MENAGE = "SELECT count(*) as total FROM MENAGE AS M JOIN LOGEMENT AS L ON L.LOGEMENT=M.ID WHERE L.BATIMENT=";
        public static string COUNT_B_LOGEMEENT_I = "SELECT count(*) as total FROM LOGEMENT WHERE  collectif=0 AND BATIMENT=";
        public static string COUNT_B_LOGEMEENT_C = "SELECT count(*) as total FROM LOGEMENT AS L JOIN    WHERE collectif=1 AND BATIMENT=";


        // ============== BLOC QUERY DATA TRANSFERT =====================

        public static string Q_INDIVIDU_C = "SELECT * FROM INDIVIDU WHERE LOGEMENT=";
        public static string Q_COUNT_INDIVIDU_C = "SELECT count(*) as lcount FROM INDIVIDU WHERE LOGEMENT=";


        //QUERY INIDVIDU
        public static string Q_INDIVIDU = "SELECT * FROM INDIVIDU WHERE MENAGE=";
        public static string Q_COUNT_INDIVIDU = "SELECT count(*) as lcount FROM INDIVIDU WHERE MENAGE=";


        //QUERY EMIGRE
        public static string Q_EMIGRE = "SELECT * FROM EMIGRE WHERE MENAGE=";
        public static string Q_COUNT_EMIGRE = "SELECT count(*) as lcount FROM EMIGRE WHERE MENAGE=";


        //QUERY DECES
        public static string Q_DECES = "SELECT * FROM  PERSONNEDECEDE WHERE MENAGE=";
        public static string Q_COUNT_DECES = "SELECT count(*) as lcount FROM  PERSONNEDECEDE WHERE MENAGE=";


        //QUERY MENAGE 
        public static string Q_MENAGE = "SELECT * FROM MENAGE WHERE LOGEMENT=";
        public static string Q_COUNT_MENAGE = "SELECT count(*) as lcount FROM MENAGE WHERE LOGEMENT=";


        //QUERY LOGMENT INDIVIDUEL
        public static string Q_LOGEMEENT_I = "SELECT * FROM LOGEMENT WHERE COLLECTIF=0 AND BATIMENT=";
        public static string Q_COUNT_LOGEMEENT_I = "SELECT count(*) as lcount FROM LOGEMENT WHERE COLLECTIF=0 AND BATIMENT=";

        // QUERY LOGMENT COLLECTIF
        public static string Q_LOGEMEENT_C = "SELECT * FROM LOGEMENT WHERE COLLECTIF=1 AND BATIMENT=";
        public static string Q_COUNT_LOGEMEENT_C = "SELECT count(*) as lcount FROM LOGEMENT WHERE COLLECTIF=1 AND BATIMENT=";

        // QUERY BATIMENT
        public static string Q_BATIMENT = "SELECT * FROM BATIMENT";
        public static string Q_COUNT_BATIMENT = "SELECT count(*) as lcount FROM BATIMENT";
        public static string Q_FORMULAIRE = "SELECT * FROM FORMULAIRE WHERE BATIMENT=";
        public static string Q_BATIMENT_BY_ID = "SELECT * FROM BATIMENT WHERE ID=";







        // ============== BLOC SDE DETAILS=====================

        //QUERY BY SDE - DECES
        public static string COUNT_DECES_F = "SELECT count(*) as total FROM PERSONNEDECEDE WHERE SEXE=2";
        public static string COUNT_DECES_H = "SELECT count(*) as total FROM PERSONNEDECEDE WHERE SEXE=1 ";
        public static string COUNT_DECES = "SELECT count(*) as total FROM PERSONNEDECEDE";


        //QUERY BY SDE - EMIGRE
        public static string COUNT_EMIGRE_F = "SELECT count(*) as total FROM EMIGRE WHERE SEXE=2";
        public static string COUNT_EMIGRE_H = "SELECT count(*) as total FROM EMIGRE WHERE SEXE=1";
        public static string COUNT_EMIGRE = "SELECT count(*) as total FROM EMIGRE ";

        //QUERY BY SDE - INDIVIDU
        public static string COUNT_INDIVIDU_F = "SELECT count(*) as total FROM INDIVIDU WHERE SEXE=2";
        public static string COUNT_INDIVIDU_H = "SELECT count(*) as total FROM INDIVIDU WHERE SEXE=1";
        public static string COUNT_INDIVIDU = "SELECT count(*) as total FROM INDIVIDU ";



        public static string COUNT_MENAGE = "SELECT count(*) as total FROM MENAGE ";
        public static string COUNT_LOGEMEENT_I = "SELECT count(*) as total FROM LOGEMENT WHERE collectif=0";
        public static string COUNT_LOGEMEENT_C = "SELECT count(*) as total FROM LOGEMENT WHERE collectif=1";
        public static string COUNT_BATIMENT= "SELECT COUNT(*) AS total FROM BATIMENT";
    }
}
