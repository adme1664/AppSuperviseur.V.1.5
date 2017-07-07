using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsi.Rgph.Ressources;
//using Ht.Ihsi.Rgph.Utility.common;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Mapper
{
    public class DataDetailsMapper
    {

        static SqliteDataReaderService reader = new SqliteDataReaderService();


        #region VISUALISATION CONTRE-ENQUETE

        public static List<DataDetails> MapToCE<T>(T obj)
        {
            Logger log = new Logger();
            IQuestionReponseService service = new QuestionReponseService();
            IContreEnqueteService service_ce = null;
            List<QuestionsModel> listQuestions = null;
            if (obj.ToString() == utils.Constant.OBJET_MODEL_BATIMENTCE)
            {
                listQuestions = service.searchQuestion(TypeQuestion.Batiment);
            }
            if (obj.ToString() == utils.Constant.OBJET_MODEL_DECESCE)
            {
                listQuestions = service.searchQuestion(TypeQuestion.Deces);
            }
            if (obj.ToString() == utils.Constant.OBJET_MODEL_INDIVIDUCE)
            {
                listQuestions = service.searchQuestion(TypeQuestion.Individu);
                service_ce = new ContreEnqueteService();
            }
            if (obj.ToString() == utils.Constant.OBJET_MODEL_LOGEMENTCE)
            {
                listQuestions = service.searchQuestion(TypeQuestion.Logement);
            }
            if (obj.ToString() == utils.Constant.OBJET_MODEL_MENAGECE)
            {
                listQuestions = service.searchQuestion(TypeQuestion.Menage);
            }

            List<DataDetails> responses = new List<DataDetails>();
            try
            {
                if (listQuestions != null)
                {
                    foreach (QuestionsModel question in listQuestions)
                    {
                        PropertyInfo property = obj.GetType().GetProperty(question.NomChamps);
                        if (property == null)
                        {

                        }
                        else
                        {
                            if (question.TypeQuestion.GetValueOrDefault() == (int)utils.Constant.TypeQuestionMobile.Choix)
                            {
                                if (question.NomChamps == property.Name)
                                {

                                    if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null)
                                    {
                                        int value = 0;
                                        if (int.TryParse(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString(), out value) == false)
                                        {
                                            DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.CodeQuestion + "-" + question.Libelle.ToLower()), obj.GetType().GetProperty(property.Name).GetValue(obj).ToString(), service.getCategorieQuestion(question.CodeCategorie).CategorieQuestion);
                                            responses.Add(detail);
                                        }
                                        else
                                        {
                                            if (obj.GetType().GetProperty(property.Name).GetValue(obj).ToString() == false.ToString())
                                            {

                                            }
                                            else
                                            {
                                                string codeUnique = question.CodeQuestion + "-" + obj.GetType().GetProperty(property.Name).GetValue(obj).ToString();
                                                ReponseModel reponse = service.getReponse(codeUnique);
                                                if (reponse != null)
                                                {
                                                    DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.CodeQuestion + "-" + question.Libelle.ToLower()), reponse.LibelleReponse, service.getCategorieQuestion(question.CodeCategorie).CategorieQuestion);
                                                    responses.Add(detail);
                                                }

                                            }

                                        }

                                    }
                                }
                            }
                            if (question.TypeQuestion.GetValueOrDefault() == (int)utils.Constant.TypeQuestionMobile.Utilisation)
                            {
                                if (question.NomChamps == property.Name)
                                {
                                    if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null)
                                    {
                                        string utilisation = service.getUtilisation(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).Libelle;
                                        DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.CodeQuestion + "-" + question.Libelle.ToLower()), utilisation);
                                        responses.Add(detail);
                                    }
                                }
                            }
                            if (question.TypeQuestion.GetValueOrDefault() == (int)utils.Constant.TypeQuestionMobile.Saisie)
                            {
                                if (question.NomChamps == property.Name)
                                {
                                    if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null)
                                    {
                                        DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.CodeQuestion + "-" + question.Libelle.ToLower()), obj.GetType().GetProperty(property.Name).GetValue(obj).ToString(), service.getCategorieQuestion(question.CodeCategorie).CategorieQuestion);
                                        responses.Add(detail);
                                    }
                                }
                            }
                            if (question.TypeQuestion.GetValueOrDefault() == (int)utils.Constant.TypeQuestionMobile.Departement)
                            {
                                if (question.NomChamps == property.Name)
                                {
                                    if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null)
                                    {
                                        string departement = service_ce.getDepartement(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).DeptNom;
                                        DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.Libelle), departement);
                                        responses.Add(detail);
                                    }
                                }
                            }
                            if (question.TypeQuestion.GetValueOrDefault() == (int)utils.Constant.TypeQuestionMobile.Commune)
                            {
                                if (question.NomChamps == property.Name)
                                {
                                    if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null)
                                    {
                                        string commune = service_ce.getCommune(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).ComNom;
                                        DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.Libelle), commune);
                                        responses.Add(detail);
                                    }
                                }
                            }
                            if (question.TypeQuestion.GetValueOrDefault() == (int)utils.Constant.TypeQuestionMobile.Pays)
                            {
                                if (question.NomChamps == property.Name)
                                {
                                    if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null)
                                    {
                                        string pays = service_ce.getPays(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).NomPays;
                                        DataDetails detail = new DataDetails(question.Libelle, pays);
                                        responses.Add(detail);
                                    }
                                }
                            }
                            if (question.TypeQuestion.GetValueOrDefault() == (int)utils.Constant.TypeQuestionMobile.Vqse)
                            {
                                if (question.NomChamps == property.Name)
                                {
                                    if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null)
                                    {
                                        string vqse = service_ce.getVqse(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).VqseNom;
                                        DataDetails detail = new DataDetails(question.Libelle, vqse);
                                        responses.Add(detail);
                                    }
                                }
                            }

                        }

                    }
                    return responses;
                }
            }
            catch (Exception ex)
            {
                log.Info("Erreur:=>" + ex.Message);
            }
            return null;
        }
        #endregion

        #region VISUALISATION QUESTION MOBILE

        public static List<DataDetails> MapToMobile<T>(T obj, string sdeID)
        {
            Logger log = new Logger();
            List<tbl_question_module> listOf = new List<tbl_question_module>();
            if (obj.ToString() == Constant.OBJET_MODEL_BATIMENT)
            {
                listOf = reader.listOfQuestionModule(Constant.MODULE_BATIMENT, sdeID);
            }
            if (obj.ToString() == Constant.OBJET_MODEL_LOGEMENT)
            {
                listOf = reader.listOfQuestionModule(Constant.MODULE_LOGEMENT, sdeID);
            }
            if (obj.ToString() == Constant.OBJET_MODEL_MENAGE)
            {
                listOf = reader.listOfQuestionModule(Constant.MODULE_MENAGE, sdeID);
            }
            if (obj.ToString() == Constant.OBJET_MODEL_EMIGRE)
            {
                listOf = reader.listOfQuestionModule(Constant.MODULE_EMIGRE, sdeID);
            }
            if (obj.ToString() == Constant.OBJET_MODEL_DECES)
            {
                listOf = reader.listOfQuestionModule(Constant.MODULE_DECES, sdeID);
            }
            if (obj.ToString() == Constant.OBJET_MODEL_INDIVIDU)
            {
                listOf = reader.listOfQuestionModule(Constant.MODULE_INDIVIDU, sdeID);
            }
            List<DataDetails> reponses = new List<DataDetails>();
            try
            {
                if (listOf.Count != 0)
                {
                    foreach (tbl_question_module qm in listOf)
                    {
                        tbl_question question = reader.getQuestion(qm.codeQuestion, sdeID);
                        if (question != null)
                        {
                            tbl_categorie_question tcq = reader.getCategorie(question.codeCategorie, sdeID);
                            string nomChamps = question.nomChamps.Remove(1).ToUpper() + question.nomChamps.Substring(1);
                            PropertyInfo property = obj.GetType().GetProperty(nomChamps);
                            if (question.typeQuestion == (int)Constant.TypeQuestionMobile.Choix || question.typeQuestion == 4)
                            {
                                if (question.nomChamps.Equals(property.Name, StringComparison.OrdinalIgnoreCase))
                                {
                                    string reponse = "";
                                    object valeur = obj.GetType().GetProperty(property.Name).GetValue(obj);
                                    if (valeur == null)
                                    {
                                    }
                                    else
                                    {
                                        reponse = reader.getReponse(question.codeQuestion, obj.GetType().GetProperty(property.Name).GetValue(obj).ToString(), sdeID);
                                    }
                                    if (question.detailsQuestion != "")
                                    {
                                        if (tcq.categorieQuestion != "" && tcq.detailsCategorie != "")
                                        {
                                            if (obj.ToString() == Constant.OBJET_MODEL_INDIVIDU)
                                            {
                                                IndividuModel ind = obj as IndividuModel;
                                                reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.Qp2APrenom) + "__________________:" + question.detailsQuestion), reponse, tcq.detailsCategorie));
                                            }
                                            else
                                                reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower() + "__________________:" + question.detailsQuestion), reponse, tcq.detailsCategorie));
                                        }
                                        else
                                        {
                                            if (obj.ToString() == Constant.OBJET_MODEL_INDIVIDU)
                                            {
                                                IndividuModel ind = obj as IndividuModel;
                                                reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.Qp2APrenom) + "__________________:" + question.detailsQuestion), reponse, tcq.categorieQuestion));
                                            }
                                            else
                                                reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower() + "__________________:" + question.detailsQuestion), reponse, tcq.categorieQuestion));
                                        }
                                    }
                                    else
                                    {
                                        if (tcq.categorieQuestion != "" && tcq.detailsCategorie != "")
                                        {
                                            if (obj.ToString() == Constant.OBJET_MODEL_INDIVIDU)
                                            {
                                                IndividuModel ind = obj as IndividuModel;
                                                reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.Qp2APrenom)), reponse, tcq.detailsCategorie));
                                            }
                                            else
                                                reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower()), reponse, tcq.detailsCategorie));
                                        }
                                        else
                                        {
                                            if (obj.ToString() == Constant.OBJET_MODEL_INDIVIDU)
                                            {
                                                IndividuModel ind = obj as IndividuModel;
                                                reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.Qp2APrenom)), reponse, tcq.categorieQuestion));
                                            }
                                            else
                                                reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower()), reponse, tcq.categorieQuestion));
                                        }
                                    }
                                }
                            }
                            //
                            //Question Pays
                            if (question.typeQuestion.GetValueOrDefault() == 5)
                            {
                                if (question.nomChamps == property.Name)
                                {
                                    if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null)
                                    {
                                        if (question.detailsQuestion != "")
                                        {
                                            if (tcq.categorieQuestion != "" && tcq.detailsCategorie != "")
                                            {
                                                string pays = reader.Sr.getpays(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).NomPays;
                                                if (obj.ToString() == Constant.OBJET_MODEL_INDIVIDU)
                                                {
                                                    IndividuModel ind = obj as IndividuModel;
                                                    DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.Qp2APrenom) + "__________________:" + question.detailsQuestion), pays, tcq.detailsCategorie);
                                                    reponses.Add(detail);
                                                }
                                                else
                                                    reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower() + "__________________:" + question.detailsQuestion), pays, tcq.detailsCategorie));
                                            }
                                            else
                                            {
                                                string pays = reader.Sr.getpays(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).NomPays;
                                                if (obj.ToString() == Constant.OBJET_MODEL_INDIVIDU)
                                                {
                                                    IndividuModel ind = obj as IndividuModel;
                                                    DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.Qp2APrenom) + "__________________:" + question.detailsQuestion), pays, tcq.categorieQuestion);
                                                    reponses.Add(detail);
                                                }
                                            }
                                        }

                                    }
                                }

                            }
                            //
                            //

                            //
                            //Question Communes
                            if (question.typeQuestion.GetValueOrDefault() == 7)
                            {
                                IndividuModel ind = obj as IndividuModel;
                                if (question.nomChamps == property.Name)
                                {
                                    if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null)
                                    {
                                        if (question.detailsQuestion != "")
                                        {
                                            if (tcq.categorieQuestion != "" && tcq.detailsCategorie != "")
                                            {
                                                string commune = reader.Sr.getCommune(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).ComNom;
                                                DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.Qp2APrenom) + "__________________:" + question.detailsQuestion), commune, tcq.detailsCategorie);
                                                reponses.Add(detail);
                                            }
                                            else
                                            {
                                                string commune = reader.Sr.getCommune(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).ComNom;
                                                DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower() + question.libelle.ToLower().Replace("{0}", ind.Qp2APrenom) + "__________________:" + question.detailsQuestion), commune, tcq.categorieQuestion);
                                                reponses.Add(detail);
                                            }
                                        }
                                        else
                                        {
                                            if (tcq.categorieQuestion != "" && tcq.detailsCategorie != "")
                                            {
                                                string commune = reader.Sr.getCommune(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).ComNom;
                                                DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.Qp2APrenom)), commune, tcq.detailsCategorie);
                                                reponses.Add(detail);
                                            }
                                            else
                                            {
                                                string commune = reader.Sr.getCommune(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).ComNom;
                                                DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.Qp2APrenom)), commune, tcq.categorieQuestion);
                                                reponses.Add(detail);
                                            }
                                        }

                                    }
                                }

                            }
                            //
                            //

                            //
                            //Question Communes
                            if (question.typeQuestion.GetValueOrDefault() == 8)
                            {
                                if (question.nomChamps == property.Name)
                                {
                                    if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null)
                                    {
                                        if (question.detailsQuestion != "")
                                        {
                                            if (tcq.categorieQuestion != "" && tcq.detailsCategorie != "")
                                            {
                                                string domaine = reader.Sr.getDomaine(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).NomDomaine;
                                                DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower()), domaine, tcq.detailsCategorie);
                                                reponses.Add(detail);
                                            }
                                            else
                                            {
                                                string domaine = reader.Sr.getDomaine(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).NomDomaine;
                                                DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower()), domaine, tcq.categorieQuestion);
                                                reponses.Add(detail);
                                            }
                                        }

                                    }
                                }

                            }
                            //
                            //

                            if (question.typeQuestion == (int)Constant.TypeQuestionMobile.Saisie || question.typeQuestion == 22 || question.typeQuestion == 13 || question.typeQuestion == 19)
                            {
                                if (question.nomChamps == property.Name)
                                {
                                    if (question.detailsQuestion != "")
                                    {
                                        if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null)
                                        {
                                            if (tcq.categorieQuestion != "" && tcq.detailsCategorie != "")
                                            {
                                                reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower() + "__________________:" + question.detailsQuestion), obj.GetType().GetProperty(property.Name).GetValue(obj).ToString(), tcq.detailsCategorie));
                                            }
                                            else
                                            {
                                                reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower() + "__________________:" + question.detailsQuestion), obj.GetType().GetProperty(property.Name).GetValue(obj).ToString(), tcq.categorieQuestion));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (tcq.categorieQuestion != "" && tcq.detailsCategorie != "")
                                        {
                                            string reponse = "";
                                            if (obj.GetType().GetProperty(property.Name).GetValue(obj) == null)
                                                reponse = "";
                                            else
                                                reponse = obj.GetType().GetProperty(property.Name).GetValue(obj).ToString();

                                            reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower()), reponse, tcq.detailsCategorie));
                                        }
                                        else
                                        {
                                            string reponse = "";
                                            if (obj.GetType().GetProperty(property.Name).GetValue(obj).ToString() == null)
                                                reponse = "";
                                            else
                                                reponse = obj.GetType().GetProperty(property.Name).GetValue(obj).ToString();
                                            reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower()), reponse, tcq.categorieQuestion));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info("Erreur:" + ex.Message);
            }
            return reponses;
        }
        #endregion
    }

}
