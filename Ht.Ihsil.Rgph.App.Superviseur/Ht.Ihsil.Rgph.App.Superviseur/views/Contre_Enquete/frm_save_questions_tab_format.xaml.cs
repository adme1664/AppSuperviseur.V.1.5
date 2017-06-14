using DevExpress.Xpf.Editors;
using Ht.Ihsil.Rgph.App.Superviseur.Exceptions;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using Ht.Ihsil.Rgph.App.Superviseur.viewModels.ContreEnquete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ht.Ihsi.Rgph.Utility.Utils;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ht.Ihsi.Rgph.Logging.Logs;

namespace Ht.Ihsil.Rgph.App.Superviseur.views.Contre_Enquete
{
    /// <summary>
    /// Interaction logic for frm_save_questions_individus.xaml
    /// </summary>
    public partial class frm_save_questions_tab_format : UserControl
    {
        #region DECLARATIONS/PROPERTIES
        Logger log = null;
        QuestionViewModel viewModel = new QuestionViewModel(TypeQuestion.Individu);
        QuestionReponseService service = null;
        ContreEnqueteService contreEnqueteService = null;
        QuestionsModel questionEnCours = null;
        ReponseModel mainReponse = null;
        Grid mainGrid = null;
        Button btnSuivant = null;
        int tabControlLength = 0;
        int tabIndex = 0;
        Thickness thick;
        IndividuCEModel individu = null;
        MenageDetailsViewModel detailsViewModel = null;
        EvaluationModel evaluation = null;
        MenageCEModel menage = null;
        ReponseSaisie reponseSaisie = null;
        ComboBox comboBox = null;
        TextEdit textbox = null;
        List<ReponseModel> listOfReponses = null;
        List<QuestionsModel> listOfQuestions = null;
        List<QuestionReponseModel> listOfQuestionReponses = null;
        List<IndividuCEModel> listOFindividu = null;
        QuestionReponseModel dernierQuestionReponse = null;
        List<ReponseModel> listOfAnswer = null;
        bool isCategorie = false;
        List<TabItem> _tabItems = null;
        ISqliteDataWriter sw = null;
        string Commune = null;
        string Departement = null;
        string Pays = null;
        string Vqse = null;

        private string module;
        private string nameOfModule = null;

        public string NameOfModule
        {
            get { return nameOfModule; }
            set { nameOfModule = value; }
        }
        public string Module
        {
            get { return module; }
            set { module = value; }
        }
        DateTime dateStart;
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Constructor frm_save_questions_individus
        /// </summary>
        /// <param Name="Model"></param>
        public frm_save_questions_tab_format(MenageDetailsViewModel viewModel)
        {
            InitializeComponent();
            log = new Logger();
            mainReponse = new ReponseModel();
            QuestionViewModel viewModelIndividu = new QuestionViewModel();
            sw = new SqliteDataWriter();
            contreEnqueteService = new ContreEnqueteService(Users.users.SupDatabasePath);
            if (viewModel != null)
            {
                detailsViewModel = viewModel;
                individu = new IndividuCEModel();
                individu = contreEnqueteService.getIndividuCEModel(Convert.ToInt32(detailsViewModel.MenageType.Id), detailsViewModel.MenageType.SdeId);
            }
            //Si l'individu est le chef de Menage on saute la question RELATION CHEF MENAGE
            if (individu.Q3LienDeParente == 1)
            {
                viewModelIndividu = new QuestionViewModel(TypeQuestion.Individu, true);
            }
            //SINON ON LA PREND
            else
            {
                viewModelIndividu = new QuestionViewModel(TypeQuestion.Individu);
            }
            this.DataContext = viewModelIndividu;
            service = new QuestionReponseService();
            reponseSaisie = new ReponseSaisie();
            InitializeComponent();
            thick = new Thickness(10, 55, 0, 0);
            btnSuivant = new Button();
            comboBox = new ComboBox();
            textbox = new TextEdit();
            //individu = new IndividuCEModel();
            //individu.IndividuId = model.IndividuId;
            //individu.BatimentId = model.BatimentId;
            //individu.LogeId = model.LogeId;
            //individu.MenageId = model.MenageId;
            //individu.SdeId = model.SdeId;
            //individu.Qp1NoOrdre = Convert.ToByte(model.Qp1NoOrdre);
            //if (model.Q3LienDeParente == 1)
            //{
            //    individu.Q3LienDeParente = 1;
            //}
            TextBlock tHeader = new TextBlock();
            tHeader.Foreground = Brushes.Red;
            tHeader.Text = "BATIMAN " + individu.BatimentId + "/ LOJMAN-" + individu.LogeId + "/MENAJ-" + individu.MenageId + "Endividi-" + individu.Qp1NoOrdre + "/SDE " + individu.SdeId;
            tHeader.FontWeight = FontWeights.Bold;
            scrl_bar_1.Visibility = Visibility.Visible;
            listOfReponses = new List<ReponseModel>();
            listOfQuestions = new List<QuestionsModel>();
            listOfQuestionReponses = new List<QuestionReponseModel>();
            dernierQuestionReponse = new QuestionReponseModel();
            listOfAnswer = new List<ReponseModel>();
            questionEnCours = viewModelIndividu.questionEnCours;
            Module = questionEnCours.CodeCategorie;
            string entete = service.getCategorieQuestion(Module).CategorieQuestion;
            if (questionEnCours.TypeQuestion == (int)Constant.TypeQuestion.Saisie)
            {
                txt_first.Visibility = Visibility.Visible;
            }
            else
            {
                if (questionEnCours.TypeQuestion == (int)Constant.TypeQuestion.Choix)
                {
                    cmb_first.Visibility = Visibility.Visible;
                }
            }
            listOfQuestions.Add(questionEnCours);
            _tabItems = new List<TabItem>();
            tab1.Header = entete;
            _tabItems.Add(tab1);
            mainGrid = new Grid();
            mainGrid = grd_tab_1;
            dateStart = DateTime.Now;
        }

        public frm_save_questions_tab_format(MenageCEModel model)
        {
            InitializeComponent();
            log = new Logger();
            menage = new MenageCEModel();
            menage = model;
            QuestionViewModel viewEvaluationModel = new QuestionViewModel(TypeQuestion.Evaluation);
            this.DataContext = viewEvaluationModel;
            service = new QuestionReponseService();
            contreEnqueteService = new ContreEnqueteService();
            reponseSaisie = new ReponseSaisie();
            InitializeComponent();
            thick = new Thickness(10, 55, 0, 0);
            btnSuivant = new Button();
            comboBox = new ComboBox();
            textbox = new TextEdit();
            evaluation = new EvaluationModel();
            evaluation.BatimentId = model.BatimentId;
            evaluation.LogeId = model.LogeId;
            evaluation.MenageId = model.MenageId;
            evaluation.SdeId = model.SdeId;
            TextBlock tHeader = new TextBlock();
            tHeader.Foreground = Brushes.Red;
            tHeader.FontWeight = FontWeights.Bold;
            scrl_bar_1.Visibility = Visibility.Visible;
            listOfReponses = new List<ReponseModel>();
            listOfQuestions = new List<QuestionsModel>();
            listOfQuestionReponses = new List<QuestionReponseModel>();
            dernierQuestionReponse = new QuestionReponseModel();
            listOfAnswer = new List<ReponseModel>();
            questionEnCours = viewEvaluationModel.questionEnCours;
            Module = questionEnCours.CodeCategorie;
            string entete = service.getCategorieQuestion(Module).CategorieQuestion;
            if (questionEnCours.TypeQuestion == (int)Constant.TypeQuestion.Saisie)
            {
                txt_first.Visibility = Visibility.Visible;
            }
            else
            {
                if (questionEnCours.TypeQuestion == (int)Constant.TypeQuestion.Choix)
                {
                    cmb_first.Visibility = Visibility.Visible;
                }
            }
            listOfQuestions.Add(questionEnCours);
            _tabItems = new List<TabItem>();
            tab1.Header = entete;
            _tabItems.Add(tab1);
            mainGrid = new Grid();
            mainGrid = grd_tab_1;
            dateStart = DateTime.Now;
        }
        #endregion

        #region TESTER LA QUESTION
        /// <summary>
        /// Tester si la question a ete deja posee
        /// </summary>
        /// <param Name="quest"></param>
        /// <param Name="listOfQuestion"></param>
        /// <returns></returns>
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
        #endregion

        #region EVENT ON CONTROLS

        #region Combobox SelectedIndexChanged
        private void cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ReponseModel reponse = (sender as ComboBox).SelectedItem as ReponseModel;
                //On ne prend pas le module si question de Type pays/communne/ departement/vsqe
                if (questionEnCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Commune ||
                    questionEnCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Departement ||
                    questionEnCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Pays ||
                    questionEnCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Vqse
                    //questionEnCours.NomChamps == Constant.QbPrincipalRepondant || questionEnCours.NomChamps == Constant.Qc3EconomiqueNoOrdre ||
                    //questionEnCours.NomChamps == Constant.Qc3EducationNoOrdre || questionEnCours.NomChamps == Constant.Qc3FeconditeNoOrdre ||
                    //questionEnCours.NomChamps == Constant.Qc3FonctionnementNoOrdre || questionEnCours.NomChamps == Constant.Qc3MembreMenageNoOrdre ||
                    //questionEnCours.NomChamps == Constant.Qc3MortaliteNoOrdre

                    )
                {
                }
                else
                {

                    if (reponse.CodeReponse != null)
                    {
                        Module = service.getQuestion(service.getQuestionReponse(reponse.CodeUniqueReponse).CodeQuestion).CodeCategorie;

                    }
                }
                comboBox = sender as ComboBox;
                mainReponse = reponse;
                if (Utils.IsNotNull(reponse))
                {
                    if (Utils.IsNotNull(individu))
                    {
                        try
                        {

                            setQuestionAndControls(questionEnCours, reponse, comboBox);

                        }
                        catch (MessageException ex)
                        {
                            MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        if (Utils.IsNotNull(evaluation))
                        {
                            setQuestionAndControls(questionEnCours, reponse, comboBox);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Source:" + e.OriginalSource.GetType());
                }
            }
            catch (Exception)
            {

            }
        }

        private void cmb_first_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ReponseModel reponse = cmb_first.SelectedItem as ReponseModel;
                setQuestionAndControls(questionEnCours, reponse, cmb_first);
            }
            catch (Exception)
            {

            }

        }
        #endregion

        #region Text Event
        private void txt_first_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    ReponseModel rep = null;

