using Ht.Ihsi.Rgph.Logging.Logs;
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
    /// Logique d'interaction pour frm_correction.xaml
    /// </summary>
    public partial class frm_correction : UserControl
    {
        ISqliteDataWriter writer;
        Logger log;
        public frm_correction()
        {
            InitializeComponent();
            log = new Logger();
        }

        private void btn_questions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                writer = new SqliteDataWriter(true);
                //Suppresion des questions
                foreach (string q in Constant.DeleteQuestions())
                {
                    bool result = writer.deleteQuestion(q);
                    log.Info("Result:" + result);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
