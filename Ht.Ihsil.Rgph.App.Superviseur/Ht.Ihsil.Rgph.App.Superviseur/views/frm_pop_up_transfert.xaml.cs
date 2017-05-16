using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
    /// Logique d'interaction pour frm_pop_up_transfert.xaml
    /// </summary>
    public partial class frm_pop_up_transfert : Window
    {
        private static string MAIN_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Data\Databases\";
        private static string TEMP_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Temp\";
        private static string BACKUP_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Backup\";
        private static string APP_DIRECTORY_PATH = AppDomain.CurrentDomain.BaseDirectory;
        private DeviceManager device;
        BackgroundWorker bckw;
        SqliteDataReaderService service;
        SqliteDataWriter sqliteWrite;
        ISqliteReader reader = null;
        IMdfService mdfService;
        Logger log;
        MainWindow1 main;
        string sdeId;
        SdeModel sdeCollecteData = null;
        SdeModel sdeCEData = null;
        TypeModel typeBatiment = null;
        ConfigurationService settings = null;
        ThreadStart ths = null;
        Thread t = null;
        bool copied = false;
        public frm_pop_up_transfert()
        {
            InitializeComponent();
            device = new DeviceManager();
            bckw = new BackgroundWorker();
            service = new SqliteDataReaderService();
            log = new Logger();
            mdfService = new MdfService();
            Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
        }

        public void pullFile()
        {

            string sdeId = null;
            Tbl_Materiels mat = null;
            try
            {
                if (device.IsConnected == true)
                {
                    DeviceManager dev = null;
                    DeviceInformation info = null;
                    dev = new DeviceManager();
                    info = dev.getDeviceInformation();
                    Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
                    settings = new ConfigurationService();

                    mat = settings.getMateriels(info.Serial);
                    if (mat != null)
                    {
                        sdeId = settings.getSdeByAgent(mat.AgentId.GetValueOrDefault()).SdeId;
                        prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 30));
                        lbl_trans.Dispatcher.BeginInvoke((Action)(() => lbl_trans.Content = "Transfert des fichiers... "));
                        if (!Directory.Exists(TEMP_DATABASE_PATH))
                        {
                            Directory.CreateDirectory(TEMP_DATABASE_PATH);
                        }
                        lbl_trans.Dispatcher.BeginInvoke((Action)(() => lbl_trans.Content = "Transfè a ap fèt... "));
                        copied = device.pullFile(TEMP_DATABASE_PATH);
                        Process[] procs = Process.GetProcessesByName("adb");
                        if (procs.Length != 0)
                        {
                            foreach (var proc in procs)
                            {
                                if (!proc.HasExited)
                                {
                                    proc.Kill();
                                }
                            }
                        }
                        prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 40));
                        if (copied == true)
                        {
                            string db_backup = BACKUP_DATABASE_PATH + @"\\" + sdeId + "\\";
                            if (!Directory.Exists(db_backup))
                            {
                                Directory.CreateDirectory(db_backup);
                            }
                            string[] files = Directory.GetFiles(TEMP_DATABASE_PATH);
                            foreach (string f in files)
                            {
                                string fileName = System.IO.Path.GetFileName(f);
                                string destFileName = System.IO.Path.Combine(db_backup, fileName + "_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".SQLITE");
                                System.IO.File.Copy(f, destFileName, true);
                                if (!Directory.Exists(MAIN_DATABASE_PATH))
                                {
                                    Directory.CreateDirectory(MAIN_DATABASE_PATH);
                                }
                                destFileName = System.IO.Path.Combine(MAIN_DATABASE_PATH, sdeId + ".SQLITE");
                                if (!System.IO.File.Exists(destFileName))
                                {
                                    System.IO.File.Move(f, destFileName);
                                }
                                else
                                {
                                    System.IO.File.Copy(f, destFileName, true);
                                }
                                break;
                            }
                            prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 90));
                            MessageBox.Show(Constant.MSG_TRANSFERT_TERMINE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                            reader = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));
                            Tbl_Sde sde = new Tbl_Sde();
                            sde = reader.getSdeDetailsFromSqliteFile();
                            sde.SdeId = sdeId;
                            sde.AgentId = mat.AgentId;
                            settings.updateSdeDetails(sde);
                            prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 100));
                            lbl_trans.Dispatcher.BeginInvoke((Action)(() => lbl_trans.Content = "Transfè a fini. "));
                            img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                            img_finish.Dispatcher.BeginInvoke((Action)(() => img_finish.Visibility = Visibility.Visible));
                            btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.IsEnabled = true));
                            btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.Content = "Fermer"));
                            this.Close();
                         }
                        else
                        {
                            MessageBox.Show(Constant.MSG_FICHIER_PAS_COPIE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 0));
                            img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                            btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.IsEnabled = true));
                        }

                    }
                    else
                    {
                        MessageBox.Show(Constant.MSG_TABLET_PAS_CONFIGURE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 0));
                        img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                        btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.IsEnabled = true));
                    }
                }
                else
                {
                    MessageBox.Show(Constant.MSG_TABLET_PAS_CONNECTE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 0));
                    img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                    btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.IsEnabled = true));
                }
            }
            catch (Exception)
            {

            }

        }

        public void pushFile()
        {

            string sdeId = null;
            if (device.IsConnected == true)
            {
                DeviceManager dev = null;
                DeviceInformation info = null;
                Tbl_Materiels mat = null;
                dev = new DeviceManager();
                info = dev.getDeviceInformation();
                Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
                settings = new ConfigurationService();
                mat = settings.getMateriels(info.Serial);
                if (mat != null)
                {
                    sdeId = settings.getSdeByAgent(mat.AgentId.GetValueOrDefault()).SdeId;
                    prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 50));
                    string sourceFileName = "";
                    if (Directory.Exists(MAIN_DATABASE_PATH))
                    {
                        string[] files = Directory.GetFiles(MAIN_DATABASE_PATH);
                        foreach (string f in files)
                        {
                            if (f.Contains(sdeId))
                            {
                                sourceFileName = System.IO.Path.GetFileName(f);
                                string destFileName = System.IO.Path.Combine(TEMP_DATABASE_PATH, Constant.DB_NAME + ".db");
                                if (!System.IO.File.Exists(destFileName))
                                {
                                    System.IO.File.Copy(f, destFileName, true);
                                }
                                else
                                {
                                    //System.IO.File.Delete(destFileName);
                                    System.IO.File.Copy(f, destFileName, true);
                                }
                                break;
                            }
                        }
                        files = Directory.GetFiles(TEMP_DATABASE_PATH);
                        bool pushed = false;
                        foreach (string f in files)
                        {
                            pushed = device.pushFile(f);
                            if (pushed == true)
                            {
                                System.IO.File.Delete(f);
                            }
                            Process[] procs = Process.GetProcessesByName("adb");
                            if (procs.Length != 0)
                            {
                                foreach (var proc in procs)
                                {
                                    if (!proc.HasExited)
                                    {
                                        proc.Kill();
                                    }
                                }
                            }
                            break;
                        }
                        prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 100));
                        MessageBox.Show(Constant.MSG_TRANSFERT_TERMINE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        lbl_trans.Dispatcher.BeginInvoke((Action)(() => lbl_trans.Content = "Transfè a fini. "));
                        img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                        img_finish.Dispatcher.BeginInvoke((Action)(() => img_finish.Visibility = Visibility.Visible));
                        btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.IsEnabled = true));
                        btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.Content = "Fermer"));
                     }
                }
                else
                {
                    MessageBox.Show(Constant.MSG_TABLET_PAS_CONFIGURE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                    btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.IsEnabled = true));
                    btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.Content = "Fermer"));
                }
            }
            else
            {
                MessageBox.Show(Constant.MSG_TABLET_PAS_CONNECTE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.IsEnabled = true));
                btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.Content = "Fermer"));
            }
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            if (btn_start.Content.ToString() == "Demarrer")
            {
                copied = false;
                if (t != null)
                {
                    if (t.IsAlive)
                    {
                        t.Abort();
                    }
                }
                prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 0));
                img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Visible));
                img_finish.Dispatcher.BeginInvoke((Action)(() => img_finish.Visibility = Visibility.Hidden));
                btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.IsEnabled = false));
                try
                {
                    ths = new ThreadStart(() => pushFile());
                    t = new Thread(ths);
                    t.Start();
                }
                catch (Exception ex)
                {
                    log.Info("ERREUR:<>===================<>" + ex.Message);
                }
            }
            else
            {
                this.Close();
            }
        }
    }
}
