using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.utils;


namespace Ht.Ihsil.Rgph.App.Superviseur.views.Contre_Enquete
{
    /// <summary>
    /// Interaction logic for AddIndividu.xaml
    /// </summary>
    public partial class AddIndividu : DXWindow
    {
        IndividuCEModel individu = null;
        ContreEnqueteService service = null;
        Logger log = null;
        ReponseModel reponse = null;
        List<ReponseModel> listOfReponse = null;
        public AddIndividu(int numeroIndividu)
        {
            InitializeComponent();
            grp.Header = "Enfomasyon sou Endividi " + numeroIndividu;
            service = new ContreEnqueteService();
            listOfReponse = new List<ReponseModel>();
            listOfReponse.Add(new ReponseModel("1", "Gason"));
            listOfReponse.Add(new ReponseModel("2", "Fi"));
            cmb_seks.ItemsSource = listOfReponse;
            log = new Logger();
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (txt_nom.Text == "")
            {
                MessageBox.Show("Ou dwe ekri non endividi a.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (txt_siyati.Text == "")
            {
                MessageBox.Show("Ou dwe ekri siyati endividi a.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }
    }
}
