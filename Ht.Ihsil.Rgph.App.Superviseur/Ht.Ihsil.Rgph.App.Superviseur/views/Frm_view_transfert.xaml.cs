using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.Exceptions;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ht.Ihsi.Rgph.Utility.Utils;
using Ht.Ihsil.Rgph.App.Superviseur.Schema;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System.Windows.Threading;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using System.Diagnostics;
using Ht.Ihsil.Rgph.App.Superviseur.Json;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace Ht.Ihsil.Rgph.App.Superviseur.views
{
    /// <summary>
    /// Interaction logic for Frm_view_transfert.xaml
    /// </summary>
    public partial class Frm_view_transfert : UserControl
    {
        private static string MAIN_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Data\Databases\";
        private static string TEMP_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Temp\";
        private static string BACKUP_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Backup\";
        private static string CONFIGURATION_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Configuration\";
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
        XmlUtils xmlConfiguration = null;
        public Frm_view_transfert(MainWindow1 main)
        {
            InitializeComponent();
            device = new DeviceManager();
            bckw = new BackgroundWorker();
            service = new SqliteDataReaderService();
            log = new Logger();
            this.main = main;
            mdfService = new MdfService();
            xmlConfiguration = new XmlUtils(CONFIGURATION_PATH + "configuration.xml");
            List<SdeModel> listOfSde = mdfService.getAllSde().ToList();
            Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
            DataContext = this;
            btn_effacer.IsEnabled = false;
            lbSdesCE.ItemsSource = listOfSde;
            lbSdes.ItemsSource = listOfSde;

            //Type de batiments
            List<TypeModel> listOfTB = new List<TypeModel>();
            listOfTB.Add(new TypeModel(" Batiman ki vid", "1"));
            listOfTB.Add(new TypeModel(" Batiman ki gen lojman lolektif", "2"));
            listOfTB.Add(new TypeModel(" Batiman ki gen lojman vid", "3"));
            listOfTB.Add(new TypeModel(" Batiman ki gen lojman endividyel", "4"));
            lbx_type_CE.ItemsSource = listOfTB;
            Users.users = new Users();
            Users.users.DatabasePath = MAIN_DATABASE_PATH;
        }

        public void changeStatus(string msg, int value)
        {

            this.Dispatcher.BeginInvoke((Action)delegate()
            {
                lbl_info_pda.Content = msg;
                prgb_trans_pda.Value = value;
            });
        }
        public void writeInBusyTool(string message)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
            {
                busyIndicator.BusyContent = message;

            }, null);
        }
        public void changeControlStatus(bool status)
        {
            main.rpc_C_ENQUETE.Dispatcher.BeginInvoke((Action)(() => main.rpc_C_ENQUETE.IsEnabled = status));
            main.rpc_rapports.Dispatcher.BeginInvoke((Action)(() => main.rpc_rapports.IsEnabled = status));
            main.rpc_sdes.Dispatcher.BeginInvoke((Action)(() => main.rpc_sdes.IsEnabled = status));
            main.rpc_tab_bord.Dispatcher.BeginInvoke((Action)(() => main.rpc_tab_bord.IsEnabled = status));
        }

        #region PULL AND PUSH FILES
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            copied = false;
            if (t != null)
            {
                if (t.IsAlive)
                {
                    t.Abort();
                }
            }
            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
            prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 0));
            img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Visible));
            img_finish.Dispatcher.BeginInvoke((Action)(() => img_finish.Visibility = Visibility.Hidden));
            try
            {
                ths = new ThreadStart(() => pullFile());
                t = new Thread(ths);
                t.Start();
            }
            catch (Exception ex)
            {
                log.Info("ERREUR:<>===================<>" + ex.Message);
            }
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
                        lbl_info_pda.Dispatcher.BeginInvoke((Action)(() => lbl_info_pda.Content = "Transfert des fichiers... "));
                        writeInBusyTool("Transfè a komanse...");
                        if (!Directory.Exists(TEMP_DATABASE_PATH))
                        {
                            Directory.CreateDirectory(TEMP_DATABASE_PATH);
                        }
                        writeInBusyTool("Fichye yo ap kopye");
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
                            //Mise a jour dans le fichier pour le rapport de tronc commun
                            reader = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));
                            Tbl_Sde sde = new Tbl_Sde();
                            sde = reader.getSdeDetailsFromSqliteFile();
                            sde.SdeId = sdeId;
                            sde.AgentId = mat.AgentId;
                            settings.updateSdeDetails(sde);
                            //
                            //Mise pour les retours
                            List<BatimentModel> listOfBatiment = reader.GetAllBatimentModel();
                            List<RetourModel> listOfRetours = settings.searchAllRetourBySde(sdeId);
                            if (listOfBatiment != null)
                            {
                                foreach (BatimentModel bat in listOfBatiment)
                                {
                                    if (listOfRetours != null)
                                    {
                                        foreach (RetourModel retour in listOfRetours)
                                        {

                                        }
                                    }

                                    //if(bat.BatimentId==settings.sea)
                                }

                            }
                            //
                            prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 100));
                            lbl_info_pda.Dispatcher.BeginInvoke((Action)(() => lbl_info_pda.Content = "Transfè a fini. "));
                            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                            img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                            img_finish.Dispatcher.BeginInvoke((Action)(() => img_finish.Visibility = Visibility.Visible));
                            changeControlStatus(true);
                        }
                        else
                        {
                            MessageBox.Show(Constant.MSG_FICHIER_PAS_COPIE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 0));
                            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                            img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                        }

                    }
                    else
                    {
                        MessageBox.Show(Constant.MSG_TABLET_PAS_CONFIGURE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 0));
                        waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                        busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                        img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                    }
                }
                else
                {
                    MessageBox.Show(Constant.MSG_TABLET_PAS_CONNECTE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 0));
                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                    img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                }
            }
            catch (Exception ex)
            {
                log.Info("Error =======================<>:" + ex.Message);
                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
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
                    prgb_trans_pc.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pc.Value = 50));
                    writeInBusyTool("Transfè a komanse...");

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
                        prgb_trans_pc.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pc.Value = 100));
                        MessageBox.Show(Constant.MSG_TRANSFERT_TERMINE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                        busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                        img_finish_pc.Dispatcher.BeginInvoke((Action)(() => img_finish_pc.Visibility = Visibility.Visible));
                        img_loading_pc.Dispatcher.BeginInvoke((Action)(() => img_loading_pc.Visibility = Visibility.Hidden));
                        busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                    }
                }
                else
                {
                    MessageBox.Show(Constant.MSG_TABLET_PAS_CONFIGURE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                    img_loading_pc.Dispatcher.BeginInvoke((Action)(() => img_loading_pc.Visibility = Visibility.Hidden));
                }
            }
            else
            {
                MessageBox.Show(Constant.MSG_TABLET_PAS_CONNECTE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
            }
        }


        private void btn_pc_tab_Click(object sender, RoutedEventArgs e)
        {
            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
            img_finish_pc.Dispatcher.BeginInvoke((Action)(() => img_finish_pc.Visibility = Visibility.Hidden));
            img_loading_pc.Dispatcher.BeginInvoke((Action)(() => img_loading_pc.Visibility = Visibility.Visible));
            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = true));
            try
            {
                ThreadStart ths = new ThreadStart(() => pushFile());
                Thread t = new Thread(ths);
                t.Start();
            }
            catch (Exception ex)
            {

            }
            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
            img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));

        }
        #endregion
        private void btn_transfertr_sc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btn_transfertr_sc.IsEnabled = false;
                writeInBusyTool("Tentative de connexion avec le serveur.");
                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
                busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = true));
                string adrMqttServer = xmlConfiguration.getAdrServer();
                if (Utilities.pingTheServer(adrMqttServer) == true)
                {
                    writeInBusyTool("Transfert des donnees vers le serveur.");
                    readData();
                }
                else
                {
                    MessageBox.Show("Le serveur n'est pas disponible.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                    btn_transfertr_sc.IsEnabled = true;
                }

            }
            catch (IPAdressException ex)
            {
                MessageBox.Show("" + ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                log.Info("Exception:" + ex.Message);
                log.Info("Inner Exception:" + ex.InnerException);
            }
        }
        public float calculPercent(int nbre)
        {
            return ((float)100) / ((float)nbre);
        }
        public void readData()
        {
            BrushConverter bc = new BrushConverter();
            img_loading_ser.Dispatcher.BeginInvoke((Action)(() => img_loading_ser.Visibility = Visibility.Visible));
            Dispatcher.Invoke(new Action(() =>
            {
                img_loading_ser.Visibility = Visibility.Visible;
                btn_transfert_pda.IsEnabled = false;
                btn_effacer.IsEnabled = false;

            }));
            txt_sortie.Text = "" + Environment.NewLine;
            if (sdeCollecteData != null)
            {
                this.sdeId = sdeCollecteData.SdeId;
                log.Info("SDE:" + sdeId);
                service = new SqliteDataReaderService(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));

                //
                //List<BatimentJson> batimentsJsons = service.GetAllBatimentsInJson();
                //log.Info("JSON Executed:" + batimentsJsons.Count);
                ////MemoryStream stream1 = new MemoryStream();
                //DataJson dataJson = new DataJson();
                //dataJson.username = "Adme Jean Jeff";
                //dataJson.deptId = "01";
                ////DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(BatimentJson));
                //using (StreamWriter write = new StreamWriter(MAIN_DATABASE_PATH + "DataJson.json"))
                //{
                //    foreach (BatimentJson bat in batimentsJsons)
                //    {
                //        bat.dateEnvoi = DateTime.Now.ToShortDateString();
                //        dataJson.data = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bat)));
                //        string dJson = JsonConvert.SerializeObject(dataJson);
                //        byte[] datas = Encoding.UTF8.GetBytes(dJson);
                //        write.Write(Encoding.UTF8.GetString(datas, 0, datas.Length));     
                //    }                                     
                //}              
                //
                sqliteWrite = new SqliteDataWriter(sdeId);
                TransfertService transfert = new TransfertService();
                List<BatimentJson> batimentsJsons = service.GetAllBatimentsInJson();
                log.Info("JSON Executed:" + batimentsJsons.Count);

                Dispatcher.Invoke(new Action(() =>
                {
                    lbl_sde.Content = "SDE:" + this.sdeId;
                    lbl_statut_transfert.Content = "En Cours";
                    lbl_statut_transfert.Foreground = (Brush)bc.ConvertFrom("#FF2AB2FF");
                    prg_trans_sc.Value = 0;
                }));
                float percent = 0;
                int nbreBat = batimentsJsons.Count();
                if (nbreBat != 0)
                {
                    percent = calculPercent(nbreBat);

                }
                else
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        lbl_sde.Content = "SDE:" + sdeId;
                        lbl_statut_transfert.Content = "deja transféré";
                        lbl_statut_transfert.Foreground = (Brush)bc.ConvertFrom("#FF2AB2FF");
                        prg_trans_sc.Value = 0;
                    }));
                }
                //
                //
                //#region ENVOI DES BATIMENTS
                //if (batimentsJsons.Count > 0)
                //{

                //    sqliteWrite = new SqliteDataWriter();
                //    foreach (BatimentJson bat in batimentsJsons)
                //    {


                //        if (Utils.IsNotNull(bat) && bat.isSynchroToCentrale == false)
                //        {
                //            DataJson dataJson = new DataJson();
                //            dataJson.username = "Adme Jean Jeff";
                //            dataJson.deptId = "01";
                //            bat.dateEnvoi = DateTime.Now.ToShortDateString();
                //            dataJson.data = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bat)));
                //            string dJson = JsonConvert.SerializeObject(dataJson);

                //            if (transfert.publishBatimentData(dJson))
                //            {
                //                //Ecriture dans le fichier sqlite pr dire que le batiment a ete transfere vers le serveur centrale
                //                BatimentModel btm = new BatimentModel();
                //                btm.BatimentId = bat.batimentId;
                //                //btm.SdeId = bat.sdeId;
                //                btm.SdeId = sdeId;
                //                btm.IsSynchroToCentrale = true;
                //                //sqliteWrite.syncroBatimentToServeur(btm);
                //                //
                //                Dispatcher.Invoke(new Action(() =>
                //                {
                //                    txt_sortie.Text += "<>===================== Batiment:" + bat.batimentId + " transféré avec succes" + Environment.NewLine;
                //                    txt_sortie.CaretIndex = txt_sortie.Text.Length;
                //                    var rect = txt_sortie.GetRectFromCharacterIndex(txt_sortie.CaretIndex);
                //                    txt_sortie.ScrollToHorizontalOffset(rect.Right);
                //                    txt_sortie.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                //                    prg_trans_sc.Value += percent;
                //                    btn_transfert_pda.IsEnabled = false;
                //                }), System.Windows.Threading.DispatcherPriority.Background);

                //            }
                //            else
                //            {
                //                Dispatcher.Invoke(new Action(() =>
                //                {
                //                    txt_sortie.Text += "<>===================== Serveur insdisponible" + Environment.NewLine;
                //                    txt_sortie.CaretIndex = txt_sortie.Text.Length;
                //                    var rect = txt_sortie.GetRectFromCharacterIndex(txt_sortie.CaretIndex);
                //                    txt_sortie.ScrollToHorizontalOffset(rect.Right);
                //                    txt_sortie.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                //                    prg_trans_sc.Value += percent;
                //                    btn_transfert_pda.IsEnabled = false;
                //                }), System.Windows.Threading.DispatcherPriority.Background);
                //            }
                //        }
                //        else
                //        {
                //            Dispatcher.Invoke(new Action(() =>
                //            {
                //                txt_sortie.Text += "<>=========Batiment deja transfere=========<>" + Environment.NewLine;
                //                txt_sortie.CaretIndex = txt_sortie.Text.Length;
                //                var rect = txt_sortie.GetRectFromCharacterIndex(txt_sortie.CaretIndex);
                //                txt_sortie.ScrollToHorizontalOffset(rect.Right);
                //                txt_sortie.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                //                prg_trans_sc.Value += percent;
                //                btn_transfert_pda.IsEnabled = false;
                //            }), System.Windows.Threading.DispatcherPriority.Background);
                //        }


                //    }

                //    Dispatcher.Invoke(new Action(() =>
                //    {
                //        lbl_sde.Content = "Transfert " + sdeId;
                //        lbl_statut_transfert.Content = "Termine";
                //        lbl_statut_transfert.Foreground = (Brush)bc.ConvertFrom("#FF26BD64");
                //        img_loading_ser.Visibility = Visibility.Hidden;
                //        btn_transfertr_sc.IsEnabled = true;
                //        btn_effacer.IsEnabled = true;
                //        writeInBusyTool("Transfert termine.");
                //        waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                //        busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                //    }));
                //}
                //else
                //{
                //    MessageBox.Show("Pa gen batiman.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                //    Dispatcher.Invoke(new Action(() =>
                //    {
                //        lbl_sde.Content = "";
                //        lbl_statut_transfert.Content = "";
                //        prg_trans_sc.Value = 0;
                //        waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                //        busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                //        img_loading_ser.Visibility = Visibility.Hidden;
                //        btn_transfertr_sc.IsEnabled = true;
                //    }));
                //}
                ////
                //#endregion

                //#region ENVOI DES RAPPORTS
                IContreEnqueteService contreEnqueteService = new ContreEnqueteService();
                //#region RAPPORT PERSONNEL
                //List<RapportPersonnelJson> rapportPersonnels = ModelMapper.MapToListJson(contreEnqueteService.searchRptPersonnel());
                //if (rapportPersonnels != null)
                //{
                //    DataJson data = new DataJson();
                //    data.deptId = "01";
                //    data.username = "user";
                //    foreach (RapportPersonnelJson rpt in rapportPersonnels)
                //    {
                //        data.data = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(rpt)));
                //        string dJson = JsonConvert.SerializeObject(data);
                //        if (transfert.publishRapportSupervisionDirect(dJson))
                //        {
                //            //Ecriture dans le fichier sqlite pr dire que le rapport a ete transfere vers le serveur centrale

                //            //
                //            Dispatcher.Invoke(new Action(() =>
                //            {
                //                txt_sortie.Text += "<>===================== Rapport:" + rpt.rapportId + " transféré avec succes" + Environment.NewLine;
                //                txt_sortie.CaretIndex = txt_sortie.Text.Length;
                //                var rect = txt_sortie.GetRectFromCharacterIndex(txt_sortie.CaretIndex);
                //                txt_sortie.ScrollToHorizontalOffset(rect.Right);
                //                txt_sortie.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                //                prg_trans_sc.Value += percent;
                //                btn_transfert_pda.IsEnabled = false;
                //            }), System.Windows.Threading.DispatcherPriority.Background);

                //        }
                //        else
                //        {
                //            Dispatcher.Invoke(new Action(() =>
                //            {
                //                txt_sortie.Text += "<>===================== Serveur insdisponible" + Environment.NewLine;
                //                txt_sortie.CaretIndex = txt_sortie.Text.Length;
                //                var rect = txt_sortie.GetRectFromCharacterIndex(txt_sortie.CaretIndex);
                //                txt_sortie.ScrollToHorizontalOffset(rect.Right);
                //                txt_sortie.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                //                prg_trans_sc.Value += percent;
                //                btn_transfert_pda.IsEnabled = false;
                //            }), System.Windows.Threading.DispatcherPriority.Background);
                //        }
                //    }

                //}
                //else
                //{
                //    MessageBox.Show("Pa gen rapo.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                //    Dispatcher.Invoke(new Action(() =>
                //    {
                //        lbl_sde.Content = "";
                //        lbl_statut_transfert.Content = "";
                //        prg_trans_sc.Value = 0;
                //        waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                //        busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                //        img_loading_ser.Visibility = Visibility.Hidden;
                //        btn_transfertr_sc.IsEnabled = true;
                //    }));
                //}
                //#endregion

                //#region RAPPORT PROBLEME
                //settings = new ConfigurationService();
                //List<ProblemeJson> problemes = ModelMapper.MapToListJson(settings.searchAllProblemesBySdeId(sdeId));
                //if (problemes != null)
                //{
                //    DataJson data = new DataJson();
                //    data.deptId = "01";
                //    data.username = "user";
                //    data.data = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(problemes)));
                //    string dJson = JsonConvert.SerializeObject(data);
                //    if (transfert.publishRapportProbleme(dJson))
                //    {
                //        //Ecriture dans le fichier sqlite pr dire que le rapport a ete transfere vers le serveur centrale

                //        //
                //        Dispatcher.Invoke(new Action(() =>
                //        {
                //            txt_sortie.Text += "<>===================== Liste de problemes transférés avec succes" + Environment.NewLine;
                //            txt_sortie.CaretIndex = txt_sortie.Text.Length;
                //            var rect = txt_sortie.GetRectFromCharacterIndex(txt_sortie.CaretIndex);
                //            txt_sortie.ScrollToHorizontalOffset(rect.Right);
                //            txt_sortie.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                //            prg_trans_sc.Value += percent;
                //            btn_transfert_pda.IsEnabled = false;
                //        }), System.Windows.Threading.DispatcherPriority.Background);

                //    }
                //    else
                //    {
                //        Dispatcher.Invoke(new Action(() =>
                //        {
                //            txt_sortie.Text += "<>===================== Serveur insdisponible" + Environment.NewLine;
                //            txt_sortie.CaretIndex = txt_sortie.Text.Length;
                //            var rect = txt_sortie.GetRectFromCharacterIndex(txt_sortie.CaretIndex);
                //            txt_sortie.ScrollToHorizontalOffset(rect.Right);
                //            txt_sortie.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                //            prg_trans_sc.Value += percent;
                //            btn_transfert_pda.IsEnabled = false;
                //        }), System.Windows.Threading.DispatcherPriority.Background);
                //    }
                //    //
                //    //foreach (ProblemeJson prob in problemes)
                //    //{
                //    //    data.data = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(prob)));
                //    //    string dJson = JsonConvert.SerializeObject(data);
                //    //    if (transfert.publishRapportProbleme(dJson))
                //    //    {
                //    //        //Ecriture dans le fichier sqlite pr dire que le rapport a ete transfere vers le serveur centrale

                //    //        //
                //    //        Dispatcher.Invoke(new Action(() =>
                //    //        {
                //    //            txt_sortie.Text += "<>===================== Rapport/Batiman:" + prob.problemeId + " transféré avec succes" + Environment.NewLine;
                //    //            txt_sortie.CaretIndex = txt_sortie.Text.Length;
                //    //            var rect = txt_sortie.GetRectFromCharacterIndex(txt_sortie.CaretIndex);
                //    //            txt_sortie.ScrollToHorizontalOffset(rect.Right);
                //    //            txt_sortie.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                //    //            prg_trans_sc.Value += percent;
                //    //            btn_transfert_pda.IsEnabled = false;
                //    //        }), System.Windows.Threading.DispatcherPriority.Background);

                //    //    }
                //    //    else
                //    //    {
                //    //        Dispatcher.Invoke(new Action(() =>
                //    //        {
                //    //            txt_sortie.Text += "<>===================== Serveur insdisponible" + Environment.NewLine;
                //    //            txt_sortie.CaretIndex = txt_sortie.Text.Length;
                //    //            var rect = txt_sortie.GetRectFromCharacterIndex(txt_sortie.CaretIndex);
                //    //            txt_sortie.ScrollToHorizontalOffset(rect.Right);
                //    //            txt_sortie.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                //    //            prg_trans_sc.Value += percent;
                //    //            btn_transfert_pda.IsEnabled = false;
                //    //        }), System.Windows.Threading.DispatcherPriority.Background);
                //    //    }
                //    //}
                //    lbl_sde.Content = "";
                //    lbl_statut_transfert.Content = "";
                //    prg_trans_sc.Value = 0;
                //    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                //    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                //    img_loading_ser.Visibility = Visibility.Hidden;
                //    btn_transfertr_sc.IsEnabled = true;
                //}
                //#endregion

                #region RAPPORT PROBLEME
                List<RapportDeroulementJson> rapportsDeroulements = ModelMapper.MapToListJson(contreEnqueteService.searchRptDeroulment());
                if (rapportsDeroulements != null)
                {
                    DataJson data = new DataJson();
                    data.deptId = "01";
                    data.username = "user";
                    foreach (RapportDeroulementJson rpt in rapportsDeroulements)
                    {
                        RapportDeroulementModel model=new RapportDeroulementModel();
                        model.RapportId=rpt.rapportId;
                        List<DetailsRapportJson> lists = ModelMapper.MapToListJson(contreEnqueteService.searchDetailsReport(model));
                        rpt.rdcDetails = lists;
                        data.data = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(rpt)));
                        string dJson = JsonConvert.SerializeObject(data);
                        if (transfert.publishRapporDeroulementCollecte(dJson))
                        {
                            //Ecriture dans le fichier sqlite pr dire que le rapport a ete transfere vers le serveur centrale

                            //
                            Dispatcher.Invoke(new Action(() =>
                            {
                                txt_sortie.Text += "<>===================== Rapport deroulement transfert avec succees" + Environment.NewLine;
                                txt_sortie.CaretIndex = txt_sortie.Text.Length;
                                var rect = txt_sortie.GetRectFromCharacterIndex(txt_sortie.CaretIndex);
                                txt_sortie.ScrollToHorizontalOffset(rect.Right);
                                txt_sortie.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                                prg_trans_sc.Value += percent;
                                btn_transfert_pda.IsEnabled = false;
                            }), System.Windows.Threading.DispatcherPriority.Background);

                        }
                        else
                        {
                            Dispatcher.Invoke(new Action(() =>
                            {
                                txt_sortie.Text += "<>===================== Serveur insdisponible" + Environment.NewLine;
                                txt_sortie.CaretIndex = txt_sortie.Text.Length;
                                var rect = txt_sortie.GetRectFromCharacterIndex(txt_sortie.CaretIndex);
                                txt_sortie.ScrollToHorizontalOffset(rect.Right);
                                txt_sortie.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                                prg_trans_sc.Value += percent;
                                btn_transfert_pda.IsEnabled = false;
                            }), System.Windows.Threading.DispatcherPriority.Background);
                        }
                    }
                    
                    //
                    //foreach (ProblemeJson prob in problemes)
                    //{
                    //    data.data = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(prob)));
                    //    string dJson = JsonConvert.SerializeObject(data);
                    //    if (transfert.publishRapportProbleme(dJson))
                    //    {
                    //        //Ecriture dans le fichier sqlite pr dire que le rapport a ete transfere vers le serveur centrale

                    //        //
                    //        Dispatcher.Invoke(new Action(() =>
                    //        {
                    //            txt_sortie.Text += "<>===================== Rapport/Batiman:" + prob.problemeId + " transféré avec succes" + Environment.NewLine;
                    //            txt_sortie.CaretIndex = txt_sortie.Text.Length;
                    //            var rect = txt_sortie.GetRectFromCharacterIndex(txt_sortie.CaretIndex);
                    //            txt_sortie.ScrollToHorizontalOffset(rect.Right);
                    //            txt_sortie.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                    //            prg_trans_sc.Value += percent;
                    //            btn_transfert_pda.IsEnabled = false;
                    //        }), System.Windows.Threading.DispatcherPriority.Background);

                    //    }
                    //    else
                    //    {
                    //        Dispatcher.Invoke(new Action(() =>
                    //        {
                    //            txt_sortie.Text += "<>===================== Serveur insdisponible" + Environment.NewLine;
                    //            txt_sortie.CaretIndex = txt_sortie.Text.Length;
                    //            var rect = txt_sortie.GetRectFromCharacterIndex(txt_sortie.CaretIndex);
                    //            txt_sortie.ScrollToHorizontalOffset(rect.Right);
                    //            txt_sortie.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                    //            prg_trans_sc.Value += percent;
                    //            btn_transfert_pda.IsEnabled = false;
                    //        }), System.Windows.Threading.DispatcherPriority.Background);
                    //    }
                    //}
                    lbl_sde.Content = "";
                    lbl_statut_transfert.Content = "";
                    prg_trans_sc.Value = 0;
                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                    img_loading_ser.Visibility = Visibility.Hidden;
                    btn_transfertr_sc.IsEnabled = true;
                }
                #endregion

                //#endregion

            }
            else
            {
                MessageBox.Show("Chwazi yon SDE.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                img_loading_ser.Visibility = Visibility.Hidden;
                btn_transfertr_sc.IsEnabled = true;
                prg_trans_sc.Value = 0;
                btn_effacer.IsEnabled = false;
                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));

            }
        }

        private void lbSdes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBox ltb = e.OriginalSource as ListBox;
                sdeCollecteData = ltb.SelectedItems.OfType<SdeModel>().FirstOrDefault();
            }
            catch (Exception)
            {

            }

        }

        private void btn_effacer_Click(object sender, RoutedEventArgs e)
        {
            txt_sortie.Text = "" + Environment.NewLine;
            btn_effacer.IsEnabled = false;
            prg_trans_sc.Value = 0;
        }
        private void lbSdesCE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBox ltb = e.OriginalSource as ListBox;
                sdeCEData = ltb.SelectedItems.OfType<SdeModel>().FirstOrDefault();
            }
            catch (Exception)
            {

            }
        }

        private void lbx_type_CE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sdeCEData != null)
                {
                    ListBox ltb = e.OriginalSource as ListBox;
                    typeBatiment = ltb.SelectedItems.OfType<TypeModel>().FirstOrDefault();
                }
                else
                {
                    lbx_type_CE.SelectedItem = null;
                    MessageBox.Show("Ou dwe chwazi yon SDE.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception)
            {

            }
        }

        private void btn_trans_ce_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (typeBatiment != null)
                {
                    BrushConverter bc = new BrushConverter();
                    img_ce.Dispatcher.BeginInvoke((Action)(() => img_ce.Visibility = Visibility.Visible));
                    Dispatcher.Invoke(new Action(() =>
                    {
                        img_ce.Visibility = Visibility.Visible;
                        //tbc_visualisation.IsEnabled = false;
                        //tbc_contre_enquete.IsEnabled = false;
                        //dashboard.IsEnabled = false;
                        btn_transfert_pda.IsEnabled = false;
                        btn_effacer_ce.IsEnabled = false;
                    }));
                    txt_sortie_ce.Text = "" + Environment.NewLine;
                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
                    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = true));
                    writeInBusyTool("Transfert contre-enquete en cours.");
                    List<ContreEnqueteType> listOfCE = mdfService.getContreEnquete(sdeCEData.SdeId, Convert.ToInt32(typeBatiment.Type));
                    TransfertService transfert = new TransfertService();

                    if (listOfCE.Count > 0)
                    {
                        float percent = calculPercent(listOfCE.Count);
                        foreach (ContreEnqueteType cet in listOfCE)
                        {
                            BatimentData batimentData = new BatimentData();
                            batimentData.contreEnquete = cet;
                            batimentData.dataType = 1;
                            BatimentType data = mdfService.getBatimentDataForCE(cet.batimentId, cet.sdeId);
                            batimentData.data = data;
                            if (transfert.publishContreEntreData(batimentData))
                            {
                                Dispatcher.Invoke(new Action(() =>
                                {
                                    txt_sortie_ce.Text += "<>===================== Kont anket sou batiman :" + cet.batimentId + " transfere avek sikse" + Environment.NewLine;
                                    txt_sortie_ce.CaretIndex = txt_sortie.Text.Length;
                                    var rect = txt_sortie_ce.GetRectFromCharacterIndex(txt_sortie.CaretIndex);
                                    txt_sortie_ce.ScrollToHorizontalOffset(rect.Right);
                                    txt_sortie_ce.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                                    prg_trans_sc_ce.Value += percent;

                                }), System.Windows.Threading.DispatcherPriority.Background);
                            }
                            else
                            {
                                Dispatcher.Invoke(new Action(() =>
                                {
                                    txt_sortie_ce.Text += "<>===================== Serveur insdisponible" + Environment.NewLine;
                                    txt_sortie_ce.CaretIndex = txt_sortie_ce.Text.Length;
                                    var rect = txt_sortie.GetRectFromCharacterIndex(txt_sortie_ce.CaretIndex);
                                    txt_sortie_ce.ScrollToHorizontalOffset(rect.Right);
                                    txt_sortie_ce.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                                    prg_trans_sc_ce.Value += percent;

                                }), System.Windows.Threading.DispatcherPriority.Background);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ou poko fè kont-ankèt sou batiman ki gen lojman kolektif", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Chwazi ki tip kont-ankèt wap transfere a.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                    btn_effacer.Dispatcher.BeginInvoke((Action)(() => btn_effacer.IsEnabled = false));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Le serveur est indisponible.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                prg_trans_sc_ce.Value = 0;
                img_ce.Dispatcher.BeginInvoke((Action)(() => img_ce.Visibility = Visibility.Hidden));
            }
        }

        private void chk_batiman_made_Checked(object sender, RoutedEventArgs e)
        {
            chk_batiman_valide.IsChecked = false;
        }

        private void chk_batiman_valide_Checked(object sender, RoutedEventArgs e)
        {
            chk_batiman_made.IsChecked = false;
        }


    }
}
