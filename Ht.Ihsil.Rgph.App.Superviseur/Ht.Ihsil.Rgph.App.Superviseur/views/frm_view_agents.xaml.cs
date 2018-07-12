using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using Ht.Ihsil.Rgph.App.Superviseur.viewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace Ht.Ihsil.Rgph.App.Superviseur.views
{
    /// <summary>
    /// Interaction logic for frm_view_agents.xaml
    /// </summary>
    public partial class frm_view_agents : UserControl
    {
        public string ConfDir = "" + System.Windows.Forms.Application.StartupPath + "\\Data\\Configuration";
        private static string MAIN_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Data\Databases\";
        private static string TEMP_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Temp\";
        private static string BACKUP_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Backup\";
        private static string APP_DIRECTORY_PATH = AppDomain.CurrentDomain.BaseDirectory;
        AgentModel agentModel = null;
        SdeModel sdeModel = null;
        IConfigurationService service = null;
        IUtilisateurService utilisateurService = null;
        bool copied = false;

        public frm_view_agents()
        {
            InitializeComponent();
            service = new ConfigurationService();
            DataContext = this;
            lbAgents.ItemsSource = service.searchAllAgents();
            btn_save_tab.IsEnabled = false;
            utilisateurService = new UtilisateurService();
        }

        private void btn_synch_Click(object sender, RoutedEventArgs e)
        {
            ThreadStart ths = null;
            Thread t = null;
            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
            ths = new ThreadStart(() => configureTab());
            t = new Thread(ths);
            t.Start();
        }
        public void configureTab()
        {
            try
            {
                DeviceManager device = new DeviceManager();
                Process[] procs = Process.GetProcessesByName("adb");
                if (device.IsConnected == true)
                {
                    DeviceInformation devInfo = device.getDeviceInformation();
                    if (devInfo != null)
                    {
                        IConfigurationService service = new ConfigurationService();
                        if (service.isMaterielExist(devInfo.Serial))
                        {
                           
                            if (service.isMaterielConfigure(devInfo.Serial))
                            {
                                MessageBox.Show("Tablet sa konfigire deja pou yon lot ajan resanse.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                //Arretez le processus ADB
                                Utilities.killProcess(procs);
                                //
                                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            }
                            else
                            {
                                tbl_personnel person = new tbl_personnel();
                                person.persId = agentModel.AgentId;
                                person.sdeId = sdeModel.SdeId;
                                SdeInformation sde = new SdeInformation();
                                sde = Utilities.getSdeInformationForTabletConf(sdeModel.SdeId);
                                
                                person.prenom = agentModel.Prenom;
                                person.nom = agentModel.Nom;
                                person.nomUtilisateur = agentModel.Username;
                                person.motDePasse = agentModel.Password;
                                
                                person.ProfileId = Constant.PROFIL_AGENT_RECENSEUR_MOBILE;
                                person.estActif = 1;
                                person.sexe = agentModel.Sexe;
                                person.email = agentModel.Email;
                                person.comId = sde.ComId;
                                person.deptId = sde.DeptId;
                                person.vqseId = sde.VqseId;
                                person.zone = sde.Zone;
                                person.codeDistrict = sde.CodeDistrict;
                                person.motDePasse = "passpass";
                                service.savePersonne(person);
                                Tbl_Materiels mat = service.getMateriels(devInfo.Serial);
                                mat.IsConfigured = 1;
                                mat.LastSynchronisation = DateTime.Now.ToString();
                                service.updateMateriels(mat);
                                string sourceFileName = "";
                                if (Directory.Exists(MAIN_DATABASE_PATH))
                                {
                                    string[] files = Directory.GetFiles(MAIN_DATABASE_PATH);
                                    foreach (string f in files)
                                    {
                                        if (f.Contains(sdeModel.SdeId))
                                        {
                                            sourceFileName = System.IO.Path.GetFileName(f);
                                            string destFileName = System.IO.Path.Combine(TEMP_DATABASE_PATH, Constant.DB_NAME + ".db");
                                            if (!System.IO.File.Exists(destFileName))
                                            {
                                                System.IO.File.Copy(f, destFileName, true);
                                            }
                                            else
                                            {
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
                                        //Arretez le processus ADB
                                        Utilities.killProcess(procs);
                                        //
                                        break;
                                    }
                                    MessageBox.Show(Constant.MSG_TRANSFERT_TERMINE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                MessageBox.Show("Tablet sa a byen konfigire.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                gridTablette.Dispatcher.BeginInvoke((Action)(() => gridTablette.ItemsSource = ModelMapper.MapToList(service.SearchMateriels())));
                                //Arretez le processus ADB
                                Utilities.killProcess(procs);
                                //
                                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ou dwe anrejistre tablet sa a avan ou konfigire li.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            //Arretez le processus ADB
                            Utilities.killProcess(procs);
                            //
                            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Constant.MSG_TABLET_PAS_CONFIGURE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    //Arretez le processus ADB
                    Utilities.killProcess(procs);
                    //
                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lbAgents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                agentModel = new AgentModel();
                ListBox ltb = e.OriginalSource as ListBox;
                agentModel = ltb.SelectedItems.OfType<AgentModel>().FirstOrDefault();
                sdeModel = service.getSdeByAgent(agentModel.AgentId);
                List<SdeModel> sdes=new List<SdeModel>();
                sdes.Add(sdeModel);
                lbSdes.ItemsSource = sdes;
                List<AgentModel> agents = new List<AgentModel>();
                agents.Add(agentModel);
                List<MaterielModel> materielForAgent=new List<MaterielModel>();
                MaterielModel materiel = ModelMapper.MapToMateriel(service.getMaterielByAgent(agentModel.AgentId));
                if (materiel.MaterielId != 0)
                {
                    //Activer ou desactiver le bouton configurer si le materiel est deja configurer
                    if (materiel.Configure == "OUI")
                    {
                        btn_synch.IsEnabled = false;
                    }
                    else
                        btn_synch.IsEnabled = true;
                    //

                    //Desactiver le bouton save si le materiel est deja enregistre
                    btn_save_tab.IsEnabled = false;
                    //
                    materiel.Agent = agentModel.AgentName;
                    materielForAgent.Add(materiel);
                    gridTablette.ItemsSource = materielForAgent;
                    
                }
                else
                {
                    //Activer le bouton save
                    btn_save_tab.IsEnabled = true;
                    //Desactiver le botuon configurer
                    btn_synch.IsEnabled = false;
                    //Efface le grid
                    gridTablette.ItemsSource = new List<MaterielModel>();
                }
                              
             }
            catch (Exception)
            {

            }
        }

        private void btn_save_tab_Click(object sender, RoutedEventArgs e)
        {
            ThreadStart ths = null;
            Thread t = null;
            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
            ths = new ThreadStart(() => saveTab());
            t = new Thread(ths);
            t.Start();
        }

        public void saveTab()
        {
            try
            {
                DeviceManager device = new DeviceManager();
                Process[] procs = Process.GetProcessesByName("adb");
                if (device.IsConnected == true)
                {
                    DeviceInformation devInfo = device.getDeviceInformation();
                    if (devInfo != null)
                    {
                        IConfigurationService service = new ConfigurationService();
                        if (service.isMaterielExist(devInfo.Serial))
                        {
                            MessageBox.Show("Tablet sa anregistre deja pou yon lot ajan resanse.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            //Arretez le processus ADB
                            Utilities.killProcess(procs);
                            //
                            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                        }
                        else
                        {
                            if(service.isAgentExist(this.agentModel.AgentId)){
                                MessageBox.Show("Tablet sa anregistre deja pou yon lot ajan resanse.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                //Arretez le processus ADB
                                Utilities.killProcess(procs);
                                //
                                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            }
                            else
                            {
                                Tbl_Materiels mat = new Tbl_Materiels();
                                mat.Model = devInfo.Model;
                                mat.Serial = devInfo.Serial;
                                mat.Version = devInfo.OsVersion;
                                mat.DateAssignation = DateTime.Now.ToString();
                                mat.LastSynchronisation = DateTime.Now.ToString();
                                mat.IsConfigured = 0;
                                mat.Imei = devInfo.Imei;
                                mat.AgentId = this.agentModel.AgentId;
                                bool result = service.saveMateriels(mat);
                                if (!Directory.Exists(TEMP_DATABASE_PATH))
                                {
                                    Directory.CreateDirectory(TEMP_DATABASE_PATH);
                                }
                                copied = device.pullFile(TEMP_DATABASE_PATH);
                                //Arretez le processus ADB
                                Utilities.killProcess(procs);
                                //
                                if (copied == true)
                                {
                                    string db_backup = BACKUP_DATABASE_PATH + @"\\" + sdeModel.SdeId + "\\";
                                    if (!Directory.Exists(db_backup))
                                    {
                                        Directory.CreateDirectory(db_backup);
                                    }

                                    //
                                    if (Directory.GetDirectories(TEMP_DATABASE_PATH).Length != 0)
                                    {
                                       TEMP_DATABASE_PATH = TEMP_DATABASE_PATH + @"\\rgph_db";
                                    }

                                    //

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
                                        destFileName = System.IO.Path.Combine(MAIN_DATABASE_PATH, sdeModel.SdeId + ".SQLITE");
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
                                    MessageBox.Show(Constant.MSG_TRANSFERT_TERMINE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    MessageBox.Show(Constant.MSG_FICHIER_PAS_COPIE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                MessageBox.Show("Tablèt sa byen anregistre", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                gridTablette.Dispatcher.BeginInvoke((Action)(() => gridTablette.ItemsSource = ModelMapper.MapToList(service.SearchMateriels())));
                                //Arretez le processus ADB
                                Utilities.killProcess(procs);
                                //
                                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            }
                            
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Constant.MSG_TABLET_PAS_CONNECTE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                   
                    //Arretez le processus ADB
                    Utilities.killProcess(procs);
                    //
                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void gridTablette_AutoGeneratingColumn(object sender, DevExpress.Xpf.Grid.AutoGeneratingColumnEventArgs e)
        {
            if (e.Column.FieldName == "MaterielId")
                e.Column.Visible = false;
            if (e.Column.FieldName == "Imei")
                e.Column.Visible = false;
        }

        private void deleteDataItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            MaterielModel row = (MaterielModel)gridTablette.GetRow(gridTablette.GetSelectedRowHandles()[0]);
            if (row != null)
            {
                MessageBoxResult confirm = MessageBox.Show("Eske ou vle efase tablèt sa a?", Constant.WINDOW_TITLE, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirm == MessageBoxResult.Yes)
                {
                    bool result = service.deleteMateriel(Convert.ToInt32(row.MaterielId));
                    if (result == true)
                    {
                        MessageBox.Show("Tablèt sa efase.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        gridTablette.ItemsSource = ModelMapper.MapToList(service.SearchMateriels());

                    }
                }
             }
        }

        private void btn_add_sup_Click(object sender, RoutedEventArgs e)
        {

            ThreadStart ths = null;
            Thread t = null;
            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
            ths = new ThreadStart(() => save_sup());
            t = new Thread(ths);
            t.Start();
        }
        public void save_sup()
        {
            try
            {
                DeviceManager device = new DeviceManager();
                Process[] procs = Process.GetProcessesByName("adb");
                if (device.IsConnected == true)
                {
                    DeviceInformation devInfo = device.getDeviceInformation();
                    if (devInfo != null)
                    {
                        IConfigurationService service = new ConfigurationService();
                        if (service.isMaterielExist(devInfo.Serial))
                        {

                            if (!service.isMaterielConfigure(devInfo.Serial))
                            {
                                MessageBox.Show("Tablet sa a poko konfigire deja pou yon ajan. Ou pa ka ajoute yon sipevize.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                //Arretez le processus ADB
                                Utilities.killProcess(procs);
                                //
                                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            }
                            else
                            {

                                tbl_personnel person = new tbl_personnel();
                                UtilisateurModel user = utilisateurService.getSuperviseur(Constant.PROFIL_SUPERVISEUR_SUPERVISION_SG);
                                if (user.CodeUtilisateur =="")
                                {
                                    Utilities.killProcess(procs);
                                    //
                                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                                    return;
                                }
                                //Verifier si il ya deja un superviseur sur la tablette
                                if (service.ifSuperviseurExist(Constant.PROFIL_SUPERVISEUR_SUPERVISION_MOBILE) == true)
                                {
                                    MessageBox.Show("Gentan gen yon sipevize ki konfigire sou tablet sa a deja.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                    //Arretez le processus ADB
                                    Utilities.killProcess(procs);
                                    //
                                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                                    return;
                                }
                                person.persId = user.UtilisateurId;
                                person.sdeId = sdeModel.SdeId;
                                SdeInformation sde = new SdeInformation();
                                sde = Utilities.getSdeInformationForTabletConf(sdeModel.SdeId);

                                person.prenom = user.Prenom;
                                person.nom = user.Nom;
                                person.nomUtilisateur = user.CodeUtilisateur;
                                person.motDePasse = user.MotDePasse;
                                person.ProfileId = Constant.PROFIL_SUPERVISEUR_SUPERVISION_MOBILE;
                                person.estActif = 1;
                                //person.sexe = agentModel.Sexe;
                                //person.email = agentModel.Email;
                                person.comId = sde.ComId;
                                person.deptId = sde.DeptId;
                                person.vqseId = sde.VqseId;
                                person.zone = sde.Zone;
                                person.codeDistrict = sde.CodeDistrict;
                                person.motDePasse = "passpass";

                                service.savePersonne(person);
                                //Tbl_Materiels mat = service.getMateriels(devInfo.Serial);
                                //mat.IsConfigured = 1;
                                //mat.LastSynchronisation = DateTime.Now.ToString();
                                //service.updateMateriels(mat);
                                string sourceFileName = "";
                                if (Directory.Exists(MAIN_DATABASE_PATH))
                                {
                                    string[] files = Directory.GetFiles(MAIN_DATABASE_PATH);
                                    foreach (string f in files)
                                    {
                                        if (f.Contains(sdeModel.SdeId))
                                        {
                                            sourceFileName = System.IO.Path.GetFileName(f);
                                            string destFileName = System.IO.Path.Combine(TEMP_DATABASE_PATH, Constant.DB_NAME + ".db");
                                            if (!System.IO.File.Exists(destFileName))
                                            {
                                                System.IO.File.Copy(f, destFileName, true);
                                            }
                                            else
                                            {
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
                                        //Arretez le processus ADB
                                        Utilities.killProcess(procs);
                                        //
                                        break;
                                    }
                                    MessageBox.Show(Constant.MSG_TRANSFERT_TERMINE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                MessageBox.Show("Tablet sa a byen konfigire.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                gridTablette.Dispatcher.BeginInvoke((Action)(() => gridTablette.ItemsSource = ModelMapper.MapToList(service.SearchMateriels())));
                                //Arretez le processus ADB
                                Utilities.killProcess(procs);
                                //
                                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ou dwe anrejistre tablet sa a avan ou konfigire li.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            //Arretez le processus ADB
                            Utilities.killProcess(procs);
                            //
                            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Constant.MSG_TABLET_PAS_CONFIGURE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    //Arretez le processus ADB
                    Utilities.killProcess(procs);
                    //
                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
