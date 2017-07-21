using Ht.Ihsi.Rgph.DataAccess.Dao;
using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.DataAccess.Exceptions;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsi.Rgph.Utility.Utils;
using Ht.Ihsil.Rgph.App.Superviseur.Json;
using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.services
{
    public class ContreEnqueteService : IContreEnqueteService
    {
        #region DECLARATIONS
        public SqliteDataReaderService readerService;
        private Logger log;
        public DaoContreEnquete daoCE;
        #endregion

        #region CONSTRUCTOR
        public ContreEnqueteService()
        {
            //readerService = new SdfDataReaderService();
            string path = Utilities.getConnectionString(Users.users.SupDatabasePath);
            daoCE = new DaoContreEnquete(path);
            log = new Logger();

        }
        public ContreEnqueteService(string sdeId)
        {
            readerService = new SqliteDataReaderService(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));
            string path = Utilities.getConnectionString(Users.users.SupDatabasePath);
            daoCE = new DaoContreEnquete(path);
            log = new Logger();

        }
        #endregion

        #region METHODS DECES
        public DecesModel getFirstDeces(MenageModel _menage)
        {
            throw new NotImplementedException();
        }

        public bool saveDecesCE(DecesCEModel _dec)
        {
            return daoCE.saveDecesCE(ModelMapper.MapToTbl_DecesCE(_dec));
        }

        public bool updateDecesCE(DecesCEModel _dec)
        {
            return daoCE.updateDecesCE(ModelMapper.MapToTbl_DecesCE(_dec));
        }

        public List<DecesCEModel> searchAllDecesCE(MenageCEModel _men)
        {
            return ModelMapper.MapToListDecesCEModel(daoCE.searchAllDecesCE(_men.BatimentId, _men.LogeId, _men.SdeId, _men.MenageId));
        }
        public DecesCEModel getDecesCEModel(long decesId, string sdeId)
        {
            DecesCEModel _deces = new DecesCEModel();
            _deces = Mapper.ModelMapper.MapToDecesCEModel(daoCE.getDecesCEModel(decesId, sdeId));
            return _deces;
        }
        #endregion

        #region CONTRE-ENQUETE METHODS
        public void saveContreEnquete(ContreEnqueteModel ce)
        {
            try
            {
                if (Utils.IsNotNull(ce))
                {
                    daoCE.saveContreEnquete(ModelMapper.MapToTblContreEnquete(ce));
                }
            }
            catch (Exception ex)
            {
                log.Info("<>=======================Erreur/saveContreEnquete:" + ex.Message);
                log.Info("<>=======================Erreur/saveContreEnquete:" + ex.InnerException);
            }
        }

        
       
        public bool updateContreEnquete(ContreEnqueteModel ce)
        {
            try
            {

                Tbl_ContreEnquete c = daoCE.getContreEnquete(Convert.ToInt32(ce.BatimentId.GetValueOrDefault()), ce.SdeId);
                c.NomSuperviseur = ce.NomSuperviseur;
                c.PrenomSuperviseur = ce.PrenomSuperviseur;
                c.DateDebut = ce.DateDebut;
                c.DateFin = ce.DateFin;
                c.Raison = ce.Raison;
                return daoCE.updateContreEnquete(c);


            }
            catch (Exception ex)
            {

            }
            return false;
        }
        /// <summary>
        /// Search Contre Enquete By Type
        /// </summary>
        /// <param name="typeContreEnquete"></param>
        /// <param name="SdeId"></param>
        /// <returns>List<ContreEnqueteModel></returns>
        public List<ContreEnqueteModel> searchContreEnquete(int typeContreEnquete, string sdeId)
        {
            return ModelMapper.MapToListContreEnqueteModel(daoCE.searchContreEnquete(sdeId, typeContreEnquete));
        }

        /// <summary>
        /// Gets all contre enquete by SDE
        /// </summary>
        ///<param name="SdeId"></param>
        /// <returns>List<ContreEnqueteModel></returns>
        public List<ContreEnqueteModel> searchContreEnquete(string sdeId)
        {
            return ModelMapper.MapToListContreEnqueteModel(daoCE.searchContreEnquete(sdeId));
        }

        /// <summary>
        /// Gets a contre enquete by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="SdeId"></param>
        /// <returns>ContreEnqueteModel</returns>
        public ContreEnqueteModel getContreEnquete(long id, string sdeId)
        {
            return ModelMapper.MapToContreEnqueteModel(daoCE.getContreEnquete(id, sdeId));
        }
#endregion

        #region METHODS BATIMENTS
        /// <summary>
        /// Return un batiment de contre enquete
        /// </summary>
        /// <param name="BatimentId"></param>
        /// <param name="SdeId"></param>
        /// <returns>BatimentCEModel</returns>
        public BatimentCEModel getBatiment(long batimentId, string sdeId)
        {
            return ModelMapper.MapToBatimentCEModel(daoCE.getBatiment(batimentId, sdeId));
        }
        /// <summary>
        /// Retourne 10 batiments ayant chacun un logement et un Menage dans un fihcier SDF representant une SDE
        /// </summary>
        /// <param Name="SdeId"></param>
        /// <returns>List<BatimentModel></returns>
        public List<BatimentModel> getBatimentWithLogInd()
        {
            try
            {
                return readerService.getAllBatimentWithLogInd();
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        /// <summary>
        /// Methode permettant de selectionner 3 batiments vides dans une SDE dans un fichier SQLITE.
        /// </summary>
        /// <param Name="SdeId"></param>
        /// <returns></returns>
        public List<BatimentModel> getBatimentVide()
        {
             try
            {
                List<BatimentModel> listB = readerService.getAllBatimentVide();
                if (Utils.IsNotNull(listB))
                {

                    if (Utils.IsNotNull(listB) && listB.Count >= 1)
                    {
                        if (listB.Count <= 3)
                        {
                            return listB;
                        }
                        else
                        {
                            Random random = new Random();
                            List<BatimentModel> listOfBatiments = new List<BatimentModel>();
                            for (int i = 0; i <= listB.Count(); i++)
                            {
                                BatimentModel bat = listB.ElementAt(random.Next(1, listB.Count()));
                                if (listOfBatiments.Count > 0)
                                {
                                    foreach (BatimentModel b in listOfBatiments)
                                    {
                                        if (Utilities.isBatimentExistInList(listOfBatiments, bat) == false)
                                            listOfBatiments.Add(bat);
                                        break;
                                    }
                                }
                                else
                                {
                                    listOfBatiments.Add(bat);
                                }
                                if (listOfBatiments.Count == 3)
                                    break;
                            }
                            return listOfBatiments;
                        }
                        
                    }

                }
            }
            catch (Exception ex)
            {
                log.Info("Erreur Contre Enquete/getRandomBlankBatiment():" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourner 3 batiments vides qui ont ete selectionne pour un contre-enquete
        /// </summary>
        /// <param Name="SdeId"></param>
        /// <returns></returns>
        public List<BatimentModel> getBatimentVideInCE(string sdeId)
        {
            try
            {
                List<BatimentModel> batModel = null;
                List<ContreEnqueteModel> model_ce = ModelMapper.MapToListContreEnqueteModel(daoCE.searchContreEnquete(sdeId,(int)Constant.TypeContrEnquete.BatimentVide));
                if (Utils.IsNotNull(model_ce))
                {
                    batModel = new List<BatimentModel>();
                    foreach (ContreEnqueteModel ce in model_ce)
                    {
                        BatimentModel b = new BatimentModel(ce.BatimentId.GetValueOrDefault(), ce.SdeId);
                        if (daoCE.isLogementExistInBatiment(Convert.ToInt32(b.BatimentId),sdeId) == false)
                        {
                            if (daoCE.getBatiment(Convert.ToInt32(b.BatimentId), b.SdeId).IsContreEnqueteMade == 1)
                            {
                                b.IsChecked = true;
                            }
                            else
                            {
                                b.IsChecked = false;
                            }
                            if (daoCE.getBatiment(Convert.ToInt32(b.BatimentId), b.SdeId).IsValidated == 1)
                            {
                                b.IsValidate = true;
                            }
                            else
                            {
                                b.IsValidate = true;
                            }
                            batModel.Add(b);
                    
                        }
                    }
                    return batModel;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        /// <summary>
        /// Retourne tous les batiments qui ont ete selectionne pour un contre-enquete dans la table Tbl_batiment_CE
        /// </summary>
        /// <param Name="SdeId"></param>
        /// <returns></returns>
        public List<BatimentModel> getBatimentInCE(string sdeId)
        {
            try
            {
                List<BatimentModel> batModel = null;
                List<ContreEnqueteModel> model_ce = ModelMapper.MapToListContreEnqueteModel(daoCE.searchContreEnquete(sdeId));
                if (Utils.IsNotNull(model_ce))
                {
                    batModel = new List<BatimentModel>();
                    foreach (ContreEnqueteModel ce in model_ce)
                    {
                        BatimentModel b = new BatimentModel(ce.BatimentId.GetValueOrDefault(), ce.SdeId);
                           batModel.Add(b);

                    }
                    return batModel;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        /// <summary>
        /// Enregistrer un batiment
        /// </summary>
        /// <param Name="bat"></param>
        /// <returns></returns>
        public bool saveBatiment(BatimentCEModel bat)
        {
            return daoCE.saveBatimentCE(ModelMapper.MapToTbl_BatimentCE(bat));
        }

        /// <summary>
        /// Modifier un batiment
        /// </summary>
        /// <param Name="bat"></param>
        /// <returns></returns>
        public bool updateBatiment(BatimentCEModel bat)
        {
            return daoCE.updateBatimentCE(ModelMapper.MapToTbl_BatimentCE(bat));
        }

        /// <summary>
        /// Retourne un logement collectif dans une SDE dans un fichier SDF.
        /// </summary>
        /// <param Name="SdeId"></param>
        /// <returns></returns>
        public BatimentModel getBatimentWithLogementC()
        {
            List<BatimentModel> listOfBat= readerService.getAllLogementCollectif();
            Random random = new Random();
            if (listOfBat.Count() == 0)
            {
                return new BatimentModel();
            }
            else
            {
                if (listOfBat.Count() == 1)
                    return listOfBat.FirstOrDefault();
                else
                {
                    BatimentModel batiment = listOfBat.ElementAt(random.Next(1, listOfBat.Count()));
                    return batiment;
                }
            }
         }
        /// <summary>
        /// Retourne 3 batiments ayant chacun un logement individuel vide dans un fichier sqlite.
        /// </summary>
        /// <param Name="SdeId"></param>
        /// <returns></returns>
        public List<BatimentModel> searchBatimentWithLogementIndVide()
        {
            List<BatimentModel> listOfBat = readerService.getAllLogementIndividuelVide();
            if (Utils.IsNotNull(listOfBat))
            {
                Random random = new Random();
                List<BatimentModel> listBatLogVide = new List<BatimentModel>();
                if (listOfBat.Count <= 1)
                {
                    return listOfBat;
                }
                for (int i = 0; i <= listOfBat.Count; i++)
                {
                    BatimentModel bat = listOfBat.ElementAt(random.Next(1, listOfBat.Count()));
                    if (listBatLogVide.Count > 0)
                    {
                        foreach (BatimentModel batiment in listBatLogVide)
                        {
                            if (Utilities.isBatimentExistInList(listBatLogVide, bat))
                            {
                                listBatLogVide.Add(bat);
                                break;
                            }
                        }
                    }
                    else
                    {
                        listBatLogVide.Add(bat);
                    }
                    if (listBatLogVide.Count == 3)
                        break;
                }

                   return listBatLogVide;    
            }
            return null;
            
        }


        /// <summary>
        /// Verifie si le batiment existe dans la SDE
        /// </summary>
        /// <param Name="BatimentId"></param>
        /// <param Name="SdeId"></param>
        /// <returns>bool</returns>
        public bool isBatimentExist(int batimentId, string sdeId)
        {
            Tbl_BatimentCE model=daoCE.getBatiment(batimentId,sdeId);
            if (Utils.IsNotNull(model))
            {
                if (model.BatimentId != 0)
                {

                    return true;
                }
            }
            
            return false;
        }
        /// <summary>
        /// Retourne un batiment ayant un logement collectif dans la table TBL_batiment_CE
        /// </summary>
        /// <param Name="SdeId"></param>
        /// <returns>BatimentModel</returns>
        public BatimentModel getBatimentWithLogColCE(string sdeId)
        {
            try
            {
                List<ContreEnqueteModel> ce = ModelMapper.MapToListContreEnqueteModel(daoCE.searchContreEnquete(sdeId, (int)Constant.TypeContrEnquete.LogementCollectif));
                List<BatimentModel> listOfBat = new List<BatimentModel>();
                foreach (ContreEnqueteModel model in ce)
                {
                    BatimentModel bat = new BatimentModel(model.BatimentId.GetValueOrDefault(), sdeId);
                    listOfBat.Add(bat);
                }
                if (Utils.IsNotNull(listOfBat))
                {
                    foreach (BatimentModel bat in listOfBat)
                    {
                        if (daoCE.isLogementCollectifExistInBatiment(Convert.ToInt32(bat.BatimentId), sdeId) == true)
                        {
                            return bat;
                        }
                    }

                }
               
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        /// <summary>
        /// Retourne 3 batiments ayant au moins un logement vide dans la table TBL_Batiment_CE
        /// </summary>
        /// <param Name="SdeId"></param>
        /// <returns></returns>
        public List<BatimentModel> getBatimentWithLogIndVideCE(string sdeId)
        {
            List<ContreEnqueteModel> listOfCes = ModelMapper.MapToListContreEnqueteModel(daoCE.searchContreEnquete(sdeId, (int)Constant.TypeContrEnquete.LogementInvididuelVide));
            List<BatimentModel> listOfBat = new List<BatimentModel>();
            foreach (ContreEnqueteModel model in listOfCes)
            {
                BatimentModel bat = new BatimentModel(model.BatimentId.GetValueOrDefault(), sdeId);
                listOfBat.Add(bat);
            }
            return listOfBat;
        }

        /// <summary>
        /// Retourne 10 batiments ayants chacun au moins un logement, un Menage et un individu dans un fichier SDF
        /// </summary>
        /// <param Name="SdeId"></param>
        /// <returns></returns>
        public List<BatimentModel> getBatimentWithLogIndCE(string sdeId)
        {
            List<ContreEnqueteModel> listOfCes = ModelMapper.MapToListContreEnqueteModel(daoCE.searchContreEnquete(sdeId, (int)Constant.TypeContrEnquete.LogementIndividuelMenage));
            List<BatimentModel> listOfBat = new List<BatimentModel>();
            foreach (ContreEnqueteModel model in listOfCes)
            {
                BatimentModel bat = new BatimentModel(model.BatimentId.GetValueOrDefault(), sdeId);
                listOfBat.Add(bat);
            }
            return listOfBat;
        }

        public List<BatimentJson> getAllBatimentCEInJson(string sdeId)
        {
            try
            {
                List<BatimentJson> batiments = new List<BatimentJson>();
                List<BatimentCEModel> batimentsContreEnquetes = ModelMapper.MapToListBatimentCEModel(daoCE.getRepository().BatimentCERepository.Find(b=>b.SdeId==sdeId).ToList());
                foreach (BatimentCEModel batimentModel in batimentsContreEnquetes)
                {
                    BatimentJson batiment = ModelMapper.MapToJson(batimentModel);
                    //Recherche du type de contreenquete
                    ContreEnqueteModel contreEnquete = ModelMapper.MapToContreEnqueteModel(daoCE.getContreEnquete(Convert.ToInt32(batiment.batimentId), sdeId));
                    if (contreEnquete != null)
                    {
                        batiment.typeContreEnquete = contreEnquete.TypeContreEnquete.GetValueOrDefault();
                        batiment.raison = ""+contreEnquete.Raison.GetValueOrDefault();
                        //Recherche en fonction du type de contreenquete (Batiment Vide)
                        if (batiment.typeContreEnquete == (long)Constant.TypeContrEnquete.BatimentVide)
                        {
                            batiments.Add(batiment);
                        }
                        //Batiment avec Logement Collectif
                        if (batiment.typeContreEnquete == (long)Constant.TypeContrEnquete.LogementCollectif)
                        {
                            List<LogementCJson> logementsCollectifs = new List<LogementCJson>();
                            List<LogementCEModel> logCES = ModelMapper.MapToListLogementCEModel(daoCE.searchLogByBatimentAndTypeLog(Convert.ToInt32(batiment.batimentId), sdeId,Constant.TYPE_LOJMAN_KOLEKTIF));
                            foreach (LogementCEModel logmnt in logCES)
                            {
                                LogementCJson logjson = ModelMapper.MapToCLJson(logmnt);
                                //Recherche des individus dans les logements collectifs
                                List<IndividuJson> individusJson = new List<IndividuJson>();
                                List<IndividuCEModel> individusLogC = ModelMapper.MapToListIndividuCEModel(daoCE.searchAllIndividuCE(logjson.logeId));
                                foreach (IndividuCEModel indCe in individusLogC)
                                {
                                    IndividuJson indJson = ModelMapper.MapToJson(indCe);
                                    individusJson.Add(indJson);
                                }
                                logjson.individus = new List<IndividuJson>();
                                logjson.individus = individusJson;
                                logementsCollectifs.Add(logjson);
                            }
                            batiment.logementCs = new List<LogementCJson>();
                            batiment.logementCs = logementsCollectifs;
                            batiments.Add(batiment);
                        }
                        //Batiment avec Logement individuel vide
                        if (batiment.typeContreEnquete == (long)Constant.TypeContrEnquete.LogementInvididuelVide)
                        {
                            List<LogementIsJson> logementsIndividuels = new List<LogementIsJson>();
                            List<LogementCEModel> logIs = ModelMapper.MapToListLogementCEModel(daoCE.searchLogByBatimentAndTypeLog(Convert.ToInt32(batiment.batimentId), sdeId, Constant.TYPE_LOJMAN_ENDIVIDYEL));
                            foreach (LogementCEModel logmntI in logIs)
                            {
                                if (logmntI.Qlin9NbreTotalMenage.GetValueOrDefault() == 0)
                                {
                                    LogementIsJson lgJson = ModelMapper.MapToILJson(logmntI);
                                    logementsIndividuels.Add(lgJson);
                                }
                            }
                            batiment.logementIs = new List<LogementIsJson>();
                            batiment.logementIs = logementsIndividuels;
                            batiments.Add(batiment);
                        }
                        //batiment avec logement dont occupant absent
                        if (batiment.typeContreEnquete == (long)Constant.TypeContrEnquete.LogementInvididuelVide)
                        {
                            List<LogementIsJson> logementsIndividuels = new List<LogementIsJson>();
                            List<LogementCEModel> logIs = ModelMapper.MapToListLogementCEModel(daoCE.searchLogByBatimentAndTypeLog(Convert.ToInt32(batiment.batimentId), sdeId, Constant.TYPE_LOJMAN_ENDIVIDYEL));
                            foreach (LogementCEModel logmntI in logIs)
                            {
                                if (logmntI.Qlin2StatutOccupation.GetValueOrDefault() != 3)
                                {
                                    LogementIsJson lgJson = ModelMapper.MapToILJson(logmntI);
                                    logementsIndividuels.Add(lgJson);
                                }
                            }
                            batiment.logementIs = new List<LogementIsJson>();
                            batiment.logementIs = logementsIndividuels;
                            batiments.Add(batiment);
                        }
                        //Batiment avec logement individuel, menage, deces, individu
                        if (batiment.typeContreEnquete == (long)Constant.TypeContrEnquete.LogementIndividuelMenage)
                        {
                            List<LogementIsJson> logementsIndividuels = new List<LogementIsJson>();
                            List<LogementCEModel> logIs = ModelMapper.MapToListLogementCEModel(daoCE.searchLogByBatimentAndTypeLog(Convert.ToInt32(batiment.batimentId), sdeId, Constant.TYPE_LOJMAN_ENDIVIDYEL));
                            foreach (LogementCEModel logmntI in logIs)
                            {
                                if (logmntI.Qlin9NbreTotalMenage.GetValueOrDefault() != 0)
                                {
                                    LogementIsJson logJson = ModelMapper.MapToILJson(logmntI);
                                    List<MenageJson> menagesJsons = new List<MenageJson>();
                                    List<MenageCEModel> menages = ModelMapper.MapToListMenageCEModel(daoCE.searchAllMenageCE(logmntI.LogeId, logmntI.BatimentId, sdeId));
                                    foreach (MenageCEModel menage in menages)
                                    {
                                        MenageJson menageJson = ModelMapper.MapToJson(menage);
                                        //Ajout des deces
                                        List<DecesJson> decesJsons = new List<DecesJson>();
                                        List<DecesCEModel> deces = ModelMapper.MapToListDecesCEModel(daoCE.searchAllDecesCE(menage.BatimentId, menage.LogeId, sdeId, menage.MenageId));
                                        foreach (DecesCEModel dec in deces)
                                        {
                                            DecesJson decesJson = ModelMapper.MapToJson(dec);
                                            decesJsons.Add(decesJson);
                                        }
                                        menageJson.deces = new List<DecesJson>();
                                        menageJson.deces = decesJsons;

                                        //Ajout des individus
                                        List<IndividuJson> individusJsons = new List<IndividuJson>();
                                        List<IndividuCEModel> individus = ModelMapper.MapToListIndividuCEModel(daoCE.searchAllIndividuCE(menage.BatimentId, menage.LogeId, sdeId, menage.MenageId));
                                        foreach (IndividuCEModel ind in individus)
                                        {
                                            IndividuJson indJson = ModelMapper.MapToJson(ind);
                                            individusJsons.Add(indJson);
                                        }
                                        menageJson.individus = new List<IndividuJson>();
                                        menageJson.individus = individusJsons;
                                        menagesJsons.Add(menageJson);
                                    }
                                    logJson.menages = new List<MenageJson>();
                                    logJson.menages = menagesJsons;
                                    logementsIndividuels.Add(logJson);
                                }
                            }
                            batiment.logementIs = new List<LogementIsJson>();
                            batiment.logementIs = logementsIndividuels;
                            batiments.Add(batiment);
                        }
                    }
                    //
                }
                return batiments;
            }
            catch (Exception ex)
            {
                log.Info("Error:" + ex.Message);
            }
            return new List<Json.BatimentJson>();
        }
#endregion

        #region LOGEMENT METHODS
        /// <summary>
        /// Sauvegarder un logment collectif
        /// </summary>
        /// <param Name="log"></param>
        /// <returns></returns>
        public bool saveLogementCE(LogementCEModel log)
        {
            return daoCE.saveLogement(ModelMapper.MapToTbl_LogementCE(log));
        }
        /// <summary>
        /// Retourne la liste des logments contre-enquetes
        /// </summary>
        /// <param Name="BatimentId"></param>
        /// <param Name="SdeId"></param>
        /// <returns></returns>

        public List<LogementCEModel> searchLogement(int batimentId, string sdeId)
        {
            return ModelMapper.MapToListLogementCEModel(daoCE.searchLogementbyBatiment(batimentId, sdeId));
        }

        /// <summary>
        /// Modifier un logement
        /// </summary>
        /// <param Name="log"></param>
        /// <returns></returns>
        public bool updateLogement(LogementCEModel log)
        {
            return daoCE.updateLogement(ModelMapper.MapToTbl_LogementCE(log));
        }
        /// <summary>
        /// Retourne un logement individuel dans un batiment 
        /// </summary>
        /// <param Name="Batiment"></param>
        /// <param Name="LogementType"></param>
        /// <returns></returns>
        public LogementModel getFirstLogementInd(BatimentModel _batiment, LogementTypeModel _logementType)
        {
            try
            {
                return readerService.getAllLogement(_batiment, _logementType).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public LogementModel getFirstLogementCol(BatimentModel _batiment, LogementTypeModel _logementType)
        {
            try
            {
                return readerService.getAllLogement(_batiment, _logementType).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public LogementCEModel getLogementCE(int batimentId, string sdeId, int logId)
        {
            try
            {
                return ModelMapper.MapToLogementCEModel(daoCE.getLogementCE(batimentId, sdeId, logId));
            }
            catch (Exception)
            {

            }
            return null;
        }
        #endregion

        #region MENAGES METHODS
        public bool saveMenageCE(MenageCEModel men)
        {
            return daoCE.saveMenage(ModelMapper.MapToTbl_MenageCE(men));
        }

        public bool updateMenageCE(MenageCEModel men)
        {
            return daoCE.updateMenage(ModelMapper.MapToTbl_MenageCE(men));
        }
        public List<MenageCEModel> searchAllMenageCE(LogementCEModel _log)
        {
            return ModelMapper.MapToListMenageCEModel(daoCE.searchAllMenageCE(_log.BatimentId, _log.LogeId, _log.SdeId));
        }

        public List<MenageDetailsModel> searchAllInMenage(MenageCEModel _men, string type)
        {
            switch (type)
            {
                case "Dèsè":
                    List<MenageDetailsModel> _details = new List<MenageDetailsModel>();
                    foreach (DecesCEModel _dec in searchAllDecesCE(_men))
                    {
                        MenageDetailsModel det = new MenageDetailsModel();
                        det.Id = _dec.DecesId.ToString();
                        det.Name = "Dèsè- " + _dec.Qd2NoOrdre;
                        det.SdeId = _dec.SdeId;
                        det.Type = Constant.CODE_TYPE_DECES;
                        if (_dec.IsContreEnqueteMade == 1)
                        {
                            det.IsContreEnqueteMade = true;
                        }
                        else
                        {
                            det.IsContreEnqueteMade = false;
                        
                        }
                        if (_dec.IsValidated == 1)
                        {
                            det.Valide = true;
                        }
                        else
                        {
                            det.Valide = false;
                        }
                        _details.Add(det);
                    }
                    return _details;

                case "Emigre":
                    List<MenageDetailsModel> _detailsEmigre = new List<MenageDetailsModel>();
                    foreach (EmigreCEModel _em in ModelMapper.MapToListEmigreCEModel(daoCE.searchAllEmigres(_men.BatimentId,_men.LogeId,_men.MenageId,_men.SdeId)))
                    {
                        MenageDetailsModel det = new MenageDetailsModel();
                        det.Id = _em.EmigreId.ToString();
                        det.Name = "Emigre- " + _em.Qn1numeroOrdre;
                        det.SdeId = _em.SdeId;
                        det.Type = Constant.CODE_TYPE_DECES;
                        if (_em.IsContreEnqueteMade == 1)
                        {
                            det.IsContreEnqueteMade = true;
                        }
                        else
                        {
                            det.IsContreEnqueteMade = false;
                        
                        }
                        if (_em.IsValidated == 1)
                        {
                            det.Valide = true;
                        }
                        else
                        {
                            det.Valide = false;
                        }
                        _detailsEmigre.Add(det);
                    }
                    return _detailsEmigre;

                case "Endividi":
                    List<MenageDetailsModel> _detailsInd = new List<MenageDetailsModel>();
                    foreach (IndividuCEModel _dec in searchAllIndividuCE(_men))
                    {
                        if (_dec.Q3LienDeParente.GetValueOrDefault() == 1)
                        {
                            MenageDetailsModel det = new MenageDetailsModel();
                            det.Id = _dec.IndividuId.ToString();
                            det.Name = _dec.Qp1NoOrdre + "/ " + _dec.Q3Prenom + "/ (CM)";
                            det.SdeId = _dec.SdeId;
                            det.Type = Constant.CODE_TYPE_ENVDIVIDI;
                            if (_dec.IsContreEnqueteMade.GetValueOrDefault() == 1)
                            {
                                det.IsContreEnqueteMade = true;
                            }
                            else
                            {
                                det.IsContreEnqueteMade = false;

                            }
                            if (_dec.IsValidated.GetValueOrDefault() == 1)
                            {
                                det.Valide = true;
                            }
                            else
                            {
                                det.Valide = false;
                            }
                            _detailsInd.Add(det);
                        }
                        else
                        {
                            MenageDetailsModel det = new MenageDetailsModel();
                            det.Id = _dec.IndividuId.ToString();
                            det.Name = _dec.Qp1NoOrdre + "/" + _dec.Q3Prenom;
                            det.SdeId = _dec.SdeId;
                            det.Type = Constant.CODE_TYPE_ENVDIVIDI;
                            if (_dec.IsContreEnqueteMade.GetValueOrDefault() == 1)
                            {
                                det.IsContreEnqueteMade = true;
                            }
                            else
                            {
                                det.IsContreEnqueteMade = false;

                            }
                            if (_dec.IsValidated.GetValueOrDefault() == 1)
                            {
                                det.Valide = true;
                            }
                            else
                            {
                                det.Valide = false;
                            }
                            _detailsInd.Add(det);
                        }

                    }
                    return _detailsInd;
            }
            return null;
        }
        public MenageCEModel getMenageById(long batimentId, long logId,string sdeId, long id)
        {
            try
            {
                return ModelMapper.MapToMenageCEModel(daoCE.getMenageCE(batimentId, logId, sdeId, id));
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        #endregion

        #region INDIVIDU METHODS
        public bool saveIndividuCE(IndividuCEModel _ind)
        {
            return daoCE.saveIndividuCE(ModelMapper.MapToTbl_IndividuCE(_ind));
        }

        public bool updateIndividuCE(IndividuCEModel _ind)
        {
            return daoCE.upateIndividuCE(ModelMapper.MapToTbl_IndividuCE(_ind));
        }
        public List<IndividuCEModel> searchAllIndividuCE(MenageCEModel _men)
        {
            return ModelMapper.MapToListIndividuCEModel(daoCE.searchAllIndividuCE(_men.BatimentId, _men.LogeId, _men.SdeId, _men.MenageId));
        }
        public IndividuModel getFirstIndividu(MenageModel _menage)
        {
            throw new NotImplementedException();
        }
        public IndividuCEModel getIndividuCEModel(long individuId, string sdeId)
        {
            IndividuCEModel individu = new IndividuCEModel();
            Tbl_IndividusCE _ind = daoCE.getIndividuCEModel(individuId, sdeId);
            individu.BatimentId = _ind.BatimentId.GetValueOrDefault();
            individu.LogeId = _ind.LogeId.GetValueOrDefault();
            individu.MenageId = _ind.MenageId.GetValueOrDefault();
            individu.IndividuId = _ind.IndividuId;
            individu.SdeId = _ind.SdeId;
            individu.Q3LienDeParente = Convert.ToByte(_ind.Q3LienDeParente.GetValueOrDefault());
            individu.Q3aRaisonChefMenage = Convert.ToByte(_ind.Q3aRaisonChefMenage.GetValueOrDefault());
            individu.Q5bAge = Convert.ToByte(_ind.Q5bAge.GetValueOrDefault());
            individu.Q2Nom = _ind.Q2Nom;
            individu.Q3Prenom = _ind.Q3Prenom;
            individu.Q4Sexe = Convert.ToByte(_ind.Q4Sexe.GetValueOrDefault());
            individu.Qp7Nationalite = Convert.ToByte(_ind.Qp7Nationalite.GetValueOrDefault());
            individu.Qp7PaysNationalite = _ind.Qp7PaysNationalite;
            individu.Q7DateNaissanceJour = Convert.ToByte(_ind.Q7DateNaissanceJour.GetValueOrDefault());
            individu.Q7DateNaissanceMois = Convert.ToByte(_ind.Q7DateNaissanceMois.GetValueOrDefault());
            individu.Q7DateNaissanceAnnee = Convert.ToInt32(_ind.Q7DateNaissanceAnnee.GetValueOrDefault());
            individu.Qp10LieuNaissance = Convert.ToByte(_ind.Qp10LieuNaissance.GetValueOrDefault());
            individu.Qp10CommuneNaissance = _ind.Qp10CommuneNaissance;
            individu.Qp10LieuNaissanceVqse = _ind.Qp10LieuNaissanceVqse;
            individu.Qp10PaysNaissance = _ind.Qp10PaysNaissance;
            individu.Qp11PeriodeResidence = Convert.ToByte(_ind.Qp11PeriodeResidence.GetValueOrDefault());
            individu.Qe2FreqentationScolaireOuUniv = Convert.ToByte(_ind.Qe2FreqentationScolaireOuUniv.GetValueOrDefault());
            individu.Qe4aNiveauEtude = Convert.ToByte(_ind.Qe4aNiveauEtude.GetValueOrDefault());
            individu.Qe4bDerniereClasseOUAneEtude = Convert.ToByte(_ind.Qe4bDerniereClasseOUAneEtude.GetValueOrDefault());
            individu.Qsm1StatutMatrimonial = Convert.ToByte(_ind.Qsm1StatutMatrimonial.GetValueOrDefault());
            individu.Qa1ActEconomiqueDerniereSemaine = Convert.ToByte(_ind.Qa1ActEconomiqueDerniereSemaine.GetValueOrDefault());
            individu.Qa2ActAvoirDemele1 = Convert.ToByte(_ind.Qa2ActAvoirDemele1.GetValueOrDefault());
            individu.Qa2ActDomestique2 = Convert.ToByte(_ind.Qa2ActDomestique2.GetValueOrDefault());
            individu.Qa2ActCultivateur3 = Convert.ToByte(_ind.Qa2ActCultivateur3.GetValueOrDefault());
            individu.Qa2ActAiderParent4 = Convert.ToByte(_ind.Qa2ActAiderParent4.GetValueOrDefault());
            individu.Qa2ActAutre5 = Convert.ToByte(_ind.Qa2ActAutre5.GetValueOrDefault());
            individu.Qa8EntreprendreDemarcheTravail = Convert.ToByte(_ind.Qa8EntreprendreDemarcheTravail.GetValueOrDefault());
            individu.Qf1aNbreEnfantNeVivantM = Convert.ToByte(_ind.Qf1aNbreEnfantNeVivantM.GetValueOrDefault());
            individu.Qf2bNbreEnfantNeVivantF = Convert.ToByte(_ind.Qf2bNbreEnfantNeVivantF.GetValueOrDefault());
            individu.Qf2aNbreEnfantVivantM = Convert.ToByte(_ind.Qf2aNbreEnfantVivantM.GetValueOrDefault());
            individu.Qf2bNbreEnfantVivantF = Convert.ToByte(_ind.Qf2bNbreEnfantVivantF.GetValueOrDefault());
            individu.Qf3DernierEnfantJour = Convert.ToByte(_ind.Qf3DernierEnfantJour.GetValueOrDefault());
            individu.Qf3DernierEnfantMois = Convert.ToByte(_ind.Qf3DernierEnfantMois.GetValueOrDefault());
            individu.Qf3DernierEnfantAnnee = Convert.ToByte(_ind.Qf3DernierEnfantAnnee.GetValueOrDefault());
            individu.Qf4DENeVivantVit = Convert.ToByte(_ind.Qf4DENeVivantVit.GetValueOrDefault());
            individu.DureeSaisie = Convert.ToInt32(_ind.DureeSaisie.GetValueOrDefault());
            individu.IsContreEnqueteMade = _ind.IsContreEnqueteMade.GetValueOrDefault();
            individu.IsValidated = _ind.IsValidated.GetValueOrDefault();
            return individu;
        }

        public IndividuCEModel getIndividuCEModel(long batimentId, long logeId, long menageId, long individuId, string sdeId)
        {
            return ModelMapper.MapToIndividuCEModel(daoCE.getIndividuCEModel(batimentId, logeId, menageId, individuId, sdeId));
        }
        public List<IndividuCEModel> searchAllIndividuCE(long _logId)
        {
            return ModelMapper.MapToListIndividuCEModel(daoCE.searchAllIndividuCE(_logId));
        }
        #endregion

        #region Geo Methods
        public List<DepartementModel> searchAllDepartement()
        {
            return ModelMapper.MapToListDepartement(daoCE.searchAllDepartement());
        }

        public List<CommuneModel> searchAllCommuneByDept(string deptId)
        {
            return ModelMapper.MapToListCommune(daoCE.searchAllCommuneByDept(deptId));
        }

        public List<VqseModel> searchAllVqsebyCom(string comId)
        {
            return ModelMapper.MapToListVqse(daoCE.searchAllVqsebyCom(comId));
        }

        public List<PaysModel> searchAllPays()
        {
            return ModelMapper.MapToListPays(daoCE.searchAllPays());
        }
        public CommuneModel getCommune(string idCommune)
        {
            return ModelMapper.MapToCommune(daoCE.getCommune(idCommune));
        }

        public DepartementModel getDepartement(string idDepartement)
        {
            return ModelMapper.MapToDepartement(daoCE.getDepartement(idDepartement));
        }

        public PaysModel getPays(string idPays)
        {
            return ModelMapper.MapToPays(daoCE.getPays(idPays));
        }

        public VqseModel getVqse(string vqseId)
        {
            return ModelMapper.MapToVsqe(daoCE.getVqse(vqseId));
        }
        #endregion

        #region RAPPORT PERSONNEL
        public bool saveRptPersonnel(RapportPersonnelModel rpt)
        {
            return daoCE.saveRptPersonnel(ModelMapper.MapToTbl_RapportPersonnel(rpt));
        }
        public bool updateteRptPersonnel(RapportPersonnelModel rpt)
        {
            return daoCE.updateteRptPersonnel(ModelMapper.MapToTbl_RapportPersonnel(rpt));
        }

        public bool deleteRptPersonnel(int rptId)
        {
            return daoCE.deleteRptPersonnel(rptId);
        }

        public RapportPersonnelModel getRptPersonnel(int rptId)
        {
            return ModelMapper.MapToRapportPersonnelModel(daoCE.getRptPersonnel(rptId));
        }

        public List<RapportPersonnelModel> searchRptPersonnel()
        {
            return ModelMapper.MapToListRapportPersonnelModel(daoCE.searchRptPersonnel());
        }

        public List<RapportPersonnelModel> searchRptPersonnelByAgent(int persId)
        {
            return ModelMapper.MapToListRapportPersonnelModel(daoCE.searchRptPersonnelbyAgent(persId));
        }
        #endregion

        #region UPDATE SQLITE FILE AFTER DOING A CONTRE-ENQUETE
        public bool updateBatimentInSqlite(BatimentCEModel bat)
        {
            throw new NotImplementedException();
        }

        public bool updateLogementInSqlite(LogementCEModel log)
        {
            throw new NotImplementedException();
        }

        public bool updateMenageInSqlite(MenageCEModel men)
        {
            throw new NotImplementedException();
        }

        public bool updateDecesInSqlite(DecesCEModel dec)
        {
            throw new NotImplementedException();
        }

        public bool updateIndividuInSqlite(IndividuCEModel ind)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region INDICATEURS SOCIO-DEMOGRAPHIQUES PAR MENAGES
        public int getTotalPersonnesByMenage(MenageCEModel model)
        {
            List<IndividuCEModel> listOf = searchAllIndividuCE(model);
            if (listOf != null)
            {
                return listOf.Count();
            }
            return 0;
        }

        public int getTotalFemmesByMenage(MenageCEModel model)
        {
            List<IndividuCEModel> listOf = searchAllIndividuCE(model);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuCEModel ind in listOf)
                {
                    if (ind.Q4Sexe == 2)
                    {
                        nbre = nbre + 1;
                    }
                }
                return nbre;
            }
            return 0;
        }

        public int getTotalEnfantMoins5AnsByMenage(MenageCEModel model)
        {
            List<IndividuCEModel> listOf = searchAllIndividuCE(model);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuCEModel ind in listOf)
                {
                    if (ind.Q5bAge < 5)
                    {
                        nbre = nbre + 1;
                    }
                }
                return nbre;
            }
            return 0;
        }

        public int getTotalPersonnesMoin15AnsByMenage(MenageCEModel model)
        {
            List<IndividuCEModel> listOf = searchAllIndividuCE(model);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuCEModel ind in listOf)
                {
                    if (ind.Q5bAge < 15)
                    {
                        nbre = nbre + 1;
                    }
                }
                return nbre;
            }
            return 0;
        }

        public int getTotalDecesByMenage(MenageCEModel model)
        {
            // MenageModel Menage = GetMenageByLogement(logeId);
            return 0;
        }

        public int getTotalHandicapVoirByMen(MenageCEModel model)
        {
            //List<IndividuCEModel> listOf = searchAllIndividuCE(model);
            //if (listOf != null)
            //{
            //    int nbre = 0;
            //    foreach (IndividuCEModel ind in listOf)
            //    {
            //        if (ind.Q5bAge >= 5)
            //        {
            //            if (ind.Qaf1aHandicapVoir >=2)
            //            {
            //                nbre = nbre + 1;
            //            }
            //        }    
            //    }
            //    return nbre;
            //}
            return 0;
        }

        public int getTotalhandicapEntendrebyMen(MenageCEModel model)
        {
            //List<IndividuCEModel> listOf = searchAllIndividuCE(model);
            //if (listOf != null)
            //{
            //    int nbre = 0;
            //    foreach (IndividuCEModel ind in listOf)
            //    {
            //        if (ind.Q5bAge >= 5)
            //        {
            //            if (ind.Qaf2aHandicapEntendre >= 2)
            //            {
            //                nbre = nbre + 1;
            //            }
            //        }    
            //    }
            //    return nbre;
            //}
            return 0;
        }

        public int getTotalHandicapMarcherByMen(MenageCEModel model)
        {
            //List<IndividuCEModel> listOf = searchAllIndividuCE(model);
            //if (listOf != null)
            //{
            //    int nbre = 0;
            //    foreach (IndividuCEModel ind in listOf)
            //    {
            //        if (ind.Q5bAge >= 5)
            //        {
            //            if (ind.Qaf3aHandicapMarcher >= 2)
            //            {
            //                nbre = nbre + 1;
            //            }
            //        }    
            //    }
            //    return nbre;
            //}
            return 0;
        }

        public int getTotalhandicapSouvenirByMen(MenageCEModel model)
        {
            //List<IndividuCEModel> listOf = searchAllIndividuCE(model);
            //if (listOf != null)
            //{
            //    int nbre = 0;
            //    foreach (IndividuCEModel ind in listOf)
            //    {
            //        if (ind.Q5bAge >= 5)
            //        {
            //            if (ind.Qaf4aHandicapSouvenir >= 2)
            //            {
            //                nbre = nbre + 1;
            //            }
            //        }    
            //    }
            //    return nbre;
            //}
            return 0;
        }

        public int getTotalHandicapSoignerbyMen(MenageCEModel model)
        {
            //List<IndividuCEModel> listOf = searchAllIndividuCE(model);
            //if (listOf != null)
            //{
            //    int nbre = 0;
            //    foreach (IndividuCEModel ind in listOf)
            //    {
            //        if (ind.Q5bAge >= 5)
            //        {
            //            if (ind.Qaf5aHandicapPourSeSoigner >= 2)
            //            {
            //                nbre = nbre + 1;
            //            }
            //        }    
            //    }
            //    return nbre;
            //}
            return 0;
        }

        public int getTotalHandicapCommuniquerbyMen(MenageCEModel model)
        {
            //List<IndividuCEModel> listOf = searchAllIndividuCE(model);
            //if (listOf != null)
            //{
            //    int nbre = 0;
            //    foreach (IndividuCEModel ind in listOf)
            //    {
            //        if (ind.Q5bAge >= 5)
            //        {
            //            if (ind.Qaf6aHandicapCommuniquer >= 2)
            //            {
            //                nbre = nbre + 1;
            //            }
            //        }    
            //    }
            //    return nbre;
            //}
            return 0;
        }

        public int getTotalAnalphabetes15AnsbyMen(MenageCEModel model)
        {
            List<IndividuCEModel> listOf = searchAllIndividuCE(model);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuCEModel ind in listOf)
                {
                    if (ind.Q5bAge >= 15)
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

        public int getTotalPersonneFrequentantEcoleByMen(MenageCEModel model)
        {
            List<IndividuCEModel> listOf = searchAllIndividuCE(model);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuCEModel ind in listOf)
                {
                    if (ind.Qe2FreqentationScolaireOuUniv <= 6)
                    {
                        if (ind.Q5bAge >= 6 && ind.Q5bAge <= 24)
                        {
                            nbre = nbre + 1;
                        }
                    }
                }
                return nbre;
            }
            return 0;
        }

        public int getTotalPersonneNiveauSecondairebyMen(MenageCEModel model)
        {
            List<IndividuCEModel> listOf = searchAllIndividuCE(model);
            if (listOf != null)
            {
                int nbre = 0;
                foreach (IndividuCEModel ind in listOf)
                {
                    if (ind.Q5bAge >= 6 && ind.Q5bAge <= 24)
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

        public int getTotalFormationProByMen(MenageCEModel model)
        {
            List<IndividuCEModel> listOf = searchAllIndividuCE(model);
            //if (listOf != null)
            //{
            //    int nbre = 0;
            //    foreach (IndividuCEModel ind in listOf)
            //    {
            //        if (ind.Q5bAge >= 6 && ind.Q5bAge <= 24)
            //        {
            //            if (ind.Qe7FormationProf <= 4)
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

        public List<BatimentModel> searchBatimentByCE(int contreEnqueteId, string sdeId)
        {
            throw new NotImplementedException();
        }

        #region RAPPORT DEROULEMENT COLLECTE
        public long saveRptDeroulement(RapportDeroulementModel rptDeroulement)
        {
            try
            {
                if (rptDeroulement != null)
                {
                    long result = daoCE.saveRptDeroulement(ModelMapper.MapToTbl_RprtDeroulement(rptDeroulement));
                    return result;
                }
            }
            catch (RapportException ex)
            {
                throw new RapportException(ex.Message);
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public bool updateRptDeroulement(RapportDeroulementModel rptDeroulement)
        {
            try
            {
                if (rptDeroulement != null)
                {
                    bool result = daoCE.updateRptDeroulement(ModelMapper.MapToTbl_RprtDeroulement(rptDeroulement));
                    return result;
                }
            }
            catch (RapportException ex)
            {
                throw new RapportException(ex.Message);
            }
            catch (Exception)
            {

            }
            return false;
        }

        public bool saveDetailsDeroulement(DetailsRapportModel details)
        {
            try
            {
                if (details != null)
                {
                    bool result = daoCE.saveDetailsDeroulement(ModelMapper.MapToTbl_DetailsRapport(details));
                    return result;
                }
            }
            catch (RapportException ex)
            {
                throw new RapportException(ex.Message);
            }
            catch (Exception)
            {

            }
            return false;
        }

        public bool updateDetailsDeroulement(DetailsRapportModel details)
        {
            try
            {
                if (details != null)
                {
                    bool result = daoCE.updateDetailsDeroulement(ModelMapper.MapToTbl_DetailsRapport(details));
                    return result;
                }
            }
            catch (RapportException ex)
            {
                throw new RapportException(ex.Message);
            }
            catch (Exception)
            {

            }
            return false;
        }

        public List<RapportDeroulementModel> searchRptDeroulment()
        {
            List<RapportDeroulementModel> listOf = new List<RapportDeroulementModel>();
            try
            {
                return ModelMapper.MapToListRapportDeroulementModel(daoCE.searchRptDeroulment());
            }
            catch (RapportException ex)
            {
                throw new RapportException(ex.Message);
            }
            catch (Exception)
            {

            }
            return new List<RapportDeroulementModel>();
        }

        public List<DetailsRapportModel> searchDetailsReport(RapportDeroulementModel rptDeroulment)
        {
            List<DetailsRapportModel> listOf = new List<DetailsRapportModel>();
            try
            {
                return ModelMapper.MapToListDetailsRapportModel(daoCE.searchDetailsReport(ModelMapper.MapToTbl_RprtDeroulement(rptDeroulment)));
            }
            catch (RapportException ex)
            {
                throw new RapportException(ex.Message);
            }
            catch (Exception)
            {

            }
            return new List<DetailsRapportModel>();
        }
        public bool deleteDetailsDeroulement(long Id)
        {
            try
            {
                DetailsRapportModel details = getDetailsRapportDeroulement(Id);
                if (details != null)
                {
                    daoCE.getRepository().RapportDetailsDeroulementRepository.Delete(Id);
                    daoCE.getRepository().Save();
                    return true;
                }
            }
            catch (Exception)
            {

            }
            return false;
        }
        #endregion
        public DetailsRapportModel getDetailsRapportDeroulement(long id)
        {
            string methodName = "getDetailsRapportDeroulement";
            try
            {
                return ModelMapper.MapToDetailsRapportModel(daoCE.getRepository().RapportDetailsDeroulementRepository.Find(rpt => rpt.DetailsRapportId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                log.Info("ERREUR:ContreEnqueteService/" + methodName + " : " + ex.Message);
            }
            return new DetailsRapportModel();
        }


        #region emigre
        public bool saveEmigre(EmigreCEModel _emigre)
        {
            return daoCE.saveEmigre(ModelMapper.MapToTbl_EmigreCE(_emigre));
        }

        public bool updateEmigre(EmigreCEModel _emigre)
        {
            return daoCE.updateEmigre(ModelMapper.MapToTbl_EmigreCE(_emigre));
        }

        public EmigreCEModel getEmigreCEModel(long emigreId, string sdeId)
        {
            return ModelMapper.MapToEmigreCEModel(daoCE.getEmigreCEModel(emigreId, sdeId));
        }

        public EmigreCEModel getEmigreCEModel(long batimentId, long logId, long menageId, long emigreId, string sdeId)
        {
            return ModelMapper.MapToEmigreCEModel(daoCE.getEmigreCEModel(batimentId,logId,menageId,emigreId,sdeId));
        }
        #endregion





        
    }
}
