using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsi.Rgph.DataAccess.Dao
{
   public interface IDaoUtilisateurs
    {
        void insertUser(Tbl_Utilisateur utilisateur);
        void updateUser(Tbl_Utilisateur utilisateur);
        Tbl_Utilisateur findUserById(long idUser);
        Tbl_Utilisateur findUserByUsername(string username);
        void deleteUser(long idUser);
        List<Tbl_Utilisateur> findAllUser();
        List<Tbl_Agent> getAllAgentBySup(string supId);
         MainRepository getRepository();

    }
}
