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
                        batiment.isFieldAllFilled = Convert.ToInt32(bat.IsFieldAllFilled);
                        repository.MBatimentRepository.UpdateGB(batiment);
                        List<tbl_logement> logements = repository.MLogementRepository.Find(l => l.batimentId == batiment.batimentId).ToList();
                        if (logements != null)
                        {
                            foreach (tbl_logement lg in logements)
                            {
                                if(lg.statut!=Constant.STATUT_MODULE_KI_FINI_1)
                                    lg.statut = bat.Statut;
                                repository.MLogementRepository.UpdateGB(lg);
                                if (lg.qlin9NbreTotalMenage.GetValueOrDefault() != 0)
                                {
                                    List<tbl_menage> menages = repository.MMenageRepository.Find(m => m.logeId == lg.logeId).ToList();
                                    if (menages != null)
                                    {
                                        foreach (tbl_menage men in menages)
                                        {
                                            if(men.statut!=Constant.STATUT_MODULE_KI_FINI_1)
                                                men.statut = lg.statut;
                                            repository.MMenageRepository.UpdateGB(men);
                                            //Changement de statut sur les eemigres
                                            if (men.qn1Emigration.GetValueOrDefault() != 0)
                                            {
                                                List<tbl_emigre> emigres = repository.MEmigreRepository.Find(em => em.menageId == men.menageId).ToList();
                                                foreach (tbl_emigre emigre in emigres)
                                                {
                                                    if (emigre.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                        emigre.statut = men.statut;
                                                    repository.MEmigreRepository.UpdateGB(emigre);
                                                }
                                            }
                                            
                                            //Changement de statut sur les deces
                                            if (men.qd1NbreDecede.GetValueOrDefault() != 0)
                                            {
                                                List<tbl_deces> deces = repository.MDecesRepository.Find(em => em.menageId == men.menageId).ToList();
                                                foreach (tbl_deces dec in deces)
                                                {
                                                    if (dec.statut != Constant.STATUT_MODULE_KI_FINI_1)
                                                        dec.statut = men.statut;
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
                                                        ind.statut = men.statut;
                                                    repository.MIndividuRepository.UpdateGB(ind);
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
    }
}
