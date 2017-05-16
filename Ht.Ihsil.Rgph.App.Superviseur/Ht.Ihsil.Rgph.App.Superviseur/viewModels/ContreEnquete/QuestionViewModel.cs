using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsi.Rgph.Utility.Utils;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels.ContreEnquete
{
    public class QuestionViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<ReponseModel> _listOfB1;
        private ObservableCollection<ReponseModel> _listOfB2;
        private string _libelle;
        private ReponseModel _itemSelected;
        private string _header;
        private string _question2;
        private bool _visibledLabel;
        public QuestionsModel questionEnCours = null;
        TypeQuestion typeQuestion = null;
        private QuestionReponseService service;
        public List<QuestionsModel> listOfQuestion = null;
        int position = 0;
        Logger log;




        #region Properties
        public string Question2
        {
            get { return _question2; }
            set
            {
                if (this._question2 != value)
                {
                    this._question2 = value;
                    NotifyPropertyChanged(ref this._question2, value);
                }
            }
        }
       
        public bool VisibledLabel
        {
            get { return _visibledLabel; }
            set
            {
                if (this._visibledLabel != value)
                {
                    this._visibledLabel = value;
                    NotifyPropertyChanged(ref this._visibledLabel, value);
                }
            }
        }
        public string Libelle
        {
            get { return _libelle; }
            set
            {
                if (this._libelle != value)
                {
                    this._libelle = value;
                    NotifyPropertyChanged(ref this._libelle, value);
                }
            }
        }
        public string Header
        {
            get { return _header; }
            set
            {
                if (this._header != value)
                {
                    this._header = value;
                    NotifyPropertyChanged(ref this._header, value);
                }
            }
        }
      
        public ObservableCollection<ReponseModel> ListOfB1
        {
            get { return _listOfB1; }
            set
            {
                if (_listOfB1 != value)
                {
                    _listOfB1 = value;
                    NotifyPropertyChanged(ref this._listOfB1, value);
                }
            }
        }

        public ObservableCollection<ReponseModel> ListOfB2
        {
            get { return _listOfB2; }
            set
            {
                if (_listOfB2 != value)
                {
                    _listOfB2 = value;
                    NotifyPropertyChanged(ref this._listOfB2, value);
                }
            }
        }
       

        #endregion

        public QuestionViewModel()
        {

        }
        public QuestionViewModel(string typeQuestion, bool isChefMenage)
        {
            service = new QuestionReponseService();
            log = new Logger();
            setQuestion(typeQuestion, isChefMenage);
        }
        public QuestionViewModel(string typeQuestion)
        {
            service = new QuestionReponseService();
            log = new Logger();
            setQuestion(typeQuestion);
        }
        public void setQuestion(string typeQuestion, bool isChefMenage)
        {
            ObservableCollection<ReponseModel> list = new ObservableCollection<ReponseModel>();
           listOfQuestion = service.searchQuestion(typeQuestion);
            if (Utils.IsNotNull(listOfQuestion))
            {
                QuestionsModel q1 = new QuestionsModel();
                if (typeQuestion == "Individu")
                {
                    if (isChefMenage == true)
                    {
                        q1 = listOfQuestion.Find(q => q.CodeQuestion == "QP4");
                
                    }
                    else
                    {
                        q1 = listOfQuestion.Find(q => q.CodeQuestion == "QP2.1");
                    }
                    
                }

                else
                {
                    q1 = listOfQuestion.ElementAt(position);
                }
                CategorieQuestionModel cat = service.getCategorieQuestion(q1.CodeCategorie);
                if (Utils.IsNotNull(cat))
                {
                    Header = cat.CategorieQuestion;
                }
                questionEnCours = q1;
                Libelle = q1.Libelle;
                VisibledLabel = false;
                if (questionEnCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Choix)
                {
                    
                    List<QuestionReponseModel> listQR = service.searchQuestionReponse(q1.CodeQuestion);
                    if (Utils.IsNotNull(listQR))
                    {

                        foreach (QuestionReponseModel qr in listQR)
                        {
                            ReponseModel rep = service.getReponse(qr.CodeUniqueReponse);
                            list.Add(rep);
                        }
                    }
                    ListOfB1 = list;
                }
                else
                {

                }
                
                position++;

            }
        }

        public void setQuestion(string typeQuestion)
        {
            ObservableCollection<ReponseModel> list = new ObservableCollection<ReponseModel>();
            listOfQuestion = service.searchQuestion(typeQuestion);
            if (Utils.IsNotNull(listOfQuestion))
            {
                QuestionsModel q1 = new QuestionsModel();
                if (typeQuestion == "Individu")
                {
                   q1 = listOfQuestion.Find(q => q.CodeQuestion == "QP2A");
                   
                }
                if (typeQuestion == "Logement")
                {
                    q1 = listOfQuestion.Find(q => q.CodeQuestion == "LJ"); 
                }
                if (typeQuestion == "Menage")
                {
                    q1 = listOfQuestion.Find(q => q.CodeQuestion == "M2"); 
                }
                if (typeQuestion == "Deces")
                {
                    q1 = listOfQuestion.Find(q => q.CodeQuestion == "D1");
                }
                if (typeQuestion == "Batiment")
                {
                    q1 = listOfQuestion.Find(q => q.CodeQuestion == "B1");
                }
                CategorieQuestionModel cat = service.getCategorieQuestion(q1.CodeCategorie);
                if (Utils.IsNotNull(cat))
                {
                    Header = cat.CategorieQuestion;
                }
                questionEnCours = q1;
                Libelle = q1.Libelle;
                VisibledLabel = false;
                if (questionEnCours.TypeQuestion.GetValueOrDefault() == (int)Constant.TypeQuestion.Choix)
                {

                    List<QuestionReponseModel> listQR = service.searchQuestionReponse(q1.CodeQuestion);
                    if (Utils.IsNotNull(listQR))
                    {

                        foreach (QuestionReponseModel qr in listQR)
                        {
                            ReponseModel rep = service.getReponse(qr.CodeUniqueReponse);
                            list.Add(rep);
                        }
                    }
                    ListOfB1 = list;
                }
                else
                {

                }

                position++;

            }
        }
        public string setSpecificQuestion(ObservableCollection<ReponseModel> listOfReponse, QuestionsModel questionEncours)
        {

            QuestionsModel q1 = service.getQuestion(questionEncours.QSuivant);
            ObservableCollection<ReponseModel> lReponse = new ObservableCollection<ReponseModel>();
            List<QuestionReponseModel> listQR = service.searchQuestionReponse(q1.QSuivant);
            if (Utils.IsNotNull(listQR))
            {

                foreach (QuestionReponseModel qr in listQR)
                {
                    ReponseModel rep = service.getReponse(qr.CodeUniqueReponse);
                    lReponse.Add(rep);
                }

                listOfReponse = lReponse;

            }
            this.questionEnCours = q1;
            return q1.Libelle;
        }
    }
}
