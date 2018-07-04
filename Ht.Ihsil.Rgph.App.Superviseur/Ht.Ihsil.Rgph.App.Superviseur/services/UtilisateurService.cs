using Ht.Ihsi.Rgph.DataAccess.Dao;
using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ht.Ihsil.Rgph.App.Superviseur.RgphWebService;
using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.Utility.Utils;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsi.Rgph.DataAccess.Repositories;

namespace Ht.Ihsil.Rgph.App.Superviseur.services
{
    public class UtilisateurService:IUtilisateurService
    {
        private DaoUtilisateurs dao;
        private DaoSettings daoSettings;
        private RGPHServiceService ws;
        
        private Logger log;

        public UtilisateurService()
        {
            string path = Utilities.getConnectionString(Users.users.SupDatabasePath);
            dao = new DaoUtilisateurs(path);
            ws = new RGPHServiceService();
            log = new Logger();
            daoSettings = new DaoSettings(path);
        }
        public void insertUser(Models.UtilisateurModel utilisateur)
        {
            dao.insertUser(EntityMapper.MapMUtilisateurInEntity(utilisateur));
        }

        public void updateUser(Models.UtilisateurModel utilisateur)
        {
            dao.updateUser(EntityMapper.MapMUtilisateurInEntity(utilisateur));
        }

        public Models.UtilisateurModel findUserById(int idUser)
        {
            return ModelMapper.MapEUtilisateurInModel(dao.findUserById(idUser));
        }

        public Models.UtilisateurModel findUserByUsername(string username)
        {
            return ModelMapper.MapEUtilisateurInModel(dao.findUserByUsername(username));
        }

        public void deleteUser(long idUser)
        {
            dao.deleteUser(idUser);
        }

        public bool isSuperviseurAccountExist()
        {
            if (dao.getRepository().UtilisateurRepository.Find(u => u.ProfileId ==Constant.PROFIL_SUPERVISEUR_SUPERVISION).Count() > 0) return true;
            return false;
       
        }


        public UtilisateurModel authenticateUserLocally(string username, string password)
        {
            log.Info("<>======authenticateUserLocally avant:");
            UtilisateurModel user = findUserByUsername(username);
            if (Utils.IsNotNull(user))
            {
                log.Info("<>=======Password : " + password + " Pwd encryted : " + user.MotDePasse);
                    
                //if (user.MotDePasse==MD5Encoder.encode(Password))
                if (user.MotDePasse == password)
                {
                    log.Info("<>==========OK");
                    return user;
                }
            }
            return null;
        }

        public UtilisateurModel authenticateUserRemotely(string username, string password)
        {
            AuthenticateUserRequest req = new AuthenticateUserRequest();
            req.username = username;
            req.password = password;
            AuthenticateUserResponse response = ws.AuthenticateUser(req);
            log.Info("<>====AuthenticateUserResponse:" + response);
            if (Utils.IsNotNull(response))
            {
                if (response.responseHeader.statusCode == Constant.RESPONSE_HEADER_SUCCESS)
                {
                    UtilisateurModel u = ModelMapper.MapAuthenticateUserResponseInModel(response);
                    u.MotDePasse = MD5Encoder.encode(password);
                    log.Info("<>=======Password : "+password +" Pwd encryted : "+u.MotDePasse);
                    insertUser(u);
                    getAllAgentFromRemote(u.CodeUtilisateur);
                    return ModelMapper.MapAuthenticateUserResponseInModel(response);
                }
                else
                {
                    log.Info("<>====AuthenticateUserResponse:" + response.responseHeader.statusCode);
                }
            }
            return null;
        }


        public List<Tbl_Agent> getAllAgentBySuperviseur(string supId)
        {
            return dao.getAllAgentBySup(supId);
        }


        public void getAllAgentFromRemote(string supId)
        {
            SynchronizeRequest req=new SynchronizeRequest();
            req.username=supId;
            SynchronizeResponse response = ws.Synchronize(req);
            List<Ht.Ihsil.Rgph.App.Superviseur.RgphWebService.AgentSde> listOfAgent = response.data.ToList();
            log.Info("<>==========Numbre:" + listOfAgent.Count);
            foreach (var l in listOfAgent)
            {
                log.Info("<>==========Code Utilisateur:" + l.codeUtilisateur);
                dao.getRepository().AgentRepository.Insert(ModelMapper.MapEAgentSdeServiceInEntites(l));
                // SdeSummary  ss = new SdeSummary();
                //ss.SdeId = l.numSde;
                //ss.CountBatimentC=l.nbreBatiment;
                //dao.getRepository().SdeSummaryRepository.Insert(ss);

            }
            dao.getRepository().Save();
        }

        public bool isAsticAccountExist()
        {
            if (dao.getRepository().UtilisateurRepository.Find(u => u.ProfileId == 6).Count() > 0) return true;
            return false;
        }
    }
}
