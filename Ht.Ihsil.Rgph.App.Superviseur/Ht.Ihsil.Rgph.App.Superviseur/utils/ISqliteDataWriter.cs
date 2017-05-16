using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.utils
{
   public interface ISqliteDataWriter
    {
       bool syncroBatimentToServeur(BatimentModel bat);
       bool validate<T>(T obj,string sdeId);
       bool changeStatus<T>(T obj, string sdeId);
       bool contreEnqueteMade<T>(T obj, string sdeId);
       bool savePersonnel(tbl_personnel person);
       bool ifPersonExist(tbl_personnel person);

    }
}
