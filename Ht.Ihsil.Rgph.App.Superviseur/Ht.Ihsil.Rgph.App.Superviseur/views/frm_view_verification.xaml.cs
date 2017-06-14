using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using Ht.Ihsil.Rgph.App.Superviseur.viewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        #region DECLARATIONS
        MdfService mdfService = null;
        ObservableCollection<SdeModel> Sdes = null;
        Logger log;
        SdeModel sde;
        ThreadStart ths = null;
        Thread t = null;
        #endregion

        #region PROPERTIES
        #endregion

        #region CONSTRUCTORS
        public frm_view_verification()
        {
            InitializeComponent();
            log = new Logger();
            Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
            ObservableCollection<SdeModel> Sdes = new ObservableCollection<SdeModel>();
            mdfService = new MdfService();
            SdeModel[] arrayOfSdes = mdfService.getAllSde();
            try
            {
                foreach (SdeModel sde in arrayOfSdes)
                {
                    Sdes.Add(sde);
                }
                listBox_sde.ItemsSource = Sdes;
            }
            catch (Exception)
            {

            }
            
        }
        #endregion

        #region CONTROL EVENTS
        private void listBox_sde_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = true));
                ListBox ltb = e.OriginalSource as ListBox;
                if (chkDistrict.IsChecked == true)
                {
                    chkDistrict.IsChecked = false;
                }
                SdeModel chooseSde = ltb.SelectedItems.OfType<SdeModel>().FirstOrDefault();
                sde = chooseSde;
                if(sde!=null)
                txt_title.Dispatcher.BeginInvoke((Action)(() => txt_title.Text = "VERIFICATION-SDE:" + sde.SdeId));
                frm_verification viewVerification = new frm_verification(sde);
                Utilities.showControl(viewVerification, grd_details);
                decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = false));
             }
            catch (Exception ex)
            {
                log.Info("Erreur/frm_view_verification:" + ex.Message);
                decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = false));
            }
        }


        private void chkDistrict_Checked(object sender, RoutedEventArgs e)
        {
            //Load le splashloading
            decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = true));
            //Change le text displaying pour indiquer la selection
            txt_title.Dispatcher.BeginInvoke((Action)(() => txt_title.Text = "VERIFICATION-DISTRICT"));
            //Deselectionner un element de la listbox si il etait deja selectionne
            listBox_sde.Dispatcher.BeginInvoke((Action)(() => listBox_sde.UnselectAll()));
            //Creation de la fenetre et ajout dans la grille
            frm_verification viewVerification = new frm_verification(true);
            Utilities.showControl(viewVerification, grd_details);
            //
            decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = false));
        }
        #endregion
    }
}
