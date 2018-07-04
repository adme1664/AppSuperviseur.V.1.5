using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ht.Ihsi.Rgph.DataAccess.Entities;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Configuration;
using Ht.Ihsi.Rgph.Logging.Logs;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity;
using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;

namespace Ht.Ihsi.Rgph.DataAccess.Repositories
{
    public class MainRepository : IDisposable
    {

        private GenericDatabaseContext _genericContext;
        private RgphContext context;
        private bool disposed = false;
        private GenericSupDatabaseContext supContext;

        #region CONSTRUCTOR
        /// <summary>
        /// Mainrepository for the SQLITE Database
        /// </summary>
        /// <param name="connectionString"></param>
        public MainRepository(string connectionString)
        {
            _genericContext = new GenericDatabaseContext(connectionString);
        }
        public MainRepository()
        {
            context = new RgphContext();
        }
        public MainRepository(string connectionString, bool typeDb)
        {
            supContext = new GenericSupDatabaseContext(connectionString);
        }

        #endregion

        #region SQLITE SUPERVISEUR TABLE VARIABLES
        private GenericRepository<Tbl_RapportPersonnel> rapportPersonnelRepository;
        private GenericRepository<Tbl_Utilisateur> utilisateurRepository;
        private GenericRepository<Tbl_Agent> agentRepository;
        private GenericRepository<Tbl_Materiels> materielsRepository;
        private GenericRepository<Tbl_Sde> sdeRepository;
        private GenericRepository<Tbl_ContreEnquete> contreEnqueteRepository;
        private GenericRepository<Tbl_Questions> questionRepository;
        private GenericRepository<Tbl_Questions_Reponses> questionReponseRepository;
        private GenericRepository<Tbl_CategorieQuestion> categorieRepository;
        private GenericRepository<Tbl_Reponses> reponsesRepository;
        private GenericRepository<Tbl_BatimentCE> batimentCERepository;
        private GenericRepository<Tbl_LogementCE> logementCERepository;
        private GenericRepository<Tbl_MenageCE> menageRepository;
        private GenericRepository<Tbl_DecesCE> decesRepository;
        private GenericRepository<Tbl_EmigreCE> emigreRepository;
        private GenericRepository<Tbl_IndividusCE> individuRepository;
        private GenericRepository<Tbl_Departement> departementRepository;
        private GenericRepository<Tbl_Commune> communeRepository;
        private GenericRepository<Tbl_VilleQuartierSectionCommunale> vqseRepository;
        private GenericRepository<Tbl_Pays> paysRepository;
        private GenericRepository<Tbl_Utilisation> utilisationRepository;
        private GenericRepository<Tbl_RprtDeroulement> rapportDeroulementRepository;
        private GenericRepository<Tbl_DetailsRapport> rapportDetailsDeroulementRepository;
        private GenericRepository<Tbl_Retour> retourRepository;
        private GenericRepository<Tbl_Probleme> problemeRepository;
        #endregion

        #region PROPERTIES SQLITE SUPERVISEUR DATABASES
        public GenericRepository<Tbl_Probleme> ProblemeRepository
        {
            get
            {
                if (this.problemeRepository == null)
                {
                    this.problemeRepository = new GenericRepository<Tbl_Probleme>(this.supContext);
                }
                return this.problemeRepository;
            }
        }
        public GenericRepository<Tbl_EmigreCE> EmigreRepository
        {
            get
            {
                if (this.emigreRepository == null)
                {
                    this.emigreRepository = new GenericRepository<Tbl_EmigreCE>(this.supContext);
                }
                return this.emigreRepository;
            }
        }
        public GenericRepository<Tbl_Retour> RetourRepository
        {
            get
            {
                if (this.retourRepository == null)
                {
                    this.retourRepository = new GenericRepository<Tbl_Retour>(this.supContext);
                }
                return this.retourRepository;
            }
        }

        public GenericRepository<Tbl_RprtDeroulement> RapportDeroulementRepository
        {
            get
            {
                if (this.rapportDeroulementRepository == null)
                {
                    this.rapportDeroulementRepository = new GenericRepository<Tbl_RprtDeroulement>(this.supContext);
                }
                return this.rapportDeroulementRepository;
            }
        }

        public GenericRepository<Tbl_DetailsRapport> RapportDetailsDeroulementRepository
        {
            get
            {
                if (this.rapportDetailsDeroulementRepository == null)
                {
                    this.rapportDetailsDeroulementRepository = new GenericRepository<Tbl_DetailsRapport>(this.supContext);
                }
                return this.rapportDetailsDeroulementRepository;
            }
        }

