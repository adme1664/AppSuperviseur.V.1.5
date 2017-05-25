using DevExpress.Xpf.Editors;
using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Xceed.Wpf.Toolkit;

namespace Ht.Ihsil.Rgph.App.Superviseur.utils
{
    public class Utilities
    {
        #region DECLARATIONS
        private static QuestionsModel questionEnCours;
        private static Thickness thick;
        private static ComboBox comboBox;
        private static TextEdit textbox;
        private static int _anneeRecensement;
        #endregion

        #region PROPERTIES
        public static int AnneeRecensement
        {
            get { return Utilities._anneeRecensement; }
            set { Utilities._anneeRecensement = value; }
        }
        public static TextEdit Textbox
        {
            get { return Utilities.textbox; }
            set { Utilities.textbox = value; }
        }

        public static ComboBox ComboBox
        {
            get { return Utilities.comboBox; }
            set { Utilities.comboBox = value; }
        }

        public static Thickness Thick
        {
            get { return thick; }
            set { thick = value; }
        }
        #endregion

        #region Utilities
        public static SdeInformation getSdeInformation(string sdeId)
        {
            try
            {
                SdeInformation sde = new SdeInformation();
                sde.DeptId = sdeId.Substring(0, 1);
                sde.ComId = sdeId.Substring(0, 3);
                sde.VqseId = sdeId.Substring(0, 6);

                //if (dep.Substring(0, 2) == "00")
                //{
                //    sde.DeptId = dep.Substring(1, 2);
                //}
                //else
                //{
                //    sde.DeptId = dep.Substring(0, 2);
                //}
                return sde;
            }
            catch (Exception)
            {

            }
            return null;
        }
        public static string getGeoInformation(string sdeId)
        {
            try
            {
                SdeInformation sde = getSdeInformation(sdeId);
                ContreEnqueteService service = new ContreEnqueteService();
                string deptNom = service.getDepartement("0" + sde.DeptId).DeptNom;
                if (sde.ComId.Length == 3)
                {
                    sde.ComId = "0" + sde.ComId;
                }
                if (sde.VqseId.Length == 6)
                {
                    sde.VqseId = "0" + sde.VqseId;
                }
                string comNom = service.getCommune(sde.ComId).ComNom;
                string vqseNom = service.getVqse(sde.VqseId).VqseNom;
                return "Depatman: " + deptNom + "/Komin: " + comNom + "/Seksyon Kominal: " + vqseNom;
            }
            catch (Exception)
            {

            }
            return null;

        }
        public static void killProcess(Process[] procs)
        {
            if (procs.Length != 0)
            {
                foreach (var proc in procs)
                {
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }
            }
        }
        /// <summary>
        /// Retourne les noms des objets
        /// </summary>
        /// <returns></returns>
        public static List<NameValue> getNameOfObjects()
        {
            List<NameValue> objects = new List<NameValue>();
            objects.Add(new NameValue("Batiment", "FRM-BAT"));
            objects.Add(new NameValue("Logement Collectif", "FRM-LCO"));
            objects.Add(new NameValue("Logement Individuel", "FRM-LIN"));
            objects.Add(new NameValue("Menage", "FRM-MEN"));
            objects.Add(new NameValue("Emigre", "FRM-EMI"));
            objects.Add(new NameValue("Deces", "FRM-DEC"));
            objects.Add(new NameValue("Individu", "FRM-IND"));
            return objects;
        }

        public static QuestionsModel QuestionEnCours
        {
            get { return questionEnCours; }
            set { questionEnCours = value; }
        }

        
        public static bool pingTheServer(string adrIp)
        {
            try
            {
                var ping = new Ping();
                var reply = ping.Send(adrIp);
                return reply != null && reply.Status == IPStatus.Success;
            }
            catch (Exception)
            {

            }
            return false;
        }

        /// <summary>
        /// Afficher un control dans un grid
        /// </summary>
        /// <param Name="control"></param>
        /// <param Name="grid"></param>
        public static void showControl(Control control, Grid grid)
        {
            //control.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            //control.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            //grid.Children.Clear();
            //grid.Children.Add(control);

            control.Dispatcher.BeginInvoke((Action)(() => control.VerticalAlignment = System.Windows.VerticalAlignment.Stretch));
            control.Dispatcher.BeginInvoke((Action)(() => control.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch));
            grid.Dispatcher.BeginInvoke((Action)(() => grid.Children.Clear()));
            grid.Dispatcher.BeginInvoke((Action)(() => grid.Children.Add(control)));
            

            //control.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            //control.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            //grid.Children.Clear();
            //grid.Children.Add(control);
        }

