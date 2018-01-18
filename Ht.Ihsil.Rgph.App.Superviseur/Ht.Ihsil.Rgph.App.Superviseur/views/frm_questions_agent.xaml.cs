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
    /// Logique d'interaction pour frm_questions_agent.xaml
    /// </summary>
    public partial class frm_questions_agent : UserControl
    {
        ContreEnqueteService service_ce;
        IConfigurationService configuration;
        AgentModel agent = null;
        RapportPersonnelModel rptModel = null;
        frm_rpt_personnel main_rpm = null;
        
        public frm_questions_agent(AgentModel agent,frm_rpt_personnel frm)
        {
            InitializeComponent();
            main_rpm = frm;
            q1.Text = Constant.q1;
            q2.Text = Constant.q2;
            q3.Text = Constant.q3;
            q4.Text = Constant.q4;
            q5.Text = Constant.q5;
            q6.Text = Constant.q6;
            q7.Text = Constant.q7;
            q8.Text = Constant.q8;
            q9.Text = Constant.q9;
            q10.Text = Constant.q10;
            q11.Text = Constant.q11;
            q12.Text = Constant.q12;
            q13.Text = Constant.q13;
            q14.Text = Constant.q14;
            q15.Text = Constant.q15;

            cQ1.ItemsSource = Constant.listOf4Choix();
            cQ2.ItemsSource = Constant.listOf4Choix();
            cQ3.ItemsSource = Constant.listOf4Choix();
            cQ4.ItemsSource = Constant.listOf4Choix();
            cQ5.ItemsSource = Constant.listOf4Choix();
            cQ6.ItemsSource = Constant.listOf4Choix();
            cQ7.ItemsSource = Constant.listOf4Choix();
            cQ8.ItemsSource = Constant.listOf3Choix();
            cQ9.ItemsSource = Constant.listOf4Choix();
            cQ10.ItemsSource = Constant.listOf4Choix();
            cQ11.ItemsSource = Constant.listOf3Choix();
            cQ12.ItemsSource = Constant.listOf3Choix();
            cQ13.ItemsSource = Constant.listOfChoixQ13();
            cQ15.ItemsSource = Constant.listOfChoixQ15();

            service_ce = new ContreEnqueteService(Users.users.SupDatabasePath);
            configuration = new ConfigurationService();
            this.agent = agent;
        }
        public frm_questions_agent(AgentModel agent,RapportPersonnelModel rpt)
        {
            InitializeComponent();
            configuration = new ConfigurationService();
            q1.Text = Constant.q1;
            q2.Text = Constant.q2;
            q3.Text = Constant.q3;
            q4.Text = Constant.q4;
            q5.Text = Constant.q5;
            q6.Text = Constant.q6;
            q7.Text = Constant.q7;
            q8.Text = Constant.q8;
            q9.Text = Constant.q9;
            q10.Text = Constant.q10;
            q11.Text = Constant.q11;
            q12.Text = Constant.q12;
            q13.Text = Constant.q13;
            q14.Text = Constant.q14;
            q15.Text = Constant.q15;

            cQ1.ItemsSource = Constant.listOf4Choix();
            cQ2.ItemsSource = Constant.listOf4Choix();
            cQ3.ItemsSource = Constant.listOf4Choix();
            cQ4.ItemsSource = Constant.listOf4Choix();
            cQ5.ItemsSource = Constant.listOf4Choix();
            cQ6.ItemsSource = Constant.listOf4Choix();
            cQ7.ItemsSource = Constant.listOf4Choix();
            cQ8.ItemsSource = Constant.listOf3Choix();
            cQ9.ItemsSource = Constant.listOf4Choix();
            cQ10.ItemsSource = Constant.listOf4Choix();
            cQ11.ItemsSource = Constant.listOf3Choix();
            cQ12.ItemsSource = Constant.listOf3Choix();
            cQ13.ItemsSource = Constant.listOfChoixQ13();
            cQ15.ItemsSource = Constant.listOfChoixQ15();
            
            rptModel = rpt;
            service_ce = new ContreEnqueteService();
            this.agent = agent;
            tQ14.Text = rptModel.q14;
            foreach (ReponseModel rapport in cQ1.Items)
            {
                if (rapport.CodeReponse == rpt.q1.ToString())
                    cQ1.SelectedItem = rapport;
            }
            foreach (ReponseModel rapport in cQ2.Items)
            {
                if (rapport.CodeReponse == rpt.q2.ToString())
                    cQ2.SelectedItem = rapport;
            }
            foreach (ReponseModel rapport in cQ3.Items)
            {
                if (rapport.CodeReponse == rpt.q3.ToString())
                    cQ3.SelectedItem = rapport;
            }
            foreach (ReponseModel rapport in cQ4.Items)
            {
                if (rapport.CodeReponse == rpt.q4.ToString())
                    cQ4.SelectedItem = rapport;
            }
            foreach (ReponseModel rapport in cQ5.Items)
            {
                if (rapport.CodeReponse == rpt.q5.ToString())
                    cQ5.SelectedItem = rapport;
            }
            foreach (ReponseModel rapport in cQ6.Items)
            {
                if (rapport.CodeReponse == rpt.q6.ToString())
                    cQ6.SelectedItem = rapport;
            }
            foreach (ReponseModel rapport in cQ7.Items)
            {
                if (rapport.CodeReponse == rpt.q7.ToString())
                    cQ7.SelectedItem = rapport;
            }
            foreach (ReponseModel rapport in cQ8.Items)
            {
                if (rapport.CodeReponse == rpt.q8.ToString())
                    cQ8.SelectedItem = rapport;
            }
            foreach (ReponseModel rapport in cQ9.Items)
            {
                if (rapport.CodeReponse == rpt.q9.ToString())
                    cQ9.SelectedItem = rapport;
            }
            foreach (ReponseModel rapport in cQ10.Items)
            {
                if (rapport.CodeReponse == rpt.q10.ToString())
                    cQ10.SelectedItem = rapport;
            }
            foreach (ReponseModel rapport in cQ11.Items)
            {
                if (rapport.CodeReponse == rpt.q12.ToString())
                    cQ11.SelectedItem = rapport;
            }
            foreach (ReponseModel rapport in cQ12.Items)
            {
                if (rapport.CodeReponse == rpt.q12.ToString())
                    cQ12.SelectedItem = rapport;
            }
            foreach (ReponseModel rapport in cQ13.Items)
            {
                if (rapport.CodeReponse == rpt.q13.ToString())
                    cQ13.SelectedItem = rapport;
            }
            foreach (ReponseModel rapport in cQ15.Items)
            {
                if (rapport.CodeReponse == rpt.q5.ToString())
                    cQ15.SelectedItem = rapport;
            }
            img_save.Source = new BitmapImage(new Uri(@"/images/update.png", UriKind.Relative));
            btn_save.ToolTip = "Modifier";
           
        }
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RapportPersonnelModel rpt = new RapportPersonnelModel();
                if (rptModel != null)
                {
                    rpt = rptModel;
                }
                ReponseModel rep1 = cQ1.SelectedItem as ReponseModel;
                ReponseModel rep2 = cQ2.SelectedItem as ReponseModel;
                ReponseModel rep3 = cQ3.SelectedItem as ReponseModel;
                ReponseModel rep4 = cQ4.SelectedItem as ReponseModel;
                ReponseModel rep5 = cQ5.SelectedItem as ReponseModel;
                ReponseModel rep6 = cQ6.SelectedItem as ReponseModel;
                ReponseModel rep7 = cQ7.SelectedItem as ReponseModel;
                ReponseModel rep8 = cQ8.SelectedItem as ReponseModel;
                ReponseModel rep9 = cQ9.SelectedItem as ReponseModel;
                ReponseModel rep10 = cQ10.SelectedItem as ReponseModel;
                ReponseModel rep11 = cQ11.SelectedItem as ReponseModel;
                ReponseModel rep12 = cQ12.SelectedItem as ReponseModel;
                ReponseModel rep13 = cQ13.SelectedItem as ReponseModel;
                ReponseModel rep15 = cQ15.SelectedItem as ReponseModel;
                string q14 = tQ14.Text;
                if (rep1 == null || rep2 == null || rep3 == null || rep4 == null || rep5 == null || rep6 == null || rep7 == null || rep8 == null || rep9 == null || rep10 == null || rep11 == null || rep12 == null || rep13 == null || rep15 == null)
                {
                    MessageBox.Show("Ou dwe mete repons yo.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    rpt.q1 = Convert.ToInt32(rep1.CodeReponse);
                    rpt.q2 = Convert.ToInt32(rep2.CodeReponse);
                    rpt.q3 = Convert.ToInt32(rep3.CodeReponse);
                    rpt.q4 = Convert.ToInt32(rep4.CodeReponse);
                    rpt.q5 = Convert.ToInt32(rep5.CodeReponse);
                    rpt.q6 = Convert.ToInt32(rep6.CodeReponse);
                    rpt.q7 = Convert.ToInt32(rep7.CodeReponse);
                    rpt.q8 = Convert.ToInt32(rep8.CodeReponse);
                    rpt.q9 = Convert.ToInt32(rep9.CodeReponse);
                    rpt.q10 = Convert.ToInt32(rep10.CodeReponse);
                    rpt.q11 = Convert.ToInt32(rep11.CodeReponse);
                    rpt.q12 = Convert.ToInt32(rep12.CodeReponse);
                    rpt.q13 = Convert.ToInt32(rep13.CodeReponse);
                    rpt.q15 = Convert.ToInt32(rep15.CodeReponse);
                    rpt.q14 = q14;
                    SdeInformation sde = Utilities.getSdeInformation(Utilities.getSdeFormatWithDistrict(configuration.getSdeByAgent(agent.AgentId).SdeId));
                    rpt.comId = sde.ComId;
                    rpt.deptId = sde.DeptId;
                    rpt.codeDistrict = sde.CodeDistrict;
                    rpt.ReportSenderId = Users.users.CodeUtilisateur;
                    rpt.persId = agent.Username;
                }

                if (btn_save.ToolTip.ToString() == "Enregistrer")
                {
                    bool result = service_ce.saveRptPersonnel(rpt);
                    if (result == true)
                    {
                        MessageBox.Show("Rapport enregistre avec succes", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        main_rpm.lbAgents.ItemsSource = configuration.searchAllAgents();
                    }
                    else
                        MessageBox.Show("Erreur", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    bool result = service_ce.updateteRptPersonnel(rpt);
                    if (result == true)
                        MessageBox.Show("Rapport modifie avec succes", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Erreur", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }                             
            }
            catch (NullReferenceException)
            {

            }
            catch (Exception)
            {

            }
                        
        }

    }
}
