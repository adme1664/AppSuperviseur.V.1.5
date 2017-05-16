using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Mapper
{
    public class EntityMapper
    {
        public static Tbl_Utilisateur MapMUtilisateurInEntity(UtilisateurModel u)
        {
            return new Tbl_Utilisateur
            {
                Statut = u.Statut,
                CodeUtilisateur = u.CodeUtilisateur,
                MotDePasse = u.MotDePasse,
                Prenom = u.Prenom,
                ProfileId = u.ProfileId,
                Nom = u.Nom
            };
        }
        public static Tbl_Agent MapMAgentInInEntity(AgentModel a)
        {
            return new Tbl_Agent

            {
                AgentId = a.AgentId,
                Cin = a.Cin,
                CodeUtilisateur = a.Username,
                Email = a.Email,
                MotDePasse = a.Password,
                Nif = a.Nif,
                Nom = a.Nom,
                Prenom = a.Prenom,
                Sexe = a.Sexe,
                Telephone = a.Telephone,
            };
        }
    }
}