        public static Thickness getThickness(Thickness t)
        {
            t.Left = 10;
            t.Top = t.Top + 35;
            t.Right = 0;
            t.Bottom = 0;
            return t;
        }
        public static void writeInBusyTool(string message, BusyIndicator busyIndicator, Control control)
        {
            control.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
            {
                busyIndicator.BusyContent = message;

            }, null);
        }
        public static void removeQuestionModel(List<QuestionsModel> listOfQuestionModel, QuestionsModel question)
        {
            try
            {
                foreach (QuestionsModel q in listOfQuestionModel)
                {
                    if (q.CodeQuestion == question.CodeQuestion)
                    {
                        listOfQuestionModel.Remove(q);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static void removeQuestionReponseModel(List<QuestionReponseModel> listOfQuestionsReponse, QuestionReponseModel question)
        {
            try
            {
                foreach (QuestionReponseModel q in listOfQuestionsReponse)
                {
                    if (q.CodeQuestion == question.CodeQuestion)
                    {
                        listOfQuestionsReponse.Remove(q);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static int getDureeSaisie(DateTime start, DateTime end)
        {
            int hourStart = start.Hour;
            int mnsStart = start.Minute;
            int hourEnd = end.Hour;
            int mnsEnd = end.Minute;
            int duree = 0;
            if (hourEnd > hourStart || hourEnd < hourStart)
            {
                duree = (mnsStart - 60) + mnsEnd;
                return duree;
            }
            else
            {
                if (hourEnd == hourStart)
                {
                    if (mnsEnd == mnsStart)
                    {
                        duree = (end.Second - start.Second) / 60;
                    }
                    else
                    {
                        duree = mnsEnd - mnsStart;
                    }
                    return duree;
                }
            }
            return 0;
        }
        public static bool isCategorieExist(List<string> list, string categorie)
        {
            foreach (string cat in list)
            {
                if (cat == categorie)
                {
                    return true;
                }
            }
            return false;
        }

        //Retourne le pourcentage entre 2 nombres
        public static double getPourcentage(int nbre1, int nbre2)
        {
            double percent = 0;
            try
            {
                if (nbre1 == 0 || nbre2 == 0)
                {
                    return percent;
                }
                else
                    percent = (nbre1 * 100) / nbre2;

            }
            catch (Exception)
            {

            }
            return percent;
        }
        #endregion

        #region SET QUESTIONS AND CONTROLS

        #endregion

        #region ConnectionString SQLITE Database
        /// <summary>
        /// get ConnectionString SQLITE Database
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sdeID"></param>
        /// <returns></returns>
        public static string getConnectionString(string path, string sdeID)
        {
            string connectionString = new EntityConnectionStringBuilder
            {
                Metadata = "res://*/Entities.MobileEntities.MobileDataEntities.csdl|res://*/Entities.MobileEntities.MobileDataEntities.ssdl|res://*/Entities.MobileEntities.MobileDataEntities.msl",
                Provider = "System.Data.SQLite.EF6",
                ProviderConnectionString = new SqlConnectionStringBuilder
                {
                    DataSource = path + sdeID + ".SQLITE"
                }.ConnectionString
                

            }.ConnectionString;
            return connectionString;

        }

        /// <summary>
        /// get ConnectionString supervision Database
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string getConnectionString(string path)
        {
            string connectionString = new EntityConnectionStringBuilder
            {
                Metadata = @"res://*/Entities.SupEntities.RgphSup.csdl|res://*/Entities.SupEntities.RgphSup.ssdl|res://*/Entities.SupEntities.RgphSup.msl",
                Provider = "System.Data.SQLite.EF6",
                ProviderConnectionString = new SqlConnectionStringBuilder
                {
                    DataSource = path + Constant.SUPDATABASE_FILE_NAME,
                }.ConnectionString


            }.ConnectionString;
            return connectionString;
        }
        #endregion    

        #region VERIFICATION
        //Effectue la verification de la sde en fonction de certains criteres
        public static List<TableVerificationModel> getVerificatoinNonReponseTotal(string path,string sdeId)
        {
            List<TableVerificationModel> rapports = new List<TableVerificationModel>();
            SqliteDataReaderService service=new SqliteDataReaderService(getConnectionString(path,sdeId));
            List<BatimentModel> batiments = new List<BatimentModel>();
            int lastParentId=0;
            int lastId=0;
            int firstParentId = 0;
            int nbreBatimentPasRempli = service.Sr.GetAllBatimentsInobservables().Count() + service.Sr.GetAllBatimentsWithAtLeastOneBlankObject().Count();
            if (nbreBatimentPasRempli != 0)
            {
                TableVerificationModel report = new TableVerificationModel();
                report.Type = "I.-STATUT DE REMPLISSAGE INITIAL (AR).";
                report.ID = 1;
                report.ParentID = 0;
                report.Indicateur = "Uniquement les questionnaires en premier passage.";
                report.Niveau = "1";
                rapports.Add(report);
                lastId=report.ID;
                firstParentId = report.ID;

                report = new TableVerificationModel();
                report.Type = "1-NOMBRE QUESTIONNAIRES PAS DU TOUT REMPLIS ";
                report.Indicateur = "";
                report.Total = ""+nbreBatimentPasRempli;
                report.ID = lastId + 1;
                report.ParentID = lastId;
                report.Niveau = "2";
                rapports.Add(report);
                lastId = report.ID;
                lastParentId = report.ID;
                batiments = service.Sr.GetAllBatimentsInobservables().ToList();
                foreach (BatimentModel bat in service.Sr.GetAllBatimentsWithAtLeastOneBlankObject())
                {
                    batiments.Add(bat);
                }
                foreach (BatimentModel batiment in batiments)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + batiment.BatimentId+" /REC-"+batiment.Qrec;
                    report.ID = lastId + 1;
                    report.ParentID = lastParentId;
                    report.Niveau = "3";
                    rapports.Add(report);
                    lastId = report.ID;
                }
                //Branche pour les batiments inobservables
                lastParentId = lastId;
                report = new TableVerificationModel();
                report.Type = "2- BÂTIMENTS INOBSERVABLES / TAUX DE NON-RÉPONSE TOTALE NRT 1 (%)";
                report.ID = lastId+1;
                report.ParentID = firstParentId;
                report.Niveau = "2";
                report.Indicateur = "BÂTIMENTS INOBSERVABLES (B1=5)";
                batiments = new List<BatimentModel>();
                batiments = service.Sr.GetAllBatimentsInobservables().ToList();
                report.Total = "" + batiments.Count;
                report.Taux = "" + getPourcentage(batiments.Count(), service.getAllBatiments().ToList().Count())+"%";
                rapports.Add(report);
                lastId = report.ID;
                lastParentId = report.ID;
                foreach (BatimentModel batiment in batiments)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + batiment.BatimentId + " /REC-" + batiment.Qrec;
                    report.ID = lastId + 1;
                    report.ParentID = lastParentId;
                    report.Niveau = "3";
                    rapports.Add(report);
                    lastId = report.ID + 1;
                }
                //Branche pour les batiments ayant au moins un logement pas rempli
                lastParentId = lastId;
                report = new TableVerificationModel();
                report.Type = "3-TAUX DE NON-RÉPONSE TOTALE (%)";
                report.ID = lastId+1;
                report.Niveau = "2";
                report.ParentID = firstParentId;
                report.Indicateur = "Objet Logement pas rempli du tout";
                batiments = new List<BatimentModel>();
                batiments = service.Sr.GetAllBatimentsWithAtLeastOneBlankObject().ToList();
                report.Total = "" + batiments.Count;
                report.Taux = "" + getPourcentage(batiments.Count(), service.getAllBatiments().ToList().Count()) + "%";
                rapports.Add(report);
                lastId = report.ID;
                lastParentId = report.ID;
                foreach (BatimentModel batiment in batiments)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + batiment.BatimentId + " /REC-" + batiment.Qrec;
                    report.ID = lastId + 1;
                    report.ParentID = lastParentId;
                    report.Niveau = "3";
                    rapports.Add(report);
                    lastId = report.ID + 1;
                }
                //Bracnhe Distribution des questonnaires selon la raison indiquee dans le rapport de l'agent recenseuur
                List<RapportArModel> rapportsAgentsRecenseurs = new List<RapportArModel>();
                lastParentId = lastId;
                report = new TableVerificationModel();
                report.Type = "4-DISTRIBUTION DES QUESTIONNAIRES EN NRT2 SELON LA RAISON (%)";
                report.ID = lastId+1;
                report.ParentID = firstParentId;
                report.Niveau = "2";
                report.Indicateur = "";
                rapportsAgentsRecenseurs = service.Sr.GetAllRptAgentRecenseurForNotFinishedObject().ToList();
                report.Total = "" + rapportsAgentsRecenseurs.Count;
                rapports.Add(report);
                lastId = report.ID;
                lastParentId = report.ID;
                //Ensemble des batiments inobservables et au moins un objet vide
                batiments = service.Sr.GetAllBatimentsInobservables().ToList();
                foreach (BatimentModel bat in service.Sr.GetAllBatimentsWithAtLeastOneBlankObject())
                {
                    batiments.Add(bat);
                }
                //Nombre de refus total
                int nonbreRefusTotal = 0;
                List<BatimentModel> batimentsEnRefus = new List<BatimentModel>();
                string raisonRefus = "";

                //Nombre Indisponibilité avec rendez-vous
                int NbreIndAvecRendezVous = 0;
                List<BatimentModel> batimentsEnIndAvecRendezVous = new List<BatimentModel>();
                string raisonAvecRendezVous = "";

                //Indisponibilite
                int nbreIndisponible = 0;
                List<BatimentModel> batimentsIndisponible = new List<BatimentModel>();
                string raisonIndisponible = "";

                //Autre
                int nbreAutre = 0;
                List<BatimentModel> batimentsEnAutre = new List<BatimentModel>();
                string raisonAutre = "";

                foreach (BatimentModel batiment in batiments)
                {
                    //On recherche les rapports AR par batiment et on fait le total
                    List<RapportArModel> rars = service.Sr.GetAllRptAgentRecenseurByBatiment(batiment.BatimentId);
                    if (rars != null)
                    {
                        foreach (RapportArModel rar in rars)
                        {
                            //Indisponibilité avec rendez-vous raison=17
                            if (rar.RaisonActionId == 17)
                            {
                                NbreIndAvecRendezVous++;
                                batimentsEnIndAvecRendezVous.Add(batiment);
                                raisonAvecRendezVous = Constant.getRaison(rar.RaisonActionId).Value;
                            }
                            //Refus
                            if (rar.RaisonActionId == 16)
                            {
                                nonbreRefusTotal++;
                                batimentsEnRefus.Add(batiment);
                                raisonRefus = Constant.getRaison(rar.RaisonActionId).Value;
                            }
                            //Indisponibilité
                            if (rar.RaisonActionId == 18)
                            {
                                nbreIndisponible++;
                                batimentsIndisponible.Add(batiment);
                                raisonIndisponible = Constant.getRaison(rar.RaisonActionId).Value;
                            }
                            //Autre
                            if (rar.RaisonActionId == 19)
                            {
                                nbreAutre++;
                                batimentsEnAutre.Add(batiment);
                                raisonAutre = rar.AutreRaisonAction;
                            }
                        }

                    }
               }
                //On definit les parents parents des bracnhes refus, indisponible etc...
                int refusParent = 0;
                int indAvecRDParent = 0;
                int indParent = 0;
                int autreParent = 0;

                //On ajoute la branche REFUS avec le total
                report = new TableVerificationModel();
                report.Type = "Refus";
                report.ID = lastId + 1;
                refusParent = report.ID;
                report.Indicateur = raisonRefus;
                report.ParentID = lastParentId;
                report.Total = "" + nonbreRefusTotal;
                rapports.Add(report);
                lastId = report.ID ;
               //On ajoute les batiments dans la branche
                foreach (BatimentModel batiment in batimentsEnRefus)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + batiment.BatimentId + " /REC-" + batiment.Qrec;
                    report.ID = lastId + 1;
                    report.ParentID = refusParent;
                    report.Niveau = "3";
                    rapports.Add(report);
                    lastId = report.ID + 1;
                }

                //On ajoute la branche Indisponibilité avec rendez-vous
                report = new TableVerificationModel();
                report.Type = "Indisponibilité avec rendez-vous";
                report.ID = lastId + 1;
                indAvecRDParent = report.ID;
                report.Indicateur = raisonAvecRendezVous;
                report.ParentID = lastParentId;
                report.Total = "" + NbreIndAvecRendezVous;
                rapports.Add(report);
                lastId = report.ID;
                //On ajoute les batiments dans la branche
                foreach (BatimentModel batiment in batimentsEnIndAvecRendezVous)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + batiment.BatimentId + " /REC-" + batiment.Qrec;
                    report.ID = lastId + 1;
                    report.ParentID = indAvecRDParent;
                    report.Niveau = "3";
                    rapports.Add(report);
                    lastId = report.ID;
                }

                //On ajoute la branche Indisponibilité
                report = new TableVerificationModel();
                report.Type = "Indisponibilité";
                report.ID = lastId + 1;
                indParent = report.ID;
                report.Indicateur = raisonIndisponible;
                report.ParentID = lastParentId;
                report.Total = "" + nbreIndisponible;
                rapports.Add(report);
                lastId = report.ID;
                //On ajoute les batiments dans la branche
                foreach (BatimentModel batiment in batimentsIndisponible)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + batiment.BatimentId + " /REC-" + batiment.Qrec;
                    report.ID = lastId + 1;
                    report.ParentID = indParent;
                    report.Niveau = "3";
                    rapports.Add(report);
                    lastId = report.ID;
                }
                //On ajoute la branche Autre
                report = new TableVerificationModel();
                report.Type = "Autre";
                report.ID = lastId + 1;
                autreParent = report.ID;
                report.Indicateur = raisonAutre;
                report.ParentID = lastParentId;
                report.Total = "" + nbreAutre;
                rapports.Add(report);
                lastId = report.ID;
                //On ajoute les batiments dans la branche
                foreach (BatimentModel batiment in batimentsEnAutre)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + batiment.BatimentId + " /REC-" + batiment.Qrec;
                    report.ID = lastId + 1;
                    report.ParentID = autreParent;
                    report.Niveau = "3";
                    rapports.Add(report);
                    lastId = report.ID;
                }          
           }
            return rapports;
        }
        public static List<TableVerificationModel> getVerificatoinNonReponseTotalForAllSdes(string path)
        {
            List<TableVerificationModel> rapports = new List<TableVerificationModel>();
            IConfigurationService configurationService = new ConfigurationService();
            List<SdeModel> listOfSdes = configurationService.searchAllSdes();

            #region CONTRUCTION DU RAPPORT                       
            List<BatimentModel> batiments = new List<BatimentModel>();
            int lastParentId = 0;
            int lastId = 0;
            int firstParentId = 0;
            int nbreBatimentPasRempli = 0;
            SqliteDataReaderService sqliteService = null;

            #region BRANCHE BATIMENTS MAL REMPLI
            //
            //Somme des batiments mal rempli pour le district du superviseur
            foreach (SdeModel sde in listOfSdes)
            {
                 sqliteService = new SqliteDataReaderService(getConnectionString(path, sde.SdeId));
                 nbreBatimentPasRempli = nbreBatimentPasRempli + (sqliteService.Sr.GetAllBatimentsInobservables().Count() + sqliteService.Sr.GetAllBatimentsWithAtLeastOneBlankObject().Count());
            }
            if (nbreBatimentPasRempli != 0)
            {
                TableVerificationModel report = new TableVerificationModel();
                report.Type = "I.-STATUT DE REMPLISSAGE INITIAL (AR).";
                report.ID = 1;
                report.ParentID = 0;
                report.Indicateur = "Uniquement les questionnaires en premier passage.";
                report.Niveau = "1";
                rapports.Add(report);
                lastId = report.ID;
                firstParentId = report.ID;

                report = new TableVerificationModel();
                int batimentPasRempliId=0;
                report.Type = "1-NOMBRE QUESTIONNAIRES PAS DU TOUT REMPLIS ";
                report.Indicateur = "";
                report.Total = "" + nbreBatimentPasRempli;
                report.ID = lastId + 1;
                report.ParentID = lastId;
                report.Niveau = "2";
                rapports.Add(report);
                lastId = report.ID;
                batimentPasRempliId= report.ID;
                lastParentId = report.ID;
                //
                //Compilation des batiments sur l'ensemble des Sdes
                foreach (SdeModel sde in listOfSdes)
                {
                    sqliteService = new SqliteDataReaderService(getConnectionString(path, sde.SdeId));
                    batiments = sqliteService.Sr.GetAllBatimentsInobservables().ToList();
                    foreach (BatimentModel bat in sqliteService.Sr.GetAllBatimentsWithAtLeastOneBlankObject())
                    {
                        batiments.Add(bat);
                    }
                    //
                    //Ajout de la branche SDE pour identifier dans quelle Sde se trouve les batiments concernes
                    report = new TableVerificationModel();
                    report.Type = "" + sde.SdeId;
                    //report.Image = "/images/database.png";
                    report.ID = lastId + 1;
                    report.Niveau = "3";
                    report.ParentID = batimentPasRempliId;
                    report.Total = "" + batiments.Count();
                    lastId = report.ID;
                    lastParentId = report.ID;
                    rapports.Add(report);
                    //
                    //On ajoute les batiments dans La SDE 
                    foreach (BatimentModel batiment in batiments)
                    {
                        report = new TableVerificationModel();
                        report.Type = "Batiman-" + batiment.BatimentId + " /REC-" + batiment.Qrec;
                        report.ID = lastId + 1;
                        report.ParentID = lastParentId;
                        report.Niveau = "4";
                        rapports.Add(report);
                        lastId = report.ID;
                    }
                }
            #endregion

            #region BRANCHE BATIMENTS INOBSERVABLES
                //Branche pour les batiments inobservables
                int nbreBatimentInobservables = 0;
                int nbreTotalBatimentDistrict = 0;
                foreach (SdeModel sde in listOfSdes)
                {
                    sqliteService = new SqliteDataReaderService(getConnectionString(path, sde.SdeId));
                    nbreBatimentInobservables = nbreBatimentInobservables + sqliteService.Sr.GetAllBatimentsInobservables().ToList().Count();
                    nbreTotalBatimentDistrict = nbreTotalBatimentDistrict + sqliteService.getAllBatiments().ToList().Count();
                }
                lastParentId = lastId;
                int batimentInobservableId = 0;
                report = new TableVerificationModel();
                report.Type = "2- BÂTIMENTS INOBSERVABLES / TAUX DE NON-RÉPONSE TOTALE NRT 1 (%)";
                report.ID = lastId+1;
                report.ParentID = firstParentId;
                report.Indicateur = "BÂTIMENTS INOBSERVABLES (B1=5)";
                report.Total = "" + nbreBatimentInobservables;
                report.Taux = "" + getPourcentage(nbreBatimentInobservables, nbreTotalBatimentDistrict) + "%";
                report.Niveau = "2";
                rapports.Add(report);
                lastId = report.ID;
                batimentInobservableId = report.ID;
                lastParentId = report.ID;
                foreach (SdeModel sde in listOfSdes)
                {
                    sqliteService = new SqliteDataReaderService(getConnectionString(path, sde.SdeId));
                    batiments = new List<BatimentModel>();
                    batiments = sqliteService.Sr.GetAllBatimentsInobservables().ToList();
                    //
                    //Ajout de la branche SDE pour identifier dans quelle Sde se trouve les batiments concernes
                    report = new TableVerificationModel();
                    report.Type = "" + sde.SdeId;
                    report.ID = lastId + 1;
                    report.ParentID = batimentInobservableId;
                    report.Total = ""+batiments.Count();
                    lastId = report.ID;
                    lastParentId = report.ID;
                    report.Niveau = "3";
                    rapports.Add(report);
                    //
                    foreach (BatimentModel batiment in batiments)
                    {
                        report = new TableVerificationModel();
                        report.Type =  "Batiman-" + batiment.BatimentId + " /REC-" + batiment.Qrec;
                        report.ID = lastId + 1;
                        report.ParentID = lastParentId;
                        report.Niveau = "4";
                        rapports.Add(report);
                        lastId = report.ID;
                    }
                }
                #endregion

            #region BRANCHE BATIMENT AYANT UN OBJET VIDE
                //
                //Branche pour les batiments ayant au moins un logement pas rempli
                lastParentId = lastId;
                int nbreBatimentObjetVide = 0;
                int BatimentObjetVideId = 0;
                foreach (SdeModel sde in listOfSdes)
                {
                    sqliteService = new SqliteDataReaderService(getConnectionString(path, sde.SdeId));
                    nbreBatimentObjetVide = nbreBatimentObjetVide + sqliteService.Sr.GetAllBatimentsWithAtLeastOneBlankObject().ToList().Count();
                    nbreTotalBatimentDistrict = nbreTotalBatimentDistrict + sqliteService.getAllBatiments().ToList().Count();
                }
                report = new TableVerificationModel();
                report.Type = "3-TAUX DE NON-RÉPONSE TOTALE (%)";
                report.ID = lastId+1;
                BatimentObjetVideId = report.ID;
                report.ParentID = firstParentId;
                report.Indicateur = "Objet Logement pas rempli du tout";
                report.Total = "" + nbreBatimentObjetVide;
                report.Taux = "" + getPourcentage(nbreBatimentObjetVide, nbreTotalBatimentDistrict) + "%";
                report.Niveau = "2";
                rapports.Add(report);
                lastId = report.ID;
                lastParentId = report.ID;
                //
                foreach (SdeModel sde in listOfSdes)
                {
                    sqliteService = new SqliteDataReaderService(getConnectionString(path, sde.SdeId));
                    batiments = new List<BatimentModel>();
                    batiments = sqliteService.Sr.GetAllBatimentsWithAtLeastOneBlankObject().ToList();
                    //
                    //Ajout de la branche SDE pour identifier dans quelle Sde se trouve les batiments concernes
                    report = new TableVerificationModel();
                    report.Type = "" + sde.SdeId;
                    report.ID = lastId + 1;
                    report.ParentID = BatimentObjetVideId;
                    report.Total = "" + batiments.Count();
                    lastId = report.ID;
                    lastParentId = report.ID;
                    report.Niveau = "3";
                    rapports.Add(report);
                    //

                    foreach (BatimentModel batiment in batiments)
                    {
                        report = new TableVerificationModel();
                        report.Type = "Batiman-" + batiment.BatimentId + " /REC-" + batiment.Qrec;
                        report.ID = lastId + 1;
                        report.ParentID = lastParentId;
                        report.Niveau = "4";
                        rapports.Add(report);
                        lastId = report.ID;
                    }
                }
                #endregion

            #region BRANCHE BATIMENT AVEC RAISON
                //
                //Bracnhe Distribution des questonnaires selon la raison indiquee dans le rapport de l'agent recenseuur
                List<RapportArModel> rapportsAgentsRecenseurs = new List<RapportArModel>();
                int nbreBatimentInobservableEtVide = 0;
                int sdeParentId = 0;
                int BatimentInobservableEtVideParentId = 0;
                batiments = new List<BatimentModel>();
                foreach (SdeModel sde in listOfSdes)
                {
                    sqliteService = new SqliteDataReaderService(getConnectionString(path, sde.SdeId));
                    nbreBatimentInobservableEtVide = nbreBatimentInobservableEtVide + sqliteService.Sr.GetAllRptAgentRecenseurForNotFinishedObject().ToList().Count();
                    ////Ensemble des batiments inobservables et au moins un objet vide
                    //batiments = sqliteService.Sr.GetAllBatimentsInobservables().ToList();
                    //foreach (BatimentModel bat in sqliteService.Sr.GetAllBatimentsWithAtLeastOneBlankObject())
                    //{
                    //    batiments.Add(bat);
                    //}
                    ////
                }
                lastParentId = lastId;
                report = new TableVerificationModel();
                report.Type = "4-DISTRIBUTION DES QUESTIONNAIRES EN NRT2 SELON LA RAISON (%)";
                report.ID = lastId+1;
                BatimentInobservableEtVideParentId = report.ID;
                report.ParentID = firstParentId;
                report.Indicateur = "";
                report.Total = "" + nbreBatimentInobservableEtVide;
                report.Niveau = "2";
                rapports.Add(report);
                lastId = report.ID;
                lastParentId = report.ID;

                foreach (SdeModel sde in listOfSdes)
                {
                    sqliteService = new SqliteDataReaderService(getConnectionString(path, sde.SdeId));
                    batiments = new List<BatimentModel>();
                    int BatimentInobservableEtVideParSde = sqliteService.Sr.GetAllRptAgentRecenseurForNotFinishedObject().ToList().Count();
                    //
                    //Ajout de la branche SDE pour identifier dans quelle Sde se trouve les batiments concernes
                    report = new TableVerificationModel();
                    report.Type = "" + sde.SdeId;
                    report.ID = lastId + 1;
                    sdeParentId = report.ID;
                    report.ParentID = BatimentInobservableEtVideParentId;
                    report.Total = "" + BatimentInobservableEtVideParSde;
                    lastId = report.ID;
                    lastParentId = report.ID;
                    report.Niveau = "3";
                    rapports.Add(report);

                    //////////////////////
                    //Ensemble des batiments inobservables et au moins un objet vide
                    batiments = sqliteService.Sr.GetAllBatimentsInobservables().ToList();
                    foreach (BatimentModel bat in sqliteService.Sr.GetAllBatimentsWithAtLeastOneBlankObject())
                    {
                        batiments.Add(bat);
                    }
                    //Nombre de refus total
                    int nonbreRefusTotal = 0;
                    List<BatimentModel> batimentsEnRefus = new List<BatimentModel>();
                    string raisonRefus = "";

                    //Nombre Indisponibilité avec rendez-vous
                    int NbreIndAvecRendezVous = 0;
                    List<BatimentModel> batimentsEnIndAvecRendezVous = new List<BatimentModel>();
                    string raisonAvecRendezVous = "";

                    //Indisponibilite
                    int nbreIndisponible = 0;
                    List<BatimentModel> batimentsIndisponible = new List<BatimentModel>();
                    string raisonIndisponible = "";

                    //Autre
                    int nbreAutre = 0;
                    List<BatimentModel> batimentsEnAutre = new List<BatimentModel>();
                    string raisonAutre = "";

                    foreach (BatimentModel batiment in batiments)
                    {
                        //On recherche les rapports AR par batiment et on fait le total
                        List<RapportArModel> rars = sqliteService.Sr.GetAllRptAgentRecenseurByBatiment(batiment.BatimentId);
                        if (rars != null)
                        {
                            foreach (RapportArModel rar in rars)
                            {
                                //Indisponibilité avec rendez-vous raison=17
                                if (rar.RaisonActionId == 17)
                                {
                                    NbreIndAvecRendezVous++;
                                    batimentsEnIndAvecRendezVous.Add(batiment);
                                    raisonAvecRendezVous = Constant.getRaison(rar.RaisonActionId).Value;
                                }
                                //Refus
                                if (rar.RaisonActionId == 16)
                                {
                                    nonbreRefusTotal++;
                                    batimentsEnRefus.Add(batiment);
                                    raisonRefus = Constant.getRaison(rar.RaisonActionId).Value;
                                }
                                //Indisponibilité
                                if (rar.RaisonActionId == 18)
                                {
                                    nbreIndisponible++;
                                    batimentsIndisponible.Add(batiment);
                                    raisonIndisponible = Constant.getRaison(rar.RaisonActionId).Value;
                                }
                                //Autre
                                if (rar.RaisonActionId == 19)
                                {
                                    nbreAutre++;
                                    batimentsEnAutre.Add(batiment);
                                    raisonAutre = rar.AutreRaisonAction;
                                }
                            }

                        }
                    }
                    //On definit les parents parents des bracnhes refus, indisponible etc...
                    int refusParent = 0;
                    int indAvecRDParent = 0;
                    int indParent = 0;
                    int autreParent = 0;

                    //On ajoute la branche REFUS avec le total
                    report = new TableVerificationModel();
                    report.Type = "Refus";
                    report.ID = lastId + 1;
                    refusParent = report.ID;
                    report.Indicateur = raisonRefus;
                    report.ParentID = sdeParentId;
                    report.Total = "" + nonbreRefusTotal;
                    rapports.Add(report);
                    lastId = report.ID;
                    //On ajoute les batiments dans la branche
                    foreach (BatimentModel batiment in batimentsEnRefus)
                    {
                        report = new TableVerificationModel();
                        report.Type = "Batiman-" + batiment.BatimentId + " /REC-" + batiment.Qrec;
                        report.ID = lastId + 1;
                        report.ParentID = refusParent;
                        report.Niveau = "4";
                        rapports.Add(report);
                        lastId = report.ID;
                    }

                    //On ajoute la branche Indisponibilité avec rendez-vous
                    report = new TableVerificationModel();
                    report.Type = "Indisponibilité avec rendez-vous";
                    report.ID = lastId + 1;
                    indAvecRDParent = report.ID;
                    report.Indicateur = raisonAvecRendezVous;
                    report.ParentID = sdeParentId;
                    report.Total = "" + NbreIndAvecRendezVous;
                    rapports.Add(report);
                    lastId = report.ID;
                    //On ajoute les batiments dans la branche
                    foreach (BatimentModel batiment in batimentsEnIndAvecRendezVous)
                    {
                        report = new TableVerificationModel();
                        report.Type = "Batiman-" + batiment.BatimentId + " /REC-" + batiment.Qrec;
                        report.ID = lastId + 1;
                        report.ParentID = indAvecRDParent;
                        report.Niveau = "4";
                        rapports.Add(report);
                        lastId = report.ID;
                    }

                    //On ajoute la branche Indisponibilité
                    report = new TableVerificationModel();
                    report.Type = "Indisponibilité";
                    report.ID = lastId + 1;
                    indParent = report.ID;
                    report.Indicateur = raisonIndisponible;
                    report.ParentID = sdeParentId;
                    report.Total = "" + nbreIndisponible;
                    rapports.Add(report);
                    lastId = report.ID;
                    //On ajoute les batiments dans la branche
                    foreach (BatimentModel batiment in batimentsIndisponible)
                    {
                        report = new TableVerificationModel();
                        report.Type = "Batiman-" + batiment.BatimentId + " /REC-" + batiment.Qrec;
                        report.ID = lastId + 1;
                        report.ParentID = indParent;
                        report.Niveau = "4";
                        rapports.Add(report);
                        lastId = report.ID ;
                    }
                    //On ajoute la branche Autre
                    report = new TableVerificationModel();
                    report.Type = "Autre";
                    report.ID = lastId + 1;
                    autreParent = report.ID;
                    report.Indicateur = raisonAutre;
                    report.ParentID = sdeParentId;
                    report.Total = "" + nbreAutre;
                    rapports.Add(report);
                    lastId = report.ID;
                    //On ajoute les batiments dans la branche
                    foreach (BatimentModel batiment in batimentsEnAutre)
                    {
                        report = new TableVerificationModel();
                        report.Type = "Batiman-" + batiment.BatimentId + " /REC-" + batiment.Qrec;
                        report.ID = lastId + 1;
                        report.ParentID = autreParent;
                        report.Niveau = "4";
                        rapports.Add(report);
                        lastId = report.ID;
                    }
                    ////////////////////////
                }
#endregion
             
            }
            #endregion
            return rapports;
        }
        public static List<TableVerificationModel> getVerificationNonReponsePartielle(string path, string sdeId)
        {
            List<TableVerificationModel> rapports = new List<TableVerificationModel>();
            SqliteDataReaderService service = new SqliteDataReaderService(getConnectionString(path, sdeId));
            int lastParentId = 0;
            int lastId = 0;
            //Ajout des nomres de logements, menages et individus remplis partiellement
            int nbreTotalLogement = service.Sr.GetAllLogements().Count();
            int nbreTotalMenages = service.Sr.GetAllMenages().Count();
            int nbreTotalIndividus = service.Sr.GetAllIndividus().Count();

            //Ajout des ID  parents 
            int parent_0 = 0;
            int parent_1 = 0;
            int parent_2 = 0;
            #region STATUT DE REMPLISSAGE INITIAL
            //Ajout de la branche STATUT DE REMPLISSAGE INITIAL
            TableVerificationModel report = new TableVerificationModel();
            report.Type = "STATUT DE REMPLISSAGE INITIAL";
            report.Indicateur = "";
            report.ParentID = 0;
            report.ID = 1;
            report.Niveau = "1";
            lastId = report.ID;
            lastParentId = report.ID;
            parent_0 = lastParentId;
            rapports.Add(report);

            #region NOMBRE QUESTIONNAIRES PARTIELLEMENT REMPLIS
            //Ajout de l'entete dans le rapport de verification
            report = new TableVerificationModel();
            report.Type = "I-NOMBRE QUESTIONNAIRES PARTIELLEMENT REMPLIS";
            report.Indicateur = "";
            report.ParentID = lastParentId;
            report.ID = lastId+1;
            parent_1 = report.ID;
            report.Niveau = "2";
            report.Total = "" + (service.Sr.GetAllLogementIndNotFinish().Count() + service.Sr.GetAllMenageNotFinish().Count() + service.Sr.GetAllIndividuNotFinish().Count());
            report.Taux = "" + getPourcentage(Convert.ToInt32(report.Total), (nbreTotalIndividus + nbreTotalLogement + nbreTotalMenages)) + "%";
            lastId = report.ID;
            lastParentId = report.ID;
            rapports.Add(report);
            //
           

            //Ajout de la branche logement
            List<LogementModel> logementsPartiellesRemplis=service.Sr.GetAllLogementIndNotFinish();
            int nbreLogementTotalPR=logementsPartiellesRemplis.Count();
            report = new TableVerificationModel();
            report.ID = lastId + 1;
            report.ParentID = parent_1;
            report.Niveau = "3";
            report.Type = "Nombre de logements individuels";
            report.Total = ""+logementsPartiellesRemplis.Count();
            report.Taux = "" + getPourcentage(nbreLogementTotalPR, nbreTotalLogement)+"%";
            rapports.Add(report);
            lastId = report.ID;
            lastParentId = report.ID;
            //Ajout des logements a l'interieur dans la branche logements
            if (nbreLogementTotalPR != 0)
            {
                foreach (LogementModel logement in logementsPartiellesRemplis)
                {
                    report = new TableVerificationModel();
                    report.ID = lastId + 1;
                    report.Type = "Batiman-" + logement.BatimentId + "/Lojman-" + logement.LogeId;
                    report.ParentID = lastParentId;
                    report.Niveau = "4";
                    rapports.Add(report);
                    lastId = report.ID;
                }
            }
            //
            //Ajout de la branche Menage
            List<MenageModel> menagesPartiellesRemplis = service.Sr.GetAllMenageNotFinish();
            int nbreMenagesPartiellesRemplis = menagesPartiellesRemplis.Count();
            report = new TableVerificationModel();
            report.ID = lastId + 1;
            report.ParentID = parent_1;
            report.Type = "Nombre de menages";
            report.Total = "" + nbreMenagesPartiellesRemplis;
            report.Taux = "" + getPourcentage(nbreMenagesPartiellesRemplis, nbreTotalMenages) + "%";
            report.Niveau = "5";
            rapports.Add(report);
            lastId = report.ID;
            lastParentId = report.ID;
            //Ajout des menages a l'interieur dans la branche Menage
            if (nbreLogementTotalPR != 0)
            {
                foreach (MenageModel men in menagesPartiellesRemplis)
                {
                    report = new TableVerificationModel();
                    report.ID = lastId + 1;
                    report.Type = "Batiman-" + men.BatimentId + "/Lojman-" + men.LogeId+"/Menaj-"+men.MenageId;
                    report.ParentID = lastParentId;
                    report.Niveau = "";
                    rapports.Add(report);
                    lastId = report.ID;
                }
            }
            //
            //Ajout de la branche INDIVIDUS
            List<IndividuModel> individusPartiellesRemplis = service.Sr.GetAllIndividuNotFinish();
            int nbreindividusPartiellesRemplis = individusPartiellesRemplis.Count();
            report = new TableVerificationModel();
            report.ID = lastId + 1;
            report.ParentID = parent_1;
            report.Type = "Nombre d'individus";
            report.Total = "" + nbreindividusPartiellesRemplis;
            report.Taux = "" + getPourcentage(nbreindividusPartiellesRemplis, nbreTotalMenages) + "%";
            report.Niveau = "6";
            rapports.Add(report);
            lastId = report.ID;
            lastParentId = report.ID;
            //Ajout des menages a l'interieur dans la branche Menage
            if (nbreLogementTotalPR != 0)
            {
                foreach (IndividuModel ind in individusPartiellesRemplis)
                {
                    report = new TableVerificationModel();
                    report.ID = lastId + 1;
                    report.Type = "Batiman-" + ind.BatimentId + "/Lojman-" + ind.LogeId + "/Menaj-" + ind.MenageId+"/Envidivi-"+ind.IndividuId;
                    report.ParentID = lastParentId;
                    report.Niveau = "6";
                    rapports.Add(report);
                    lastId = report.ID;
               }
            }
            #endregion

            #region NOMBRE DE LOGEMENTS OCCUPÉS AVEC OCCUPANTS ABSENTS
            //Ajout de la branche principale
            report = new TableVerificationModel();
            List<LogementModel> logementsOccupantAbsent = service.Sr.GetAllLogementOccupantAbsent();
            int nbreLogOccupantAbsent = logementsOccupantAbsent.Count();
            report.Type = "2-NOMBRE DE LOGEMENTS OCCUPÉS AVEC OCCUPANTS ABSENTS";
            report.ParentID = 1;
            report.ID = lastId + 1;
            parent_2 = report.ID;
            report.Niveau = "2";
            report.Total = "" + nbreLogOccupantAbsent;
            report.Taux = "" + getPourcentage(nbreLogOccupantAbsent, nbreTotalLogement);
            lastId = report.ID;
            lastParentId = report.ID;
            rapports.Add(report);
            //
            //Ajout des branches enfants 
            if (nbreLogOccupantAbsent != 0)
            {
                foreach (LogementModel logement in logementsOccupantAbsent)
                {
                    report = new TableVerificationModel();
                    report.ID = lastId + 1;
                    report.Type = "Batiman-" + logement.BatimentId + "/Lojman-" + logement.LogeId;
                    report.ParentID = lastParentId;
                    lastId = report.ID;
                    report.Niveau = "3";
                    rapports.Add(report);
                }
            }
            #endregion

            #region MENAGES PARTIELLEMENT REMPLIS
            report = new TableVerificationModel();
            report.Type = "3- %  DE MÉNAGES PARTIELLEMENT REMPLIS";
            report.ParentID = 1;
            report.ID = lastId + 1;
            parent_2 = report.ID;
            report.Niveau = "2";
            report.Total = "" + nbreMenagesPartiellesRemplis;
            report.Taux = "" + getPourcentage(nbreMenagesPartiellesRemplis, nbreTotalMenages);
            rapports.Add(report);
            lastId = report.ID;
            lastParentId = report.ID;
            if (nbreMenagesPartiellesRemplis > 0)
            {
                 //Nombre Indisponibilité avec rendez-vous
                int NbreIndAvecRendezVous = 0;
                List<MenageModel> menageEnIndAvecRendezVous = new List<MenageModel>();
                string raisonAvecRendezVous = "";

                //Abandon
                int nbreAbandon = 0;
                List<MenageModel> menageAbandon = new List<MenageModel>();
                string raisonAbandon = "";

                //Autre
                int nbreAutre = 0;
                List<MenageModel> menageEnAutre = new List<MenageModel>();
                string raisonAutre = "";
                //
                //Constrcution des branches Abandon, Rendexvous, Autre
                foreach (MenageModel menage in menagesPartiellesRemplis)
                {
                    List<RapportArModel> rarForMenage=service.Sr.GetAllRptAgentRecenseurByMenage(menage.MenageId);
                    foreach (RapportArModel rar in rarForMenage)
                    {
                        if (rar.RaisonActionId == 7)
                        {
                            nbreAbandon = nbreAbandon + 1;
                            menageAbandon.Add(menage);
                            raisonAbandon = Constant.getRaison(rar.RaisonActionId).Value;
                        }
                        if (rar.RaisonActionId == 8)
                        {
                            NbreIndAvecRendezVous = NbreIndAvecRendezVous + 1;
                            menageEnIndAvecRendezVous.Add(menage);
                            raisonAvecRendezVous = Constant.getRaison(rar.RaisonActionId).Value;
                        }
                        if (rar.RaisonActionId == 10)
                        {
                            nbreAutre = nbreAutre + 1;
                            menageEnAutre.Add(menage);
                            raisonAutre = Constant.getRaison(rar.RaisonActionId).Value;
                        }

                    }
                }
                //
                //Ajout des ces branches dans lea branche Menage
                int parentAbandon = 0;
                int parentRendezVous = 0;
                int parentAutre = 0;

                //Ajout de la branche Refus
                report = new TableVerificationModel();
                report.Type = "Abandon";
                report.ID = lastId +1;
                report.ParentID = lastParentId;
                parentAbandon = report.ID;
                report.Niveau = "2";
                lastId=report.ID;
                report.Total = ""+menageAbandon.Count;
                rapports.Add(report);
                //Ajout des menages se trouvant a l'interieur 
                foreach (MenageModel men in menageAbandon)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + men.BatimentId + "/Lojman-" + men.LogeId + "'Menaj-" + men.MenageId;
                    report.ID = lastId + 1;
                    report.ParentID = parentAbandon;
                    lastId = report.ID;
                    rapports.Add(report);
                }

                //Ajout de la branche Interruption avec Rendez-vous
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = lastParentId;
                report.Type = "Interruption avec Rendez-vous";
                report.Total = "" + menageEnIndAvecRendezVous.Count;
                parentRendezVous = report.ID;
                lastId = report.ID;
                rapports.Add(report);
                //Ajout de menages se trouvant a l'interieur
                foreach (MenageModel men in menageEnIndAvecRendezVous)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + men.BatimentId + "/Lojman-" + men.LogeId + "'Menaj-" + men.MenageId;
                    report.ID = lastId + 1;
                    report.ParentID = parentRendezVous;
                    lastId = report.ID;
                    rapports.Add(report);
                }
                //Ajout de la branche Autre
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = lastParentId;
                report.Type = "Autre";
                report.Total = "" + menageEnAutre.Count;
                parentAutre = report.ID;
                lastId = report.ID;
                rapports.Add(report);
                //Ajout de menages se trouvant a l'interieur
                foreach (MenageModel men in menageEnAutre)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + men.BatimentId + "/Lojman-" + men.LogeId + "'Menaj-" + men.MenageId;
                    report.ID = lastId + 1;
                    report.ParentID = parentAutre;
                    lastId = report.ID;
                    rapports.Add(report);
                }

            }
            #endregion

            #region INDIVIDUS PARTIELLEMENT REMPLIS
            report = new TableVerificationModel();
            report.Type = "4- % D´INDIVIDUS PARTIELLEMENT REMPLIS";
            report.ParentID = 1;
            report.ID = lastId + 1;
            parent_2 = report.ID;
            report.Niveau = "2";
            report.Total = "" + nbreindividusPartiellesRemplis;
            report.Taux = "" + getPourcentage(nbreindividusPartiellesRemplis, nbreTotalIndividus);
            rapports.Add(report);
            lastId = report.ID;
            lastParentId = report.ID;
            if (nbreindividusPartiellesRemplis > 0)
            {
                //Nombre Indisponibilité avec rendez-vous
                int NbreIndAvecRendezVous = 0;
                List<IndividuModel> individuEnIndAvecRendezVous = new List<IndividuModel>();
                string raisonAvecRendezVous = "";

                //Abandon
                int nbreAbandon = 0;
                List<IndividuModel> indviduAbandon = new List<IndividuModel>();
                string raisonAbandon = "";

                //Autre
                int nbreAutre = 0;
                List<IndividuModel> individuEnAutre = new List<IndividuModel>();
                string raisonAutre = "";
                //
                //Constrcution des branches Abandon, Rendexvous, Autre
                foreach (IndividuModel ind in individusPartiellesRemplis)
                {
                    List<RapportArModel> rarForIndividu = service.Sr.GetAllRptAgentRecenseurByIndividu(ind.IndividuId);
                    foreach (RapportArModel rar in rarForIndividu)
                    {
                        if (rar.RaisonActionId == 7)
                        {
                            nbreAbandon = nbreAbandon + 1;
                            indviduAbandon.Add(ind);
                            raisonAbandon = Constant.getRaison(rar.RaisonActionId).Value;
                        }
                        if (rar.RaisonActionId == 8)
                        {
                            NbreIndAvecRendezVous = NbreIndAvecRendezVous + 1;
                            individuEnIndAvecRendezVous.Add(ind);
                            raisonAvecRendezVous = Constant.getRaison(rar.RaisonActionId).Value;
                        }
                        if (rar.RaisonActionId == 10)
                        {
                            nbreAutre = nbreAutre + 1;
                            individuEnAutre.Add(ind);
                            raisonAutre = rar.AutreRaisonAction;
                        }

                    }
                }
                //
                //Ajout des ces branches dans lea branche Menage
                int parentAbandon = 0;
                int parentRendezVous = 0;
                int parentAutre = 0;

                //Ajout de la branche Refus
                report = new TableVerificationModel();
                report.Type = "Abandon";
                report.ID = lastId + 1;
                report.ParentID = lastParentId;
                parentAbandon = report.ID;
                report.Niveau = "6";
                lastId = report.ID;
                report.Total = "" + indviduAbandon.Count;
                rapports.Add(report);
                //Ajout des menages se trouvant a l'interieur 
                foreach (IndividuModel ind in indviduAbandon)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + ind.BatimentId + "/Lojman-" + ind.LogeId + "Menaj-" + ind.MenageId+"/Envidivi-"+ind.IndividuId;
                    report.ID = lastId + 1;
                    report.ParentID = parentAbandon;
                    lastId = report.ID;
                    rapports.Add(report);
                }

                //Ajout de la branche Interruption avec Rendez-vous
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = lastParentId;
                report.Type = "Interruption avec Rendez-vous";
                report.Niveau = "6";
                report.Total = "" + individuEnIndAvecRendezVous.Count;
                parentRendezVous = report.ID;
                lastId = report.ID;
                rapports.Add(report);
                //Ajout de menages se trouvant a l'interieur
                foreach (IndividuModel ind in individuEnIndAvecRendezVous)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + ind.BatimentId + "/Lojman-" + ind.LogeId + "'Menaj-" + ind.MenageId + "/Envidivi-" + ind.IndividuId;
                    report.ID = lastId + 1;
                    report.ParentID = parentRendezVous;
                    lastId = report.ID;
                    rapports.Add(report);
                }
                //Ajout de la branche Autre
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = lastParentId;
                report.Type = "Autre";
                report.Niveau = "6";
                report.Total = "" + individuEnAutre.Count;
                parentAutre = report.ID;
                lastId = report.ID;
                rapports.Add(report);
                //Ajout de menages se trouvant a l'interieur
                foreach (IndividuModel ind in individuEnAutre)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + ind.BatimentId + "/Lojman-" + ind.LogeId + "'Menaj-" + ind.MenageId + "/Envidivi-" + ind.IndividuId;
                    report.ID = lastId + 1;
                    report.ParentID = parentAutre;
                    report.Indicateur = raisonAutre;
                    lastId = report.ID;
                    rapports.Add(report);
                }

            }

            #endregion
            #endregion


            return rapports;
        }
        public static List<TableVerificationModel> getVerificationNonReponsePartielleForAllSdes(string path)

        {
            List<TableVerificationModel> rapports = new List<TableVerificationModel>();
            IConfigurationService configurationService = new ConfigurationService();
            List<SdeModel> listOfSdes = configurationService.searchAllSdes();
            int nbreLogements = 0;
            int nbreTotalLogement = 0;
            int nbreTotalMenages = 0;
            int nbreTotalIndividus = 0;
            int nbreMenages = 0;
            int nbreIndividus = 0;
            int nbreLogementsOccupantsAbsents = 0;
            int notFinishObject = 0;
            ISqliteReader service = null;

            foreach (SdeModel sde in listOfSdes)
            {
                service = new SqliteReader(Utilities.getConnectionString(path, sde.SdeId));
                nbreTotalIndividus += service.GetAllIndividus().Count;
                nbreTotalLogement += service.GetAllLogements().Count;
                nbreTotalMenages += service.GetAllMenages().Count;
                nbreLogements = nbreLogements + service.GetAllLogementIndNotFinish().Count;
                nbreMenages += service.GetAllMenageNotFinish().Count;
                nbreIndividus += service.GetAllIndividuNotFinish().Count;
                nbreLogementsOccupantsAbsents += service.GetAllLogementOccupantAbsent().Count;
                notFinishObject += (service.GetAllLogementIndNotFinish().Count() + service.GetAllMenageNotFinish().Count() + service.GetAllIndividuNotFinish().Count());
            }

            //Ajout des ID  parents 
            int parent_0 = 0;
            int parent_1 = 0;
            int parent_2 = 0;
            int parentLogement = 0;
            int parentMenage = 0;
            int parentIndividus = 0;
            int lastId = 0;
            int lastParentId = 0;

            #region STATUT DE REMPLISSAGE INITIAL
            //Ajout de la branche STATUT DE REMPLISSAGE INITIAL
            TableVerificationModel report = new TableVerificationModel();
            report.Type = "STATUT DE REMPLISSAGE INITIAL";
            report.Indicateur = "";
            report.ParentID = 0;
            report.ID = 1;
            report.Niveau = "1";
            lastId = report.ID;
            lastParentId = report.ID;
            parent_0 = lastParentId;
            rapports.Add(report);

            #region NOMBRE QUESTIONNAIRES PARTIELLEMENT REMPLIS
            //Branche 1
            report = new TableVerificationModel();
            report.Type = "1- NOMBRE QUESTIONNAIRES PARTIELLEMENT REMPLIS";
            report.ID = lastId + 1;
            report.ParentID = parent_0;
            lastId = report.ID;
            parent_1 = report.ID;
            report.Total = ""+notFinishObject;
            report.Niveau = "2";
            rapports.Add(report);

            //Liste des logements, menages individus
            List<LogementModel> logements = null;
            List<MenageModel> menages=null;
            List<IndividuModel> individus=null;
            //
            //Ajout des sdes
            foreach (SdeModel sd in listOfSdes)
            {
                service = new SqliteReader(Utilities.getConnectionString(path, sd.SdeId));
                logements = service.GetAllLogementIndNotFinish();
                menages=service.GetAllMenageNotFinish();
                individus=service.GetAllIndividuNotFinish();
                report = new TableVerificationModel();
                report.Type = ""+sd.SdeId;
                report.ID = lastId + 1;
                lastId = report.ID;
                report.Niveau = "3";
                report.Total = ""+ (logements.Count + menages.Count + individus.Count);
                report.ParentID = parent_1;
                parent_2 = report.ID;
                rapports.Add(report);
                //
                //Ajout de la branche Logements
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = parent_2;
                report.Type = "Nombre de logements individuels";
                report.Total = ""+logements.Count;
                report.Niveau = "4";
                report.Taux ="%"+ getPourcentage(logements.Count, service.GetAllLogements().Count);
                lastId = report.ID;
                parentLogement = report.ID;
                rapports.Add(report);
                //Ajout des branches logements, menages individus dans la SDE


                foreach (LogementModel logment in logements)
                {
                    report = new TableVerificationModel();
                    report.ID = lastId + 1;
                    report.ParentID = parentLogement;
                    report.Type = "Batiman-" + logment.BatimentId + "/Lojman-" + logment.LogeId;
                    lastId = report.ID;
                    report.Niveau = "5";
                    rapports.Add(report);
                }
                //
                //Ajout de la branche Menages
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = parent_2;
                report.Type = "Nombre de ménages";
                report.Total = "" + menages.Count;
                report.Niveau = "4";
                report.Taux = "%" + getPourcentage(menages.Count, service.GetAllMenages().Count);
                lastId = report.ID;
                parentMenage = report.ID;
                rapports.Add(report);

                foreach(MenageModel men in menages )
                {
                    report = new TableVerificationModel();
                    report.ID = lastId + 1;
                    report.ParentID = parentMenage;
                    report.Type = "Batiman-" + men.BatimentId + "/Lojman-" + men.LogeId+"/Menaj-"+men.MenageId;
                    lastId = report.ID;
                    report.Niveau = "5";
                    rapports.Add(report);
                }
                //
                //Ajout de la branche Menages
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = parent_2;
                report.Type = "Nombre d´individus";
                report.Total = "" + individus.Count;
                report.Niveau = "4";
                report.Taux = "%" + getPourcentage(individus.Count, service.GetAllIndividus().Count);
                lastId = report.ID;
                parentIndividus = report.ID;
                rapports.Add(report);
                foreach (IndividuModel ind in individus)
                {
                    report = new TableVerificationModel();
                    report.ID = lastId + 1;
                    report.ParentID = parentIndividus;
                    report.Type = "Batiman-" + ind.BatimentId + "/Lojman-" + ind.LogeId + "/Menaj-" + ind.MenageId+"/Endividi-"+ind.IndividuId;
                    lastId = report.ID;
                    report.Niveau = "5";
                    rapports.Add(report);
                }

            }
            #endregion

            #region NOMBRE DE LOGEMENTS OCCUPÉS AVEC OCCUPANTS ABSENTS
            report = new TableVerificationModel();
            report.Type = "2- NOMBRE DE LOGEMENTS OCCUPÉS AVEC OCCUPANTS ABSENTS";
            report.ID = lastId + 1;
            report.ParentID = parent_0;
            lastId = report.ID;
            parent_1 = report.ID;
            report.Total = "" + nbreLogementsOccupantsAbsents;
            report.Taux = "" + getPourcentage(nbreLogementsOccupantsAbsents, nbreTotalLogement)+"%";
            report.Niveau = "2";
            rapports.Add(report);

            //AJout des branches Sdes
            foreach (SdeModel sd in listOfSdes)
            {
                service = new SqliteReader(Utilities.getConnectionString(path, sd.SdeId));
                logements = new List<LogementModel>();
                logements = service.GetAllLogementOccupantAbsent();
                report = new TableVerificationModel();
                report.Type = "" + sd.SdeId;
                report.ID = lastId + 1;
                lastId = report.ID;
                report.Total = ""+logements.Count;
                report.Taux = "" + getPourcentage(logements.Count, nbreTotalLogement) + "%";
                report.Niveau = "3";
                report.ParentID = parent_1;
                parent_2 = report.ID;
                rapports.Add(report);

                //Ajout des logemnts dans chaque SDE
                foreach (LogementModel logChild in logements)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + logChild.BatimentId + "/Lojman-" + logChild.LogeId;
                    report.ID = lastId + 1;
                    lastId = report.ID;
                    report.Niveau = "5";
                    report.ParentID = parent_2;
                    rapports.Add(report);
                }
            }
            #endregion

            #region DE MÉNAGES PARTIELLEMENT REMPLIS
            int parentAbandon = 0;
            int parentRendezVous = 0;
            int parentAutre = 0;
            report = new TableVerificationModel();
            report.Type = "3- % DE MÉNAGES PARTIELLEMENT REMPLIS";
            report.ID = lastId + 1;
            report.ParentID = parent_0;
            lastId = report.ID;
            parent_1 = report.ID;
            report.Total = "" + nbreMenages;
            report.Taux = "" + getPourcentage(nbreMenages, nbreTotalMenages) + "%";
            report.Niveau = "2";
            rapports.Add(report);

            foreach (SdeModel sd in listOfSdes)
            {
                service = new SqliteReader(Utilities.getConnectionString(path, sd.SdeId));
                menages = service.GetAllMenageNotFinish();
                report = new TableVerificationModel();
                report.Type = "" + sd.SdeId;
                report.ID = lastId + 1;
                lastId = report.ID;
                report.Total = ""+menages.Count;
                report.Taux = "" + getPourcentage(menages.Count, nbreTotalMenages);
                report.Niveau = "3";
                report.ParentID = parent_1;
                parent_2 = report.ID;
                rapports.Add(report);

                //Ajout des branches Abandon, Rendex-Vous, Autre et leurs filles
                int NbreIndAvecRendezVous = 0;
                List<MenageModel> menageEnIndAvecRendezVous = new List<MenageModel>();
                string raisonAvecRendezVous = "";

                //Abandon
                int nbreAbandon = 0;
                List<MenageModel> menageAbandon = new List<MenageModel>();
                string raisonAbandon = "";

                //Autre
                int nbreAutre = 0;
                List<MenageModel> menageEnAutre = new List<MenageModel>();
                string raisonAutre = "";
                //
                //Constrcution des branches Abandon, Rendexvous, Autre
                foreach (MenageModel menage in menages)
                {
                    List<RapportArModel> rarForMenage = service.GetAllRptAgentRecenseurByMenage(menage.MenageId);
                    foreach (RapportArModel rar in rarForMenage)
                    {
                        if (rar.RaisonActionId == 7)
                        {
                            nbreAbandon = nbreAbandon + 1;
                            menageAbandon.Add(menage);
                            raisonAbandon = Constant.getRaison(rar.RaisonActionId).Value;
                        }
                        if (rar.RaisonActionId == 8)
                        {
                            NbreIndAvecRendezVous = NbreIndAvecRendezVous + 1;
                            menageEnIndAvecRendezVous.Add(menage);
                            raisonAvecRendezVous = Constant.getRaison(rar.RaisonActionId).Value;
                        }
                        if (rar.RaisonActionId == 10)
                        {
                            nbreAutre = nbreAutre + 1;
                            menageEnAutre.Add(menage);
                            raisonAutre = Constant.getRaison(rar.RaisonActionId).Value;
                        }

                    }
                }
                //
                //Ajout de la branche Refus
                report = new TableVerificationModel();
                report.Type = "Abandon";
                report.ID = lastId + 1;
                report.ParentID = parent_2;
                parentAbandon = report.ID;
                report.Niveau = "4";
                lastId = report.ID;
                report.Total = "" + menageAbandon.Count;
                report.Taux = "" + getPourcentage(menageAbandon.Count, nbreTotalMenages);
                rapports.Add(report);
                //Ajout des menages se trouvant a l'interieur 
                foreach (MenageModel men in menageAbandon)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + men.BatimentId + "/Lojman-" + men.LogeId + "'Menaj-" + men.MenageId;
                    report.ID = lastId + 1;
                    report.ParentID = parentAbandon;
                    lastId = report.ID;
                    report.Niveau = "5";
                    rapports.Add(report);
                }

                //Ajout de la branche Interruption avec Rendez-vous
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = parent_2;
                report.Type = "Interruption avec Rendez-vous";
                report.Total = "" + menageEnIndAvecRendezVous.Count;
                parentRendezVous = report.ID;
                report.Niveau = "4";
                lastId = report.ID;
                report.Taux = "" + getPourcentage(menageEnIndAvecRendezVous.Count, nbreTotalMenages);
                rapports.Add(report);
                //Ajout de menages se trouvant a l'interieur
                foreach (MenageModel men in menageEnIndAvecRendezVous)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + men.BatimentId + "/Lojman-" + men.LogeId + "'Menaj-" + men.MenageId;
                    report.ID = lastId + 1;
                    report.ParentID = parentRendezVous;
                    lastId = report.ID;
                    report.Niveau = "5";
                    rapports.Add(report);
                }
                //Ajout de la branche Autre
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = parent_2;
                report.Type = "Autre";
                report.Total = "" + menageEnAutre.Count;
                parentAutre = report.ID;
                report.Niveau = "4";
                lastId = report.ID;
                report.Taux = "" + getPourcentage(menageEnAutre.Count, nbreTotalMenages);
                rapports.Add(report);
                //Ajout de menages se trouvant a l'interieur
                foreach (MenageModel men in menageEnAutre)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + men.BatimentId + "/Lojman-" + men.LogeId + "'Menaj-" + men.MenageId;
                    report.ID = lastId + 1;
                    report.ParentID = parentAutre;
                    lastId = report.ID;
                    report.Niveau = "5";
                    rapports.Add(report);
                }

            }
            #endregion

            #region % D´INDIVIDUS PARTIELLEMENT REMPLIS
             parentAbandon = 0;
             parentRendezVous = 0;
             parentAutre = 0;

             report = new TableVerificationModel();
             report.Type = "4- % D´INDIVIDUS PARTIELLEMENT REMPLI";
             report.ID = lastId + 1;
             report.ParentID = parent_0;
             lastId = report.ID;
             parent_1 = report.ID;
             report.Total = "" + nbreIndividus;
             report.Taux = "" + getPourcentage(nbreIndividus, nbreTotalIndividus) + "%";
             report.Niveau = "2";
             rapports.Add(report);

             foreach (SdeModel sd in listOfSdes)
             {
                 service = new SqliteReader(Utilities.getConnectionString(path, sd.SdeId));
                 individus = new List<IndividuModel>();
                 individus = service.GetAllIndividuNotFinish();
                 report = new TableVerificationModel();
                 report.Type = "" + sd.SdeId;
                 report.ID = lastId + 1;
                 lastId = report.ID;
                 report.Total = "" + individus.Count;
                 report.Taux = "" + getPourcentage(individus.Count, nbreTotalIndividus)+"%";
                 report.Niveau = "3";
                 report.ParentID = parent_1;
                 parent_2 = report.ID;
                 rapports.Add(report);

                 //Ajout des branches Abandon, Rendex-Vous, Autre et leurs filles
                 int NbreIndAvecRendezVous = 0;
                 List<IndividuModel> individusEnIndAvecRendezVous = new List<IndividuModel>();
                 string raisonAvecRendezVous = "";

                 //Abandon
                 int nbreAbandon = 0;
                 List<IndividuModel> individusAbandon = new List<IndividuModel>();
                 string raisonAbandon = "";

                 //Autre
                 int nbreAutre = 0;
                 List<IndividuModel> individusEnAutre = new List<IndividuModel>();
                 string raisonAutre = "";
                 //
                 //Constrcution des branches Abandon, Rendexvous, Autre
                 foreach (IndividuModel ind in individus)
                 {
                     List<RapportArModel> rarForIndividu = service.GetAllRptAgentRecenseurByIndividu(ind.IndividuId);
                     foreach (RapportArModel rar in rarForIndividu)
                     {
                         if (rar.RaisonActionId == 7)
                         {
                             nbreAbandon = nbreAbandon + 1;
                             individusAbandon.Add(ind);
                             raisonAbandon = Constant.getRaison(rar.RaisonActionId).Value;
                         }
                         if (rar.RaisonActionId == 8)
                         {
                             NbreIndAvecRendezVous = NbreIndAvecRendezVous + 1;
                             individusEnIndAvecRendezVous.Add(ind);
                         }
                         if (rar.RaisonActionId == 10)
                         {
                             nbreAutre = nbreAutre + 1;
                             ind.Raison = rar.AutreRaisonAction;
                             individusEnAutre.Add(ind);
                             raisonAutre = Constant.getRaison(rar.RaisonActionId).Value;

                         }

                     }
                 }
                 //
                 //Ajout de la branche Refus
                 report = new TableVerificationModel();
                 report.Type = "Abandon";
                 report.ID = lastId + 1;
                 report.ParentID = parent_2;
                 parentAbandon = report.ID;
                 report.Niveau = "4";
                 lastId = report.ID;
                 report.Total = "" + individusAbandon.Count;
                 report.Taux = "" + getPourcentage(individusAbandon.Count, nbreTotalIndividus);
                 rapports.Add(report);
                 //Ajout des menages se trouvant a l'interieur 
                 foreach (IndividuModel ind in individusAbandon)
                 {
                     report = new TableVerificationModel();
                     report.Type = "Batiman-" + ind.BatimentId + "/Lojman-" + ind.LogeId + "Menaj-" + ind.MenageId+"/Endividi"+ind.IndividuId;
                     report.ID = lastId + 1;
                     report.ParentID = parentAbandon;
                     report.Indicateur = raisonAbandon;
                     lastId = report.ID;
                     report.Niveau = "5";
                     rapports.Add(report);
                 }

                 //Ajout de la branche Interruption avec Rendez-vous
                 report = new TableVerificationModel();
                 report.ID = lastId + 1;
                 report.ParentID = parent_2;
                 report.Type = "Interruption avec Rendez-vous";
                 report.Total = "" + individusEnIndAvecRendezVous.Count;
                 parentRendezVous = report.ID;
                 report.Niveau = "4";
                 lastId = report.ID;
                 report.Taux = "" + getPourcentage(individusEnIndAvecRendezVous.Count, nbreTotalIndividus);
                 rapports.Add(report);
                 //Ajout de menages se trouvant a l'interieur
                 foreach (IndividuModel ind in individusEnIndAvecRendezVous)
                 {
                     report = new TableVerificationModel();
                     report.Type = "Batiman-" + ind.BatimentId + "/Lojman-" + ind.LogeId + "Menaj-" + ind.MenageId + "/Endividi" + ind.IndividuId;
                     report.ID = lastId + 1;
                     report.ParentID = parentRendezVous;
                     report.Indicateur = raisonAvecRendezVous;
                     lastId = report.ID;
                     report.Niveau = "5";
                     rapports.Add(report);
                 }
                 //Ajout de la branche Autre
                 report = new TableVerificationModel();
                 report.ID = lastId + 1;
                 report.ParentID = parent_2;
                 report.Type = "Autre";
                 report.Total = "" + individusEnAutre.Count;
                 parentAutre = report.ID;
                 report.Niveau = "4";
                 lastId = report.ID;
                 report.Taux = "" + getPourcentage(individusEnAutre.Count, nbreTotalIndividus);
                 rapports.Add(report);
                 //Ajout de menages se trouvant a l'interieur
                 foreach (IndividuModel ind in individusEnAutre)
                 {
                     report = new TableVerificationModel();
                     report.Type = "Batiman-" + ind.BatimentId + "/Lojman-" + ind.LogeId + "Menaj-" + ind.MenageId + "/Endividi" + ind.IndividuId;
                     report.ID = lastId + 1;
                     report.ParentID = parentAutre;
                     lastId = report.ID;
                     report.Niveau = "5";
                     report.Indicateur = ind.Raison;
                     rapports.Add(report);
                 }

             }

            #endregion

            #endregion

            return rapports;
        }
        public static List<VerificationFlag> getVerificationNonReponseParVariable(string path, string sdeId)
        {
            List<VerificationFlag> rapports = new List<VerificationFlag>();
            //Ajout des ID  parents 
            int parent_0 = 0;
            int parent_1 = 0;
            int parent_2 = 0;
            int parent_3 = 0;
            //
            VerificationFlag report = new VerificationFlag();
            report.ID = 1;
            report.ParentID = 0;
            parent_0 = report.ParentID;
            report.Theme = "Caractéristiques socio-démographiques";
            report.Sous_Theme = "Age";
            report.Denominateur = "Population totale";
            report.Flag = "FL1";
            report.Variable1 = "M11.5";
            report.Variable2 = "M11.6";
            report.Indice = "% de la population totale sans information sur la date de naissance ni sur l´âge";
            rapports.Add(report);

            return rapports;
        }
        public static List<CouvertureModel> getTotalCouverture(string path, SdeModel sde)
        {
            List<CouvertureModel> couvertures = new List<CouvertureModel>();
            try
            {
                 //Le nombre de batiments
                CouvertureModel model1 = new CouvertureModel();
                model1.Couverture = "Nombre de bâtiments";
                model1.Actualisation = sde.TotalBatCartographie.GetValueOrDefault();
                model1.Total = sde.TotalBatRecense.GetValueOrDefault();
                couvertures.Add(model1);
                // Nombre de meanges
                model1 = new CouvertureModel();
                model1.Couverture = "Nombre de menages  recensés";
                model1.Actualisation = 0;
                model1.Total = sde.TotalMenageRecense.GetValueOrDefault();
                couvertures.Add(model1);

                //Nombre de logements Individuels
                model1 = new CouvertureModel();
                model1.Couverture = "Nombre de logements  individuels";
                model1.Actualisation = 0;
                model1.Total = sde.TotalLogeIRecense.GetValueOrDefault();
                couvertures.Add(model1);
                //Nombre de personnes
                model1 = new CouvertureModel();
                model1.Couverture = "Nombre de personnes  recensées";
                model1.Actualisation = 0;
                model1.Total = sde.TotalIndRecense.GetValueOrDefault();
                couvertures.Add(model1);

                couvertures.Add(model1);
            }
            catch(Exception)
            {

            }
            return couvertures;
        }

        #endregion

        #region GESTION DES RAPPORTS
        public static List<RapportModel> getRapportTroncCommun(SdeModel sde)
        {
            try
            {
                List<RapportModel> rapports = new List<RapportModel>();
                RapportModel report = new RapportModel();
                report.Type = "Couverture";
                report.ID = "1";
                report.ParentID = "0";
                rapports.Add(report);
                //Rapport de couverture
                report = new RapportModel();
                report.ID = "2";
                report.ParentID = "1";
                report.Indicateur = "Nombre de bâtiments recensés par l'agent recenseur";
                report.Total = "" + sde.TotalBatRecense.GetValueOrDefault();
                report.Pourcentage = "" + Utilities.getPourcentage(sde.TotalBatRecenseV.GetValueOrDefault(), sde.TotalBatRecense.GetValueOrDefault()) + "%";
                rapports.Add(report);

                report = new RapportModel();
                report.ID = "3";
                report.ParentID = "1";
                report.Indicateur = "Nombre de logements recensés par l'agent recenseur";
                //if()
                int nbreTotal = sde.TotalLogeCRecense.GetValueOrDefault() + sde.TotalLogeIRecense.GetValueOrDefault();
                int nbreValide = sde.TotalLogeCRecense.GetValueOrDefault() + sde.TotalLogeIRecense.GetValueOrDefault();
                report.Total = "" + nbreTotal;
                report.Pourcentage = "" + Utilities.getPourcentage(nbreValide, nbreTotal) + "%";
                rapports.Add(report);

                report = new RapportModel();
                report.ID = "4";
                report.ParentID = "1";
                report.Indicateur = "Nombre de ménages recensés par l'agent recenseur";
                report.Total = "" + sde.TotalMenageRecense.GetValueOrDefault();
                report.Pourcentage = "" + Utilities.getPourcentage(sde.TotalMenageRecenseV.GetValueOrDefault(), sde.TotalMenageRecense.GetValueOrDefault()) + "%";
                rapports.Add(report);

                report = new RapportModel();
                report.ID = "5";
                report.ParentID = "1";
                report.Indicateur = "Nombre de personnes recensées par l'agent recenseur";
                report.Total = "" + sde.TotalIndRecense.GetValueOrDefault();
                rapports.Add(report);

                //Rapport batiment
                report = new RapportModel();
                report.ID = "6";
                report.ParentID = "0";
                report.Type = "Batiment";
                rapports.Add(report);


                report = new RapportModel();
                report.ID = "7";
                report.ParentID = "6";
                report.Indicateur = "Nombre de bâtiments recensés par l'agent recenseur";
                report.Total = "" + sde.TotalBatRecense.GetValueOrDefault();
                report.Pourcentage = "" + Utilities.getPourcentage(sde.TotalBatRecenseV.GetValueOrDefault(), sde.TotalBatRecense.GetValueOrDefault()) + "%";
                rapports.Add(report);
                //Rapport Logement Individuel
                report = new RapportModel();
                report.ID = "8";
                report.ParentID = "0";
                report.Type = "Logement Individuel";
                rapports.Add(report);


                report = new RapportModel();
                report.ID = "9";
                report.ParentID = "8";
                report.Indicateur = "Distribution du nombre de logements individuels recensés\r\nselon l´occupation OCCUPE  et l´état d´avancement\r\nde la couverture de la SDE.";
                report.Total = "" + sde.TotalLogeIOccupeRecense.GetValueOrDefault();
                report.Pourcentage = "" + Utilities.getPourcentage(sde.TotalLogeIOccupeRecenseV.GetValueOrDefault(), sde.TotalLogeIOccupeRecense.GetValueOrDefault()) + "%";
                rapports.Add(report);

                report = new RapportModel();
                report.ID = "10";
                report.ParentID = "8";
                report.Indicateur = "Distribution du nombre de logements individuels recensés\r\nselon l´occupation VIDE  et l´état d´avancement\r\nde la couverture de la SDE.";
                report.Total = "" + sde.TotalLogeIVideRecense.GetValueOrDefault();
                report.Pourcentage = "" + Utilities.getPourcentage(sde.TotalLogeIVideRecenseV.GetValueOrDefault(), sde.TotalLogeIVideRecense.GetValueOrDefault()) + "%";
                rapports.Add(report);

                report = new RapportModel();
                report.ID = "11";
                report.ParentID = "8";
                report.Indicateur = "Distribution du nombre de logements individuels recensés\r\nselon l´occupation TEMPOREL  et l´état d´avancement\r\nde la couverture de la SDE.";
                report.Total = "" + sde.TotalLogeIUsageTemporelRecense.GetValueOrDefault();
                report.Pourcentage = "" + Utilities.getPourcentage(sde.TotalLogeIUsageTemporelRecenseV.GetValueOrDefault(), sde.TotalLogeIUsageTemporelRecense.GetValueOrDefault()) + "%";
                rapports.Add(report);
                //Logement Collectif
                report = new RapportModel();
                report.ID = "12";
                report.ParentID = "0";
                report.Type = "Logement Collectif";
                rapports.Add(report);

                report = new RapportModel();
                report.ID = "13";
                report.ParentID = "12";
                report.Indicateur = "Distribution du nombre de logements collectifs recensés \r\nselon l´occupation OCCUPE  et l´état d´avancement de la couverture \r\n de la SDE.";
                report.Total = "" + sde.TotalLogeIOccupeRecense.GetValueOrDefault();
                report.Pourcentage = "" + Utilities.getPourcentage(sde.TotalLogeIOccupeRecenseV.GetValueOrDefault(), sde.TotalLogeIOccupeRecense.GetValueOrDefault()) + "%";
                rapports.Add(report);

                report = new RapportModel();
                report.ID = "14";
                report.ParentID = "12";
                report.Indicateur = "Distribution du nombre de logements collectifs recensés \r\nselon l´occupation VIDE  et l´état d´avancement de la couverture \r\n de la SDE.";
                report.Total = "" + sde.TotalLogeIVideRecense.GetValueOrDefault();
                report.Pourcentage = "" + Utilities.getPourcentage(sde.TotalLogeIVideRecenseV.GetValueOrDefault(), sde.TotalLogeIVideRecense.GetValueOrDefault()) + "%"; ;
                rapports.Add(report);

                report = new RapportModel();
                report.ID = "15";
                report.ParentID = "12";
                report.Indicateur = "Distribution du nombre de logements collectifs recensés \r\nselon l´occupation TEMPOREL  et l´état d´avancement de la couverture \r\n de la SDE.";
                report.Total = "" + sde.TotalLogeIUsageTemporelRecense.GetValueOrDefault();
                report.Pourcentage = "" + Utilities.getPourcentage(sde.TotalLogeIUsageTemporelRecenseV.GetValueOrDefault(), sde.TotalLogeIUsageTemporelRecense.GetValueOrDefault()) + " %";
                rapports.Add(report);

                //Rapport personnes
                report = new RapportModel();
                report.ID = "16";
                report.ParentID = "0";
                report.Type = "Personnes";
                rapports.Add(report);

                report = new RapportModel();
                report.ID = "17";
                report.ParentID = "16";
                report.Indicateur = "Distribution du nombre de personnes recensées par  logement\r\nindividuel selon l´état d´avancement de la couverture de la SDE.";
                report.Total = "" + sde.TotalIndRecense.GetValueOrDefault();
                rapports.Add(report);
                return rapports;
            }
            catch (Exception)
            {

            }
            return null;
            
        }

        public static List<RapportModel> getRrtPerformanceDemographique(SdeModel sde)
        {
            try
            {
                if (sde != null)
                {
                    List<RapportModel> rapports = new List<RapportModel>();
                    RapportModel rapport = new RapportModel();

                    rapport.ID = "1";
                    rapport.ParentID = "0";
                    rapport.Type = "Indicateurs de Performances";
                    rapports.Add(rapport);

                    rapport = new RapportModel();
                    rapport.ID = "2";
                    rapport.ParentID = "1";
                    rapport.Indicateur = "Nombre de bâtiments recensés par jour d´enquête";
                    rapport.Total = "" + sde.TotalBatRecenseParJour.GetValueOrDefault();
                    rapport.Pourcentage = "" + Utilities.getPourcentage(sde.TotalBatRecenseParJourV.GetValueOrDefault(), sde.TotalBatRecenseParJour.GetValueOrDefault()) + "%";
                    rapports.Add(rapport);

                    rapport = new RapportModel();
                    rapport.ID = "3";
                    rapport.ParentID = "1";
                    rapport.Indicateur = "Nombre de logements recensés par jour d´enquête";
                    rapport.Total = "" + sde.TotalLogeRecenseParJour.GetValueOrDefault();
                    rapport.Pourcentage = "" + Utilities.getPourcentage(sde.TotalLogeRecenseParJourV.GetValueOrDefault(), sde.TotalLogeRecenseParJour.GetValueOrDefault()) + "%";
                    rapports.Add(rapport);

                    rapport = new RapportModel();
                    rapport.ID = "4";
                    rapport.ParentID = "1";
                    rapport.Indicateur = "Nombre de ménages interviewés par jour d´enquête";
                    rapport.Total = "" + sde.TotalMenageRecenseParJour.GetValueOrDefault();
                    rapport.Pourcentage = "" + Utilities.getPourcentage(sde.TotalMenageRecenseParJourV.GetValueOrDefault(), sde.TotalMenageRecenseParJour.GetValueOrDefault()) + "%";
                    rapports.Add(rapport);

                    rapport = new RapportModel();
                    rapport.ID = "5";
                    rapport.ParentID = "1";
                    rapport.Indicateur = "Nombre de personnes comptées par jour d´enquête";
                    rapports.Add(rapport);

                    //Indicateurs demogra[hiques
                    rapport = new RapportModel();
                    rapport.ID = "6";
                    rapport.ParentID = "0";
                    rapport.Type = "Indicateurs  Démographiques";
                    rapports.Add(rapport);

                    rapport = new RapportModel();
                    rapport.ID = "7";
                    rapport.ParentID = "6";
                    rapport.Indicateur = "Volume de population recensée ";
                    rapport.Total = "" + sde.TotalIndRecense.GetValueOrDefault();
                    rapport.Pourcentage = "N/A";
                    rapports.Add(rapport);

                    rapport = new RapportModel();
                    rapport.ID = "8";
                    rapport.ParentID = "6";
                    rapport.Indicateur = "Indice de masculinité";
                    rapport.Total = "" + sde.IndiceMasculinite.GetValueOrDefault();
                    rapport.Pourcentage = "N/A";
                    rapports.Add(rapport);

                    rapport = new RapportModel();
                    rapport.ID = "9";
                    rapport.ParentID = "6";
                    rapport.Indicateur = "Proportion (%) d´enfants de moins de 5 ans";
                    rapport.Total = "" + sde.TotalEnfantDeMoinsDe5Ans.GetValueOrDefault();
                    rapport.Pourcentage = "" + Utilities.getPourcentage(sde.TotalEnfantDeMoinsDe5Ans.GetValueOrDefault(), sde.TotalIndRecense.GetValueOrDefault()) + "%";
                    rapports.Add(rapport);

                    rapport = new RapportModel();
                    rapport.ID = "10";
                    rapport.ParentID = "6";
                    rapport.Indicateur = "Proportion (%)  de personnes de 18 ans et plus";
                    rapport.Total = "" + sde.TotalIndividu18AnsEtPlus.GetValueOrDefault();
                    rapport.Pourcentage = "" + Utilities.getPourcentage(sde.TotalIndividu18AnsEtPlus.GetValueOrDefault(), sde.TotalIndRecense.GetValueOrDefault()) + "%";
                    rapports.Add(rapport);

                    rapport = new RapportModel();
                    rapport.ID = "11";
                    rapport.ParentID = "6";
                    rapport.Indicateur = "Proportion (%)  de personnes de 10 ans et plus";
                    rapport.Total = "" + sde.TotalIndividu10AnsEtPlus.GetValueOrDefault();
                    rapport.Pourcentage = "" + Utilities.getPourcentage(sde.TotalIndividu10AnsEtPlus.GetValueOrDefault(), sde.TotalIndRecense.GetValueOrDefault()) + "%";
                    rapports.Add(rapport);

                    rapport = new RapportModel();
                    rapport.ID = "12";
                    rapport.ParentID = "6";
                    rapport.Indicateur = "Proportion (%)  de personnes de 65 ans plus";
                    rapport.Total = "" + sde.TotalIndividu65AnsEtPlus.GetValueOrDefault();
                    rapport.Pourcentage = "" + Utilities.getPourcentage(sde.TotalIndividu65AnsEtPlus.GetValueOrDefault(), sde.TotalIndRecense.GetValueOrDefault()) + "%";
                    rapports.Add(rapport);

                    rapport = new RapportModel();
                    rapport.ID = "13";
                    rapport.ParentID = "6";
                    rapport.Indicateur = "Proportion (%)  de ménages de grande taille (6 personnes et plus par exemple)";
                    rapport.Total = "" + sde.TotalMenageDe6IndsEtPlus.GetValueOrDefault();
                    rapport.Pourcentage = "" + Utilities.getPourcentage(sde.TotalMenageDe6IndsEtPlus.GetValueOrDefault(), sde.TotalMenageRecense.GetValueOrDefault()) + "%";
                    rapports.Add(rapport);

                    return rapports;
                }
            }
            catch (Exception)
            {

            }
            return null;
        }

        public static List<RapportComparaisonModel> getRprtComparaisonChefMage(MenageCEModel model)
        {
            try
            {
                RapportComparaisonModel parentReport = new RapportComparaisonModel();
                string reponseAgent = null;
                string reponseSup = null;
                Users.users.ID = 0;
                ISqliteReader reader = new SqliteReader(getConnectionString(Users.users.DatabasePath, model.SdeId));
                IContreEnqueteService ce_service = new ContreEnqueteService();
                List<IndividuCEModel> listOfInCe = null;
                List<IndividuModel> listOfInd = reader.GetIndividuByMenage(model.MenageId);
                List<RapportComparaisonModel> rapports = new List<RapportComparaisonModel>();
                if (listOfInd != null)
                {
                    IndividuModel individu = null;
                    IndividuCEModel indCe = null;
                    foreach (IndividuModel ind in listOfInd)
                    {
                        if (ind.Qp3LienDeParente == Constant.CHEF_MENAGE)
                        {
                            individu = ind;
                        }
                        break;
                    }

                    //REcherche du chef de Menage dans les contre-enquetes
                    listOfInCe = ce_service.searchAllIndividuCE(model);
                    foreach (IndividuCEModel ind in listOfInCe)
                    {
                        if (ind.Q6LienDeParente == Constant.CHEF_MENAGE)
                        {
                            indCe = ind;
                        }
                        break;
                    }
                    #region PROFIL CHEF MENAGE
                    RapportComparaisonModel rpt = new RapportComparaisonModel();
                    tbl_question question = reader.getQuestionByNomChamps(Constant.Q5bAge);
                    rpt.ID = "" + getId();
                    rpt.ParentID = "0";
                    rpt.Type = "0. PROFIL CHEF(FE) DE MÉNAGE";
                    rapports.Add(rpt);

                    parentReport = rpt;
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Numero d'ordre", "Nimewo Od", individu.Q1NoOrdre.ToString(), indCe.Qp1NoOrdre.ToString());
                    rapports.Add(rpt);
                    parentReport = rpt;
                    //AGE CHEF MENAGE
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Age", question.libelle, individu.Qp5bAge.ToString(), indCe.Q5bAge.GetValueOrDefault().ToString());
                    parentReport = rpt;
                    rapports.Add(rpt);
                    //Sexe Chef Menage
                    question = reader.getQuestionByNomChamps(Constant.Q4Sexe);
                    reponseAgent = reader.getReponse(question.codeQuestion, individu.Qp4Sexe.ToString());
                    reponseSup = reader.getReponse(question.codeQuestion, indCe.Q4Sexe.ToString());
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Sexe", question.libelle, reponseAgent, reponseSup);
                    rapports.Add(rpt);
                    parentReport = rpt;
                    //ACTIVITE ECONOMIQUE
                    question = reader.getQuestionByNomChamps(Constant.Qa1ActEconomiqueDerniereSemaine);
                    reponseAgent = reader.getReponse(question.codeQuestion, individu.Qa1ActEconomiqueDerniereSemaine.ToString());
                    reponseSup = reader.getReponse(question.codeQuestion, indCe.Qa1ActEconomiqueDerniereSemaine.ToString());
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Situation d'activite", question.libelle, reponseAgent, reponseSup);
                    rapports.Add(rpt);
                    parentReport = rpt;
                    #endregion

                    #region SOCIO-DEMOGRAPHIE
                    rpt = new RapportComparaisonModel();
                    rpt.ID = "" + getId();
                    rpt.ParentID = "0";
                    rpt.Type = "1. SOCIO-DÉMOGRAPHIE DU MÉNAGE";
                    rapports.Add(rpt);
                    parentReport = new RapportComparaisonModel();
                    parentReport = rpt;

                    //Nombre Personnes vivant dans le Menage
                    reponseAgent = reader.getTotalPersonnesByMenage(model.MenageId).ToString();
                    reponseSup = ce_service.getTotalPersonnesByMenage(model).ToString();
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre Total de personnes dans le menage", "Quantite?", reponseAgent, reponseSup);
                    rapports.Add(rpt);
                    parentReport = rpt;
                    //
                    //Nombre de personnes de sexe feminin
                    reponseAgent = reader.getTotalFemmesByMenage(model.MenageId).ToString();
                    reponseSup = ce_service.getTotalFemmesByMenage(model).ToString();
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre de personnes de sexe feminin", "Quantite?", reponseAgent, reponseSup);
                    rapports.Add(rpt);
                    parentReport = rpt;
                    //
                    //Nombre d'enfants de moins de 5 ans
                    reponseAgent = reader.getTotalEnfantMoins5AnsByMenage(model.MenageId).ToString();
                    reponseSup = ce_service.getTotalEnfantMoins5AnsByMenage(model).ToString();
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre d'enfants de moins de 5 ans", "Quantite?", reponseAgent, reponseSup);
                    rapports.Add(rpt);
                    parentReport = rpt;
                    //
                    //Nombre de personnes de 15 ans et plus
                    reponseAgent = reader.getTotalPersonnesMoin15AnsByMenage(model.MenageId).ToString();
                    reponseSup = ce_service.getTotalPersonnesMoin15AnsByMenage(model).ToString();
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre de personnes de 15 ans et plus", "Quantite?", reponseAgent, reponseSup);
                    rapports.Add(rpt);
                    parentReport = rpt;
                    //
                    #endregion

                    #region ACTIVITE ET FONCTIONNEMENT
                    rpt = new RapportComparaisonModel();
                    rpt.ID = "" + getId();
                    rpt.ParentID = "0";
                    rpt.Type = "2. ACTIVITÉ ET FONCTIONNEMENT";
                    rapports.Add(rpt);
                    parentReport = new RapportComparaisonModel();
                    parentReport = rpt;

                    //Handicap Voir
                    reponseAgent = reader.getTotalHandicapVoirByMen(model.MenageId).ToString();
                    reponseSup = ce_service.getTotalHandicapVoirByMen(model).ToString();
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre de personnes (5 ans et +) déclarant une limitation dans la vue", "Quantite?", reponseAgent, reponseSup);
                    rapports.Add(rpt);
                    parentReport = rpt;
                    //
                    //Handicap Entendre
                    reponseAgent = reader.getTotalhandicapEntendrebyMen(model.MenageId).ToString();
                    reponseSup = ce_service.getTotalhandicapEntendrebyMen(model).ToString();
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre de personnes (5 ans et +) déclarant une limitation dans l´audition", "Quantite?", reponseAgent, reponseSup);
                    rapports.Add(rpt);
                    parentReport = rpt;
                    //
                    //Handicap Marcher
                    reponseAgent = reader.getTotalHandicapMarcherByMen(model.MenageId).ToString();
                    reponseSup = ce_service.getTotalHandicapMarcherByMen(model).ToString();
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre de personnes (5 ans et +) déclarant une limitation dans la mobilité des membres inférieurs", "Quantite?", reponseAgent, reponseSup);
                    rapports.Add(rpt);
                    parentReport = rpt;
                    //
                    //Handicap Marcher
                    reponseAgent = reader.getTotalhandicapSouvenirByMen(model.MenageId).ToString();
                    reponseSup = ce_service.getTotalhandicapSouvenirByMen(model).ToString();
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre de personnes (5 ans et +) déclarant une limitation dans la concentration", "Quantite?", reponseAgent, reponseSup);
                    rapports.Add(rpt);
                    parentReport = rpt;
                    //
                    //Handicap Soigner
                    reponseAgent = reader.getTotalHandicapSoignerbyMen(model.MenageId).ToString();
                    reponseSup = ce_service.getTotalHandicapSoignerbyMen(model).ToString();
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre de personnes (5 ans et +) déclarant une limitation dans les soins personnels", "Quantite?", reponseAgent, reponseSup);
                    rapports.Add(rpt);
                    parentReport = rpt;
                    //
                    //Handicap Communiquer
                    reponseAgent = reader.getTotalHandicapCommuniquerbyMen(model.MenageId).ToString();
                    reponseSup = ce_service.getTotalHandicapCommuniquerbyMen(model).ToString();
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre de personnes (5 ans et +) déclarant une limitation dans la communication avec autrui", "Quantite?", reponseAgent, reponseSup);
                    rapports.Add(rpt);
                    parentReport = rpt;
                    //

                    #endregion

                    #region ALPHABETISATION  ET ÉDUCATION
                    rpt = new RapportComparaisonModel();
                    rpt.ID = "" + getId();
                    rpt.ParentID = "0";
                    rpt.Type = "3. ALPHABÉTISATION ET ÉDUCATION";
                    rapports.Add(rpt);
                    parentReport = new RapportComparaisonModel();
                    parentReport = rpt;
                    //
                    //Handicap ALPHABÉTISATION
                    reponseAgent = reader.getTotalAnalphabetes15AnsbyMen(model.MenageId).ToString();
                    reponseSup = ce_service.getTotalAnalphabetes15AnsbyMen(model).ToString();
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre de personnes de 15 ans et plus analphabètes", "Quantite?", reponseAgent, reponseSup);
                    rapports.Add(rpt);
                    parentReport = rpt;
                    //
                    //Handicap Frequentation Ecole
                    reponseAgent = reader.getTotalPersonneFrequentantEcoleByMen(model.MenageId).ToString();
                    reponseSup = ce_service.getTotalPersonneFrequentantEcoleByMen(model).ToString();
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre de personnes (6 - 24 ans) fréquentant l´école ou l´université", "Quantite?", reponseAgent, reponseSup);
                    rapports.Add(rpt);
                    parentReport = rpt;
                    //
                    //Niveau Secondaire
                    reponseAgent = reader.getTotalPersonneNiveauSecondairebyMen(model.MenageId).ToString();
                    reponseSup = ce_service.getTotalPersonneNiveauSecondairebyMen(model).ToString();
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre de personnes (6 - 24 ans) ayant un niveau d´études secondaires", "Quantite?", reponseAgent, reponseSup);
                    rapports.Add(rpt);
                    parentReport = rpt;
                    //
                    //Niveau Secondaire
                    reponseAgent = reader.getTotalFormationProByMen(model.MenageId).ToString();
                    reponseSup = ce_service.getTotalFormationProByMen(model).ToString();
                    rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre de personnes (6 - 24 ans) avec formation professionnelle", "Quantite?", reponseAgent, reponseSup);
                    rapports.Add(rpt);
                    parentReport = rpt;
                    //

                    #endregion


                }
                return rapports;
            }
            catch (Exception)
            {

            }
            return null;

        }

        public static List<RapportComparaisonModel> getRprtComparaisonBatiment(BatimentCEModel model)
        {
            try
            {
                List<RapportComparaisonModel> rapports = new List<RapportComparaisonModel>();
                RapportComparaisonModel parentReport = new RapportComparaisonModel();
                RapportComparaisonModel rpt = new RapportComparaisonModel();
                string reponseAgent = null;
                string reponseSup = null;
                tbl_question question = null;
                Users.users.ID = 0;

                ISqliteReader reader = new SqliteReader(getConnectionString(Users.users.DatabasePath, model.SdeId));
                IContreEnqueteService ce_service = new ContreEnqueteService();
                BatimentCEModel batiment_ce = null;
                BatimentModel batiment_mob = null;
                batiment_ce = ce_service.getBatiment(model.BatimentId, model.SdeId);
                batiment_mob = reader.GetBatimentbyId(model.BatimentId);

                rpt.ID = "" + getId();
                rpt.ParentID = "0";
                rpt.Type = "1. BATIMENT";
                rapports.Add(rpt);
                parentReport = rpt;

                //Question B1
                question = reader.getQuestionByNomChamps(Constant.Qb1Etat);
                reponseAgent = reader.getReponse(question.codeQuestion, batiment_mob.Qb1Etat.ToString());
                reponseSup = reader.getReponse(question.codeQuestion, batiment_ce.Qb1Etat.GetValueOrDefault().ToString());
                rpt = getChildNodeForMainReportForChefMenage(parentReport, "Etat", question.libelle, reponseAgent, reponseSup);
                rapports.Add(rpt);
                parentReport = rpt;

                //Question B2
                question = reader.getQuestionByNomChamps(Constant.Qb2Type);
                reponseAgent = reader.getReponse(question.codeQuestion, batiment_mob.Qb2Type.ToString());
                reponseSup = reader.getReponse(question.codeQuestion, batiment_ce.Qb2Type.GetValueOrDefault().ToString());
                rpt = getChildNodeForMainReportForChefMenage(parentReport, "Type", question.libelle, reponseAgent, reponseSup);
                rapports.Add(rpt);
                parentReport = rpt;
                //
                //Question B3
                question = reader.getQuestionByNomChamps(Constant.Qb3NombreEtage);
                reponseAgent = reader.getReponse(question.codeQuestion, batiment_mob.Qb3NombreEtage.ToString());
                reponseSup = reader.getReponse(question.codeQuestion, batiment_ce.Qb3NombreEtage.GetValueOrDefault().ToString());
                rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre Etage", question.libelle, reponseAgent, reponseSup);
                rapports.Add(rpt);
                parentReport = rpt;
                //
                //Question B4
                question = reader.getQuestionByNomChamps(Constant.Qb4MateriauMur);
                reponseAgent = reader.getReponse(question.codeQuestion, batiment_mob.Qb4MateriauMur.ToString());
                reponseSup = reader.getReponse(question.codeQuestion, batiment_ce.Qb4MateriauMur.GetValueOrDefault().ToString());
                rpt = getChildNodeForMainReportForChefMenage(parentReport, "Materiau Mur", question.libelle, reponseAgent, reponseSup);
                rapports.Add(rpt);
                parentReport = rpt;
                //
                //Question B5
                question = reader.getQuestionByNomChamps(Constant.Qb5MateriauToit);
                reponseAgent = reader.getReponse(question.codeQuestion, batiment_mob.Qb5MateriauToit.ToString());
                reponseSup = reader.getReponse(question.codeQuestion, batiment_ce.Qb5MateriauToit.GetValueOrDefault().ToString());
                rpt = getChildNodeForMainReportForChefMenage(parentReport, "Materiau Toit", question.libelle, reponseAgent, reponseSup);
                rapports.Add(rpt);
                parentReport = rpt;
                //
                //Question B6
                question = reader.getQuestionByNomChamps(Constant.Qb6StatutOccupation);
                reponseAgent = reader.getReponse(question.codeQuestion, batiment_mob.Qb6StatutOccupation.ToString());
                reponseSup = reader.getReponse(question.codeQuestion, batiment_ce.Qb6StatutOccupation.GetValueOrDefault().ToString());
                rpt = getChildNodeForMainReportForChefMenage(parentReport, "Statut D'occupation", question.libelle, reponseAgent, reponseSup);
                rapports.Add(rpt);
                parentReport = rpt;
                //
                //Question B7.1
                question = reader.getQuestionByNomChamps(Constant.Qb7Utilisation1);
                reponseAgent = reader.getReponse(question.codeQuestion, batiment_mob.Qb7Utilisation1.ToString());
                reponseSup = reader.getReponse(question.codeQuestion, batiment_ce.Qb7Utilisation1.GetValueOrDefault().ToString());
                rpt = getChildNodeForMainReportForChefMenage(parentReport, "Utilisation 1", question.libelle, reponseAgent, reponseSup);
                rapports.Add(rpt);
                parentReport = rpt;
                //
                //Question B7.2
                question = reader.getQuestionByNomChamps(Constant.Qb7Utilisation2);
                reponseAgent = reader.getReponse(question.codeQuestion, batiment_mob.Qb7Utilisation2.ToString());
                reponseSup = reader.getReponse(question.codeQuestion, batiment_ce.Qb7Utilisation2.GetValueOrDefault().ToString());
                rpt = getChildNodeForMainReportForChefMenage(parentReport, "Utilisation 2", question.libelle, reponseAgent, reponseSup);
                rapports.Add(rpt);
                parentReport = rpt;
                //
                //Question B8.1
                question = reader.getQuestionByNomChamps(Constant.Qb8NbreLogeCollectif);
                reponseAgent = batiment_mob.Qb8NbreLogeCollectif.ToString();
                reponseSup = batiment_ce.Qb8NbreLogeCollectif.GetValueOrDefault().ToString();
                rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre de logements Collectifs", question.libelle, reponseAgent, reponseSup);
                rapports.Add(rpt);
                parentReport = rpt;
                //
                //Question B8.2
                question = reader.getQuestionByNomChamps(Constant.Qb8NbreLogeIndividuel);
                reponseAgent = batiment_mob.Qb8NbreLogeIndividuel.ToString();
                reponseSup = batiment_ce.Qb8NbreLogeIndividuel.GetValueOrDefault().ToString();
                rpt = getChildNodeForMainReportForChefMenage(parentReport, "Nombre de logements individuels", question.libelle, reponseAgent, reponseSup);
                rapports.Add(rpt);
                parentReport = rpt;
                //
                return rapports;
            }
            catch (Exception)
            {

            }
            return null;
        }

        public static List<RapportComparaisonModel> getRprtComparaisonLogement(LogementCEModel model)
        {
            try
            {
                List<RapportComparaisonModel> rapports = new List<RapportComparaisonModel>();
                RapportComparaisonModel parentReport = new RapportComparaisonModel();
                RapportComparaisonModel rpt = new RapportComparaisonModel();
                string reponseAgent = null;
                string reponseSup = null;
                tbl_question question = null;
                Users.users.ID = 0;

                ISqliteReader reader = new SqliteReader(getConnectionString(Users.users.DatabasePath, model.SdeId));
                IContreEnqueteService ce_service = new ContreEnqueteService();
                LogementCEModel log_ce = null;
                LogementModel log_mob = null;
                log_ce = ce_service.getLogementCE(Convert.ToInt32(model.BatimentId), model.SdeId, Convert.ToInt32(model.LogeId));
                log_mob = reader.GetLogementById(model.LogeId);
                rpt.ID = "" + getId();
                rpt.ParentID = "0";
                rpt.Type = "1. LOGEMENT";
                rapports.Add(rpt);
                parentReport = rpt;

                //Quesiton L1
                question = reader.getQuestionByNomChamps(Constant.Qlin1NumeroOrdre);
                reponseAgent = log_mob.Qlin1NumeroOrdre.ToString();
                reponseSup = log_ce.Qlin1NumeroOrdre.GetValueOrDefault().ToString();
                rpt = getChildNodeForMainReportForChefMenage(parentReport, "LIN1- Numero Ordre", "Numero Ordre", reponseAgent, reponseSup);
                rapports.Add(rpt);
                parentReport = rpt;
                //
                //Quesiton L2
                question = reader.getQuestionByNomChamps(Constant.Qlin2StatutOccupation);
                reponseAgent = reader.getReponse(question.codeQuestion, log_mob.Qlin2StatutOccupation.ToString());
                reponseSup = reader.getReponse(question.codeQuestion, log_ce.Qlin2StatutOccupation.GetValueOrDefault().ToString());
                rpt = getChildNodeForMainReportForChefMenage(parentReport, "LIN2- Statut d'Occupation", question.libelle, reponseAgent, reponseSup);
                rapports.Add(rpt);
                parentReport = rpt;
                //
                //Quesiton LIN9
                question = reader.getQuestionByNomChamps(Constant.Qlin6NombrePiece);
                reponseAgent = log_mob.Qlin6NombrePiece.ToString();
                reponseSup = log_ce.Qlin6NombrePiece.GetValueOrDefault().ToString();
                rpt = getChildNodeForMainReportForChefMenage(parentReport, "LIN9- Nombre de Pieces", question.libelle, reponseAgent, reponseSup);
                rapports.Add(rpt);
                parentReport = rpt;
                //
                //Quesiton LIN10
                question = reader.getQuestionByNomChamps(Constant.Qlin7NbreChambreACoucher);
                reponseAgent = log_mob.Qlin7NbreChambreACoucher.ToString();
                reponseSup = log_ce.Qlin7NbreChambreACoucher.GetValueOrDefault().ToString();
                rpt = getChildNodeForMainReportForChefMenage(parentReport, "LIN10- Nombre de Chambres à coucher", question.libelle, reponseAgent, reponseSup);
                rapports.Add(rpt);
                parentReport = rpt;
                //
                ////Quesiton LIN11
                //question = reader.getQuestionByNomChamps(Constant.Qlin11NbreIndividuVivant);
                //reponseAgent = log_mob.nbre.ToString();
                //reponseSup = log_ce.Qlin11NbreIndividuVivant.GetValueOrDefault().ToString();
                //rpt = getChildNodeForMainReportForChefMenage(parentReport, "LIN11- Nombre d'individus", question.libelle, reponseAgent, reponseSup);
                //rapports.Add(rpt);
                //parentReport = rpt;
                ////
                ////Quesiton LIN12
                //question = reader.getQuestionByNomChamps(Constant.Qlin8NbreIndividuDepense);
                //reponseAgent = log_mob.Qlin8NbreIndividuDepense.ToString();
                //reponseSup = log_ce.Qlin8NbreIndividuDepense.GetValueOrDefault().ToString();
                //rpt = getChildNodeForMainReportForChefMenage(parentReport, "LIN12- Nombre de depense", question.libelle, reponseAgent, reponseSup);
                //rapports.Add(rpt);
                //parentReport = rpt;
                //
                //Quesiton LIN13
                question = reader.getQuestionByNomChamps(Constant.Qlin9NbreTotalMenage);
                reponseAgent = log_mob.Qlin9NbreTotalMenage.ToString();
                reponseSup = log_ce.Qlin9NbreTotalMenage.GetValueOrDefault().ToString();
                rpt = getChildNodeForMainReportForChefMenage(parentReport, "LIN13- Nombre de menages", question.libelle, reponseAgent, reponseSup);
                rapports.Add(rpt);
                parentReport = rpt;
                //
                return rapports;
            }
            catch (Exception)
            {

            }
            return null;
            

        }
        public static RapportComparaisonModel getChildNodeForMainReportForChefMenage(RapportComparaisonModel parentRpt,string domaine, string libelleQuestion, string valeurAgent, string valeurSuperviseur)
        {
            try
            {
                RapportComparaisonModel child = new RapportComparaisonModel();
                child.ID = "" + getId();
                child.ParentID = parentRpt.ParentID;
                child.Domaine = domaine;
                child.Question = libelleQuestion;
                child.Agent = valeurAgent;
                child.Superviseur = valeurSuperviseur;
                if (valeurAgent == null)
                    valeurAgent = "";
                if (valeurSuperviseur == null)
                    valeurSuperviseur = "";
                if (valeurAgent.Equals(valeurSuperviseur))
                {
                    child.Comparaison = "OUI";
                }
                else
                {
                    child.Comparaison = "NON";
                }
                return child;
            }
            catch (Exception)
            {

            }
            return null;
        }
        public static int getId()
        {
            Users.users.ID = Users.users.ID + 1;
            return Users.users.ID;
        }

        #endregion
      
    }
}