                    if (txt_first.Text == null || txt_first.Text == "")
                    {
                        MessageBox.Show("Ou dwe mete yon valè.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        rep = new ReponseModel();
                        txt_first = sender as TextEdit;
                        string uniqueId = txt_first.Uid.ToString();
                        if (uniqueId != "")
                        {
                            rep.CodeQuestion = txt_first.Uid.ToString();
                            rep.CodeReponse = txt_first.Text;
                            questionEnCours = service.getQuestion(rep.CodeQuestion);
                            setQuestionAndControls(questionEnCours, rep, txt_first);
                        }
                        else
                        {
                            rep.CodeReponse = txt_first.Text;
                            rep.CodeQuestion = questionEnCours.CodeQuestion;
                            txt_first.Uid = questionEnCours.CodeQuestion;
                            setQuestionAndControls(questionEnCours, rep, txt_first);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void txt_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
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
                        textbox = sender as TextEdit;
                        string uniqueId = textbox.Uid.ToString();
                        if (uniqueId != "")
                        {
                            rep.CodeQuestion = textbox.Uid.ToString();
                            rep.CodeReponse = textbox.Text;
                            questionEnCours = service.getQuestion(rep.CodeQuestion);
                        }
                        else
                        {
                            rep.CodeReponse = textbox.Text;
                            textbox.Uid = questionEnCours.CodeQuestion;
                            rep.CodeQuestion = questionEnCours.CodeQuestion;
                        }
                        if (Utils.IsNotNull(individu))
                        {
                            try
                            {
                                setQuestionAndControls(questionEnCours, rep, textbox);
                            }
                            catch (MessageException ex)
                            {
                                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            try
                            {
                                setQuestionAndControls(questionEnCours, rep, textbox);
                            }
                            catch (MessageException ex)
                            {
                                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
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
            try
            {
                if (Utils.IsNotNull(individu))
                {
                    setLastQuestionBeforeSave<IndividuCEModel>(individu);
                }
            }
            catch (Exception)
            {

            }

        }

        #endregion

        #region SET OBJET
        /// <summary>
        /// Set l'objet individuCEModel avec les reponses selectionnees
        /// </summary>
        /// <param Name="reponse"></param>
        /// <param Name="ind"></param>
        /// <returns></returns>
        public IndividuCEModel getIndividuModel(ReponseSaisie reponse, IndividuCEModel ind)
        {
            if (reponse.NomChamps == Constant.Q2Nom) { ind.Q2Nom = reponse.CodeReponse; }
            if (reponse.NomChamps == Constant.Q3Prenom) { ind.Q3Prenom = reponse.CodeReponse; }
            if (reponse.NomChamps == Constant.Q3LienDeParente)
            {
                ind.Q3LienDeParente = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<IndividuCEModel>(this.individu);
            }
            if (reponse.NomChamps == Constant.Q4Sexe)
            {
                ind.Q4Sexe = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<IndividuCEModel>(this.individu);
            }
            if (reponse.NomChamps == Constant.Q5bAge)
            {
                ind.Q5bAge = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<IndividuCEModel>(this.individu);
            }
            if (reponse.NomChamps == Constant.Qp7Nationalite) { ind.Qp7Nationalite = Convert.ToByte(reponse.CodeReponse); }
            if (reponse.NomChamps == Constant.Qp7PaysNationalite) { ind.Qp7PaysNationalite = reponse.CodeReponse; }

            if (reponse.NomChamps == Constant.Q7DateNaissanceJour) 
            { 
                ind.Q7DateNaissanceJour = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<IndividuCEModel>(this.individu);
            }
            if (reponse.NomChamps == Constant.Q7DateNaissanceMois) 
            {
                ind.Q7DateNaissanceMois = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<IndividuCEModel>(this.individu);
            }
            if (reponse.NomChamps == Constant.Q7DateNaissanceAnnee)
            {
                ind.Q7DateNaissanceAnnee = Convert.ToInt32(reponse.CodeReponse);
                checkConstraint<IndividuCEModel>(this.individu);
            }

            if (reponse.NomChamps == Constant.Qp10LieuNaissance) { ind.Qp10LieuNaissance = Convert.ToByte(reponse.CodeReponse); }
            if (reponse.NomChamps == Constant.Qp10CommuneNaissance) { ind.Qp10CommuneNaissance = reponse.CodeReponse; }
            if (reponse.NomChamps == Constant.Qp10LieuNaissanceVqse) { ind.Qp10LieuNaissanceVqse = reponse.CodeReponse; }
            if (reponse.NomChamps == Constant.Qp10PaysNaissance) { ind.Qp10PaysNaissance = reponse.CodeReponse; }
            if (reponse.NomChamps == Constant.Qp11PeriodeResidence) { ind.Qp11PeriodeResidence = Convert.ToByte(reponse.CodeReponse); }

            if (reponse.NomChamps == Constant.Qe2FreqentationScolaireOuUniv) { ind.Qe2FreqentationScolaireOuUniv = Convert.ToByte(reponse.CodeReponse); }
            if (reponse.NomChamps == Constant.Qe4aNiveauEtude)
            {
                ind.Qe4aNiveauEtude = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<IndividuCEModel>(this.individu);
            }
            if (reponse.NomChamps == Constant.Qe4bDerniereClasseOUAneEtude)
            {
                ind.Qe4bDerniereClasseOUAneEtude = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<IndividuCEModel>(this.individu);
            }
            if (reponse.NomChamps == Constant.Qsm1StatutMatrimonial)
            {
                ind.Qsm1StatutMatrimonial = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<IndividuCEModel>(this.individu);
            }
            if (reponse.NomChamps == Constant.Qa1ActEconomiqueDerniereSemaine)
            {
                ind.Qa1ActEconomiqueDerniereSemaine = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<IndividuCEModel>(this.individu);
            }
            if (reponse.NomChamps == Constant.Qa2ActAvoirDemele1) { ind.Qa2ActAvoirDemele1 = Convert.ToByte(reponse.CodeReponse); }
            if (reponse.NomChamps == Constant.Qa2ActDomestique2) { ind.Qa2ActDomestique2 = Convert.ToByte(reponse.CodeReponse); }
            if (reponse.NomChamps == Constant.Qa2ActCultivateur3) { ind.Qa2ActCultivateur3 = Convert.ToByte(reponse.CodeReponse); }
            if (reponse.NomChamps == Constant.Qa2ActAiderParent4) { ind.Qa2ActAiderParent4 = Convert.ToByte(reponse.CodeReponse); }
            if (reponse.NomChamps == Constant.Qa2ActAutre5) { ind.Qa2ActAutre5 = Convert.ToByte(reponse.CodeReponse); }

            if (reponse.NomChamps == Constant.Qa8EntreprendreDemarcheTravail) { ind.Qa8EntreprendreDemarcheTravail = Convert.ToByte(reponse.CodeReponse); }
            if (reponse.NomChamps == Constant.Qf1aNbreEnfantNeVivantM)
            {
                ind.Qf1aNbreEnfantNeVivantM = Convert.ToInt32(reponse.CodeReponse);
                checkConstraint<IndividuCEModel>(this.individu);
            }
            if (reponse.NomChamps == Constant.Qf2bNbreEnfantNeVivantF)
            {
                ind.Qf2bNbreEnfantNeVivantF = Convert.ToInt32(reponse.CodeReponse);
                checkConstraint<IndividuCEModel>(this.individu);
            }
            if (reponse.NomChamps == Constant.Qf2aNbreEnfantVivantM)
            {
                ind.Qf2aNbreEnfantVivantM = Convert.ToInt32(reponse.CodeReponse);
            }
            if (reponse.NomChamps == Constant.Qf2bNbreEnfantVivantF)
            {
                ind.Qf2bNbreEnfantVivantF = Convert.ToInt32(reponse.CodeReponse);
                checkConstraint<IndividuCEModel>(this.individu);
            }
            if (reponse.NomChamps == Constant.Qf3DernierEnfantJour)
            {
                ind.Qf3DernierEnfantJour = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<IndividuCEModel>(this.individu);
            }
            if (reponse.NomChamps == Constant.Qf3DernierEnfantMois)
            {
                ind.Qf3DernierEnfantMois = Convert.ToByte(reponse.CodeReponse);
                checkConstraint<IndividuCEModel>(this.individu);
            }
            if (reponse.NomChamps == Constant.Qf3DernierEnfantAnnee)
            {
                ind.Qf3DernierEnfantAnnee = Convert.ToInt32(reponse.CodeReponse);
                checkConstraint<IndividuCEModel>(this.individu);
            }
         if (reponse.NomChamps == Constant.DureeSaisie) { ind.DureeSaisie = Convert.ToInt32(reponse.CodeReponse); }
            return ind;

        }

        //public EvaluationModel getEvaluationModel(ReponseSaisie reponse, EvaluationModel _eva)
        //{

        //    if (reponse.NomChamps == Constant.Qa1StatutQuestionnaire) { _eva.Qa1StatutQuestionnaire = Convert.ToByte(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qa1RaisonStatut) { _eva.Qa1RaisonStatut = Convert.ToByte(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.QbPrincipalRepondant)
        //    {
        //        foreach (IndividuCEModel _ind in listOFindividu)
        //        {
        //            if (_ind.IndividuId == Convert.ToInt32(reponse.CodeReponse))
        //            {
        //                _eva.Qb1RepondantNoOrdre = _ind.Qp1NoOrdre.GetValueOrDefault();
        //                _eva.Qb1RepondantRChefMenage = _ind.Q3LienDeParente.GetValueOrDefault();
        //                _eva.Qb1RepondantNiveauEtude = _ind.Qe4aNiveauEtude;
        //                _eva.Qb1RepondantSexe = _ind.Q4Sexe.GetValueOrDefault();
        //            }
        //        }
        //    }
        //    if (reponse.NomChamps == Constant.Qc1MembreMenage) { _eva.Qc1MembreMenage = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qc2MembreMenage) { _eva.Qc2MembreMenage = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qc3MembreMenageNoOrdre)
        //    { 
        //        _eva.Qc3MembreMenageNoOrdre = Convert.ToInt32(reponse.CodeReponse);
        //        foreach (IndividuCEModel _ind in listOFindividu)
        //        {
        //            if (_ind.IndividuId == Convert.ToInt32(reponse.CodeReponse))
        //            {
        //                _eva.Qc3MembreMenageNoOrdre = _ind.Qp1NoOrdre.GetValueOrDefault();
        //                _eva.Qc3MembreMenageNom = _ind.Qp2Nom + " " + _ind.Qp2Prenom;
        //            }
        //        }
        //    }

        //    if (reponse.NomChamps == Constant.Qc1Mortalite) { _eva.Qc1Mortalite = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qc2Mortalite) { _eva.Qc2Mortalite = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qc3MortaliteNoOrdre) 
        //    {
        //        foreach (IndividuCEModel _ind in listOFindividu)
        //        {
        //            if (_ind.IndividuId == Convert.ToInt32(reponse.CodeReponse))
        //            {
        //                _eva.Qc3MortaliteNoOrdre = _ind.Qp1NoOrdre.GetValueOrDefault();
        //                _eva.Qc3MortaliteNom = _ind.Qp2Nom + " " + _ind.Qp2Prenom;
        //            }
        //        }
        //    }
        //    if (reponse.NomChamps == Constant.Qc1Education) { _eva.Qc1Education = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qc2Education) { _eva.Qc2Education = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qc3EducationNoOrdre) 
        //    {
        //        foreach (IndividuCEModel _ind in listOFindividu)
        //        {
        //            if (_ind.IndividuId == Convert.ToInt32(reponse.CodeReponse))
        //            {
        //                _eva.Qc3EducationNoOrdre = _ind.Qp1NoOrdre.GetValueOrDefault();
        //                _eva.Qc3EducationNom = _ind.Qp2Nom + " " + _ind.Qp2Prenom;
        //            }
        //        }
        //    }
        //    if (reponse.NomChamps == Constant.Qc1Fonctionnement) { _eva.Qc1Fonctionnement = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qc2Fonctionnement) { _eva.Qc2Fonctionnement = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qc3FonctionnementNoOrdre)
        //    {
        //        foreach (IndividuCEModel _ind in listOFindividu)
        //        {
        //            if (_ind.IndividuId == Convert.ToInt32(reponse.CodeReponse))
        //            {
        //                _eva.Qc3FonctionnementNoOrdre = _ind.Qp1NoOrdre.GetValueOrDefault();
        //                _eva.Qc3FonctionnementNom = _ind.Qp2Nom + " " + _ind.Qp2Prenom;
        //            }
        //        }
        //    }
        //    if (reponse.NomChamps == Constant.Qc1Economique) { _eva.Qc1Economique = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qc2Economique) { _eva.Qc2Economique = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qc3EconomiqueNoOrdre) 
        //    {
        //        foreach (IndividuCEModel _ind in listOFindividu)
        //        {
        //            if (_ind.IndividuId == Convert.ToInt32(reponse.CodeReponse))
        //            {
        //                _eva.Qc3EconomiqueNoOrdre = _ind.Qp1NoOrdre.GetValueOrDefault();
        //                _eva.Qc3EconomiqueNom = _ind.Qp2Nom + " " + _ind.Qp2Prenom;
        //            }
        //        }
        //    }
        //    if (reponse.NomChamps == Constant.Qc1Fecondite) { _eva.Qc1Fecondite = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qc2Fecondite) { _eva.Qc2Fecondite = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qc3FeconditeNoOrdre)
        //    {
        //        foreach (IndividuCEModel _ind in listOFindividu)
        //        {
        //            if (_ind.IndividuId == Convert.ToInt32(reponse.CodeReponse))
        //            {
        //                _eva.Qc3FeconditeNoOrdre = _ind.Qp1NoOrdre.GetValueOrDefault();
        //                _eva.Qc3FeconditeNom = _ind.Qp2Nom + " " + _ind.Qp2Prenom;
        //            }
        //        }
        //    }
        //    if (reponse.NomChamps == Constant.Qd11NbrePerVivant) { _eva.Qd11NbrePerVivant = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qd12NbrePerVivantG) { _eva.Qd12NbrePerVivantG = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qd13NbrePerVivantF) { _eva.Qd13NbrePerVivantF = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qd21NbrePerRecense) { _eva.Qd21NbrePerRecense = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qd22NbrePerRecenseG) { _eva.Qd22NbrePerRecenseG = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qd23NbrePerRecenseF) { _eva.Qd23NbrePerRecenseF = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qd31NbrePerUneAnnee) { _eva.Qd31NbrePerUneAnnee = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qd32NbrePerUneAnneeG) { _eva.Qd32NbrePerUneAnneeG = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qd33NbrePerUneAnneeF) { _eva.Qd33NbrePerUneAnneeF = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qd41NbrePerCinqAnnee) { _eva.Qd41NbrePerCinqAnnee = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qd42NbrePerCinqAnneeG) { _eva.Qd42NbrePerCinqAnneeG = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qd43NbrePerCinqAnneeF) { _eva.Qd43NbrePerCinqAnneeF = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qd5nbreFilleTreizeAnnee) { _eva.Qd5nbreFilleTreizeAnnee = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qe1StatutFinal) { _eva.Qe1StatutFinal = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.Qe1RaisonStatutFinal) { _eva.Qe1RaisonStatutFinal = Convert.ToInt32(reponse.CodeReponse); }
        //    if (reponse.NomChamps == Constant.NomSuperviseur) { _eva.NomSuperviseur = reponse.CodeReponse; }
        //    return _eva;
        //}
        #endregion

        #region CONSTRAINTS
        /// <summary>
        /// Contraintes sur les valeurs de l'objet IndividuCeModel
        /// </summary>
        /// <typeparam Name="T"></typeparam>
        /// <param Name="objectType"></param>
        public void checkConstraint<T>(T objectType)
        {
            if (objectType.ToString() == Constant.OBJET_MODEL_INDIVIDUCE)
            {

                #region CONTRAINTE TESTANT L'AGE DU CHEF DE MENAGE QP5
                if (individu.Q3LienDeParente.GetValueOrDefault() != 0 && individu.Q3LienDeParente.GetValueOrDefault() == 1)
                {
                    if (individu.Q5bAge.GetValueOrDefault() != 0 && individu.Q5bAge.GetValueOrDefault() <= 15)
                    {
                        individu.Q5bAge = 0;
                        individu.Q3LienDeParente = 0;
                        throw new MessageException("verifye laj moun lan, setifye si se li ki chef menaj lan.");
                    }
                }
                #endregion

                #region CONTRAINTE CHEF MENAGE DE SON SEXE ET LES MEMBRES DE SA FAMILLE

                #region NE PAS PERMETTRE D'AVOIR 2 CHEF DE MENAGE DANS UN MENAGE
                if (this.individu.Q3LienDeParente.GetValueOrDefault() != 0 && this.individu.Q3LienDeParente.GetValueOrDefault() == 1)
                {
                    MenageCEModel men = new MenageCEModel();
                    men.MenageId = this.individu.MenageId;
                    men.BatimentId = this.individu.BatimentId;
                    men.LogeId = this.individu.LogeId;
                    men.SdeId = this.individu.SdeId;
                    IndividuCEModel chefMenage = null;
                    List<IndividuCEModel> listOf = contreEnqueteService.searchAllIndividuCE(men);
                    if (Utils.IsNotNull(listOf))
                    {
                        foreach (IndividuCEModel ind in listOf)
                        {
                            if (ind.Q3LienDeParente.GetValueOrDefault() == 1)
                            {
                                chefMenage = new IndividuCEModel();
                                chefMenage = ind;
                                break;
                            }

                        }
                        if (chefMenage == null)
                        {

                        }
                        else
                        {
                            if (chefMenage.IndividuId != 0)
                            {
                                if (individu.IndividuId == chefMenage.IndividuId)
                                {

                                }
                                else
                                {
                                    throw new MessageException("Pa ka gen 2 chèf menaj nan yon menaj.");

                                }
                            }
                        }
                    }
                }
                #endregion

                #region TEST SUR LA DIFFERENCE D'AGE DU CHEF DE FAMILLE ET SON ENFANT
                if (this.individu.Q3LienDeParente.GetValueOrDefault() != 0 && this.individu.Q3LienDeParente.GetValueOrDefault() == 3)
                {
                    MenageCEModel men = new MenageCEModel();
                    men.MenageId = this.individu.MenageId;
                    men.BatimentId = this.individu.BatimentId;
                    men.LogeId = this.individu.LogeId;
                    men.SdeId = this.individu.SdeId;
                    IndividuCEModel chefMenage = null;
                    List<IndividuCEModel> listOf = contreEnqueteService.searchAllIndividuCE(men);
                    if (Utils.IsNotNull(listOf))
                    {
                        foreach (IndividuCEModel ind in listOf)
                        {
                            if (ind.Q3LienDeParente.GetValueOrDefault() == 1)
                            {
                                chefMenage = new IndividuCEModel();
                                chefMenage = ind;
                                break;
                            }
                        }

                    }
                    if (chefMenage != null)
                    {
                        if (individu.Q5bAge.GetValueOrDefault() != 0)
                        {

                            //Si le chef de Menage est un garcon la difference de son age et l'age de son enfant doit etre superieur ou egal 15 ans

                            if (chefMenage.Q4Sexe.GetValueOrDefault() != 0 && chefMenage.Q4Sexe.GetValueOrDefault() == 1)
                            {
                                long age = chefMenage.Q5bAge.GetValueOrDefault() - individu.Q5bAge.GetValueOrDefault();
                                if (age < 15)
                                {
                                    individu.Q5bAge = 0;
                                    individu.Q3LienDeParente = 0;
                                    throw new MessageException(Constant.MSG_CHEF_MENAGE);
                                }
                            }
                            else //sinon si c'est une femme , elle est doit etre superieur a 13 ans
                            {
                                if (chefMenage.Q4Sexe.GetValueOrDefault() != 0 && chefMenage.Q4Sexe.GetValueOrDefault() == 2)
                                {
                                    if ((chefMenage.Q5bAge.GetValueOrDefault() - individu.Q5bAge.GetValueOrDefault()) < 13)
                                    {
                                        individu.Q5bAge = 0;
                                        individu.Q3LienDeParente = 0;
                                        throw new MessageException(Constant.MSG_CHEF_MENAGE);
                                    }
                                }
                            }
                        }
                    }

                }
                #endregion

                #region TEST SUR LA DIFFERENCE D'AGE DU CHEF DE FAMILLE ET SON PETIT ENFANT
                if (this.individu.Q3LienDeParente.GetValueOrDefault() != 0 && this.individu.Q3LienDeParente.GetValueOrDefault() == 7)
                {
                    MenageCEModel men = new MenageCEModel();
                    men.MenageId = this.individu.MenageId;
                    men.BatimentId = this.individu.BatimentId;
                    men.LogeId = this.individu.LogeId;
                    men.SdeId = this.individu.SdeId;
                    IndividuCEModel chefMenage = null;
                    List<IndividuCEModel> listOf = contreEnqueteService.searchAllIndividuCE(men);
                    if (Utils.IsNotNull(listOf))
                    {
                        foreach (IndividuCEModel ind in listOf)
                        {
                            if (ind.Q3LienDeParente.GetValueOrDefault() == 1)
                            {
                                chefMenage = new IndividuCEModel();
                                chefMenage = ind;
                                break;
                            }

                        }

                    }
                    if (chefMenage != null)
                    {
                        if (individu.Q5bAge.GetValueOrDefault() != 0)
                        {
                            //Si le chef de Menage est un garcon la difference de son Age et l'Age de son enfant doit etre superieur ou egal 32 ans

                            if (chefMenage.Q4Sexe.GetValueOrDefault() != 0 && chefMenage.Q4Sexe.GetValueOrDefault() == 1)
                            {

                                if ((chefMenage.Q5bAge.GetValueOrDefault() - individu.Q5bAge.GetValueOrDefault()) < 32)
                                {
                                    individu.Q5bAge = 0;
                                    individu.Q3LienDeParente = 0;
                                    throw new MessageException(Constant.MSG_CHEF_MENAGE);
                                }
                            }
                            else //sinon si c'est une femme , elle est doit etre superieur a 28 ans
                            {
                                if (chefMenage.Q4Sexe.GetValueOrDefault() != 0 && chefMenage.Q4Sexe.GetValueOrDefault() == 2)
                                {
                                    if ((chefMenage.Q5bAge.GetValueOrDefault() - individu.Q5bAge.GetValueOrDefault()) < 28)
                                    {
                                        individu.Q5bAge = 0;
                                        individu.Q5bAge = 0;
                                        throw new MessageException(Constant.MSG_CHEF_MENAGE);
                                    }
                                }
                            }

                            ////Si le chef de Menage est un garcon la difference de son Age et l'Age de son petit enfant doit etre superieur 30 ans
                            //if (chefMenage.Q4Sexe.GetValueOrDefault() != 0 && chefMenage.Q4Sexe.GetValueOrDefault() == 1)
                            //{
                            //    if ((chefMenage.Q5bAge.GetValueOrDefault() - individu.Q5bAge.GetValueOrDefault()) < 30)
                            //    {
                            //        individu.Q5bAge = 0;
                            //        individu.Q3LienDeParente = 0;
                            //        throw new MessageException("Pitit chèf menaj la dwe gen pou piti 30 ane.");
                            //    }
                            //}
                            //else //sinon si c'est une femme , elle est doit etre superieur a 26 ans
                            //{
                            //    if (chefMenage.Q4Sexe.GetValueOrDefault() != 0 && chefMenage.Q4Sexe.GetValueOrDefault() == 2)
                            //    {
                            //        if ((chefMenage.Q5bAge.GetValueOrDefault() - individu.Q5bAge.GetValueOrDefault()) < 26)
                            //        {
                            //            individu.Q5bAge = 0;
                            //            individu.Q3LienDeParente = 0;
                            //            throw new MessageException("Pitit chèf menaj la dwe gen pou piti 26 ane.");
                            //        }
                            //    }
                            //}
                        }
                    }


                }
                #endregion

                #region TEST SUR LA DIFFERENCE D'AGE DU CHEF DE FAMILLE ET DE SES PARENTS
                if (this.individu.Q3LienDeParente.GetValueOrDefault() != 0 && this.individu.Q3LienDeParente.GetValueOrDefault() == 6)
                {
                    MenageCEModel men = new MenageCEModel();
                    men.MenageId = this.individu.MenageId;
                    men.BatimentId = this.individu.BatimentId;
                    men.LogeId = this.individu.LogeId;
                    men.SdeId = this.individu.SdeId;
                    IndividuCEModel chefMenage = null;
                    List<IndividuCEModel> listOf = contreEnqueteService.searchAllIndividuCE(men);
                    if (Utils.IsNotNull(listOf))
                    {
                        foreach (IndividuCEModel ind in listOf)
                        {
                            if (ind.Q3LienDeParente.GetValueOrDefault() == 1)
                            {
                                chefMenage = new IndividuCEModel();
                                chefMenage = ind;
                            }
                            break;
                        }

                    }
                    if (chefMenage != null)
                    {
                        if (individu.Q5bAge.GetValueOrDefault() != 0)
                        {
                            //Si le chef de Menage est un garcon la difference de son Age et l'Age de son parent doit etre superieur 12 ans
                            if (chefMenage.Q4Sexe.GetValueOrDefault() != 0 && chefMenage.Q4Sexe.GetValueOrDefault() == 1)
                            {
                                if ((chefMenage.Q5bAge.GetValueOrDefault() - individu.Q5bAge.GetValueOrDefault()) < 15)
                                {
                                    if (individu.Q4Sexe.GetValueOrDefault() == 1)
                                    {
                                        individu.Q5bAge = 0;
                                        individu.Q3LienDeParente = 0;
                                        throw new MessageException(Constant.MSG_CHEF_MENAGE);
                                    }
                                    else
                                    {
                                        individu.Q5bAge = 0;
                                        individu.Q3LienDeParente = 0;
                                        throw new MessageException(Constant.MSG_CHEF_MENAGE);
                                    }
                                }
                            }
                            else //sinon si c'est une femme , elle est doit etre superieur a 12 ans
                            {

                                if ((chefMenage.Q5bAge.GetValueOrDefault() - individu.Q5bAge.GetValueOrDefault()) < 13)
                                {
                                    if (individu.Q4Sexe.GetValueOrDefault() == 1)
                                    {
                                        individu.Q5bAge = 0;
                                        individu.Q3LienDeParente = 0;
                                        throw new MessageException(Constant.MSG_CHEF_MENAGE);
                                    }
                                    else
                                    {
                                        individu.Q5bAge = 0;
                                        individu.Q3LienDeParente = 0;
                                        throw new MessageException(Constant.MSG_CHEF_MENAGE);
                                    }
                                }
                            }
                        }
                    }

                }
                #endregion
             

                #region CONTRAINTE DU SEXE MADAME/OU MARI CHEF MENAGE
                if (this.individu.Q4Sexe.GetValueOrDefault() != 0)
                {
                    MenageCEModel men = new MenageCEModel();
                    men.MenageId = this.individu.MenageId;
                    men.BatimentId = this.individu.BatimentId;
                    men.LogeId = this.individu.LogeId;
                    men.SdeId = this.individu.SdeId;
                    IndividuCEModel chefMenage = null;
                    List<IndividuCEModel> listOf = contreEnqueteService.searchAllIndividuCE(men);
                    if (Utils.IsNotNull(listOf))
                    {
                        foreach (IndividuCEModel ind in listOf)
                        {
                            if (ind.Q3LienDeParente.GetValueOrDefault() == 1)
                            {
                                chefMenage = new IndividuCEModel();
                                chefMenage = ind;
                                break;
                            }

                        }
                        if (chefMenage != null)
                        {
                            if (this.individu.Q3LienDeParente.GetValueOrDefault() == 2)
                            {
                                if (chefMenage.Q4Sexe.GetValueOrDefault() == this.individu.Q4Sexe.GetValueOrDefault())
                                {
                                    throw new MessageException("Madanm/Mari pa ka gen menm sèks ak chèf menaj la.");
                                }

                            }
                        }

                    }
                }
                #endregion
                #endregion

                #region CONTRAINTE LE STATUT MATRIMONIAL DU CONJOINT ET CELUI DU CHEF MENAGE
                if (this.individu.Qsm1StatutMatrimonial.GetValueOrDefault() != 0)
                {
                    MenageCEModel men = new MenageCEModel();
                    men.MenageId = this.individu.MenageId;
                    men.BatimentId = this.individu.BatimentId;
                    men.LogeId = this.individu.LogeId;
                    men.SdeId = this.individu.SdeId;
                    IndividuCEModel chefMenage = null;
                    List<IndividuCEModel> listOf = contreEnqueteService.searchAllIndividuCE(men);
                    if (Utils.IsNotNull(listOf))
                    {
                        foreach (IndividuCEModel ind in listOf)
                        {
                            if (ind.Q3LienDeParente.GetValueOrDefault() == 1)
                            {
                                chefMenage = new IndividuCEModel();
                                chefMenage = ind;
                                break;
                            }

                        }
                        if (chefMenage != null)
                        {
                            if (this.individu.Q3LienDeParente.GetValueOrDefault() == 2)
                            {
                                if (chefMenage.Qsm1StatutMatrimonial.GetValueOrDefault() != this.individu.Qsm1StatutMatrimonial.GetValueOrDefault())
                                {
                                    throw new MessageException("Madanm/Mari a dwe gen menm estati matrimonyal ak chèf menaj la.");
                                }
                            }
                        }

                    }
                }
                #endregion

                #region CONTRAINTE SUR LE JOUR, LE MOIS ET L'ANNEE DE LA DATE NAISSANCE

                //Contrainte sur le jour de naissance
                if (this.individu.Q7DateNaissanceJour.GetValueOrDefault() < 0)
                    throw new MessageException("Jou a pa bon.");

                if (this.individu.Q7DateNaissanceJour.GetValueOrDefault() != 0)
                {
                    if (this.individu.Q7DateNaissanceJour.GetValueOrDefault() > 31)
                        throw new MessageException("Jou pa dwe depase 31.");

                    //Pour le mois de fevrier
                    if (this.individu.Q7DateNaissanceMois.GetValueOrDefault() != 0)
                    {
                        if (this.individu.Q7DateNaissanceMois.GetValueOrDefault() == 2)
                        {
                            if(this.individu.Q7DateNaissanceJour.GetValueOrDefault() > 29)
                                throw new MessageException("Jou pa dwe depase 29.");
                        }
                    }
                }

                //Contrainte sur le mois
                if (this.individu.Q7DateNaissanceMois.GetValueOrDefault() < 0)
                    throw new MessageException("Mwa a pa bon.");
                if (this.individu.Q7DateNaissanceMois.GetValueOrDefault() != 0)
                {
                    if(this.individu.Q7DateNaissanceMois.GetValueOrDefault()>12)
                        throw new MessageException("Mwa pa dwe depase 12.");

                }
                //Contrainte sur l'annee
                if (this.individu.Q7DateNaissanceAnnee.GetValueOrDefault() < 0)
                    throw new MessageException("Mwa a pa bon.");
                if (this.individu.Q7DateNaissanceAnnee.GetValueOrDefault() != 0)
                {
                    if (this.individu.Q7DateNaissanceAnnee.GetValueOrDefault() > 2017)
                        throw new MessageException("Ane pa dwe depase 2017.");

                }
                if (this.individu.Q7DateNaissanceAnnee.GetValueOrDefault() != 0)
                {
                    if (this.individu.Q7DateNaissanceAnnee.GetValueOrDefault() < 1890)
                        throw new MessageException("Ane pa dwe pi piti ke 1890.");

                }

                #endregion
              
                #region CONTRAINTE TESTANT SI L'AGE DE L'INDIVIDU EST 4 ans PASSE A LA QUESTION E2
                if (this.individu.Qp11PeriodeResidence.GetValueOrDefault() !=0)
                {
                    if (this.individu.Q5bAge.GetValueOrDefault() < 3 )
                    {
                        throw new MessageFinException(Constant.MESSAGE12EXCEPTION);
                    }
                }
                #endregion

                #region CONTRAINTE TESTANT LE NIVEAU D'ETUDE DE L'L'INDIVIDU EN FONCTION DE SON AGE
                if (this.individu.Qe4aNiveauEtude.GetValueOrDefault() != 0)
                {
                    if (this.individu.Qe4aNiveauEtude.GetValueOrDefault() == 1)
                    {
                        if (this.individu.Q5bAge.GetValueOrDefault() < 3)
                        {
                            throw new MessageException("" + Constant.MSG_NIVEAU_AGE_ETUDE);
                        }
                    }
                    if (this.individu.Qe4aNiveauEtude.GetValueOrDefault() == 2)
                    {
                        if (this.individu.Q5bAge.GetValueOrDefault() < 5)
                        {
                            throw new MessageException("" + Constant.MSG_NIVEAU_AGE_ETUDE);
                        }
                    }
                    if (this.individu.Qe4aNiveauEtude.GetValueOrDefault() == 3)
                    {
                        if (this.individu.Q5bAge.GetValueOrDefault() <7)
                        {
                            throw new MessageException("" + Constant.MSG_NIVEAU_AGE_ETUDE);
                        }
                    }
                    if (this.individu.Qe4aNiveauEtude.GetValueOrDefault() == 4)
                    {
                        if (this.individu.Q5bAge.GetValueOrDefault() < 10)
                        {
                            throw new MessageException("" + Constant.MSG_NIVEAU_AGE_ETUDE);
                        }
                    }
                    if (this.individu.Qe4aNiveauEtude.GetValueOrDefault() == 5)
                    {
                        if (this.individu.Q5bAge.GetValueOrDefault() <= 14)
                        {
                            throw new MessageException("" + Constant.MSG_NIVEAU_AGE_ETUDE);
                        }
                    }
                    if (this.individu.Qe4aNiveauEtude.GetValueOrDefault() == 6)
                    {
                        if (this.individu.Q5bAge.GetValueOrDefault() < 17)
                        {
                            throw new MessageException("" + Constant.MSG_NIVEAU_AGE_ETUDE);
                        }
                    }
                }
                #endregion

                #region CONTRAINTE TESTANT LE NIVEAU D'ETUDE
                if (this.individu.Qe4aNiveauEtude.GetValueOrDefault() != 0)
                {
                    int frequentation = Convert.ToInt32(this.individu.Qe2FreqentationScolaireOuUniv.GetValueOrDefault());
                    int niveau = Convert.ToInt32(this.individu.Qe4aNiveauEtude.GetValueOrDefault());
                    if (frequentation == 1)
                    {
                        if (niveau > 3)
                        {
                            throw new MessageException("" + Constant.MSG_NIVEAU_ETUDE);
                        }
                    }
                    if (frequentation == 2)
                    {
                        if (niveau <= 3 || niveau > 4)
                        {
                            throw new MessageException("" + Constant.MSG_NIVEAU_ETUDE);
                        }
                    }
                    if (frequentation == 3)
                    {
                        if (niveau <= 4 || niveau > 5)
                        {
                            throw new MessageException("" + Constant.MSG_NIVEAU_ETUDE);
                        }
                    }
                    if (frequentation == 4)
                    {
                        if (niveau <= 5 || niveau > 6)
                        {
                            throw new MessageException("" + Constant.MSG_NIVEAU_ETUDE);
                        }
                    }
                    if (frequentation == 5)
                    {
                        if (niveau <= 6 || niveau > 7)
                        {
                            throw new MessageException("" + Constant.MSG_NIVEAU_ETUDE);
                        }
                    }
                    if (frequentation == 6)
                    {
                        if (niveau <= 7 || niveau > 8)
                        {
                            throw new MessageException("" + Constant.MSG_NIVEAU_ETUDE);
                        }
                    }
                }
                #endregion

                #region CONTRAINTE LA DERNIERE CLASSE FREQUENTEE
                if (this.individu.Qe4bDerniereClasseOUAneEtude.GetValueOrDefault() != 0)
                {
                    int derniereClasse = Convert.ToInt32(this.individu.Qe4bDerniereClasseOUAneEtude.GetValueOrDefault());
                    if (this.individu.Qe4aNiveauEtude.GetValueOrDefault() == 3)
                    {
                        if (derniereClasse > 3)
                        {
                            throw new MessageException(Constant.MSG_NIVEAU_ETUDE);
                        }
                    }
                    if (this.individu.Qe4aNiveauEtude.GetValueOrDefault() == 4 || this.individu.Qe4aNiveauEtude.GetValueOrDefault() == 5)
                    {
                        if (derniereClasse <= 3 || derniereClasse > 17)
                        {
                            throw new MessageException(Constant.MSG_NIVEAU_ETUDE);
                        }
                    }
                    if (this.individu.Qe4aNiveauEtude.GetValueOrDefault() == 7)
                    {
                        if (derniereClasse < 21 && derniereClasse > 27)
                        {
                            throw new MessageException(Constant.MSG_NIVEAU_ETUDE);
                        }
                    }
                    if (this.individu.Qe4aNiveauEtude.GetValueOrDefault() == 8)
                    {
                        if (derniereClasse < 28)
                        {
                            throw new MessageException(Constant.MSG_NIVEAU_ETUDE);
                        }
                    }
                }
                #endregion                

                #region CONTRAINTE TESTANT SI L'AGE DE L'INDIVIDU EST MOINS DE 10 ANS/NE PAS PRENDRE LES QUESTIONS ACTIVITE ECONOMIQUE ET FECONDITE
                //if (individu.Qe7FormationProf.GetValueOrDefault() != 0)
                //{
                //    if (individu.Q5bAge < 10)
                //    {

                //        throw new MessageFinException("Fomilè a fini pou moun sa a. Pran yon lot moun.");
                //    }
                //}
                #endregion

                #region SI L'INDIVIDU EST UN HOMME/OU UNE FEMME DE MOINS DE 13 ANS/  PAS DE FECONDITE

                if (individu.Qa1ActEconomiqueDerniereSemaine.GetValueOrDefault() != 0)
                {
                    if (this.individu.Q4Sexe.GetValueOrDefault() == 1)
                    {
                        throw new MessageFinException("" + Constant.MESSAGE12EXCEPTION);
                    }
                    if (individu.Q4Sexe.GetValueOrDefault() == 2 && individu.Q5bAge < 13)
                    {
                        throw new MessageFinException("" + Constant.MESSAGE12EXCEPTION);
                    }
                }

                #endregion

                #region CONTRAINTE TESTANT LE NOMBRE D'ENFANT QUE PEUT AVOIR UNE FEMME

                if (this.individu.Q4Sexe.GetValueOrDefault() != 0 && this.individu.Qf2bNbreEnfantNeVivantF.GetValueOrDefault() != 0)
                {
                    int nbre = Convert.ToInt32(this.individu.Qf2aNbreEnfantVivantM.GetValueOrDefault() + this.individu.Qf2bNbreEnfantNeVivantF.GetValueOrDefault());
                    if (this.individu.Q5bAge.GetValueOrDefault() >= 12 && this.individu.Q5bAge.GetValueOrDefault() <= 13)
                    {
                        if (nbre > 2)
                        {
                            nbre = 0;
                            throw new MessageException(Constant.MSG_AGE);

                        }
                    }
                    else
                    {
                        if (this.individu.Q5bAge.GetValueOrDefault() >= 13 && this.individu.Q5bAge.GetValueOrDefault() <= 19)
                        {
                            if (nbre > 3)
                            {
                                throw new MessageException("Moun sa a paka gen plis ke 3 timoun.");
                            }
                        }
                        else
                        {
                            if (this.individu.Q5bAge.GetValueOrDefault() >= 20 && this.individu.Q5bAge.GetValueOrDefault() <= 24)
                            {
                                if (nbre > 6)
                                {
                                    nbre = 0;
                                    throw new MessageException("Moun sa a paka gen plis ke 6 timoun.");
                                }
                            }
                            else
                            {
                                if (this.individu.Q5bAge.GetValueOrDefault() >= 25 && this.individu.Q5bAge.GetValueOrDefault() <= 29)
                                {
                                    if (nbre > 8)
                                    {
                                        nbre = 0;
                                        throw new MessageException("Moun sa a paka gen plis ke 8 timoun.");
                                    }
                                }
                                else
                                {
                                    if (this.individu.Q5bAge.GetValueOrDefault() >= 30 && this.individu.Q5bAge.GetValueOrDefault() <= 34)
                                    {
                                        if (nbre > 10)
                                        {
                                            nbre = 0;
                                            throw new MessageException("Moun sa a paka gen plis ke 10 timoun.");
                                        }
                                    }
                                    else
                                    {
                                        if (this.individu.Q5bAge.GetValueOrDefault() >= 35 && this.individu.Q5bAge.GetValueOrDefault() <= 39)
                                        {
                                            if (nbre > 12)
                                            {
                                                nbre = 0;
                                                throw new MessageException("Moun sa a paka gen plis 12 timoun.");
                                            }
                                        }
                                        else
                                        {
                                            if (this.individu.Q5bAge.GetValueOrDefault() >= 40 && this.individu.Q5bAge.GetValueOrDefault() <= 44)
                                            {
                                                if (nbre > 14)
                                                {
                                                    nbre = 0;
                                                    throw new MessageException("Moun sa a paka gen plis ke 14 timoun.");
                                                }
                                            }
                                            else
                                            {
                                                if (this.individu.Q5bAge.GetValueOrDefault() >= 45 && this.individu.Q5bAge.GetValueOrDefault() <= 49)
                                                {
                                                    if (nbre > 16)
                                                    {
                                                        nbre = 0;
                                                        throw new MessageException("Moun sa a paka gen plis ke 16 timoun.");
                                                    }
                                                }
                                                else
                                                {
                                                    if (this.individu.Q5bAge.GetValueOrDefault() >= 50)
                                                    {
                                                        if (nbre > 18)
                                                        {
                                                            nbre = 0;
                                                            throw new MessageException("Moun sa a paka gen plis ke 18 timoun.");
                                                        }
                                                    }
                                                }

                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region CONTRAINTE TESTANT LE NOMBRE DE GARCONS ET DE FILLE VIVANT DANS LE MENAGE
                if (this.individu.Qf2bNbreEnfantVivantF.GetValueOrDefault()!=0)
                {
                    if (this.individu.Qf2bNbreEnfantVivantF.GetValueOrDefault() > this.individu.Qf2bNbreEnfantNeVivantF.GetValueOrDefault())
                    {
                        throw new MessageException("Pa ka gen tout fi sa yo kap viv anndan menaj.");
                    }
                    
                }
                if (this.individu.Qf2aNbreEnfantVivantM.GetValueOrDefault() != 0)
                {
                    if (this.individu.Qf2aNbreEnfantVivantM.GetValueOrDefault() > this.individu.Qf1aNbreEnfantNeVivantM.GetValueOrDefault())
                    {
                        throw new MessageException("Pa ka gen tout gason sa yo kap viv anndan menaj.");
                    }

                }
                #endregion

                #region CONTRAINTE TEST SUR LE NOMBRE DE GARCONS DECLARES NES ET VIVANT DANS LE MENAGE
                //if (this.individu.Qf2NbreEnfantNeVivantG.GetValueOrDefault() != 0 && this.individu.Qf3NbreEnfantMenageG.GetValueOrDefault() != 0)
                //{
                //    if (this.individu.Qf3NbreEnfantMenageG.GetValueOrDefault() > this.individu.Qf2NbreEnfantNeVivantG.GetValueOrDefault())
                //    {
                //        throw new MessageException("" + Constant.MSG_AGE);
                //    }
                //}

                #endregion

                #region CONTRAINTE SUR LE JOUR, LE MOIS ET L'ANNEE DE LA DATE NAISSANCE

                //Contrainte sur le jour de naissance
                if (this.individu.Qf3DernierEnfantJour.GetValueOrDefault() < 0)
                    throw new MessageException("Jou a pa bon.");

                if (this.individu.Qf3DernierEnfantJour.GetValueOrDefault() != 0)
                {
                    if (this.individu.Qf3DernierEnfantJour.GetValueOrDefault() > 31)
                        throw new MessageException("Jou pa dwe depase 31.");

                    //Pour le mois de fevrier
                    if (this.individu.Qf3DernierEnfantJour.GetValueOrDefault() != 0)
                    {
                        if (this.individu.Qf3DernierEnfantMois.GetValueOrDefault() == 2)
                        {
                            if (this.individu.Qf3DernierEnfantJour.GetValueOrDefault() > 29)
                                throw new MessageException("Jou pa dwe depase 29.");
                        }
                    }
                }

                //Contrainte sur le mois
                if (this.individu.Qf3DernierEnfantMois.GetValueOrDefault() < 0)
                    throw new MessageException("Mwa a pa bon.");
                if (this.individu.Qf3DernierEnfantMois.GetValueOrDefault() != 0)
                {
                    if (this.individu.Qf3DernierEnfantMois.GetValueOrDefault() > 12)
                        throw new MessageException("Mwa pa dwe depase 12.");

                }
                //Contrainte sur l'annee
                if (this.individu.Qf3DernierEnfantAnnee.GetValueOrDefault() < 0)
                    throw new MessageException("Mwa a pa bon.");
                if (this.individu.Qf3DernierEnfantAnnee.GetValueOrDefault() != 0)
                {
                    if (this.individu.Qf3DernierEnfantAnnee.GetValueOrDefault() > 2017)
                        throw new MessageException("Ane pa dwe depase 2017.");

                }
                if (this.individu.Qf3DernierEnfantAnnee.GetValueOrDefault() != 0)
                {
                    if (this.individu.Qf3DernierEnfantAnnee.GetValueOrDefault() < 1890)
                        throw new MessageException("Ane pa dwe pi piti ke 1890.");

                }

                #endregion
                            
            }

        }
        #endregion

        #region QUESTIONS AND CONTROLS
        /// <summary>
        /// 
        /// </summary>
        /// <param Name="questionCours"></param>
        /// <param Name="reponse"></param>
        /// <param Name="control"></param>
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

                if (questionCours.NomObjet == "Individu")
                {
                    this.individu = getIndividuModel(reponseSaisie, individu);
                }

                #region Type  Geographic Question
                setGeoInformation(questionCours, reponse);
                #endregion


                //Si la reponse vient d'une Question Saisie on lui l'attribue le code de la question
                if (reponse.CodeUniqueReponse == null || reponse.CodeUniqueReponse == "")
                {
                    lastQuestion.CodeUniqueReponse = questionCours.CodeQuestion;
                    lastQuestion.CodeQuestion = questionCours.CodeQuestion;
                }
                else
                //Commune,Departement,Pays,Vqses on lui l'attribue le code de la question
                {
                    if (reponse.CodeQuestion != null)
                    {
                        if (questionEnCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Commune ||
                                questionEnCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Departement ||
                                questionEnCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Pays ||
                                questionEnCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Vqse
                            )
                        {
                            lastQuestion.CodeQuestion = reponse.CodeQuestion;
                        }
                        else
                        {
                            lastQuestion.CodeQuestion = reponse.CodeQuestion;
                        }

                    }
                    //Si elle vient d'une question de choix, on prend le code unique
                    else
                    {
                        lastQuestion = service.getQuestionReponse(reponse.CodeUniqueReponse);
                    }
                }

                if (Utils.IsNotNull(lastQuestion))
                {
                    if (isQuestionExist(listOfQuestionReponses, lastQuestion) == true)// si la question a ete deja posee
                    {
                        #region SI LA QUESTION A ETE DEJA POSEE
                        listOfCurrentQuestion = new List<QuestionReponseModel>();
                        listOfCurrentQuestion = service.searchQuestionReponse(lastQuestion.CodeQuestion);
                        if (listOfCurrentQuestion.Count == 0)// si la question posee est une question saisie
                        {
                            question = service.getQuestion(lastQuestion.CodeQuestion);
                            setGeoInformation(question, reponse);
                            question = service.getQuestion(question.QSuivant);
                        }
                        else // si est elle vient d'une question de choix, on recupere la question dans l'element selectionne
                        {
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
                            //Si a partir de la reponse, on peut passer a une question suivante, on passe directement a la question suivante
                            if (qrCurrent.QSuivant != "" && qrCurrent.QSuivant != "FIN")
                            {
                                Module = service.getQuestion(qrCurrent.CodeQuestion).CodeCategorie;//Change le module
                                reponseSaisie.NomChamps = service.getQuestion(qrCurrent.CodeQuestion).NomChamps;
                                reponseSaisie.CodeReponse = reponse.CodeReponse;
                                this.individu = getIndividuModel(reponseSaisie, this.individu);
                                question = service.getQuestion(qrCurrent.QSuivant);
                            }
                            else
                            {
                                //sinon on teste si le suivant de la question met fin au questionnaire. si c'est le cas
                                //on efface les controls qui se trouvent au dessous du formulaire et on met le bouton enregistrer
                                if (qrCurrent.QSuivant == "FIN")
                                {
                                    int index = mainGrid.Children.IndexOf(control);
                                    int taille = mainGrid.Children.Count;
                                    mainGrid.Children.RemoveRange(index + 1, taille);
                                    questionCours = service.getQuestion(qrCurrent.CodeQuestion);
                                    if (questionCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Choix)
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
                                    mainGrid.Children.Add(btn);
                                    btn.Click += btn_Click;
                                }
                                // si le suivant de la reponse est une autre question, on passe directement a elle
                                else
                                {
                                    questionCours = service.getQuestion(qrCurrent.CodeQuestion);
                                    reponseSaisie.NomChamps = questionCours.NomChamps;
                                    reponseSaisie.CodeReponse = reponse.CodeReponse;
                                    this.individu = getIndividuModel(reponseSaisie, this.individu);
                                    question = service.getQuestion(questionCours.QSuivant);
                                }

                            }
                        }
                        //On retire la question posee dans la liste des questions posees
                        Utilities.removeQuestionModel(listOfQuestions, question);
                        if (question.CodeQuestion != null)
                        {
                            listOfQuestions.Add(question);// on l'ajoute a nouveau
                            //On charge les modalites de reponses s'il y en a
                            if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Commune ||
                                question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Departement ||
                                question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Pays ||
                                question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Vqse ||
                                question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Choix
                            )
                            {
                                listOfAnswer = setAnswers(question);

                            }
                            //Efface le contenu de la grille a partir de l'index de l'element selectionne
                            int index = mainGrid.Children.IndexOf(control);
                            int taille = mainGrid.Children.Count;
                            mainGrid.Children.RemoveRange(index + 1, taille);
                            //
                            //Si la question en cours est de Type choix(Commune, pays, departement, section communale et des modalites),
                            //on initiale le control avec un combox
                            questionCours = question;
                            if (questionCours.TypeQuestion == (int)utils.Constant.TypeQuestion.Choix ||
                                questionCours.TypeQuestion == (int)utils.Constant.TypeQuestion.Pays ||
                                questionCours.TypeQuestion == (int)utils.Constant.TypeQuestion.Commune ||
                                questionCours.TypeQuestion == (int)utils.Constant.TypeQuestion.Departement ||
                                questionCours.TypeQuestion == (int)utils.Constant.TypeQuestion.Vqse)
                            {
                                comboBox = (ComboBox)control;
                                thick = comboBox.Margin;
                            }
                            //Si c'est une question de saisie libre, on initialise le control a une textbox
                            else
                            {
                                if (questionCours.TypeQuestion == (int)utils.Constant.TypeQuestion.Saisie)
                                {
                                    textbox = (TextEdit)control;
                                    thick = textbox.Margin;
                                }
                            }
                            //Puis on cree les controls
                            foreach (QuestionsModel q in listOfQuestions)
                            {
                                if (q.CodeQuestion == question.CodeQuestion)
                                {

                                }
                                else
                                {
                                    setContentsOfControls(question, thick, listOfAnswer, isCategorie, mainGrid);
                                    break;
                                }

                            }

                        }

                        #endregion

                    }
                    // si c'est pour la premiere fois on pose la question
                    else
                    {
                        //Si l'utilisateur a apportee certaines modifications 
                        //dans une partie du  questionnaire, on recupere la question en cours dans la reponse 
                        if (lastQuestion.CodeQuestion != questionCours.CodeQuestion)
                        {
                            questionCours = service.getQuestion(service.getQuestionReponse(reponse.CodeUniqueReponse).CodeQuestion);
                            Module = service.getQuestion(questionCours.CodeQuestion).CodeCategorie;
                        }
                        #region SI IL Y A UN SAUT
                        //S'il y a un saut dans la question, on passe dans le saut
                        if (questionCours.EstSautReponse.GetValueOrDefault() == true)
                        {
                            //On charge les modalites de reponse de la question
                            listOfCurrentQuestion = service.searchQuestionReponse(questionCours.CodeQuestion);
                            //On compare la reponse selectionnee par l'utilisateur avec l'une des modalites de reponses
                            //pour pouvoir recuperer la question
                            qrCurrent = new QuestionReponseModel();
                            for (int i = 0; i < listOfCurrentQuestion.Count; i++)
                            {
                                if (reponse.CodeUniqueReponse == listOfCurrentQuestion.ElementAt(i).CodeUniqueReponse)
                                {
                                    qrCurrent = listOfCurrentQuestion.ElementAt(i);
                                }
                            }
                            listOfQuestionReponses.Add(qrCurrent);
                            //On passe a la question suivante a partir de la reponse selectionne par l'utilisateur
                            //Si la question suivante est "FIN", on met fin au questionnaire et on ajoute le bouton enregistrer
                            if (qrCurrent.QSuivant == "FIN")
                            {
                                if (questionCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Choix)
                                {
                                    comboBox = control as ComboBox;
                                }
                                else
                                {
                                    textbox = control as TextEdit;
                                }
                                Button btn = new Button();
                                btn.HorizontalAlignment = HorizontalAlignment.Left;
                                btn.VerticalAlignment = VerticalAlignment.Top;
                                btn.Width = 85;
                                btn.Height = 26;
                                btn.Margin = Utilities.getThickness(thick);
                                btn.Content = "Anrejistre";
                                mainGrid.Children.Add(btn);
                                btn.Click += btn_Click;
                            }
                            else
                            {
                                //Sinon on passe a la question suivante qui se trouve dans la reponse selectionnee par l'utilisateur
                                question = service.getQuestion(qrCurrent.QSuivant); //Question suivante
                                if (Utils.IsNotNull(question))
                                {
                                    if (isQuestionExist(question, listOfQuestions) == false)
                                    {
                                        listOfQuestions.Add(question);
                                        listOfReponses.Add(reponse);
                                        listOfAnswer = setAnswers(question);

                                        //On initialise les controls
                                        foreach (QuestionsModel q in listOfQuestions)
                                        {
                                            if (q.CodeQuestion == question.CodeQuestion)
                                            {

                                            }
                                            else
                                            {
                                                setContentsOfControls(question, thick, listOfAnswer, isCategorie, mainGrid);
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
                            setQuestionTypeAndControl(question);
                        }
                        #endregion
                    }
                }
            }
            #region Exceptions
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (MessageFinException ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                int index = mainGrid.Children.IndexOf(control);
                int taille = mainGrid.Children.Count;
                mainGrid.Children.RemoveRange(index + 1, taille);
                Button btn = new Button();
                btn.HorizontalAlignment = HorizontalAlignment.Left;
                btn.VerticalAlignment = VerticalAlignment.Top;
                btn.Width = 85;
                btn.Height = 26;
                btn.Margin = Utilities.getThickness(thick);
                btn.Content = "Anrejistre";
                mainGrid.Children.Add(btn);
                btn.Click += btn_Click;

            }
            catch (SautException)
            {
                //if (this.individu.Q5bAge.GetValueOrDefault() <= 3 && this.individu.Q5bAge.GetValueOrDefault() < 5)
                //{
                //    if (this.individu.Qp12DomicileAvantRecensement.GetValueOrDefault() == 4)
                //    {
                //        questionCours = service.getQuestion("QE2");
                //        listOfAnswer = setAnswers(questionCours);
                //        setContentsOfControls(questionCours, thick, listOfAnswer, isCategorie, mainGrid);
                //    }
                //}
                //else
                setContentsOfControls(questionEnCours, thick, listOfAnswer, isCategorie, mainGrid);

            }

            catch (Exception ex)
            {
                log.Info("==============Exception/setQuestionAndControls:" + ex.Message);
            }
            #endregion

        }


        public void setContentsOfControls(QuestionsModel question, Thickness thickSet, List<ReponseModel> listOfAnswer, bool isCategorie, Grid grd)
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
                    tb.Width = 705;
                    tb.Height = 35;
                    tb.Text = question.Libelle;
                    tb.Margin = Utilities.getThickness(thickSet);
                    tb.FontWeight = FontWeights.Bold;
                    grd.Children.Add(tb);
                    thick = Utilities.getThickness(thickSet);
                    if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Choix)
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

                            grd.Children.Add(cmb);
                            comboBox = cmb;
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
                            grd.Children.Add(cmb);
                            comboBox = cmb;
                            thick = Utilities.getThickness(thick);
                            questionEnCours = question;
                        }
                    }

                    else
                    {
                        if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Saisie)
                        {

                            if (question.ContrainteQuestion.GetValueOrDefault() == (int)Constant.Contrainte.Numerique)
                            {
                                if (question.NomChamps == Constant.Q5bAge)
                                {

                                    TextEdit txt = new TextEdit();
                                    txt.Margin = Utilities.getThickness(thick);
                                    txt.HorizontalAlignment = HorizontalAlignment.Left;
                                    txt.VerticalAlignment = VerticalAlignment.Top;
                                    txt.Width = 505;
                                    txt.Height = 21;
                                    txt.MaskType = MaskType.Numeric;
                                    txt.Mask = "n0";
                                    grd.Children.Add(txt);
                                    textbox = txt;
                                    thick = Utilities.getThickness(thick);
                                    questionEnCours = question;
                                }

                            }
                            else
                            {
                                TextEdit txt = new TextEdit();
                                txt.Margin = Utilities.getThickness(thick);
                                txt.HorizontalAlignment = HorizontalAlignment.Left;
                                txt.VerticalAlignment = VerticalAlignment.Top;
                                txt.Width = 505;
                                txt.Height = 21;
                                grd.Children.Add(txt);
                                textbox = txt;
                                thick = Utilities.getThickness(thick);
                                questionEnCours = question;
                            }
                        }
                    }

                    Button btn = new Button();
                    btn.HorizontalAlignment = HorizontalAlignment.Left;
                    btn.VerticalAlignment = VerticalAlignment.Top;
                    btn.Width = 85;
                    btn.Height = 26;
                    btn.Margin = Utilities.getThickness(thick);
                    btn.Content = "Anrejistre";
                    grd.Children.Add(btn);
                    btn.Click += btn_Click;

                }
                #endregion

                #region SI C'EST PAS DERNIERE QUESTION
                else
                {
                    #region SI LA CATEGORIE CHANGE
                    if (Module != question.CodeCategorie)
                    {

                        Module = service.getCategorieQuestion(question.CodeCategorie).CodeCategorie;
                        NameOfModule = service.getCategorieQuestion(question.CodeCategorie).CategorieQuestion;
                        Button btn = new Button();
                        btn.Content = "Kontinye";
                        btn.Margin = thick;
                        btn.Width = 120;
                        btn.Height = 22;
                        btn.Click += btnSuivant_Click;
                        questionEnCours = new QuestionsModel();
                        questionEnCours = question;
                        btnSuivant = btn;
                        grd.Children.Add(btnSuivant);
                    }
                    #endregion
                    else
                    {
                        //Ajouter le libelle de la question//
                        //Si la question en cours est Qevd1 affiche une fenetre
                        if (question.NomObjet == "Evaluation")
                        {
                            if (question.CodeQuestion == "Qevd1")
                            {

                            }
                            else
                            {
                                TextBlock tb = new TextBlock();
                                tb.TextWrapping = TextWrapping.Wrap;
                                tb.HorizontalAlignment = HorizontalAlignment.Left;
                                tb.VerticalAlignment = VerticalAlignment.Top;
                                tb.Width = 705;
                                tb.Height = 35;
                                tb.Text = question.Libelle.Replace("{0}", individu.Q3Prenom);
                                tb.Margin = Utilities.getThickness(thickSet);
                                tb.FontWeight = FontWeights.Bold;
                                grd.Children.Add(tb);
                                thick = Utilities.getThickness(thickSet);
                            }
                        }
                        else
                        {
                            TextBlock tb = new TextBlock();
                            tb.TextWrapping = TextWrapping.Wrap;
                            tb.HorizontalAlignment = HorizontalAlignment.Left;
                            tb.VerticalAlignment = VerticalAlignment.Top;
                            tb.Width = 705;
                            tb.Height = 35;
                            tb.Text = question.Libelle.Replace("{0}", individu.Q3Prenom);
                            tb.Margin = Utilities.getThickness(thickSet);
                            tb.FontWeight = FontWeights.Bold;
                            grd.Children.Add(tb);
                            thick = Utilities.getThickness(thickSet);
                        }

                        #region TYPE QUESTION CHOIX
                        if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Choix)
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
                                cmb.SelectedIndex = -1;
                                grd.Children.Add(cmb);

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
                                grd.Children.Add(cmb);
                                comboBox = cmb;
                                thick = Utilities.getThickness(thick);
                                questionEnCours = question;
                                cmb.SelectionChanged += cmb_SelectionChanged;
                            }
                        }
                        #endregion

                        else
                        {
                            #region TYPE QUESTION SAISIE

                            if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Saisie)
                            {
                                if (question.ContrainteQuestion.GetValueOrDefault() == (int)Constant.Contrainte.Numerique)
                                {
                                    if (question.NomChamps == Constant.Q5bAge)
                                    {
                                        //int Age = 2016 - individu.Qp5AnneeNaissance.GetValueOrDefault();
                                        TextEdit txt = new TextEdit();
                                        //txt.Text = Age.ToString();
                                        txt.Margin = Utilities.getThickness(thick);
                                        txt.HorizontalAlignment = HorizontalAlignment.Left;
                                        txt.VerticalAlignment = VerticalAlignment.Top;
                                        txt.Width = 505;
                                        txt.Height = 21;
                                        txt.MaskType = MaskType.Numeric;
                                        txt.Mask = "n0";
                                        grd.Children.Add(txt);
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
                                        txt.MaskType = MaskType.Numeric;
                                        txt.Mask = "n0";
                                        grd.Children.Add(txt);
                                        textbox = txt;
                                        txt.KeyDown += txt_KeyDown;
                                        thick = Utilities.getThickness(thick);
                                        questionEnCours = question;
                                    }

                                }
                                else
                                {
                                    TextEdit txt = new TextEdit();
                                    txt.Margin = Utilities.getThickness(thick);
                                    txt.HorizontalAlignment = HorizontalAlignment.Left;
                                    txt.VerticalAlignment = VerticalAlignment.Top;
                                    txt.Width = 505;
                                    txt.Height = 21;
                                    grd.Children.Add(txt);
                                    textbox = txt;
                                    txt.KeyDown += txt_KeyDown;
                                    thick = Utilities.getThickness(thick);
                                    questionEnCours = question;
                                }

                            }
                            #endregion

                            #region TYPE QUESTION PAYS/DEPARTEMENT/COMMUNNE/SECTION/COMMUNALE
                            if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Commune ||
                                question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Departement ||
                                question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Pays ||
                                question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Vqse)
                            {
                                ComboBox cmb = new ComboBox();
                                cmb.Margin = Utilities.getThickness(thick);
                                cmb.HorizontalAlignment = HorizontalAlignment.Left;
                                cmb.VerticalAlignment = VerticalAlignment.Top;
                                cmb.Width = 505;
                                cmb.Height = 21;
                                cmb.ItemsSource = listOfAnswer;
                                cmb.DisplayMemberPath = "LibelleReponse";
                                grd.Children.Add(cmb);
                                comboBox = cmb;
                                thick = Utilities.getThickness(thick);
                                questionEnCours = question;
                                cmb.SelectionChanged += cmb_SelectionChanged;
                            }
                            #endregion

                            #region TYPE QUESTION AUTOMATIQUE

                            else
                            {
                                if (question.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Automatique)
                                {
                                    if (question.NomObjet == "Evaluation")
                                    {
                                        if (question.CodeQuestion == "Qevd1")
                                        {
                                            frm_details_personnes_EV frm_details = new frm_details_personnes_EV(menage);
                                            grd.Children.Add(frm_details);
                                            thick = frm_details.Margin;
                                            thick = new Thickness(10, 400, 0, 0);
                                            Button btn = new Button();
                                            btn.Content = "Kontinye";
                                            btn.Margin = thick;
                                            btn.Width = 120;
                                            btn.Height = 22;
                                            btn.Click += btnSuivant_Click;
                                            questionEnCours = new QuestionsModel();
                                            questionEnCours = service.getQuestion("Qeve1");
                                            listOfAnswer = setAnswers(questionEnCours);
                                            Module = questionEnCours.CodeCategorie;
                                            NameOfModule = service.getCategorieQuestion(questionEnCours.CodeCategorie).CategorieQuestion;
                                            btnSuivant = btn;
                                            grd.Children.Add(btnSuivant);
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                    }
                }
                #endregion
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TabItem newTab = AddTabItem();
                thick = new Thickness(10, 35, 0, 0);
                comboBox = new ComboBox();
                comboBox.Margin = thick;
                tabIndex += 1;
                if (tabIndex == tabControlLength)
                {
                    if (questionEnCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Choix ||
                        questionEnCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Automatique)
                    {
                        ReponseModel reponse = mainReponse;

                        if (reponse.CodeReponse == "")
                        {
                            MessageBox.Show("Ou dwe reponn tout kesyon yo", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            setContentsOfControls(questionEnCours, thick, listOfAnswer, isCategorie, mainGrid);
                        }
                    }
                    else
                    {
                        if (questionEnCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Saisie)
                        {
                            textbox.Margin = thick;
                            ReponseModel rep = new ReponseModel();
                            rep.CodeReponse = textbox.Text;
                            setContentsOfControls(questionEnCours, thick, listOfAnswer, isCategorie, mainGrid);
                        }
                    }
                    tab_main.Items.Add(newTab);
                    tab_main.SelectedItem = newTab;
                    if (questionEnCours.CodeQuestion == "Qeve1")
                    {
                        btnSuivant.IsEnabled = true;
                    }
                    else
                    {
                        btnSuivant.IsEnabled = false;
                    }

                }
                else
                {
                    if (tabIndex < tabControlLength)
                    {
                        tab_main.SelectedItem = tab_main.Items.GetItemAt(tabControlLength - 1);

                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private TabItem AddTabItem()
        {
            //Aouter un tab dans la grille
            try
            {
                int count = tab_main.Items.Count;
                tabControlLength = count;
                // create new tab item
                TabItem tab = new TabItem();

                tab.Header = string.Format(NameOfModule);
                tab.Name = "tab1";
                tab.HeaderTemplate = tab_main.FindResource("TabHeader") as DataTemplate;

                // add controls to tab item, this case I added just a textbox
                Grid grd_tab = new Grid();
                grd_tab.Name = string.Format("grd_tab{0}", count);
                tab.Content = grd_tab;
                thick = new Thickness(10, 35, 0, 0);
                mainGrid = grd_tab;
                _tabItems.Insert(count - 1, tab);

                return tab;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        void scrl_vwr_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            throw new NotImplementedException();
        }

        //Gerer la derniere question dans le formulaire
        void setLastQuestionBeforeSave<T>(T objet)
        {
            try
            {
                DateTime dayEnd = DateTime.Now;
                //Si la derniere question est de Type choix on recupere la reponse dans un combobox
                if (questionEnCours.TypeQuestion.GetValueOrDefault() == Convert.ToInt32(Constant.TypeQuestion.Choix))
                {
                    ReponseModel reponse = comboBox.SelectedItem as ReponseModel;
                    //On teste si on n'a pas selectionne une reponse
                    if (reponse.CodeReponse == "")
                    {
                        MessageBox.Show("Ou dwe reponn tout kesyon yo", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    //sinon
                    else
                    {
                        if (objet.ToString() == Constant.OBJET_MODEL_INDIVIDUCE)
                        {
                            reponseSaisie.NomChamps = questionEnCours.NomChamps;
                            reponseSaisie.CodeReponse = reponse.CodeReponse;
                            if (reponseSaisie.NomChamps == Constant.Qa1ActEconomiqueDerniereSemaine &&
                                this.individu.Q4Sexe.GetValueOrDefault() == 1)
                            {
                                this.individu.DureeSaisie = Utilities.getDureeSaisie(dateStart, dayEnd);
                                this.individu.IsContreEnqueteMade = 1;
                                bool result = contreEnqueteService.updateIndividuCE(objet as IndividuCEModel);
                                IndividuModel ind = new IndividuModel();
                                ind.IndividuId = this.individu.IndividuId;
                                ind.SdeId = this.individu.SdeId;
                                ind.IsContreEnqueteMade = true;
                                sw.contreEnqueteMade<IndividuModel>(ind, ind.SdeId);
                                MessageBox.Show("Endividi sa a anregistre", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                //Fire les controles sur l'interface (Le check mark)
                                detailsViewModel.Status = true;
                                detailsViewModel.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_anket_fet);
                                detailsViewModel.ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
                            }
                            else
                            {
                                this.individu.DureeSaisie = Utilities.getDureeSaisie(dateStart, dayEnd);
                                this.individu.IsContreEnqueteMade = 1;
                                individu = getIndividuModel(reponseSaisie, individu);
                                bool result = contreEnqueteService.updateIndividuCE(objet as IndividuCEModel);
                                IndividuModel ind = new IndividuModel();
                                ind.IndividuId = this.individu.IndividuId;
                                ind.SdeId = this.individu.SdeId;
                                ind.IsContreEnqueteMade = true;
                                sw.contreEnqueteMade<IndividuModel>(ind, ind.SdeId);
                                MessageBox.Show("Endividi sa a anregistre", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                //Fire les controles sur l'interface (Le check mark)
                                detailsViewModel.Status = true;
                                detailsViewModel.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_anket_fet);
                                detailsViewModel.ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
                            }
                        }
                    }
                }
                else
                {
                    //si la derniere question est Type sasie
                    if (textbox.Text == "")
                    {
                        MessageBox.Show("Ou dwe reponn tout kesyon yo", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        if (objet.ToString() == Constant.OBJET_MODEL_INDIVIDUCE)
                        {

                            reponseSaisie.NomChamps = questionEnCours.NomChamps;
                            reponseSaisie.CodeReponse = textbox.Text;
                            individu = getIndividuModel(reponseSaisie, individu);
                            this.individu.DureeSaisie = Utilities.getDureeSaisie(dateStart, dayEnd);
                            this.individu.IsContreEnqueteMade = 1;
                            bool result = contreEnqueteService.updateIndividuCE(objet as IndividuCEModel);
                            IndividuModel ind = new IndividuModel();
                            ind.IndividuId = this.individu.IndividuId;
                            ind.SdeId = this.individu.SdeId;
                            ind.IsContreEnqueteMade = true;
                            sw.contreEnqueteMade<IndividuModel>(ind, ind.SdeId);
                            MessageBox.Show("Endividi sa a anregistre", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                    }
                }
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                log.Info("<>======================Exception/setLastQuestionBeforeSave:" + ex.Message);
            }

        }
        private void scrl_bar_1_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        //Evenemet lorsqu'on selectionne un tab dans la grille
        private void tab_main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.Source is TabControl)
                {
                    Grid grid = new Grid();
                    TabItem item = tab_main.SelectedItem as TabItem;
                    tabIndex = tab_main.SelectedIndex;
                    log.Info("Taille Tab:" + tabControlLength);
                    log.Info("Index:" + tabIndex);
                    if (item != null)
                    {
                        ScrollViewer view = item.Content as ScrollViewer;
                        if (view != null)
                        {
                            grid = view.Content as Grid;
                            if (grid != null)
                            {
                                log.Info("Name:" + grid.Name);
                                mainGrid = grid;
                            }
                        }
                        else
                        {
                            grid = new Grid();
                            grid = item.Content as Grid;
                            if (grid != null)
                            {
                                log.Info("Name:" + grid.Name);
                                mainGrid = grid;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void setQuestionTypeAndControl(QuestionsModel question)
        {
            if (Utils.IsNotNull(question))
            {
                if (isQuestionExist(question, listOfQuestions) == false)
                {
                    listOfQuestions.Add(question);
                    listOfAnswer = setAnswers(question);

                    foreach (QuestionsModel q in listOfQuestions)
                    {
                        if (q.CodeQuestion == question.CodeQuestion)
                        {

                        }
                        else
                        {
                            setContentsOfControls(question, thick, listOfAnswer, isCategorie, mainGrid);
                        }
                        break;
                    }

                }

            }
        }

        //Recupere le code du departement ou de la commune/ section communale ou le pays
        public void setGeoInformation(QuestionsModel question, ReponseModel reponse)
        {
            if (question.TypeQuestion == (int)Constant.TypeQuestion.Pays)
            {
                Pays = reponse.CodeUniqueReponse;
            }
            if (question.TypeQuestion == (int)Constant.TypeQuestion.Departement)
            {
                Departement = reponse.CodeUniqueReponse;
            }
            if (question.TypeQuestion == (int)Constant.TypeQuestion.Commune)
            {
                Commune = reponse.CodeUniqueReponse;
            }
            if (question.TypeQuestion == (int)Constant.TypeQuestion.Vqse)
            {
                Vqse = reponse.CodeUniqueReponse;
            }
        }

        public List<ReponseModel> setAnswers(QuestionsModel question)
        {
            List<ReponseModel> listOfAnswers = new List<ReponseModel>();

            if (question.TypeQuestion == (int)Constant.TypeQuestion.Pays)
            {
                List<PaysModel> listOfPays = contreEnqueteService.searchAllPays();
                foreach (PaysModel pays in listOfPays)
                {
                    ReponseModel rep = new ReponseModel();
                    rep.CodeUniqueReponse = pays.CodePays;
                    rep.LibelleReponse = pays.NomPays;
                    rep.CodeQuestion = question.CodeQuestion;
                    rep.CodeReponse = pays.CodePays;
                    listOfAnswers.Add(rep);
                }
            }
            else
            {
                //On cherche les departements si la question est de Departement, on le charge dans les combox
                if (question.TypeQuestion == (int)Constant.TypeQuestion.Departement)
                {
                    listOfAnswers = new List<ReponseModel>();
                    List<DepartementModel> listOfDept = contreEnqueteService.searchAllDepartement();
                    foreach (DepartementModel pays in listOfDept)
                    {
                        ReponseModel rep = new ReponseModel();
                        rep.CodeUniqueReponse = pays.DeptId;
                        rep.LibelleReponse = pays.DeptNom;
                        rep.CodeQuestion = question.CodeQuestion;
                        rep.CodeReponse = pays.DeptId;
                        listOfAnswers.Add(rep);
                    }
                }
                else
                {
                    //On cherche les communes si la question est de Commune, on le charge dans les combox
                    if (question.TypeQuestion == (int)Constant.TypeQuestion.Commune)
                    {
                        listOfAnswers = new List<ReponseModel>();
                        List<CommuneModel> listOfCom = contreEnqueteService.searchAllCommuneByDept(Departement);
                        foreach (CommuneModel com in listOfCom)
                        {
                            ReponseModel rep = new ReponseModel();
                            rep.CodeUniqueReponse = com.ComID;
                            rep.LibelleReponse = com.ComNom;
                            rep.CodeQuestion = question.CodeQuestion;
                            rep.CodeReponse = com.ComID;
                            listOfAnswers.Add(rep);
                        }
                    }
                    else
                    {
                        //On cherche les sections communales si la question est de Vqse, on le charge dans les combox
                        if (question.TypeQuestion == (int)Constant.TypeQuestion.Vqse)
                        {
                            listOfAnswers = new List<ReponseModel>();
                            List<VqseModel> listOfVqse = contreEnqueteService.searchAllVqsebyCom(Commune);
                            foreach (VqseModel vqse in listOfVqse)
                            {
                                ReponseModel rep = new ReponseModel();
                                rep.CodeUniqueReponse = vqse.VqseId;
                                rep.LibelleReponse = vqse.VqseNom;
                                rep.CodeQuestion = question.CodeQuestion;
                                rep.CodeReponse = vqse.VqseId;
                                listOfAnswers.Add(rep);
                            }
                        }
                        else
                        {
                            //On cherche les modalites si la question est de Type choix
                            List<QuestionReponseModel> listOfCurrentQuestion = service.searchQuestionReponse(question.CodeQuestion);
                            if (listOfCurrentQuestion.Count == 0)
                            {

                            }
                            else
                            {
                                listOfAnswers = new List<ReponseModel>();
                                foreach (QuestionReponseModel qr in listOfCurrentQuestion)
                                {
                                    if (qr.EstEnfant.GetValueOrDefault() == true)
                                    {
                                        isCategorie = true;
                                        ReponseModel rep = service.getReponse(qr.CodeUniqueReponse);
                                        rep.Category = service.getReponse(qr.CodeParent).LibelleReponse;
                                        listOfAnswers.Add(rep);
                                    }
                                    else
                                    {
                                        if (qr.AvoirEnfant.GetValueOrDefault() == true && qr.CodeParent == "")
                                        {

                                        }
                                        else
                                        {
                                            if (qr.CodeParent == "" || qr.CodeParent == "0")
                                            {
                                                ReponseModel rep = service.getReponse(qr.CodeUniqueReponse);
                                                listOfAnswers.Add(rep);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return listOfAnswers;

        }
        /////////////////////////////
    }
}
