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
    /// Logique d'interaction pour frm_rpt_personnel.xaml
    /// </summary>
    public partial class frm_rpt_personnel : UserControl
    {
        IConfigurationService configuration = null;
        IContreEnqueteService service_ce = null;
        AgentModel agent;
        public frm_rpt_personnel()
        {
            InitializeComponent();
            configuration = new ConfigurationService();
            DataContext = this;
            lbAgents.ItemsSource = configuration.searchAllAgents();
            service_ce = new ContreEnqueteService(Users.users.SupDatabasePath);
            agent = new AgentModel();
        }
        
        private void lbAgents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lbRprts.ItemsSource = new List<RapportPersonnelModel>();
                ListBox ltb = e.OriginalSource as ListBox;
                this.agent = ltb.SelectedItems.OfType<AgentModel>().FirstOrDefault();
                string sde = configuration.getSdeByAgent(agent.AgentId).SdeId;
                List<RapportPersonnelModel> listOfRpt = new List<RapportPersonnelModel>();
                listOfRpt = service_ce.searchRptPersonnelByAgent(Convert.ToInt32(agent.Username));
                if (listOfRpt.Count == 0)
                {
                    RapportPersonnelModel rpt = new RapportPersonnelModel();
                    rpt.RapportName = "Nouveau";
                    listOfRpt.Add(rpt);
                    lbRprts.ItemsSource = listOfRpt;
                }
                else
                {
                    lbRprts.ItemsSource = listOfRpt;
                }        
            }
            catch (Exception)
            {

            }
        }
        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            
            frm_questions_agent frm=new frm_questions_agent(this.agent);
            Utilities.showControl(frm, grd_details);
        }

        private void lbRprts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBox ltb = e.OriginalSource as ListBox;
                RapportPersonnelModel rpt = ltb.SelectedItems.OfType<RapportPersonnelModel>().FirstOrDefault();
                if (rpt == null)
                {
                    frm_questions_agent frm = new frm_questions_agent(this.agent);
                    Utilities.showControl(frm, grd_details);
                }
                else
                {
                    if (rpt.RapportName == "Nouveau")
                    {
                        frm_questions_agent frm = new frm_questions_agent(this.agent);
                        Utilities.showControl(frm, grd_details);
                    }
                    else
                    {
                        frm_questions_agent frm = new frm_questions_agent(this.agent, rpt);
                        Utilities.showControl(frm, grd_details);
                    }
                } 
            }
            catch (Exception)
            {

            }
        }
    }
}
