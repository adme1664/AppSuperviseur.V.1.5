using Ht.Ihsi.Rgph.Logging.Logs;
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
    /// Logique d'interaction pour frm_rpt_deroulement.xaml
    /// </summary>
    public partial class frm_rpt_deroulement : UserControl
    {
        #region DECLARATIONS
        List<TabItem> _tabMainItems = null;
        List<TabItem> _tabSousItems = null;
        TabControl sousTabControl = null;
        DetailsRapportModel rapport = null;
        RapportDeroulementModel rapportDeroulement = null;
        List<DetailsRapportModel> detailsRapports = null;
        int numberOfItemsInMainTab = 0;
        int numberOfItemsInSubTab = 0;
        int indexOfMainTab = 0;
        int indexOfSubTab = 0;
        string header = null;
        Logger log;
        XmlUtils xmlReader;
        List<KeyValue> ListOfDomaines = null;
        List<KeyValue> listOfSousDomaines = null;
        List<KeyValue> listOfSolutions = null;
        List<KeyValue> listOfProblemes = null;
        List<KeyValue> listOfSuivi = null;
        Label label = null;
        ComboBox comboBoxProbleme = null;
        ComboBox comboBoxSolution = null;
        ComboBox comboBoxSuivi = null;
        TextBox textPrecision = null;
        TextBox TextSuggest = null;
        Thickness thick;
        Button btnSuivant = null;
        Button btnSave = null;
        int firstKey = 0;
        String nextTabName = null;
        IContreEnqueteService service = null;
        TabItem tab = null;
        KeyValue keyProbleme = null;
        KeyValue keySolution = null;
        KeyValue keySuivi = null;
        string precision = "";
        string suggestion = "";
        #endregion

        #region CONSTRUCTORS
        public frm_rpt_deroulement(RapportDeroulementModel rptDeroulement,frm_rpt_dereoulement_entete rpt_entete)
        {
            InitializeComponent();
            keyProbleme = new KeyValue();
            keySolution = new KeyValue();
            keySuivi = new KeyValue();
            service = new ContreEnqueteService(Users.users.SupDatabasePath);
            rapportDeroulement = new RapportDeroulementModel();
            rapportDeroulement = rptDeroulement;
            detailsRapports = new List<DetailsRapportModel>();
            _tabMainItems = new List<TabItem>();
            _tabSousItems = new List<TabItem>();
            listOfSousDomaines = new List<KeyValue>();
            ListOfDomaines = new List<KeyValue>();
            rapport = new DetailsRapportModel();
            thick = new Thickness(11, 8, 0, 0);
            log = new Logger();
            xmlReader = new XmlUtils(AppDomain.CurrentDomain.BaseDirectory + @"App_data\rapports.xml");
            int positionLastMainTab = 0;
            int positionLastSubTab = 0;
            ListOfDomaines = xmlReader.listOfDomaines();
            foreach (KeyValue key in ListOfDomaines)
            {
                positionLastMainTab++;
                positionLastSubTab = 0;
                //Retourne le premier domaines
                firstKey = key.Key;
                rapport.Domaine = firstKey;
                //
                Grid grid = new Grid();
                //Creation d'un sous table pour les sous domaines et problemes
                sousTabControl = new TabControl();
                List<KeyValue> sousDomaines = xmlReader.listOfSousDomaines(firstKey.ToString());
                //Ajout des sous-domaines sous forme de tab
                foreach (KeyValue ksd in sousDomaines)
                {
                    positionLastSubTab++;
                    //Cration d'une tab par sous domaines
                    tab = new TabItem();
                    tab.Name = "tab_" + key.Key + "_" + ksd.Key;
                    header = ksd.Value;
                    tab.Header = header;
                    rapport.SousDomaine = ksd.Key;
                    //Creation d'un grid pour ajouter dans le sous table
                    Grid gridSousTab = new Grid();
                    //Retourne les listes de problemes et des solutions a proposer
                    listOfProblemes = new List<KeyValue>();
                    listOfProblemes = xmlReader.listOfProblemes(rapport.Domaine.ToString(), rapport.SousDomaine.ToString());
                    listOfSolutions = new List<KeyValue>();
                    listOfSolutions = xmlReader.listOfSolutions(rapport.Domaine.ToString(), rapport.SousDomaine.ToString());
                    listOfSuivi = new List<KeyValue>();
                    listOfSuivi = xmlReader.listOfSuivi();
                    //
                    //Creation d'un groupbox et d'une grille
                    GroupBox group = new GroupBox();
                    group.Header = "Description";
                    Grid gridGroup = new Grid();

                    //Creation label probleme et ajout dans la grille gridGroup
                    //Label Probleme
                    label = new Label();
                    label.Content = "Intervention";
                    label.HorizontalAlignment = HorizontalAlignment.Left;
                    label.Margin = thick;
                    gridGroup.Children.Add(label);
                    //
                    //Creation du combobox et ajout dans la grille gridGroup
                    comboBoxProbleme = new ComboBox();
                    comboBoxProbleme.ItemsSource = listOfProblemes;
                    comboBoxProbleme.DisplayMemberPath = "Value";
                    thick = Utilities.getRightThickness(thick);
                    comboBoxProbleme.Margin = thick;
                    comboBoxProbleme.HorizontalAlignment = HorizontalAlignment.Left;
                    comboBoxProbleme.VerticalAlignment = VerticalAlignment.Top;
                    comboBoxProbleme.Width = 200;
                    comboBoxProbleme.SelectionChanged += comboBoxProbleme_SelectionChanged;
                    gridGroup.Children.Add(comboBoxProbleme);
                    //
                    //Label solution
                    label = new Label();
                    label.Content = "Solution";
                    label.HorizontalAlignment = HorizontalAlignment.Left;
                    thick = Utilities.getThickness(thick);
                    label.Margin = thick;
                    gridGroup.Children.Add(label);
                    //
                    //Ajout combobox solution
                    comboBoxSolution = new ComboBox();
                    comboBoxSolution.ItemsSource = listOfSolutions;
                    comboBoxSolution.DisplayMemberPath = "Value";
                    thick = Utilities.getRightThickness(thick);
                    comboBoxSolution.Margin = thick;
                    comboBoxSolution.HorizontalAlignment = HorizontalAlignment.Left;
                    comboBoxSolution.VerticalAlignment = VerticalAlignment.Top;
                    comboBoxSolution.Width = 200;
                    comboBoxSolution.SelectionChanged += comboBoxSolution_SelectionChanged;
                    gridGroup.Children.Add(comboBoxSolution);
                    //
                    //Label precisions
                    label = new Label();
                    label.Content = "Precisions";
                    label.HorizontalAlignment = HorizontalAlignment.Left;
                    thick = Utilities.getThickness(thick);
                    label.Margin = thick;
                    gridGroup.Children.Add(label);
                    //
                    //Ajout textbox pour precision
                    textPrecision = new TextBox();
                    textPrecision.HorizontalAlignment = HorizontalAlignment.Left;
                    textPrecision.VerticalAlignment = VerticalAlignment.Top;
                    textPrecision.TextWrapping = TextWrapping.Wrap;
                    textPrecision.AcceptsReturn = true;
                    textPrecision.Height = 70;
                    textPrecision.Width = 200;
                    thick = Utilities.getRightThickness(thick);
                    textPrecision.Margin = thick;
                    textPrecision.LostFocus += textPrecision_LostFocus;
                    gridGroup.Children.Add(textPrecision);
                    //
                    //Label precisions
                    label = new Label();
                    label.Content = "Suggestions";
                    label.HorizontalAlignment = HorizontalAlignment.Left;
                    thick.Top = thick.Top + 50;
                    thick = Utilities.getThickness(thick);
                    label.Margin = thick;
                    gridGroup.Children.Add(label);

                    //
                    //Ajout textbox pour precision
                    TextSuggest = new TextBox();
                    TextSuggest.HorizontalAlignment = HorizontalAlignment.Left;
                    TextSuggest.VerticalAlignment = VerticalAlignment.Top;
                    TextSuggest.TextWrapping = TextWrapping.Wrap;
                    TextSuggest.AcceptsReturn = true;
                    TextSuggest.Height = 70;
                    TextSuggest.Width = 200;
                    thick = Utilities.getRightThickness(thick);
                    TextSuggest.Margin = thick;
                    TextSuggest.LostFocus += TextSuggest_LostFocus;
                    gridGroup.Children.Add(TextSuggest);
                    //
                    //Label suivi
                    label = new Label();
                    label.Content = "Suivi";
                    label.HorizontalAlignment = HorizontalAlignment.Left;
                    thick.Top = thick.Top + 50;
                    thick = Utilities.getThickness(thick);
                    label.Margin = thick;
                    gridGroup.Children.Add(label);
                    //
                    comboBoxSuivi = new ComboBox();
                    comboBoxSuivi.ItemsSource = listOfSuivi;
                    comboBoxSuivi.DisplayMemberPath = "Value";
                    thick = Utilities.getRightThickness(thick);
                    comboBoxSuivi.Margin = thick;
                    comboBoxSuivi.HorizontalAlignment = HorizontalAlignment.Left;
                    comboBoxSuivi.VerticalAlignment = VerticalAlignment.Top;
                    comboBoxSuivi.Width = 200;
                    comboBoxSuivi.SelectionChanged += comboBoxSuivi_SelectionChanged;
                    gridGroup.Children.Add(comboBoxSuivi);
                    //
                   
                    //
                    //Ajout d'un bouton save dans la derniere
                    if (positionLastMainTab == (ListOfDomaines.Count))
                    {
                        if (positionLastSubTab == (sousDomaines.Count ))
                        {
                            btnSave = new Button();
                            btnSave.Content = "Sauvegarder";
                            thick = Utilities.getThickness(thick);
                            btnSave.Margin = thick;
                            btnSave.HorizontalAlignment = HorizontalAlignment.Left;
                            btnSave.VerticalAlignment = VerticalAlignment.Top;
                            btnSave.Width = 80;
                            btnSave.Click += btnSave_Click;
                            gridGroup.Children.Add(btnSave);
                        }
                        else
                        {
                            //Ajout d'un bouton suivant
                            btnSuivant = new Button();
                            btnSuivant.Content = "Suivant";
                            thick = Utilities.getThickness(thick);
                            btnSuivant.Margin = thick;
                            btnSuivant.HorizontalAlignment = HorizontalAlignment.Left;
                            btnSuivant.VerticalAlignment = VerticalAlignment.Top;
                            btnSuivant.Width = 80;
                            btnSuivant.Click += btnSuivant_Click;
                            gridGroup.Children.Add(btnSuivant);
                        }
                    }
                    else
                    {
                        //Ajout d'un bouton suivant
                        btnSuivant = new Button();
                        btnSuivant.Content = "Suivant";
                        thick = Utilities.getThickness(thick);
                        btnSuivant.Margin = thick;
                        btnSuivant.HorizontalAlignment = HorizontalAlignment.Left;
                        btnSuivant.VerticalAlignment = VerticalAlignment.Top;
                        btnSuivant.Width = 80;
                        btnSuivant.Click += btnSuivant_Click;
                        gridGroup.Children.Add(btnSuivant);
                    }

                    //Ajout gridGroup dans le groupbox
                    group.Content = gridGroup;
                    //
                    //Ajout du groupbox dans la grille qui est dans la tabCOntrol
                    gridSousTab.Children.Add(group);
                    tab.Content = gridSousTab;
                    sousTabControl.Items.Add(tab);
                    //reinitialiser le margin
                    thick = new Thickness(11, 8, 0, 0);

                    
                }
                //
                //Ajout 
                grid.Children.Add(sousTabControl);
                //
                tab = new TabItem();
                tab.Header = header;
                tab.Name = "tab_" + key.Key;
                tab.Content = grid;
                header = key.Value;
                //name the header of the tab
                tab.Header = header;
                //Add a grid to the first and add it to the ta                
                _tabMainItems.Add(tab);
                //               
                mainTab.Items.Add(tab);
                //break;
            }
            //
            //Disabled all tabs
            disabledTabControls();
            numberOfItemsInMainTab = mainTab.Items.Count;
            
        }

        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long rptId=0;
                if (rapportDeroulement != null)
                {
                    rapportDeroulement.DateRapport = DateTime.Now.ToString();
                     rptId = service.saveRptDeroulement(rapportDeroulement);
                }
                if (detailsRapports != null)
                {
                    foreach (DetailsRapportModel rpt in detailsRapports)
                    {
                        rpt.RapportId = rptId;
                        bool result=service.saveDetailsDeroulement(rpt);
                        log.Info("" + result);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        void TextSuggest_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox t = sender as TextBox;
                suggestion = t.Text;
                log.Info(suggestion);
            }
            catch(Exception)
            {

            }
        }

        void textPrecision_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox t = sender as TextBox;
                precision = t.Text;
                log.Info(precision);
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region CONBOXBOX EVENT SELECTIONCHANGED EVENT
        void comboBoxSuivi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                keySuivi = new KeyValue();
                keySuivi = (sender as ComboBox).SelectedItem as KeyValue;
            }
            catch (Exception)
            {

            }
        }

        void comboBoxSolution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                keySolution = new KeyValue();
                keySolution = (sender as ComboBox).SelectedItem as KeyValue;
            }
            catch (Exception)
            {

            }
        }

        void comboBoxProbleme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                keyProbleme = new KeyValue();
                keyProbleme = (sender as ComboBox).SelectedItem as KeyValue;
            }
            catch (Exception)
            {

            }
        }
        #endregion

        void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            TabItem item = mainTab.Items[indexOfMainTab] as TabItem;
            //indexOfMainTab++;
            //Cherche la tab selectionnes
            if (item.IsSelected == true)
            {
                TabItem itemIn = item;
                itemIn.Dispatcher.BeginInvoke((Action)(() => itemIn.IsEnabled = true));
                Grid grid = itemIn.Content as Grid;
                if (grid != null)
                {
                    TabControl tabC = grid.Children.Cast<UIElement>().FirstOrDefault() as TabControl;
                    if (tabC != null)
                    {
                        numberOfItemsInSubTab = tabC.Items.Count;
                        TabItem it = tabC.Items[indexOfSubTab] as TabItem;
                        //
                        //Construction de l'objet rapportModel et ajout dans la liste
                        rapport = new DetailsRapportModel();
                        rapport = getDomainesAndSousDomainesInTabName(it.Name);
                        //recuperer les valeurs dans les combo et les textbox
                        if (keyProbleme.Key != 0)
                        {
                            rapport.Probleme = keyProbleme.Key;
                            keyProbleme = new KeyValue();
                        }
                        else
                        {
                            MessageBox.Show("Ou dwe chwazi yon pwoblem", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (keySolution.Key != 0)
                        {
                            rapport.Solution = keySolution.Key;
                            keySolution = new KeyValue();
                        }
                        else
                        {
                            MessageBox.Show("Ou dwe chwazi ki solisyon w ap pote", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (keySuivi.Key != 0)
                        {
                            rapport.Suivi = keySuivi.Key.ToString();
                            keySuivi = new KeyValue();
                        }
                        else
                        {
                            MessageBox.Show("Ou dwe di ki swivi ki pou fet", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (textPrecision.Text != null)
                        {
                            rapport.Precisions = precision;
                            precision = "";
                        }
                        if (TextSuggest.Text != null)
                        {
                            rapport.Suggestions = suggestion;
                            suggestion = "";
                        }
                        detailsRapports.Add(rapport);
                        
                        //
                        //Passe a la table suivante
                        if (it.IsSelected == true)
                        {
                            //Si le nombre de tab est superieur a 1, on s'avance vers les autres tab
                            if (numberOfItemsInSubTab > 1)
                            {
                                //si le nombre de sous tab est egal au nombre de tab dans le soustab
                                if (indexOfSubTab == (numberOfItemsInSubTab - 1))
                                {
                                    indexOfMainTab++;
                                    //On selectionne la derniere tab dans le tabcontrol
                                    it.Dispatcher.BeginInvoke((Action)(() => it.IsEnabled = true));
                                    TabItem nextTab = tabC.Items[indexOfSubTab - 1] as TabItem;
                                    nextTab.Dispatcher.BeginInvoke((Action)(() => nextTab.IsEnabled = true));
                                    nextTab.Dispatcher.BeginInvoke((Action)(() => nextTab.IsSelected = true));                                 
                                    
                                    //Puis on reinitialise a 0
                                    indexOfSubTab = 0;

                                    //Si on attend le nombre maximal de tables
                                    if (indexOfMainTab == (numberOfItemsInMainTab - 1))
                                    {
                                        TabItem nextMainTab = mainTab.Items[indexOfMainTab] as TabItem;
                                        nextMainTab.Dispatcher.BeginInvoke((Action)(() => nextMainTab.IsEnabled = true));
                                        nextMainTab.Dispatcher.BeginInvoke((Action)(() => nextMainTab.IsSelected = true));
                                    }
                                    else
                                    {
                                        //si le nbre de tab dans la tabcontrol mere est atteint
                                        if (indexOfMainTab > (numberOfItemsInMainTab - 1))
                                        {
                                            return;
                                        }
                                        //sinon on passe a la table suivante
                                        else
                                        {
                                            TabItem nextMainTab = mainTab.Items[indexOfMainTab] as TabItem;
                                            nextMainTab.Dispatcher.BeginInvoke((Action)(() => nextMainTab.IsEnabled = true));
                                            nextMainTab.Dispatcher.BeginInvoke((Action)(() => nextMainTab.IsSelected = true));
                                        }

                                    }

                                }
                                else
                                {
                                    if (indexOfSubTab > (numberOfItemsInSubTab - 1))
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        indexOfSubTab++;
                                        it.Dispatcher.BeginInvoke((Action)(() => it.IsEnabled = true));
                                        TabItem nextTab = tabC.Items[indexOfSubTab] as TabItem;
                                        nextTab.Dispatcher.BeginInvoke((Action)(() => nextTab.IsEnabled = true));
                                        nextTab.Dispatcher.BeginInvoke((Action)(() => nextTab.IsSelected = true)); 
                                    }                                       
                                    
                                }
                                                           
                            }
                            else
                            {
                                TabItem nextMainTab = mainTab.Items[indexOfMainTab+1] as TabItem;
                                nextMainTab.Dispatcher.BeginInvoke((Action)(() => nextMainTab.IsEnabled = true));
                                nextMainTab.Dispatcher.BeginInvoke((Action)(() => nextMainTab.IsSelected = true));
                            }
                            
                            //s'il est superieur
                         
                        }
                    }
                }
            }
        }
        public void disabledTabControls()
        {
            foreach (TabItem item in mainTab.Items)
            {
                //Cherche les tab non selectionnes
                if (item.IsSelected == false)
                {
                    item.Dispatcher.BeginInvoke((Action)(() => item.IsEnabled = false));
                }
                TabItem itemIn = item;
                Grid grid = itemIn.Content as Grid;
                if (grid != null)
                {
                    TabControl tabC = grid.Children.Cast<UIElement>().FirstOrDefault() as TabControl;
                    if (tabC != null)
                    {
                        foreach (TabItem it in tabC.Items)
                        {
                            if (it.IsSelected == false)
                            {
                                it.Dispatcher.BeginInvoke((Action)(() => it.IsEnabled = false));
                            }
                        }
                    }
                }
            }
        }

        public DetailsRapportModel getDomainesAndSousDomainesInTabName(string tabName)
        {
            DetailsRapportModel rpt = new DetailsRapportModel();
            if (tabName != null)
            {
                string[] concat = tabName.Split('_');
                if (concat.Count() == 3)
                {
                    rpt.Domaine = Convert.ToInt32(concat[1]);
                    rpt.SousDomaine = Convert.ToInt32(concat[2]);
                }
            }
            return rpt;
        }
    }
}
