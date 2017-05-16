using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ht.Ihsil.Rgph.App.Superviseur.views
{
    /// <summary>
    /// Logique d'interaction pour frm_rapport_deroulement.xaml
    /// </summary>
    public partial class frm_rapport_deroulement : UserControl
    {
        public ObservableCollection<DetailsRapportDeroulement> Rapports;
        public ObservableCollection<KeyValue> ListOfSousDomaine { get; set; }
        public ObservableCollection<KeyValue> ListOfSolutions { get; set; }
        public ObservableCollection<KeyValue> ListOfSuivi { get; set; }
        public ObservableCollection<KeyValue> ListOfDomaine { get; set; }
        public ObservableCollection<KeyValue> ListOfProbleme { get; set; }
        RapportDeroulementModel rapport;
        DetailsRapportDeroulement dtModel;
        Logger log;
        XmlUtils xmlReader;
        IContreEnqueteService service = null;
        private string codeDomaine = null;
        public frm_rapport_deroulement()
        {
            InitializeComponent();
            log = new Logger();
            xmlReader = new XmlUtils(AppDomain.CurrentDomain.BaseDirectory + @"App_data\rapports.xml");
            ListOfDomaine = new ObservableCollection<KeyValue>();
            Rapports = new ObservableCollection<DetailsRapportDeroulement>();
            ListOfSousDomaine = new ObservableCollection<KeyValue>();
            ListOfProbleme = new ObservableCollection<KeyValue>();
            ListOfSolutions = new ObservableCollection<KeyValue>();
            ListOfSuivi = new ObservableCollection<KeyValue>();
            foreach (KeyValue key in xmlReader.listOfDomaines())
            {
                ListOfDomaine.Add(key);
            }
            service = new ContreEnqueteService(Users.users.SupDatabasePath);
            cmbDomaine.ItemsSource = ListOfDomaine;
        }
        public frm_rapport_deroulement(RapportDeroulementModel rpt)
        {
            InitializeComponent();
            log = new Logger();
            xmlReader = new XmlUtils(AppDomain.CurrentDomain.BaseDirectory + @"App_data\rapports.xml");
            ListOfDomaine = new ObservableCollection<KeyValue>();
            Rapports = new ObservableCollection<DetailsRapportDeroulement>();
            ListOfSousDomaine = new ObservableCollection<KeyValue>();
            ListOfProbleme = new ObservableCollection<KeyValue>();
            ListOfSolutions = new ObservableCollection<KeyValue>();
            ListOfSuivi = new ObservableCollection<KeyValue>();
            service = new ContreEnqueteService(Users.users.SupDatabasePath);

            foreach (KeyValue key in xmlReader.listOfDomaines())
            {
                ListOfDomaine.Add(key);
            }
            cmbDomaine.ItemsSource = ListOfDomaine;
            rapport = new RapportDeroulementModel();
            rapport = rpt;
            if (rapport.RapportId != 0)
            {
                List<DetailsRapportModel> details = service.searchDetailsReport(rapport);
                int i = 0;
                foreach (DetailsRapportModel dt in details)
                {
                    i++;
                    DetailsRapportDeroulement row = new DetailsRapportDeroulement();
                    row.Commentaire = dt.Commentaire;
                    row.Domaine = xmlReader.getDomaine(Convert.ToInt32(dt.Domaine));
                    row.SousDomaine = xmlReader.getSousDomaine(Convert.ToInt32(dt.Domaine), Convert.ToInt32(dt.SousDomaine));
                    row.Solution = xmlReader.getSolution(Convert.ToInt32(dt.Domaine), Convert.ToInt32(dt.SousDomaine), Convert.ToInt32(dt.Solution));
                    row.Probleme = xmlReader.getProbleme(Convert.ToInt32(dt.Domaine), Convert.ToInt32(dt.SousDomaine), Convert.ToInt32(dt.Probleme));
                    row.Precisions = dt.Precisions;
                    row.Suggestions = dt.Suggestions;
                    row.Suivi = xmlReader.getSuivi(Convert.ToInt32(dt.Suivi));
                    row.RapportId = dt.RapportId;
                    row.DetailsRapportId = dt.DetailsRapportId;
                    row.Num = i;
                    Rapports.Add(row);
                }
                grid_rapport.ItemsSource = Rapports;
                //Changer le libelle sur le bouton sauvegarder en modifier
                if (dtModel != null)
                {
                    btnSauvegarder.Content = "Modifier";
                }
                else
                {
                    btnSauvegarder.Content = "Sauvegarder";
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (btnSauvegarder.Content.ToString() =="Sauvegarder")
            {
                rapport.DateRapport = DateTime.Now.ToString();
                long rapportId = service.saveRptDeroulement(rapport);
                if (rapportId != 0)
                {
                    for (int i = 0; i < grid_rapport.VisibleRowCount; i++)
                    {
                        DetailsRapportDeroulement row = (DetailsRapportDeroulement)grid_rapport.GetRow(i);
                        DetailsRapportModel modelForSaving = new DetailsRapportModel();
                        modelForSaving = ModelMapper.MapToDetailsRapportModel(row);
                        modelForSaving.RapportId = rapportId;
                        bool result = service.saveDetailsDeroulement(modelForSaving);
                        log.Info("Enregistrer:" + result);
                    }
                }
            }
            else
            {
                List<DetailsRapportModel> listOfDetails = service.searchDetailsReport(rapport);
                if (listOfDetails != null)
                {
                    foreach (DetailsRapportModel dt in listOfDetails)
                    {
                        for (int i = 0; i < grid_rapport.VisibleRowCount; i++)
                        {
                            DetailsRapportDeroulement row = (DetailsRapportDeroulement)grid_rapport.GetRow(i);
                            DetailsRapportModel modelForUpdating = new DetailsRapportModel();
                            modelForUpdating = ModelMapper.MapToDetailsRapportModel(row);
                            modelForUpdating.RapportId = rapport.RapportId;
                            if (modelForUpdating.DetailsRapportId == dt.DetailsRapportId && modelForUpdating.RapportId == dt.RapportId)
                            {
                                dt.Commentaire = modelForUpdating.Commentaire;
                                dt.Domaine = modelForUpdating.Domaine;
                                dt.Solution = modelForUpdating.Solution;
                                dt.SousDomaine = modelForUpdating.SousDomaine;
                                dt.Suggestions = modelForUpdating.Suggestions;
                                dt.Suivi = modelForUpdating.Suivi;
                                dt.Precisions = modelForUpdating.Precisions;
                                dt.Probleme = modelForUpdating.Probleme;
                                bool result = service.updateDetailsDeroulement(dt);
                                log.Info("Result updating============================<>" + result);
                            }
                        }
                    }
                }
            }

        }

        public ObservableCollection<KeyValue> getListOfDomaines()
        {
            ObservableCollection<KeyValue> listOf = new ObservableCollection<KeyValue>();
            List<KeyValue> listOfDomaines = xmlReader.listOfDomaines();
            foreach (KeyValue key in listOfDomaines)
            {
                listOf.Add(key);
            }
            return listOf;
        }
        private void cmbDomaine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            KeyValue domaine = (KeyValue)cmb.SelectedItem;
            codeDomaine = domaine.Key.ToString();
            ListOfSousDomaine.Clear();
            foreach (KeyValue key in xmlReader.listOfSousDomaines(codeDomaine))
            {
                ListOfSousDomaine.Add(key);
            }
            cmbSousDomaine.ItemsSource = ListOfSousDomaine;
            if (dtModel != null)
            {
                int i = 0;
                foreach (KeyValue key in cmbSousDomaine.Items)
                {
                    if (key.Key == dtModel.SousDomaine.Key)
                    {
                        cmbSousDomaine.Dispatcher.BeginInvoke((Action)(() => cmbSousDomaine.SelectedItem = cmbSousDomaine.Items[i]));
                        break;
                    }
                    i++;
                }
            }
        }

        private void cmbSousDomaine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            KeyValue sousDomaine = (KeyValue)cmb.SelectedItem;
            if (sousDomaine == null)
                return;
            else
            {
                //Selectionner le Probleme
                ListOfProbleme.Clear();
                foreach (KeyValue key in xmlReader.listOfProblemes(codeDomaine, sousDomaine.Key.ToString()))
                {
                    ListOfProbleme.Add(key);
                }
                if (cmbIntervention.Items.Count == 0)
                {
                    cmbIntervention.ItemsSource = ListOfProbleme;
                }
                //Selectionner la solution
                ListOfSolutions.Clear();
                foreach (KeyValue key in xmlReader.listOfSolutions(codeDomaine, sousDomaine.Key.ToString()))
                {
                    ListOfSolutions.Add(key);
                }
                cmbSolution.ItemsSource = ListOfSolutions;
                if (dtModel != null)
                {
                    int l = 0;
                    foreach (KeyValue key in cmbIntervention.Items)
                    {
                        if (key.Key == dtModel.Solution.Key)
                        {
                            cmbSolution.Dispatcher.BeginInvoke((Action)(() => cmbSolution.SelectedItem = cmbSolution.Items[l]));
                            break;
                        }
                        l++;
                    }
                }

                //Selectionner la valeur enregistree dans la base pour l'intervention concernee
                if (dtModel != null)
                {
                    int j = 0;
                    foreach (KeyValue key in cmbIntervention.Items)
                    {
                        if (key.Key == dtModel.Probleme.Key)
                        {
                            cmbIntervention.Dispatcher.BeginInvoke((Action)(() => cmbIntervention.SelectedItem = cmbIntervention.Items[j]));
                            break;
                        }
                        j++;
                    }
                }
                //
                //Selectionne le suivi enregistree
                ListOfSuivi.Clear();
                foreach (KeyValue key in xmlReader.listOfSuivi())
                {
                    ListOfSuivi.Add(key);
                }
                cmbSuivi.ItemsSource = ListOfSuivi;
                if (dtModel != null)
                {
                    int k = 0;
                    foreach (KeyValue key in cmbSuivi.Items)
                    {
                        if (key.Key == dtModel.Suivi.Key)
                        {
                            cmbSuivi.Dispatcher.BeginInvoke((Action)(() => cmbSuivi.SelectedItem = cmbSuivi.Items[k]));
                            break;
                        }
                        k++;
                    }
                }

            }
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            KeyValue domaine = new KeyValue();
            KeyValue sousDomaine = new KeyValue();
            KeyValue probleme = new KeyValue();
            KeyValue solution = new KeyValue();
            KeyValue suivi = new KeyValue();
            domaine = (KeyValue)cmbDomaine.SelectedItem;
            sousDomaine = (KeyValue)cmbSousDomaine.SelectedItem;
            probleme = (KeyValue)cmbIntervention.SelectedItem;
            solution = (KeyValue)cmbSolution.SelectedItem;
            suivi = (KeyValue)cmbSuivi.SelectedItem;

            if (domaine.Key != 0 && sousDomaine.Key != 0
                && probleme.Key != 0 && solution.Key != 0
                && suivi.Key != 0)
            {
                int i = Rapports.Count;
                DetailsRapportDeroulement rprt = new DetailsRapportDeroulement(i + 1, domaine, sousDomaine, probleme, solution, txtPrecision.Text, txtSuggestion.Text, suivi, "Aucun");
                if (dtModel != null)
                {
                    rprt.RapportId = dtModel.RapportId;
                    rprt.DetailsRapportId = dtModel.DetailsRapportId;
                    foreach (DetailsRapportDeroulement dt in Rapports)
                    {
                        if (dt.DetailsRapportId == rprt.DetailsRapportId && dt.RapportId == rprt.RapportId)
                        {
                            Rapports.Remove(dt);
                            Rapports.Add(rprt);
                            grid_rapport.ItemsSource = Rapports;
                            ListOfDomaine = new ObservableCollection<KeyValue>();
                            ListOfDomaine = getListOfDomaines();
                            ListOfSousDomaine = new ObservableCollection<KeyValue>();
                            ListOfProbleme = new ObservableCollection<KeyValue>();
                            ListOfSolutions = new ObservableCollection<KeyValue>();
                            ListOfSuivi = new ObservableCollection<KeyValue>();
                            txtPrecision.Text = "";
                            txtSuggestion.Text = "";
                            break;
                        }
                    }
                }
                else
                {
                    Rapports.Add(rprt);
                    grid_rapport.ItemsSource = Rapports;
                    ListOfDomaine = new ObservableCollection<KeyValue>();
                    ListOfDomaine = getListOfDomaines();
                    ListOfSousDomaine = new ObservableCollection<KeyValue>();
                    ListOfProbleme = new ObservableCollection<KeyValue>();
                    ListOfSolutions = new ObservableCollection<KeyValue>();
                    ListOfSuivi = new ObservableCollection<KeyValue>();
                    txtPrecision.Text = "";
                    txtSuggestion.Text = "";
                }
                
            }
        }

        private void TableView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.NewRow != null)
            {
                DetailsRapportDeroulement rpt = (DetailsRapportDeroulement)e.NewRow;
                dtModel = new DetailsRapportDeroulement();
                dtModel = rpt;
                int i = 0;
                txtPrecision.Text = rpt.Precisions;
                txtSuggestion.Text = rpt.Suggestions;
                foreach (KeyValue key in cmbDomaine.Items)
                {
                    if (key.Key == rpt.Domaine.Key)
                    {
                        cmbDomaine.Dispatcher.BeginInvoke((Action)(() => cmbDomaine.SelectedItem = cmbDomaine.Items[i]));
                        break;
                    }
                    i++;
                }
           }
        }

        private void grid_rapport_AutoGeneratingColumn(object sender, AutoGeneratingColumnEventArgs e)
        {
            if (e.Column.FieldName == "DetailsRapportId")
                e.Column.Visible = false;
            if (e.Column.FieldName == "RapportId")
                e.Column.Visible = false;
        }

    }
}
