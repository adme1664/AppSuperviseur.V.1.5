using Ht.Ihsi.Rgph.Utility.Utils;
using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using Ht.Ihsil.Rgph.App.Superviseur.viewModels.ContreEnquete;
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
    /// Logique d'interaction pour frm_save_CE.xaml
    /// </summary>
    public partial class frm_save_CE : UserControl
    {
        #region CONSTRUCTORS
        public frm_save_CE(ContreEnqueteModel model)
        {
            ContreEnqueteService service = new ContreEnqueteService(Users.users.SupDatabasePath);
            InitializeComponent();
            string nomSup = Users.users.Nom;
            ContreEnqueteModel model_ce = ModelMapper.MapToContreEnqueteModel(service.daoCE.getContreEnquete(Convert.ToInt32(model.BatimentId), model.SdeId));
            if (Utils.IsNotNull(model_ce))
            {
                model_ce.BatimentId = model.BatimentId.GetValueOrDefault();
                model_ce.Raison = model.Raison.GetValueOrDefault();
                model_ce.DateDebut = model.DateDebut;
                model_ce.DateFin = model.DateFin;
                model_ce.NomSuperviseur = model.NomSuperviseur;
                model_ce.SdeId = model.SdeId;
                model_ce.PrenomSuperviseur = model.PrenomSuperviseur;
                model_ce.ModelTirage = model.ModelTirage.GetValueOrDefault();
                model_ce.CodeDistrict = "000-879-90";
                //model_ce.Header = "BATIMAN " + model_ce.BatimentId + "/ SDE" + model_ce.SdeId;
                DataContext = model_ce;
            }
            else
            {
                model_ce.BatimentId = Convert.ToInt32(model_ce.BatimentId);
                model_ce.SdeId = model_ce.SdeId;
                model_ce.PrenomSuperviseur = Users.users.Prenom;
                model_ce.NomSuperviseur = Users.users.Nom;
                model_ce.CodeDistrict = "000-879-90";
            }

            cmb_modelTirage.ItemsSource = Constant.ChoixModelTirage();
            cmb_raison.ItemsSource = Constant.ChoixRaisonContreEnquete();
            cmb_statut.ItemsSource = Constant.ChoixStatutContreEnquete();

            foreach (ReponseModel rep in cmb_modelTirage.Items)
            {
                if (rep.CodeReponse == model_ce.ModelTirage.GetValueOrDefault().ToString())
                {
                    cmb_modelTirage.SelectedItem = rep;
                }
            }
            foreach (ReponseModel rep in cmb_raison.Items)
            {
                if (rep.CodeReponse == model_ce.Raison.GetValueOrDefault().ToString())
                {
                    cmb_raison.SelectedItem = rep;
                }
            }
            foreach (ReponseModel rep in cmb_statut.Items)
            {
                if (rep.CodeReponse == model_ce.Statut.GetValueOrDefault().ToString())
                {
                    cmb_statut.SelectedItem = rep;
                }
            }
        }
        #endregion
        private void btn_start_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmb_modelTirage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void cmb_raison_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmb_statut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
