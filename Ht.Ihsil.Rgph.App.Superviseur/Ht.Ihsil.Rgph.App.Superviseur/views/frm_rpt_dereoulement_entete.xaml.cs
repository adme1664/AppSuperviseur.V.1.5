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
    /// Logique d'interaction pour frm_rpt_dereoulement_entete.xaml
    /// </summary>
    public partial class frm_rpt_dereoulement_entete : UserControl
    {
        IConfigurationService configuration = null;
        IContreEnqueteService service_ce = null;
        SdeModel sde = null;
        public frm_rpt_dereoulement_entete()
        {
            InitializeComponent();
            service_ce = new ContreEnqueteService(Users.users.SupDatabasePath);
            configuration = new ConfigurationService();
            SdeModel sde = configuration.searchAllSdes().First();
            List<SdeModel> sdes = new List<SdeModel>();
            sdes.Add(sde);
            lbCodeDistrict.ItemsSource = sdes;
        }

        private void btn_add_report_Click(object sender, RoutedEventArgs e)
        {
            if(sde.CodeDistrict!=null)
            {
                 RapportDeroulementModel rptDeroulement = new RapportDeroulementModel();
                 rptDeroulement.CodeDistrict = sde.CodeDistrict;
                 frm_rpt_deroulement frm = new frm_rpt_deroulement(rptDeroulement, this);
                 Utilities.showControl(frm, grd_details);
            }
            else
            {
                MessageBox.Show("Vous devez selectionner le code du district", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void lbCodeDistrict_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox ltb = e.OriginalSource as ListBox;
            sde = new SdeModel();
            sde = ltb.SelectedItems.OfType<SdeModel>().FirstOrDefault();
            List<RapportDeroulementModel> listOf = new List<RapportDeroulementModel>();
            listOf = service_ce.searchRptDeroulment();
            if (listOf.Count() == 0)
            {
                RapportDeroulementModel rpt = new RapportDeroulementModel();
                rpt.RapportName = "Nouveau";
                rpt.CodeDistrict = sde.CodeDistrict;
                listOf.Add(rpt);
                lbRprts.ItemsSource = listOf;
                btn_add_report.IsEnabled = true;
            }
            else
            {
                btn_add_report.IsEnabled = true;
                lbRprts.ItemsSource = listOf;
            }
        }

        private void lbRprts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBox ltb = sender as ListBox;
                RapportDeroulementModel rpt = ltb.SelectedItems.OfType<RapportDeroulementModel>().FirstOrDefault();
                if (rpt != null)
                {
                    if (rpt.RapportName == "Nouveau")
                    {
                        frm_rpt_deroulement frm = new frm_rpt_deroulement(rpt, this);
                        Utilities.showControl(frm, grd_details);
                    }
                    else
                    {
                        frm_rapport_deroulement frm = new frm_rapport_deroulement(rpt,null);
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
