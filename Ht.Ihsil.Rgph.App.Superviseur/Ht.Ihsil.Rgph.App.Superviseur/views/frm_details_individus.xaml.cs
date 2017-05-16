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

namespace Ht.Ihsil.Rgph.App.Superviseur.views
{
    /// <summary>
    /// Logique d'interaction pour frm_details_individus.xaml
    /// </summary>
    /// 
    
    public partial class frm_details_individus : UserControl
    {
        private SqliteDataReaderService service;
        public frm_details_individus(MenageDetailsModel _model)
        {
            InitializeComponent();
            service = new SqliteDataReaderService(Utilities.getConnectionString(Users.users.DatabasePath,_model.SdeId));
            IndividuModel _ind = service.getIndividuDetail(Convert.ToInt32(_model.Id));
            //dtg_page1.ItemsSource = DataDetailsMapper.MapToPage1(_ind);
            //dtg_page2.ItemsSource = DataDetailsMapper.MapToPage2(_ind);
            //dtg_page3.ItemsSource = DataDetailsMapper.MapToPage3(_ind);
            //dtg_page4.ItemsSource = DataDetailsMapper.MapToPage4(_ind);
            //dtg_page5.ItemsSource = DataDetailsMapper.MapToPage5(_ind);
            //dtg_page6.ItemsSource = DataDetailsMapper.MapToPage6(_ind);
            //dtg_page7.ItemsSource = DataDetailsMapper.MapToPage7(_ind);
            //dtg_page8.ItemsSource = DataDetailsMapper.MapToPage8(_ind);
            //dtg_page9.ItemsSource = DataDetailsMapper.MapToPage9(_ind);
        }
    }
}
