using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using Ht.Ihsil.Rgph.App.Superviseur.viewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour frm_view_verification.xaml
    /// </summary>
    public partial class frm_view_verification : UserControl
    {
        MdfService mdfService = null;
        ObservableCollection<SdeModel> Sdes = null;
        Logger log;
        SdeModel sde;
       
        #region PROPERTIES
        #endregion

        public frm_view_verification()
        {
            InitializeComponent();
            log = new Logger();
            Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
            ObservableCollection<SdeModel> Sdes = new ObservableCollection<SdeModel>();
            mdfService = new MdfService();
            SdeModel[] arrayOfSdes = mdfService.getAllSde();
            foreach(SdeModel sde in arrayOfSdes)
            {
                Sdes.Add(sde);
            }
            listBox_sde.ItemsSource = Sdes;
        }

        private void listBox_sde_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                wInd.Dispatcher.BeginInvoke((Action)(() => wInd.DeferedVisibility = true));
                ListBox ltb = e.OriginalSource as ListBox;
                SdeModel chooseSde = ltb.SelectedItems.OfType<SdeModel>().FirstOrDefault();
                sde = chooseSde;
                txt_title.Text = "VERIFICATION-SDE:" + sde.SdeId;
                frm_verification viewVerification = new frm_verification(sde);
                Utilities.showControl(viewVerification, grd_details);
                wInd.Dispatcher.BeginInvoke((Action)(() => wInd.DeferedVisibility = false));
            }
            catch (Exception ex)
            {
                log.Info("Erreur/frm_view_verification:" + ex.Message);
            }
        }

        private void chkDistrict_Checked(object sender, RoutedEventArgs e)
        {
            wInd.Dispatcher.BeginInvoke((Action)(() => wInd.DeferedVisibility = true));
            frm_verification viewVerification = new frm_verification();
            Utilities.showControl(viewVerification, grd_details);
            wInd.Dispatcher.BeginInvoke((Action)(() => wInd.DeferedVisibility = false));
        }
    }
}
