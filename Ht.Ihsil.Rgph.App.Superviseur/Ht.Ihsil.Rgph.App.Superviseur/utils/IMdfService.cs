using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.Schema;
using Ht.Ihsil.Rgph.App.Superviseur.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.utils
{
    public interface IMdfService
    {
        Tbl_Sde getSdeDetails(string sdeID);
        List<SdeSumModel> getAllSdeSummary();
        SdeModel[] getAllSde();
        BatimentType getBatimentDataForCE(long batimentId,string sdeId);
        List<ContreEnqueteType> getContreEnquete(string sdeId, int type);
        bool updateBatimentForCE(long batimentId, string sdeId, string statutCE);
        
    }
}
