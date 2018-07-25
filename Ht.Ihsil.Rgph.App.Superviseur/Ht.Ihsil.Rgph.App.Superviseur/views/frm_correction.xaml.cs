using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        ThreadStart ths = null;
        Thread t = null;
        public frm_correction()
        {
            InitializeComponent();
            log = new Logger();
        }

        private void btn_questions_Click(object sender, RoutedEventArgs e)
        {
            
            if (t != null)
            {
                if (t.IsAlive)
                {
                    t.Abort();
                }
            }
            
           
            try
            {

                ths = new ThreadStart(() => correctQuestionnaireCE());
                t = new Thread(ths);
                t.Start();
            }
            catch (Exception ex)
            {
                log.Info("ERREUR:<>===================<>" + ex.Message);
            }
            finally
            {
                decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = false));
                busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
            }
        }

        public void correctQuestionnaireCE()
        {
            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "Koreksyon an ap fet"));
            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = true));
            decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = true));
            try
            {
                writer = new SqliteDataWriter(true);
                //Suppresion des questions
                foreach (string q in Constant.DeleteQuestions())
                {
                    bool result = writer.deleteQuestion(q);
                    log.Info("Result:" + result);
                }
                //Suppression des questions reponses
                foreach (string qr in Constant.DeleteQuestionsReponses())
                {
                    bool result = writer.deleteQuestion(qr);
                    log.Info("Result:" + result);
                }
                //Suppression des reponses
                foreach (string qr in Constant.DeleteReponses())
                {
                    bool result = writer.deleteQuestion(qr);
                    log.Info("Result:" + result);
                }

                //Ajout des questions
                foreach (string qr in Constant.Questions())
                {
                    bool result = writer.insertQuestion(qr);
                    log.Info("Result:" + result);
                }
                //Ajout des questions reponses
                foreach (string qr in Constant.QuestionsReponsesLogements())
                {
                    bool result = writer.insertQuestionReponse(qr);
                    log.Info("Result:" + result);
                }
                foreach (string qr in Constant.QuestionsReponsesMenages())
                {
                    bool result = writer.insertQuestionReponse(qr);
                    log.Info("Result:" + result);
                }
                //Ajout des reponses
                foreach (string qr in Constant.Reponses())
                {
                    bool result = writer.insertReponses(qr);
                    log.Info("Result:" + result);
                }
                MessageBox.Show("Koreksyon an fini.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = false));
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, Constant.WINDOW_TITLE + "/[ERE]", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
