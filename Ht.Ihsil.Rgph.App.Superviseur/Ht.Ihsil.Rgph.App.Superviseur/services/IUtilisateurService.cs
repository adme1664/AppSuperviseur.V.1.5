using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.services
{
   public interface IUtilisateurService
    {
       bool isSuperviseurAccountExist();
       bool isAsticAccountExist();
       UtilisateurModel authenticateUserLocally(string username, string password);
       UtilisateurModel authenticateUserRemotely(string username, string password);
        void insertUser(UtilisateurModel utilisateur);
        void updateUser(UtilisateurModel utilisateur);
        UtilisateurModel findUserById(int idUser);
        UtilisateurModel findUserByUsername(string username);

        void getAllAgentFromRemote(string supId);
        List<Tbl_Agent> getAllAgentBySuperviseur(string supId);
        void deleteUser(long idUser);
    }
}
