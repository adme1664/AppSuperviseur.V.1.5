﻿using System;
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
using System.Windows.Shapes;
using DevExpress.Xpf.Ribbon;
using Ht.Ihsil.Rgph.App.Superviseur.views;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using Ht.Ihsil.Rgph.App.Superviseur.views.Contre_Enquete;
using Ht.Ihsil.Rgph.App.Superviseur.Models;


namespace Ht.Ihsil.Rgph.App.Superviseur
{
    /// <summary>
    /// Interaction logic for MainWindow1.xaml
    /// </summary>
    public partial class MainWindow1 : DXRibbonWindow
    {
        public MainWindow1()
        {
            InitializeComponent();
            if (Users.users.Profile == "7")
            {
                bbi_avances.IsVisible = false;
                bbi_agents.IsVisible = false;
                txt_connecteduser.Text = "" + Users.users.Nom + " " + Users.users.Prenom + " (Superviseur)";
            }
            if (Users.users.Profile == "6")
            {
                rpc_transfert.IsEnabled = false;
                rpc_rpt_personnel.IsEnabled = false;
                rpc_C_ENQUETE.IsEnabled = false;
                rpc_rapports.IsEnabled = false;
                rpc_sdes.IsEnabled = false;
                txt_connecteduser.Text = "" + Users.users.Nom + " " + Users.users.Prenom + " (ASTIC)";
            }
        }

        private void bbi_sdes_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            frm_view_sdes frm_sde = new frm_view_sdes();
            Utilities.showControl(frm_sde, main_grid);
        }

        private void bbi_agents_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            frm_view_agents frm_agents = new frm_view_agents();
            Utilities.showControl(frm_agents, main_grid);
        }

        private void bbi_visualisation_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            frm_visualisation frm_visualisation = new frm_visualisation();
            Utilities.showControl(frm_visualisation, main_grid);
        }

        private void bbi_transfert_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            main_grid_1.IsSplashScreenShown = true;
            Frm_view_transfert frm_transfert = new Frm_view_transfert(this);
            main_grid_1.Child = frm_transfert;
            main_grid.Children.Clear();
            main_grid.Children.Add(main_grid_1);
            main_grid_1.IsSplashScreenShown = false;
        }

        private void bbi_batiman_vid_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            main_grid_1.IsSplashScreenShown = true;
            frm_batiment_vide frm_ce = new frm_batiment_vide((int)Constant.TypeContrEnquete.BatimentVide);
            Utilities.showControl(frm_ce, main_grid);
            main_grid_1.IsSplashScreenShown = false;
        }

        private void bbi_lojman_vid_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            frm_batiment_vide frm_ce = new frm_batiment_vide((int)Constant.TypeContrEnquete.LogementInvididuelVide);
            Utilities.showControl(frm_ce, main_grid);
        }

        private void bbi_lojman_kolektif_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            frm_batiment_vide frm_ce = new frm_batiment_vide((int)Constant.TypeContrEnquete.LogementCollectif);
            Utilities.showControl(frm_ce, main_grid);
        }

        private void bbi_menaj_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            frm_batiment_vide frm_ce = new frm_batiment_vide((int)Constant.TypeContrEnquete.LogementIndividuelMenage);
            Utilities.showControl(frm_ce, main_grid);
        }

        private void rpc_sdes_Loaded(object sender, RoutedEventArgs e)
        {
            //frm_visualisation frm_visualisation = new frm_visualisation();
            //if (Users.users.Profile == "6")
            //    frm_visualisation.IsEnabled = false;
            //Utilities.showControl(frm_visualisation, main_grid);
        }

        private void rpc_rpt_tronc_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            frm_rapports rapports = new frm_rapports();
            Utilities.showControl(rapports, main_grid);
        }

        private void rpc_rpt_personnel_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            frm_rpt_personnel rpt = new frm_rpt_personnel();
            Utilities.showControl(rpt, main_grid);
        }

        private void bbi_avances_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            frm_configurations conf = new frm_configurations();
            Utilities.showControl(conf, main_grid);
        }

        private void rpc_rpt_deroulement_collecte_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            frm_rpt_dereoulement_entete rpt = new frm_rpt_dereoulement_entete();
            Utilities.showControl(rpt, main_grid);
        }

        private void bbi_verification_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            frm_view_verification verification = new frm_view_verification();
            Utilities.showControl(verification, main_grid);
            //frm_verification verification = new frm_verification();
            //Utilities.showControl(verification, main_grid);
        }

        private void bbi_retour_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            frm_retours retours = new frm_retours();
            Utilities.showControl(retours, main_grid);
        }
    }
}
