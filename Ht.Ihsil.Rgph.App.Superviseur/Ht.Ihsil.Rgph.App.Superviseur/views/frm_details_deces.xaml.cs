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
    /// Logique d'interaction pour frm_details_deces.xaml
    /// </summary>
    public partial class frm_details_deces : UserControl
    {
        private SqliteDataReaderService service;
        public frm_details_deces(MenageDetailsModel _deces)
        {
            InitializeComponent();
            service = new SqliteDataReaderService(Utilities.getConnectionString(Users.users.DatabasePath, _deces.SdeId));
            DecesModel d = service.getDecesDetail(Convert.ToInt32(_deces.Id));
            dtg_pag1.ItemsSource = DataDetailsMapper.MapToMobile<DecesModel>(d,d.SdeId);

        }
    }
}
