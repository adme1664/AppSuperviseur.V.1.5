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
using System.Windows.Shapes;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System.Windows.Threading;
using System.Threading;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Configuration;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsi.Rgph.Utility.Utils;
using Ht.Ihsi.Rgph.Logging.Logs;

namespace Ht.Ihsil.Rgph.App.Superviseur.views
{
    /// <summary>
    /// Logique d'interaction pour frm_connexion.xaml
    /// </summary>
    public partial class frm_connexion : Window
    {
        private IUtilisateurService service;
        private bool state = false;
        private static string MAIN_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Data\Databases\";

        Logger log;
        public frm_connexion()
        {
            InitializeComponent();
            Users.users = new Users();
            Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
            service = new UtilisateurService();
            log = new Logger();
        }

        private void btn_connexion_Click(object sender, RoutedEventArgs e)
        {


            try
            {

                initializeConnexion(t_username.Text, t_password.Password);
                if (state == true)
                {
                    MainWindow1 m = new MainWindow1();
                    this.Close();
                    m.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                log.Info("Error:" + ex.Message);
            }
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void btn_annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            App.Current.Shutdown();
        }

        public void writeInBusyTool(string message)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
            {
                busyIndicator.BusyContent = message;

            }, null);
        }

        //Pinger le serveur

        public bool pingTheServer(string adrIp)
        {
            try
            {
                var ping = new Ping();
                var reply = ping.Send(adrIp);
                return reply != null && reply.Status == IPStatus.Success;
            }
            catch (Exception)
            {

            }
            return false;
        }
        public bool initializeConnexion(string username, string password)
        {
            bool pingStatus = true;
            Dispatcher.Invoke(new Action(() =>
            {
                busyIndicator.IsBusy = true;
                busyIndicator.BusyContent = "Tentative de connexion...";
                img_loading.Visibility = Visibility.Visible;
            }
            ));

            if (username.Length == 0 || password.Length == 0)
            {
                busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                MessageBox.Show("Veuillez saisir un nom utilisateur et un mot de passe", "ERREUR/IHSI", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UtilisateurModel user = null;
                busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "Connexion avec la base de donnéees..."));
                if (service.isSuperviseurAccountExist() == true)
                {
                    try
                    {
                        string[] tab = username.Split('.');
                        username = tab[0] + "" + tab[1];
                        user = service.authenticateUserLocally(username, password);
                        img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));

                    }
                    catch (Exception ex)
                    {
                        log.Info("Error:" + ex.Message);
                        img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                    }
                }
                else
                {

                    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "Application non encore configuree. Connexion avec le serveur..."));
                    pingStatus = pingTheServer(ConfigurationManager.AppSettings.Get("adrIpServer"));
                    if (pingStatus == false)
                    {
                        MessageBox.Show("Serveur indisponible", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }

                    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "Connexion avec le serveur en cours..."));
                    try
                    {
                        user = service.authenticateUserRemotely(username, password);
                    }
                    catch (Exception ex)
                    {

                    }

                    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "Connexion reussie"));
                }
                if (Utils.IsNotNull(user))
                {
                    Users.users.CodeUtilisateur = user.CodeUtilisateur;
                    Users.users.Prenom = user.Prenom;
                    Users.users.Nom = user.Nom;
                    Users.users.Profile = "" + user.ProfileId;
                    state = true;
                    Users.users.DatabasePath = MAIN_DATABASE_PATH;
                    //Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory;
                }
                else
                {
                    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "Erreur de connexion."));
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                    {
                        busyIndicator.IsBusy = false;
                        lbl_error.Content = "Erreur- Nom Utilisateur ou mot de passe errone.";
                        lbl_error.Visibility = Visibility.Visible;
                        img_loading.Visibility = Visibility.Hidden;
                    }, null);
                }
            }

            return state;
        }

        private void chk_isAstic_Checked(object sender, RoutedEventArgs e)
        {
            if (chk_isAstic.IsChecked == true)
            {
                t_username.Mask = "006.0000";
            }
            else
            {
                t_username.Mask = "007.0000";
            }
        }

        private void t_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {

                    initializeConnexion(t_username.Text, t_password.Password);
                    if (state == true)
                    {
                        MainWindow1 m = new MainWindow1();
                        this.Close();
                        m.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    log.Info("Error:" + ex.Message);
                }
            }
        }


    }
}
