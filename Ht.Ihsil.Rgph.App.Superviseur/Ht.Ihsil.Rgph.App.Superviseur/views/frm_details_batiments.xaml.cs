using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
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
    /// Logique d'interaction pour frm_details_batiments.xaml
    /// </summary>
    public partial class frm_details_batiments : UserControl
    {
        public frm_details_batiments(BatimentModel _batiment)
        {
            InitializeComponent();
            dtg_pag1.ItemsSource=DataDetailsMapper.MapToMobile<BatimentModel>(_batiment,_batiment.SdeId);
        }
    }
}
