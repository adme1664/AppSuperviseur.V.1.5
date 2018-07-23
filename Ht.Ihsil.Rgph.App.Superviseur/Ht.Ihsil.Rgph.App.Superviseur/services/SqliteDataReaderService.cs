using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.DataAccess.Repositories;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.SchemaTest;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ht.Ihsi.Rgph.Utility.Utils;
using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsil.Rgph.App.Superviseur.Json;

namespace Ht.Ihsil.Rgph.App.Superviseur.services
{
    public class SqliteDataReaderService
    {
        private MainRepository sqliteRepository;
        private ILogger log;
        ISqliteReader sr;
        private static readonly object syncLock = new object();
        private static readonly Random random=new Random();
        public List<int> numbers = null;

        public ISqliteReader Sr
        {
            get { return sr; }
            set { sr = value; }
        }

        public SqliteDataReaderService(string connectionString)
        {
            Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
            sqliteRepository = new MainRepository(Users.users.SupDatabasePath, true);
            log = new Logger();
            sr = new SqliteReader(connectionString);
            numbers = new List<int>();
        }
        public SqliteDataReaderService()
        {
            numbers = new List<int>();
        }

        public List<BatimentJson> GetAllBatimentsInJson()
        {
            try
            {
                return Sr.GetAllBatimentsInJson();
            }
            catch (Exception ex)
            {
                log.Info("Erreur:" + ex.Message);
            }
            return new List<BatimentJson>();
        }
        /// Envoyer un batiment vers le web service
        /// </summary>
        /// <param Name="batiment"></param>
        /// <returns>bool</returns>
        public bool InsertBatimentType(BatimentType batiment)
        {
            //InsertBatimentRequest batimentRequest = new InsertBatimentRequest();
            //InsertBatimentResponse batimentResponse = new InsertBatimentResponse();
            //if (Utilities.pingTheServer(ConfigurationManager.AppSettings.Get("adrIpServer")))
            //{
            //    batimentRequest.batimentData = batiment;
            //    batimentResponse = ws.InsertBatiment(batimentRequest);
            //    if (batimentResponse.responseHeader.statusCode == "SUCCESS")
            //    {
            //        return true;
            //    }

            //}
            return false;

        }
        /// <summary>
        /// Mise a jour de la table SDE lors des synchronisation avec le web service ou avec la table de l'agent.
        /// </summary>
        /// <param Name="sde"></param>
        /// <returns></returns>
        public bool UpdateSdeSummary(Tbl_Sde sde)
        {
            try
            {
                if (sde != null)
                {
                    Tbl_Sde _sde = sqliteRepository.SdeRepository.FindOne(sde.SdeId);
                    _sde.TotalBatRecense = sde.TotalBatRecense;
                    _sde.TotalLogeCRecense = sde.TotalLogeCRecense;
                    _sde.TotalLogeIRecense = sde.TotalLogeIRecense;
                    _sde.TotalMenageRecense = sde.TotalMenageRecense;
                    _sde.TotalIndRecense = sde.TotalIndRecense;
                    _sde.TotalEmigreRecense = sde.TotalEmigreRecense;
                    _sde.TotalDecesRecense = sde.TotalDecesRecense;
                    sqliteRepository.SdeRepository.Update(_sde);
                    sqliteRepository.Save();
                    return true;
                }

            }
            catch (Exception ex)
            {
                log.Info("<>===Unable to update SdeSummary" + ex.Message);
                return false;
            }

            return false;
        }

