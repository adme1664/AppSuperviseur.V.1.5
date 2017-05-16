using DevExpress.Xpf.Editors;
using Ht.Ihsi.Rgph.Utility.Utils;
using Ht.Ihsil.Rgph.App.Superviseur.Exceptions;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using Ht.Ihsil.Rgph.App.Superviseur.viewModels.Contre_enquete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ht.Ihsil.Rgph.App.Superviseur.views.Contre_Enquete
{
    /// <summary>
    /// Logique d'interaction pour frm_save_batiment.xaml
    /// </summary>
    public partial class frm_save_batiment : UserControl
    {
        QuestionViewModel viewModel = new QuestionViewModel();
        QuestionReponseService service = null;
        QuestionsModel questionEnCours = null;
        Thickness thick;
        BatimentCEModel batiment = null;
        ComboBoxEdit comboBox = null;
        TextEdit textbox = null;
        List<ReponseModel> listOfReponses = null;
        List<QuestionsModel> listOfQuestions = null;
        List<QuestionReponseModel> listOfQuestionReponses = null;
        public frm_save_batiment()
        {
            this.DataContext = viewModel;
            service = new QuestionReponseService();
            InitializeComponent();
            thick = new Thickness(10, 55, 0, 0);
            batiment = new BatimentCEModel();
            listOfReponses = new List<ReponseModel>();
            listOfQuestions = new List<QuestionsModel>();
            listOfQuestionReponses = new List<QuestionReponseModel>();
            questionEnCours = viewModel.questionEnCours;
            listOfQuestions.Add(questionEnCours);

        }



        public bool isQuestionExist(QuestionsModel quest, List<QuestionsModel> listOfQuestion)
        {
            foreach (QuestionsModel q in listOfQuestion)
            {
                if (q.CodeQuestion == quest.CodeQuestion)
                {
                    return true;
                }
            }
            return false;
        }
        public bool isQuestionExist(List<QuestionReponseModel> listQRep, QuestionReponseModel qRep)
        {
            foreach (QuestionReponseModel r in listQRep)
            {
                if (qRep.CodeQuestion == r.CodeQuestion)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isReponseCodeUnique(List<QuestionReponseModel> qRep, ReponseModel reponse)
        {
            foreach (QuestionReponseModel rep in qRep)
            {
                if (reponse.CodeUniqueReponse == rep.CodeUniqueReponse)
                {
                    return true;
                }
            }
            return false;
        }
        public bool isReponseCodeUnique(ReponseModel reponse, List<ReponseModel> listOfReponse)
        {
            foreach (ReponseModel rep in listOfReponse)
            {
                if (rep.CodeUniqueReponse == reponse.CodeUniqueReponse)
                {
                    return true;
                }
            }
            return false;
        }


        #region Combobox SelectedIndexChanged
        private void cmb_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            ComboBoxEdit cmb = new ComboBoxEdit();
            TextBlock tb = new TextBlock();

            ReponseModel reponse = comboBox.SelectedItem as ReponseModel;
            if (Utils.IsNotNull(reponse))
            {
                setQuestionAndControls(questionEnCours, reponse, comboBox);

            }
        }

        private void cmb_first_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            ReponseModel reponse = cmb_first.SelectedItem as ReponseModel;
            setQuestionAndControls(questionEnCours, reponse, cmb_first);

        }
        #endregion


        #region Text Event
        public void txt_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ReponseModel rep = null;
            if (e.Key == Key.Enter)
            {
                if (textbox.Text == null || textbox.Text == "")
                {
                    MessageBox.Show("Ou dwe mete yon valè.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    rep = new ReponseModel();
                    rep.CodeReponse = textbox.Text;
                    setQuestionAndControls(questionEnCours, rep, textbox);
                }

            }
        }
        #endregion

        public void setQuestionAndControls(QuestionsModel questionCours, ReponseModel reponse, Control control)
        {
            QuestionReponseModel lastQuestion = new QuestionReponseModel();
            List<QuestionReponseModel> listOfCurrentQuestion = new List<QuestionReponseModel>();
            QuestionReponseModel qrCurrent = new QuestionReponseModel();
            QuestionsModel question = new QuestionsModel();

            if (reponse.CodeUniqueReponse == null || reponse.CodeUniqueReponse == "")//Si la reponse vient d'une Question Saisie on lui l'attribue le code de la question
            {
                lastQuestion.CodeUniqueReponse = questionCours.CodeQuestion;
            }
            else//Si elle vient d'une question de choix, on prend le code unique
            {
                lastQuestion = service.getQuestionReponse(reponse.CodeUniqueReponse);
            }

            if (Utils.IsNotNull(lastQuestion))
            {
                if (isQuestionExist(listOfQuestionReponses, lastQuestion) == true)// si la question a ete deja posee
                {
                    lastQuestion = service.getQuestionReponse(reponse.CodeUniqueReponse);
                    listOfCurrentQuestion = new List<QuestionReponseModel>();
                    listOfCurrentQuestion = service.searchQuestionReponse(lastQuestion.CodeQuestion);
                    qrCurrent = new QuestionReponseModel();
                    for (int i = 0; i < listOfCurrentQuestion.Count; i++)
                    {
                        if (reponse.CodeUniqueReponse == listOfCurrentQuestion.ElementAt(i).CodeUniqueReponse)
                        {
                            qrCurrent = listOfCurrentQuestion.ElementAt(i);
                        }
                    }

                    if (isQuestionExist(listOfQuestionReponses, lastQuestion) == false)
                    {
                        listOfQuestionReponses.Add(qrCurrent);
                    }
                    if (qrCurrent.QSuivant != "" && qrCurrent.QSuivant != null)
                    {
                        question = service.getQuestion(qrCurrent.QSuivant);
                    }
                    else
                    {
                        question = service.getQuestion(questionCours.QSuivant);
                    }
                    Utilities.removeQuestionModel(listOfQuestions, question);
                    if (Utils.IsNotNull(question))
                    {
                        listOfQuestions.Add(question);
                        listOfCurrentQuestion = service.searchQuestionReponse(question.CodeQuestion);
                        if (Utils.IsNotNull(listOfCurrentQuestion))
                        {
                            List<ReponseModel> listOfAnswer = new List<ReponseModel>();
                            foreach (QuestionReponseModel qr in listOfCurrentQuestion)
                            {
                                ReponseModel rep = service.getReponse(qr.CodeUniqueReponse);
                                listOfAnswer.Add(rep);
                            }
                            int index = main_grid.Children.IndexOf(control);
                            int taille = main_grid.Children.Count;
                            main_grid.Children.RemoveRange(index + 1, taille);
                            comboBox = control as ComboBoxEdit;
                            thick = comboBox.Margin;

                            foreach (QuestionsModel q in listOfQuestions)
                            {
                                if (q.CodeQuestion == question.CodeQuestion)
                                {

                                }
                                else
                                {
                                    setContentsOfConntrols(question, thick, listOfAnswer);
                                }
                                break;
                            }

                        }
                    }

                }
                else
                {
                    #region SI IL Y A UN SAUT
                    if (questionCours.EstSautReponse.GetValueOrDefault() == true)
                    {
                        listOfCurrentQuestion = service.searchQuestionReponse(questionCours.CodeQuestion);
                        qrCurrent = new QuestionReponseModel();
                        for (int i = 0; i < listOfCurrentQuestion.Count; i++)
                        {
                            if (reponse.CodeUniqueReponse == listOfCurrentQuestion.ElementAt(i).CodeUniqueReponse)
                            {
                                qrCurrent = listOfCurrentQuestion.ElementAt(i);
                            }
                        }
                        listOfQuestionReponses.Add(qrCurrent);
                        question = service.getQuestion(qrCurrent.QSuivant); //Question suivante
                        if (Utils.IsNotNull(question))
                        {
                            if (isQuestionExist(question, listOfQuestions) == false)
                            {
                                listOfQuestions.Add(question);
                                listOfReponses.Add(reponse);
                                listOfCurrentQuestion = service.searchQuestionReponse(question.CodeQuestion);
                                if (Utils.IsNotNull(listOfCurrentQuestion))
                                {
                                    List<ReponseModel> listOfAnswer = new List<ReponseModel>();
                                    foreach (QuestionReponseModel qr in listOfCurrentQuestion)
                                    {
                                        ReponseModel rep = service.getReponse(qr.CodeUniqueReponse);
                                        listOfAnswer.Add(rep);
                                    }

                                    foreach (QuestionsModel q in listOfQuestions)
                                    {
                                        if (q.CodeQuestion == question.CodeQuestion)
                                        {

                                        }
                                        else
                                        {
                                            setContentsOfConntrols(question, thick, listOfAnswer);
                                        }
                                        break;
                                    }

                                }
                            }

                        }


                    }
                    #endregion

                    #region SINON
                    else
                    {
                        listOfQuestionReponses.Add(lastQuestion);
                       
                        question = service.getQuestion(questionCours.QSuivant); //Question suivante
                        if (Utils.IsNotNull(question))
                        {
                            if (isQuestionExist(question, listOfQuestions) == false)
                            {
                                listOfQuestions.Add(question);
                                List<QuestionReponseModel> listOfQR = service.searchQuestionReponse(question.CodeQuestion);
                                if (Utils.IsNotNull(listOfQR))
                                {
                                    List<ReponseModel> listOfAnswer = new List<ReponseModel>();
                                    foreach (QuestionReponseModel qr in listOfQR)
                                    {
                                        ReponseModel rep = service.getReponse(qr.CodeUniqueReponse);
                                        listOfAnswer.Add(rep);
                                    }
                                    foreach (QuestionsModel q in listOfQuestions)
                                    {
                                        if (q.CodeQuestion == question.CodeQuestion)
                                        {

                                        }
                                        else
                                        {
                                            setContentsOfConntrols(question, thick, listOfAnswer);
                                        }
                                        break;
                                    }

                                }
                            }

                        }
                    }
                    #endregion
                }
            }

        }
        public BatimentCEModel getBatimentModel(ReponseSaisie reponse)
        {
            BatimentCEModel batiment = new BatimentCEModel();
            if (reponse.NomChamps == Constant.Qb1Statut)
            {
                batiment.Qb1Statut = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qb2Etat)
            {
                batiment.Qb2Etat = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qb3Type)
            {
                batiment.Qb3Type = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qb4MateriauMur)
            {
                batiment.Qb4MateriauMur = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qb5MateriauToit)
            {
                batiment.Qb5MateriauToit = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qb8Utilisation1)
            {
                batiment.Qb8Utilisation1 = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qb8Utilisation2)
            {
                if (checkConstraintUtilisation(batiment) == true)
                {
                    MessageBox.Show("Ou dwe chwazi yon lot itilizasyon", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    batiment.Qb8Utilisation2 = Convert.ToByte(reponse.CodeReponse);
                }
            }
            if (reponse.NomChamps == Constant.Qb9NbreLogeCollectif)
            {
                batiment.Qb9NbreLogeCollectif = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qb9NbreLogeIndividuel)
            {
                batiment.Qb9NbreLogeIndividuel = Convert.ToByte(reponse.CodeReponse);
            }
            return batiment;

        }

        public bool checkConstraintUtilisation(BatimentCEModel model)
        {
            if (model.Qb8Utilisation1.GetValueOrDefault() == model.Qb8Utilisation2.GetValueOrDefault())
            {
                return true;
            }
            return false;
        }

        public void siDernierQuestion(QuestionsModel question)
        {

        }
        public void siQuestionEstSaut(QuestionsModel currentQuestion, ReponseModel reponse)
        {
            List<QuestionReponseModel> listOfCurrentQuestion = service.searchQuestionReponse(currentQuestion.CodeQuestion);
            QuestionReponseModel qrCurrent = new QuestionReponseModel();
            for (int i = 0; i < listOfCurrentQuestion.Count; i++)
            {
                if (reponse.CodeUniqueReponse == listOfCurrentQuestion.ElementAt(i).CodeUniqueReponse)
                {
                    qrCurrent = listOfCurrentQuestion.ElementAt(i);
                }
            }
            if (isQuestionExist(listOfQuestionReponses, qrCurrent) == false)
            {
                listOfQuestionReponses.Add(qrCurrent);

            }
            QuestionsModel question = service.getQuestion(qrCurrent.QSuivant); //Question suivante
            if (Utils.IsNotNull(question))
            {
                if (isQuestionExist(question, listOfQuestions) == false)
                {
                    listOfQuestions.Add(question);
                    listOfReponses.Add(reponse);
                    List<QuestionReponseModel> listOfQR = service.searchQuestionReponse(question.CodeQuestion);
                    if (Utils.IsNotNull(listOfQR))
                    {
                        List<ReponseModel> listOfAnswer = new List<ReponseModel>();
                        foreach (QuestionReponseModel qr in listOfQR)
                        {
                            ReponseModel rep = service.getReponse(qr.CodeUniqueReponse);
                            listOfAnswer.Add(rep);
                        }
                        foreach (QuestionsModel q in listOfQuestions)
                        {
                            if (q.CodeQuestion == question.CodeQuestion)
                            {

                            }
                            else
                            {
                                setContentsOfConntrols(question, thick, listOfAnswer);
                            }
                            break;
                        }

                    }
                }

            }


        }

        public void setContentsOfConntrols(QuestionsModel question, Thickness thickSet, List<ReponseModel> listOfAnswer)
        {
            #region TEST SI DERNIERE QUESTION
            if (question.QSuivant == "FIN")//Ajouter le bouton Enregistrer
            {
                TextBlock tb = new TextBlock();
                tb.HorizontalAlignment = HorizontalAlignment.Left;
                tb.VerticalAlignment = VerticalAlignment.Top;
                tb.Width = 505;
                tb.Height = 21;
                tb.Text = question.Libelle;
                tb.Margin = Utilities.getThickness(thickSet);
                tb.FontWeight = FontWeights.Bold;
                main_grid.Children.Add(tb);
                thick = Utilities.getThickness(thickSet);
                if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Choix)
                {
                    ComboBoxEdit cmb = new ComboBoxEdit();
                    cmb.Margin = Utilities.getThickness(thick);
                    cmb.HorizontalAlignment = HorizontalAlignment.Left;
                    cmb.VerticalAlignment = VerticalAlignment.Top;
                    cmb.Width = 505;
                    cmb.Height = 21;
                    cmb.ItemsSource = listOfAnswer;
                    cmb.DisplayMember = "LibelleReponse";
                    main_grid.Children.Add(cmb);
                    comboBox = cmb;
                    thick = Utilities.getThickness(thick);
                    questionEnCours = question;
                    //batiment.Qb1Statut = Convert.ToByte(reponse.CodeReponse);
                }
                else
                {
                    if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Saisie)
                    {
                        TextEdit txt = new TextEdit();
                        txt.Margin = Utilities.getThickness(thick);
                        txt.HorizontalAlignment = HorizontalAlignment.Left;
                        txt.VerticalAlignment = VerticalAlignment.Top;
                        txt.Width = 505;
                        txt.Height = 21;
                        txt.MaskType = MaskType.Numeric;
                        txt.Mask = "n0";
                        main_grid.Children.Add(txt);
                        textbox = txt;
                        thick = Utilities.getThickness(thick);
                        questionEnCours = question;
                    }
                }

                Button btn = new Button();
                btn.HorizontalAlignment = HorizontalAlignment.Left;
                btn.VerticalAlignment = VerticalAlignment.Top;
                btn.Width = 75;
                btn.Height = 26;
                btn.Margin = Utilities.getThickness(thick);
                btn.Content = "Anrejistre";
                main_grid.Children.Add(btn);

            }
            #endregion

            #region SI C'EST PAS DERNIERE QUESTION
            else
            {
                TextBlock tb = new TextBlock();
                tb.HorizontalAlignment = HorizontalAlignment.Left;
                tb.VerticalAlignment = VerticalAlignment.Top;
                tb.Width = 505;
                tb.Height = 21;
                tb.Text = question.Libelle;
                tb.Margin = Utilities.getThickness(thickSet);
                tb.FontWeight = FontWeights.Bold;
                main_grid.Children.Add(tb);
                thick = Utilities.getThickness(thickSet);
                if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Choix)
                {
                    ComboBoxEdit cmb = new ComboBoxEdit();
                    cmb.Margin = Utilities.getThickness(thick);
                    cmb.HorizontalAlignment = HorizontalAlignment.Left;
                    cmb.VerticalAlignment = VerticalAlignment.Top;
                    cmb.Width = 505;
                    cmb.Height = 21;
                    cmb.ItemsSource = listOfAnswer;
                    cmb.DisplayMember = "LibelleReponse";
                    main_grid.Children.Add(cmb);
                    comboBox = cmb;
                    cmb.SelectedIndexChanged += cmb_SelectedIndexChanged;
                    thick = Utilities.getThickness(thick);
                    questionEnCours = question;
                    //batiment.Qb1Statut = Convert.ToByte(reponse.CodeReponse);
                }
                else
                {
                    if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Saisie)
                    {
                        TextEdit txt = new TextEdit();
                        txt.Margin = Utilities.getThickness(thick);
                        txt.HorizontalAlignment = HorizontalAlignment.Left;
                        txt.VerticalAlignment = VerticalAlignment.Top;
                        txt.Width = 505;
                        txt.Height = 21;
                        txt.MaskType = MaskType.Numeric;
                        txt.Mask = "n0";
                        main_grid.Children.Add(txt);
                        textbox = txt;
                        txt.KeyDown += txt_KeyDown;
                        thick = Utilities.getThickness(thick);
                        questionEnCours = question;
                    }
                }
            }
            #endregion

        }

    }
}
