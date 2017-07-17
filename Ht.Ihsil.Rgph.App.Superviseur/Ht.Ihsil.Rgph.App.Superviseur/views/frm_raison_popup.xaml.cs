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
    /// Logique d'interaction pour frm_raison_popup.xaml
    /// </summary>
    public partial class frm_raison_popup : Window
    {
        public string Raison { get; set; }
        public bool IsValidate { get; set; }
        public frm_raison_popup()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void btn_valider_Click(object sender, RoutedEventArgs e)
        {
            if (txtRaison.Text != null)
            {
                Raison = txtRaison.Text;
                IsValidate = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Fok ou di poukisa.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
