using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ht.Ihsil.Rgph.App.Superviseur.views
{
    /// <summary>
    /// Logique d'interaction pour frm_pop_verified_validate.xaml
    /// </summary>
    public partial class frm_pop_verified_validate : Window
    {
        private static string MAIN_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Data\Databases\";
        SqliteDataReaderService service;
        ISqliteDataWriter sw;
        ThreadStart ths = null;
        Thread t = null;
        string sdeId;
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        public frm_pop_verified_validate(String sdeId)
        {
            InitializeComponent();
            service = new SqliteDataReaderService(Utilities.getConnectionString(MAIN_DATABASE_PATH, sdeId));
            sw = new SqliteDataWriter();
            this.sdeId = sdeId;
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            if (btn_start.Content.ToString() == "Verifier")
            {
                if (t != null)
                {
                    if (t.IsAlive)
                    {
                        t.Abort();
                    }
                }
                else
                {
                    ths = new ThreadStart(() => verifier());
                    t = new Thread(ths);
                    t.Start();
                }
                prgb_verifie.Dispatcher.BeginInvoke((Action)(() => prgb_verifie.Value = 0));
                img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Visible));
                img_finish.Dispatcher.BeginInvoke((Action)(() => img_finish.Visibility = Visibility.Hidden));
                btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.IsEnabled = false));
            }
            else
            {
                //Sinon on ferme la fenetre
                this.Close();
            }
        }

        public void verifier()
        {
            int nbre = 0;
            //Recherche les batiments non verifies
            List<BatimentModel> batiments = service.Sr.GetAllBatimentModelTermine();
            float pourcentage = calculPercent(batiments.Count);
            foreach (BatimentModel batiment in batiments)
            {
                //Verifier si le batiment est deja verifie
                if (batiment.IsVerified != (int)Constant.StatutVerifie.Verifie)
                {
                    //Changement de statut du batiment
                    bool result = sw.changeToVerified<BatimentModel>(batiment, sdeId,Users.users.DatabasePath);
                    prgb_verifie.Dispatcher.BeginInvoke((Action)(() => prgb_verifie.Value += pourcentage));
                    nbre++;
                }              
            }
            if (nbre == batiments.Count)
            {
                MessageBox.Show("Verification effectuee", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.IsEnabled = true));
                btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.Content = "Fermer"));
                btn_annuler.Dispatcher.BeginInvoke((Action)(() => btn_annuler.IsEnabled = true));
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        private void btn_annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public float calculPercent(int nbre)
        {
            return ((float)100) / ((float)nbre);
        }

    }
}
