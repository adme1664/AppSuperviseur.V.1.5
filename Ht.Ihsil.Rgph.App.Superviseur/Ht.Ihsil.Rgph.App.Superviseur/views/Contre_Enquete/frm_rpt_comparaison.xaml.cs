using DevExpress.Xpf.Grid;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
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
    /// Logique d'interaction pour frm_rpt_comparaison.xaml
    /// </summary>
    public partial class frm_rpt_comparaison : UserControl
    {
        public frm_rpt_comparaison(MenageCEModel model)
        {
            InitializeComponent();
            List<RapportComparaisonModel> rapportsProfilChefMenage = new List<RapportComparaisonModel>();
            lbl_details.Text = Utilities.getGeoInformation(model.SdeId);
            lbl_menage.Text = "Batiman:" + model.BatimentId + "/Lojman:" + model.LogeId + "/Menaj:" + model.MenageId;
            rapportsProfilChefMenage = Utilities.getRprtComparaisonChefMage(model);
            treeListComparaison.ItemsSource = rapportsProfilChefMenage;
        }
        public frm_rpt_comparaison(BatimentCEModel model)
        {
            InitializeComponent();
            List<RapportComparaisonModel> rapportsBatiment = new List<RapportComparaisonModel>();
            lbl_details.Text = Utilities.getGeoInformation(model.SdeId);
            lbl_menage.Text = "Batiman:" + model.BatimentId;
            rapportsBatiment = Utilities.getRprtComparaisonBatiment(model);
            treeListComparaisonBatiment.ItemsSource = rapportsBatiment;
        }
        public frm_rpt_comparaison(LogementCEModel model)
        {
            InitializeComponent();
            List<RapportComparaisonModel> rapportsBatiment = new List<RapportComparaisonModel>();
            lbl_details.Text = Utilities.getGeoInformation(model.SdeId);
            lbl_menage.Text = "Batiman: " + model.BatimentId+"/Lojman: "+model.LogeId;
            rapportsBatiment = Utilities.getRprtComparaisonLogement(model);
            treeListComparaisonBatiment.ItemsSource = rapportsBatiment;
        }

    }
}
