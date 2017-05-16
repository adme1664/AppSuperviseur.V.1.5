using Ht.Ihsil.Rgph.App.Superviseur.Models;
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
        MdfService mdf_service;
        SdeModel sde;
        public frm_rapports()
        {
            InitializeComponent();
            mdf_service = new MdfService();
            List<SdeModel> listOfSde = mdf_service.getAllSde().ToList();
            DataContext = this;
            lbSdes.ItemsSource = listOfSde;
        }

        private void lbSdes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBox ltb = e.OriginalSource as ListBox;
                sde = ltb.SelectedItems.OfType<SdeModel>().FirstOrDefault();
                dtg_rapports.ItemsSource = Utilities.getRapportTroncCommun(sde);
                List<RapportModel> rapports = Utilities.getRrtPerformanceDemographique(sde);
                dtg_indPerDemographique.ItemsSource = rapports;
            }
            catch (Exception)
            {
                List<RapportModel> rapports = new List<RapportModel>();
                dtg_rapports.ItemsSource = rapports;
            }
        }

    }
}
