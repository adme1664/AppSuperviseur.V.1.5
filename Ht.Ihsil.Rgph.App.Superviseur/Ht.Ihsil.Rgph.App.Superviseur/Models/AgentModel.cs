using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public  class AgentModel
    {
        public int AgentId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Sexe { get; set; }
        public string Nif { get; set; }
        public string Cin { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string AgentName { get; set; }
    }
}
