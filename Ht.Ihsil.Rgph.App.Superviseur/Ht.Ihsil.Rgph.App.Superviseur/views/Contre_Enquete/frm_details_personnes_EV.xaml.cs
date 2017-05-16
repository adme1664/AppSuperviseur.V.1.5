using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for frm_details_personnes_EV.xaml
    /// </summary>
    public partial class frm_details_personnes_EV : UserControl
    {
        ContreEnqueteService service = null;
        public frm_details_personnes_EV(MenageCEModel _men)
        {
            service = new ContreEnqueteService(Users.users.SupDatabasePath);
            InitializeComponent();
            //dtgInfo.ItemsSource = service.getInformationPopulationMenage(_men);
        }
    }
}
