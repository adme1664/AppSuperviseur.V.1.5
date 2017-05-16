using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
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

namespace Ht.Ihsil.Rgph.App.Superviseur.views.Contre_Enquete
{
    /// <summary>
    /// Logique d'interaction pour frm_view_ce.xaml
    /// </summary>
    public partial class frm_view_ce : UserControl
    {
#region CONSTRCUTORS
        public  frm_view_ce (Object obj)
        {
            InitializeComponent();
            MenageCEModel _menage = null;
            LogementCEModel _logement = null;
            DecesCEModel _deces = null;
            IndividuCEModel _individu = null;
            List<DataDetails> reponses = null;
            BatimentCEModel _batiment = null;
            
            if (obj.ToString() == Constant.OBJET_MODEL_MENAGECE)
            {
                _menage = obj as MenageCEModel;
                reponses = DataDetailsMapper.MapToCE<MenageCEModel>(_menage);
            }
            if (obj.ToString() == Constant.OBJET_MODEL_BATIMENTCE)
            {
                _batiment = obj as BatimentCEModel;
                reponses = DataDetailsMapper.MapToCE<BatimentCEModel>(_batiment);
            }
            if (obj.ToString() == Constant.OBJET_MODEL_LOGEMENTCE)
            {
                _logement = obj as LogementCEModel;
                reponses = DataDetailsMapper.MapToCE<LogementCEModel>(_logement);
            }
            if (obj.ToString() == Constant.OBJET_MODEL_INDIVIDUCE)
            {
                _individu = obj as IndividuCEModel;
                reponses = DataDetailsMapper.MapToCE<IndividuCEModel>(_individu);
            }
            if (obj.ToString() == Constant.OBJET_MODEL_DECESCE)
            {
                _deces = obj as DecesCEModel;
                reponses = DataDetailsMapper.MapToCE<DecesCEModel>(_deces);
            }
           
            string kategori = reponses.ElementAt(0).Kategori;
            List<String> listOfkategori = new List<string>();
            listOfkategori.Add(kategori);
            foreach (DataDetails rep in reponses)
            {
                if (rep.Kategori != kategori)
                {
                    if (Utilities.isCategorieExist(listOfkategori, rep.Kategori) == false)
                    {
                        listOfkategori.Add(rep.Kategori);
                        kategori = rep.Kategori;
                    }
                    
                }
                
            }
            if (listOfkategori.Count == 0)
            {
                listOfkategori.Add(kategori);
            }
            if (listOfkategori.Count > 0)
            {
                foreach (String kat in listOfkategori)
                {
                    List<DataDetails> listPerkategori = reponses.FindAll(k => k.Kategori == kat);
                    TabItem item = new TabItem();
                    item.HorizontalAlignment = HorizontalAlignment.Stretch;
                    item.VerticalAlignment = VerticalAlignment.Stretch;
                    item.VerticalContentAlignment = VerticalAlignment.Stretch;
                    StackPanel stc = new StackPanel();
                    stc.Height = 35;
                    stc.Orientation = Orientation.Horizontal;
                    stc.Margin = new Thickness(4, 0, 39, 0);

                    Image img = new Image();
                    BitmapImage bImg = new BitmapImage();
                    bImg.BeginInit();
                    bImg.UriSource = new Uri(@"/images/tb.png",UriKind.Relative);
                    bImg.EndInit();
                    img.Source = bImg;
                    stc.Children.Add(img);

                    TextBlock txt = new TextBlock();
                    txt.FontSize = 11;
                    txt.VerticalAlignment = VerticalAlignment.Center;
                    txt.HorizontalAlignment = HorizontalAlignment.Center;
                    txt.FontWeight = FontWeights.Bold;
                    txt.Text = kat;
                    stc.Children.Add(txt);

                    item.Header = stc;

                    Grid mgrd = new Grid();
                    mgrd.HorizontalAlignment = HorizontalAlignment.Stretch;
                    mgrd.VerticalAlignment = VerticalAlignment.Stretch;

                    Grid cgrd = new Grid();
                    cgrd.HorizontalAlignment = HorizontalAlignment.Stretch;
                    cgrd.VerticalAlignment = VerticalAlignment.Stretch;
                    cgrd.Margin = new Thickness(10, 10, 0, 0);

                    DataGrid dtg1 = new DataGrid();
                    dtg1.ColumnWidth = 605;
                    dtg1.FontSize = 13;
                    Brush brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFC9E7F5"));
                    dtg1.AlternatingRowBackground = brush;
                    dtg1.AlternationCount = 2;
                    dtg1.Margin = new Thickness(10, 24, 10, 10);
                    dtg1.HorizontalAlignment = HorizontalAlignment.Stretch;
                    dtg1.VerticalAlignment = VerticalAlignment.Stretch;
                    dtg1.ItemsSource = listPerkategori;
                    dtg1.AutoGeneratingColumn += dtg1_AutoGeneratingColumn;
                    cgrd.Children.Add(dtg1);
                    mgrd.Children.Add(cgrd);
                    item.Content = mgrd;
                    main_tab.Items.Add(item);
                }
            }
        }
        public frm_view_ce(Object obj, string sdeId)
        {
            InitializeComponent();
            List<DataDetails> reponses = null;
            IndividuModel _mIndividu = null;
            BatimentModel _mBatiment = null;
            MenageModel _mMenage = null;
            LogementModel _mLogement = null;
            EmigreModel _mEmigre = null;
            DecesModel _mDeces = null;
            MenageDetailsModel detailsModel = null;
            string libele = null;
            ISqliteReader reader = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath,sdeId));
            if (obj.ToString() == Constant.OBJET_MODEL_BATIMENT)
            {
                _mBatiment = obj as BatimentModel;
                libele ="#SDE:"+_mBatiment.SdeId+"/Batiman:"+_mBatiment.BatimentId;
                reponses = DataDetailsMapper.MapToMobile<BatimentModel>(_mBatiment, sdeId);
            }
            if (obj.ToString() == Constant.OBJET_MODEL_LOGEMENT)
            {
                _mLogement= obj as LogementModel;
                libele = "#SDE:" + _mLogement.SdeId + "/Batiman:" + _mLogement.BatimentId.ToString()+"/Lojman:"+_mLogement.Qlin1NumeroOrdre.ToString();
                reponses = DataDetailsMapper.MapToMobile<LogementModel>(_mLogement, sdeId);
            }
            if (obj.ToString() == Constant.OBJET_MODEL_MENAGE)
            {
                _mMenage = obj as MenageModel;
                libele = "#SDE:" + _mMenage.SdeId + "/Batiman:" + _mMenage.BatimentId.ToString() + "/Lojman:" + _mMenage.LogeId.ToString()+"/Menaj:"+_mMenage.Qm1NoOrdre.ToString();
                reponses = DataDetailsMapper.MapToMobile<MenageModel>(_mMenage, sdeId);
            }
            if (obj.ToString() == Constant.OBJET_MODEL_MENAGE_DETAILS)
            {
                detailsModel = obj as MenageDetailsModel;
                if (detailsModel.Type == Constant.CODE_TYPE_EMIGRE)
                {
                    _mEmigre = reader.GetEmigreById(Convert.ToInt32(detailsModel.Id));
                    libele = "#SDE:" + _mEmigre.SdeId + "/Batiman:" + _mEmigre.BatimentId.ToString() + "/Lojman:" + _mEmigre.LogeId.ToString() + "/Menaj:" + _mEmigre.MenageId.ToString() + "/Emigre:" + _mEmigre.Qn1numeroOrdre.ToString();
                    reponses = DataDetailsMapper.MapToMobile<EmigreModel>(_mEmigre, sdeId);
                }
                if (detailsModel.Type == Constant.CODE_TYPE_DECES)
                {
                    _mDeces = reader.GetDecesById(Convert.ToInt32(detailsModel.Id));
                    libele = "#SDE:" + _mDeces.SdeId + "/Batiman:" + _mDeces.BatimentId.ToString() + "/Lojman:" + _mDeces.LogeId.ToString() + "/Menaj:" + _mDeces.MenageId.ToString() + "/Dese:" + _mDeces.Qd2NoOrdre.ToString();
                    reponses = DataDetailsMapper.MapToMobile<DecesModel>(_mDeces, sdeId);
                }
                if (detailsModel.Type == Constant.CODE_TYPE_ENVDIVIDI)
                {
                    _mIndividu = reader.GetIndividuById(Convert.ToInt32(detailsModel.Id));
                    libele = "#SDE:" + _mIndividu.SdeId + "/Batiman:" + _mIndividu.BatimentId.ToString() + "/Lojman:" + _mIndividu.LogeId.ToString() + "/Menaj:" + _mIndividu.MenageId.ToString() + "/Endividi:" + _mIndividu.Q1NoOrdre.ToString();
                    reponses = DataDetailsMapper.MapToMobile<IndividuModel>(_mIndividu, sdeId);
                }
            }
            
