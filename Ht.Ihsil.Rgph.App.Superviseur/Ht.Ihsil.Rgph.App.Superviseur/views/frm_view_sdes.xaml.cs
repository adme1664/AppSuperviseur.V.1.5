using Ht.Ihsi.Rgph.DataAccess.Entities;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
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

namespace Ht.Ihsil.Rgph.App.Superviseur.views
{
    /// <summary>
    /// Interaction logic for frm_view_sdes.xaml
    /// </summary>
    public partial class frm_view_sdes : UserControl
    {
        ConfigurationService service;
        Logger log;
        public frm_view_sdes()
        {
            InitializeComponent();
            service = new ConfigurationService();
            log = new Logger();
            stck_bat_re.Children.Clear();
            stck_num_bat.Children.Clear();
            stck_percent.Children.Clear();
            stck_num_sde.Children.Clear();
            List<SdeModel> _sdes = service.searchAllSdes();
            foreach (SdeModel s in _sdes)
            {
                Label lSde = new Label();
                lSde.Content = s.SdeId;
                lSde.HorizontalAlignment = HorizontalAlignment.Left;
                FontFamily font = new System.Windows.Media.FontFamily("Candara");
                lSde.FontSize = 15;
                lSde.FontFamily = font;
                lSde.FontWeight = FontWeights.Bold;
                lSde.VerticalAlignment = VerticalAlignment.Top;
                stck_num_sde.Children.Add(lSde);

                Label lBC = new Label();
                lBC.Content = s.TotalBatCartographie.GetValueOrDefault().ToString();
                lBC.HorizontalAlignment = HorizontalAlignment.Left;
                lBC.VerticalAlignment = VerticalAlignment.Top;
                lBC.FontSize = 15;
                lBC.FontFamily = font;
                lBC.FontWeight = FontWeights.Bold;
                stck_num_bat.Children.Add(lBC);

                Label lBR = new Label();
                lBR.Content = s.TotalBatRecense.GetValueOrDefault().ToString();
                lBR.HorizontalAlignment = HorizontalAlignment.Left;
                lBR.VerticalAlignment = VerticalAlignment.Top;
                lBR.FontSize = 15;
                lBR.FontFamily = font;
                lBR.FontWeight = FontWeights.Bold;
                stck_bat_re.Children.Add(lBR);
                int percent = 0;
                if (s.TotalBatCartographie.GetValueOrDefault() != 0)
                {
                    percent = (s.TotalBatRecense.GetValueOrDefault() * 100) / s.TotalBatCartographie.GetValueOrDefault();
                }
                Label lPercent = new Label();
                lPercent.Content = percent.ToString() + " %";
                lPercent.HorizontalAlignment = HorizontalAlignment.Left;
                lPercent.VerticalAlignment = VerticalAlignment.Top;
                lPercent.FontSize = 15;
                lPercent.FontFamily = font;
                lPercent.FontWeight = FontWeights.Bold;
                stck_percent.Children.Add(lPercent);
            }
        }
    }
}