        public GenericRepository<Tbl_RapportPersonnel> RapportPersonnelRepository
        {
            get
            {
                if (this.rapportPersonnelRepository == null)
                {
                    this.rapportPersonnelRepository = new GenericRepository<Tbl_RapportPersonnel>(this.supContext);
                }
                return this.rapportPersonnelRepository;
            }
        }
        public GenericRepository<Tbl_Utilisation> UtilisationRepository
        {
            get
            {
                if (this.utilisationRepository == null)
                {
                    this.utilisationRepository = new GenericRepository<Tbl_Utilisation>(this.supContext);
                }
                return this.utilisationRepository;
            }
        }
        public GenericRepository<Tbl_Materiels> MaterielsRepository
        {
            get
            {
                if (this.materielsRepository == null)
                {
                    this.materielsRepository = new GenericRepository<Tbl_Materiels>(this.supContext);
                }
                return this.materielsRepository;
            }
        }
        public GenericRepository<Tbl_Pays> PaysRepository
        {
            get
            {
                if (this.paysRepository == null)
                {
                    this.paysRepository = new GenericRepository<Tbl_Pays>(this.supContext);
                }
                return this.paysRepository;
            }
        }
        public GenericRepository<Tbl_VilleQuartierSectionCommunale> VqseRepository
        {
            get
            {
                if (this.vqseRepository == null)
                {
                    this.vqseRepository = new GenericRepository<Tbl_VilleQuartierSectionCommunale>(this.supContext);
                }
                return this.vqseRepository;
            }
        }
        public GenericRepository<Tbl_Commune> CommuneRepository
        {
            get
            {
                if (this.communeRepository == null)
                {
                    this.communeRepository = new GenericRepository<Tbl_Commune>(this.supContext);
                }
                return this.communeRepository;
            }
        }
        public GenericRepository<Tbl_Departement> DepartementRepository
        {
            get
            {
                if (this.departementRepository == null)
                {
                    this.departementRepository = new GenericRepository<Tbl_Departement>(this.supContext);
                }
                return this.departementRepository;
            }
        }
        public GenericRepository<Tbl_IndividusCE> IndividuRepository
        {
            get
            {
                if (this.individuRepository == null)
                {
                    this.individuRepository = new GenericRepository<Tbl_IndividusCE>(this.supContext);
                }
                return this.individuRepository;
            }
        }
        public GenericRepository<Tbl_DecesCE> DecesRepository
        {
            get
            {
                if (this.decesRepository == null)
                {
                    this.decesRepository = new GenericRepository<Tbl_DecesCE>(this.supContext);
                }
                return this.decesRepository;
            }
        }
        public GenericRepository<Tbl_MenageCE> MenageRepository
        {
            get
            {
                if (this.menageRepository == null)
                {
                    this.menageRepository = new GenericRepository<Tbl_MenageCE>(this.supContext);
                }
                return this.menageRepository;
            }
        }
        public GenericRepository<Tbl_LogementCE> LogementCERepository
        {
            get
            {
                if (this.logementCERepository == null)
                {
                    this.logementCERepository = new GenericRepository<Tbl_LogementCE>(this.supContext);
                }
                return this.logementCERepository;
            }
        }
        public GenericRepository<Tbl_BatimentCE> BatimentCERepository
        {
            get
            {
                if (this.batimentCERepository == null)
                {
                    this.batimentCERepository = new GenericRepository<Tbl_BatimentCE>(this.supContext);
                }
                return this.batimentCERepository;
            }
        }

        public GenericRepository<Tbl_Sde> SdeRepository
        {
            get
            {
                if (this.sdeRepository == null)
                {
                    this.sdeRepository = new GenericRepository<Tbl_Sde>(this.supContext);
                }
                return this.sdeRepository;
            }
        }

        public GenericRepository<Tbl_Reponses> ReponseRepository
        {
            get
            {
                if (this.reponsesRepository == null)
                {
                    this.reponsesRepository = new GenericRepository<Tbl_Reponses>(this.supContext);
                }
                return this.reponsesRepository;
            }
        }

        public GenericRepository<Tbl_Questions_Reponses> QuestionReponseRepository
        {
            get
            {
                if (this.questionReponseRepository == null)
                {
                    this.questionReponseRepository = new GenericRepository<Tbl_Questions_Reponses>(this.supContext);
                }
                return this.questionReponseRepository;
            }
        }

