using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour frm_retours.xaml
    /// </summary>
    public partial class frm_retours : UserControl
    {
        ObservableCollection<RetourModel> ListOfRetours;
        ObservableCollection<SdeModel> Sdes;
        ConfigurationService service;
        string SdeId = "";
        public frm_retours()
        {
            InitializeComponent();
            ListOfRetours = new ObservableCollection<RetourModel>();
            Sdes = new ObservableCollection<SdeModel>();
            service = new ConfigurationService();
            //
            //Telechargement des sdes
            try
            {
                List<SdeModel> listOf = service.searchAllSdes();
                foreach (SdeModel sde in listOf)
                {
                    SdeModel sd = new SdeModel();
                    sd.SdeId = sde.SdeId;
                    Sdes.Add(sd);
                }
            }
            catch (Exception ex)
            {

            }
            lb_sdes.ItemsSource = Sdes;
        }

        private void lb_sdes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox ltb = e.OriginalSource as ListBox;
            SdeId = ltb.SelectedItems.OfType<SdeModel>().FirstOrDefault().SdeId;
            //
            //Chargement des retours a partir de la sde selectionneee
            ListOfRetours = new ObservableCollection<RetourModel>();
            List<RetourModel> retours = service.searchAllRetourBySde(SdeId);
            foreach (RetourModel model in retours)
            {
                ListOfRetours.Add(model);
            }
            grid.ItemsSource = ListOfRetours;
        }

        private void grid_AutoGeneratingColumn(object sender, DevExpress.Xpf.Grid.AutoGeneratingColumnEventArgs e)
        {
            if (e.Column.FieldName == "RetourId")
                e.Column.Visible = false;
            if (e.Column.FieldName == "SdeId")
                e.Column.Visible = false;
        }
    }
}
