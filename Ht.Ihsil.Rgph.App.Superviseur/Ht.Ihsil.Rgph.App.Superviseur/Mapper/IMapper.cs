
using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Mapper
{
    public interface IMapper
    {
        List<KeyValuePair<string, int>> MapToSdeDetail(Tbl_Sde s, int type);
        List<KeyValuePair<string, int>> MapToSdeDetail(Tbl_Sde s);
        BatimentModel MapToBatiment(tbl_batiment batiment);
        LogementModel MapToLogementInd(tbl_logement logement);
        LogementModel MapToLogementCollectif(tbl_logement logement);

        LogementCollectifModel MapLogementCModel(LogementModel _logement);
        LogementIndividuelModel MapLogementIModel(LogementModel _logement);
        MenageModel MapToMenage(tbl_menage menage);
        MenageModel MapToMenage1(tbl_menage menage);
        IndividuModel MapToIndividu(tbl_individu individu);
        DecesModel MapToDeces(tbl_deces deces);
        EmigreModel MapToEmigre(tbl_emigre emigre);
        MenageDetailsModel MapToMenageDetails<T>(T type);

    }
}