        public GenericRepository<Tbl_Questions> QuestionRepository
        {
            get
            {
                if (questionRepository == null)
                {
                    this.questionRepository = new GenericRepository<Tbl_Questions>(this.supContext);
                }
                return this.questionRepository;
            }

        }

        public GenericRepository<Tbl_CategorieQuestion> CategorieRepository
        {
            get
            {
                if (this.categorieRepository == null)
                {
                    this.categorieRepository = new GenericRepository<Tbl_CategorieQuestion>(this.supContext);
                }
                return this.categorieRepository;
            }
        }

        public GenericRepository<Tbl_Agent> AgentRepository
        {
            get
            {
                if (this.agentRepository == null)
                {
                    this.agentRepository = new GenericRepository<Tbl_Agent>(this.supContext);
                }
                return this.agentRepository;
            }

        }

        public GenericRepository<Tbl_Utilisateur> UtilisateurRepository
        {
            get
            {
                if (utilisateurRepository == null)
                {
                    this.utilisateurRepository = new GenericRepository<Tbl_Utilisateur>(this.supContext);
                }
                return this.utilisateurRepository;

            }

        }
        public GenericRepository<Tbl_ContreEnquete> ContreEnqueteRepository
        {
            get
            {
                if (contreEnqueteRepository == null)
                {
                    this.contreEnqueteRepository = new GenericRepository<Tbl_ContreEnquete>(this.supContext);
                }
                return this.contreEnqueteRepository;

            }

        }
        #endregion

        #region SQLITE TABLE VARIABLE
        private GenericRepository<tbl_batiment> _mBatimentRepository;
        private GenericRepository<tbl_deces> _mDecesRepository;

        private GenericRepository<tbl_departement> _mDepartementRepository;
        private GenericRepository<tbl_commune> _mCommuneRepository;
        private GenericRepository<tbl_vqse> _mVqseRepository;
        private GenericRepository<tbl_pays> _mPaysRepository;
        private GenericRepository<tbl_domaine_etude> _mDomaineEtudeRepository;

        private GenericRepository<tbl_emigre> _mEmigreRepository;
        private GenericRepository<tbl_individu> _mIndividuRepository;
        private GenericRepository<tbl_logement> _mLogementRepository;
        private GenericRepository<tbl_menage> _mMenageRepository;
        private GenericRepository<tbl_question> _mQuestionRepository;
        private GenericRepository<tbl_question_reponse> _mQuestionReponseRepository;
        private GenericRepository<tbl_question_module> _mQuestionModuleRepository;
        private GenericRepository<tbl_categorie_question> _mCategorieQuestionRepository;
        private GenericRepository<tbl_personnel> _mPersonnelRepository;
        private GenericRepository<tbl_rapportrar> _mRapportARRepository;
        private GenericRepository<tbl_rapportfinal> _mRapportFinalRepository;
        #endregion

        #region PROPERTIES MOBILE

        public GenericRepository<tbl_rapportrar> MRapportARRepository
        {
            get
            {
                if (this._mRapportARRepository == null)
                {
                    _mRapportARRepository = new GenericRepository<tbl_rapportrar>(this._genericContext);
                }
                return this._mRapportARRepository;
            }

        }
        public GenericRepository<tbl_rapportfinal> MRapportFinalRepository
        {
            get
            {
                if (this._mRapportFinalRepository == null)
                {
                    _mRapportFinalRepository = new GenericRepository<tbl_rapportfinal>(this._genericContext);
                }
                return this._mRapportFinalRepository;
            }

        }
        public GenericRepository<tbl_departement> MDepartementRepository
        {
            get
            {
                if (this._mDepartementRepository == null)
                {
                    _mDepartementRepository = new GenericRepository<tbl_departement>(this._genericContext);
                }
                return this._mDepartementRepository;
            }

        }
        public GenericRepository<tbl_commune> MCommuneRepository
        {
            get
            {
                if (this._mCommuneRepository == null)
                {
                    _mCommuneRepository = new GenericRepository<tbl_commune>(this._genericContext);
                }
                return this._mCommuneRepository;
            }

        }
        public GenericRepository<tbl_vqse> MVqseRepository
        {
            get
            {
                if (this._mVqseRepository == null)
                {
                    _mVqseRepository = new GenericRepository<tbl_vqse>(this._genericContext);
                }
                return this._mVqseRepository;
            }

        }
        public GenericRepository<tbl_pays> MPayspository
        {
            get
            {
                if (this._mPaysRepository == null)
                {
                    _mPaysRepository = new GenericRepository<tbl_pays>(this._genericContext);
                }
                return this._mPaysRepository;
            }

        }
        public GenericRepository<tbl_domaine_etude> MDomaineEtudeRepository
        {
            get
            {
                if (this._mDomaineEtudeRepository == null)
                {
                    _mDomaineEtudeRepository = new GenericRepository<tbl_domaine_etude>(this._genericContext);
                }
                return this._mDomaineEtudeRepository;
            }

        }
        public GenericRepository<tbl_personnel> MPersonnelRepository
        {
            get
            {
                if (this._mPersonnelRepository == null)
                {
                    _mPersonnelRepository = new GenericRepository<tbl_personnel>(this._genericContext);
                }
                return this._mPersonnelRepository;
            }

        }

