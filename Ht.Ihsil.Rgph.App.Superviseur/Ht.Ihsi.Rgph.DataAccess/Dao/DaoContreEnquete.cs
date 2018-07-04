using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.DataAccess.Exceptions;
using Ht.Ihsi.Rgph.DataAccess.Repositories;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsi.Rgph.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsi.Rgph.DataAccess.Dao
{
    public class DaoContreEnquete : IDaoContreEnquete
    {
        #region DECLARATIONS
        private MainRepository repository;
        private Logger log;
        string connectionString = "";

        #endregion

        #region CONSTRUCTOR
        public DaoContreEnquete()
        {
            
            repository = new MainRepository();
            log = new Logger();
        }
        public DaoContreEnquete(string connectionString)
        {
            repository = new MainRepository(connectionString, true);
            this.connectionString = connectionString;
            log = new Logger();
        }
        #endregion

        public MainRepository getRepository()
        {
            if (repository == null)
            {
                repository = new MainRepository(connectionString, true);
            }
            return repository;
        }

        #region CONTRE-ENQUETE
        public Tbl_ContreEnquete saveContreEnquete(Tbl_ContreEnquete ce)
        {
            try
            {
                Tbl_ContreEnquete cToSave=repository.ContreEnqueteRepository.Insert(ce);
                repository.Save();
                return cToSave;
            }
            catch (Exception ex)
            {
                log.Info("<>==========================DAO/saveContreEnquete:" + ex.Message);
                throw new MessageException("Erreur lors de la sauvegarde. Error: " + ex.Message);
            }
        }

        public Tbl_ContreEnquete getContreEnquete(int batimentId, string sdeId)
        {
            return repository.ContreEnqueteRepository.Find(s => s.SdeId == sdeId && s.BatimentId == batimentId).FirstOrDefault();
        }
        public Tbl_ContreEnquete getContreEnquete(long id, string sdeId)
        {
            return repository.ContreEnqueteRepository.Find(s => s.SdeId == sdeId && s.ContreEnqueteId ==id).FirstOrDefault();
        }

        public List<Tbl_ContreEnquete> searchContreEnquete(string sdeId)
        {
            return repository.ContreEnqueteRepository.Find(s => s.SdeId == sdeId).ToList();
        }

        public List<Tbl_ContreEnquete> searchContreEnquete(string sdeId, int typeContreEnquete)
        {
            return repository.ContreEnqueteRepository.Find(s => s.SdeId == sdeId && s.TypeContreEnquete==typeContreEnquete).ToList();
        }

        public bool updateContreEnquete(Tbl_ContreEnquete ce)
        {
            try
            {
                Tbl_ContreEnquete c = getContreEnquete(Convert.ToInt32(ce.BatimentId), ce.SdeId);
                if (Utils.IsNotNull(c))
                {
                    c.Raison = ce.Raison; c.DateFin = ce.DateFin;
                    c.DateDebut = ce.DateDebut;
                    c.DateFin = ce.DateFin;
                    c.Raison = ce.Raison.GetValueOrDefault();
                    c.Statut = ce.Statut.GetValueOrDefault();
                    repository.ContreEnqueteRepository.Update(c);
                    repository.Save();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new MessageException("Erreur lors de la sauvegarde. Error: " + ex.Message);
            }
            return false;
        }
        #endregion

        #region BATIMENT
        public bool saveBatimentCE(Tbl_BatimentCE bat)
        {
            try
            {
                repository.BatimentCERepository.Insert(bat);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("<>================Exception:saveBatimentCE" + ex.Message);
                log.Info("<>====Inner Exception:" + ex.InnerException);
            }
            return false;
        }
        public bool updateBatimentCE(Tbl_BatimentCE bat)
        {
            Tbl_BatimentCE batUpdate = getBatiment(bat.BatimentId.GetValueOrDefault(),bat.SdeId);
            if (batUpdate != null)
            {
                
                batUpdate.SdeId = bat.SdeId;
                batUpdate.Qb1Etat = bat.Qb1Etat;
                batUpdate.Qb2Type = bat.Qb2Type;
                batUpdate.DureeSaisie = bat.DureeSaisie;
                batUpdate.DateDebutCollecte = bat.DateDebutCollecte;
                batUpdate.DateFinCollecte = bat.DateFinCollecte;
                batUpdate.Qb4MateriauMur = bat.Qb4MateriauMur;
                batUpdate.Qb6StatutOccupation = bat.Qb6StatutOccupation;
                batUpdate.Qb5MateriauToit = bat.Qb5MateriauToit;
                batUpdate.Qb7Utilisation1 = bat.Qb7Utilisation1;
                batUpdate.Qb7Utilisation2 = bat.Qb7Utilisation2;
                batUpdate.Qb3NombreEtage = bat.Qb3NombreEtage;
                batUpdate.Qadresse = bat.Qadresse;
                batUpdate.Qhabitation = bat.Qhabitation;
                batUpdate.Qrec = bat.Qrec;
                batUpdate.Qrgph = bat.Qrgph;
                batUpdate.Qb8NbreLogeCollectif = bat.Qb8NbreLogeCollectif;
                batUpdate.Qb8NbreLogeIndividuel = bat.Qb8NbreLogeIndividuel;
                batUpdate.IsContreEnqueteMade = bat.IsContreEnqueteMade;
                batUpdate.Qb6StatutOccupation = bat.Qb6StatutOccupation;
                batUpdate.IsValidated = bat.IsValidated;
                batUpdate.Statut = bat.Statut;
                repository.BatimentCERepository.Update(batUpdate);
                repository.Save();
                repository.SupDatabaseContext.Entry(batUpdate).Reload();
                return true;
            }
            return false;
        }
        public Tbl_BatimentCE getBatiment(long batId,string sdeId)
        {
            return repository.BatimentCERepository.Find(b=>b.BatimentId==batId && b.SdeId==sdeId).FirstOrDefault();
        }
        public List<Tbl_BatimentCE> searchBatimentWithLogement(string sdeId)
        {
            List<Tbl_BatimentCE> listOfBat = repository.BatimentCERepository.Find(b => b.SdeId == sdeId).ToList();
            if (Utils.IsNotNull(listOfBat))
            {
                List<Tbl_BatimentCE> listOfB = new List<Tbl_BatimentCE>();
                foreach (Tbl_BatimentCE batiment in listOfBat)
                {
                    
                }
            }
            return repository.BatimentCERepository.Find(b => b.SdeId == sdeId && b.Qb8NbreLogeCollectif.GetValueOrDefault() == 1).ToList();
        }
        #endregion

        #region LOGEMENT

        public List<Tbl_LogementCE> searchLogByBatimentAndTypeLog(long batimentId, string sdeId, long categorieLogement)
        {
            try
            {
                return repository.LogementCERepository.Find(b => b.BatimentId == batimentId && b.SdeId == sdeId && b.QlCategLogement == categorieLogement).ToList();
            }
            catch (Exception)
            {

            }
            return new List<Tbl_LogementCE>();
        }
        public bool saveLogement(Tbl_LogementCE _log)
        {
            try
            {
                repository.LogementCERepository.Insert(_log);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("Error:/saveLogement=>" + ex.Message);
            }
            return false;
        }
        public List<Tbl_LogementCE> searchLogementbyBatiment(int batimentId, string sdeId)
        {
            return repository.LogementCERepository.Find(l => l.BatimentId == batimentId && l.SdeId == sdeId).ToList();
        }
        public List<Tbl_LogementCE> searchLogement(string sdeId)
        {
            throw new NotImplementedException();
        }

        public bool isLogementExistInBatiment(int batimentId, string sdeId)
        {
            List<Tbl_LogementCE> listOfBat = repository.LogementCERepository.Find(l => l.BatimentId == batimentId && l.SdeId == sdeId).ToList();
            if (listOfBat.Count() != 0)
            {
                return true;
            }
            return false;
        }
        public bool isLogementVideExistInBat(int batimentId, string sdeId)
        {
            List<Tbl_LogementCE> listOfBat = repository.LogementCERepository.Find(l => l.BatimentId == batimentId && l.SdeId == sdeId && l.Qlin2StatutOccupation == 3).ToList();
            if (listOfBat.Count() != 0)
            {
                return true;
            }
            return false;
        }
        public bool isLogementCollectifExistInBatiment(int batimentId, string sdeId)
        {
            List<Tbl_LogementCE> listOfBat = repository.LogementCERepository.Find(l => l.BatimentId == batimentId && l.SdeId == sdeId && l.QlCategLogement==2).ToList();
            if (listOfBat.Count() != 0)
            {
                return true;
            }
            return false;
        }
        public bool updateLogement(Tbl_LogementCE log)
        {
           try
           {
               Tbl_LogementCE logement = repository.LogementCERepository.Find(l => l.BatimentId == log.BatimentId && l.LogeId == log.LogeId && l.SdeId == log.SdeId).FirstOrDefault();
               if (Utils.IsNotNull(logement))
               {
                   logement.Qlin6NombrePiece = log.Qlin6NombrePiece;
                   logement.QlcTypeLogement = log.QlcTypeLogement;
                   logement.QlCategLogement = log.QlCategLogement;
                   logement.Qlc2bTotalFille = log.Qlc2bTotalFille;
                   logement.Qllc2bTotalGarcon = log.Qllc2bTotalGarcon;
                   logement.Qlin2StatutOccupation = log.Qlin2StatutOccupation;
                   logement.Qlin9NbreTotalMenage = log.Qlin9NbreTotalMenage;
                   logement.Qlin8NbreIndividuDepense = log.Qlin8NbreIndividuDepense;
                   logement.Qlin5MateriauSol = log.Qlin5MateriauSol;
                   logement.Qlin4TypeLogement = log.Qlin4TypeLogement;
                   logement.Qlin7NbreChambreACoucher = log.Qlin7NbreChambreACoucher;
                   logement.DureeSaisie = log.DureeSaisie;
                   logement.IsContreEnqueteMade = log.IsContreEnqueteMade;
                   logement.IsValidated =log.IsValidated;
                   repository.LogementCERepository.Update(logement);
                   repository.Save();
                   repository.SupDatabaseContext.Entry(logement).Reload();
                   return true;
               }
           }
           catch (Exception ex)
           {
               
           }
            return false;
        }
        public Tbl_LogementCE getLogementCE(int batimentId, string sdeId, int logId)
        {
            return repository.LogementCERepository.Find(l => l.BatimentId == batimentId && l.SdeId == sdeId && l.LogeId == logId).FirstOrDefault();
        }
        #endregion

        #region MENAGE
        public bool saveMenage(Tbl_MenageCE _men)
        {
            try
            {
                repository.MenageRepository.Insert(_men);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("<>=============================Exception/saveMenage:" + ex.Message);
                throw new MessageException("Erreur lors de la sauvegarde. Error: " + ex.Message);
            }
            //return false;
        }

        public bool updateMenage(Tbl_MenageCE _men)
        {
            try
            {
                Tbl_MenageCE menage = repository.MenageRepository.Find(m => m.BatimentId == _men.BatimentId && m.LogeId == _men.LogeId && m.MenageId == _men.MenageId && m.SdeId==_men.SdeId).FirstOrDefault();
                if (Utils.IsNotNull(menage))
                {
                    menage.MenageId = _men.MenageId;
                    menage.LogeId = _men.LogeId;
                    menage.BatimentId = _men.BatimentId;
                    menage.SdeId = _men.SdeId;
                    menage.Qm1NoOrdre = _men.Qm1NoOrdre;
                    menage.Qm2ModeJouissance = _men.Qm2ModeJouissance;
                    menage.Qm5SrcEnergieCuisson1 = _men.Qm5SrcEnergieCuisson1;
                    menage.Qm5SrcEnergieCuisson2 = _men.Qm5SrcEnergieCuisson2;
                    menage.Qm8EndroitBesoinPhysiologique = _men.Qm8EndroitBesoinPhysiologique;
                    menage.Qm11TotalIndividuVivant = _men.Qm11TotalIndividuVivant;
                    menage.DureeSaisie = _men.DureeSaisie;
                    menage.IsContreEnqueteMade = _men.IsContreEnqueteMade;
                    menage.IsValidated = _men.IsValidated;
                    repository.MenageRepository.Update(menage);
                    repository.Save();
                    repository.SupDatabaseContext.Entry(menage).Reload();
                    return true;

                }
            }
            catch (Exception ex)
            {
                log.Info("<>=====================Exception/updateMenage:" + ex.Message);
                throw new MessageException("Erreur lors de la sauvegarde. Error: " + ex.Message);
            }
            return false;
        }

        public List<Tbl_MenageCE> searchAllMenageCE(long batimentId, long logeId, string sdeId)
        {
            try
            {
                return repository.MenageRepository.Find(m => m.BatimentId == batimentId && m.LogeId == logeId && m.SdeId == sdeId).ToList();
            }
            catch (Exception ex)
            {
                log.Info("<>===============================Exception/searchAllMenageCE:" + ex.Message);
            }
            return null;
        }

        public Tbl_MenageCE getMenageCE(long batimentId, long logeId, string sdeId, long menageId)
        {
            return repository.MenageRepository.Find(m => m.BatimentId == batimentId && m.LogeId == logeId && m.MenageId == menageId && m.SdeId == sdeId).FirstOrDefault();
        }
        public Tbl_MenageCE getMenageCE(long menageId)
        {
            return repository.MenageRepository.Find(m => m.MenageId == menageId).FirstOrDefault();
        }
        
        #endregion

        #region DECES
        public bool saveDecesCE(Tbl_DecesCE _deces)
        {
            try
            {
                repository.DecesRepository.Insert(_deces);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("<>===============Exception/saveDecesCE:" + ex.Message);
                throw new MessageException("Erreur lors de la sauvegarde. Error: " + ex.Message);
            }
           // return false;
        }

        public bool updateDecesCE(Tbl_DecesCE _deces)
        {
            try
            {
                Tbl_DecesCE _dec = repository.DecesRepository.Find(d => d.BatimentId == _deces.BatimentId && d.DecesId == _deces.DecesId && d.LogeId == _deces.LogeId && d.MenageId == _deces.MenageId).FirstOrDefault();
                if (_dec != null)
                {
                    _dec.MenageId = _deces.MenageId;
                    _dec.LogeId = _deces.LogeId;
                    _dec.BatimentId = _deces.BatimentId;
                    _dec.SdeId = _deces.SdeId;
                    _dec.Qd2NoOrdre = _deces.Qd2NoOrdre;
                    _dec.Qd1aNbreDecesF = _deces.Qd1aNbreDecesF;
                    _dec.Qd1aNbreDecesG = _deces.Qd1aNbreDecesG;
                    _dec.DureeSaisie = _deces.DureeSaisie;
                    _dec.IsContreEnqueteMade = _deces.IsContreEnqueteMade;
                    _dec.IsValidated = _deces.IsValidated;
                    repository.DecesRepository.Update(_dec);
                    repository.Save();
                    repository.SupDatabaseContext.Entry(_dec).Reload();
                    return true;

                }
            
            }
            catch (Exception ex)
            {
                log.Info("<>==============Exception/updateDecesCE:" + ex.Message);
                throw new MessageException("Erreur lors de la sauvegarde. Error: " + ex.Message);
            }
            return false;
        }
        public Tbl_DecesCE getDecesCEModel(long decesId, string sdeId)
        {
            try
            {
                return repository.DecesRepository.Find(d => d.DecesId == decesId && d.SdeId == sdeId).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public List<Tbl_DecesCE> searchAllDecesCE(long batimentId, long logeId, string sdeId, long menageId)
        {
            try
            {
                return repository.DecesRepository.Find(d => d.BatimentId == batimentId && d.LogeId == logeId && d.MenageId == menageId && d.SdeId == sdeId).ToList();
            }
            catch (Exception ex)
            {
                log.Info("<>===============================Exception/searchAllDecesCE:" + ex.Message);
            }
            return null;
        }

        public Tbl_DecesCE getDecesCEModel(long batimentId, long logId, long menageId, long decesId, string sdeId)
        {
            return repository.DecesRepository.Find(d => d.BatimentId == batimentId && d.LogeId == logId && d.MenageId == menageId && d.DecesId == decesId && d.SdeId == sdeId).FirstOrDefault();
        }


        #endregion

        #region EMIGRE
        public bool saveEmigre(Tbl_EmigreCE _emigre)
        {
            try
            {
                repository.EmigreRepository.Insert(_emigre);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Erreur/saveEmigre:" + ex.Message);
                return false;
            }
        }

        public bool updateEmigre(Tbl_EmigreCE _emigre)
        {
            try
            {
                Tbl_EmigreCE _em = getEmigreCEModel(_emigre.EmigreId.GetValueOrDefault(), _emigre.SdeId);
                _em.BatimentId = _emigre.BatimentId;
                _em.LogeId = _emigre.LogeId;
                _em.MenageId = _emigre.MenageId;
                _em.EmigreId = _emigre.EmigreId;
                _em.DateDebutCollecte = _emigre.DateDebutCollecte;
                _em.DateFinCollecte = _emigre.DateFinCollecte;
                _em.IsContreEnqueteMade = _emigre.IsContreEnqueteMade;
                _em.IsValidated = _emigre.IsValidated;
                _em.Qn1NbreEmigreF = _emigre.Qn1NbreEmigreF;
                _em.Qn1Emigration = _emigre.Qn1Emigration;
                _emigre.Qn1numeroOrdre = _emigre.Qn1numeroOrdre;
                _em.SdeId = _emigre.SdeId;
                repository.EmigreRepository.Update(_em);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Erreur/updateEmigre:" + ex.Message);
                return false;
            }
        }

        public Tbl_EmigreCE getEmigreCEModel(long emigreId, string sdeId)
        {
            return repository.EmigreRepository.Find(em => em.SdeId == sdeId && em.EmigreId == emigreId).FirstOrDefault();
        }

        public Tbl_EmigreCE getEmigreCEModel(long batimentId, long logId, long menageId, long emigreId, string sdeId)
        {
            return repository.EmigreRepository.Find(em => em.SdeId == sdeId && em.EmigreId == emigreId && em.LogeId==logId && em.BatimentId==batimentId && em.MenageId==menageId).FirstOrDefault();
        }
        public List<Tbl_EmigreCE> searchAllEmigres(long batimentId, long logId, long menageId, string sdeId)
        {
            return repository.EmigreRepository.Find(em => em.MenageId == menageId && em.BatimentId == batimentId && em.LogeId == logId && em.SdeId == sdeId).ToList();
        }

        #endregion

        #region INDIVIDU
        public bool saveIndividuCE(Tbl_IndividusCE _ind)
        {
            try
            {
                repository.IndividuRepository.Insert(_ind);
                repository.Save();
                //repository.Refresh<Tbl_IndividuCE>(_ind);
                return true;
            }
            catch (DbEntityValidationException entity)
            {
                foreach (DbEntityValidationResult result in entity.EntityValidationErrors)
                {
                    string entityName = result.Entry.Entity.GetType().Name;
                    foreach (DbValidationError error in result.ValidationErrors)
                    {
                        log.Info("<>=====================Property Name:"+error.PropertyName +"Error Name:"+error.ErrorMessage);
                    }
                }
                
                
            }
            catch (Exception ex)
            {
                log.Info("Error:/saveIndividuCE=>" + ex.Message);
                throw new MessageException("Erreur lors de la sauvegarde. Error: " + ex.Message);
            }
            return false;
        }

        public bool upateIndividuCE(Tbl_IndividusCE _ind)
        {
            try
            {
                Tbl_IndividusCE individu = repository.IndividuRepository.Find(i => i.BatimentId == _ind.BatimentId && i.LogeId == _ind.LogeId && i.MenageId == _ind.MenageId && i.IndividuId == _ind.IndividuId).FirstOrDefault();
                if (Utils.IsNotNull(individu))
                {
                    //individu.Qp2Nom = _ind.Qp2Nom;
                    //individu.Qp2Prenom = _ind.Qp2Prenom;
                    individu.Q3LienDeParente = _ind.Q3LienDeParente;
                    individu.Q3aRaisonChefMenage = _ind.Q3aRaisonChefMenage;
                    individu.Q5bAge = _ind.Q5bAge;
                    individu.Q4Sexe = _ind.Q4Sexe;
                    individu.Qp7Nationalite = _ind.Qp7Nationalite;
                    individu.Qp7PaysNationalite = _ind.Qp7PaysNationalite;
                    individu.Q7DateNaissanceAnnee = _ind.Q7DateNaissanceAnnee;
                    individu.Q7DateNaissanceJour = _ind.Q7DateNaissanceJour;
                    individu.Q7DateNaissanceMois = _ind.Q7DateNaissanceMois;
                    //individu.Qaf1aHandicapVoir = _ind.Qaf1aHandicapVoir;
                    //individu.Qaf2aHandicapEntendre = _ind.Qaf2aHandicapEntendre;
                    //individu.Qaf3aHandicapMarcher = _ind.Qaf3aHandicapMarcher;
                    //individu.Qaf4aHandicapSouvenir = _ind.Qaf4aHandicapSouvenir;
                    //individu.Qaf5aHandicapPourSeSoigner = _ind.Qaf5aHandicapPourSeSoigner;
                    //individu.Qaf6aHandicapCommuniquer = _ind.Qaf6aHandicapCommuniquer;
                    individu.Qp10LieuNaissance = _ind.Qp10LieuNaissance;
                    individu.Qp10CommuneNaissance = _ind.Qp10CommuneNaissance;
                    individu.Qp10LieuNaissanceVqse = _ind.Qp10LieuNaissanceVqse;
                    individu.Qp10PaysNaissance = _ind.Qp10PaysNaissance;
                    individu.Qp11PeriodeResidence = _ind.Qp11PeriodeResidence;
                    //individu.Qp12DomicileAvantRecensement = _ind.Qp12DomicileAvantRecensement;
                    //individu.Qp12DomicileAvantRecensementComm = _ind.Qp12DomicileAvantRecensementComm;
                    //individu.Qp12DomicileAvantRecensementVqse = _ind.Qp12DomicileAvantRecensementVqse;
                    //individu.Qp12DomicileAvantRecensementPays = _ind.Qp12DomicileAvantRecensementPays;
                    //individu.Qe1EstAlphabetise = _ind.Qe1EstAlphabetise;
                    individu.Qe2FreqentationScolaireOuUniv = _ind.Qe2FreqentationScolaireOuUniv;
                    individu.Qe4aNiveauEtude = _ind.Qe4aNiveauEtude;
                    individu.Qe4bDerniereClasseOUAneEtude = _ind.Qe4bDerniereClasseOUAneEtude;
                    //individu.Qe5DiplomeUniversitaire = _ind.Qe5DiplomeUniversitaire;
                    //individu.Qe7FormationProf = _ind.Qe7FormationProf;
                    individu.Qsm1StatutMatrimonial = _ind.Qsm1StatutMatrimonial;
                    individu.Qa1ActEconomiqueDerniereSemaine = _ind.Qa1ActEconomiqueDerniereSemaine;
                    individu.Qa2ActAvoirDemele1 = _ind.Qa2ActAvoirDemele1;
                    individu.Qa2ActDomestique2 = _ind.Qa2ActDomestique2;
                    individu.Qa2ActCultivateur3 = _ind.Qa2ActCultivateur3;
                    individu.Qa2ActAiderParent4 = _ind.Qa2ActAiderParent4;
                    individu.Qa2ActAutre5 = _ind.Qa2ActAutre5;
                    //individu.Qa3StatutEmploie = _ind.Qa3StatutEmploie;
                    //individu.Qa4SecteurInstitutionnel = _ind.Qa4SecteurInstitutionnel;
                    //individu.Qa5TypeBienProduitParEntreprise = _ind.Qa5TypeBienProduitParEntreprise;
                    //individu.Qa6LieuActDerniereSemaine = _ind.Qa6LieuActDerniereSemaine;
                    //individu.Qa7FoncTravail = _ind.Qa7FoncTravail;
                    individu.Qa8EntreprendreDemarcheTravail = _ind.Qa8EntreprendreDemarcheTravail;
                    //individu.Qa9VouloirTravailler = _ind.Qa9VouloirTravailler;
                    //individu.Qa10DisponibilitePourTravail = _ind.Qa10DisponibilitePourTravail;
                    //individu.Qa11RecevoirTransfertArgent = _ind.Qa11RecevoirTransfertArgent;
                    individu.Qf1aNbreEnfantNeVivantM = _ind.Qf1aNbreEnfantNeVivantM;
                    individu.Qf2bNbreEnfantNeVivantF = _ind.Qf2bNbreEnfantNeVivantF;
                    individu.Qf2aNbreEnfantVivantM = _ind.Qf2aNbreEnfantVivantM;
                    individu.Qf2bNbreEnfantVivantF = _ind.Qf2bNbreEnfantVivantF;
                    individu.Qf3DernierEnfantJour = _ind.Qf3DernierEnfantJour;
                    individu.Qf3DernierEnfantMois = _ind.Qf3DernierEnfantMois;
                    individu.Qf3DernierEnfantAnnee = _ind.Qf3DernierEnfantAnnee;
                    individu.Qf4DENeVivantVit = _ind.Qf4DENeVivantVit;
                    individu.DureeSaisie = _ind.DureeSaisie;
                    individu.IsContreEnqueteMade = _ind.IsContreEnqueteMade;
                    individu.IsValidated = _ind.IsValidated;
                    repository.IndividuRepository.Update(individu);
                    repository.Save();
                    repository.SupDatabaseContext.Entry(individu).Reload();
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Info("<>=======================Exception/upateIndividuCE:" + ex.Message);
                throw new MessageException("Erreur lors de la sauvegarde. Error: " + ex.Message);
            }
            return false;
        }


        public List<Tbl_IndividusCE> searchAllIndividuCE(long batimentId, long logeId, string sdeId, long menageId)
        {
            try
            {
                return repository.IndividuRepository.Find(i => i.BatimentId == batimentId && i.LogeId == logeId && i.MenageId == menageId && i.SdeId == sdeId).ToList();
            }
            catch (Exception ex)
            {
                log.Info("<>===============================Exception/searchAllIndividuCE:" + ex.Message);
            }
            return null;
        }
        public List<Tbl_IndividusCE> searchAllIndividuCE(long logId)
        {
            try
            {
                return repository.IndividuRepository.Find(ind => ind.LogeId == logId).ToList();
            }
            catch (Exception ex)
            {
                log.Info("<>===============================Exception/searchAllIndividuCE:" + ex.Message);
            }
            return new List<Tbl_IndividusCE>();
        }

        public Tbl_IndividusCE getIndividuCEModel(long individuId, string sdeId)
        {
            return repository.IndividuRepository.Find(i => i.IndividuId == individuId && i.SdeId == sdeId).FirstOrDefault();
        }
        public Tbl_IndividusCE getIndividuCEModel(long batimentId, long logeId, long menageId, long individuId, string sdeId)
        {
            return repository.IndividuRepository.Find(i => i.BatimentId == batimentId && i.LogeId == logeId && i.MenageId == menageId && i.IndividuId == individuId && i.SdeId == sdeId).FirstOrDefault();
        }
        #endregion

        #region GEOLOCALISATION
        public List<Tbl_Departement> searchAllDepartement()
        {
            try
            {
                return repository.DepartementRepository.Find().ToList();
            }
            catch (Exception)
            {

            }
            return null;
        }

        public List<Tbl_Commune> searchAllCommuneByDept(string deptId)
        {
            try
            {
                return repository.CommuneRepository.Find(c => c.DeptId == deptId).ToList();
            }
            catch (Exception)
            {

            }
            return null;
        }

        public List<Tbl_VilleQuartierSectionCommunale> searchAllVqsebyCom(string comId)
        {
            try
            {
                return repository.VqseRepository.Find(v => v.ComId == comId).ToList();
            }
            catch (Exception)
            {

            }
            return null;
        }

        public List<Tbl_Pays> searchAllPays()
        {
            try
            {
                return repository.PaysRepository.Find().ToList();
            }
            catch (Exception)
            {

            }
            return null;
        }
        public Tbl_Departement getDepartement(string idDepartement)
        {
            try
            {
                return repository.DepartementRepository.Find(d => d.DeptId == idDepartement).FirstOrDefault();
            }
            catch (Exception)
            {

            }
            return null;
        }

        public Tbl_Commune getCommune(string idCommune)
        {
            try
            {
                return repository.CommuneRepository.Find(c => c.ComId == idCommune).FirstOrDefault();
            }
            catch (Exception)
            {

            }
            return null;
        }

        public Tbl_VilleQuartierSectionCommunale getVqse(string vqseId)
        {
            try
            {
                return repository.VqseRepository.Find(v => v.VqseId == vqseId).First();
            }
            catch (Exception)
            {

            }
            return null;
        }

        public Tbl_Pays getPays(string idPays)
        {
            try
            {
                return repository.PaysRepository.Find(p => p.CodePays == idPays).FirstOrDefault();
            }
            catch (Exception)
            {

            }
            return null;
        }
        #endregion

        #region RAPPORT PERSONNEL
        public bool saveRptPersonnel(Tbl_RapportPersonnel rpt)
        {
            try
            {
                repository.RapportPersonnelRepository.Insert(rpt);
                repository.Save();
                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public bool updateteRptPersonnel(Tbl_RapportPersonnel rpt)
        {
            Tbl_RapportPersonnel rptToUpdate = repository.RapportPersonnelRepository.Find(r => r.rapportId == rpt.rapportId).FirstOrDefault();
            try
            {
                if (rptToUpdate != null)
                {
                    rptToUpdate.codeDistrict = rpt.codeDistrict;
                    rptToUpdate.comId = rpt.comId;
                    rptToUpdate.dateEvaluation = rpt.dateEvaluation;
                    rptToUpdate.deptId = rpt.deptId;
                    rptToUpdate.persId = rpt.persId;
                    rptToUpdate.q1 = rpt.q1;
                    rptToUpdate.q10 = rpt.q10;
                    rptToUpdate.q11 = rpt.q11;
                    rptToUpdate.q12 = rpt.q12;
                    rptToUpdate.q13 = rpt.q13;
                    rptToUpdate.q14 = rpt.q14;
                    rptToUpdate.q15 = rpt.q15;
                    rptToUpdate.q9 = rpt.q9;
                    rptToUpdate.q8 = rpt.q8;
                    rptToUpdate.q7 = rpt.q7;
                    rptToUpdate.q6 = rpt.q6;
                    rptToUpdate.q5 = rpt.q5;
                    rptToUpdate.q4 = rpt.q4;
                    rptToUpdate.q3 = rpt.q3;
                    rptToUpdate.q2 = rpt.q2;
                    rptToUpdate.q1 = rpt.q1;
                    rptToUpdate.ReportSenderId = rpt.ReportSenderId;
                    rptToUpdate.score = rpt.score;
                    repository.RapportPersonnelRepository.Update(rptToUpdate);
                    repository.Save();
                    return true;
                }
            }
            catch (Exception)
            {

            }
            return false;
        }

        public bool deleteRptPersonnel(int rptId)
        {
            try
            {
                Tbl_RapportPersonnel rptToDelelte = repository.RapportPersonnelRepository.FindOne(rptId);
                if (rptToDelelte != null)
                {
                    repository.RapportPersonnelRepository.Delete(rptToDelelte.rapportId);
                    return true;
                }
            }
            catch (Exception)
            {

            }
            return false;
        }
        public Tbl_RapportPersonnel getRptPersonnel(int rptId)
        {
            return repository.RapportPersonnelRepository.FindOne(rptId);
        }

        public List<Tbl_RapportPersonnel> searchRptPersonnel()
        {
            return repository.RapportPersonnelRepository.Find().ToList();
        }
        public List<Tbl_RapportPersonnel> searchRptPersonnelbyAgent(int persId)
        {
            return repository.RapportPersonnelRepository.Find(r => r.persId == persId).ToList();
        }
        #endregion

        #region RAPPORT DEROULEMENT COLLECTE

        public long saveRptDeroulement(Tbl_RprtDeroulement rptDeroulement)
        {
            try
            {
               Tbl_RprtDeroulement rpt= repository.RapportDeroulementRepository.Insert(rptDeroulement);
                repository.Save();
                return rpt.RapportId;
            }
            catch (Exception ex)
            {
                log.Info("DaoContreEnquete:/saveRptDeroulement=>Error:" + ex.Message);
                throw new RapportException("DaoContreEnquete:/saveRptDeroulement=>Error:" + ex.Message);
            }
            //return false;
        }

        public bool updateRptDeroulement(Tbl_RprtDeroulement rptDeroulement)
        {
            try
            {
                Tbl_RprtDeroulement deroulement = repository.RapportDeroulementRepository.Find(d => d.RapportId == rptDeroulement.RapportId).FirstOrDefault();
                if (deroulement != null)
                {
                    deroulement.DateRapport = rptDeroulement.DateRapport;
                    deroulement.CodeDistrict = rptDeroulement.CodeDistrict;
                    repository.RapportDeroulementRepository.Update(deroulement);
                    repository.Save();
                    return true;
                }
            }
               
            catch (Exception ex)
            {
                log.Info("DaoContreEnquete:/updateRptDeroulement=>Error:" + ex.Message);
                throw new RapportException("DaoContreEnquete:/updateRptDeroulement=>Error:" + ex.Message);
            }
            return false;
        }

        public bool saveDetailsDeroulement(Tbl_DetailsRapport details)
        {
            try
            {
                repository.RapportDetailsDeroulementRepository.Insert(details);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("DaoContreEnquete:/saveDetailsDeroulement=>Error:" + ex.Message);
                throw new RapportException("DaoContreEnquete:/saveDetailsDeroulement=>Error:" + ex.Message);
            }
            //return false;
        }

        public bool updateDetailsDeroulement(Tbl_DetailsRapport details)
        {
            try
            {
                Tbl_DetailsRapport rpt = repository.RapportDetailsDeroulementRepository.Find(r => r.DetailsRapportId == details.DetailsRapportId && r.RapportId == details.RapportId).FirstOrDefault();
                if (rpt != null)
                {
                    rpt.Commentaire = details.Commentaire;
                    rpt.Domaine = details.Domaine;
                    rpt.SousDomaine = details.SousDomaine;
                    rpt.Suggestions = details.Suggestions;
                    rpt.Solution = details.Solution;
                    rpt.Precisions = details.Precisions;
                    rpt.Probleme = details.Probleme;
                    rpt.Suivi = details.Suivi;
                    repository.RapportDetailsDeroulementRepository.Update(rpt);
                    repository.Save();
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Info("DaoContreEnquete:/updateDetailsDeroulement=>Error:" + ex.Message);
                throw new RapportException("DaoContreEnquete:/updateDetailsDeroulement=>Error:" + ex.Message);
            }
            return false;
        }
        

        public List<Tbl_RprtDeroulement> searchRptDeroulment()
        {
            try
            {
                List<Tbl_RprtDeroulement> listOf = new List<Tbl_RprtDeroulement>();
                listOf = repository.RapportDeroulementRepository.Find().ToList();
                return listOf;
            }
            catch (Exception ex)
            {
                log.Info("DaoContreEnquete:/searchRptDeroulment=>Error:" + ex.Message);
                throw new RapportException("DaoContreEnquete:/searchRptDeroulment=>Error:" + ex.Message);
            }
        }

        public Tbl_RprtDeroulement getRptDeroulement(string codeDistrict)
        {
            throw new NotImplementedException();
        }

        public List<Tbl_DetailsRapport> searchDetailsReport(Tbl_RprtDeroulement rptDeroulment)
        {
            try
            {
                List<Tbl_DetailsRapport> listOf = new List<Tbl_DetailsRapport>();
                listOf = repository.RapportDetailsDeroulementRepository.Find(r=>r.RapportId==rptDeroulment.RapportId).ToList();
                return listOf;
            }
            catch (Exception ex)
            {
                log.Info("DaoContreEnquete:/searchDetailsReport=>Error:" + ex.Message);
                throw new RapportException("DaoContreEnquete:/searchDetailsReport=>Error:" + ex.Message);
            }
        }
        #endregion









    }
}
