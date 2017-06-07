using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsi.Rgph.DataAccess.Repositories;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Ht.Ihsil.Rgph.App.Superviseur.Json;

namespace Ht.Ihsil.Rgph.App.Superviseur.utils
{
    public class SqliteReader : ISqliteReader
    {
        #region DECLARATIONS
        private MainRepository repository;

        Logger log;
        #endregion

        #region CONSTRUCTORS
        public SqliteReader(string connectionString)
        {
            repository = new MainRepository(connectionString);
            log = new Logger();
        }
        public MainRepository Repository(string connectionString)
        {
            if (repository == null)
            {
                return new MainRepository(connectionString);
            }
            return repository;
        }
        #endregion

        #region Retrieving data for BATIMENTS
        /// <summary>
        /// Retourne les batiments pour les transformer en Json
        /// </summary>
        /// <returns></returns>
        public List<BatimentJson> GetAllBatimentsInJson()
        {
            List<BatimentJson> listOfJsons = new List<BatimentJson>();

            List<BatimentModel> listsOfBatimentModels = GetAllBatimentModel();
            foreach (BatimentModel bat in listsOfBatimentModels)
            {
                BatimentJson batJson = ModelMapper.MapToJson(bat);
                //Recherche de logement (Collectif ou indivi) pour un batiment
                #region RECHERCHE DE LOGEMENTS COLLECTIFS
                batJson.logementCs = new List<LogementCJson>();
                List<LogementCJson> logementsCollectifs = ModelMapper.MapToListLCJson(GetLogementCByBatiment(bat.BatimentId));
                foreach (LogementCJson logCJson in logementsCollectifs)
                {
                    List<IndividuJson> individusCollectifs = ModelMapper.MapToListJson(GetIndividuByLoge(logCJson.logeId));
                    if (individusCollectifs.Count != 0)
                    {
                        logCJson.individus = new List<IndividuJson>();
                        logCJson.individus = individusCollectifs;
                    }
                    batJson.logementCs.Add(logCJson);
                }
                #endregion

                #region RECHERCHE DE LOGEMENTS INDIVIDUELS
                List<LogementIsJson> logModels = ModelMapper.MapToListLIJson(GetLogementIByBatiment(bat.BatimentId));
                if (logModels != null)
                {
                    foreach (LogementIsJson logm in logModels)
                    {
                        //Recherche de menage(s) dans le logement
                        LogementIsJson logJson = logm;
                        List<MenageModel> menModels = GetMenageByLogement(logm.logeId);
                        if (menModels != null)
                        {
                            List<MenageJson> menages = new List<MenageJson>();
                            foreach (MenageModel men in menModels)
                            {
                                MenageJson menageJson = ModelMapper.MapToJson(men);
                                //Recherche des emigres
                                List<EmigreModel> emModels = ModelMapper.MapToListEmigre(repository.MEmigreRepository.Find(em => em.menageId == men.MenageId).ToList());
                                if (emModels != null)
                                {
                                    menageJson.emigres = new List<EmigreJson>();
                                    foreach (EmigreModel em in emModels)
                                    {
                                        EmigreJson emigreJson = ModelMapper.MapToJson(em);
                                        menageJson.emigres.Add(emigreJson);
                                    }
                                }
                                //Recherche de Deces
                                List<DecesModel> decModels = GetDecesByMenage(men.MenageId).ToList();
                                if (decModels != null)
                                {
                                    menageJson.deces = new List<DecesJson>();
                                    menageJson.deces = ModelMapper.MapToListJson(decModels);
                                }
                                //Recherche des individus
                                List<IndividuModel> indModels = GetIndividuByMenage(men.MenageId);
                                if (indModels != null)
                                {
                                    menageJson.individus = new List<IndividuJson>();
                                    menageJson.individus = ModelMapper.MapToListJson(indModels);
                                }
                                menages.Add(menageJson);
                            }
                            logJson.menages = new List<MenageJson>();
                            logJson.menages = menages;
                            batJson.logementIs = new List<LogementIsJson>();
                            batJson.logementIs.Add(logJson);
                        }
                    }
                }
                #endregion

                listOfJsons.Add(batJson);
            }
            return listOfJsons;
        }
        /// <summary>
        /// Retourne tous les batiments ayant au moins un objet vide
        /// </summary>
        /// <returns></returns>
        public List<BatimentModel> GetAllBatimentsWithAtLeastOneBlankObject()
        {
            try
            {
                List<BatimentModel> finalBatimentsList = new List<BatimentModel>();
                List<BatimentModel> batiments = GetAllBatimentNotFinished();
                //Recherche les logements dans ces batiments
                foreach (BatimentModel bat in batiments)
                {
                    //Recherche des logements par batiment
                    List<LogementModel> getLogements = GetLogementIByBatiment(bat.BatimentId);
                    if (getLogements != null)
                    {
                        foreach (LogementModel logement in getLogements)
                        {
                            if (logement.Qlin2StatutOccupation == 2 || logement.Qlin2StatutOccupation == 3)
                            {
                                finalBatimentsList.Add(bat);
                            }
                        }
                    }
                }
                return finalBatimentsList;
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllBatimentsWithAtLeastOneBlankObject" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne tous les batiments qui sont inobservables (modalite=5)
        /// </summary>
        /// <returns></returns>
        public List<BatimentModel> GetAllBatimentsInobservables()
        {
            try
            {
                return ModelMapper.MapToListBatimentModel(repository.MBatimentRepository.Find(b => b.qb1Etat == (int)Constant.EtatBatiment.Inobservable).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllBatimentsInobservables" + ex.Message);
            }
            return null;
        }


        /// <summary>
        /// Get all Batiments
        /// </summary>
        /// <returns>List<BatimentModel></returns>
        public List<BatimentModel> GetAllBatimentModel()
        {
            try
            {
                return ModelMapper.MapToListBatimentModel(repository.MBatimentRepository.Find().ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllBatimentModel" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Get a batiment by its ID
        /// </summary>
        /// <param name="BatimentId"></param>
        /// <returns>BatimentModel</returns>
        public BatimentModel GetBatimentbyId(long batimentId)
        {
            try
            {
                return ModelMapper.MapToBatiment(repository.MBatimentRepository.Find(b => b.batimentId == batimentId).FirstOrDefault());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetBatimentbyId" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Get only Batiments With Logements Vides
        /// </summary>
        /// <returns>List<BatimentModel></returns>
        public List<BatimentModel> GetAllBatimentWithLogVide()
        {
            try
            {
                var listOfBatiments = from bat in repository.MBatimentRepository.Find().ToList()
                                      join lg in repository.MLogementRepository.Find().ToList()
                                      on bat.batimentId equals lg.batimentId
                                      where lg.qlin2StatutOccupation == Convert.ToInt32(3)
                                      select bat;
                return ModelMapper.MapToListBatimentModel(listOfBatiments.ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllBatimentWithLogVide" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retourne tous les batiments finis qui ont des logements individuels
        /// </summary>
        /// <returns></returns>
        public List<BatimentModel> GetAllBatimentFinishedWithLogInd()
        {
            try
            {
                List<tbl_menage> menages = repository.MMenageRepository.Find(m => m.qm11TotalIndividuVivant > 0).ToList();
                List<BatimentModel> batiments = new List<BatimentModel>();
                foreach (tbl_menage men in menages)
                {
                    BatimentModel bat = GetBatimentbyId(men.batimentId.GetValueOrDefault());
                    if (bat.Statut == Constant.STATUT_MODULE_KI_FINI_1)
                        batiments.Add(bat);
                }
                return batiments;
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllBatimentWithLogInd" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Get only Batiments With Logements individuels
        /// </summary>
        /// <returns></returns>
        public List<BatimentModel> GetAllBatimentWithLogInd()
        {
            try
            {
                List<tbl_menage> menages = repository.MMenageRepository.Find(m => m.qm11TotalIndividuVivant > 0).ToList();
                List<BatimentModel> batiments = new List<BatimentModel>();
                foreach (tbl_menage men in menages)
                {
                    BatimentModel bat = GetBatimentbyId(men.batimentId.GetValueOrDefault());
                    batiments.Add(bat);
                }
                return batiments;
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllBatimentWithLogInd" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Get Batiment with Logements Collectifs
        /// </summary>
        /// <returns>List<BatimentModel></returns>
        public List<BatimentModel> GetABatimentWithLogC()
        {
            try
            {
                var listOfBatiments = from bat in repository.MBatimentRepository.Find().ToList()
                                      join lg in repository.MLogementRepository.Find().ToList()
                                      on bat.batimentId equals lg.batimentId
                                      where lg.qlCategLogement == 1
                                      select bat;
                return ModelMapper.MapToListBatimentModel(listOfBatiments.Distinct().ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetABatimentWithLogC" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retourne seulement les batiments vides
        /// </summary>
        /// <returns>List<BatimentModel></returns>
        public List<BatimentModel> GetAllBatimentVide()
        {
            try
            {
                List<tbl_batiment> batiments = repository.MBatimentRepository.Find(b => b.qb6StatutOccupation == 2).ToList();
                IEnumerable<tbl_batiment> listOfBatiments = batiments.GroupBy(b => b.batimentId).Select(group => group.First());
                return ModelMapper.MapToListBatimentModel(listOfBatiments.ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllBatimentVide" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retourne les batiments qui ont logments, des menages et des deces
        /// </summary>
        /// <returns>List<BatimentModel></returns>
        public List<BatimentModel> GetBatLogMenWithDeces()
        {
            try
            {
                var listOfBatiments = from bat in repository.MBatimentRepository.Find().ToList()
                                      join lg in repository.MLogementRepository.Find().ToList()
                                       on bat.batimentId equals lg.batimentId
                                      join men in repository.MMenageRepository.Find().ToList()
                                      on lg.logeId equals men.logeId
                                      join dec in repository.MDecesRepository.Find().ToList()
                                      on men.menageId equals dec.menageId
                                      select bat;
                return ModelMapper.MapToListBatimentModel(listOfBatiments.ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetBatLogMenWithDeces" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Gets a List of Batiments Mal rempli
        /// </summary>
        /// <returns>List<BatimentType></returns>
        public List<BatimentType> GetAllBatimentMalRempli()
        {
            List<BatimentType> result = new List<BatimentType>();
            try
            {
                List<BatimentModel> batiments = ModelMapper.MapToListBatimentModel(repository.MBatimentRepository.Find(b => b.statut == (int)Constant.StatutModule.MalRempli).ToList());
                foreach (BatimentModel bat in batiments)
                {
                    BatimentType batType = new BatimentType();
                    batType.batimentId = Convert.ToInt32(bat.BatimentId);
                    batType.sdeId = bat.SdeId;
                    if (!isBatimentAlreadySent(batType.sdeId, batType.batimentId))
                        result.Add(batType);
                }
            }
            catch (Exception ex)
            {
                log.Info("SqliteReqder/GetAllBatimentTermine:" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Gets a List of Batiments Not Finish
        /// </summary>
        /// <returns>List<BatimentType></returns>
        public List<BatimentType> GetAllBatimentPasFini()
        {
            List<BatimentType> result = new List<BatimentType>();
            try
            {
                List<BatimentModel> batiments = ModelMapper.MapToListBatimentModel(repository.MBatimentRepository.Find(b => b.statut == Constant.STATUT_MODULE_KI_PA_FINI_3).ToList());
                foreach (BatimentModel bat in batiments)
                {
                    BatimentType batType = new BatimentType();
                    batType.batimentId = Convert.ToInt32(bat.BatimentId);
                    batType.sdeId = bat.SdeId;
                    //Test si le batiment est deja envoye
                    if (!isBatimentAlreadySent(batType.sdeId, batType.batimentId))
                        result.Add(batType);
                }
            }
            catch (Exception ex)
            {
                log.Info("SqliteReqder/GetAllBatimentTermine:" + ex.Message);
            }
            return result;
        }
        /// <summary>
        /// Retourne Tous les batiments pas finis
        /// </summary>
        /// <returns></returns>
        /// 
        public List<BatimentModel> GetAllBatimentNotFinished()
        {
            List<BatimentModel> result = new List<BatimentModel>();
            try
            {
                result = ModelMapper.MapToListBatimentModel(repository.MBatimentRepository.Find(b => b.statut == (int)Constant.StatutModule.PasFini).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReqder/GetAllBatimentNotFinished:" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Gets a List of Batiments Finish
        /// </summary>
        /// <returns>List<BatimentType></returns>
        public List<BatimentType> GetAllBatimentTermine()
        {
            List<BatimentType> result = new List<BatimentType>();
            try
            {
                List<BatimentModel> batiments = ModelMapper.MapToListBatimentModel(repository.MBatimentRepository.Find(b => b.statut == (int)Constant.StatutModule.Fini).ToList());
                foreach (BatimentModel bat in batiments)
                {
                    BatimentType batType = new BatimentType();
                    batType.batimentId = Convert.ToInt32(bat.BatimentId);
                    batType.sdeId = bat.SdeId;
                    if (!isBatimentAlreadySent(batType.sdeId, batType.batimentId))
                        result.Add(batType);
                }
            }
            catch (Exception ex)
            {
                log.Info("SqliteReqder/GetAllBatimentTermine:" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Gets a List of Batiments to send to the RestFull Service
        /// </summary>
        /// <returns>List<BatimentType></returns>
        public List<BatimentType> GetAllBatimentType()
        {
            List<BatimentType> result = new List<BatimentType>();
            try
            {
                List<BatimentModel> batiments = ModelMapper.MapToListBatimentModel(repository.MBatimentRepository.Find().ToList());
                foreach (BatimentModel bat in batiments)
                {
                    BatimentType batType = new BatimentType();
                    batType.batimentId = Convert.ToInt32(bat.BatimentId);
                    batType.sdeId = bat.SdeId;
                    //if (!isBatimentAlreadySent(batType.SdeId, batType.BatimentId))
                    result.Add(batType);
                }
            }
            catch (Exception ex)
            {
                log.Info("SqliteReqder/GetAllBatimentType:" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Gets a  Batiment to send to the RestFull Service
        /// </summary>
        /// <param name="BatimentId"></param>
        /// <returns>BatimentType</returns>
        public BatimentType ReadBatimentType(long batimentId)
        {
            BatimentType resultat = null;
            try
            {
                tbl_batiment batiment = repository.MBatimentRepository.Find(b => b.batimentId == batimentId).FirstOrDefault();
                if (batiment != null)
                {
                    int i = 0;
                    resultat = WsModelMapper.MapReaderToBatimentType(batiment);

                    /**
                     * Lecture les logements collectifs
                     */
                    List<tbl_logement> logementsC = repository.MLogementRepository.Find(l => l.batimentId == batiment.batimentId && l.qlCategLogement == Constant.TYPE_LOJMAN_KOLEKTIF).ToList();
                    int lcount = logementsC.Count;
                    if (lcount > 0)
                    {
                        int iLogC = 0;
                        resultat.logementCs = new LogementCType[lcount];
                        foreach (tbl_logement logement in logementsC)
                        {
                            LogementCType log = new LogementCType();
                            log = WsModelMapper.MapReaderToLogementCollectifType(logement);
                            //resultat.statisticCheck.nombreLogeColletif++;

                            /*
                             * Lecture des individus des logements collectifs
                             */
                            List<tbl_individu> individusC = repository.MIndividuRepository.Find(ind => ind.logeId == logement.logeId).ToList();
                            lcount = individusC.Count;
                            if (lcount > 0)
                            {
                                log.individus = new IndividuType[lcount];
                                i = 0;
                                foreach (tbl_individu individus in individusC)
                                {
                                    log.individus[i] = WsModelMapper.MapReaderToIndividuType(individus);
                                    //resultat.statisticCheck.nombreInvididuLC++;
                                    i++;
                                }
                            }
                            resultat.logementCs[iLogC] = log;
                            iLogC++;
                        }
                    }

                    /**
                     * Lecture les logements Individuels
                     */
                    List<tbl_logement> logementsI = repository.MLogementRepository.Find(l => l.batimentId == batiment.batimentId && l.qlCategLogement == Constant.TYPE_LOJMAN_ENDIVIDYEL).ToList();
                    lcount = logementsI.Count;
                    if (lcount > 0)
                    {
                        resultat.logementIs = new LogementIType[lcount];
                        int iLogI = 0;
                        foreach (tbl_logement logI in logementsI)
                        {
                            LogementIType logInd = WsModelMapper.MapReaderToLogementIndType(logI);
                            //resultat.statisticCheck.nombreLogeIndividuel++;
                            /*
                             * Lecture des menages
                             */
                            List<tbl_menage> menages = repository.MMenageRepository.Find(m => m.logeId == logI.logeId).ToList();
                            lcount = menages.Count;
                            if (lcount > 0)
                            {
                                logInd.menages = new MenageType[lcount];
                                int iMenage = 0;
                                foreach (tbl_menage men in menages)
                                {
                                    MenageType mt = WsModelMapper.MapReaderToMenageType(men);
                                    //resultat.statisticCheck.nombreMenage++;

                                    /*
                                     * Lecture des personnes decedees 
                                     */
                                    List<tbl_deces> deces = repository.MDecesRepository.Find(d => d.menageId == men.menageId).ToList();
                                    lcount = deces.Count;
                                    if (lcount > 0)
                                    {
                                        mt.deces = new DecesType[lcount];
                                        i = 0;
                                        foreach (tbl_deces dec in deces)
                                        {
                                            mt.deces[i] = WsModelMapper.MapReaderToDecesType(dec);
                                            //resultat.statisticCheck.nombreDeces++;
                                            i++;
                                        }
                                    }

                                    /*
                                     * Lecture des emigres
                                     */
                                    List<tbl_emigre> emigres = repository.MEmigreRepository.Find(m => m.menageId == men.menageId).ToList();
                                    lcount = emigres.Count;
                                    if (lcount > 0)
                                    {
                                        mt.emigres = new EmigreType[lcount];
                                        i = 0;
                                        foreach (tbl_emigre em in emigres)
                                        {
                                            mt.emigres[i] = WsModelMapper.MapReaderToEmigreType(em);
                                            //resultat.statisticCheck.nombreEmigre++;
                                            i++;
                                        }
                                    }

                                    /*
                                     * Lecture des individus
                                     */
                                    List<tbl_individu> individus = repository.MIndividuRepository.Find(ind => ind.menageId == men.menageId).ToList();
                                    lcount = individus.Count;
                                    if (lcount > 0)
                                    {
                                        mt.individus = new IndividuType[lcount];
                                        i = 0;
                                        foreach (tbl_individu individu in individus)
                                        {
                                            mt.individus[i] = WsModelMapper.MapReaderToIndividuType(individu);
                                            //resultat.statisticCheck.nombreInvididuLI++;
                                            i++;
                                        }
                                    }

                                    //ajouter Menage dans Logement
                                    logInd.menages[iMenage] = mt;
                                    iMenage++;
                                }
                            }

                            resultat.logementIs[iLogI] = logInd;
                            iLogI++;
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                log.Info("<>============Error : " + ex.Message);
            }

            return resultat;
        }

        /// <summary>
        /// Determine if a batiment is already sent
        /// </summary>
        /// <param name="SdeId"></param>
        /// <param name="batId"></param>
        /// <returns>bool</returns>
        public bool isBatimentAlreadySent(string sdeId, long batId)
        {
            MainRepository rep = new MainRepository(sdeId);
            tbl_batiment result = rep.MBatimentRepository.Find(d => d.batimentId == batId).FirstOrDefault();
            if (result.isSynchroToCentrale.GetValueOrDefault() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Retrieving data for other objects like DECES, EMIGRE,INDIVIDU

        /// <summary>
        /// Retourne tous les menages
        /// </summary>
        /// <returns></returns>
        public List<MenageModel> GetAllMenages()
        {
            try
            {
                return ModelMapper.MapToListMenageModel(repository.MMenageRepository.Find().ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllMenages" + ex.Message);
            }
            return new List<MenageModel>();
        }

        /// <summary>
        /// Retrouve les menages qui ne sont pas encore termines
        /// </summary>
        /// <returns></returns>
        public List<MenageModel> GetAllMenageNotFinish()
        {
            try
            {
                return ModelMapper.MapToListMenageModel(repository.MMenageRepository.Find(m => m.isFieldAllFilled == 0).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllMenageNotFinish" + ex.Message);
            }
            return new List<MenageModel>();
        }
        /// <summary>
        /// Retourne un menage a partir de son ID
        /// </summary>
        /// <param name="menageId"></param>
        /// <returns></returns>
        public MenageModel GetMenageById(long menageId)
        {
            try
            {
                return ModelMapper.MapToMenage(repository.MMenageRepository.Find(m => m.menageId == menageId).FirstOrDefault());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetMenageById" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Gets all finished menages in a logement
        /// </summary>
        /// <param name="logId"></param>
        /// <returns>List<MenageModel></returns>
        public List<MenageModel> GetMenageFiniByLogement(long logId)
        {
            try
            {
                return ModelMapper.MapToListMenageModel(repository.MMenageRepository.Find(m => m.logeId == logId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetMenageFiniByLogement" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retourne les menages a partir d'un logement
        /// </summary>
        /// <param name="logId"></param>
        /// <returns>List<MenageModel></returns>
        public List<MenageModel> GetMenageByLogement(long logId)
        {
            try
            {
                return ModelMapper.MapToListMenageModel(repository.MMenageRepository.Find(m => m.logeId == logId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetMenageByLogement" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retourne les deces par menage selectionne
        /// </summary>
        /// <param name="menageId"></param>
        /// <returns>List<DecesModel></returns>
        public List<DecesModel> GetDecesByMenage(long menageId)
        {
            try
            {
                return ModelMapper.MapToListDeces(repository.MDecesRepository.Find(m => m.menageId == menageId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetDecesByMenage" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retourne les individus par menage selectionne
        /// </summary>
        /// <param name="menageId"></param>
        /// <returns></returns>
        public List<IndividuModel> GetIndividuByMenage(long menageId)
        {
            try
            {
                return ModelMapper.MapToListIndividu(repository.MIndividuRepository.Find(m => m.menageId == menageId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetIndividuByMenage" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retourne les emigres par menage selectionnes
        /// </summary>
        /// <param name="menageId"></param>
        /// <returns></returns>
        public List<MenageDetailsModel> GetEmigreByMenageDetails(long menageId)
        {
            try
            {
                List<EmigreModel> emigres = ModelMapper.MapToListEmigre(repository.MEmigreRepository.Find(d => d.menageId == menageId).ToList());
                if (emigres != null)
                {
                    List<MenageDetailsModel> models = new List<MenageDetailsModel>();
                    foreach (EmigreModel em in emigres)
                    {
                        MenageDetailsModel menageDetails = ModelMapper.MapToMenageDetails<EmigreModel>(em);
                        models.Add(menageDetails);
                    }
                    return models;
                }
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetEmigreByMenageDetails" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retourne les deces par menage selectionne
        /// </summary>
        /// <param name="menageId"></param>
        /// <returns>List<MenageDetailsModel></returns>
        public List<MenageDetailsModel> GetDecesByMenageDetails(long menageId)
        {
            try
            {
                List<DecesModel> deces = ModelMapper.MapToListDeces(repository.MDecesRepository.Find(d => d.menageId == menageId).ToList());
                if (deces != null)
                {
                    List<MenageDetailsModel> models = new List<MenageDetailsModel>();
                    foreach (DecesModel dec in deces)
                    {
                        MenageDetailsModel menageDetails = ModelMapper.MapToMenageDetails<DecesModel>(dec);
                        models.Add(menageDetails);
                    }
                    return models;
                }
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetDecesByMenageDetails" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retourne les individus par menage selectionne
        /// </summary>
        /// <param name="menageId"></param>
        /// <returns>List<MenageDetailsModel></returns>
        public List<MenageDetailsModel> GetIndividuByMenageDetails(long menageId)
        {
            try
            {
                List<IndividuModel> individus = ModelMapper.MapToListIndividu(repository.MIndividuRepository.Find(d => d.menageId == menageId).ToList());
                if (individus != null)
                {
                    List<MenageDetailsModel> models = new List<MenageDetailsModel>();
                    foreach (IndividuModel ind in individus)
                    {
                        MenageDetailsModel menageDetails = ModelMapper.MapToMenageDetails<IndividuModel>(ind);
                        models.Add(menageDetails);
                    }
                    return models;
                }
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetIndividuByMenageDetails" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retourne les individus par logements
        /// </summary>
        /// <param name="logeId"></param>
        /// <returns>List<IndividuModel></returns>
        public List<IndividuModel> GetIndividuByLoge(long logeId)
        {
            try
            {
                return ModelMapper.MapToListIndividu(repository.MIndividuRepository.Find(ind => ind.logeId == logeId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetIndividuByLoge" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Get a Deces by ID
        /// </summary>
        /// <param name="decesId"></param>
        /// <returns>DecesModel</returns>
        public DecesModel GetDecesById(long decesId)
        {
            try
            {
                return ModelMapper.MapToDeces(repository.MDecesRepository.FindOne(decesId));
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetDecesById" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Get an Emigre By ID
        /// </summary>
        /// <param name="emigreId"></param>
        /// <returns>EmigreModel</returns>
        public EmigreModel GetEmigreById(int emigreId)
        {
            try
            {
                return ModelMapper.MapToEmigre(repository.MEmigreRepository.Find(e => e.emigreId == emigreId).FirstOrDefault());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetEmigreById" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Get an Individu by ID
        /// </summary>
        /// <param name="indId"></param>
        /// <returns>IndividuModel</returns>
        public IndividuModel GetIndividuById(long indId)
        {
            try
            {
                return ModelMapper.MapToIndividu(repository.MIndividuRepository.Find(i => i.individuId == indId).FirstOrDefault());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetIndividuById" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retrouve les individus qui ne sont pas encore termines
        /// </summary>
        /// <returns></returns>
        public List<IndividuModel> GetAllIndividuNotFinish()
        {
            try
            {
                return ModelMapper.MapToListIndividu(repository.MIndividuRepository.Find(i => i.isFieldAllFilled == 0).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllIndividuNotFinish" + ex.Message);
            }
            return new List<IndividuModel>();
        }

        /// <summary>
        /// Retourne tous les individus dans la SDE
        /// </summary>
        /// <returns>List IndividuModel</returns>
        public List<IndividuModel> GetAllIndividus()
        {
            try
            {
                return ModelMapper.MapToListIndividu(repository.MIndividuRepository.Find().ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllIndividus" + ex.Message);
            }
            return new List<IndividuModel>();
        }
        /// <summary>
        /// Retourne les individus sans l'age et la date de naissance
        /// </summary>
        /// <returns> List IndividuModel </returns>
        public List<IndividuModel> GetAllIndividusWithoutAgeAndBirthDay()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retourne les individus sans la date de naissance
        /// </summary>
        /// <returns>List IndividuModel</returns>
        public List<IndividuModel> GetAllIndividusWithoutBirthDay()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retourne les individus sans l'age
        /// </summary>
        /// <returns>List IndividuModel</returns>
        public List<IndividuModel> GetAllIndividusWithoutAge()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retourne les individus de 3 ans sans le nivseau d'etude
        /// </summary>
        /// <returns>List IndividuModel</returns>
        public List<IndividuModel> GetAllIndividus3ansWithoutNiveauEtude()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retourne toutes les femmes de 13 ans sans de garcons et de filles nes vivants
        /// </summary>
        /// <returns>List IndividuModel</returns>
        public List<IndividuModel> GetAllIndividusFemmes13ansSansFGNesVivants()
        {
            throw new NotImplementedException();
        }



        #endregion

        #region CODIFICATION ET COMPTEUR DE FLAG
        public Codification getInformationForCodification()
        {
            string methodName = "getInformationForCodification";
            Codification _aCodififer = new Codification();
            try
            {
                List<IndividuModel> listOfIndividus = ModelMapper.MapToListIndividu(repository.MIndividuRepository.Find().ToList());
                if (listOfIndividus.Count != 0)
                {
                    foreach (IndividuModel ind in listOfIndividus)
                    {

                        #region Recherche des individus avec la P10
                        //P10=1
                        if (ind.Qp10LieuNaissance == 1 && ind.Statut == (int)Constant.StatutModule.Fini)
                        {
                            if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
                            {
                                _aCodififer.P10_1 += 1;
                            }
                        }
                        //p10=2
                        if (ind.Qp10LieuNaissance == 2 && ind.Statut == (int)Constant.StatutModule.Fini)
                        {
                            if (ind.Qp10CommuneNaissance == Constant.PA_KONNEN_KOMIN_OU_PEYI || ind.Qp10VqseNaissance == Constant.PA_KONNEN_SEKSYON_KOMINAL)
                            {
                                if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
                                {
                                    _aCodififer.P10_2 += 1;
                                }
                            }

                        }
                        //p10=3
                        if (ind.Qp10LieuNaissance == 3 && ind.Statut == (int)Constant.StatutModule.Fini)
                        {
                            if (ind.Qp10PaysNaissance == Constant.PA_KONNEN_KOMIN_OU_PEYI)
                            {
                                if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
                                {
                                    _aCodififer.P10_3 += 1;
                                }
                            }
                        }
                        //p10=4
                        if (ind.Qp10LieuNaissance == 4 && ind.Statut == (int)Constant.StatutModule.Fini)
                        {
                            if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
                            {
                                _aCodififer.P10_4 += 1;
                            }
                        }
                        #endregion

                        #region Recherche des individus avec la variable P12
                        //p12=1
                        if (ind.Qp12DomicileAvantRecensement == 1 && ind.Statut == (int)Constant.StatutModule.Fini)
                        {
                            if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
                            {
                                _aCodififer.P12_1 += 1;
                            }
                        }
                        //p12=2
                        if (ind.Qp12DomicileAvantRecensement == 2 && ind.Statut == (int)Constant.StatutModule.Fini)
                        {
                            if (ind.Qp12CommuneDomicileAvantRecensement == Constant.PA_KONNEN_KOMIN_OU_PEYI || ind.Qp12VqseDomicileAvantRecensement == Constant.PA_KONNEN_SEKSYON_KOMINAL)
                            {
                                if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
                                {
                                    _aCodififer.P12_2 += 1;
                                }
                            }
                        }
                        //p12=3
                        if (ind.Qp12DomicileAvantRecensement == 3 && ind.Statut == (int)Constant.StatutModule.Fini)
                        {
                            if (ind.Qp12PaysDomicileAvantRecensement == Constant.PA_KONNEN_KOMIN_OU_PEYI)
                            {
                                if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
                                {
                                    _aCodififer.P12_3 += 1;
                                }
                            }
                        }
                        //p12=4
                        if (ind.Qp12DomicileAvantRecensement == 4 && ind.Statut == (int)Constant.StatutModule.Fini)
                        {
                            if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
                            {
                                _aCodififer.P12_4 += 1;
                            }
                        }
                        #endregion

                        #region Recherche des individus avec la variavle A5
                        int typeBien = Convert.ToInt32(ind.Qa5TypeBienProduitParEntreprise);
                        //A5 est codifie
                        if (typeBien < 40)
                        {
                            _aCodififer.A5Codifie += 1;
                        }
                        //A5 =autre
                        if (typeBien == 40)
                        {
                            _aCodififer.A5Autre += 1;
                        }
                        //A5 ne sait pas
                        if (typeBien == 41)
                        {
                            _aCodififer.A5NeSaitPas += 1;
                        }
                        #endregion

                        #region Recherche des individus avec la variavle A7
                        int typeActivite = Convert.ToInt32(ind.Qa7FoncTravail);
                        //A7 est bien codifiee
                        if (typeActivite < 132)
                        {
                            _aCodififer.A7Codifie += 1;
                        }
                        //A7 est codifiee a autre
                        if (typeActivite == 132)
                        {
                            _aCodififer.A7Autre += 1;
                        }
                        //A7 est codifiee a ne sait pas
                        if (typeActivite == 133)
                        {
                            _aCodififer.A7NeSaitPas += 1;
                        }
                        #endregion
                    }
                    return _aCodififer;
                }
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + ":" + ex.Message);
            }
            return new Codification();
        }
        public Flag CountTotalFlag()
        {
            string methodName = "CountFlag";
            try
            {
                List<IndividuModel> listOfIndividus = GetAllIndividus();
                Flag compteur = new Flag();


                foreach (IndividuModel ind in listOfIndividus)
                {
                    int numberOfProblems = 0;
                    #region Test date de naissance/age
                    if (ind.Qp5DateNaissanceAnnee == 9999 && ind.Qp5DateNaissanceJour == 99 && ind.Qp5DateNaissanceMois == 99)
                    {
                        numberOfProblems += 1;
                    }
                    if (ind.Qp5DateNaissanceAnnee == 9999 && ind.Qp5DateNaissanceJour == 99 && ind.Qp5DateNaissanceMois == 99 && ind.Qp5bAge != 999)
                    {
                        numberOfProblems += 1;
                    }
                    if (ind.Qp5DateNaissanceAnnee != 9999 && ind.Qp5DateNaissanceJour != 99 && ind.Qp5DateNaissanceMois != 99 && ind.Qp5bAge == 999)
                    {
                        numberOfProblems += 1;
                    }
                    #endregion

                    #region Niveau d'etude
                    if (ind.Qe4aNiveauEtude >= 3 && ind.Qe4aNiveauEtude < 9 && ind.Qe4bDerniereClasseOUAneEtude == "99")
                    {
                        numberOfProblems += 1;
                    }
                    #endregion

                    #region Fecondite
                    if (ind.Qp4Sexe == (int)Constant.Sexe.Fi && ind.Qp5bAge >= 13 && ind.Qf1aNbreEnfantNeVivantM == 99)
                    {
                        numberOfProblems += 1;
                    }
                    if (ind.Qp4Sexe == (int)Constant.Sexe.Fi && ind.Qp5bAge >= 13 && ind.Qf1bNbreEnfantNeVivantF == 99)
                    {
                        numberOfProblems += 1;
                    }
                    if (ind.Qp4Sexe == (int)Constant.Sexe.Fi && ind.Qp5bAge >= 13 && ind.Qf1bNbreEnfantNeVivantF == 99 && ind.Qf1aNbreEnfantNeVivantM == 99)
                    {
                        numberOfProblems += 1;
                    }
                    #endregion

                    #region Information sur la date de naissance du premier enfant
                    if (ind.Qp4Sexe == (int)Constant.Sexe.Fi && ind.Qp5bAge >= 13 && ind.Qf3DernierEnfantAnnee == 9999 && ind.Qf3DernierEnfantJour == 99 && ind.Qf3DernierEnfantMois == 99)
                    {
                        numberOfProblems += 1;
                    }
                    #endregion

                    #region Activite Economique
                    if (ind.Qp5bAge >= 10 && ind.Qa5TypeBienProduitParEntreprise == "99")
                    {
                        numberOfProblems += 1;
                    }
                    if (ind.Qp5bAge >= 10 && ind.Qa7FoncTravail == 99)
                    {
                        numberOfProblems += 1;
                    }
                    if (ind.Qp5bAge >= 10 && ind.Qa8EntreprendreDemarcheTravail == 10)
                    {
                        numberOfProblems += 1;
                    }
                    #endregion

                    #region Test de coherence Age/Niveau Etude
                    if (ind.Qp5bAge >= 3 && ind.Qp5bAge < 6 && ind.Qe4aNiveauEtude > 3)
                    {
                        numberOfProblems += 1;
                    }
                    if (ind.Qp5bAge >= 6 && ind.Qp5bAge < 9 && ind.Qe4aNiveauEtude > 5)
                    {
                        numberOfProblems += 1;
                    }
                    #endregion

                    #region DECOMPTE
                    if (numberOfProblems == 0)
                        compteur.Flag0 += 1;
                    if (numberOfProblems == 1)
                        compteur.Flag1 += 1;
                    if (numberOfProblems == 2)
                        compteur.Flag2 += 1;
                    if (numberOfProblems == 3)
                        compteur.Flag3 += 1;
                    if (numberOfProblems == 4)
                        compteur.Flag4 += 1;
                    if (numberOfProblems == 5)
                        compteur.Flag5 += 1;
                    if (numberOfProblems == 6)
                        compteur.Flag6 += 1;
                    if (numberOfProblems == 7)
                        compteur.Flag7 += 1;
                    if (numberOfProblems == 8)
                        compteur.Flag8 += 1;
                    if (numberOfProblems == 9)
                        compteur.Flag9 += 1;
                    if (numberOfProblems == 10)
                        compteur.Flag10 += 1;
                    if (numberOfProblems == 11)
                        compteur.Flag11 += 1;
                    if (numberOfProblems == 12)
                        compteur.Flag12 += 1;
                    if (numberOfProblems == 13)
                        compteur.Flag13 += 1;
                    #endregion
                }
                return compteur;

            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + ":" + ex.Message);
            }
            return new Flag();
        }
        public Flag Count2FlagAgeDateNaissance()
        {
            string methodName = "Count2FlagAgeDateNaissance";
            try
            {
                List<IndividuModel> listOfIndividus = GetAllIndividus();
                Flag compteur = new Flag();


                foreach (IndividuModel ind in listOfIndividus)
                {
                    int numberOfProblems = 0;
                    #region Test date de naissance/age
                    if (ind.Qp5DateNaissanceAnnee == 9999 && ind.Qp5DateNaissanceJour == 99 && ind.Qp5DateNaissanceMois == 99)
                    {
                        numberOfProblems += 1;
                    }
                    if (ind.Qp5DateNaissanceAnnee == 9999 && ind.Qp5DateNaissanceJour == 99 && ind.Qp5DateNaissanceMois == 99 && ind.Qp5bAge != 999)
                    {
                        numberOfProblems += 1;
                    }
                    if (ind.Qp5DateNaissanceAnnee != 9999 && ind.Qp5DateNaissanceJour != 99 && ind.Qp5DateNaissanceMois != 99 && ind.Qp5bAge == 999)
                    {
                        numberOfProblems += 1;
                    }
                    #endregion
                    if (numberOfProblems == 0)
                        compteur.Flag0 += 1;
                    if (numberOfProblems == 1)
                        compteur.Flag1 += 1;
                    if (numberOfProblems == 2)
                        compteur.Flag2 += 1;
                }
                return compteur;

            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + ":" + ex.Message);
            }
            return new Flag();
        }

        public Flag CountFlagFecondite()
        {
            string methodName = "CountFlagFecondite";
            try
            {
                List<IndividuModel> listOfIndividus = GetAllIndividus();
                Flag compteur = new Flag();


                foreach (IndividuModel ind in listOfIndividus)
                {
                    int numberOfProblems = 0;
                    #region Fecondite
                    if (ind.Qp4Sexe == (int)Constant.Sexe.Fi && ind.Qp5bAge >= 13 && ind.Qf1aNbreEnfantNeVivantM == 99)
                    {
                        numberOfProblems += 1;
                    }
                    if (ind.Qp4Sexe == (int)Constant.Sexe.Fi && ind.Qp5bAge >= 13 && ind.Qf1bNbreEnfantNeVivantF == 99)
                    {
                        numberOfProblems += 1;
                    }
                    if (ind.Qp4Sexe == (int)Constant.Sexe.Fi && ind.Qp5bAge >= 13 && ind.Qf1bNbreEnfantNeVivantF == 99 && ind.Qf1aNbreEnfantNeVivantM == 99)
                    {
                        numberOfProblems += 1;
                    }
                    #endregion
                    if (numberOfProblems == 0)
                        compteur.Flag0 += 1;
                    if (numberOfProblems == 1)
                        compteur.Flag1 += 1;
                    if (numberOfProblems == 2)
                        compteur.Flag2 += 1;
                    if (numberOfProblems == 3)
                        compteur.Flag3 += 1;
                }
                return compteur;

            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + ":" + ex.Message);
            }
            return new Flag();
        }

        public Flag CountFlagEmploi()
        {
            string methodName = "CountFlagEmploi";
            try
            {
                List<IndividuModel> listOfIndividus = GetAllIndividus();
                Flag compteur = new Flag();


                foreach (IndividuModel ind in listOfIndividus)
                {
                    int numberOfProblems = 0;
                    #region Activite Economique
                    if (ind.Qp5bAge >= 10 && ind.Qa5TypeBienProduitParEntreprise == "99")
                    {
                        numberOfProblems += 1;
                    }
                    if (ind.Qp5bAge >= 10 && ind.Qa7FoncTravail == 99)
                    {
                        numberOfProblems += 1;
                    }
                    if (ind.Qp5bAge >= 10 && ind.Qa8EntreprendreDemarcheTravail == 10)
                    {
                        numberOfProblems += 1;
                    }
                    #endregion
                    if (numberOfProblems == 0)
                        compteur.Flag0 += 1;
                    if (numberOfProblems == 1)
                        compteur.Flag1 += 1;
                    if (numberOfProblems == 2)
                        compteur.Flag2 += 1;
                    if (numberOfProblems == 3)
                        compteur.Flag3 += 1;
                }
                return compteur;

            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + ":" + ex.Message);
            }
            return new Flag();
        }
        #endregion

        #region Retrieving data for LOGEMENT
        /// <summary>
        /// Retourne tous les logements dans un batiment
        /// </summary>
        /// <param name="batimentId"></param>
        /// <returns></returns>
        public List<LogementModel> GetAllLogementsByBatiment(long batimentId)
        {
            try
            {
                return ModelMapper.MapToListLogementModel(repository.MLogementRepository.Find(l => l.batimentId == batimentId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("GetAllLogementsByBatiment:/GetAllLogementOccupantAbsent" + ex.Message);
            }
            return new List<LogementModel>();
        }
        /// <summary>
        /// Retourne les logements dont les occupants sont absents
        /// </summary>
        /// <returns>List GetAllLogementOccupantAbsent</returns>
        public List<LogementModel> GetAllLogementOccupantAbsent()
        {
            try
            {
                return ModelMapper.MapToListLogementModel(repository.MLogementRepository.Find(l => l.qlin2StatutOccupation == 2).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllLogementOccupantAbsent" + ex.Message);
            }
            return new List<LogementModel>();
        }
        /// <summary>
        /// Retourne tous les logements
        /// </summary>
        /// <returns>List LogementModel</returns>
        public List<LogementModel> GetAllLogements()
        {
            try
            {
                return ModelMapper.MapToListLogementModel(repository.MLogementRepository.Find().ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllLogements" + ex.Message);
            }
            return new List<LogementModel>();
        }
        /// <summary>
        /// Retourne les logements qui ne sont pas encore termines
        /// </summary>
        /// <returns></returns>
        public List<LogementModel> GetAllLogementIndNotFinish()
        {
            try
            {
                return ModelMapper.MapToListLogementModel(repository.MLogementRepository.Find(l => l.isFieldAllFilled == 0).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllLogementIndNotFinish" + ex.Message);
            }
            return new List<LogementModel>();
        }

        /// <summary>
        /// retourne les logements collectifs par batiments
        /// </summary>
        /// <param name="BatimentId"></param>
        /// <returns>List<LogementModel></returns>
        public List<LogementModel> GetLogementCByBatiment(long batimentId)
        {
            try
            {
                return ModelMapper.MapToListLogementModel(repository.MLogementRepository.Find(l => l.batimentId == batimentId && l.qlCategLogement == Constant.TYPE_LOJMAN_KOLEKTIF).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetLogementCByBatiment" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Get finished logements in a batiment
        /// </summary>
        /// <param name="batimentId"></param>
        /// <returns>List<LogementModel></returns>
        public List<LogementModel> GetLogementIFiniByBatiment(long batimentId)
        {
            try
            {
                return ModelMapper.MapToListLogementModel(repository.MLogementRepository.Find(l => l.batimentId == batimentId && l.qlCategLogement == Constant.TYPE_LOJMAN_ENDIVIDYEL && l.statut == Constant.STATUT_MODULE_KI_FINI_1).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetLogementIFiniByBatiment" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// retourne les logements individuels par batiments
        /// </summary>
        /// <param name="BatimentId"></param>
        /// <returns></returns>
        public List<LogementModel> GetLogementIByBatiment(long batimentId)
        {
            try
            {
                return ModelMapper.MapToListLogementModel(repository.MLogementRepository.Find(l => l.batimentId == batimentId && l.qlCategLogement == Constant.TYPE_LOJMAN_ENDIVIDYEL).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetLogementIByBatiment" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retourne un logement par son ID
        /// </summary>
        /// <param name="logId"></param>
        /// <returns>LogementModel</returns>
        public LogementModel GetLogementById(long logId)
        {
            try
            {
                return ModelMapper.MapToLogement(repository.MLogementRepository.Find(l => l.logeId == logId).FirstOrDefault());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetLogementById" + ex.Message);
            }
            return null;
        }
        #endregion

        #region OPERATIONS SUR CHAQUE SDES

        public int getTotalBatimentCartographies(string sdeId)
        {
            try
            {
                MainRepository repo = new MainRepository();
                return Convert.ToInt32(repo.SdeRepository.Find(s => s.SdeId == sdeId).FirstOrDefault().TotalBatCartographie);
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/getTotalBatimentCartographies:" + ex.Message);
            }
            return 0;
        }
        public Tbl_Sde getSdeDetailsFromSqliteFile()
        {
            Tbl_Sde sde = new Tbl_Sde();
            try
            {
                sde.TotalBatRecense = getTotalBatiment();
                sde.TotalDecesRecense = getTotalDeces();
                sde.TotalDecesFRecense = getTotalDecesFemmes();
                sde.TotalDecesGRecense = getTotalDecesHommes();
                sde.TotalEmigreFRecense = getTotalEmigresFemmes();
                sde.TotalEmigreGRecense = getTotalEmigresHommes();
                sde.TotalEmigreRecense = getTotalEmigres();
                sde.TotalIndFRecense = getTotalIndividusFemmes();
                sde.TotalIndGRecense = getTotalIndividusHommes();
                sde.TotalLogeCRecense = getTotalLogementCs();
                sde.TotalLogeIRecense = getTotalLogementInds();
                sde.TotalMenageRecense = getTotalMenages();
                sde.TotalIndRecense = getTotalIndividus();
                //sde.TotalBatRecenseNV = getTotalBatRecenseNV();
                sde.TotalBatRecenseV = getTotalBatRecenseV();
                sde.TotalEnfantDeMoinsDe5Ans = getTotalEnfantDeMoinsDe1Ans();
                sde.TotalIndividu10AnsEtPlus = getTotalIndividu10AnsEtPlus();
                sde.TotalIndividu18AnsEtPlus = getTotalIndividu18AnsEtPlus();
                sde.TotalIndividu65AnsEtPlus = getTotalIndividu65AnsEtPlus();
                //sde.TotalLogeCRecenseNV_ = getTotalLogeCRecenseNV();
                sde.TotalLogeCRecenseV = getTotalLogeCRecenseV();
                sde.TotalLogeIRecenseNV = getTotalLogeIRecenseNV();
                sde.TotalLogeIRecenseV = getTotalLogeIRecenseV();
                sde.TotalMenageRecenseV = getTotalMenageRecenseV();
                sde.TotalMenageRecenseNV = getTotalMenageRecenseNV();
                sde.TotalLogeIOccupeRecense = getTotalLogeIOccupeRecense();
                sde.TotalLogeIOccupeRecenseNV = getTotalLogeIOccupeRecenseNV();
                sde.TotalLogeIOccupeRecenseV = getTotalLogeIOccupeRecenseV();
                sde.TotalLogeIUsageTemporelRecense = getTotalLogeIUsageTemporelRecense();
                sde.TotalLogeIUsageTemporelRecenseNV = getTotalLogeIUsageTemporelRecenseNV();
                sde.TotalLogeIUsageTemporelRecenseV = getTotalLogeIUsageTemporelRecenseV();
                sde.TotalLogeIVideRecense = getTotalLogeIVideRecense();
                sde.TotalLogeIVideRecenseV = getTotalLogeIVideRecenseV();
                sde.TotalLogeIVideRecenseNV = getTotalLogeIVideRecenseNV();
                sde.IndiceMasculinite = Convert.ToInt64(getIndiceMasculinite());
            }
            catch (Exception)
            {

            }

            return sde;
        }
        public int getTotalBatiment()
        {
            try
            {
                return repository.MBatimentRepository.Find().Count();
            }
            catch (Exception ex)
            {
                log.Info("Exception:" + ex.Message);
                log.Info("Exception:" + ex.InnerException);
            }
            return 0;

        }

        public int getTotalLogements()
        {
            try
            {
                return repository.MLogementRepository.Find().Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalMenages()
        {
            try
            {

                return repository.MMenageRepository.Find().Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalEmigres()
        {
            try
            {

                return repository.MEmigreRepository.Find().Count();

            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalEmigresFemmes()
        {
            try
            {
                return repository.MEmigreRepository.Find(em => em.qn2bSexe == Constant.FEMININ).ToList().Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalEmigresHommes()
        {

            try
            {
                return repository.MEmigreRepository.Find(em => em.qn2bSexe == Constant.MASCULIN).ToList().Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalDeces()
        {
            try
            {
                return repository.MDecesRepository.Find().Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalDecesFemmes()
        {
            try
            {
                List<tbl_deces> list = repository.MDecesRepository.Find(dc => dc.qd2aSexe == Constant.FEMININ).ToList();
                return list.Count();

            }
            catch (Exception)
            {

            }
            return 0;

        }

        public int getTotalDecesHommes()
        {
            try
            {
                return repository.MDecesRepository.Find(dc => dc.qd2aSexe == Constant.MASCULIN).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalIndividus()
        {
            try
            {
                return repository.MIndividuRepository.Find().Count();

            }
            catch (Exception)
            {

            }
            return 0;

        }

        public int getTotalIndividusFemmes()
        {
            try
            {
                return repository.MIndividuRepository.Find(ind => ind.qp4Sexe == Constant.FEMININ).Count();

            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/getTotalIndividusFemmes:Error=> " + ex.Message);
            }
            return 0;
        }

        public int getTotalIndividusHommes()
        {
            try
            {
                return repository.MIndividuRepository.Find(ind => ind.qp4Sexe == Constant.MASCULIN).Count();

            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/getTotalIndividusHommes:Error=> " + ex.Message);
            }
            return 0;

        }
        public int getTotalLogementCs()
        {
            try
            {
                return repository.MLogementRepository.Find(lg => lg.qlCategLogement == Constant.TYPE_LOJMAN_KOLEKTIF).Count();

            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalLogementInds()
        {
            try
            {
                return repository.MLogementRepository.Find(lg => lg.qlCategLogement == Constant.TYPE_LOJMAN_ENDIVIDYEL).Count();

            }
            catch (Exception)
            {

            }
            return 0;
        }
        public int getTotalBatRecenseV()
        {
            try
            {
                return repository.MBatimentRepository.Find(b => b.isValidated == Constant.STATUS_VALIDATED_1).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalBatRecenseNV()
        {
            try
            {
                return repository.MBatimentRepository.Find(b => b.isValidated == Constant.STATUS_NOT_VALIDATED_0).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalLogeCRecenseV()
        {
            try
            {
                return repository.MLogementRepository.Find(l => l.isValidated == Constant.STATUS_VALIDATED_1 && l.qlCategLogement == Constant.TYPE_LOJMAN_KOLEKTIF).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalLogeCRecenseNV()
        {
            try
            {
                return repository.MLogementRepository.Find(l => l.isValidated == Constant.STATUS_NOT_VALIDATED_0 && l.qlCategLogement == Constant.TYPE_LOJMAN_KOLEKTIF).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalLogeIRecenseV()
        {
            try
            {
                return repository.MLogementRepository.Find(l => l.isValidated == Constant.STATUS_VALIDATED_1 && l.qlCategLogement == Constant.TYPE_LOJMAN_ENDIVIDYEL).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalLogeIRecenseNV()
        {
            try
            {
                return repository.MLogementRepository.Find(l => l.isValidated == Constant.STATUS_NOT_VALIDATED_0 && l.qlCategLogement == Constant.TYPE_LOJMAN_ENDIVIDYEL).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalMenageRecenseV()
        {
            try
            {
                return repository.MMenageRepository.Find(m => m.isValidated == Constant.STATUS_VALIDATED_1).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalMenageRecenseNV()
        {
            try
            {
                return repository.MMenageRepository.Find(m => m.isValidated == Constant.STATUS_NOT_VALIDATED_0).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalIndRecenseV()
        {
            try
            {
                return repository.MIndividuRepository.Find(i => i.statut == Constant.STATUS_VALIDATED_1).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalIndRecenseNV()
        {
            try
            {
                return repository.MIndividuRepository.Find(i => i.statut == Constant.STATUS_NOT_VALIDATED_0).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }
        #region INDICATEURS DE PERFOMANCES
        public double getTotalBatRecenseParJourV()
        {
            string methodName = "getTotalBatRecenseParJourV";
            double nbreParJour = 0;
            try
            {
                List<BatimentModel> listOfBatiments = ModelMapper.MapToListBatimentModel(repository.MBatimentRepository.Find(b => b.statut == (int)Constant.StatutModule.Fini).ToList());
                BatimentModel firstBatiment = listOfBatiments.First();
                BatimentModel lastBatiment = listOfBatiments.Last();
                DateTime dateSaisieFirst = DateTime.ParseExact(firstBatiment.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null);
                DateTime dateSaisieLast = DateTime.ParseExact(lastBatiment.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null);
                double totalOfDays = (dateSaisieLast - dateSaisieFirst).TotalDays;
                totalOfDays = Math.Truncate(totalOfDays);
                if (totalOfDays == 0)
                    nbreParJour = listOfBatiments.Count();
                else
                {
                    if (totalOfDays >= 2)
                        nbreParJour = listOfBatiments.Count() / (totalOfDays - 1);
                    else
                    {
                        nbreParJour = listOfBatiments.Count();
                    }
                }
                log.Info("Nombre de batiments/jar:" + nbreParJour);
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + " : " + ex.Message);
            }
            return nbreParJour;
        }

        public int getTotalBatRecenseParJourNV()
        {
            throw new NotImplementedException();
        }
        public double getTotalLogeRecenseParJourV()
        {
            string methodName = "getTotalLogeCRecenseParJourV";
            double nbreParJour = 0;
            try
            {
                List<LogementModel> listOfLogements = ModelMapper.MapToListLogementModel(repository.MLogementRepository.Find(l => l.statut == (int)Constant.StatutModule.Fini).ToList());
                LogementModel firstBatiment = listOfLogements.First();
                LogementModel lastBatiment = listOfLogements.Last();
                DateTime dateSaisieFirst = DateTime.ParseExact(firstBatiment.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null);
                DateTime dateSaisieLast = DateTime.ParseExact(lastBatiment.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null);
                double totalOfDays = (dateSaisieLast - dateSaisieFirst).TotalDays;
                totalOfDays = Math.Truncate(totalOfDays);
                if (totalOfDays == 0)
                    nbreParJour = listOfLogements.Count();
                else
                {
                    if (totalOfDays >= 2)
                        nbreParJour = listOfLogements.Count() / (totalOfDays - 1);
                    else
                    {
                        nbreParJour = listOfLogements.Count();
                    }
                }
                log.Info("Nombre de batiments/jar:" + nbreParJour);
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + " : " + ex.Message);
            }
            return nbreParJour;
        }
        public double getTotalLogeCRecenseParJourV()
        {
            string methodName = "getTotalLogeCRecenseParJourV";
            double nbreParJour = 0;
            try
            {
                List<LogementModel> listOfLogements = ModelMapper.MapToListLogementModel(repository.MLogementRepository.Find(l => l.statut == (int)Constant.StatutModule.Fini).ToList());
                LogementModel firstLogement = listOfLogements.First();
                LogementModel lastLogement = listOfLogements.Last();
                DateTime dateSaisieFirst = DateTime.ParseExact(firstLogement.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null);
                DateTime dateSaisieLast = DateTime.ParseExact(lastLogement.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null);
                double totalOfDays = (dateSaisieLast - dateSaisieFirst).TotalDays;
                totalOfDays = Math.Truncate(totalOfDays);
                if (totalOfDays == 0)
                    nbreParJour = listOfLogements.Count();
                else
                {
                    if (totalOfDays >= 2)
                        nbreParJour = listOfLogements.Count() / (totalOfDays - 1);
                    else
                    {
                        nbreParJour = listOfLogements.Count();
                    }
                }
                log.Info("Nombre de batiments/jar:" + nbreParJour);
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + " : " + ex.Message);
            }
            return nbreParJour;
        }

        public int getTotalLogeCRecenseParJourNV()
        {
            throw new NotImplementedException();
        }

        public int getTotalLogeIRecenseParJourV()
        {
            string methodName = "";
            try
            {

            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + " : " + ex.Message);
            }
            return 0;
        }

        public int getTotalLogeIRecenseParJourNV()
        {
            throw new NotImplementedException();
        }

        public double getTotalMenageRecenseParJourV()
        {
            string methodName = "getTotalLogeCRecenseParJourV";
            double nbreParJour = 0;
            try
            {
                List<MenageModel> listOfMenages = ModelMapper.MapToListMenageModel(repository.MMenageRepository.Find(l => l.statut == (int)Constant.StatutModule.Fini).ToList());
                MenageModel firstMenage = listOfMenages.First();
                MenageModel lastMenage = listOfMenages.Last();
                DateTime dateSaisieFirst = DateTime.ParseExact(firstMenage.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null);
                DateTime dateSaisieLast = DateTime.ParseExact(lastMenage.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null);
                double totalOfDays = (dateSaisieLast - dateSaisieFirst).TotalDays;
                totalOfDays = Math.Truncate(totalOfDays);
                if (totalOfDays == 0)
                    nbreParJour = listOfMenages.Count();
                else
                {
                    if (totalOfDays >= 2)
                        nbreParJour = listOfMenages.Count() / (totalOfDays - 1);
                    else
                    {
                        nbreParJour = listOfMenages.Count();
                    }
                }
                log.Info("Nombre de batiments/jar:" + nbreParJour);
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + " : " + ex.Message);
            }
            return nbreParJour;
        }

        public int getTotalMenageRecenseParJourNV()
        {
            throw new NotImplementedException();
        }

        public double getTotalIndRecenseParJourV()
        {
            string methodName = "getTotalIndRecenseParJourV";
            double nbreParJour = 0;
            try
            {
                List<IndividuModel> listOfIndividus = ModelMapper.MapToListIndividu(repository.MIndividuRepository.Find(l => l.statut == (int)Constant.StatutModule.Fini).ToList());
                IndividuModel firstIndividu = listOfIndividus.First();
                IndividuModel lastIndividu = listOfIndividus.Last();
                DateTime dateSaisieFirst = DateTime.ParseExact(firstIndividu.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null);
                DateTime dateSaisieLast = DateTime.ParseExact(lastIndividu.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null);
                double totalOfDays = (dateSaisieLast - dateSaisieFirst).TotalDays;
                totalOfDays = Math.Truncate(totalOfDays);
                if (totalOfDays == 0)
                    nbreParJour = listOfIndividus.Count();
                else
                {
                    if (totalOfDays >= 2)
                        nbreParJour = listOfIndividus.Count() / (totalOfDays - 1);
                    else
                    {
                        nbreParJour = listOfIndividus.Count();
                    }
                }
                log.Info("Nombre de batiments/jar:" + nbreParJour);
                
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + " : " + ex.Message);
            }
            return  nbreParJour;;
        }

        public int getTotalIndRecenseParJourNV()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region INDICATEURS SOCIO-DEMOGRAPHIQUES
        public float getIndiceMasculinite()
        {
            try
            {
                float indice = 0;
                float nbreG = (float)getTotalIndividusHommes();
                float nbreF = (float)getTotalIndividusFemmes();
                indice = nbreG / nbreF;
                indice = indice * 100;
                return indice;
            }
            catch (Exception)
            {

            }
            return 0;
        }
        /// <summary>
        /// Retourne les enfants de moins de 5 ans
        /// </summary>
        /// <returns></returns>
        public int getTotalEnfantDeMoinsDe1Ans()
        {
            try
            {
                return repository.MIndividuRepository.Find(i => i.qp5bAge < 1).Count();
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/getTotalEnfantDeMoinsDe1Ans:Error=> " + ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// Retourne les personnes de plus de 18 ans
        /// </summary>
        /// <returns>Int</returns>
        public int getTotalIndividu18AnsEtPlus()
        {
            try
            {
                return repository.MIndividuRepository.Find(i => i.qp5bAge >= 18).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }
        /// <summary>
        /// Retourne les personnes de plus de 10 ans
        /// </summary>
        /// <returns></returns>
        public int getTotalIndividu10AnsEtPlus()
        {
            try
            {
                return repository.MIndividuRepository.Find(i => i.qp5bAge >= 10).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        /// <summary>
        /// Retourne les individus de plus de 65 ans
        /// </summary>
        /// <returns></returns>
        public int getTotalIndividu65AnsEtPlus()
        {
            try
            {
                return repository.MIndividuRepository.Find(i => i.qp5bAge >= 65).Count();
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/getTotalIndividu65AnsEtPlus:Error=> " + ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// Retourne de meanges unipersonnels
        /// </summary>
        /// <returns></returns>
        public int getTotalMenageUnipersonnel()
        {
            try
            {
                return repository.MMenageRepository.Find(m => m.qm11TotalIndividuVivant == 1).Count();
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/getTotalMenageUnipersonnel" + ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// Retourne le nombre de menages ayant plus de 6 personnes
        /// </summary>
        /// <returns>Int</returns>
        public int getTotalMenageDe6IndsEtPlus()
        {
            try
            {
                return repository.MMenageRepository.Find(m => m.qm11TotalIndividuVivant >= 6).Count();
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/getTotalMenageDe6IndsEtPlus" + ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// Retourne le nombre de personnes vivant dans les logements collectifs
        /// </summary>
        /// <returns></returns>
        public int getTotalPersonnesByLogementCollections()
        {
            try
            {
                int nbLogementCollectif = 0;
                //On recherche les logements collectifs avant et apres on fait le total
                List<LogementModel> listOfLogementsCollectifs = ModelMapper.MapToListLogementModel(repository.MLogementRepository.Find(l => l.qlCategLogement == 0).ToList());
                if (listOfLogementsCollectifs != null)
                {
                    foreach (LogementModel lgc in listOfLogementsCollectifs)
                    {
                        nbLogementCollectif = nbLogementCollectif + lgc.QlcTotalIndividus;
                    }
                    return nbLogementCollectif;
                }

            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/getTotalPersonnesByLogementCollections:" + ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// Nombre de personnes presentant au moins une limitation
        /// </summary>
        /// <returns>Int</returns>
        public int getTotalPersonnesByLimitation()
        {
            try
            {
                return repository.MIndividuRepository.Find(i => i.qaf2HandicapEntendre > 1 || i.qaf3HandicapMarcher > 1 ||
                                                           i.qaf1HandicapVoir > 1 || i.qaf4HandicapSouvenir > 1 ||
                                                           i.qaf5HandicapPourSeSoigner > 1 || i.qaf6HandicapCommuniquer > 1).Count();
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/getTotalPersonnesByLimitation:" + ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// Nomgre de femmes chef de menage
        /// </summary>
        /// <returns></returns>
        public int getTotalFemmeChefMenage()
        {
            try
            {
                return repository.MIndividuRepository.Find(i => i.qp3LienDeParente == 1 && i.qp4Sexe == 2).Count();
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/getTotalFemmeChefMenage:" + ex.Message);
            }
            return 0;
        }
        #endregion

        #region AUTRES
        public int getTotalLogeIOccupeRecenseNV()
        {
            try
            {
                return repository.MLogementRepository.Find(l => l.qlCategLogement == Constant.TYPE_LOJMAN_ENDIVIDYEL && l.qlin2StatutOccupation == Constant.LOJMAN_OKIPE_TOUTAN && l.isValidated == Constant.STATUS_NOT_VALIDATED_0).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalLogeIOccupeRecenseV()
        {
            try
            {
                return repository.MLogementRepository.Find(l => l.qlCategLogement == Constant.TYPE_LOJMAN_ENDIVIDYEL && l.qlin2StatutOccupation == Constant.LOJMAN_OKIPE_TOUTAN && l.isValidated == Constant.STATUS_VALIDATED_1).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalLogeIVideRecenseNV()
        {
            try
            {
                return repository.MLogementRepository.Find(l => l.qlCategLogement == Constant.TYPE_LOJMAN_ENDIVIDYEL && l.qlin2StatutOccupation == Constant.LOJMAN_VID && l.isValidated == Constant.STATUS_NOT_VALIDATED_0).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalLogeIVideRecenseV()
        {
            try
            {
                return repository.MLogementRepository.Find(l => l.qlCategLogement == Constant.TYPE_LOJMAN_ENDIVIDYEL && l.qlin2StatutOccupation == Constant.LOJMAN_VID && l.isValidated == Constant.STATUS_VALIDATED_1).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalLogeIUsageTemporelRecenseNV()
        {
            try
            {
                return repository.MLogementRepository.Find(l => l.qlCategLogement == Constant.TYPE_LOJMAN_ENDIVIDYEL && l.qlin2StatutOccupation == Constant.LOJMAN_OKIPE_YON_LE_KONSA && l.isValidated == Constant.STATUS_NOT_VALIDATED_0).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalLogeIUsageTemporelRecenseV()
        {
            try
            {
                return repository.MLogementRepository.Find(l => l.qlCategLogement == Constant.TYPE_LOJMAN_ENDIVIDYEL && l.qlin2StatutOccupation == Constant.LOJMAN_OKIPE_YON_LE_KONSA && l.isValidated == Constant.STATUS_VALIDATED_1).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalLogeIOccupeRecense()
        {
            try
            {
                return repository.MLogementRepository.Find(l => l.qlCategLogement == Constant.TYPE_LOJMAN_ENDIVIDYEL && l.qlin2StatutOccupation == Constant.LOJMAN_OKIPE_TOUTAN).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalLogeIVideRecense()
        {
            try
            {
                return repository.MLogementRepository.Find(l => l.qlCategLogement == Constant.TYPE_LOJMAN_ENDIVIDYEL && l.qlin2StatutOccupation == Constant.LOJMAN_VID).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalLogeIUsageTemporelRecense()
        {
            try
            {
                return repository.MLogementRepository.Find(l => l.qlCategLogement == Constant.TYPE_LOJMAN_ENDIVIDYEL && l.qlin2StatutOccupation == Constant.LOJMAN_OKIPE_YON_LE_KONSA).Count();
            }
            catch (Exception)
            {

            }
            return 0;
        }
        #endregion

        #endregion

        #region QUESTIONS REPONSES

        public List<tbl_question> searchQuestionByCategorie(string codeCategorie)
        {
            try
            {
                return repository.MQuestionRepository.Find(q => q.codeCategorie == codeCategorie).ToList();
            }
            catch (Exception ex)
            {
                log.Info("Error:searchQuestionByCategorie" + ex.Message);
            }
            return new List<tbl_question>();
        }
        public tbl_question getQuestion(string codeQuestion)
        {
            try
            {
                return repository.MQuestionRepository.Find(q => q.codeQuestion == codeQuestion).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Info("Error:getQuestion" + ex.Message);
            }
            return null;
        }

        public string getReponse(string codeQuestion, string codeReponse)
        {
            try
            {
                List<tbl_question_reponse> ListOfreponse = repository.MQuestionReponseRepository.Find(rep => rep.codeQuestion == codeQuestion).ToList();
                foreach (tbl_question_reponse qr in ListOfreponse)
                {
                    if (qr.codeReponse == codeReponse)
                    {
                        return qr.libelleReponse;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info("Error:getReponse" + ex.Message);
            }
            return null;
        }


        public List<tbl_question_module> listOfQuestionModule(string codeModule)
        {
            try
            {
                return repository.MQuestionModuleRepository.Find(qm => qm.codeModule == codeModule).ToList();
            }
            catch (Exception ex)
            {
                log.Info("Error:listOfQuestionModule" + ex.Message);
            }
            return null;
        }


        public string getLibelleCategorie(string codeCategorie)
        {
            try
            {
                return repository.MCategorieQuestionRepository.Find(qc => qc.codeCategorie == codeCategorie).FirstOrDefault().categorieQuestion;
            }
            catch (Exception ex)
            {
                log.Info("Error:libelleCategorie" + ex.Message);
            }
            return null;
        }
        public tbl_categorie_question getCategorie(string codeCategorie)
        {
            try
            {
                return repository.MCategorieQuestionRepository.Find(qc => qc.codeCategorie == codeCategorie).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Info("Error:getCategorie" + ex.Message);
            }
            return null;
        }


        public tbl_question getQuestionByNomChamps(string nomChamps)
        {
            try
            {
                return repository.MQuestionRepository.Find(q => q.nomChamps == nomChamps).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Info("Error:getQuestionByNomChamps" + ex.Message);
            }
            return null;
        }
        #endregion

        #region INDICATEURS SOCIO-DEMOGRAPHIQUES PAR MENAGES
        public int getTotalPersonnesByMenage(long menageId)
        {
            List<IndividuModel> listOf = GetIndividuByMenage(menageId);
            if (listOf != null)
            {
                return listOf.Count();
            }
            return 0;
        }

        public int getTotalFemmesByMenage(long menageId)
        {
            List<IndividuModel> listOf = GetIndividuByMenage(menageId);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuModel ind in listOf)
                {
                    if (ind.Qp4Sexe == 2)
                    {
                        nbre = nbre + 1;
                    }
                }
                return nbre;
            }
            return 0;
        }

        public int getTotalEnfantMoins5AnsByMenage(long menageId)
        {
            List<IndividuModel> listOf = GetIndividuByMenage(menageId);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuModel ind in listOf)
                {
                    if (ind.Qp5bAge < 5)
                    {
                        nbre = nbre + 1;
                    }
                }
                return nbre;
            }
            return 0;
        }

        public int getTotalPersonnesMoin15AnsByMenage(long menageId)
        {
            List<IndividuModel> listOf = GetIndividuByMenage(menageId);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuModel ind in listOf)
                {
                    if (ind.Qp5bAge < 15)
                    {
                        nbre = nbre + 1;
                    }
                }
                return nbre;
            }
            return 0;
        }

        public int getTotalDecesByMenage(long logeId)
        {
            // MenageModel Menage = GetMenageByLogement(logeId);
            return 0;
        }

        public int getTotalHandicapVoirByMen(long menageId)
        {
            List<IndividuModel> listOf = GetIndividuByMenage(menageId);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuModel ind in listOf)
                {
                    if (ind.Qp5bAge >= 5)
                    {
                        if (ind.Qaf1HandicapVoir >= 2)
                        {
                            nbre = nbre + 1;
                        }
                    }
                }
                return nbre;
            }
            return 0;
        }

        public int getTotalhandicapEntendrebyMen(long menageId)
        {
            List<IndividuModel> listOf = GetIndividuByMenage(menageId);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuModel ind in listOf)
                {
                    if (ind.Qp5bAge >= 5)
                    {
                        if (ind.Qaf2HandicapEntendre >= 2)
                        {
                            nbre = nbre + 1;
                        }
                    }
                }
                return nbre;
            }
            return 0;
        }

        public int getTotalHandicapMarcherByMen(long menageId)
        {
            List<IndividuModel> listOf = GetIndividuByMenage(menageId);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuModel ind in listOf)
                {
                    if (ind.Qp5bAge >= 5)
                    {
                        if (ind.Qaf3HandicapMarcher >= 2)
                        {
                            nbre = nbre + 1;
                        }
                    }
                }
                return nbre;
            }
            return 0;
        }

        public int getTotalhandicapSouvenirByMen(long menageId)
        {
            List<IndividuModel> listOf = GetIndividuByMenage(menageId);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuModel ind in listOf)
                {
                    if (ind.Qp5bAge >= 5)
                    {
                        if (ind.Qaf4HandicapSouvenir >= 2)
                        {
                            nbre = nbre + 1;
                        }
                    }
                }
                return nbre;
            }
            return 0;
        }

        public int getTotalHandicapSoignerbyMen(long menageId)
        {
            List<IndividuModel> listOf = GetIndividuByMenage(menageId);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuModel ind in listOf)
                {
                    if (ind.Qp5bAge >= 5)
                    {
                        if (ind.Qaf5HandicapPourSeSoigner >= 2)
                        {
                            nbre = nbre + 1;
                        }
                    }
                }
                return nbre;
            }
            return 0;
        }

        public int getTotalHandicapCommuniquerbyMen(long menageId)
        {
            List<IndividuModel> listOf = GetIndividuByMenage(menageId);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuModel ind in listOf)
                {
                    if (ind.Qp5bAge >= 5)
                    {
                        if (ind.Qaf6HandicapCommuniquer >= 2)
                        {
                            nbre = nbre + 1;
                        }
                    }
                }
                return nbre;
            }
            return 0;
        }

        public int getTotalAnalphabetes15AnsbyMen(long menageId)
        {
            List<IndividuModel> listOf = GetIndividuByMenage(menageId);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuModel ind in listOf)
                {
                    if (ind.Qp5bAge >= 15)
                    {
                        if (ind.Qe2FreqentationScolaireOuUniv == 5)
                        {
                            nbre = nbre + 1;
                        }

                    }
                }
                return nbre;
            }
            return 0;
        }

        public int getTotalPersonneFrequentantEcoleByMen(long menageId)
        {
            List<IndividuModel> listOf = GetIndividuByMenage(menageId);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuModel ind in listOf)
                {
                    if (ind.Qe2FreqentationScolaireOuUniv <= 6)
                    {
                        if (ind.Qp5bAge >= 6 && ind.Qp5bAge <= 24)
                        {
                            nbre = nbre + 1;
                        }
                    }
                }
                return nbre;
            }
            return 0;
        }

        public int getTotalPersonneNiveauSecondairebyMen(long menageId)
        {
            List<IndividuModel> listOf = GetIndividuByMenage(menageId);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuModel ind in listOf)
                {
                    if (ind.Qp5bAge >= 6 && ind.Qp5bAge <= 24)
                    {
                        if (ind.Qe4aNiveauEtude >= 4 && ind.Qe4aNiveauEtude == 6)
                        {
                            nbre = nbre + 1;
                        }
                    }

                }
                return nbre;
            }
            return 0;
        }

        public int getTotalFormationProByMen(long menageId)
        {
            //List<IndividuModel> listOf = GetIndividuByMenage(menageId);
            //if (listOf != null)
            //{
            //    int nbre = 0;
            //    foreach (IndividuModel ind in listOf)
            //    {
            //        if (ind.qp5bAge >= 6 && ind.qp5bAge <= 24)
            //        {
            //            if (ind. <= 4)
            //            {
            //                nbre = nbre + 1;
            //            }
            //        }

            //    }
            //    return nbre;
            //}
            return 0;
        }
        #endregion

        #region GEOGRAPHIQUES
        public tbl_pays getpays(string codePays)
        {
            return repository.MPayspository.Find(p => p.CodePays == codePays).FirstOrDefault();
        }

        public tbl_departement getDepartement(string deptId)
        {
            return repository.MDepartementRepository.Find(p => p.DeptId == deptId).FirstOrDefault();
        }

        public tbl_commune getCommune(string comId)
        {
            string[] com = comId.Split('-');
            string code = com[0];
            return repository.MCommuneRepository.Find(c => c.ComId == code).FirstOrDefault();
        }

        public tbl_vqse getVqse(string vqse)
        {
            return repository.MVqseRepository.Find(c => c.VqseId == vqse).FirstOrDefault();
        }
        #endregion

        #region DOMAINES
        public tbl_domaine_etude getDomaine(string domaineId)
        {
            return repository.MDomaineEtudeRepository.Find(d => d.Code == domaineId).FirstOrDefault();
        }
        #endregion

        #region RAPPORT AGENT RECENSEUR
        /// <summary>
        /// Retourne tous les rapports dresses par l'agent recenseur
        /// </summary>
        /// <returns></returns>
        public List<RapportArModel> GetAllRptAgentRecenseur()
        {
            try
            {
                return ModelMapper.MapToListRapportARModel(repository.MRapportARRepository.Find().ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllRptAgentRecenseur:" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retourne tous les rapports dresses par l'agent recenseur sur un batiment
        /// </summary>
        /// <param name="batimentId"></param>
        /// <returns></returns>
        public List<RapportArModel> GetAllRptAgentRecenseurByBatiment(long batimentId)
        {
            try
            {
                return ModelMapper.MapToListRapportARModel(repository.MRapportARRepository.Find(b => b.batimentId == batimentId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllRptAgentRecenseurByBatiment:" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne tous les rapports dresses par l'agent recenseur sur un logement
        /// </summary>
        /// <param name="logeId"></param>
        /// <returns></returns>
        public List<RapportArModel> GetAllRptAgentRecenseurByLogement(long logeId)
        {
            try
            {
                return ModelMapper.MapToListRapportARModel(repository.MRapportARRepository.Find(l => l.logeId == logeId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllRptAgentRecenseurByLogement:" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retourne tous les rapports dresses par l'agent recenseur sur un menage
        /// </summary>
        /// <param name="menageId"></param>
        /// <returns></returns>
        public List<RapportArModel> GetAllRptAgentRecenseurByMenage(long menageId)
        {
            try
            {
                return ModelMapper.MapToListRapportARModel(repository.MRapportARRepository.Find(m => m.menageId == menageId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllRptAgentRecenseurByMenage:" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne tous les rapports pour lesquels au moins un objet n'est pas termine
        /// </summary>
        /// <returns></returns>
        public List<RapportArModel> GetAllRptAgentRecenseurForNotFinishedObject()
        {
            try
            {
                return ModelMapper.MapToListRapportARModel(repository.MRapportARRepository.Find(r => r.raisonActionId > 15).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllRptAgentRecenseurForNotFinishedObject:" + ex.Message);
            }
            return null;
        }

        public List<RapportArModel> GetAllRptAgentRecenseurByIndividu(long individuId)
        {
            try
            {
                return ModelMapper.MapToListRapportARModel(repository.MRapportARRepository.Find(r => r.individuId == individuId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllRptAgentRecenseurByIndividu:" + ex.Message);
            }
            return null;
        }
        #endregion

















    }
}
