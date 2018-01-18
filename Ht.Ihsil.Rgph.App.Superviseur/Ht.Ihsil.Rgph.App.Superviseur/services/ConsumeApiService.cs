using Ht.Ihsil.Rgph.App.Superviseur.Json;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.services
{
   public class ConsumeApiService 
    {
       static HttpClient client = null;
       public ConsumeApiService()
       {
           client = new HttpClient();
           client.BaseAddress = new Uri("http://localhost:8082/rgph/api/v1/management/");
           client.DefaultRequestHeaders.Accept.Clear();
           client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
       }
     public  async Task<Utilisateur> authenticateUser(Utilisateur userToConnect)
       {
           Utilisateur user = null;
           string utilisateur = JsonConvert.SerializeObject(userToConnect);
           HttpResponseMessage response = await client.GetAsync("authenticateUser/" + utilisateur);
           if (response.IsSuccessStatusCode)
           {
               user = await response.Content.ReadAsAsync<Utilisateur>();
           }
           return user;
       }
     public async Task<List<SdeBean>> listOfSde(Utilisateur userToConnect)
       {
           List<SdeBean> sdes = null;
           string utilisateur = JsonConvert.SerializeObject(userToConnect);
           HttpResponseMessage response = await client.GetAsync("searchSdesBySuperviseur/" + utilisateur);
           if (response.IsSuccessStatusCode)
           {
               sdes = await response.Content.ReadAsAsync<List<SdeBean>>();
           }
           return sdes;
       }
     public async Task<List<AgentJson>> listOfAgent(Utilisateur userToConnect)
       {
           List<AgentJson> agents = null;
           string utilisateur = JsonConvert.SerializeObject(userToConnect);
           HttpResponseMessage response = await client.GetAsync("searchAgentsBySuperviseur/" + utilisateur);
           if (response.IsSuccessStatusCode)
           {
               agents = await response.Content.ReadAsAsync<List<AgentJson>>();
           }
           return agents;
       }

       //async  Task runASync()
       //{
           

       //}
    }
}
