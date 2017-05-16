using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsi.Rgph.Utility.Utils;
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
using DevExpress.Xpf.Charts;

namespace Ht.Ihsil.Rgph.App.Superviseur.views
{
    /// <summary>
    /// Logique d'interaction pour frm_details_sdes.xaml
    /// </summary>
    public partial class frm_details_sdes : UserControl
    {
        private SqliteDataReaderService service;
        Logger log;
        public frm_details_sdes(SdeModel _sde)
        {
            InitializeComponent();
            log = new Logger();
            service = new SqliteDataReaderService(Utilities.getConnectionString(Users.users.DatabasePath, _sde.SdeId));
            Tbl_Sde ss = service.getSdeDetails(_sde.SdeId);
            pieSeriesEndividi.Points.Add(new SeriesPoint("Gason", ss.TotalIndGRecense.GetValueOrDefault()));
            pieSeriesEndividi.Points.Add(new SeriesPoint("Fi", ss.TotalIndFRecense.GetValueOrDefault()));

            pieSeriesEmigre.Points.Add(new SeriesPoint("Gason", ss.TotalEmigreGRecense.GetValueOrDefault()));
            pieSeriesEmigre.Points.Add(new SeriesPoint("Fi", ss.TotalEmigreFRecense.GetValueOrDefault()));

            pieSeriesDeces.Points.Add(new SeriesPoint("Gason", ss.TotalDecesGRecense.GetValueOrDefault()));
            pieSeriesDeces.Points.Add(new SeriesPoint("Fi", ss.TotalDecesFRecense.GetValueOrDefault()));

            barSeriesDetails.Points.Add(new SeriesPoint("Batiman", ss.TotalBatRecense.GetValueOrDefault()));
            barSeriesDetails.Points.Add(new SeriesPoint("Lojman Envidiyel", ss.TotalLogeIRecense.GetValueOrDefault()));
            barSeriesDetails.Points.Add(new SeriesPoint("Lojman Kolektif", ss.TotalLogeCRecense.GetValueOrDefault()));
            barSeriesDetails.Points.Add(new SeriesPoint("Menaj", ss.TotalMenageRecense.GetValueOrDefault()));
            lbl_details_sde.Text = Utilities.getGeoInformation(_sde.SdeId);
            }
    }
}
