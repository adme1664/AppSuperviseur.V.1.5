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
    /// Logique d'interaction pour frm_rapports.xaml
    /// </summary>
    public partial class frm_rapports : UserControl
    {
        ConfigurationService service = null;
        SdeModel sde;
        Logger log;
        public frm_rapports()
        {
            InitializeComponent();
            service = new ConfigurationService();
            log = new Logger();
            List<SdeModel> listOfSde = new List<SdeModel>();
            try
            {
                listOfSde = service.searchAllSdes();
                DataContext = this;

            }
            catch (Exception)
            {
            }
            lbSdes.ItemsSource = listOfSde;

        }

        private void lbSdes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBox ltb = e.OriginalSource as ListBox;
                sde = ltb.SelectedItems.OfType<SdeModel>().FirstOrDefault();
                lbl_localisation.Text=Utilities.getGeoInformation(sde.SdeId);
                AgentModel agent=service.findAgentSderById(sde.AgentId);
                lbl_agent.Text = agent.Nom + " " + agent.Prenom;
                dtg_rapports.ItemsSource = Utilities.getRapportTroncCommun(sde);
                //List<RapportModel> rapports = Utilities.getRrtPerformanceDemographique(sde);
                //dtg_indPerDemographique.ItemsSource = rapports;
            }
            catch (Exception ex)
            {
                log.Info("Erreur while generating the report:" + ex.Message);
                List<RapportModel> rapports = new List<RapportModel>();
                dtg_rapports.ItemsSource = rapports;
            }
        }

    }
}
