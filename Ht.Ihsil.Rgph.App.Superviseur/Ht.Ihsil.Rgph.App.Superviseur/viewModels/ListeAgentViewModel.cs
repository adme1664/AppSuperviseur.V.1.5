using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
    public class ListeAgentViewModel
    {
        private UtilisateurService service;
         public ObservableCollection<Tbl_Agent> listOfAgent { get; private set; }
        
        public ListeAgentViewModel()
        {
            service=new UtilisateurService();
            List<Tbl_Agent> lOfAgent = service.getAllAgentBySuperviseur(Users.users.CodeUtilisateur);
            listOfAgent = new ObservableCollection<Tbl_Agent>();
            foreach(var l in lOfAgent)
            {
                listOfAgent.Add(l);  
            }
         }
    }
}
