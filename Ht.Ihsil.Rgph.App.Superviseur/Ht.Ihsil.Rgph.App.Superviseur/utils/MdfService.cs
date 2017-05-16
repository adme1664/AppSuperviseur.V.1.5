using Ht.Ihsi.Rgph.DataAccess.Dao;
using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
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

namespace Ht.Ihsil.Rgph.App.Superviseur.utils
{
    public class MdfService:IMdfService
    {
        MainRepository repository;
        Logger log;
        DaoContreEnquete dao;
        public MdfService()
        {
            log = new Logger();
            dao = new DaoContreEnquete(Utilities.getConnectionString(Users.users.SupDatabasePath));
        }
        public Tbl_Sde getSdeDetails(string sdeID)
        {
            try
            {
                return dao.getRepository().SdeRepository.FindOne(sdeID);
            }
            catch (Exception ex)
            {
                log.Info("<>MdfService/getSdeDetails:" + ex.Message);
            }
            return null;
        }



        public List<viewModels.SdeSumModel> getAllSdeSummary()
        {
            throw new NotImplementedException();
        }

        public Models.SdeModel[] getAllSde()
        {
            try
            {
                List<SdeModel> sdeModels=new List<SdeModel>();
                List<Tbl_Sde> sdes = dao.getRepository().SdeRepository.Find().ToList();
                if (sdes != null)
                {
                    foreach (Tbl_Sde sde in sdes)
                    {
                        SdeModel model = ModelMapper.MapToSdeModel(sde);
                        sdeModels.Add(model);
                    }
                    return sdeModels.ToArray();
                }
            }
            catch (Exception ex)
            {
                log.Info("<>MdfService/getAllSde:" + ex.Message);
            }
            return null;
        }

        public bool updateBatimentForCE(long batimentId, string sdeId, string statutCE)
        {
            throw new NotImplementedException();
        }


        public BatimentType getBatimentDataForCE(long batimentId, string sdeId)
        {
            try
            {
                BatimentType bat = WsModelMapper.MapReaderToBatimentType(repository.BatimentCERepository.Find(b => b.BatimentId==batimentId && b.SdeId == sdeId).FirstOrDefault());
                if (bat.batimentId != 0)
                {
                    //Recherche de logements collectifs
                    List<LogementCType> logementCs = WsModelMapper.MapToListLogementCType( repository.LogementCERepository.Find(l => l.BatimentId == bat.batimentId && l.SdeId == sdeId && l.QlCategLogement==Constant.TYPE_LOJMAN_KOLEKTIF).ToList());
                    int lCount = logementCs.Count;
                    if (lCount>0)
                    {
                        bat.logementCs = new LogementCType[lCount];
                        int iLogC = 0;
                        foreach (LogementCType lg in logementCs)
                        {

                            List<IndividuType> listOfLC = WsModelMapper.MapToListIndividuType( repository.IndividuRepository.Find(ind => ind.BatimentId == lg.batimentId && ind.LogeId == lg.logeId && ind.SdeId == lg.sdeId).ToList());
                            lCount = listOfLC.Count;
                            if (lCount > 0)
                            {
                                int ilgc = 0;
                                lg.individus = new IndividuType[lCount];
                                foreach (IndividuType ind in listOfLC)
                                {
                                    lg.individus[ilgc] = ind;
                                    ilgc+=1;
                                }
                            }
                            bat.logementCs[iLogC] = lg;
                            iLogC++;
                        }
                    }
                    //Recherche de logements individuels
                    List<LogementIType> logementsIs = WsModelMapper.MapToListLogementIType(repository.LogementCERepository.Find(l => l.BatimentId == bat.batimentId && l.SdeId == bat.sdeId && l.QlCategLogement == Constant.TYPE_LOJMAN_ENDIVIDYEL).ToList());
                    lCount = logementsIs.Count;
                    if (lCount > 0)
                    {
                        bat.logementIs = new LogementIType[lCount];
                        int iLogI = 0;
                        foreach (LogementIType li in logementsIs)
                        {
                            List<MenageType> menages = WsModelMapper.MapToListMenageType(repository.MenageRepository.Find(m=>m.BatimentId==li.batimentId && m.LogeId==li.logeId && m.SdeId==li.sdeId).ToList());
                            lCount = menages.Count;
                            if (lCount > 0)
                            {
                                li.menages = new MenageType[lCount];
                                int iMenage = 0;
                                foreach (MenageType men in menages)
                                {
                                    List<DecesType> deces = WsModelMapper.MapToListDecesType(repository.DecesRepository.Find(d => d.BatimentId == men.batimentId && d.LogeId == men.logeId && d.MenageId == men.menageId && d.SdeId == men.sdeId).ToList());
                                    lCount = deces.Count;
                                    if (lCount > 0)
                                    {
                                        men.deces = new DecesType[lCount];
                                        men.deces = deces.ToArray();
                                    }
                                    List<IndividuType> individus = WsModelMapper.MapToListIndividuType(repository.IndividuRepository.Find(ind => ind.BatimentId == men.batimentId && ind.LogeId == men.logeId && ind.MenageId == men.menageId && ind.SdeId == men.sdeId).ToList());
                                    lCount = individus.Count;
                                    if (lCount > 0)
                                    {
                                        men.individus = new IndividuType[lCount];
                                        men.individus = individus.ToArray();
                                    }
                                    li.menages[iMenage] = men;
                                    iMenage++;
                                }

                            }
                            bat.logementIs[iLogI] = li;
                            iLogI++;
                        }
                    }
                    return bat;
                }
                
            }
            catch (Exception)
            {

            }
            return null;
        }


        public List<Schema.ContreEnqueteType> getContreEnquete(string sdeId, int type)
        {
            try
            {
                return WsModelMapper.MapToListContreEnqueteType( repository.ContreEnqueteRepository.Find(c => c.SdeId == sdeId && c.TypeContreEnquete==type).ToList());
            }
            catch (Exception)
            {

            }
            return null;
        }
    }
}
