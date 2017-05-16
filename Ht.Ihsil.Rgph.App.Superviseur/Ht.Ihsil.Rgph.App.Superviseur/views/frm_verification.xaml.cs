using DevExpress.Xpf.Charts;
using Ht.Ihsil.Rgph.App.Superviseur.entites;
using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
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

namespace Ht.Ihsil.Rgph.App.Superviseur.views
{
    /// <summary>
    /// Logique d'interaction pour frm_verification.xaml
    /// </summary>
    public partial class frm_verification : UserControl
    {

        #region DECLARATION
        private static string MAIN_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Data\Databases\";
        ISqliteReader reader;
        bool tabNotesAlreadyOpen = false;
        private SdeModel sdeSelected = null;
        List<BatimentModel> listOfBatiments;
        NameValue TypeDifficulte;
        #endregion

        #region CONSTRUCTORS
        public frm_verification(SdeModel sde)
        {
            InitializeComponent();
            sdeSelected = sde;
            listOfBatiments = new List<BatimentModel>();
            //Style of the tabcontrol



            reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sde.SdeId));
            List<TableVerificationModel> verificationsNonReponseTotal = Utilities.getVerificatoinNonReponseTotal(MAIN_DATABASE_PATH, sde.SdeId);
            dtg_non_reponse_totale.ItemsSource = verificationsNonReponseTotal;
            List<TableVerificationModel> verificationsNonReponsePartielle = Utilities.getVerificationNonReponsePartielle(MAIN_DATABASE_PATH, sde.SdeId);
            dtg_non_reponse_partielle.ItemsSource = verificationsNonReponsePartielle;
            List<VerificationFlag> verficationFlags = Utilities.getVerificationNonReponseParVariable(MAIN_DATABASE_PATH, sde.SdeId);
            dtg_non_reponse_totale_variable.ItemsSource = verficationFlags;

            //reader.getTotalIndividusFemmes()
            //Utilities.getPourcentage(reader.getTotalIndividusFemmes(),reader.getTotalIndividus())
            //reader.getTotalIndividu10AnsEtPlus()
            //reader.getTotalIndividu18AnsEtPlus()


            pieSeriesNbreFemmes.Points.Add(new SeriesPoint("Nombre de personnes recensées", 50));
            pieSeriesNbreFemmes.Points.Add(new SeriesPoint("% de femmes dans la population recensée", 40));

            barSeriesProportionsDetails.Points.Add(new SeriesPoint("Proportion (%) d´enfants de moins d´un (1) an", 50));
            barSeriesProportionsDetails.Points.Add(new SeriesPoint("Proportion (%)  de personnes de 10 ans et plus", 25));
            barSeriesProportionsDetails.Points.Add(new SeriesPoint("Proportion (%)  de personnes de 18 ans et plus", 45));
            barSeriesProportionsDetails.Points.Add(new SeriesPoint("Proportion (%)  de personnes de 65 ans plus", reader.getTotalIndividu65AnsEtPlus()));

            barSeriesIndCouverture.Points.Add(new SeriesPoint("Nombre de Batiments", 45));
            barSeriesIndCouverture.Points.Add(new SeriesPoint("Nombre de Logements Individuels", 90));
            barSeriesIndCouverture.Points.Add(new SeriesPoint("Nombre de Menages", 150));
            barSeriesIndCouverture.Points.Add(new SeriesPoint("Nombre de Personnes", 280));

            //reader.getTotalPersonnesByLogementCollections()
            //reader.getTotalPersonnesByLimitation()

            barSeriesIndividus.Points.Add(new SeriesPoint("Nombre de personnes recensées dans les logements collectifs", 50));
            barSeriesIndividus.Points.Add(new SeriesPoint("Nombre de personnes présentant au moins une limitation dans leurs activités. ", 50));

            barSeriesTailleMenage.Points.Add(new SeriesPoint("Taille moyenne des ménages", 85));
            barSeriesTailleMenage.Points.Add(new SeriesPoint("Proportion (%)  de ménages unipersonnels (1 personne au plus)", reader.getTotalMenageUnipersonnel()));
            barSeriesTailleMenage.Points.Add(new SeriesPoint("Proportion (%)  de ménages de grande taille (6  et plus)", reader.getTotalMenageDe6IndsEtPlus()));

            //reader.getTotalMenages()
            //reader.getTotalFemmeChefMenage()