        public GenericRepository<tbl_question> MQuestionRepository
        {
            get
            {
                if (this._mQuestionRepository == null)
                {
                    _mQuestionRepository = new GenericRepository<tbl_question>(this._genericContext);
                }
                return this._mQuestionRepository;
            }

        }
        public GenericRepository<tbl_categorie_question> MCategorieQuestionRepository
        {
            get
            {
                if (this._mCategorieQuestionRepository == null)
                {
                    _mCategorieQuestionRepository = new GenericRepository<tbl_categorie_question>(this._genericContext);
                }
                return this._mCategorieQuestionRepository;
            }

        }
        public GenericRepository<tbl_question_module> MQuestionModuleRepository
        {
            get
            {
                if (this._mQuestionModuleRepository == null)
                {
                    _mQuestionModuleRepository = new GenericRepository<tbl_question_module>(this._genericContext);
                }
                return this._mQuestionModuleRepository;
            }

        }
        public GenericRepository<tbl_question_reponse> MQuestionReponseRepository
        {
            get
            {
                if (this._mQuestionReponseRepository == null)
                {
                    _mQuestionReponseRepository = new GenericRepository<tbl_question_reponse>(this._genericContext);
                }
                return this._mQuestionReponseRepository;
            }

        }
        public GenericRepository<tbl_batiment> MBatimentRepository
        {
            get
            {
                if (this._mBatimentRepository == null)
                {
                    _mBatimentRepository = new GenericRepository<tbl_batiment>(this._genericContext);
                }
                return this._mBatimentRepository;
            }

        }
        public GenericRepository<tbl_menage> MMenageRepository
        {
            get
            {
                if (this._mMenageRepository == null)
                {
                    _mMenageRepository = new GenericRepository<tbl_menage>(this._genericContext);
                }
                return this._mMenageRepository;
            }

        }
        public GenericRepository<tbl_logement> MLogementRepository
        {
            get
            {
                if (this._mLogementRepository == null)
                {
                    _mLogementRepository = new GenericRepository<tbl_logement>(this._genericContext);
                }
                return this._mLogementRepository;
            }
        }
        public GenericRepository<tbl_individu> MIndividuRepository
        {
            get
            {
                if (this._mIndividuRepository == null)
                {
                    _mIndividuRepository = new GenericRepository<tbl_individu>(this._genericContext);
                }
                return this._mIndividuRepository;
            }

        }
        public GenericRepository<tbl_emigre> MEmigreRepository
        {
            get
            {
                if (this._mEmigreRepository == null)
                {
                    _mEmigreRepository = new GenericRepository<tbl_emigre>(this._genericContext);
                }
                return this._mEmigreRepository;
            }

        }

        public GenericRepository<tbl_deces> MDecesRepository
        {
            get
            {
                if (this._mDecesRepository == null)
                {
                    _mDecesRepository = new GenericRepository<tbl_deces>(this._genericContext);
                }
                return this._mDecesRepository;
            }
        }
        #endregion

        #region CONTEXT
        public RgphContext Context
        {
            get
            {
                return this.context;
            }
        }

        public GenericDatabaseContext GenericContext
        {
            get { return this._genericContext; }
        }

        public GenericSupDatabaseContext SupDatabaseContext
        {
            get { return this.supContext; }
        }
        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    supContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Save()
        {
            Logger log = new Logger();
            try
            {
                supContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        log.Info("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }

            }
        }
        public void SaveGB()
        {
            Logger log = new Logger();
            try
            {
                _genericContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        log.Info("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }

            }
        }

    }
}