            string kategori = reponses.ElementAt(0).Kategori;
            List<String> listOfkategori = new List<string>();
            listOfkategori.Add(kategori);
            foreach (DataDetails rep in reponses)
            {
                if (rep.Kategori != kategori)
                {
                    if (Utilities.isCategorieExist(listOfkategori, rep.Kategori) == false)
                    {
                        listOfkategori.Add(rep.Kategori);
                        kategori = rep.Kategori;
                    }
                }
            }
            if (listOfkategori.Count == 0)
            {
                listOfkategori.Add(kategori);
            }
            if (listOfkategori.Count > 0)
            {
                foreach (String kat in listOfkategori)
                {
                    List<DataDetails> listPerkategori = reponses.FindAll(k => k.Kategori == kat);
                    TabItem item = new TabItem();
                    item.HorizontalAlignment = HorizontalAlignment.Stretch;
                    item.VerticalAlignment = VerticalAlignment.Stretch;
                    item.VerticalContentAlignment = VerticalAlignment.Stretch;
                    StackPanel stc = new StackPanel();
                    stc.Height = 35;
                    stc.Orientation = Orientation.Horizontal;
                    stc.Margin = new Thickness(4, 0, 39, 0);

                    Image img = new Image();
                    BitmapImage bImg = new BitmapImage();
                    bImg.BeginInit();
                    bImg.UriSource = new Uri(@"/images/tb.png", UriKind.Relative);
                    bImg.EndInit();
                    img.Source = bImg;
                    stc.Children.Add(img);

                    TextBlock txt = new TextBlock();
                    txt.FontSize = 11;
                    txt.VerticalAlignment = VerticalAlignment.Center;
                    txt.HorizontalAlignment = HorizontalAlignment.Center;
                    txt.FontWeight = FontWeights.Bold;
                    txt.Text = kat;
                    stc.Children.Add(txt);

                    item.Header = stc;

                    Grid mgrd = new Grid();
                    mgrd.HorizontalAlignment = HorizontalAlignment.Stretch;
                    mgrd.VerticalAlignment = VerticalAlignment.Stretch;

                    Grid cgrd = new Grid();
                    cgrd.HorizontalAlignment = HorizontalAlignment.Stretch;
                    cgrd.VerticalAlignment = VerticalAlignment.Stretch;
                    cgrd.Margin = new Thickness(10, 10, 0, 0);

                    Label lbl_details = new Label();
                    lbl_details.Content =libele;
                    lbl_details.FontWeight = FontWeights.Bold;
                    lbl_details.FontStyle = FontStyles.Italic;
                    lbl_details.Foreground = Brushes.Blue;
                    lbl_details.HorizontalAlignment = HorizontalAlignment.Left;
                    lbl_details.Margin = new Thickness(3, 0, 0, 0);
                    lbl_details.VerticalAlignment = VerticalAlignment.Top;

                    cgrd.Children.Add(lbl_details);
                    DataGrid dtg1 = new DataGrid();
                    dtg1.ColumnWidth = 605;
                    dtg1.FontSize = 13;
                    Brush brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFC9E7F5"));
                    dtg1.AlternatingRowBackground = brush;
                    dtg1.AlternationCount = 2;
                    dtg1.Margin = new Thickness(10, 24, 10, 10);
                    dtg1.HorizontalAlignment = HorizontalAlignment.Stretch;
                    dtg1.VerticalAlignment = VerticalAlignment.Stretch;
                    dtg1.ItemsSource = listPerkategori;
                    dtg1.AutoGeneratingColumn += dtg1_AutoGeneratingColumn;
                    cgrd.Children.Add(dtg1);
                    mgrd.Children.Add(cgrd);
                    item.Content = mgrd;
                    main_tab.Items.Add(item);
                }
            }
        }
        public frm_view_ce(BatimentInCeModel bat)
        {
            InitializeComponent();
            string libelle = "SDE:"+bat.SdeId+" Batiment:"+bat.BatimentId;
            List<DataDetails> reponses = null;
            if (bat != null)
            {
                reponses = new List<DataDetails>();
                DataDetails d1 = new DataDetails("Nimewo Batiman", bat.BatimentId);
                reponses.Add(d1);
                DataDetails d2 = new DataDetails("SDE", bat.SdeId);
                reponses.Add(d2);
                DataDetails d3 = new DataDetails("Nimewo REC", bat.Rec);
                reponses.Add(d3);
                DataDetails d4 = new DataDetails("Nimewo RGPH", bat.Rgph);
                reponses.Add(d4);
                DataDetails d5 = new DataDetails("Adrès", bat.Adresse);
                reponses.Add(d5);
                DataDetails d6 = new DataDetails("Non Chèf Menaj la", bat.NomChefMenage);
                reponses.Add(d6);
            }
            TabItem item = new TabItem();
            item.HorizontalAlignment = HorizontalAlignment.Stretch;
            item.VerticalAlignment = VerticalAlignment.Stretch;
            item.VerticalContentAlignment = VerticalAlignment.Stretch;
            StackPanel stc = new StackPanel();
            stc.Height = 35;
            stc.Orientation = Orientation.Horizontal;
            stc.Margin = new Thickness(4, 0, 39, 0);

            Image img = new Image();
            BitmapImage bImg = new BitmapImage();
            bImg.BeginInit();
            bImg.UriSource = new Uri(@"/images/tb.png", UriKind.Relative);
            bImg.EndInit();
            img.Source = bImg;
            stc.Children.Add(img);

            TextBlock txt = new TextBlock();
            txt.FontSize = 11;
            txt.VerticalAlignment = VerticalAlignment.Center;
            txt.HorizontalAlignment = HorizontalAlignment.Center;
            txt.FontWeight = FontWeights.Bold;
            stc.Children.Add(txt);

            item.Header = stc;

            Grid mgrd = new Grid();
            mgrd.HorizontalAlignment = HorizontalAlignment.Stretch;
            mgrd.VerticalAlignment = VerticalAlignment.Stretch;

            Grid cgrd = new Grid();
            cgrd.HorizontalAlignment = HorizontalAlignment.Stretch;
            cgrd.VerticalAlignment = VerticalAlignment.Stretch;
            cgrd.Margin = new Thickness(10, 10, 0, 0);

            Label lbl_details = new Label();
            lbl_details.Content = libelle;
            lbl_details.FontWeight = FontWeights.Bold;
            lbl_details.FontStyle = FontStyles.Italic;
            lbl_details.Foreground = Brushes.Blue;
            lbl_details.HorizontalAlignment = HorizontalAlignment.Left;
            lbl_details.Margin = new Thickness(3, 0, 0, 0);
            lbl_details.VerticalAlignment = VerticalAlignment.Top;

            cgrd.Children.Add(lbl_details);
            DataGrid dtg1 = new DataGrid();
            dtg1.ColumnWidth = 605;
            dtg1.FontSize = 13;
            Brush brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFC9E7F5"));
            dtg1.AlternatingRowBackground = brush;
            dtg1.AlternationCount = 2;
            dtg1.Margin = new Thickness(10, 24, 10, 10);
            dtg1.HorizontalAlignment = HorizontalAlignment.Stretch;
            dtg1.VerticalAlignment = VerticalAlignment.Stretch;
            dtg1.ItemsSource = reponses;
            dtg1.AutoGeneratingColumn += dtg1_AutoGeneratingColumn;
            cgrd.Children.Add(dtg1);
            mgrd.Children.Add(cgrd);
            item.Content = mgrd;
            main_tab.Items.Add(item);
            
        }

#endregion
        void dtg1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Kategori")
            {
                e.Column.Visibility = Visibility.Hidden;
            }
            if (e.Column.Header.ToString() == "Kesyon" || e.Column.Header.ToString() == "Repons")
            {
                var col = e.Column as DataGridTextColumn;

                var style = new Style(typeof(TextBlock));
                style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
                style.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));
                col.ElementStyle = style;
            }
        }

       
    }
}