        #region #region OPERATION SUR LE FICHIER SQLITE
        /// <summary>
        /// Retourne tous les batiments en format BatimentType pour les envoyer vers le web service.
        /// </summary>
        /// <returns>List<BatimentType</returns>
        public List<BatimentType> GetAllBatimentType()
        {
            try
            {
                return Sr.GetAllBatimentType();
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/GetAllBatimentType" + ex.Message);
            }
            return null;
        }
        public List<BatimentType> GetAllBatimentMalRempli()
        {
            try
            {
                return Sr.GetAllBatimentMalRempli();
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/GetAllBatimentMalRempli" + ex.Message);
            }
            return null;
        }
        public List<BatimentType> GetAllBatimentPasFini()
        {
            try
            {
                return Sr.GetAllBatimentPasFini();
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/GetAllBatimentPasFini" + ex.Message);
            }
            return null;
        }
        public List<BatimentType> GetAllBatimentTermine()
        {
            try
            {
                return Sr.GetAllBatimentTermine();
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/GetAllBatimentTermine" + ex.Message);
            }
            return null;
        }
        public BatimentType ReadBatimentType(long batimentId)
        {
            try
            {
                return Sr.ReadBatimentType(batimentId);
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/ReadBatimentType" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// <summary>
        /// Retourne les informations sur un individu.
        /// </summary>
        /// <param Name="individuId"></param>
        /// <returns>IndividuModel</returns>
        public IndividuModel getIndividuDetail(long individuId)
        {
            try
            {
                return Sr.GetIndividuById(individuId);
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getIndividuDetail" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne les informations sur un emigre.
        /// </summary>
        /// <param Name="id"></param>
        /// <returns>EmigreModel</returns>
        public EmigreModel getEmigreDetail(int id)
        {
            try
            {
                return Sr.GetEmigreById(id);
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getEmigreDetail" + ex.Message);
            }

            return null;
        }
        /// <summary>
        /// Retourne les informations sur un deces.
        /// </summary>
        /// <param Name="id"></param>
        /// <returns>DecesModel</returns>
        public DecesModel getDecesDetail(long id)
        {
            try
            {
                return Sr.GetDecesById(id);
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getDecesDetail" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne les informations sur un SDE.
        /// </summary>
        /// <param Name="SdeId"></param>
        /// <returns>Sde</returns>
        public Tbl_Sde getSdeDetails(string sdeId)
        {
            return Sr.getSdeDetailsFromSqliteFile();
        }
        /// <summary>
        /// Retourne tous les batiments dans une SDE.
        /// </summary>
        /// <returns>BatimentModel[]</returns>
        public BatimentModel[] getAllBatiments()
        {
            try
            {
                if (Sr.GetAllBatimentModel() == null)
                    return null;
                else
                {
                    return Sr.GetAllBatimentModel().ToArray();
                }

            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getAllBatiments" + ex.Message);
            }
            return null;
        }

        public MenageModel getMenageById(long menageId)
        {
            try
            {
                return Sr.GetMenageById(menageId);
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getMenageById" + ex.Message);
            }
            return null;
        }

        #endregion

        #region OPERATION DE CONTRE-ENQUETE
        public List<BatimentModel> getAllBatimentVide()
        {
            try
            {
                return Sr.GetAllBatimentVide();
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getAllBatimentVide" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Verifier sur un batiment existe deja.
        /// </summary>
        /// <param Name="listOfBat"></param>
        /// <param Name="bat"></param>
        /// <returns>bool</returns>

        /// <summary>
        /// Retourne un Menage ayant des deces et des individus.
        /// </summary>
        /// <param Name="_men"></param>
        /// <param Name="_bat"></param>
        /// <param Name="_log"></param>
        /// <returns>MenageModel</returns>
        MenageModel getMenageModel(MenageModel _men, BatimentModel _bat, LogementModel _log)
        {
            List<DecesModel> listOfDeces = Sr.GetDecesByMenage(Convert.ToInt32(_men.MenageId));

            if (Utils.IsNotNull(listOfDeces))
            {
                _men.Deces = new List<DecesModel>();
                foreach (DecesModel _deces in listOfDeces)
                {
                    _deces.BatimentId = Convert.ToInt32(_bat.BatimentId);
                    _deces.LogeId = _log.LogeId;
                    _men.Deces.Add(_deces);
                }
            }
            List<IndividuModel> listOfIndividu = Sr.GetIndividuByMenage(Convert.ToInt32(_men.MenageId));
            if (Utils.IsNotNull(listOfIndividu))
            {
                _men.Individus = new List<IndividuModel>();
                foreach (IndividuModel _ind in listOfIndividu)
                {
                    _ind.BatimentId = Convert.ToInt32(_bat.BatimentId);
                    _ind.LogeId = _log.LogeId;
                    _men.Individus.Add(_ind);
                }
            }
            MenageModel menage = _men;
            return menage;
        }

        public List<BatimentModel> getAllBatimentWithLogementOccupantAbsent()
        {

            string methodName = "getAllBatimentWithLogementOccupantAbsent";
            List<BatimentModel> listOfBatiment = new List<BatimentModel>();
            try
            {
                List<LogementModel> listOfLogement = sr.GetAllLogementOccupantAbsent();
                if (listOfLogement.Count != 0)
                {
                    foreach (LogementModel logement in listOfLogement)
                    {
                        BatimentModel bat = new BatimentModel();
                        bat.BatimentId = logement.BatimentId;
                        bat.SdeId = logement.SdeId;
                        listOfBatiment.Add(bat);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/" + methodName + ":Erreur=>" + ex.Message);
            }
            return listOfBatiment;
        }

        public List<BatimentModel> getAllBatimentWithLogementOccupesOccasionnellemt()
        {

            string methodName = "getAllBatimentWithLogementOccupesOccasionnellemt";
            List<BatimentModel> listOfBatiment = new List<BatimentModel>();
            try
            {
                List<LogementModel> listOfLogement = sr.GetAllLogementOccupeOccasionnellement();
                if (listOfLogement.Count != 0)
                {
                    foreach (LogementModel logement in listOfLogement)
                    {
                        BatimentModel bat = new BatimentModel();
                        bat.BatimentId = logement.BatimentId;
                        bat.SdeId = logement.SdeId;
                        listOfBatiment.Add(bat);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/" + methodName + ":Erreur=>" + ex.Message);
            }
            return listOfBatiment;
        }
        /// <summary>
        /// Retourne 3 batiments ayant des logements occupes occasionnelllement
        /// </summary>
        /// <returns></returns>
        public List<BatimentModel> get3BatimentWithLogementOccupesOccasionnellement()
        {

            string methodName = "get3BatimentWithLogementOccupesOccasionnellement";
            List<BatimentModel> listOfBatiment = new List<BatimentModel>();
            try
            {
                List<BatimentModel> listOf = getAllBatimentWithLogementOccupesOccasionnellemt();
                if (listOf.Count > 0)
                {
                   
                    List<BatimentModel> listBatLogOccupesOccasionellement = new List<BatimentModel>();
                    if (listOf.Count <= 1)
                    {
                        return listOf;
                    }
                    int aleatoire = 0;
                    for (int i = 0; i <= listOf.Count; i++)
                    {
                        aleatoire = RandomNumber((aleatoire + 1), listOf.Count);
                        BatimentModel bat = listOf.ElementAt(aleatoire);
                        if (listBatLogOccupesOccasionellement.Count > 0)
                        {
                            foreach (BatimentModel batiment in listBatLogOccupesOccasionellement)
                            {
                                if (Utilities.isBatimentExistInList(listBatLogOccupesOccasionellement, bat)==false)
                                {
                                    listBatLogOccupesOccasionellement.Add(bat);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            listBatLogOccupesOccasionellement.Add(bat);
                        }
                        if (listBatLogOccupesOccasionellement.Count == 3)
                            break;
                    }

                    //Ajout des logements
                    foreach (BatimentModel bat in listBatLogOccupesOccasionellement)
                    {
                        List<LogementModel> logmnt = Sr.GetLogementIByBatiment(bat.BatimentId);
                        if (logmnt.Count != 0)
                        {
                            bat.Logement = new List<LogementModel>();
                            foreach (LogementModel lg in logmnt)
                            {
                                if (lg.Qlin2StatutOccupation == (int)Constant.StatutOccupation.Okipe_le_konsa)
                                {
                                    bat.Logement.Add(lg);
                                }
                            }
                            listOfBatiment.Add(bat);
                        }
                    }
                }               
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/" + methodName + ":Erreur=>" + ex.Message);
            }
            return listOfBatiment;
        }

        /// <summary>
        /// Retourne 10 batiments ayant au moins un logement individuel et un Menage 
        /// </summary>
        /// <returns>List<BatimentModel></returns>
        public List<BatimentModel> getAllBatimentWithLogInd()
        {
            try
            {

                List<BatimentModel> listOfBatiments = new List<BatimentModel>();
                List<BatimentModel> listOfBatimentsUnique = new List<BatimentModel>();
                List<BatimentModel> listOfBatWithLInd = new List<BatimentModel>();
                listOfBatiments = Sr.GetAllBatimentFinishedWithLogInd();
                if (Utils.IsNotNull(listOfBatiments))
                {
                    #region si le nombre de batiments collectes dans la SDE est superieur a 3
                    if (listOfBatiments.Count() >= 9)
                    {
                        int aleatoire = 0;
                        //On prend 3 batiments ayant au moins un logement et un Menage;
                        for (int i = 0; i < listOfBatiments.Count; i++)
                        {
                            BatimentModel bat = listOfBatiments.ElementAt(RandomNumber(0, listOfBatiments.Count));
                            if (Utilities.isBatimentExistInList(listOfBatimentsUnique, bat) == false)
                            {
                                listOfBatimentsUnique.Add(bat);
                            }
                            if (listOfBatimentsUnique.Count == 3)
                            {
                                break;
                            }                            
                        }
                        foreach (BatimentModel _bat in listOfBatimentsUnique)
                        {
                            if (Convert.ToInt32(_bat.BatimentId) != 0)
                            {
                                List<LogementModel> listOfLogmentInd = new List<LogementModel>();
                                listOfLogmentInd = Sr.GetLogementIByBatiment(_bat.BatimentId);
                                if (listOfLogmentInd.Count() == 1)
                                {
                                    LogementModel _log = listOfLogmentInd.FirstOrDefault();
                                    _log.SdeId = _bat.SdeId;
                                    List<MenageModel> listOfMenage = new List<MenageModel>();
                                    listOfMenage = Sr.GetMenageByLogement(Convert.ToInt32(_log.LogeId));
                                    //Si le nombre de menages est egal a 1
                                    if (listOfMenage.Count() == 1)
                                    {
                                        MenageModel _men = listOfMenage.FirstOrDefault();
                                        _men.BatimentId = Convert.ToInt32(_bat.BatimentId);
                                        _men.SdeId = _bat.SdeId;
                                        _log.Menages = new List<MenageModel>();
                                        _log.Menages.Add(getMenageModel(_men, _bat, _log));
                                        _bat.Logement = new List<LogementModel>();
                                        _bat.Logement.Add(_log);
                                        listOfBatWithLInd.Add(_bat);
                                    }
                                    else
                                    {
                                        //Sinon
                                        if (listOfMenage.Count() > 1)
                                        {
                                            MenageModel _men = listOfMenage.ElementAt(RandomNumber(1, listOfMenage.Count()));
                                            _men.BatimentId = Convert.ToInt32(_bat.BatimentId);
                                            _men.SdeId = _bat.SdeId;
                                            _log.Menages = new List<MenageModel>();
                                            _log.Menages.Add(getMenageModel(_men, _bat, _log));
                                            _bat.Logement = new List<LogementModel>();
                                            _bat.Logement.Add(_log);
                                            listOfBatWithLInd.Add(_bat);
                                        }
                                    }
                                }
                                else
                                {
                                    if (listOfLogmentInd.Count() > 1)
                                    {
                                        LogementModel _log = listOfLogmentInd.ElementAt(RandomNumber(1, listOfLogmentInd.Count()));
                                        _log.SdeId = _bat.SdeId;
                                        List<MenageModel> listOfMenage = new List<MenageModel>();
                                        listOfMenage = Sr.GetMenageByLogement(Convert.ToInt32(_log.LogeId));
                                        if (listOfMenage.Count() == 1)
                                        {
                                            MenageModel _men = listOfMenage.FirstOrDefault();
                                            _men.BatimentId = Convert.ToInt32(_bat.BatimentId);
                                            _men.SdeId = _bat.SdeId;
                                            _log.Menages = new List<MenageModel>();
                                            _log.Menages.Add(getMenageModel(_men, _bat, _log));
                                            _bat.Logement = new List<LogementModel>();
                                            _bat.Logement.Add(_log);
                                            listOfBatWithLInd.Add(_bat);
                                        }
                                        else
                                        {
                                            if (listOfMenage.Count() > 1)
                                            {
                                                MenageModel _men = listOfMenage.ElementAt(RandomNumber(1, listOfMenage.Count()));
                                                _men.BatimentId = Convert.ToInt32(_bat.BatimentId);
                                                _men.SdeId = _bat.SdeId;
                                                _log.Menages = new List<MenageModel>();
                                                _log.Menages.Add(getMenageModel(_men, _bat, _log));
                                                _bat.Logement = new List<LogementModel>();
                                                _bat.Logement.Add(_log);
                                                listOfBatWithLInd.Add(_bat);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region si le nombre de batiments collectes dans la SDE est a 3
                    else
                    {
                        //On prend 3 batiments ayant au moins un logement et un Menage;
                        for (int i = 0; i < listOfBatiments.Count; i++)
                        {
                            BatimentModel bat = listOfBatiments.ElementAt(i);
                            if (Utilities.isBatimentExistInList(listOfBatimentsUnique, bat) == false)
                            {
                                listOfBatimentsUnique.Add(bat);
                            }
                            if (listOfBatimentsUnique.Count == 3)
                            {
                                break;
                            }                            
                        }
                        foreach (BatimentModel _bat in listOfBatimentsUnique)
                        {
                            if (Convert.ToInt32(_bat.BatimentId) != 0)
                            {
                                List<LogementModel> listOfLogmentInd = new List<LogementModel>();
                                listOfLogmentInd = Sr.GetLogementIByBatiment(_bat.BatimentId);
                                if (listOfLogmentInd.Count() == 1)
                                {
                                    LogementModel _log = listOfLogmentInd.FirstOrDefault();
                                    _log.SdeId = _bat.SdeId;
                                    List<MenageModel> listOfMenage = new List<MenageModel>();
                                    listOfMenage = Sr.GetMenageByLogement(Convert.ToInt32(_log.LogeId));
                                    //Si le nombre de menages est egal a 1
                                    if (listOfMenage.Count() == 1)
                                    {
                                        MenageModel _men = listOfMenage.FirstOrDefault();
                                        _men.BatimentId = Convert.ToInt32(_bat.BatimentId);
                                        _men.SdeId = _bat.SdeId;
                                        _log.Menages = new List<MenageModel>();
                                        _log.Menages.Add(getMenageModel(_men, _bat, _log));
                                        _bat.Logement = new List<LogementModel>();
                                        _bat.Logement.Add(_log);
                                        listOfBatWithLInd.Add(_bat);
                                    }
                                    else
                                    {
                                        //Sinon
                                        if (listOfMenage.Count() > 1)
                                        {
                                            MenageModel _men = listOfMenage.ElementAt(RandomNumber(1, listOfMenage.Count()));
                                            _men.BatimentId = Convert.ToInt32(_bat.BatimentId);
                                            _men.SdeId = _bat.SdeId;
                                            _log.Menages = new List<MenageModel>();
                                            _log.Menages.Add(getMenageModel(_men, _bat, _log));
                                            _bat.Logement = new List<LogementModel>();
                                            _bat.Logement.Add(_log);
                                            listOfBatWithLInd.Add(_bat);
                                        }
                                    }
                                }
                                else
                                {
                                    if (listOfLogmentInd.Count() > 1)
                                    {
                                        LogementModel _log = listOfLogmentInd.ElementAt(RandomNumber(1, listOfLogmentInd.Count()));
                                        _log.SdeId = _bat.SdeId;
                                        List<MenageModel> listOfMenage = new List<MenageModel>();
                                        listOfMenage = Sr.GetMenageByLogement(Convert.ToInt32(_log.LogeId));
                                        if (listOfMenage.Count() == 1)
                                        {
                                            MenageModel _men = listOfMenage.FirstOrDefault();
                                            _men.BatimentId = Convert.ToInt32(_bat.BatimentId);
                                            _men.SdeId = _bat.SdeId;
                                            _log.Menages = new List<MenageModel>();
                                            _log.Menages.Add(getMenageModel(_men, _bat, _log));
                                            _bat.Logement = new List<LogementModel>();
                                            _bat.Logement.Add(_log);
                                            listOfBatWithLInd.Add(_bat);
                                        }
                                        else
                                        {
                                            if (listOfMenage.Count() > 1)
                                            {
                                                MenageModel _men = listOfMenage.ElementAt(RandomNumber(1, listOfMenage.Count()));
                                                _men.BatimentId = Convert.ToInt32(_bat.BatimentId);
                                                _men.SdeId = _bat.SdeId;
                                                _log.Menages = new List<MenageModel>();
                                                _log.Menages.Add(getMenageModel(_men, _bat, _log));
                                                _bat.Logement = new List<LogementModel>();
                                                _bat.Logement.Add(_log);
                                                listOfBatWithLInd.Add(_bat);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    
                    #endregion
                    IEnumerable<BatimentModel> lBat = listOfBatWithLInd.GroupBy(b => b.BatimentId).Select(group => group.First());
                    return lBat.ToList();
                }
                else
                {
                    return new List<BatimentModel>();
                }

            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getAllBatimentWithLogInd" + ex.Message);
            }
            return new List<BatimentModel>();
        }
        public LogementModel[] getAllLogementCollectif(BatimentModel _batiment)
        {
            try
            {
                if (Sr == null)
                {
                    Sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, _batiment.SdeId));
                }
                return Sr.GetLogementCByBatiment(Convert.ToInt32(_batiment.BatimentId)).ToArray();
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getAllLogementCollectif" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne la liste des batiments ayant au moins un logement collectif
        /// </summary>
        /// <returns>List<BatimentModel></returns>
        public List<BatimentModel> getAllLogementCollectif()
        {
            List<BatimentModel> listOfBatiments = new List<BatimentModel>();
            List<BatimentModel> listOfBatWithLc = new List<BatimentModel>();

            try
            {
                listOfBatiments = Sr.GetABatimentWithLogC();
                foreach (BatimentModel batiment in listOfBatiments)
                {
                    List<LogementModel> listOfLogementCollectif = new List<LogementModel>();
                    listOfLogementCollectif = getAllLogementCollectif(batiment).ToList();
                    if (listOfLogementCollectif.Count() != 0)
                    {
                        List<LogementModel> listLogementVide = new List<LogementModel>();
                        foreach (LogementModel logement in listOfLogementCollectif)
                        {
                            logement.SdeId = batiment.SdeId;
                            listLogementVide.Add(logement);
                        }
                        batiment.Logement = listLogementVide;
                        listOfBatWithLc.Add(batiment);
                    }

                }
                return listOfBatWithLc;
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getAllLogementCollectif" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne tous les logements individuels
        /// </summary>
        /// <param Name="Batiment"></param>
        /// <returns>LogementModel[]</returns>
        public LogementModel[] getAllLogementIndividuel(BatimentModel _batiment)
        {
            try
            {
                if (Sr == null)
                {
                    Sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, _batiment.SdeId));
                }
                return Sr.GetLogementIByBatiment(Convert.ToInt32(_batiment.BatimentId)).ToArray();
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getAllLogementIndividuel" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne la listedes batiments ayant au moins un logement vide dans une SDE
        /// </summary>
        /// <returns>List<BatimentModel></returns>
        public List<BatimentModel> getAllLogementIndividuel()
        {
            List<BatimentModel> listOfBatiments = new List<BatimentModel>();
            List<BatimentModel> listOfBatWithLI = new List<BatimentModel>();
            try
            {
                listOfBatiments = Sr.GetAllBatimentModel();
                foreach (BatimentModel batiment in listOfBatiments)
                {
                    List<LogementModel> listOfLogementIndividuel = new List<LogementModel>();
                    listOfLogementIndividuel = getAllLogementIndividuel(batiment).ToList();
                    if (listOfLogementIndividuel.Count() != 0)
                    {
                        List<LogementModel> listLogement = new List<LogementModel>();
                        foreach (LogementModel logement in listOfLogementIndividuel)
                        {
                            logement.SdeId = batiment.SdeId;
                            listLogement.Add(logement);
                        }
                        batiment.Logement = listLogement;
                        listOfBatWithLI.Add(batiment);
                    }
                }
                return listOfBatWithLI;
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getAllLogementIndividuel" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne la listedes batiments ayant au moins un logement vide dans une SDE
        /// </summary>
        /// <returns>List<BatimentModel></returns>
        public List<BatimentModel> getAllLogementIndividuelVide()
        {
            List<BatimentModel> listOfBatiments = new List<BatimentModel>();
            List<BatimentModel> listOfBatWithLIV = new List<BatimentModel>();
            try
            {
                listOfBatiments = Sr.GetAllBatimentWithLogVide();
                foreach (BatimentModel batiment in listOfBatiments)
                {
                    List<LogementModel> listOfLogementIndividuel = new List<LogementModel>();
                    listOfLogementIndividuel = getAllLogementIndividuel(batiment).ToList();
                    if (listOfLogementIndividuel.Count() != 0)
                    {
                        List<LogementModel> listLogementVide = new List<LogementModel>();
                        foreach (LogementModel logement in listOfLogementIndividuel)
                        {
                            if (logement.Qlin2StatutOccupation == (int)Constant.StatutOccupation.Pa_okipe)
                            {
                                logement.SdeId = batiment.SdeId;
                                listLogementVide.Add(logement);
                                batiment.Logement = new List<LogementModel>();
                                batiment.Logement = listLogementVide;
                                if (Utilities.isBatimentExistInList(listOfBatWithLIV, batiment) == false)
                                {
                                    listOfBatWithLIV.Add(batiment);
                                }
                             }

                        }

                    }

                }
                return listOfBatWithLIV;
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getAllLogementIndividuelVide" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne tous les menages a l'interieur d'un logement
        /// </summary>
        /// <param Name="Logement"></param>
        /// <returns>MenageModel[]</returns>
        public MenageModel[] getAllMenage(LogementModel _logement)
        {
            try
            {
                if (Sr == null)
                {
                    Sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, _logement.SdeId));
                }
                return Sr.GetMenageByLogement(Convert.ToInt32(_logement.LogeId)).ToArray();
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getAllMenage" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne tous les batiments ayant au moins un Menage
        /// </summary>
        /// <returns>List<BatimentModel></returns>
        public List<BatimentModel> getMenageInBatiment()
        {
            List<BatimentModel> listOfBatiments = new List<BatimentModel>();
            List<BatimentModel> listOfBatWitMenages = new List<BatimentModel>();
            try
            {
                listOfBatiments = Sr.GetAllBatimentModel();
                foreach (BatimentModel bati in listOfBatiments)
                {
                    List<LogementModel> listOfLogements = new List<LogementModel>();
                    listOfLogements = getAllLogementIndividuel(bati).ToList();
                    if (listOfLogements.Count() != 0)
                    {
                        foreach (LogementModel logement in listOfLogements)
                        {
                            List<MenageModel> listOfMenages = new List<MenageModel>();
                            listOfMenages = getAllMenage(logement).ToList();
                            if (listOfMenages.Count() != 0)
                            {
                                foreach (MenageModel menage in listOfMenages)
                                {
                                    logement.Menages.Add(menage);
                                }
                                bati.Logement.Add(logement);
                                listOfBatWitMenages.Add(bati);
                            }
                        }

                    }
                }
                return listOfBatWitMenages;
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getMenageInBatiment" + ex.Message);
            }

            return null;
        }
        /// <summary>
        /// Retourne tous les batiments ayant au moins un logement vide.
        /// </summary>
        /// <param Name="Sde"></param>
        /// <returns>List<BatimentModel></returns>
        public List<BatimentModel> getBlankLogementIndividuel(SdeModel _sde)
        {
            List<BatimentModel> listOfBatiments = new List<BatimentModel>();
            List<BatimentModel> listOfBatWithLc = new List<BatimentModel>();
            try
            {
                listOfBatiments = Sr.GetAllBatimentModel();
                foreach (BatimentModel bati in listOfBatiments)
                {
                    List<LogementModel> listOfLogementIndividuel = new List<LogementModel>();
                    listOfLogementIndividuel = getAllLogementIndividuel(bati).ToList();
                    if (listOfLogementIndividuel.Count() != 0)
                    {
                        List<LogementModel> listLogementVide = new List<LogementModel>();
                        foreach (LogementModel log in listOfLogementIndividuel)
                        {
                            if (log.Qlin2StatutOccupation == (int)Constant.StatutOccupation.Pa_okipe)
                            {
                                log.SdeId = _sde.SdeId;
                                listLogementVide.Add(log);
                            }
                        }
                        bati.Logement = listLogementVide;
                    }
                    listOfBatWithLc.Add(bati);

                }
                return listOfBatWithLc;
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getBlankLogementIndividuel" + ex.Message);
            }

            return null;
        }

        public LogementTypeModel[] getLogementType()
        {
            return new LogementTypeModel[]
            {
               new LogementTypeModel("Lojman kolektif"),
               new LogementTypeModel("Lojman Endividyel")
            };
        }
        public MenageTypeModel[] getAllInMenage()
        {
            return new MenageTypeModel[]
            {
                new MenageTypeModel("Dèsè"),
                new MenageTypeModel("Emigre"),
                new MenageTypeModel("Endividi"),
                new MenageTypeModel("Rapo"),
            };
        }
        public MenageTypeModel[] getAllinMenageCE()
        {
            return new MenageTypeModel[]
            {

                new MenageTypeModel("Dèsè"),
                new MenageTypeModel("Emigre"),
                new MenageTypeModel("Endividi"),
                new MenageTypeModel("Rapo"),
            };
        }
        public LogementModel[] getAllLogement(BatimentModel _batiment, LogementTypeModel _logementType)
        {
            switch (_logementType.LogementName)
            {
                case "Lojman kolektif":
                    return getAllLogementCollectif(_batiment);

                case "Lojman Endividyel":
                    return getAllLogementIndividuel(_batiment);

            }
            return null;
        }
        /// <summary>
        /// Retourne tous les elements a l'interieur d'un Menage
        /// </summary>
        /// <param Name="_model"></param>
        /// <param Name="_menageType"></param>
        /// <returns>MenageDetailsModel[]</returns>
        public MenageDetailsModel[] getDetailsMenage(MenageModel _model, MenageTypeModel _menageType)
        {
            switch (_menageType.Name)
            {
                case "Rapo":
                    return getRapportFinal(_model);
                case "Dèsè":
                    return getDecesForMenage(_model);

                case "Emigre":
                    return getEmigreForMenage(_model);
                case "Endividi":
                    return getIndividuForMenage(_model);

            }
            return null;
        }

        /// <summary>
        /// Retourne le rapport pour un menage
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        public MenageDetailsModel[] getRapportFinal(MenageModel _model)
        {
            try
            {
                if (Sr == null)
                {
                    Sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, _model.SdeId));
                }
                return Sr.GetRapportFinal(_model.MenageId).ToArray();

            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getRapportFinal" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retourne les deces a l'interieur d'un Menage
        /// </summary>
        /// <param Name="_model"></param>
        /// <returns>MenageDetailsModel[]</returns>
        public MenageDetailsModel[] getDecesForMenage(MenageModel _model)
        {
            try
            {
                if (Sr == null)
                {
                    Sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, _model.SdeId));
                }
                return Sr.GetDecesByMenageDetails(_model.MenageId).ToArray();

            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getDecesForMenage" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne les emigres a l'interieur d'un Menage
        /// </summary>
        /// <param Name="_model"></param>
        /// <returns>MenageDetailsModel[]</returns>
        public MenageDetailsModel[] getEmigreForMenage(MenageModel _model)
        {
            try
            {
                if (Sr == null)
                {
                    Sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, _model.SdeId));
                }
                return Sr.GetEmigreByMenageDetails(_model.MenageId).ToArray();

            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getEmigreForMenage" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne les individus a l'interieur d'un Menage
        /// </summary>
        /// <param Name="_model"></param>
        /// <returns>MenageDetailsModel[]</returns>
        public MenageDetailsModel[] getIndividuForMenage(MenageModel _model)
        {
            try
            {
                if (Sr == null)
                {
                    Sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, _model.SdeId));
                }
                return Sr.GetIndividuByMenageDetails(_model.MenageId).ToArray();
            }
            catch (Exception ex)
            {
                log.Info("SqliteDataReaderService/getIndividuForMenage" + ex.Message);
            }
            return null;
        }
        #region QUESTIONS
        public tbl_question getQuestion(string codeQuestion, string sdeId)
        {
            if (Sr == null)
            {
                Sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));
            }
            return Sr.getQuestion(codeQuestion);
        }
        public string getReponse(string codeQuestion, string codeReponse, string sdeId)
        {
            if (Sr == null)
            {
                Sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));
            }
            return Sr.getReponse(codeQuestion, codeReponse);
        }
        public List<tbl_question_module> listOfQuestionModule(string codeModule, string sdeId)
        {
            if (Sr == null)
            {
                Sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));
            }
            return Sr.listOfQuestionModule(codeModule);
        }

        public string libelleCategorie(string codeCategorie, string sdeId)
        {
            if (Sr == null)
            {
                Sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));
            }
            return Sr.getLibelleCategorie(codeCategorie);
        }
        public tbl_categorie_question getCategorie(string codeCategorie, string sdeId)
        {
            if (Sr == null)
            {
                Sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));
            }
            return Sr.getCategorie(codeCategorie);
        }
        #endregion
       

        public  int RandomNumber(int min, int max)
        {
                         
            lock (syncLock)
            { // synchronize
                if (numbers.Count != 0)
                {
                    int num = random.Next(min, max);
                    foreach (int n in numbers)
                    {
                        if (num != n)
                            return num;                        
                    }
                }
                else
                {
                    int num = random.Next(min, max);
                    numbers.Add(num);
                    return num;
                }
            }
            return 0;
        }

       
    }
}