            pieSeriesNbreMenage.Points.Add(new SeriesPoint("Nombre de ménages recensés", 50));
            pieSeriesNbreMenage.Points.Add(new SeriesPoint("Proportion (%)  de ménages dont le chef est une femme. ", 45));

            //
            dataGridCouverture.ItemsSource = Utilities.getTotalCouverture(MAIN_DATABASE_PATH, sde);

        }
        public frm_verification()
        {
            InitializeComponent();
            List<TableVerificationModel> verificationsNonReponseTotal = Utilities.getVerificatoinNonReponseTotalForAllSdes(MAIN_DATABASE_PATH);
            dtg_non_reponse_totale.ItemsSource = verificationsNonReponseTotal;

        }
        #endregion

        #region GESTION DES NOTES
        private void tabGestionNotes_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tabNotesAlreadyOpen == false)
            {
                //Creation de la liste des problemes et leurs codes;
                cmbTypeProbleme.ItemsSource = Constant.ListOfDifficultes();
                cmbObjet.ItemsSource = Utilities.getNameOfObjects();
                btn_save.IsEnabled = false;
                tabNotesAlreadyOpen = true;
            }
        }

        private void cmbObjet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            NameValue selectedName = (NameValue)cmb.SelectedItem;
            if (selectedName != null)
            {
                txtLibelle.Text = "";
                //
                //Si la difficulte selectionnee est de type B3.17
                if (TypeDifficulte.Name == "B3.17")
                {
                    List<QuestionModule> listOfQuestions = ModelMapper.MapToListQuestionModule(reader.listOfQuestionModule(selectedName.Value));
                    List<QuestionsModel> models = new List<QuestionsModel>();
                    foreach (QuestionModule qm in listOfQuestions)
                    {

                        QuestionsModel Q = ModelMapper.MapToQuestionModel(reader.getQuestion(qm.codeQuestion));
                        models.Add(Q);
                    }
                    cmbCodeQuestion.ItemsSource = models;
                }
                //Sinon
                else
                {
                    cmbDomaine.ItemsSource = Constant.searchSectionByObjet(selectedName.Name);
                }
            }
        }

        private void cmbCodeQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            QuestionsModel selectedQuestion = (QuestionsModel)cmb.SelectedItem;
            if (selectedQuestion != null)
            {

                txtLibelle.Text = selectedQuestion.Libelle;
                cmbBatiment.ItemsSource = reader.GetAllBatimentModel();
            }
        }
        public void blankAndDisabedComponents()
        {
            txtIndicateur.Text = "";
            cmbDomaine.IsEnabled = false;
            cmbObjet.IsEnabled = false;
            cmbCodeQuestion.IsEnabled = false;
            txtNature.IsEnabled = false;
            cmbBatiment.IsEnabled = false;
            cmbBatiment.ItemsSource = new List<BatimentModel>();
            //cmbObjet.ItemsSource = new List<Na>();
            cmbDomaine.ItemsSource = new List<NameValue>();
            cmbCodeQuestion.ItemsSource = new List<QuestionsModel>();
            listBCodeBatiment.Items.Clear();
            txtNature.Text = "";
            txtLibelle.Text = "";
            btn_save.IsEnabled = false;
            listOfBatiments = new List<BatimentModel>();
            //listBCodeBatiment.ItemsSource = new List<BatimentModel>();
        }
        private void cmbBatiment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            BatimentModel batiment = (BatimentModel)cmb.SelectedItem;
            bool equal = false;
            btn_save.IsEnabled = true;
            if (batiment != null)
            {
                if (listOfBatiments.Count == 0)
                {
                    listBCodeBatiment.Items.Add(batiment);
                    listOfBatiments.Add(batiment);
                }
                else
                {
                    foreach (BatimentModel bat in listOfBatiments)
                    {
                        if (batiment.BatimentId == bat.BatimentId)
                        {
                            equal = true;
                            break;
                        }
                    }
                    if (equal == false)
                    {
                        listBCodeBatiment.Items.Add(batiment);
                        listOfBatiments.Add(batiment);
                    }

                }

            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IConfigurationService configuration = new ConfigurationService();
                NameValue valueSelected = null;
                QuestionsModel questionSelected = null;
                NameValue domaineSelected = null;
                //
                //Test si le superviseur n'a pas selectionne un objet
                if (cmbObjet.SelectedItem != null)
                {
                    valueSelected = (NameValue)cmbObjet.SelectedItem;
                }
                else
                {
                    MessageBox.Show("Ou dwe chwazi ki objè ajan an gen pwoblèm avèk li a.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                //
                //Test si le superviseur n'a pas selectionne une question
                if (cmbCodeQuestion.SelectedItem != null)
                {
                    questionSelected = (QuestionsModel)cmbCodeQuestion.SelectedItem;
                }
                else
                {
                    MessageBox.Show("Ou dwe chwazi yon kesyon.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                //
                //Si le supervisueur a choisi une domaine
                if(cmbDomaine.SelectedItem!=null){
                    domaineSelected=(NameValue)cmbDomaine.SelectedItem;
                }
                //
                //Test si le superviseur n'a pas choisi de batiments
                if (listBCodeBatiment.Items.Count != 0)
                {
                    bool result=false;
                    foreach (BatimentModel batiment in listBCodeBatiment.Items)
                    {
                        ProblemeModel probleme = new ProblemeModel();
                        probleme.Objet = valueSelected.Name;
                        probleme.SdeId = sdeSelected.SdeId;
                        probleme.CodeQuestion = questionSelected.CodeQuestion;
                        probleme.BatimentId = batiment.BatimentId;
                        probleme.Nature = txtNature.Text;
                        if (TypeDifficulte.Name == "B3.18")
                        {
                            probleme.Domaine = domaineSelected.Value;
                        }
                        if (btn_save.Content.ToString() == "Sauvegarder")
                        {
                            result = configuration.saveProbleme(probleme);
                        }
                        else
                        {
                            result = configuration.updateProbleme(probleme);
                        }
                        
                        
                    }
                    if (result == true)
                    {
                        MessageBox.Show("Anregistreman an fet ak siksè.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        blankAndDisabedComponents();
                    }
                    else
                    {
                        MessageBox.Show("Gen yon èrè pandan anregistreman.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        blankAndDisabedComponents();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gen yon pwoblem pandan anregistreman an ap fet.=>/" + ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                blankAndDisabedComponents();
            }
        }

        private void cmbTypeProbleme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            NameValue objet = (NameValue)cmb.SelectedItem;
            if (objet != null)
            {
                TypeDifficulte = objet;
                if (objet.Name == "B3.17")
                {
                    txtIndicateur.Text = objet.Value;
                    cmbDomaine.IsEnabled = false;
                    cmbObjet.IsEnabled = true;
                    cmbCodeQuestion.IsEnabled = true;
                    txtNature.IsEnabled = true;
                    cmbBatiment.IsEnabled = true;
                    listBCodeBatiment.IsEnabled = true;
                }
                else
                {
                    txtIndicateur.Text = objet.Value;
                    cmbDomaine.IsEnabled = true;
                    cmbObjet.IsEnabled = true;
                    cmbCodeQuestion.IsEnabled = true;
                    txtNature.IsEnabled = true;
                    cmbBatiment.IsEnabled = true;
                    //btn_save.IsEnabled = true;
                    listBCodeBatiment.IsEnabled = true;
                }
            }
        }

        private void cmbDomaine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            NameValue objet = (NameValue)cmb.SelectedItem;
            if (objet != null)
            {
                List<QuestionsModel> listOfQuestions = ModelMapper.MapToListQuestionModel(reader.searchQuestionByCategorie(objet.Name));
                //
                if (listOfQuestions != null)
                {
                    cmbCodeQuestion.ItemsSource = listOfQuestions;
                }
            }
        }
        #endregion

        #region CONTROL EVENTS METHODS

        private void DXTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                IConfigurationService configuration = new ConfigurationService();
                grdDifficulties.ItemsSource = configuration.searchAllProblemes();
            }
            catch (Exception)
            {

            }
        }

        private void updateRowItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            ProblemeModel row =(ProblemeModel) grdDifficulties.GetRow(grdDifficulties.GetSelectedRowHandles()[0]);
            if (row != null)
            {
                tabAjout.Header = "Modifier";
                tabControlNotes.SelectedItem = tabAjout;
                cmbObjet.IsEnabled = true;
                btn_save.Content = "Modifier";
                btn_save.IsEnabled = true;
                //
                //Selection dy type de difficulte rencontree 
                if (row.Domaine != null)
                {
                    foreach (NameValue type in cmbTypeProbleme.Items)
                    {
                        if (type.Name == "B3.18")
                        {
                            cmbTypeProbleme.SelectedItem = type;
                            break;
                        }
                        
                    }
                    //
                }
                else
                {
                    foreach (NameValue type in cmbTypeProbleme.Items)
                    {
                        if (type.Name == "B3.17")
                        {
                            cmbTypeProbleme.SelectedItem = type;
                            break;
                        }
                        
                    }
                }
                //Selectionne l'objet a modfiee
                foreach (NameValue val in cmbObjet.Items)
                {
                    if (row.Objet == val.Name)
                    {
                        cmbObjet.SelectedItem = val;
                        break;
                    }
                    
                }
                //Selection du domaine
                foreach (NameValue domaine in cmbDomaine.Items)
                {
                    if (row.Domaine == domaine.Value)
                    {
                        cmbDomaine.SelectedItem = domaine;
                    }
                }
               
                //
                //Selectionne la question a modifiee
                foreach(QuestionsModel question in cmbCodeQuestion.Items)
                {
                    if (question.CodeQuestion == row.CodeQuestion)
                    {
                        cmbCodeQuestion.SelectedItem = question;
                        //Set le libelle de la question
                        txtLibelle.Text = question.Libelle;
                        break;
                        //
                    }
                    
                }
                //
                //Set la nature de la difficultee rencontree
                if (row.Nature != null)
                {
                    txtNature.Text = row.Nature;
                }
                //
                //Set les codes des batiments selectionnes
                if (row.BatimentId != null)
                {
                    listOfBatiments = new List<BatimentModel>();
                    foreach (BatimentModel batiment in cmbBatiment.Items)
                    {
                        if (batiment.BatimentId == row.BatimentId)
                        {
                            listOfBatiments.Add(batiment);
                            listBCodeBatiment.Items.Add(batiment);
                            //List<BatimentModel> listOf=new List<BatimentModel>();
                            //listOf.Add(batiment);
                            //listBCodeBatiment.ItemsSource = listOf;
                        }
                    }
                }
               

             }
        }

        private void deleteDataItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            ProblemeModel row =(ProblemeModel) grdDifficulties.GetRow(grdDifficulties.GetSelectedRowHandles()[0]);
            if (row != null)
            {
                IConfigurationService configuration = new ConfigurationService();
                MessageBoxResult confirm = MessageBox.Show("Eske ou vle efase anrejistreman sa a?", Constant.WINDOW_TITLE, MessageBoxButton.YesNo,MessageBoxImage.Question);
                if (confirm == MessageBoxResult.Yes)
                {
                    bool result = configuration.deleteProbleme(Convert.ToInt32(row.ProblemeId));
                    if (result == true)
                    {
                        MessageBox.Show("Anregistreman sa efase.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        grdDifficulties.ItemsSource = configuration.searchAllProblemes();
                    }
                }
            }
        }

        private void dtg_non_reponse_totale_AutoGeneratingColumn(object sender, DevExpress.Xpf.Grid.AutoGeneratingColumnEventArgs e)
        {
            if (e.Column.FieldName == "Color")
                e.Column.Visible = false;

        }
        #endregion

        #region CHARTS 
        public void createGraphicControls()
        {
            int nbrePersonnes = 0;
            int nbreFemmesRecenses = 0;
            int nbreEnfant5Ans = 0;
            int nbreEnfant10Ans = 0;
            int nbrePersonnes18Ans = 0;
            int nbrePersonnes65Ans = 0;
            int nbreBatiment = 0;
            int nbreLogementC = 0;
            int nbreLogementInd = 0;
            int nbreMenage = 0;
            int nbreLimitation = 0;
            int nbreFemmeChefMenage = 0;
            int nbreMenageUniPersonnel = 0;
            int nbreMenage6Personnes = 0;


            if (sdeSelected == null)
            {
                IConfigurationService configuration = new ConfigurationService();
                foreach(SdeModel sde in configuration.searchAllSdes())
                {
                    reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sde.SdeId));
                    nbreBatiment = nbreBatiment + reader.GetAllBatimentModel().Count();
                    nbrePersonnes = nbrePersonnes + reader.GetAllIndividus().Count();
                    nbreFemmesRecenses = nbreFemmesRecenses + reader.getTotalIndividusFemmes();
                    

                }
            }
        }
        #endregion

    }
}
