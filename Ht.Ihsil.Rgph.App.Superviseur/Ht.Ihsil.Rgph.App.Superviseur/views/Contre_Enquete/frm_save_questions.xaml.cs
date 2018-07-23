using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsi.Rgph.Utility.Utils;
using Ht.Ihsil.Rgph.App.Superviseur.Exceptions;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using Ht.Ihsil.Rgph.App.Superviseur.viewModels.ContreEnquete;
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
    public partial class frm_save_questions : UserControl
    {

        #region DECLARATIONS
        QuestionViewModel viewModel = new QuestionViewModel();
        QuestionReponseService service = null;
        ContreEnqueteService contreEnqueteService = null;
        QuestionsModel questionEnCours = null;
        Thickness thick;
        BatimentCEModel batiment = null;
        LogementCEModel logement = null;
        MenageCEModel menage = null;
        DecesCEModel deces = null;
        EmigreCEModel emigre = null;
        ReponseSaisie reponseSaisie = null;
        ComboBox comboBox = null;
        TextEdit textbox = null;
        List<ReponseModel> listOfReponses = null;
        List<QuestionsModel> listOfQuestions = null;
        Logger log = null;
        List<QuestionReponseModel> listOfQuestionReponses = null;
        DateTime dateStart;
        ISqliteDataWriter sw;
        DataGrid dtg1 = null;

        BatimentCEViewModel batViewModel = null;
        LogementCEViewModel logViewModel = null;
        MenageCEViewModel menViewModel = null;
        MenageDetailsViewModel detailsViewModel = null;
        #endregion

        #region Constructors
        public frm_save_questions(BatimentCEViewModel viewModel)
        {
            if (viewModel != null)
            {
                batiment = viewModel.Batiment;
                batViewModel = viewModel;
            }
                
            QuestionViewModel viewModelBatiment = new QuestionViewModel(TypeQuestion.Batiment);
            this.DataContext = viewModelBatiment;
            log = new Logger();
            service = new QuestionReponseService();
            contreEnqueteService = new ContreEnqueteService();
            reponseSaisie = new ReponseSaisie();
            InitializeComponent();
            thick = new Thickness(10, 55, 0, 0);
            comboBox = new ComboBox();
            textbox = new TextEdit();
            TextBlock tHeader = new TextBlock();
            tHeader.Text = "BATIMAN " + batiment.BatimentId + "/SDE " + batiment.SdeId;
            tHeader.FontWeight = FontWeights.Bold;
            tHeader.Foreground = Brushes.Red;
            grp.Header = tHeader;
            listOfReponses = new List<ReponseModel>();
            listOfQuestions = new List<QuestionsModel>();
            listOfQuestionReponses = new List<QuestionReponseModel>();
            questionEnCours = viewModelBatiment.questionEnCours;
            listOfQuestions.Add(questionEnCours);
            dateStart = DateTime.Now;
            sw = new SqliteDataWriter();
        }
        public frm_save_questions(LogementCEViewModel viewModel)
        {
            if (viewModel != null)
            {
                logViewModel = viewModel;
                logement = viewModel.Logement;
                
            }
            QuestionViewModel viewModelLogement = new QuestionViewModel(TypeQuestion.Logement,logement);
            this.DataContext = viewModelLogement;
            log = new Logger();
            service = new QuestionReponseService();
            contreEnqueteService = new ContreEnqueteService();
            reponseSaisie = new ReponseSaisie();
            InitializeComponent();
            thick = new Thickness(10, 55, 0, 0);
            comboBox = new ComboBox();
            textbox = new TextEdit();
            TextBlock tHeader = new TextBlock();
            tHeader.Foreground = Brushes.Red;
            tHeader.Text = "BATIMAN " + logViewModel.BatimentId + "/ LOJMAN-" + logement.Qlin1NumeroOrdre + "/SDE " + logement.SdeId;
            tHeader.FontWeight = FontWeights.Bold;
            grp.Header = tHeader;
            listOfReponses = new List<ReponseModel>();
            listOfQuestions = new List<QuestionsModel>();
            listOfQuestionReponses = new List<QuestionReponseModel>();
            questionEnCours = viewModelLogement.questionEnCours;
            listOfQuestions.Add(questionEnCours);
            dateStart = DateTime.Now;
            sw = new SqliteDataWriter();

        }
        public frm_save_questions(MenageCEViewModel viewModel)
        {
            if (viewModel != null)
            {
                menViewModel = viewModel;
                menage = viewModel.Menage;
            }
            QuestionViewModel viewModelMenage = new QuestionViewModel(TypeQuestion.Menage);
            this.DataContext = viewModelMenage;
            log = new Logger();
            service = new QuestionReponseService();
            contreEnqueteService = new ContreEnqueteService();
            reponseSaisie = new ReponseSaisie();
            InitializeComponent();
            thick = new Thickness(10, 55, 0, 0);
            comboBox = new ComboBox();
            textbox = new TextEdit();
            TextBlock tHeader = new TextBlock();
            tHeader.Foreground = Brushes.Red;
            tHeader.Text = "BATIMAN " + menage.BatimentId + "/ LOJMAN-" + menage.LogeId + "/MENAJ-" + menage.Qm1NoOrdre + "/SDE " + menage.SdeId;
            tHeader.FontWeight = FontWeights.Bold;
            grp.Header = tHeader;
            listOfReponses = new List<ReponseModel>();
            listOfQuestions = new List<QuestionsModel>();
            listOfQuestionReponses = new List<QuestionReponseModel>();
            questionEnCours = viewModelMenage.questionEnCours;
            listOfQuestions.Add(questionEnCours);
            dateStart = DateTime.Now;
            sw = new SqliteDataWriter();
            dtg1 = new DataGrid();
            //dtg1.Margin = new Thickness(10, 24, 10, 10);


        }
        public frm_save_questions(MenageDetailsViewModel viewModel)
        {
            contreEnqueteService = new ContreEnqueteService();
            if (viewModel != null)
            {
                detailsViewModel = viewModel;
                if (detailsViewModel.Type == (int)Constant.CODE_TYPE_DECES)
                {
                    deces = contreEnqueteService.getDecesCEModel(Convert.ToInt32(detailsViewModel.MenageType.Id), detailsViewModel.MenageType.SdeId);
                }
                if (detailsViewModel.Type == (int)Constant.CODE_TYPE_EMIGRE)
                {
                    emigre = contreEnqueteService.getEmigreCEModel(Convert.ToInt32(detailsViewModel.MenageType.Id), detailsViewModel.MenageType.SdeId);
                }
            }
            QuestionViewModel viewModelEmigre = new QuestionViewModel(TypeQuestion.Emigre);
            this.DataContext = viewModelEmigre;
            log = new Logger();
            service = new QuestionReponseService();
            reponseSaisie = new ReponseSaisie();
            InitializeComponent();
            thick = new Thickness(10, 55, 0, 0);
            comboBox = new ComboBox();
            textbox = new TextEdit();
            TextBlock tHeader = new TextBlock();
            tHeader.Foreground = Brushes.Red;
            tHeader.Text = "BATIMAN " + deces.BatimentId + "/ LOJMAN-" + deces.LogeId + "/MENAJ-" + deces.MenageId + "/Emigre-" + emigre.Qn1numeroOrdre + "/SDE " + deces.SdeId;
            tHeader.FontWeight = FontWeights.Bold;
            grp.Header = tHeader;
            listOfReponses = new List<ReponseModel>();
            listOfQuestions = new List<QuestionsModel>();
            listOfQuestionReponses = new List<QuestionReponseModel>();
            questionEnCours = viewModelEmigre.questionEnCours;
            listOfQuestions.Add(questionEnCours);
            dateStart = DateTime.Now;
            sw = new SqliteDataWriter();
        }

        #endregion

        #region TESTING QUESTIONS
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
        #endregion


        #region EVENT ON CONTROLS

        #region Combobox SelectedIndexChanged
        private void cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = new ComboBox();
            TextBlock tb = new TextBlock();

            ReponseModel reponse = (sender as ComboBox).SelectedItem as ReponseModel;
            comboBox = sender as ComboBox;
            try
            {
                if (Utils.IsNotNull(reponse))
                {
                    if (Utils.IsNotNull(batiment))
                    {
                        setQuestionAndControls(questionEnCours, reponse, comboBox);
                    }
                    else
                    {
                        if (Utils.IsNotNull(logement))
                        {

                            setQuestionAndControls(questionEnCours, reponse, comboBox);

                        }
                        else
                        {
                            if (Utils.IsNotNull(menage))
                            {
                               
                                setQuestionAndControls(questionEnCours, reponse, comboBox);
                            }
                            else
                            {
                                if (Utils.IsNotNull(deces))
                                {
                                    setQuestionAndControls(questionEnCours, reponse, comboBox);
                                }
                            }
                        }
                    }

                }
            }

            catch (Exception)
            {

            }
        }

        private void cmb_first_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReponseModel reponse = cmb_first.SelectedItem as ReponseModel;
            setQuestionAndControls(questionEnCours, reponse, cmb_first);

        }
        #endregion


        #region Text Event
        public void txt_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                ReponseModel rep = null;
                if (e.Key == Key.Enter)
                {
                    textbox = sender as TextEdit;
                    if (textbox.Text == null || textbox.Text == "")
                    {
                        MessageBox.Show("Ou dwe mete yon valè.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        rep = new ReponseModel();
                        textbox = sender as TextEdit;
                        string uniqueId = textbox.Uid.ToString();
                        if (uniqueId != "")
                        {
                            rep.CodeQuestion = uniqueId;
                            rep.CodeReponse = textbox.Text;
                            questionEnCours = service.getQuestion(rep.CodeQuestion);

                        }
                        else
                        {
                            rep.CodeReponse = textbox.Text;
                            textbox.Uid = questionEnCours.CodeQuestion;
                            rep.CodeQuestion = questionEnCours.CodeQuestion;
                        }
                        if (Utils.IsNotNull(logement))
                        {
                            setQuestionAndControls(questionEnCours, rep, textbox);
                        }
                        else
                        {
                            if (Utils.IsNotNull(menage))
                            {
                                setQuestionAndControls(questionEnCours, rep, textbox);
                            }
                            else
                            {
                                if (Utils.IsNotNull(batiment))
                                {
                                    try
                                    {
                                        if (questionEnCours.TypeQuestion == (int)Constant.TypeQuestionMobile.Choix)
                                        {
                                            setQuestionAndControls(questionEnCours, rep, comboBox);
                                        }
                                        else
                                        {
                                            setQuestionAndControls(questionEnCours, rep, textbox);

                                        }
                                    }
                                    catch (MessageException ex)
                                    {

                                    }
                                }
                                else
                                {
                                    if (Utils.IsNotNull(deces))
                                    {
                                        setQuestionAndControls(questionEnCours, rep, textbox);
                                    }

                                }

                            }

                        }

                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (Utils.IsNotNull(batiment))
            {
                setLastQuestionBeforeSave<BatimentCEModel>(batiment);
            }
            else
            {
                if (Utils.IsNotNull(logement))
                {
                    setLastQuestionBeforeSave<LogementCEModel>(logement);
                }
                else
                {
                    if (Utils.IsNotNull(menage))
                    {
                        setLastQuestionBeforeSave<MenageCEModel>(menage);
                    }
                    else
                    {
                        if (Utils.IsNotNull(deces))
                        {
                            setLastQuestionBeforeSave<DecesCEModel>(deces);
                        }
                    }
                }
            }

        }
        #endregion

        #region QUESTIONS AND CONTROLS
        public void setQuestionAndControls(QuestionsModel questionCours, ReponseModel reponse, Control control)
        {
            try
            {
                QuestionReponseModel lastQuestion = new QuestionReponseModel();
                List<QuestionReponseModel> listOfCurrentQuestion = new List<QuestionReponseModel>();
                QuestionReponseModel qrCurrent = new QuestionReponseModel();
                QuestionsModel question = new QuestionsModel();
                reponseSaisie.NomChamps = questionCours.NomChamps;

                reponseSaisie.CodeReponse = reponse.CodeReponse;
                if (questionCours.NomObjet == "Logement")
                {
                    this.logement = getLogementModel(reponseSaisie, logement);
                }
                if (questionCours.NomObjet == "Batiment")
                {
                    batiment = getBatimentModel(reponseSaisie, batiment);

                }
                if (questionCours.NomObjet == "Menage")
                {
                    this.menage = getMenageModel(reponseSaisie, menage);
                }
                if (questionCours.NomObjet == "Deces")
                {
                    this.deces = getDecesModel(reponseSaisie, deces);
                }
                bool isCategorie = false;
                //if (reponse.CodeUniqueReponse == null || reponse.CodeUniqueReponse == "")//Si la reponse vient d'une Question Saisie on lui l'attribue le code de la question
                if (questionCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Saisie)
                {
                    lastQuestion.CodeUniqueReponse = reponse.CodeUniqueReponse;
                    lastQuestion.CodeQuestion = reponse.CodeQuestion;
                }
                if (questionCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Utilisation)
                {
                    lastQuestion.CodeQuestion = reponse.CodeQuestion;
                }
                if (questionCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Choix)//Si elle vient d'une question de choix, on prend le code unique
                {
                    if (questionCours.NomChamps == Constant.Qb7GrandeUtilisation2)
                    {
                        lastQuestion.CodeQuestion = "B7.2";
                    }
                    else
                    {
                        lastQuestion = service.getQuestionReponse(reponse.CodeUniqueReponse);
                    }
                }

                if (Utils.IsNotNull(lastQuestion))
                {
                    #region SI LA QUESTION A ETE DEJA POSEE
                    if (isQuestionExist(listOfQuestionReponses, lastQuestion) == true)// si la question a ete deja posee
                    {
                        listOfCurrentQuestion = new List<QuestionReponseModel>();
                        listOfCurrentQuestion = service.searchQuestionReponse(lastQuestion.CodeQuestion);
                        qrCurrent = new QuestionReponseModel();
                        if (listOfCurrentQuestion.Count == 0)// si la question posee est une question saisie
                        {
                            question = service.getQuestion(questionCours.QSuivant);
                        }
                        else
                        {
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
                            if (qrCurrent.QSuivant != "" && qrCurrent.QSuivant != "FIN")
                            {
                                if (questionCours.NomObjet == "Batiment")
                                {
                                    reponseSaisie.NomChamps = service.getQuestion(qrCurrent.CodeQuestion).NomChamps;
                                    reponseSaisie.CodeReponse = reponse.CodeReponse;
                                    batiment = getBatimentModel(reponseSaisie, batiment);
                                    question = service.getQuestion(qrCurrent.QSuivant);
                                }
                                if (questionCours.NomObjet == "Logement")
                                {
                                    reponseSaisie.NomChamps = service.getQuestion(qrCurrent.CodeQuestion).NomChamps;
                                    reponseSaisie.CodeReponse = reponse.CodeReponse;
                                    logement = getLogementModel(reponseSaisie, logement);
                                    question = service.getQuestion(qrCurrent.QSuivant);
                                }
                                if (questionCours.NomObjet == "Menage")
                                {
                                    reponseSaisie.NomChamps = service.getQuestion(qrCurrent.CodeQuestion).NomChamps;
                                    reponseSaisie.CodeReponse = reponse.CodeReponse;
                                    menage = getMenageModel(reponseSaisie, menage);
                                    question = service.getQuestion(qrCurrent.QSuivant);
                                }
                                if (questionCours.NomObjet == "Deces")
                                {
                                    reponseSaisie.NomChamps = service.getQuestion(qrCurrent.CodeQuestion).NomChamps;
                                    reponseSaisie.CodeReponse = reponse.CodeReponse;
                                    deces = getDecesModel(reponseSaisie, deces);
                                    question = service.getQuestion(qrCurrent.QSuivant);
                                }
                                if (questionCours.NomObjet == "Emigre")
                                {
                                    reponseSaisie.NomChamps = service.getQuestion(qrCurrent.CodeQuestion).NomChamps;
                                    reponseSaisie.CodeReponse = reponse.CodeReponse;
                                    deces = getDecesModel(reponseSaisie, deces);
                                    question = service.getQuestion(qrCurrent.QSuivant);
                                }

                            }
                            else
                            {
                                if (qrCurrent.QSuivant == "FIN")
                                {
                                    int index = main_grid.Children.IndexOf(control);
                                    int taille = main_grid.Children.Count;
                                    main_grid.Children.RemoveRange(index + 1, taille);
                                    questionCours = service.getQuestion(qrCurrent.CodeQuestion);
                                    if (questionCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Choix)
                                    {
                                        comboBox = control as ComboBox;
                                        thick = comboBox.Margin;
                                    }
                                    else
                                    {
                                        textbox = control as TextEdit;
                                        thick = textbox.Margin;
                                    }

                                    Button btn = new Button();
                                    btn.HorizontalAlignment = HorizontalAlignment.Left;
                                    btn.VerticalAlignment = VerticalAlignment.Top;
                                    btn.Width = 85;
                                    btn.Height = 26;
                                    btn.Margin = Utilities.getThickness(thick);
                                    btn.Content = "Anrejistre";
                                    questionEnCours = questionCours;
                                    main_grid.Children.Add(btn);
                                    btn.Click += btn_Click;
                                }
                                else
                                {
                                    questionCours = service.getQuestion(qrCurrent.CodeQuestion);
                                    if (questionCours.NomObjet == "Batiment")
                                    {
                                        reponseSaisie.NomChamps = questionCours.NomChamps;
                                        reponseSaisie.CodeReponse = reponse.CodeReponse;
                                        batiment = getBatimentModel(reponseSaisie, batiment);
                                        question = service.getQuestion(questionCours.QSuivant);

                                    }
                                    if (questionCours.NomObjet == "Logement")
                                    {
                                        reponseSaisie.NomChamps = questionCours.NomChamps;
                                        reponseSaisie.CodeReponse = reponse.CodeReponse;
                                        logement = getLogementModel(reponseSaisie, logement);
                                        question = service.getQuestion(questionCours.QSuivant);

                                    }
                                    if (questionCours.NomObjet == "Menage")
                                    {
                                        reponseSaisie.NomChamps = questionCours.NomChamps;
                                        reponseSaisie.CodeReponse = reponse.CodeReponse;
                                        menage = getMenageModel(reponseSaisie, menage);
                                        question = service.getQuestion(questionCours.QSuivant);
                                    }
                                    if (questionCours.NomObjet == "Deces")
                                    {
                                        reponseSaisie.NomChamps = questionCours.NomChamps;
                                        reponseSaisie.CodeReponse = reponse.CodeReponse;
                                        deces = getDecesModel(reponseSaisie, deces);
                                        question = service.getQuestion(questionCours.QSuivant);
                                    }
                                    if (questionCours.NomObjet == "Emigre")
                                    {
                                        reponseSaisie.NomChamps = questionCours.NomChamps;
                                        reponseSaisie.CodeReponse = reponse.CodeReponse;
                                        deces = getDecesModel(reponseSaisie, deces);
                                        question = service.getQuestion(questionCours.QSuivant);
                                    }

                                }
                            }

                        }
                        Utilities.removeQuestionModel(listOfQuestions, question);
                        if (question.CodeQuestion != null)
                        {
                            List<ReponseModel> listOfAnswer = new List<ReponseModel>();
                            listOfQuestions.Add(question);
                            if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Utilisation)
                            {
                                question.QPrecedent = reponse.CodeUniqueReponse;
                                listOfAnswer = setAnswers(question);
                            }
                            else
                            {
                                if (question.NomChamps == Constant.Qb7GrandeUtilisation2)
                                {
                                    listOfCurrentQuestion = service.searchQuestionReponse("B7.2");
                                }
                                else
                                {
                                    listOfCurrentQuestion = service.searchQuestionReponse(question.CodeQuestion);
                                }
                                if (listOfCurrentQuestion.Count == 0)
                                {

                                }
                                else
                                {
                                    foreach (QuestionReponseModel qr in listOfCurrentQuestion)
                                    {

                                        if (qr.EstEnfant.GetValueOrDefault() == true)
                                        {
                                            isCategorie = true;
                                            ReponseModel rep = service.getReponse(qr.CodeUniqueReponse);
                                            rep.CodeQuestion = qr.CodeQuestion;
                                            rep.Category = service.getReponse(qr.CodeParent).LibelleReponse;
                                            listOfAnswer.Add(rep);
                                        }
                                        else
                                        {

                                            if (qr.CodeParent == "")
                                            {
                                                ReponseModel rep = new ReponseModel();
                                                if (question.NomChamps == Constant.Qb7GrandeUtilisation1)
                                                {
                                                    if (service.getReponse(qr.CodeUniqueReponse).CodeReponse != "0")
                                                    {
                                                        rep = service.getReponse(qr.CodeUniqueReponse);
                                                        rep.CodeQuestion = qr.CodeQuestion;
                                                        listOfAnswer.Add(rep);
                                                    }
                                                }
                                                else
                                                {
                                                    rep = service.getReponse(qr.CodeUniqueReponse);
                                                    rep.CodeQuestion = qr.CodeQuestion;
                                                    listOfAnswer.Add(rep);
                                                }

                                            }

                                        }

                                    }
                                }
                            }

                            //On efface les controles sous adjacents
                            int index = main_grid.Children.IndexOf(control);
                            int taille = main_grid.Children.Count;
                            main_grid.Children.RemoveRange(index + 1, taille);
                            if (questionCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Choix || questionCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Utilisation)
                            {
                                comboBox = control as ComboBox;
                                thick = comboBox.Margin;
                            }
                            else
                            {
                                textbox = control as TextEdit;
                                thick = textbox.Margin;
                            }
                            foreach (QuestionsModel q in listOfQuestions)
                            {
                                if (q.CodeQuestion == question.CodeQuestion)
                                {

                                }
                                else
                                {
                                    setContentsOfConntrols(question, thick, listOfAnswer, isCategorie);
                                }
                                break;
                            }
                        }

                    }
                    #endregion
                    #region SINON
                    else
                    {
                        #region SI IL Y A UN SAUT
                        if (questionCours.EstSautReponse.GetValueOrDefault() == true)
                        {
                            if (questionCours.NomChamps == Constant.Qb7GrandeUtilisation2)
                            {
                                listOfCurrentQuestion = service.searchQuestionReponse("B7.2");
                            }
                            else
                            {
                                listOfCurrentQuestion = service.searchQuestionReponse(questionCours.CodeQuestion);
                            }
                            qrCurrent = new QuestionReponseModel();
                            for (int i = 0; i < listOfCurrentQuestion.Count; i++)
                            {
                                if (reponse.CodeUniqueReponse == listOfCurrentQuestion.ElementAt(i).CodeUniqueReponse)
                                {
                                    qrCurrent = listOfCurrentQuestion.ElementAt(i);
                                }
                            }
                            listOfQuestionReponses.Add(qrCurrent);
                            if (qrCurrent.QSuivant == "FIN")
                            {
                                if (questionCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Choix)
                                {
                                    comboBox = control as ComboBox;
                                }
                                else
                                {
                                    textbox = control as TextEdit;
                                }
                                if (questionCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Grid)
                                {

                                }
                                Button btn = new Button();
                                btn.HorizontalAlignment = HorizontalAlignment.Left;
                                btn.VerticalAlignment = VerticalAlignment.Top;
                                btn.Width = 85;
                                btn.Height = 26;
                                btn.Margin = Utilities.getThickness(thick);
                                btn.Content = "Anrejistre";
                                main_grid.Children.Add(btn);
                                btn.Click += btn_Click;
                            }
                            else
                            {

                                question = service.getQuestion(qrCurrent.QSuivant);
                                List<ReponseModel> listOfAnswer = new List<ReponseModel>();//Question suivante
                                if (Utils.IsNotNull(question))
                                {
                                    if (isQuestionExist(question, listOfQuestions) == false)
                                    {
                                        listOfQuestions.Add(question);
                                        listOfReponses.Add(reponse);
                                        if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Utilisation)
                                        {
                                            question.QPrecedent = reponse.CodeUniqueReponse;
                                            listOfAnswer = setAnswers(question);
                                            setContentsOfConntrols(question, thick, listOfAnswer, isCategorie);
                                        }
                                        else
                                        {
                                            listOfCurrentQuestion = service.searchQuestionReponse(question.CodeQuestion);
                                            if (Utils.IsNotNull(listOfCurrentQuestion))
                                            {
                                                foreach (QuestionReponseModel qr in listOfCurrentQuestion)
                                                {
                                                    if (qr.EstEnfant.GetValueOrDefault() == true)
                                                    {
                                                        isCategorie = true;
                                                        ReponseModel rep = service.getReponse(qr.CodeUniqueReponse);
                                                        rep.CodeQuestion = qr.CodeQuestion;
                                                        rep.Category = service.getReponse(qr.CodeParent).LibelleReponse;
                                                        listOfAnswer.Add(rep);
                                                    }
                                                    else
                                                    {
                                                        if (qr.CodeParent == "")
                                                        {
                                                            ReponseModel rep = new ReponseModel();
                                                            if (question.NomChamps == Constant.Qb7GrandeUtilisation1)
                                                            {
                                                                if (service.getReponse(qr.CodeUniqueReponse).CodeReponse != "0")
                                                                {
                                                                    rep = service.getReponse(qr.CodeUniqueReponse);
                                                                    rep.CodeQuestion = qr.CodeQuestion;
                                                                    listOfAnswer.Add(rep);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                rep = service.getReponse(qr.CodeUniqueReponse);
                                                                rep.CodeQuestion = qr.CodeQuestion;
                                                                listOfAnswer.Add(rep);
                                                            }

                                                        }

                                                    }
                                                }

                                                foreach (QuestionsModel q in listOfQuestions)
                                                {
                                                    if (q.CodeQuestion == question.CodeQuestion)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        setContentsOfConntrols(question, thick, listOfAnswer, isCategorie);
                                                    }
                                                    break;
                                                }

                                            }
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
                                    List<ReponseModel> listOfAnswer = new List<ReponseModel>();
                                    listOfQuestions.Add(question);
                                    if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Utilisation)
                                    {
                                        question.QPrecedent = reponse.CodeUniqueReponse;
                                        listOfAnswer = setAnswers(question);
                                        setContentsOfConntrols(question, thick, listOfAnswer, isCategorie);
                                    }
                                    else
                                    {
                                        List<QuestionReponseModel> listOfQR = new List<QuestionReponseModel>();
                                        if (question.NomChamps == Constant.Qb7GrandeUtilisation2)
                                        {
                                            listOfQR = service.searchQuestionReponse("B7.2");
                                        }
                                        else
                                        {
                                            listOfQR = service.searchQuestionReponse(question.CodeQuestion);
                                        }
                                        if (listOfQR.Count != 0)
                                        {
                                            foreach (QuestionReponseModel qr in listOfQR)
                                            {
                                                if (qr.EstEnfant.GetValueOrDefault() == true)
                                                {
                                                    isCategorie = true;
                                                    ReponseModel rep = service.getReponse(qr.CodeUniqueReponse);
                                                    rep.Category = service.getReponse(qr.CodeParent).LibelleReponse;
                                                    listOfAnswer.Add(rep);
                                                }
                                                else
                                                {
                                                    if (qr.CodeParent == "")
                                                    {
                                                        ReponseModel rep = new ReponseModel();
                                                        if (question.NomChamps == Constant.Qb7GrandeUtilisation1)
                                                        {
                                                            if (service.getReponse(qr.CodeUniqueReponse).CodeReponse != "0")
                                                            {
                                                                rep = service.getReponse(qr.CodeUniqueReponse);
                                                                rep.CodeQuestion = qr.CodeQuestion;
                                                                listOfAnswer.Add(rep);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            rep = service.getReponse(qr.CodeUniqueReponse);
                                                            rep.CodeQuestion = qr.CodeQuestion;
                                                            listOfAnswer.Add(rep);
                                                        }

                                                    }

                                                }
                                            }
                                            foreach (QuestionsModel q in listOfQuestions)
                                            {
                                                if (q.CodeQuestion == question.CodeQuestion)
                                                {

                                                }
                                                else
                                                {
                                                    setContentsOfConntrols(question, thick, listOfAnswer, isCategorie);
                                                }
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            #region SI C'EST UN MENAGE
                                            //On charge dans la table individu
                                            if (question.NomObjet == "Menage")
                                            {
                                                setContentsOfConntrols(question, thick, listOfAnswer, isCategorie);
                                            }
                                            #endregion
                                            else
                                            {
                                                setContentsOfConntrols(question, thick, listOfAnswer, isCategorie);
                                            }
                                        }
                                    }

                                }

                            }
                        }
                        #endregion
                    }
                    #endregion
                }

            }
            #region Exceptions
            catch (MessageFinException ex)
            {
                QuestionsModel question = new QuestionsModel();
                question = service.getQuestion("N4d");
                List<ReponseModel> listOfAnswer = new List<ReponseModel>();
                listOfQuestions.Add(question);
                List<QuestionReponseModel> listOfQR = service.searchQuestionReponse(question.CodeQuestion);
                if (listOfQR.Count != 0)
                {
                    foreach (QuestionReponseModel qr in listOfQR)
                    {
                        if (qr.EstEnfant.GetValueOrDefault() == true)
                        {
                            //isCategorie = true;
                            ReponseModel rep = service.getReponse(qr.CodeUniqueReponse);
                            rep.Category = service.getReponse(qr.CodeParent).LibelleReponse;
                            listOfAnswer.Add(rep);
                        }
                        else
                        {
                            if (qr.CodeParent == "")
                            {
                                ReponseModel rep = service.getReponse(qr.CodeUniqueReponse);
                                listOfAnswer.Add(rep);
                            }

                        }
                    }
                    setContentsOfConntrols(question, thick, listOfAnswer, false);
                }
            }
            catch (SautUtilisationException ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                int index = main_grid.Children.IndexOf(control);
                int taille = main_grid.Children.Count;
                main_grid.Children.RemoveRange(index + 1, taille);
                Button btn = new Button();
                btn.HorizontalAlignment = HorizontalAlignment.Left;
                btn.VerticalAlignment = VerticalAlignment.Top;
                btn.Width = 85;
                btn.Height = 26;
                btn.Margin = Utilities.getThickness(thick);
                btn.Content = "Anrejistre";
                main_grid.Children.Add(btn);
                btn.Click += btn_Click;
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion

        }
        public void setContentsOfConntrols(QuestionsModel question, Thickness thickSet, List<ReponseModel> listOfAnswer, bool isCategorie)
        {
            try
            {
                #region TEST SI DERNIERE QUESTION
                if (question.QSuivant == "FIN")//Ajouter le bouton Enregistrer
                {
                    TextBlock tb = new TextBlock();
                    tb.TextWrapping = TextWrapping.Wrap;
                    tb.HorizontalAlignment = HorizontalAlignment.Left;
                    tb.VerticalAlignment = VerticalAlignment.Top;
                    tb.Width = 905;
                    tb.Height = 35;
                    tb.Text = question.Libelle;
                    tb.Margin = Utilities.getThickness(thickSet);
                    tb.FontWeight = FontWeights.Bold;
                    main_grid.Children.Add(tb);
                    thick = Utilities.getThickness(thickSet);
                    if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Choix || question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Utilisation)
                    {
                        if (isCategorie == true)
                        {
                            ComboBox cmb = new ComboBox();
                            cmb.Margin = Utilities.getThickness(thick);
                            cmb.HorizontalAlignment = HorizontalAlignment.Left;
                            cmb.VerticalAlignment = VerticalAlignment.Top;
                            cmb.Width = 505;
                            cmb.Height = 21;

                            var txtTemplate = new FrameworkElementFactory(typeof(TextBlock));
                            txtTemplate.SetBinding(TextBlock.TextProperty, new Binding("Name"));
                            txtTemplate.SetValue(TextBlock.FontWeightProperty, FontWeights.Bold);
                            DataTemplate dt = new DataTemplate() { VisualTree = txtTemplate };
                            GroupStyle gs = new GroupStyle();
                            gs.HeaderTemplate = dt;
                            cmb.GroupStyle.Add(gs);

                            var txt1 = new FrameworkElementFactory(typeof(TextBlock));
                            txt1.SetBinding(TextBlock.TextProperty, new Binding("Name"));
                            DataTemplate dt1 = new DataTemplate() { VisualTree = txt1 };
                            cmb.ItemTemplate = dt1;

                            ListCollectionView lcv = new ListCollectionView(listOfAnswer);
                            lcv.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
                            cmb.ItemsSource = lcv;

                            main_grid.Children.Add(cmb);
                            comboBox = cmb;
                            comboBox.SelectedIndex = -1;
                            thick = Utilities.getThickness(thick);
                            questionEnCours = question;

                        }
                        else
                        {
                            ComboBox cmb = new ComboBox();
                            cmb.Margin = Utilities.getThickness(thick);
                            cmb.HorizontalAlignment = HorizontalAlignment.Left;
                            cmb.VerticalAlignment = VerticalAlignment.Top;
                            cmb.Width = 505;
                            cmb.Height = 21;
                            cmb.ItemsSource = listOfAnswer;
                            cmb.DisplayMemberPath = "LibelleReponse";
                            main_grid.Children.Add(cmb);
                            comboBox = cmb;
                            comboBox.SelectedIndex = -1;
                            thick = Utilities.getThickness(thick);
                            questionEnCours = question;
                        }
                    }

                    else
                    {
                        if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Saisie)
                        {
                            if (question.ContrainteQuestion.GetValueOrDefault() == (int)Constant.Contrainte.Numerique)
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
                            else
                            {
                                TextEdit txt = new TextEdit();
                                txt.Margin = Utilities.getThickness(thick);
                                txt.HorizontalAlignment = HorizontalAlignment.Left;
                                txt.VerticalAlignment = VerticalAlignment.Top;
                                txt.Width = 505;
                                txt.Height = 21;
                                main_grid.Children.Add(txt);
                                textbox = txt;
                                thick = Utilities.getThickness(thick);
                                questionEnCours = question;
                            }
                        }
                        if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Grid)
                        {
                            DataGrid dtg = new DataGrid();
                            dtg.Margin = Utilities.getThickness(thick);
                            dtg.HorizontalAlignment = HorizontalAlignment.Left;
                            dtg.VerticalAlignment = VerticalAlignment.Top;
                            dtg.Width = 505;
                            dtg.Height = 150;
                            List<IndividuCEModel> listOf = contreEnqueteService.searchAllIndividuCE(menage);
                            List<IndModel> models = new List<IndModel>();
                            if (listOf.Count != 0)
                            {
                                foreach (IndividuCEModel ind in listOf)
                                {
                                    IndModel model = new IndModel(ind.Q2Nom, ind.Q3Prenom, ind.Q4Sexe.GetValueOrDefault().ToString(), ind.Q5bAge.GetValueOrDefault().ToString());
                                    models.Add(model);
                                }
                            }
                            dtg.ItemsSource = models;
                            dtg1 = dtg;
                            main_grid.Children.Add(dtg1);
                            thick = Utilities.getThickness(new Thickness(10, 380, 0, 0));
                            questionEnCours = question;
                        }
                    }

                    Button btn = new Button();
                    btn.HorizontalAlignment = HorizontalAlignment.Left;
                    btn.VerticalAlignment = VerticalAlignment.Top;
                    btn.Width = 85;
                    btn.Height = 26;
                    btn.Margin = Utilities.getThickness(thick);
                    btn.Content = "Anrejistre";
                    main_grid.Children.Add(btn);
                    btn.Click += btn_Click;

                }
                #endregion

                #region SI C'EST PAS DERNIERE QUESTION
                else
                {
                    TextBlock tb = new TextBlock();
                    tb.TextWrapping = TextWrapping.Wrap;
                    tb.HorizontalAlignment = HorizontalAlignment.Left;
                    tb.VerticalAlignment = VerticalAlignment.Top;
                    tb.Width = 905;
                    tb.Height = 35;
                    tb.Text = question.Libelle;
                    tb.Margin = Utilities.getThickness(thickSet);
                    tb.FontWeight = FontWeights.Bold;
                    main_grid.Children.Add(tb);
                    thick = Utilities.getThickness(thickSet);
                    if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Choix || question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Utilisation)
                    {
                        if (isCategorie == true)
                        {
                            ComboBox cmb = new ComboBox();
                            cmb.Margin = Utilities.getThickness(thick);
                            cmb.HorizontalAlignment = HorizontalAlignment.Left;
                            cmb.VerticalAlignment = VerticalAlignment.Top;
                            cmb.Width = 505;
                            cmb.Height = 21;

                            var txtTemplate = new FrameworkElementFactory(typeof(TextBlock));
                            txtTemplate.SetBinding(TextBlock.TextProperty, new Binding("Name"));
                            txtTemplate.SetValue(TextBlock.FontWeightProperty, FontWeights.Bold);
                            DataTemplate dt = new DataTemplate() { VisualTree = txtTemplate };
                            GroupStyle gs = new GroupStyle();
                            gs.HeaderTemplate = dt;
                            cmb.GroupStyle.Add(gs);

                            var txt1 = new FrameworkElementFactory(typeof(TextBlock));
                            txt1.SetBinding(TextBlock.TextProperty, new Binding("Name"));
                            DataTemplate dt1 = new DataTemplate() { VisualTree = txt1 };
                            cmb.ItemTemplate = dt1;

                            ListCollectionView lcv = new ListCollectionView(listOfAnswer);
                            lcv.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
                            cmb.ItemsSource = lcv;
                            main_grid.Children.Add(cmb);
                            comboBox = cmb;
                            comboBox.SelectedIndex = -1;
                            thick = Utilities.getThickness(thick);
                            questionEnCours = question;
                            cmb.SelectionChanged += cmb_SelectionChanged;
                        }
                        else
                        {


                            ComboBox cmb = new ComboBox();
                            cmb.Margin = Utilities.getThickness(thick);
                            cmb.HorizontalAlignment = HorizontalAlignment.Left;
                            cmb.VerticalAlignment = VerticalAlignment.Top;
                            cmb.Width = 505;
                            cmb.Height = 21;
                            cmb.ItemsSource = listOfAnswer;
                            cmb.DisplayMemberPath = "LibelleReponse";
                            main_grid.Children.Add(cmb);
                            comboBox = cmb;
                            comboBox.SelectedIndex = -1;
                            thick = Utilities.getThickness(thick);
                            questionEnCours = question;
                            cmb.SelectionChanged += cmb_SelectionChanged;
                        }
                    }
                    else
                    {
                        if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestionMobile.Saisie)
                        {
                            if (question.ContrainteQuestion.GetValueOrDefault() == (int)Constant.Contrainte.Numerique)
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
                            else
                            {
                                TextEdit txt = new TextEdit();
                                txt.Margin = Utilities.getThickness(thick);
                                txt.HorizontalAlignment = HorizontalAlignment.Left;
                                txt.VerticalAlignment = VerticalAlignment.Top;
                                txt.Width = 505;
                                txt.Height = 21;
                                main_grid.Children.Add(txt);
                                textbox = txt;
                                txt.KeyDown += txt_KeyDown;
                                thick = Utilities.getThickness(thick);
                                questionEnCours = question;
                            }

                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        void setLastQuestionBeforeSave<T>(T objet)
        {
            try
            {
                DateTime dateEnd = DateTime.Now;
                #region SI LA DERNIERE QUESTION EST DE TYPE CHOIX
                if (questionEnCours.TypeQuestion.GetValueOrDefault() == Convert.ToInt32(Constant.TypeQuestionMobile.Choix) ||
                    questionEnCours.TypeQuestion.GetValueOrDefault()== Convert.ToInt32(Constant.TypeQuestionMobile.Utilisation ))
                {
                    ReponseModel reponse = comboBox.SelectedItem as ReponseModel;
                    if (reponse.CodeReponse == "")
                    {
                        MessageBox.Show("Ou dwe reponn tout kesyon yo", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        if (objet.ToString() == Constant.OBJET_MODEL_BATIMENTCE)
                        {
                            reponseSaisie.NomChamps = questionEnCours.NomChamps;
                            reponseSaisie.CodeReponse = reponse.CodeReponse;
                            
                            batiment = getBatimentModel(reponseSaisie, batiment);
                            batiment.DureeSaisie = Utilities.getDureeSaisie(dateStart, dateEnd);
                            batiment.IsContreEnqueteMade = true;
                            bool result = contreEnqueteService.updateBatiment(this.batiment);
                            BatimentModel bat = new BatimentModel();
                            bat.BatimentId = this.batiment.BatimentId;
                            bat.IsContreEnqueteMade = true;
                            bat.SdeId = this.batiment.SdeId;
                            bool save=sw.contreEnqueteMade<BatimentModel>(bat, bat.SdeId);
                            if (save == true)
                            {
                                MessageBox.Show("Batiman sa a anregistre.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                //Fire les controles sur l'interface (Le check mark)
                                batViewModel.Status = true;
                                batViewModel.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_anket_fet);
                                batViewModel.ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
                            }
                            else
                            {
                                MessageBox.Show("Erreur lors de la sauvegarde", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            
                        }
                        else
                        {
                            if (objet.ToString() == Constant.OBJET_MODEL_LOGEMENTCE)
                            {
                                reponseSaisie.NomChamps = questionEnCours.NomChamps;
                                reponseSaisie.CodeReponse = reponse.CodeReponse;
                                logement = getLogementModel(reponseSaisie, logement);
                                logement.DureeSaisie = Utilities.getDureeSaisie(dateStart, dateEnd);
                                logement.IsContreEnqueteMade = 1;
                                bool result = contreEnqueteService.updateLogement(this.logement);
                                LogementModel lg = new LogementModel();
                                lg.LogeId = this.logement.LogeId;
                                lg.SdeId = this.logement.SdeId;
                                lg.IsContreEnqueteMade = true;
                                bool save=sw.contreEnqueteMade<LogementModel>(lg, lg.SdeId);
                                if (save && result==true)
                                {
                                    MessageBox.Show("Lojman sa a anregistre.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                    //Fire les controles sur l'interface (Le check mark)
                                    logViewModel.Status = true;
                                    logViewModel.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_anket_fet);
                                    logViewModel.ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
                                }
                                else
                                {
                                    MessageBox.Show("Erreur lors de la sauvegarde", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            else
                            {
                                if (objet.ToString() == Constant.OBJET_MODEL_MENAGECE)
                                {
                                    reponseSaisie.NomChamps = questionEnCours.NomChamps;
                                    reponseSaisie.CodeReponse = reponse.CodeReponse;
                                    menage = getMenageModel(reponseSaisie, menage);
                                    menage.DureeSaisie = Utilities.getDureeSaisie(dateStart, dateEnd);
                                    menage.IsContreEnqueteMade = true;
                                    bool result = contreEnqueteService.updateMenageCE(this.menage);
                                    MenageModel men = new MenageModel();
                                    men.MenageId = this.menage.MenageId;
                                    men.IsContreEnqueteMade = true;
                                    men.SdeId = menage.SdeId;
                                    bool save=sw.contreEnqueteMade<MenageModel>(men, men.SdeId);
                                    if (save && result == true)
                                    {
                                        MessageBox.Show("Menaj sa a anregistre.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                        //Fire les controles sur l'interface (Le check mark)
                                        menViewModel.Status = true;
                                        menViewModel.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_anket_fet);
                                        menViewModel.ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Gen yon erè pandan anregistreman", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                    
                                }
                                else
                                {
                                    if (objet.ToString() == Constant.OBJET_MODEL_DECESCE)
                                    {
                                            reponseSaisie.NomChamps = questionEnCours.NomChamps;
                                            reponseSaisie.CodeReponse = reponse.CodeReponse;
                                            deces = getDecesModel(reponseSaisie, deces);
                                            deces.DureeSaisie = Utilities.getDureeSaisie(dateStart, dateEnd);
                                            deces.IsContreEnqueteMade = 1;
                                            bool result = contreEnqueteService.updateDecesCE(this.deces);
                                            DecesModel dec = new DecesModel();
                                            dec.DecesId = this.deces.DecesId;
                                            dec.SdeId = this.deces.SdeId;
                                            dec.IsContreEnqueteMade = true;
                                            bool save = sw.contreEnqueteMade<DecesModel>(dec, dec.SdeId);
                                            if (save && result == true)
                                            {
                                                MessageBox.Show("Desè sa a anregistre.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                                detailsViewModel.Status = true;
                                                detailsViewModel.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_anket_fet);
                                                detailsViewModel.ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Gen yon erè pandan anregistreman", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                            }   
                                     }
                                }
                            }
                        }
                    }
                }
#endregion

                #region SINON 'CEST UNE QUESTION DE SAISIE DE DONNEES
                else
                {
                    if (textbox.Text == "")
                    {
                        MessageBox.Show("Ou dwe reponn tout kesyon yo", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        if (objet.ToString() == Constant.OBJET_MODEL_BATIMENTCE)
                        {
                            reponseSaisie.NomChamps = questionEnCours.NomChamps;
                            reponseSaisie.CodeReponse = textbox.Text;
                            if(batiment.Qb7Utilisation1==0)
                            batiment = getBatimentModel(reponseSaisie, batiment);
                            if (batiment.Qb8NbreLogeIndividuel.GetValueOrDefault() == 0 && batiment.Qb8NbreLogeCollectif.GetValueOrDefault() == 0)
                            {
                                if (batiment.Qb7Utilisation1.GetValueOrDefault() != 0 && batiment.Qb7Utilisation2.GetValueOrDefault() != 0)
                                {
                                    throw new MessageException("Fok yon n nan repons sa yo diferan de « 0 ».");
                                }
                            }
                            else
                            {
                                batiment.DureeSaisie = Utilities.getDureeSaisie(dateStart, dateEnd);
                                batiment.IsContreEnqueteMade = true;
                                bool result = contreEnqueteService.updateBatiment(this.batiment);
                                BatimentModel bat = new BatimentModel();
                                bat.BatimentId = this.batiment.BatimentId;
                                bat.IsContreEnqueteMade = true;
                                bat.SdeId = this.batiment.SdeId;
                                bool save=sw.contreEnqueteMade<BatimentModel>(bat, bat.SdeId);
                                if (save && result)
                                {
                                    MessageBox.Show("Batiman sa a anregistre", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                    //Fire les controles sur l'interface (Le check mark)
                                    batViewModel.Status = true;
                                    batViewModel.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_anket_fet);
                                    batViewModel.ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
                                }
                                else
                                {
                                    MessageBox.Show("Erreur lors de la sauvegarde", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                
                            }
                        }
                        else
                        {
                            if (objet.ToString() == Constant.OBJET_MODEL_LOGEMENTCE)
                            {
                                reponseSaisie.NomChamps = questionEnCours.NomChamps;
                                reponseSaisie.CodeReponse = textbox.Text;
                                logement = getLogementModel(reponseSaisie, logement);
                                logement.DureeSaisie = Utilities.getDureeSaisie(dateStart, dateEnd);
                                logement.IsContreEnqueteMade = 1;
                                bool result = contreEnqueteService.updateLogement(this.logement);
                                LogementModel lg = new LogementModel();
                                lg.LogeId = this.logement.BatimentId;
                                lg.SdeId = this.logement.SdeId;
                                lg.IsContreEnqueteMade = true;
                                bool save=sw.contreEnqueteMade<LogementModel>(lg, lg.SdeId);
                                if (save && result)
                                {
                                    MessageBox.Show("Lojman sa a anregistre", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                    logViewModel.Status = true;
                                    logViewModel.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_anket_fet);
                                    logViewModel.ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
                                }
                                else
                                {
                                    MessageBox.Show("Erreur lors de la sauvegarde", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                
                            }
                            else
                            {
                                if (objet.ToString() == Constant.OBJET_MODEL_MENAGECE)
                                {
                                    reponseSaisie.NomChamps = questionEnCours.NomChamps;
                                    reponseSaisie.CodeReponse = textbox.Text;
                                    menage = getMenageModel(reponseSaisie, menage);
                                    menage.DureeSaisie = Utilities.getDureeSaisie(dateStart, dateEnd);
                                    menage.IsContreEnqueteMade = true;
                                    bool result = contreEnqueteService.updateMenageCE(this.menage);
                                    MenageModel men = new MenageModel();
                                    men.MenageId = this.menage.MenageId;
                                    men.IsContreEnqueteMade = true;
                                    men.SdeId = this.menage.SdeId;
                                    bool save=sw.contreEnqueteMade<MenageModel>(men, men.SdeId);
                                    if (save && result)
                                    {
                                        MessageBox.Show("Menaj sa a anregistre", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                        menViewModel.Status = true;
                                        menViewModel.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_anket_fet);
                                        menViewModel.ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Erreur lors de la sauvegarde", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                    
                                }
                                else
                                {
                                    if (objet.ToString() == Constant.OBJET_MODEL_DECESCE)
                                    {
                                        reponseSaisie.NomChamps = questionEnCours.NomChamps;
                                        reponseSaisie.CodeReponse = textbox.Text;
                                        deces = getDecesModel(reponseSaisie, deces);
                                        deces.DureeSaisie = Utilities.getDureeSaisie(dateStart, dateEnd);
                                        deces.IsContreEnqueteMade = 1;
                                        bool result = contreEnqueteService.updateDecesCE(this.deces);
                                        DecesModel dec = new DecesModel();
                                        dec.DecesId = this.deces.DecesId;
                                        dec.SdeId = this.deces.SdeId;
                                        dec.IsContreEnqueteMade = true;
                                        bool save=sw.contreEnqueteMade<DecesModel>(dec, dec.SdeId);
                                        if (save && result)
                                        {
                                            MessageBox.Show("Desè sa a anregistre", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                            detailsViewModel.Status = true;
                                            detailsViewModel.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_anket_fet);
                                            detailsViewModel.ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Erreur lors de la sauvegarde", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                        }                                       
                                    }
                                }
                            }

                        }
                    }
                }
                #endregion
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

        }
        #endregion

        public List<ReponseModel> setAnswers(QuestionsModel question)
        {
            List<ReponseModel> listOfAnswers = new List<ReponseModel>();
            List<UtilisationModel> listOf = new List<UtilisationModel>();
            listOf = service.searchUtilisation(question.QPrecedent);
            //if (listOf.Count == 0)
            //{
            //    listOf = service.searchUtlisation(question.CodeQuestion);
            //}
            foreach (UtilisationModel model in listOf)
            {
                ReponseModel rep = new ReponseModel();
                rep.CodeQuestion = question.CodeQuestion;
                rep.CodeReponse = model.CodeUtilisation;
                rep.LibelleReponse = model.Libelle;
                rep.CodeUniqueReponse = model.CodeUtilisation;
                listOfAnswers.Add(rep);
            }
            return listOfAnswers;
        }

        #region SET OBJET MODEL
        public BatimentCEModel getBatimentModel(ReponseSaisie reponse, BatimentCEModel batiment)
        {

            if (reponse.NomChamps == Constant.Qb1Etat)
            {
                batiment.Qb1Etat = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qb2Type)
            {
                batiment.Qb2Type = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qb3NombreEtage)
            {
                batiment.Qb3NombreEtage = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qb4MateriauMur)
            {
                batiment.Qb4MateriauMur = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qb5MateriauToit)
            {
                batiment.Qb5MateriauToit = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qb6StatutOccupation)
            {
                batiment.Qb6StatutOccupation = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qb7Utilisation1)
            {
                batiment.Qb7Utilisation1 = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<BatimentCEModel>(this.batiment);
            }
            if (reponse.NomChamps == Constant.Qb7Utilisation2)
            {
                batiment.Qb7Utilisation2 = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<BatimentCEModel>(this.batiment);
            }
            if (reponse.NomChamps == Constant.Qb8NbreLogeCollectif)
            {
                batiment.Qb8NbreLogeCollectif = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<BatimentCEModel>(this.batiment);
            }
            if (reponse.NomChamps == Constant.Qb6StatutOccupation)
            {
                batiment.Qb6StatutOccupation = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qb3NombreEtage)
            {
                batiment.Qb3NombreEtage = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qb8NbreLogeIndividuel)
            {
                batiment.Qb8NbreLogeIndividuel = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<BatimentCEModel>(this.batiment);
            }
            return batiment;

        }
        public LogementCEModel getLogementModel(ReponseSaisie reponse, LogementCEModel logement)
        {
            if (reponse.NomChamps == Constant.Qlin6NombrePieceETChambreACoucher)
            {
                logement.Qlin6NombrePiece = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<LogementCEModel>(this.logement);
            }
           
            if (reponse.NomChamps == Constant.Qlin2StatutOccupation)
                logement.Qlin2StatutOccupation = Convert.ToByte(reponse.CodeReponse);
            if (reponse.NomChamps == Constant.QlcTypeLogement)
                logement.QlcTypeLogement = Convert.ToByte(reponse.CodeReponse);
            if (reponse.NomChamps == Constant.QlCategLogement)
                logement.QlCategLogement = Convert.ToByte(reponse.CodeReponse);
            if (reponse.NomChamps == Constant.Qlin4TypeLogement)
                logement.Qlin4TypeLogement = Convert.ToByte(reponse.CodeReponse);
            if (reponse.NomChamps == Constant.Qlin5MateriauSol)
                logement.Qlin5MateriauSol = Convert.ToByte(reponse.CodeReponse);
            if (reponse.NomChamps == Constant.Qlin6NombrePieceETChambreACoucher)
            {
                logement.Qlin6NombrePiece = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<LogementCEModel>(this.logement);
            }
            if (reponse.NomChamps == Constant.Qlin7NbreChambreACoucher)
                logement.Qlin7NbreChambreACoucher = Convert.ToByte(reponse.CodeReponse);
            if (reponse.NomChamps == Constant.Qlin8NbreIndividuDepense)
            {
                logement.Qlin8NbreIndividuDepense = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qlin9NbreTotalMenage)
            {
                logement.Qlin9NbreTotalMenage = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<LogementCEModel>(this.logement);
            }
            return logement;
        }
        public MenageCEModel getMenageModel(ReponseSaisie reponse, MenageCEModel menage)
        {
            if (reponse.NomChamps == Constant.Qm2ModeJouissance)
                menage.Qm2ModeJouissance = Convert.ToByte(reponse.CodeReponse);
            if (reponse.NomChamps == Constant.Qm5SrcEnergieCuisson1)
                menage.Qm5SrcEnergieCuisson1 = Convert.ToByte(reponse.CodeReponse);
            if (reponse.NomChamps == Constant.Qm5SrcEnergieCuisson2)
                menage.Qm5SrcEnergieCuisson2 = Convert.ToByte(reponse.CodeReponse);
            if (reponse.NomChamps == Constant.Qm8EndroitBesoinPhysiologique)
                menage.Qm8EndroitBesoinPhysiologique = Convert.ToByte(reponse.CodeReponse);
            if (reponse.NomChamps == Constant.Qm11TotalIndividuVivant)
                menage.Qm11TotalIndividuVivant = Convert.ToByte(reponse.CodeReponse);

            return menage;
        }
        public DecesCEModel getDecesModel(ReponseSaisie reponse, DecesCEModel deces)
        {
            if (reponse.NomChamps == Constant.Qd1Deces)
            {
                deces.Qd1Deces = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qd1NbreDecedeFille)
            {
                deces.Qd1aNbreDecesF = Convert.ToByte(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qd1NbreDecedeGarcon)
            {
                deces.Qd1aNbreDecesG = Convert.ToByte(reponse.CodeReponse);
            }
            return deces;
        }
        #endregion

        #region CONSTRAINTS
        public void checkConstraint<T>(T objectType)
        {
            #region CONSTRAINT ONLY FOR BATIMENT
            if (objectType.ToString() == Constant.OBJET_MODEL_BATIMENTCE)
            {
                //if Qb8Utilisation1 and Qb8Utilisation2 have the same answer
                BatimentCEModel batiment = objectType as BatimentCEModel;
                if (batiment.Qb7Utilisation2.GetValueOrDefault() == batiment.Qb7Utilisation1.GetValueOrDefault())
                {
                    if (batiment.Qb7Utilisation2.GetValueOrDefault() == batiment.Qb7Utilisation2.GetValueOrDefault())
                    {
                        throw new MessageException("Ou dwe chwazi yon lot itilizasyon.");

                    }

                }
                #region CONTRAINTE SUR LES DEUX UTILISATIONS 
                if (this.batiment.Qb7Utilisation1.GetValueOrDefault() != 0)
                {
                    if (this.batiment.Qb7Utilisation1.GetValueOrDefault() >= 20 && this.batiment.Qb7Utilisation1.GetValueOrDefault() <= 26 || this.batiment.Qb7Utilisation1.GetValueOrDefault() >= 30 && this.batiment.Qb7Utilisation1.GetValueOrDefault() <= 41)
                    {
                        throw new SautUtilisationException(Constant.MESSAGE12EXCEPTION);
                    }
                }
                if (this.batiment.Qb7Utilisation2.GetValueOrDefault() != 0)
                {
                    if (this.batiment.Qb7Utilisation2.GetValueOrDefault() >= 20 && this.batiment.Qb7Utilisation2.GetValueOrDefault() <= 26 || this.batiment.Qb7Utilisation2.GetValueOrDefault() >= 30 && this.batiment.Qb7Utilisation2.GetValueOrDefault() <= 41)
                    {
                        throw new SautUtilisationException(Constant.MESSAGE12EXCEPTION);
                    }
                }
                #endregion

                #region CONTRAINTE SUR LE NOMBRE DE LOGEMENT COL OU IND
                //if()
                #endregion

            }

            #endregion

            #region CONSTRAINT ONLY FOR LOGEMENT
            if (objectType.ToString() == Constant.OBJET_MODEL_LOGEMENTCE)
            {
                LogementCEModel logement = objectType as LogementCEModel;
                #region CONTRAINTE SUR LE STATUT D'OCCUPATION DU LOGEMENT
                if (logement.Qlin2StatutOccupation.GetValueOrDefault() != 0)
                {
                    if (logement.Qlin2StatutOccupation.GetValueOrDefault() == 3 || logement.Qlin2StatutOccupation.GetValueOrDefault() == 4)
                    {
                        throw new SautUtilisationException(Constant.MESSAGE12EXCEPTION);
                    }
                }
                #endregion

                #region CONTRAINTE SUR LE NOMBRE DE PIECES ET LE TYPE DE LOGEMENT
                if (logement.Qlin6NombrePiece.GetValueOrDefault() != 0)
                {
                    if (logement.Qlin4TypeLogement.GetValueOrDefault() == 2)
                    {
                        if (logement.Qlin6NombrePiece.GetValueOrDefault() > 1)
                        {
                            throw new MessageException("Paka genyen plis ke yon (1) pyès nan tip de lojman sa a.");
                        }
                    }
                }
                #endregion

                #region NOMBRE DE PIECES ET DE CHAMBRES A COUCHER

                if (logement.Qlin6NombrePiece.GetValueOrDefault() != 0 && logement.Qlin7NbreChambreACoucher.GetValueOrDefault() != 0)
                {
                    if (logement.Qlin7NbreChambreACoucher.GetValueOrDefault() > logement.Qlin6NombrePiece.GetValueOrDefault())
                    {
                        throw new MessageException("Kantite pyès la pa dwe depase kantite chanm pou moun kouche a.");
                    }
                }
                #endregion

                #region NOMBRE DE PIECES !=0
                if (logement.Qlin6NombrePiece.GetValueOrDefault() == 0)
                {
                    throw new MessageException("Paka pa gen pyès nan lojman an. Ekri kantite pyès lojman an genyen.");
                }
                #endregion

                #region LIN13
                if (logement.Qlin8NbreIndividuDepense.GetValueOrDefault() == 1)
                {
                    if(logement.Qlin9NbreTotalMenage.GetValueOrDefault()==0)
                        throw new MessageException("Paka pa gen mennaj nan lojman. Verifye kesyon LIN12");
                }
                #endregion
            }
            #endregion

            #region CONSTRAINT ONLY FRO MENAGE
            //if (objectType.ToString() == Constant.OBJET_MODEL_MENAGECE)
            //{
            //    MenageCEModel Menage = objectType as MenageCEModel;
            //    if (Menage. > 0 && Menage.Qm14NbreIndividuG > 0)
            //    {
            //        int nbre = Menage.Qm14NbreIndividuG.GetValueOrDefault() + Menage.Qm14NbreIndividuF.GetValueOrDefault();
            //        if (Menage.Qm14NbreTotalIndividu > nbre)
            //        {
            //            Menage.Qm14NbreIndividuF = 0;
            //            throw new MessageException("Kantite moun ou mete a (Gason + Fi) twòp piti.");
            //        }
            //        else
            //        {
            //            if (Menage.Qm14NbreTotalIndividu < nbre)
            //            {
            //                Menage.Qm14NbreIndividuF = 0;
            //                throw new MessageException("Kantite moun ou mete a (Gason + Fi) twòp.");
            //            }
            //        }
            //    }
            //    if (Menage.Qm14NbreIndividuG.GetValueOrDefault() > Menage.Qm14NbreTotalIndividu.GetValueOrDefault())
            //    {
            //        Menage.Qm14NbreIndividuG = 0;
            //        throw new MessageException("Kantite gason ou mete a twòp.");
            //    }
            //    if (Menage.Qm14NbreIndividuF.GetValueOrDefault() > Menage.Qm14NbreTotalIndividu.GetValueOrDefault())
            //    {
            //        Menage.Qm14NbreIndividuF = 0;
            //        throw new MessageException("Kantite fi ou mete a twòp.");
            //    }
            //}
            #endregion

           
        }
        #endregion

    }
}
