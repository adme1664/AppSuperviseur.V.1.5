using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsi.Rgph.DataAccess.Repositories;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.Exceptions;
using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.utils
{
    public class SqliteDataWriter : ISqliteDataWriter
    {
        public MainRepository repository;
        Logger log;
        private SqliteReader sr;
        private static string className = "SqliteDataWriter";

        public SqliteDataWriter()
        {
            //repository = new MainRepository(Utilities.getConnectionString(SdeId));
            log = new Logger();
        }
        public SqliteDataWriter(string sdeId)
        {
            repository = new MainRepository(Utilities.getConnectionString(Users.users.DatabasePath,sdeId));
            log = new Logger();
        }
        public SqliteDataWriter(string path, string fileName)
        {
            repository = new MainRepository(Utilities.getConnectionString(path, fileName));
            log = new Logger();
        }
        public SqliteDataWriter(bool isSuperviseurDatabase)
        {
            repository = new MainRepository(Utilities.getConnectionString(Users.users.SupDatabasePath),true);
            log = new Logger();
        }
        
        public bool syncroBatimentToServeur(BatimentModel bat)
        {
            try
            {
                if (repository == null)
                {
                    repository = new MainRepository(Utilities.getConnectionString(Users.users.DatabasePath, bat.SdeId));
                }
                int batID = Convert.ToInt32(bat.BatimentId);
                tbl_batiment batiment = repository.MBatimentRepository.Find(b => b.batimentId == batID).FirstOrDefault();
                if (batiment == null)
                {

                }
                else 
                {
                    batiment.isSynchroToCentrale = Convert.ToInt32(bat.IsSynchroToCentrale);
                    repository.MBatimentRepository.UpdateGB(batiment);
                    repository.SaveGB();
                    return true;
                }

            }
            catch (Exception ex)
            {
                log.Info("<>:SqliteDataWriter/syncroBatimentToServeur:Error:" + ex.Message);
                throw new MessageException("Erreur lors de la sauvegarde" + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Validate an object (batiment, Logement, Menage...)
        /// </summary>
        /// <typeparam Name="T"></typeparam>
        /// <param Name="obj"></param>
        /// <param Name="SdeId"></param>
        /// <returns>bool</returns>
        public bool validate<T>(T obj, string sdeId)
        {
            try
            {
                if (repository == null)
                {
                    repository = new MainRepository(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));
                }
                if (obj.ToString() == Constant.OBJET_MODEL_BATIMENT)
                {
                    BatimentModel bat = obj as BatimentModel;
                    int batID = Convert.ToInt32(bat.BatimentId);
                    tbl_batiment batiment = repository.MBatimentRepository.Find(b => b.batimentId == batID).FirstOrDefault();
                    if (batiment.batimentId != 0)
                    {
                        batiment.isValidated = Convert.ToInt32(bat.IsValidated);
                        repository.MBatimentRepository.UpdateGB(batiment);
                        repository.SaveGB();
                        return true;
                    }

                }
                if (obj.ToString() == Constant.OBJET_MODEL_LOGEMENT)
                {
                    LogementModel logm = obj as LogementModel;
                    tbl_logement logement = repository.MLogementRepository.FindOne(logm.LogeId);
                    if (logement.logeId != 0)
                    {
                        logement.isValidated = Convert.ToInt32(logm.IsValidated);
                        repository.MLogementRepository.UpdateGB(logement);
                        repository.SaveGB();
                        return true;
                    }

                }
                if (obj.ToString() == Constant.OBJET_MODEL_MENAGE)
                {
                    MenageModel men = obj as MenageModel;
                    tbl_menage menage = repository.MMenageRepository.FindOne(men.MenageId);
                    if (menage.menageId != 0)
                    {
                        menage.isValidated = Convert.ToInt32(men.IsValidated);
                        repository.MMenageRepository.UpdateGB(menage);
                        repository.SaveGB();
                        return true;
                    }

                }
               
            }
            catch (Exception ex)
            {
                log.Info("<>:SqliteDataWriter/validate:Error:" + ex.Message);
                throw new MessageException("Erreur lors de la sauvegarde" + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Change the status of an object in Rempli, Mal Repmli ou fini (Batiment, Logement, Menage...)
        /// </summary>
        /// <typeparam Name="T"></typeparam>
        /// <param Name="obj"></param>
        /// <param Name="SdeId"></param>
        /// <returns>bool</returns>
        public bool changeStatus<T>(T obj, string sdeId)
        {
            try
            {
                if (repository == null)
                {
                    repository = new MainRepository(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));
                }
                #region RETOUR BATIMENT
                if (obj.ToString() == Constant.OBJET_MODEL_BATIMENT)
                {
                    BatimentModel bat = obj as BatimentModel;
                    int batID = Convert.ToInt32(bat.BatimentId);
                    tbl_batiment batiment = repository.MBatimentRepository.Find(b => b.batimentId == batID).FirstOrDefault();
                    if (batiment.batimentId != 0)
                    {
                        batiment.statut = bat.Statut;
                        batiment.isVerified = bat.IsVerified;
                        batiment.isFieldAllFilled = Convert.ToInt32(bat.IsFieldAllFilled);
                        repository.MBatimentRepository.UpdateGB(batiment);
                        List<tbl_logement> logements = repository.MLogementRepository.Find(l => l.batimentId == batiment.batimentId).ToList();
                        if (logements != null)
                        {
                            foreach (tbl_logement lg in logements)
                            {
                                if (lg.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                {
                                    lg.statut = bat.Statut;
                                    lg.isVerified = (int)Constant.StatutVerifie.PasVerifie;
                                    repository.MLogementRepository.UpdateGB(lg);
                                }                                                                   
                                if (lg.qlin9NbreTotalMenage.GetValueOrDefault() != 0)
                                {
                                    List<tbl_menage> menages = repository.MMenageRepository.Find(m => m.logeId == lg.logeId).ToList();
                                    if (menages != null)
                                    {
                                        foreach (tbl_menage men in menages)
                                        {
                                            if (men.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                            {
                                                men.statut = lg.statut;
                                                men.isVerified = (int)Constant.StatutVerifie.PasVerifie;
                                                repository.MMenageRepository.UpdateGB(men);
                                            }
                                                
                                            //Changement de statut sur les eemigres
                                            if (men.qn1Emigration.GetValueOrDefault() != 0)
                                            {
                                                List<tbl_emigre> emigres = repository.MEmigreRepository.Find(em => em.menageId == men.menageId).ToList();
                                                foreach (tbl_emigre emigre in emigres)
                                                {
                                                    if (emigre.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                    {
                                                        emigre.statut = men.statut;
                                                        emigre.isVerified = (int)Constant.StatutVerifie.PasVerifie;
                                                        repository.MEmigreRepository.UpdateGB(emigre);
                                                    }
                                                        
                                                }
                                            }
                                            
                                            //Changement de statut sur les deces
                                            if (men.qd1NbreDecede.GetValueOrDefault() != 0)
                                            {
                                                List<tbl_deces> deces = repository.MDecesRepository.Find(em => em.menageId == men.menageId).ToList();
                                                foreach (tbl_deces dec in deces)
                                                {
                                                    if (dec.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                    {
                                                        dec.statut = men.statut;
                                                        dec.isVerified = (int)Constant.StatutVerifie.PasVerifie;
                                                        repository.MDecesRepository.UpdateGB(dec);
                                                    }                                                        
                                                }
                                            }
                                            //Changement de statut sur les individus
                                            if (men.qm11TotalIndividuVivant.GetValueOrDefault() != 0)
                                            {
                                                List<tbl_individu> individus = repository.MIndividuRepository.Find(em => em.menageId == men.menageId).ToList();
                                                foreach (tbl_individu ind in individus)
                                                {
                                                    if (ind.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                    {
                                                        ind.statut = men.statut;
                                                        ind.isVerified = (int)Constant.StatutVerifie.PasVerifie;
                                                        repository.MIndividuRepository.UpdateGB(ind);
                                                    }
                                                        
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        repository.SaveGB();
                        return true;
                    }
                }
                #endregion

                #region RETOUR LOGEMENT
                if (obj.ToString() == Constant.OBJET_MODEL_LOGEMENT)
                {
                    LogementModel logement = obj as LogementModel;
                    tbl_logement entity = repository.MLogementRepository.Find(l => l.logeId==logement.LogeId && l.sdeId == sdeId).FirstOrDefault();
                    if (entity != null)
                    {
                        entity.statut = logement.Statut;
                        entity.isFieldAllFilled = Convert.ToInt32(logement.IsFieldAllFilled);
                        repository.MLogementRepository.UpdateGB(entity);
                        repository.SaveGB();
                        //Mettre les objets non fini en statut malrempli
                        //On commence avec les menages
                        if (entity.qlin9NbreTotalMenage.GetValueOrDefault() != 0)
                        {
                            List<tbl_menage> menages = repository.MMenageRepository.Find(m => m.logeId == entity.logeId).ToList();
                            if (menages != null)
                            {
                                foreach (tbl_menage men in menages)
                                {
                                    if (men.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                        men.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                    repository.MMenageRepository.UpdateGB(men);
                                    //Changement de statut sur les eemigres
                                    if (men.qn1NbreEmigre.GetValueOrDefault() != 0)
                                    {
                                        List<tbl_emigre> emigres = repository.MEmigreRepository.Find(em => em.menageId == men.menageId).ToList();
                                        foreach (tbl_emigre emigre in emigres)
                                        {
                                            if (emigre.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                emigre.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                            repository.MEmigreRepository.UpdateGB(emigre);
                                        }
                                    }

                                    //Changement de statut sur les deces
                                    if (men.qd1Deces.GetValueOrDefault() != 0)
                                    {
                                        List<tbl_deces> deces = repository.MDecesRepository.Find(em => em.menageId == men.menageId).ToList();
                                        foreach (tbl_deces dec in deces)
                                        {
                                            if (dec.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                dec.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                            repository.MDecesRepository.UpdateGB(dec);
                                        }
                                    }
                                    //Changement de statut sur les individus
                                    if (men.qm11TotalIndividuVivant.GetValueOrDefault() != 0)
                                    {
                                        List<tbl_individu> individus = repository.MIndividuRepository.Find(em => em.menageId == men.menageId).ToList();
                                        foreach (tbl_individu ind in individus)
                                        {
                                            if (ind.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                ind.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                            repository.MIndividuRepository.UpdateGB(ind);
                                        }
                                    }
                                }
                            }
                        }
                                
                        //
                        tbl_batiment batToChange = repository.MBatimentRepository.Find(l => l.batimentId == logement.BatimentId && l.sdeId == sdeId).FirstOrDefault();
                        if (batToChange != null)
                        {
                            batToChange.statut = Convert.ToInt32(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                            repository.MBatimentRepository.UpdateGB(batToChange);
                            repository.SaveGB();
                        }
                        return true;
                    }
                }
                #endregion

                #region MENAGE
                if (obj.ToString() == Constant.OBJET_MODEL_MENAGE)
                {
                    MenageModel menage = obj as MenageModel;
                    tbl_menage menageEntity = repository.MMenageRepository.Find(m => m.menageId == menage.MenageId && m.sdeId == sdeId).FirstOrDefault();
                    if (menageEntity != null)
                    {
                        menageEntity.isFieldAllFilled = Convert.ToInt32(menage.IsFieldAllFilled);
                        menage.Statut = menage.Statut;
                        repository.MMenageRepository.UpdateGB(menageEntity);
                        repository.SaveGB();
                        //On change le statut du logement dans lequel se trouve le menage
                        tbl_logement logEntity = repository.MLogementRepository.Find(l => l.logeId == menage.LogeId && l.sdeId == sdeId).FirstOrDefault();
                        if (logEntity != null)
                        {
                            logEntity.statut = Convert.ToInt32(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                            repository.MLogementRepository.UpdateGB(logEntity);
                            repository.SaveGB();
                            //
                            #region On change les autres objetsqui se trouvent dans le logement
                                                        if (logEntity.qlin9NbreTotalMenage.GetValueOrDefault() != 0)
                                                        {
                                                            List<tbl_menage> menages = repository.MMenageRepository.Find(m => m.logeId == logEntity.logeId).ToList();
                                                            if (menages != null)
                                                            {
                                                                foreach (tbl_menage men in menages)
                                                                {
                                                                    if (men.statut != Constant.STATUT_MODULE_KI_FINI_1 && menageEntity.menageId!=men.menageId)
                                                                        men.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                                                    repository.MMenageRepository.UpdateGB(men);
                                                                    //Changement de statut sur les eemigres
                                                                    if (men.qn1Emigration.GetValueOrDefault() != 0)
                                                                    {
                                                                        List<tbl_emigre> emigres = repository.MEmigreRepository.Find(em => em.menageId == men.menageId).ToList();
                                                                        foreach (tbl_emigre emigre in emigres)
                                                                        {
                                                                            if (emigre.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                                                emigre.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                                                            repository.MEmigreRepository.UpdateGB(emigre);
                                                                        }
                                                                    }

                                                                    //Changement de statut sur les deces
                                                                    if (men.qd1Deces.GetValueOrDefault() != 0)
                                                                    {
                                                                        List<tbl_deces> deces = repository.MDecesRepository.Find(em => em.menageId == men.menageId).ToList();
                                                                        foreach (tbl_deces dec in deces)
                                                                        {
                                                                            if (dec.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                                                dec.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                                                            repository.MDecesRepository.UpdateGB(dec);
                                                                        }
                                                                    }
                                                                    //Changement de statut sur les individus
                                                                    if (men.qm11TotalIndividuVivant.GetValueOrDefault() != 0)
                                                                    {
                                                                        List<tbl_individu> individus = repository.MIndividuRepository.Find(em => em.menageId == men.menageId).ToList();
                                                                        foreach (tbl_individu ind in individus)
                                                                        {
                                                                            if (ind.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                                                ind.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                                                            repository.MIndividuRepository.UpdateGB(ind);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                            #endregion
                            //
                        }
                        //On change le statut du batiment dans lequel se trouve le menage
                        tbl_batiment batToChange = repository.MBatimentRepository.Find(b => b.batimentId == menage.BatimentId && b.sdeId == sdeId).FirstOrDefault();
                        if (batToChange != null)
                        {
                            batToChange.statut = Convert.ToInt32(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                            repository.MBatimentRepository.UpdateGB(batToChange);
                            repository.SaveGB();

                            //On change le statut des logements se trouvant a l'interieur d'un batiment
                            List<tbl_logement> logements = repository.MLogementRepository.Find(l => l.batimentId == batToChange.batimentId).ToList();
                            if (logements != null)
                            {
                                foreach (tbl_logement lg in logements)
                                {
                                    if (lg.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                        lg.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                    repository.MLogementRepository.UpdateGB(lg);
                                }
                            }
                            //
                        }
                        return true;
                    }
                }
                #endregion

                #region EMIGRE
                if (obj.ToString() == Constant.OBJET_MODEL_EMIGRE)
                {
                    EmigreModel emigre = obj as EmigreModel;
                    if (emigre != null)
                    {
                        tbl_emigre emigreToChange = repository.MEmigreRepository.Find(e => e.emigreId == emigre.EmigreId && e.sdeId == sdeId).FirstOrDefault();
                        if (emigreToChange != null)
                        {
                            emigreToChange.statut = emigre.Statut;
                            emigreToChange.isFieldAllFilled = Convert.ToInt32(emigre.IsFieldAllFilled);
                            repository.MEmigreRepository.UpdateGB(emigreToChange);
                            repository.SaveGB();
                        }
                        //On change le statut du menage dans lequel se trouve le deces
                        tbl_menage menageToChange = repository.MMenageRepository.Find(m => m.menageId == emigre.MenageId && m.sdeId == sdeId).FirstOrDefault();
                        if (menageToChange != null)
                        {
                            menageToChange.statut = Convert.ToInt32(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                            repository.MMenageRepository.UpdateGB(menageToChange);
                            repository.SaveGB();
                        }
                        //On change le statut du logement dans lequel se trouve le menage
                        tbl_logement logEntity = repository.MLogementRepository.Find(l => l.logeId == emigre.LogeId && l.sdeId == sdeId).FirstOrDefault();
                        if (logEntity != null)
                        {
                            logEntity.statut = Convert.ToInt32(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                            repository.MLogementRepository.UpdateGB(logEntity);
                            repository.SaveGB();

                            #region On change les autres objets qui se trouvent dans le logement
                            if (logEntity.qlin9NbreTotalMenage.GetValueOrDefault() != 0)
                            {
                                List<tbl_menage> menages = repository.MMenageRepository.Find(m => m.logeId == logEntity.logeId).ToList();
                                if (menages != null)
                                {
                                    foreach (tbl_menage men in menages)
                                    {
                                        if (men.statut != Constant.STATUT_MODULE_KI_FINI_1 && menageToChange.menageId != men.menageId)
                                            men.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                        repository.MMenageRepository.UpdateGB(men);
                                        //Changement de statut sur les eemigres
                                        if (men.qn1Emigration.GetValueOrDefault() != 0)
                                        {
                                            List<tbl_emigre> emigres = repository.MEmigreRepository.Find(em => em.menageId == men.menageId).ToList();
                                            foreach (tbl_emigre em in emigres)
                                            {
                                                if (em.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                    em.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                                repository.MEmigreRepository.UpdateGB(em);
                                            }
                                        }

                                        //Changement de statut sur les deces
                                        if (men.qd1Deces.GetValueOrDefault() != 0)
                                        {
                                            List<tbl_deces> deces = repository.MDecesRepository.Find(em => em.menageId == men.menageId).ToList();
                                            foreach (tbl_deces dec in deces)
                                            {
                                                if (dec.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                    dec.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                                repository.MDecesRepository.UpdateGB(dec);
                                            }
                                        }
                                        //Changement de statut sur les individus
                                        if (men.qm11TotalIndividuVivant.GetValueOrDefault() != 0)
                                        {
                                            List<tbl_individu> individus = repository.MIndividuRepository.Find(em => em.menageId == men.menageId).ToList();
                                            foreach (tbl_individu ind in individus)
                                            {
                                                if (ind.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                    ind.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                                repository.MIndividuRepository.UpdateGB(ind);
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        //On change le statut du batiment dans lequel se trouve le menage
                        tbl_batiment batToChange = repository.MBatimentRepository.Find(b => b.batimentId == emigre.BatimentId && b.sdeId == sdeId).FirstOrDefault();
                        if (batToChange != null)
                        {
                            batToChange.statut = Convert.ToInt32(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                            repository.MBatimentRepository.UpdateGB(batToChange);
                            repository.SaveGB();
                            //
                            //On change les logements qui se trouvent a l'interieur des batiments
                            List<tbl_logement> logements = repository.MLogementRepository.Find(l => l.batimentId == batToChange.batimentId).ToList();
                            if (logements != null)
                            {
                                foreach (tbl_logement lg in logements)
                                {
                                    if (lg.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                        lg.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                    repository.MLogementRepository.UpdateGB(lg);
                                }
                            }
                            //
                        }
                        return true;
                    }
                }
                #endregion

                #region DECES
                if (obj.ToString() == Constant.OBJET_MODEL_DECES)
                {
                    DecesModel deces = obj as DecesModel;
                    if (deces != null)
                    {
                        tbl_deces decesToChange = repository.MDecesRepository.Find(e => e.decesId == deces.DecesId && e.sdeId == sdeId).FirstOrDefault();
                        if (decesToChange != null)
                        {
                            decesToChange.statut = deces.Statut;
                            decesToChange.isFieldAllFilled = Convert.ToInt32(deces.IsFieldAllFilled);
                            repository.MDecesRepository.UpdateGB(decesToChange);
                            repository.SaveGB();
                        }
                        //On change le statut du menage dans lequel se trouve le deces
                        tbl_menage menageToChange = repository.MMenageRepository.Find(m => m.menageId == deces.MenageId && m.sdeId == sdeId).FirstOrDefault();
                        if (menageToChange != null)
                        {
                            menageToChange.statut = Convert.ToInt32(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                            repository.MMenageRepository.UpdateGB(menageToChange);
                            repository.SaveGB();
                        }
                        //On change le statut du logement dans lequel se trouve le menage
                        tbl_logement logEntity = repository.MLogementRepository.Find(l => l.logeId == deces.LogeId && l.sdeId == sdeId).FirstOrDefault();
                        if (logEntity != null)
                        {
                            logEntity.statut = Convert.ToInt32(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                            repository.MLogementRepository.UpdateGB(logEntity);
                            repository.SaveGB();

                            #region On change les autres objets qui se trouvent dans le logement
                            if (logEntity.qlin9NbreTotalMenage.GetValueOrDefault() != 0)
                            {
                                List<tbl_menage> menages = repository.MMenageRepository.Find(m => m.logeId == logEntity.logeId).ToList();
                                if (menages != null)
                                {
                                    foreach (tbl_menage men in menages)
                                    {
                                        if (men.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                            men.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                        repository.MMenageRepository.UpdateGB(men);
                                        //Changement de statut sur les eemigres
                                        if (men.qn1Emigration.GetValueOrDefault() != 0)
                                        {
                                            List<tbl_emigre> emigres = repository.MEmigreRepository.Find(em => em.menageId == men.menageId).ToList();
                                            foreach (tbl_emigre emigre in emigres)
                                            {
                                                if (emigre.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                    emigre.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                                repository.MEmigreRepository.UpdateGB(emigre);
                                            }
                                        }

                                        //Changement de statut sur les deces
                                        if (men.qd1Deces.GetValueOrDefault() != 0)
                                        {
                                            List<tbl_deces> listOfDeces = repository.MDecesRepository.Find(em => em.menageId == men.menageId).ToList();
                                            foreach (tbl_deces dec in listOfDeces)
                                            {
                                                if (dec.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                    dec.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                                repository.MDecesRepository.UpdateGB(dec);
                                            }
                                        }
                                        //Changement de statut sur les individus
                                        if (men.qm11TotalIndividuVivant.GetValueOrDefault() != 0)
                                        {
                                            List<tbl_individu> individus = repository.MIndividuRepository.Find(em => em.menageId == men.menageId).ToList();
                                            foreach (tbl_individu ind in individus)
                                            {
                                                if (ind.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                    ind.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                                repository.MIndividuRepository.UpdateGB(ind);
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        //On change le statut du batiment dans lequel se trouve le menage
                        tbl_batiment batToChange = repository.MBatimentRepository.Find(b => b.batimentId == deces.BatimentId && b.sdeId == sdeId).FirstOrDefault();
                        if (batToChange != null)
                        {
                            batToChange.statut = Convert.ToInt32(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                            repository.MBatimentRepository.UpdateGB(batToChange);
                            repository.SaveGB();

                            //On change le statut des logements se trouvant a l'interieur d'un batiment
                            List<tbl_logement> logements = repository.MLogementRepository.Find(l => l.batimentId == batToChange.batimentId).ToList();
                            if (logements != null)
                            {
                                foreach (tbl_logement lg in logements)
                                {
                                    if (lg.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                        lg.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                    repository.MLogementRepository.UpdateGB(lg);
                                }
                            }
                            //
                        }
                        return true;
                    }
                }
                #endregion

                #region INDIVIDU
                if (obj.ToString() == Constant.OBJET_MODEL_INDIVIDU)
                {
                    IndividuModel individu = obj as IndividuModel;
                    if (individu != null)
                    {
                        tbl_individu individuToChange = repository.MIndividuRepository.Find(e => e.individuId == individu.IndividuId && e.sdeId == sdeId).FirstOrDefault();
                        if (individuToChange != null)
                        {
                            individuToChange.statut = individu.Statut;
                            individuToChange.isFieldAllFilled = Convert.ToInt32(individu.IsFieldAllFilled);
                            repository.MIndividuRepository.UpdateGB(individuToChange);
                            repository.SaveGB();
                        }
                        //On change le statut du menage dans lequel se trouve le deces
                        tbl_menage menageToChange = repository.MMenageRepository.Find(m => m.menageId == individu.MenageId && m.sdeId == sdeId).FirstOrDefault();
                        if (menageToChange != null)
                        {
                            menageToChange.statut = Convert.ToInt32(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                            repository.MMenageRepository.UpdateGB(menageToChange);
                            repository.SaveGB();
                        }
                        //On change le statut du logement dans lequel se trouve le menage
                        tbl_logement logEntity = repository.MLogementRepository.Find(l => l.logeId == individu.LogeId && l.sdeId == sdeId).FirstOrDefault();
                        if (logEntity != null)
                        {
                            logEntity.statut = Convert.ToInt32(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                            repository.MLogementRepository.UpdateGB(logEntity);
                            repository.SaveGB();

                            #region On change les autres objetsqui se trouvent dans le logement
                            if (logEntity.qlin9NbreTotalMenage.GetValueOrDefault() != 0)
                            {
                                List<tbl_menage> menages = repository.MMenageRepository.Find(m => m.logeId == logEntity.logeId).ToList();
                                if (menages != null)
                                {
                                    foreach (tbl_menage men in menages)
                                    {
                                        if (men.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                            men.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                        repository.MMenageRepository.UpdateGB(men);
                                        //Changement de statut sur les eemigres
                                        if (men.qn1Emigration.GetValueOrDefault() != 0)
                                        {
                                            List<tbl_emigre> emigres = repository.MEmigreRepository.Find(em => em.menageId == men.menageId).ToList();
                                            foreach (tbl_emigre emigre in emigres)
                                            {
                                                if (emigre.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                    emigre.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                                repository.MEmigreRepository.UpdateGB(emigre);
                                            }
                                        }

                                        //Changement de statut sur les deces
                                        if (men.qd1Deces.GetValueOrDefault() != 0)
                                        {
                                            List<tbl_deces> listOfDeces = repository.MDecesRepository.Find(em => em.menageId == men.menageId).ToList();
                                            foreach (tbl_deces dec in listOfDeces)
                                            {
                                                if (dec.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                    dec.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                                repository.MDecesRepository.UpdateGB(dec);
                                            }
                                        }
                                        //Changement de statut sur les individus
                                        if (men.qm11TotalIndividuVivant.GetValueOrDefault() != 0)
                                        {
                                            List<tbl_individu> individus = repository.MIndividuRepository.Find(em => em.menageId == men.menageId).ToList();
                                            foreach (tbl_individu ind in individus)
                                            {
                                                if (ind.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                    ind.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                                repository.MIndividuRepository.UpdateGB(ind);
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        //On change le statut du batiment dans lequel se trouve le menage
                        tbl_batiment batToChange = repository.MBatimentRepository.Find(b => b.batimentId == individu.BatimentId && b.sdeId == sdeId).FirstOrDefault();
                        if (batToChange != null)
                        {
                            batToChange.statut = Convert.ToInt32(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                            repository.MBatimentRepository.UpdateGB(batToChange);
                            repository.SaveGB();

                            //On change le statut des logements se trouvant a l'interieur d'un batiment
                            List<tbl_logement> logements = repository.MLogementRepository.Find(l => l.batimentId == batToChange.batimentId).ToList();
                            if (logements != null)
                            {
                                foreach (tbl_logement lg in logements)
                                {
                                    if (lg.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                        lg.statut = Constant.STATUT_MODULE_KI_MAL_RANPLI_2;
                                    repository.MLogementRepository.UpdateGB(lg);
                                }
                            }
                            //
                        }
                        return true;
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                throw new MessageException("<=====================>:SqliteDataWriter/changeStatus:Error:" + ex.Message);
                //log.Info("<>:SqliteDataWriter/changeStatus:Error:" + ex.Message);
            }
            return false;
        }
        /// <summary>
        /// make a contre-enquete
        /// </summary>
        /// <typeparam Name="T"></typeparam>
        /// <param Name="obj"></param>
        /// <param Name="SdeId"></param>
        /// <returns>bool</returns>
        public bool contreEnqueteMade<T>(T obj, string sdeId)
        {
            try
            {
                if (repository == null)
                {
                    repository = new MainRepository(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));
                }
                if (obj.ToString() == Constant.OBJET_MODEL_BATIMENT)
                {
                    BatimentModel bat = obj as BatimentModel;
                    int batID = Convert.ToInt32(bat.BatimentId);
                    tbl_batiment batiment = repository.MBatimentRepository.Find(b => b.batimentId == batID).FirstOrDefault();
                    if (batiment.batimentId != 0)
                    {
                        batiment.isContreEnqueteMade = Convert.ToInt32(bat.IsContreEnqueteMade);
                        repository.MBatimentRepository.UpdateGB(batiment);
                        repository.SaveGB();
                        return true;
                    }

                }
                if (obj.ToString() == Constant.OBJET_MODEL_LOGEMENT)
                {
                    LogementModel logm = obj as LogementModel;
                    tbl_logement logement = repository.MLogementRepository.FindOne(logm.LogeId);
                    if (logement.logeId != 0)
                    {
                        logement.isContreEnqueteMade = Convert.ToInt32(logm.IsContreEnqueteMade);
                        repository.MLogementRepository.UpdateGB(logement);
                        repository.SaveGB();
                        return true;
                    }

                }
                if (obj.ToString() == Constant.OBJET_MODEL_MENAGE)
                {
                    MenageModel men = obj as MenageModel;
                    tbl_menage menage = repository.MMenageRepository.FindOne(men.MenageId);
                    if (menage.menageId != 0)
                    {
                        menage.isContreEnqueteMade = Convert.ToInt32(men.IsContreEnqueteMade);
                        repository.MMenageRepository.UpdateGB(menage);
                        repository.SaveGB();
                        return true;
                    }

                }
                
                if (obj.ToString() == Constant.OBJET_MODEL_DECES)
                {
                    DecesModel dec = obj as DecesModel;
                    tbl_deces deces = repository.MDecesRepository.FindOne(dec.DecesId);
                    if (deces.decesId != 0)
                    {
                        deces.isContreEnqueteMade = Convert.ToInt32(dec.IsContreEnqueteMade);
                        repository.MDecesRepository.UpdateGB(deces);
                        repository.SaveGB();
                        return true;
                    }
                }
                if (obj.ToString() == Constant.OBJET_MODEL_INDIVIDU)
                {
                    IndividuModel ind = obj as IndividuModel;
                    tbl_individu individu = repository.MIndividuRepository.FindOne(ind.IndividuId);
                    if (individu.individuId != 0)
                    {
                        individu.isContreEnqueteMade = Convert.ToInt32(ind.IsContreEnqueteMade);
                        repository.MIndividuRepository.UpdateGB(individu);
                        repository.SaveGB();
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new MessageException("Erreur lors de la sauvegarde" + ex.Message);
            }
            return false;
        }


        public bool savePersonnel(tbl_personnel person)
        {
            try
            {
                if (repository == null)
                {
                    repository = new MainRepository(Utilities.getConnectionString(Users.users.DatabasePath, person.sdeId));
                }
                if (person != null)
                {
                   
                        repository.MPersonnelRepository.Insert(person);
                        repository.SaveGB();
                        return true;
                }
            }
            catch (Exception ex)
            {
                log.Info("<>:SqliteDataWriter/savePersonnel:Error:" + ex.Message);
                throw new MessageException("Erreur lors de la sauvegarde" + ex.Message);
            }
            return false;
        }
        public bool ifPersonExist(tbl_personnel person)
        {
            try
            {
                if (repository == null)
                {
                    repository = new MainRepository(Utilities.getConnectionString(Users.users.DatabasePath, person.sdeId));
                }
                tbl_personnel pers = repository.MPersonnelRepository.FindOne(person.nomUtilisateur);
                if (pers != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Info("<>:SqliteDataWriter/ifPersonnExist:Error:" + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Changer le statut de verification de chaque objet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="sdeId"></param>
        /// <returns></returns>
        public bool verified<T>(T obj, string sdeId)
        {
            try
            {
                if (repository == null)
                {
                    repository = new MainRepository(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));
                }
                if (obj.ToString() == Constant.OBJET_MODEL_BATIMENT)
                {
                    BatimentModel bat = obj as BatimentModel;
                    int batID = Convert.ToInt32(bat.BatimentId);
                    tbl_batiment batiment = repository.MBatimentRepository.Find(b => b.batimentId == batID).FirstOrDefault();
                    if (batiment.batimentId != 0)
                    {
                        batiment.isVerified = (int)Constant.StatutVerifie.Verifie;
                        repository.MBatimentRepository.UpdateGB(batiment);
                        repository.SaveGB();
                        return true;
                    }

                }
                if (obj.ToString() == Constant.OBJET_MODEL_LOGEMENT)
                {
                    LogementModel logm = obj as LogementModel;
                    tbl_logement logement = repository.MLogementRepository.FindOne(logm.LogeId);
                    if (logement.logeId != 0)
                    {
                        logement.isVerified = (int)Constant.StatutVerifie.Verifie;
                        repository.MLogementRepository.UpdateGB(logement);
                        repository.SaveGB();
                        return true;
                    }

                }
                if (obj.ToString() == Constant.OBJET_MODEL_MENAGE)
                {
                    MenageModel men = obj as MenageModel;
                    tbl_menage menage = repository.MMenageRepository.FindOne(men.MenageId);
                    if (menage.menageId != 0)
                    {
                        menage.isVerified = (int)Constant.StatutVerifie.Verifie;
                        repository.MMenageRepository.UpdateGB(menage);
                        repository.SaveGB();
                        return true;
                    }

                }
                if (obj.ToString() == Constant.OBJET_EMIGRE)
                {
                    EmigreModel emigre = obj as EmigreModel;
                    tbl_emigre em = repository.MEmigreRepository.FindOne(emigre.EmigreId);
                    if (em.emigreId != 0)
                    {
                        em.isVerified = (int)Constant.StatutVerifie.Verifie;
                        repository.MEmigreRepository.UpdateGB(em);
                        repository.SaveGB();
                        return true;
                    }
                }
                if (obj.ToString() == Constant.OBJET_DECES)
                {
                    DecesModel dec = obj as DecesModel;
                    tbl_deces deces = repository.MDecesRepository.FindOne(dec.DecesId);
                    if (deces.decesId != 0)
                    {
                        deces.isVerified = (int)Constant.StatutVerifie.Verifie;
                        repository.MDecesRepository.UpdateGB(deces);
                        repository.SaveGB();
                        return true;
                    }
                }
                if (obj.ToString() == Constant.OBJET_INDIVIDU)
                {
                    IndividuModel ind = obj as IndividuModel;
                    tbl_individu individu = repository.MIndividuRepository.FindOne(ind.IndividuId);
                    if (individu.individuId != 0)
                    {
                        individu.isVerified = (int)Constant.StatutVerifie.Verifie;
                        repository.MIndividuRepository.UpdateGB(individu);
                        repository.SaveGB();
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                log.Info("<>:SqliteDataWriter/verified:Error:" + ex.Message);
                throw new MessageException("Erreur lors de la sauvegarde" + ex.Message);
            }
            return false;
        }

        public bool changeToVerified<T>(T obj, string sdeId, string path)
        {
            try
            {
                if (repository == null)
                {
                    repository = new MainRepository(Utilities.getConnectionString(path, sdeId));
                }
                #region VERIFIED BATIMENT
                if (obj.ToString() == Constant.OBJET_MODEL_BATIMENT)
                {
                    BatimentModel bat = obj as BatimentModel;
                    int batID = Convert.ToInt32(bat.BatimentId);
                    tbl_batiment batiment = repository.MBatimentRepository.Find(b => b.batimentId == batID).FirstOrDefault();
                    if (batiment.batimentId != 0)
                    {
                        batiment.isVerified = (int)Constant.StatutVerifie.Verifie;
                        repository.MBatimentRepository.UpdateGB(batiment);
                        List<tbl_logement> logements = repository.MLogementRepository.Find(l => l.batimentId == batiment.batimentId).ToList();
                        if (logements != null)
                        {
                            foreach (tbl_logement lg in logements)
                            {
                                if (lg.statut == Constant.STATUT_MODULE_KI_FINI_1)
                                {
                                    lg.isVerified = (int)Constant.StatutVerifie.Verifie;
                                    repository.MLogementRepository.UpdateGB(lg);
                                }
                                if (lg.qlin9NbreTotalMenage.GetValueOrDefault() != 0)
                                {
                                    List<tbl_menage> menages = repository.MMenageRepository.Find(m => m.logeId == lg.logeId).ToList();
                                    if (menages != null)
                                    {
                                        foreach (tbl_menage men in menages)
                                        {
                                            if (men.statut == Constant.STATUT_MODULE_KI_FINI_1)
                                            {
                                                men.isVerified = (int)Constant.StatutVerifie.Verifie;
                                                repository.MMenageRepository.UpdateGB(men);
                                            }

                                            //Changement de statut sur les eemigres
                                            if (men.qn1Emigration.GetValueOrDefault() != 0)
                                            {
                                                List<tbl_emigre> emigres = repository.MEmigreRepository.Find(em => em.menageId == men.menageId).ToList();
                                                foreach (tbl_emigre emigre in emigres)
                                                {
                                                    if (emigre.statut == Constant.STATUT_MODULE_KI_FINI_1)
                                                    {
                                                        emigre.isVerified = (int)Constant.StatutVerifie.Verifie;
                                                        repository.MEmigreRepository.UpdateGB(emigre);
                                                    }

                                                }
                                            }

                                            //Changement de statut sur les deces
                                            if (men.qd1NbreDecede.GetValueOrDefault() != 0)
                                            {
                                                List<tbl_deces> deces = repository.MDecesRepository.Find(em => em.menageId == men.menageId).ToList();
                                                foreach (tbl_deces dec in deces)
                                                {
                                                    if (dec.statut== Constant.STATUT_MODULE_KI_FINI_1)
                                                    {
                                                        dec.isVerified = (int)Constant.StatutVerifie.Verifie;
                                                        repository.MDecesRepository.UpdateGB(dec);
                                                    }
                                                }
                                            }
                                            //Changement de statut sur les individus
                                            if (men.qm11TotalIndividuVivant.GetValueOrDefault() != 0)
                                            {
                                                List<tbl_individu> individus = repository.MIndividuRepository.Find(em => em.menageId == men.menageId).ToList();
                                                foreach (tbl_individu ind in individus)
                                                {
                                                    if (ind.statut == Constant.STATUT_MODULE_KI_FINI_1)
                                                    {
                                                        ind.isVerified = (int)Constant.StatutVerifie.Verifie;
                                                        repository.MIndividuRepository.UpdateGB(ind);
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        repository.SaveGB();
                        return true;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new MessageException("<=====================>:SqliteDataWriter/changeStatus:Error:" + ex.Message);
                //log.Info("<>:SqliteDataWriter/changeStatus:Error:" + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// insert a new batiment
        /// </summary>
        /// <param name="bat"></param>
        /// <returns></returns>
        public bool insertBatiment(tbl_batiment bat)
        {
            string methodName = "insertBatiment";
            try
            {
                repository.MBatimentRepository.Insert(bat);
                repository.SaveGB();
                return true;
            }
            catch (Exception ex)
            {
                log.Info(className + "/" + methodName + ":" + ex.Message);
            }
            return false;
        }

        public bool updateBatiment(tbl_batiment batiment)
        {
            string methodName = "updateBatiment";
            try
            {
                tbl_batiment batToUpdate = repository.MBatimentRepository.FindOne(batiment.batimentId);
                batToUpdate.batimentId = Convert.ToInt32(batiment.batimentId);
                batToUpdate.deptId = batiment.deptId;
                batToUpdate.comId = batiment.comId;
                batToUpdate.vqseId = batiment.vqseId;
                batToUpdate.sdeId = batiment.sdeId;
                batToUpdate.zone = Convert.ToByte(batiment.zone);
                batToUpdate.disctrictId = batiment.disctrictId;
                batToUpdate.qhabitation = batiment.qhabitation;
                batToUpdate.qlocalite = batiment.qlocalite;
                batToUpdate.qadresse = batiment.qadresse;
                batToUpdate.qrec = batiment.qrec;
                batToUpdate.qrgph = batiment.qrgph;
                batToUpdate.qb1Etat = Convert.ToByte(batiment.qb1Etat);
                batToUpdate.qb2Type = Convert.ToByte(batiment.qb2Type);
                batToUpdate.qb3NombreEtage = Convert.ToByte(batiment.qb3NombreEtage);
                batToUpdate.qb4MateriauMur = Convert.ToByte(batiment.qb4MateriauMur);
                batToUpdate.qb5MateriauToit = Convert.ToByte(batiment.qb5MateriauToit);
                batToUpdate.qb6StatutOccupation = Convert.ToByte(batiment.qb6StatutOccupation);
                batToUpdate.qb7Utilisation1 = Convert.ToByte(batiment.qb7Utilisation1);
                batToUpdate.qb7Utilisation2 = Convert.ToByte(batiment.qb7Utilisation2);
                batToUpdate.qb8NbreLogeCollectif = Convert.ToByte(batiment.qb8NbreLogeCollectif);
                batToUpdate.qb8NbreLogeIndividuel = Convert.ToByte(batiment.qb8NbreLogeIndividuel);
                batToUpdate.statut = Convert.ToByte(batiment.statut);
                batToUpdate.dateEnvoi = batiment.dateEnvoi;
                //batToUpdate.isValidated = Convert.ToInt32(batiment.isValidated);
                batToUpdate.isSynchroToAppSup = Convert.ToInt32(batiment.isSynchroToAppSup);
                batToUpdate.isSynchroToCentrale = Convert.ToInt32(batiment.isSynchroToCentrale);
                batToUpdate.dateDebutCollecte = batiment.dateDebutCollecte;
                batToUpdate.dateFinCollecte = batiment.dateFinCollecte;
                batToUpdate.dureeSaisie = Convert.ToInt32(batiment.dureeSaisie);
                batToUpdate.isFieldAllFilled = Convert.ToInt32(batiment.isFieldAllFilled);
                batToUpdate.statut = Convert.ToInt32(batiment.statut);
                //batToUpdate.isContreEnqueteMade = Convert.ToInt32(batiment.isContreEnqueteMade);
                batToUpdate.latitude = batiment.latitude;
                batToUpdate.longitude = batiment.longitude;
                batToUpdate.codeAgentRecenceur = batiment.codeAgentRecenceur;
                //batToUpdate.isVerified = batiment.isVerified;                
                repository.MBatimentRepository.UpdateGB(batToUpdate);
                repository.SaveGB();
                return true;
            }
            catch (Exception ex)
            {
                log.Info(className + "/" + methodName + ":" + ex.Message);
            }
            return false;
        }

        public bool insertLogement(tbl_logement logement)
        {
            string methodName = "insertLogement";
            try
            {
                repository.MLogementRepository.Insert(logement);
                repository.SaveGB();
                return true;
            }
            catch (Exception ex)
            {
                log.Info(className + "/" + methodName + ":" + ex.Message);
            }
            return false;
        }

        public bool updateLogement(tbl_logement logement)
        {
            string methodName = "updateLogement";
            try
            {
                tbl_logement logToUpdate = repository.MLogementRepository.FindOne(logement.logeId);
                logToUpdate.logeId = Convert.ToInt32(logement.logeId);
                logToUpdate.batimentId = Convert.ToInt32(logement.batimentId);
                logToUpdate.sdeId = logement.sdeId;
                logToUpdate.qlCategLogement = Convert.ToByte(logement.qlCategLogement);
                logToUpdate.qlin1NumeroOrdre = Convert.ToByte(logement.qlin1NumeroOrdre);
                logToUpdate.qlc1TypeLogement = Convert.ToByte(logement.qlc1TypeLogement);
                logToUpdate.qlc2bTotalGarcon = Convert.ToByte(logement.qlc2bTotalGarcon);
                logToUpdate.qlc2bTotalFille = Convert.ToByte(logement.qlc2bTotalFille);
                logToUpdate.qlcTotalIndividus = Convert.ToByte(logement.qlcTotalIndividus);
                logToUpdate.qlin2StatutOccupation = Convert.ToByte(logement.qlin2StatutOccupation);
                logToUpdate.qlin3ExistenceLogement = Convert.ToByte(logement.qlin3ExistenceLogement);
                logToUpdate.qlin4TypeLogement = Convert.ToByte(logement.qlin4TypeLogement);
                logToUpdate.qlin5MateriauSol = Convert.ToByte(logement.qlin5MateriauSol);
                logToUpdate.qlin6NombrePiece = Convert.ToByte(logement.qlin6NombrePiece);
                logToUpdate.qlin7NbreChambreACoucher = Convert.ToByte(logement.qlin7NbreChambreACoucher);
                logToUpdate.qlin8NbreIndividuDepense = Convert.ToByte(logement.qlin8NbreIndividuDepense);
                logToUpdate.qlin9NbreTotalMenage = Convert.ToByte(logement.qlin9NbreTotalMenage);
                logToUpdate.statut = Convert.ToByte(logement.statut);
                //logToUpdate.isValidated = Convert.ToInt32(logement.isValidated);
                logToUpdate.dateDebutCollecte = logement.dateDebutCollecte;
                logToUpdate.dateFinCollecte = logement.dateFinCollecte;
                logToUpdate.dureeSaisie = Convert.ToInt32(logement.dureeSaisie);
                logToUpdate.isFieldAllFilled = Convert.ToInt32(logement.isFieldAllFilled);
                //logToUpdate.isContreEnqueteMade = Convert.ToInt32(logement.isContreEnqueteMade);
                logToUpdate.nbrTentative = Convert.ToByte(logement.nbrTentative);
                logToUpdate.codeAgentRecenceur = logement.codeAgentRecenceur;
                //logToUpdate.isVerified = logement.isVerified;
                repository.MLogementRepository.UpdateGB(logToUpdate);
                repository.SaveGB();
                return true;

            }
            catch (Exception ex)
            {
                log.Info(className + "/" + methodName + ":" + ex.Message);
            }
            return false;
        }

        public bool insertMenage(tbl_menage men)
        {
            string methodName = "insertMenage";
            try
            {
                repository.MMenageRepository.Insert(men);
                repository.SaveGB();
                return true;
            }
            catch (Exception ex)
            {
                log.Info(className + "/" + methodName + ":" + ex.Message);
            }
            return false;
        }

        public bool updateMenage(tbl_menage menage)
        {
            string methodName = "updateMenage";
            try
            {
                tbl_menage menToUpdate = repository.MMenageRepository.FindOne(menage.menageId);
                menToUpdate.menageId = Convert.ToInt32(menage.menageId);
                menToUpdate.logeId = Convert.ToInt32(menage.logeId);
                menToUpdate.batimentId = Convert.ToInt32(menage.batimentId);
                menToUpdate.sdeId = menage.sdeId;
                menToUpdate.qm1NoOrdre = Convert.ToByte(menage.qm1NoOrdre);
                menToUpdate.qm2ModeJouissance = Convert.ToByte(menage.qm2ModeJouissance);
                menToUpdate.qm3ModeObtentionLoge = Convert.ToByte(menage.qm3ModeObtentionLoge);
                menToUpdate.qm4_1ModeAprovEauABoire = Convert.ToByte(menage.qm4_1ModeAprovEauABoire);
                menToUpdate.qm4_2ModeAprovEauAUsageCourant = Convert.ToByte(menage.qm4_2ModeAprovEauAUsageCourant);
                menToUpdate.qm5SrcEnergieCuisson1 = Convert.ToByte(menage.qm5SrcEnergieCuisson1);
                menToUpdate.qm5SrcEnergieCuisson2 = Convert.ToByte(menage.qm5SrcEnergieCuisson2);
                menToUpdate.qm6TypeEclairage = Convert.ToByte(menage.qm6TypeEclairage);
                menToUpdate.qm7ModeEvacDechet = Convert.ToByte(menage.qm7ModeEvacDechet);
                menToUpdate.qm8EndroitBesoinPhysiologique = Convert.ToByte(menage.qm8EndroitBesoinPhysiologique);
                menToUpdate.qm9NbreRadio1 = Convert.ToInt32(menage.qm9NbreRadio1);
                menToUpdate.qm9NbreTelevision2 = Convert.ToInt32(menage.qm9NbreTelevision2);
                menToUpdate.qm9NbreRefrigerateur3 = Convert.ToInt32(menage.qm9NbreRefrigerateur3);
                menToUpdate.qm9NbreFouElectrique4 = Convert.ToInt32(menage.qm9NbreFouElectrique4);
                menToUpdate.qm9NbreOrdinateur5 = Convert.ToInt32(menage.qm9NbreOrdinateur5);
                menToUpdate.qm9NbreMotoBicyclette6 = Convert.ToInt32(menage.qm9NbreMotoBicyclette6);
                menToUpdate.qm9NbreVoitureMachine7 = Convert.ToInt32(menage.qm9NbreVoitureMachine7);
                menToUpdate.qm9NbreBateau8 = Convert.ToInt32(menage.qm9NbreBateau8);
                menToUpdate.qm9NbrePanneauGeneratrice9 = Convert.ToInt32(menage.qm9NbrePanneauGeneratrice9);
                menToUpdate.qm9NbreMilletChevalBourique10 = Convert.ToInt32(menage.qm9NbreMilletChevalBourique10);
                menToUpdate.qm9NbreBoeufVache11 = Convert.ToInt32(menage.qm9NbreBoeufVache11);
                menToUpdate.qm9NbreCochonCabrit12 = Convert.ToInt32(menage.qm9NbreCochonCabrit12);
                menToUpdate.qm9NbreBeteVolaille13 = Convert.ToInt32(menage.qm9NbreBeteVolaille13);
                menToUpdate.qm10AvoirPersDomestique = Convert.ToByte(menage.qm10AvoirPersDomestique);
                menToUpdate.qm10TotalDomestiqueFille = Convert.ToByte(menage.qm10TotalDomestiqueFille);
                menToUpdate.qm10TotalDomestiqueGarcon = Convert.ToByte(menage.qm10TotalDomestiqueGarcon);
                menToUpdate.qm11TotalIndividuVivant = Convert.ToInt32(menage.qm11TotalIndividuVivant);
                menToUpdate.qn1Emigration = Convert.ToByte(menage.qn1Emigration);
                menToUpdate.qn1NbreEmigre = Convert.ToByte(menage.qn1NbreEmigre);
                menToUpdate.qd1Deces = Convert.ToByte(menage.qd1Deces);
                menToUpdate.qd1NbreDecede = Convert.ToByte(menage.qd1NbreDecede);
                menToUpdate.statut = Convert.ToByte(menage.statut);
                //menToUpdate.isValidated = Convert.ToInt32(menage.isValidated);
                menToUpdate.dateDebutCollecte = menage.dateDebutCollecte;
                menToUpdate.dateFinCollecte = menage.dateFinCollecte;
                menToUpdate.dureeSaisie = Convert.ToInt32(menage.dureeSaisie);
                menToUpdate.isFieldAllFilled = Convert.ToInt32(menage.isFieldAllFilled);
                //menToUpdate.isContreEnqueteMade = Convert.ToInt32(menage.isContreEnqueteMade);
                menToUpdate.codeAgentRecenceur = menage.codeAgentRecenceur;
                //menToUpdate.isVerified = menage.isVerified;
                repository.MMenageRepository.UpdateGB(menToUpdate);
                repository.SaveGB();
                return true;
            }
            catch (Exception ex)
            {
                log.Info(className + "/" + methodName + ":" + ex.Message);
            }
            return false;
        }

        public bool insertDeces(DecesModel dec)
        {
            string methodName = "insertDeces";
            try
            {
                repository.MDecesRepository.Insert(EntityMapper.mapTo(dec));
                repository.SaveGB();
                return true;
            }
            catch (Exception ex)
            {
                log.Info(className + "/" + methodName + ":" + ex.Message);
            }
            return false;
        }

        public bool updateDeces(DecesModel dec)
        {
            string methodName = "updateDeces";
            try
            {
                repository.MDecesRepository.UpdateGB(EntityMapper.mapTo(dec));
                repository.SaveGB();
                return true;
            }
            catch (Exception ex)
            {
                log.Info(className + "/" + methodName + ":" + ex.Message);
            }
            return false;
        }

        public bool insertEmigre(EmigreModel em)
        {
            string methodName = "insertEmigre";
            try
            {
                repository.MEmigreRepository.Insert(EntityMapper.mapTo(em));
                repository.SaveGB();
                return true;
            }
            catch (Exception ex)
            {
                log.Info(className + "/" + methodName + ":" + ex.Message);
            }
            return false;
        }

        public bool updateEmigre(EmigreModel em)
        {
            string methodName = "updateEmigre";
            try
            {
                repository.MEmigreRepository.UpdateGB(EntityMapper.mapTo(em));
                repository.SaveGB();
                return true;
            }
            catch (Exception ex)
            {
                log.Info(className + "/" + methodName + ":" + ex.Message);
            }
            return false;
        }

        public bool insertIndividu(IndividuModel ind)
        {
            string methodName = "insertIndividu";
            try
            {
                repository.MIndividuRepository.Insert(EntityMapper.mapTo(ind));
                repository.SaveGB();
                return true;
            }
            catch (Exception ex)
            {
                log.Info(className + "/" + methodName + ":" + ex.Message);
            }
            return false;
        }

        public bool updateIndividu(IndividuModel ind)
        {
            string methodName = "updateIndividu";
            try
            {
                repository.MIndividuRepository.UpdateGB(EntityMapper.mapTo(ind));
                repository.SaveGB();
                return true;
            }
            catch (Exception ex)
            {
                log.Info(className + "/" + methodName + ":" + ex.Message);
            }
            return false;
        }


        public bool deleteBatiment(tbl_batiment bat)
        {
            try
            {
                repository.MBatimentRepository.DeleteGB(bat);
                repository.SaveGB();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("SQLWRITER:"+ex.Message);
            }
            return false;
        }

        public bool deleteLogement(tbl_logement bat)
        {
            try
            {
                repository.MLogementRepository.DeleteGB(bat);
                repository.SaveGB();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("SQLWRITER:" + ex.Message);

            }
            return false;
        }

        public bool deleteMenage(tbl_menage menage)
        {
            try
            {
                repository.MMenageRepository.DeleteGB(menage);
                repository.SaveGB();
                return true;

            }
            catch (Exception ex)
            {
                log.Info("SQLWRITER:" + ex.Message);
            }
            return false;
        }

        public bool deleteEmigre(tbl_emigre emigre)
        {
            try
            {
                repository.MEmigreRepository.DeleteGB(emigre);
                repository.SaveGB();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("SQLWRITER:" + ex.Message);
            }
            return false;
        }

        public bool deleteDeces(tbl_deces deces)
        {
            try
            {
                repository.MDecesRepository.DeleteGB(deces);
                repository.SaveGB();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("SQLWRITER:" + ex.Message);
            }
            return false;
        }

        public bool deleteIndividu(tbl_individu individu)
        {
            try
            {
                repository.MIndividuRepository.DeleteGB(individu);
                repository.SaveGB();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("SQLWRITER:" + ex.Message);
            }
            return false;
        }


        public bool insertQuestion(string question)
        {
            string methodName = "insertQuestion";
            try
            {
                repository.QuestionRepository.CommandSqlString(question);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("SQLWRITER:/"+methodName + ex.Message);
            }
            return false;
        }

        public bool insertQuestionReponse(string commandSql)
        {
            string methodName = "insertQuestionReponse";
            try
            {
                repository.QuestionReponseRepository.CommandSqlString(commandSql);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("SQLWRITER:/" + methodName + ex.Message);
            }
            return false;
        }

        public bool deleteQuestion(string commandSql)
        {
            string methodName = "deleteQuestion";
            try
            {
                repository.QuestionRepository.CommandSqlString(commandSql);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("SQLWRITER:/" + methodName + ex.Message);
            }
            return false;
        }

        public bool deleteQuestionReponse(string commandSql)
        {
            string methodName = "deleteQuestionReponse";
            try
            {
                repository.QuestionReponseRepository.CommandSqlString(commandSql);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("SQLWRITER:/" + methodName + ex.Message);
            }
            return false;
        }


        public bool commandSqlString(string command)
        {
            //string methodName = "deleteQuestionReponse";
            //try
            //{
            //    repository.QuestionReponseRepository.CommandSqlString(commandSql);
            //    repository.Save();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    log.Info("SQLWRITER:/" + methodName + ex.Message);
            //}
            return false;
        }

        public bool insertReponses(string commandSql)
        {
            string methodName = "deleteQuestionReponse";
            try
            {
                repository.ReponseRepository.CommandSqlString(commandSql);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("SQLWRITER:/" + methodName + ex.Message);
            }
            return false;
        }

        public bool deleteReponses(string commandSql)
        {
            string methodName = "deleteQuestionReponse";
            try
            {
                repository.ReponseRepository.CommandSqlString(commandSql);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("SQLWRITER:/" + methodName + ex.Message);
            }
            return false;
        }
    }
}
