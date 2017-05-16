using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.Constants;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.RgphWebService;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ht.Ihsil.Rgph.App.Superviseur.Json;

namespace Ht.Ihsil.Rgph.App.Superviseur.Mapper
{
    public class ModelMapper
    {
        #region MAPPING SDE
        public static List<KeyValuePair<string, int>> MapToSdeDetail(Tbl_Sde s, int type)
        {
            List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
            try
            {
                int countf = 0;
                int counth = 0;
                if (type == Constant.CODE_TYPE_ENVDIVIDI)
                {
                    countf = Convert.ToInt32(s.TotalIndFRecense.GetValueOrDefault());
                    counth = Convert.ToInt32(s.TotalIndGRecense.GetValueOrDefault());
                }
                else if (type == Constant.CODE_TYPE_EMIGRE)
                {
                    countf = Convert.ToInt32(s.TotalEmigreFRecense.GetValueOrDefault());
                    counth = Convert.ToInt32(s.TotalEmigreGRecense.GetValueOrDefault());
                }
                else
                {
                    countf = Convert.ToInt32(s.TotalDecesFRecense.GetValueOrDefault());
                    counth = Convert.ToInt32(s.TotalDecesGRecense.GetValueOrDefault());
                }

                result.Add(new KeyValuePair<string, int>(" " + countf + " F", countf));
                result.Add(new KeyValuePair<string, int>(" " + counth + " G", counth));
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public static List<KeyValuePair<string, int>> MapToSdeDetail(Tbl_Sde s)
        {
            List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
            if (s != null)
            {
                result.Add(new KeyValuePair<string, int>("1", Convert.ToInt32(s.TotalBatRecense.GetValueOrDefault())));
                result.Add(new KeyValuePair<string, int>("2", Convert.ToInt32(s.TotalLogeCRecense.GetValueOrDefault())));
                result.Add(new KeyValuePair<string, int>("3", Convert.ToInt32(s.TotalLogeIRecense.GetValueOrDefault())));
                result.Add(new KeyValuePair<string, int>("4", Convert.ToInt32(s.TotalMenageRecense.GetValueOrDefault())));
                result.Add(new KeyValuePair<string, int>("5", Convert.ToInt32(s.TotalDecesRecense.GetValueOrDefault())));
                result.Add(new KeyValuePair<string, int>("6", Convert.ToInt32(s.TotalEmigreRecense.GetValueOrDefault())));
                result.Add(new KeyValuePair<string, int>("7", Convert.ToInt32(s.TotalIndRecense.GetValueOrDefault())));
            }
            return result;
        }
        public static SdeModel MapToSdeModel1(Tbl_Sde sde)
        {
            SdeModel model = new SdeModel();
            if (sde != null)
            {
                model.SdeId = sde.SdeId;
                model.NoOrdre = sde.NoOrdre;
            }
            return model;
        }
        public static SdeModel MapToSdeModel(Tbl_Sde sde)
        {
            SdeModel model = new SdeModel();
            try
            {
                model.SdeId = sde.SdeId;
                model.CodeDistrict = sde.CodeDistrict;
                model.NoOrdre = sde.NoOrdre;
                model.StatutContreEnquete = Convert.ToInt32(sde.StatutContreEnquete.GetValueOrDefault());
                model.StatutCollecte = Convert.ToInt32(sde.StatutCollecte.GetValueOrDefault());
                model.RaisonCouverture = Convert.ToInt32(sde.RaisonCouverture.GetValueOrDefault());
                model.TotalBatCartographie = Convert.ToInt32(sde.TotalBatCartographie.GetValueOrDefault());
                model.TotalBatRecense = Convert.ToInt32(sde.TotalBatRecense.GetValueOrDefault());
                model.TotalLogeCRecense = Convert.ToInt32(sde.TotalLogeCRecense.GetValueOrDefault());
                model.TotalLogeIRecense = Convert.ToInt32(sde.TotalLogeIRecense.GetValueOrDefault());
                model.TotalMenageRecense = Convert.ToInt32(sde.TotalMenageRecense.GetValueOrDefault());
                model.TotalBatRecenseV = Convert.ToInt32(sde.TotalBatRecenseV.GetValueOrDefault());
                model.TotalLogeCRecenseV = Convert.ToInt32(sde.TotalLogeCRecenseV.GetValueOrDefault());
                model.TotalLogeIRecenseV = Convert.ToInt32(sde.TotalLogeIRecenseV.GetValueOrDefault());
                model.TotalMenageRecenseV = Convert.ToInt32(sde.TotalMenageRecenseV.GetValueOrDefault());
                //model.TotalBatRecenseNV = Convert.ToInt32(sde.TotalBatRecenseNV.GetValueOrDefault());
                //model.TotalLogeCRecenseNV = Convert.ToInt32(sde.TotalLogeCRecenseNV_.GetValueOrDefault());
                model.TotalLogeIRecenseNV = Convert.ToInt32(sde.TotalLogeIRecenseNV.GetValueOrDefault());
                //model.TotalMenageRecenseNV = sde.TotalMenageRecenseNV.GetValueOrDefault();
                model.TotalIndRecense = Convert.ToInt32(sde.TotalIndRecense.GetValueOrDefault());
                model.TotalIndFRecense = Convert.ToInt32(sde.TotalIndFRecense.GetValueOrDefault());
                model.TotalIndGRecense = Convert.ToInt32(sde.TotalIndGRecense.GetValueOrDefault());
                model.TotalEmigreRecense = Convert.ToInt32(sde.TotalEmigreRecense.GetValueOrDefault());
                model.TotalEmigreFRecense = Convert.ToInt32(sde.TotalEmigreFRecense.GetValueOrDefault());
                model.TotalEmigreGRecense = Convert.ToInt32(sde.TotalEmigreGRecense.GetValueOrDefault());
                model.TotalDecesRecense = Convert.ToInt32(sde.TotalDecesRecense.GetValueOrDefault());
                model.TotalDecesFRecense = Convert.ToInt32(sde.TotalDecesFRecense.GetValueOrDefault());
                model.TotalDecesGRecense = Convert.ToInt32(sde.TotalDecesGRecense.GetValueOrDefault());
                model.TotalEnfantDeMoinsDe5Ans = Convert.ToInt32(sde.TotalEnfantDeMoinsDe5Ans.GetValueOrDefault());
                model.TotalIndividu10AnsEtPlus = Convert.ToInt32(sde.TotalIndividu10AnsEtPlus.GetValueOrDefault());
                model.TotalIndividu18AnsEtPlus = Convert.ToInt32(sde.TotalIndividu18AnsEtPlus.GetValueOrDefault());
                model.TotalIndividu65AnsEtPlus = Convert.ToInt32(sde.TotalIndividu65AnsEtPlus.GetValueOrDefault());
                model.TotalLogeIOccupeRecense = Convert.ToInt32(sde.TotalLogeIOccupeRecense.GetValueOrDefault());
                model.TotalLogeIOccupeRecenseNV = Convert.ToInt32(sde.TotalLogeIOccupeRecenseNV.GetValueOrDefault());
                model.TotalLogeIOccupeRecenseV = Convert.ToInt32(sde.TotalLogeIOccupeRecenseV.GetValueOrDefault());
                model.TotalLogeIRecense = Convert.ToInt32(sde.TotalLogeIRecense.GetValueOrDefault());
                model.TotalLogeIUsageTemporelRecense = Convert.ToInt32(sde.TotalLogeIUsageTemporelRecense.GetValueOrDefault());
                model.TotalLogeIUsageTemporelRecenseNV = Convert.ToInt32(model.TotalLogeIUsageTemporelRecenseNV.GetValueOrDefault());
                model.TotalLogeIUsageTemporelRecenseV = Convert.ToInt32(sde.TotalLogeIUsageTemporelRecenseV.GetValueOrDefault());
                model.TotalLogeIVideRecense = Convert.ToInt32(sde.TotalLogeIVideRecense.GetValueOrDefault());
                model.TotalLogeIVideRecenseNV = Convert.ToInt32(sde.TotalLogeIVideRecenseNV.GetValueOrDefault());
                model.TotalLogeIVideRecenseV = Convert.ToInt32(sde.TotalLogeIVideRecenseV.GetValueOrDefault());
                model.IndiceMasculinite = sde.IndiceMasculinite.GetValueOrDefault();
            }
            catch (Exception)
            {

            }

            return model;

        }
        public static Tbl_Sde MapToSde(SdeModel sde)
        {
            Tbl_Sde entity = new Tbl_Sde();
            try
            {
                entity.SdeId = sde.SdeId;
                entity.NoOrdre = sde.NoOrdre;
                entity.CodeDistrict = sde.CodeDistrict;
                //entity.StatutContreEnquete = sde.StatutContreEnquete.GetValueOrDefault();
                entity.StatutCollecte = sde.StatutCollecte.GetValueOrDefault();
                entity.RaisonCouverture = sde.RaisonCouverture.GetValueOrDefault();
                entity.TotalBatCartographie = sde.TotalBatCartographie.GetValueOrDefault();
                entity.TotalBatRecense = sde.TotalBatRecense.GetValueOrDefault();
                entity.TotalLogeCRecense = sde.TotalLogeCRecense.GetValueOrDefault();
                entity.TotalLogeIRecense = sde.TotalLogeIRecense.GetValueOrDefault();
                entity.TotalMenageRecense = sde.TotalMenageRecense.GetValueOrDefault();
                entity.TotalBatRecenseV = sde.TotalBatRecenseV.GetValueOrDefault();
                entity.TotalLogeCRecenseV = sde.TotalLogeCRecenseV.GetValueOrDefault();
                entity.TotalLogeIRecenseV = sde.TotalLogeIRecenseV.GetValueOrDefault();
                entity.TotalMenageRecenseV = sde.TotalMenageRecenseV.GetValueOrDefault();
                //entity.TotalBatRecenseNV = sde.TotalBatRecenseNV.GetValueOrDefault();
                //entity.TotalLogeCRecenseNV_ = sde.TotalLogeCRecenseNV.GetValueOrDefault();
                entity.TotalLogeIRecenseNV = sde.TotalLogeIRecenseNV.GetValueOrDefault();
                entity.TotalMenageRecenseNV = sde.TotalMenageRecenseNV.GetValueOrDefault();
                entity.TotalIndRecense = sde.TotalIndRecense.GetValueOrDefault();
                entity.TotalIndFRecense = sde.TotalIndFRecense.GetValueOrDefault();
                entity.TotalIndGRecense = sde.TotalIndGRecense.GetValueOrDefault();
                entity.TotalEmigreRecense = sde.TotalEmigreRecense.GetValueOrDefault();
                entity.TotalEmigreFRecense = sde.TotalEmigreFRecense.GetValueOrDefault();
                entity.TotalEmigreGRecense = sde.TotalEmigreGRecense.GetValueOrDefault();
                entity.TotalDecesRecense = sde.TotalDecesRecense.GetValueOrDefault();
                entity.TotalDecesFRecense = sde.TotalDecesFRecense.GetValueOrDefault();
                entity.TotalDecesGRecense = sde.TotalDecesGRecense.GetValueOrDefault();
                entity.TotalEnfantDeMoinsDe5Ans = sde.TotalEnfantDeMoinsDe5Ans.GetValueOrDefault();
                entity.TotalIndividu10AnsEtPlus = sde.TotalIndividu10AnsEtPlus.GetValueOrDefault();
                entity.TotalIndividu18AnsEtPlus = sde.TotalIndividu18AnsEtPlus.GetValueOrDefault();
                entity.TotalIndividu65AnsEtPlus = sde.TotalIndividu65AnsEtPlus.GetValueOrDefault();
                entity.TotalLogeIOccupeRecense = sde.TotalLogeIOccupeRecense.GetValueOrDefault();
                entity.TotalLogeIOccupeRecenseNV = sde.TotalLogeIOccupeRecenseNV.GetValueOrDefault();
                entity.TotalLogeIOccupeRecenseV = sde.TotalLogeIOccupeRecenseV.GetValueOrDefault();
                entity.TotalLogeIRecense = sde.TotalLogeIRecense.GetValueOrDefault();
                entity.TotalLogeIUsageTemporelRecense = sde.TotalLogeIUsageTemporelRecense.GetValueOrDefault();
                entity.TotalLogeIUsageTemporelRecenseNV = entity.TotalLogeIUsageTemporelRecenseNV.GetValueOrDefault();
                entity.TotalLogeIUsageTemporelRecenseV = sde.TotalLogeIUsageTemporelRecenseV.GetValueOrDefault();
                entity.TotalLogeIVideRecense = sde.TotalLogeIVideRecense.GetValueOrDefault();
                entity.TotalLogeIVideRecenseNV = sde.TotalLogeIVideRecenseNV.GetValueOrDefault();
                entity.TotalLogeIVideRecenseV = sde.TotalLogeIVideRecenseV.GetValueOrDefault();

            }
            catch (Exception)
            {

            }
            return entity;
        }
        public static List<SdeModel> MapToListSdeModel(List<Tbl_Sde> list)
        {
            List<SdeModel> listOfSde = new List<SdeModel>();
            if (list.Count != 0)
            {
                foreach (Tbl_Sde sde in list)
                {
                    SdeModel model = new SdeModel();
                    model = ModelMapper.MapToSdeModel(sde);
                    listOfSde.Add(model);
                }
            }
            return listOfSde;
        }
        #endregion

        #region MAPPING TBL_UTILISATION
        public static UtilisationModel MapToUtilisationModel(Tbl_Utilisation model)
        {
            UtilisationModel uti = new UtilisationModel();
            if (model != null)
            {
                uti.CodeCategorie = model.CodeCategorie;
                uti.CodeUtilisation = model.CodeUtilisation;
                uti.Libelle = model.Libelle;
                return uti;
            }

            return uti;
        }
        public static List<UtilisationModel> MapToListUtilisationModel(List<Tbl_Utilisation> listOf)
        {
            List<UtilisationModel> list = new List<UtilisationModel>();
            foreach (Tbl_Utilisation utilisation in listOf)
            {
                UtilisationModel model = MapToUtilisationModel(utilisation);
                list.Add(model);
            }
            return list;
        }
        #endregion

        #region TBL_UTILISATION && TBL_AGENT
        public static UtilisateurModel MapEUtilisateurInModel(Tbl_Utilisateur u)
        {
            if (u != null)
            {
                return new UtilisateurModel
                {
                    Statut = Convert.ToByte(u.Statut.GetValueOrDefault()),
                    CodeUtilisateur = u.CodeUtilisateur,
                    MotDePasse = u.MotDePasse,
                    Prenom = u.Prenom,
                    ProfileId = Convert.ToByte(u.ProfileId),
                    Nom = u.Nom
                };
            }
            return new UtilisateurModel();
        }
        public static AgentModel MapToAgentModel(Tbl_Agent a)
        {
            if (a != null)
            {
                return new AgentModel
                {
                    AgentId = Convert.ToInt32(a.AgentId),
                    Cin = a.Cin,
                    Username = a.CodeUtilisateur,
                    Email = a.Email,
                    Password = a.MotDePasse,
                    Nif = a.Nif,
                    Nom = a.Nom,
                    Prenom = a.Prenom,
                    Sexe = a.Sexe,
                    Telephone = a.Telephone,
                    AgentName = "" + a.Nom + " " + a.Prenom + "(" + a.CodeUtilisateur + ")"
                };
            }
            return new AgentModel();
        }
        public static Tbl_Agent MapEAgentSdeServiceInEntites(Ht.Ihsil.Rgph.App.Superviseur.RgphWebService.AgentSde a)
        {
            return new Tbl_Agent
            {
                //    Cin = a.cin,
                //    Username = a.Username,
                //    Email = a.email,
                //    Password = a.Password,
                //    NbreBatiment = a.nbreBatiment,
                //    Nif = a.nif,
                //    Nom = a.Nom,
                //    Prenom = a.Prenom,
                //    Sexe = a.sexe,
                //    Telephone = a.telephone,
                //    NumSde = a.numSde
            };
        }
        public static UtilisateurModel MapAuthenticateUserResponseInModel(AuthenticateUserResponse u)
        {
            return new UtilisateurModel
          {
              Statut = Convert.ToByte(u.statut),
              CodeUtilisateur = u.codeUtilisateur,
              MotDePasse = u.motDePasse,
              Prenom = u.prenom,
              ProfileId = u.profileId,
              Nom = u.nom
          };
        }
        #endregion

        #region JSON MAPPER
        public static BatimentJson MapToJson(BatimentModel batiment)
        {
            if (batiment != null)
            {
                return new BatimentJson
                {
                    BatimentId = Convert.ToInt32(batiment.BatimentId),
                    DeptId = batiment.DeptId,
                    ComId = batiment.ComId,
                    VqseId = batiment.VqseId,
                    SdeId = batiment.SdeId,
                    Zone = Convert.ToByte(batiment.Zone),
                    DisctrictId = batiment.DisctrictId,
                    Qhabitation = batiment.Qhabitation,
                    Qlocalite = batiment.Qlocalite,
                    Qadresse = batiment.Qadresse,
                    Qrec = batiment.Qrec,
                    Qrgph = batiment.Qrgph,
                    Qb1Etat = Convert.ToByte(batiment.Qb1Etat),
                    Qb2Type = Convert.ToByte(batiment.Qb2Type),
                    Qb3NombreEtage = Convert.ToByte(batiment.Qb3NombreEtage),
                    Qb4MateriauMur = Convert.ToByte(batiment.Qb4MateriauMur),
                    Qb5MateriauToit = Convert.ToByte(batiment.Qb5MateriauToit),
                    Qb6StatutOccupation = Convert.ToByte(batiment.Qb6StatutOccupation),
                    Qb7Utilisation1 = Convert.ToByte(batiment.Qb7Utilisation1),
                    Qb7Utilisation2 = Convert.ToByte(batiment.Qb7Utilisation2),
                    Qb8NbreLogeCollectif = Convert.ToByte(batiment.Qb8NbreLogeCollectif),
                    Qb8NbreLogeIndividuel = Convert.ToByte(batiment.Qb8NbreLogeIndividuel),
                    Statut = Convert.ToByte(batiment.Statut),
                    DateEnvoi = batiment.DateEnvoi,
                    IsValidated = Convert.ToBoolean(batiment.IsValidated),
                    IsSynchroToAppSup = Convert.ToBoolean(batiment.IsSynchroToAppSup),
                    IsSynchroToCentrale = Convert.ToBoolean(batiment.IsSynchroToCentrale),
                    DateDebutCollecte = batiment.DateDebutCollecte,
                    DateFinCollecte = batiment.DateFinCollecte,
                    DureeSaisie = Convert.ToInt32(batiment.DureeSaisie),
                    IsFieldAllFilled = Convert.ToBoolean(batiment.IsFieldAllFilled),
                    IsContreEnqueteMade = Convert.ToBoolean(batiment.IsContreEnqueteMade),
                    Latitude = batiment.Latitude,
                    Longitude = batiment.Longitude,
                    CodeAgentRecenceur = batiment.CodeAgentRecenceur
                };
            }
            return new BatimentJson();
        }
        public static LogementJson MapToJson(LogementModel logement)
        {
            if (logement != null)
            {
                return new LogementJson
                {
                    LogeId = Convert.ToInt32(logement.LogeId),
                    BatimentId = Convert.ToInt32(logement.BatimentId),
                    SdeId = logement.SdeId,
                    QlCategLogement = Convert.ToByte(logement.QlCategLogement),
                    Qlin1NumeroOrdre = Convert.ToByte(logement.Qlin1NumeroOrdre),
                    Qlc1TypeLogement = Convert.ToByte(logement.Qlc1TypeLogement),
                    Qlc2bTotalGarcon = Convert.ToByte(logement.Qlc2bTotalGarcon),
                    Qlc2bTotalFille = Convert.ToByte(logement.Qlc2bTotalFille),
                    QlcTotalIndividus = Convert.ToByte(logement.QlcTotalIndividus),
                    Qlin2StatutOccupation = Convert.ToByte(logement.Qlin2StatutOccupation),
                    Qlin3ExistenceLogement = Convert.ToByte(logement.Qlin3ExistenceLogement),
                    Qlin4TypeLogement = Convert.ToByte(logement.Qlin4TypeLogement),
                    Qlin5MateriauSol = Convert.ToByte(logement.Qlin5MateriauSol),
                    Qlin6NombrePiece = Convert.ToByte(logement.Qlin6NombrePiece),
                    Qlin7NbreChambreACoucher = Convert.ToByte(logement.Qlin7NbreChambreACoucher),
                    Qlin8NbreIndividuDepense = Convert.ToByte(logement.Qlin8NbreIndividuDepense),
                    Qlin9NbreTotalMenage = Convert.ToByte(logement.Qlin9NbreTotalMenage),
                    Statut = Convert.ToByte(logement.Statut),
                    IsValidated = Convert.ToBoolean(logement.IsValidated),
                    DateDebutCollecte = logement.DateDebutCollecte,
                    DateFinCollecte = logement.DateFinCollecte,
                    DureeSaisie = Convert.ToInt32(logement.DureeSaisie),
                    IsFieldAllFilled = Convert.ToBoolean(logement.IsFieldAllFilled),
                    IsContreEnqueteMade = Convert.ToBoolean(logement.IsContreEnqueteMade),
                    NbrTentative = Convert.ToByte(logement.NbrTentative),
                    CodeAgentRecenceur = logement.CodeAgentRecenceur
                };
            }
            return new LogementJson();
        }
        public static MenageJson MapToJson(MenageModel menage)
        {
            if (menage != null)
            {
                return new MenageJson
                {
                    MenageId = Convert.ToInt32(menage.MenageId),
                    LogeId = Convert.ToInt32(menage.LogeId),
                    BatimentId = Convert.ToInt32(menage.BatimentId),
                    SdeId = menage.SdeId,
                    Qm1NoOrdre = Convert.ToByte(menage.Qm1NoOrdre),
                    Qm2ModeJouissance = Convert.ToByte(menage.Qm2ModeJouissance),
                    Qm3ModeObtentionLoge = Convert.ToByte(menage.Qm3ModeObtentionLoge),
                    Qm4_1ModeAprovEauABoire = Convert.ToByte(menage.Qm4_1ModeAprovEauABoire),
                    Qm4_2ModeAprovEauAUsageCourant = Convert.ToByte(menage.Qm4_2ModeAprovEauAUsageCourant),
                    Qm5SrcEnergieCuisson1 = Convert.ToByte(menage.Qm5SrcEnergieCuisson1),
                    Qm5SrcEnergieCuisson2 = Convert.ToByte(menage.Qm5SrcEnergieCuisson2),
                    Qm6TypeEclairage = Convert.ToByte(menage.Qm6TypeEclairage),
                    Qm7ModeEvacDechet = Convert.ToByte(menage.Qm7ModeEvacDechet),
                    Qm8EndroitBesoinPhysiologique = Convert.ToByte(menage.Qm8EndroitBesoinPhysiologique),
                    Qm9NbreRadio1 = Convert.ToInt32(menage.Qm9NbreRadio1),
                    Qm9NbreTelevision2 = Convert.ToInt32(menage.Qm9NbreTelevision2),
                    Qm9NbreRefrigerateur3 = Convert.ToInt32(menage.Qm9NbreRefrigerateur3),
                    Qm9NbreFouElectrique4 = Convert.ToInt32(menage.Qm9NbreFouElectrique4),
                    Qm9NbreOrdinateur5 = Convert.ToInt32(menage.Qm9NbreOrdinateur5),
                    Qm9NbreMotoBicyclette6 = Convert.ToInt32(menage.Qm9NbreMotoBicyclette6),
                    Qm9NbreVoitureMachine7 = Convert.ToInt32(menage.Qm9NbreVoitureMachine7),
                    Qm9NbreBateau8 = Convert.ToInt32(menage.Qm9NbreBateau8),
                    Qm9NbrePanneauGeneratrice9 = Convert.ToInt32(menage.Qm9NbrePanneauGeneratrice9),
                    Qm9NbreMilletChevalBourique10 = Convert.ToInt32(menage.Qm9NbreMilletChevalBourique10),
                    Qm9NbreBoeufVache11 = Convert.ToInt32(menage.Qm9NbreBoeufVache11),
                    Qm9NbreCochonCabrit12 = Convert.ToInt32(menage.Qm9NbreCochonCabrit12),
                    Qm9NbreBeteVolaille13 = Convert.ToInt32(menage.Qm9NbreBeteVolaille13),
                    Qm10AvoirPersDomestique = Convert.ToByte(menage.Qm10AvoirPersDomestique),
                    Qm10TotalDomestiqueFille = Convert.ToByte(menage.Qm10TotalDomestiqueFille),
                    Qm10TotalDomestiqueGarcon = Convert.ToByte(menage.Qm10TotalDomestiqueGarcon),
                    Qm11TotalIndividuVivant = Convert.ToInt32(menage.Qm11TotalIndividuVivant),
                    Qn1Emigration = Convert.ToByte(menage.Qn1Emigration),
                    Qn1NbreEmigre = Convert.ToByte(menage.Qn1NbreEmigre),
                    Qd1Deces = Convert.ToByte(menage.Qd1Deces),
                    Qd1NbreDecede = Convert.ToByte(menage.Qd1NbreDecede),
                    Statut = Convert.ToByte(menage.Statut),
                    IsValidated = Convert.ToBoolean(menage.IsValidated),
                    DateDebutCollecte = menage.DateDebutCollecte,
                    DateFinCollecte = menage.DateFinCollecte,
                    DureeSaisie = Convert.ToInt32(menage.DureeSaisie),
                    IsFieldAllFilled = Convert.ToBoolean(menage.IsFieldAllFilled),
                    IsContreEnqueteMade = Convert.ToBoolean(menage.IsContreEnqueteMade),
                    CodeAgentRecenceur = menage.CodeAgentRecenceur,
                };
            }
            return new MenageJson();
        }
        public static EmigreJson MapToJson(EmigreModel emigre)
        {
            if (emigre != null)
            {
                return new EmigreJson
                {
                    EmigreId = Convert.ToInt32(emigre.EmigreId),
                    MenageId = Convert.ToInt32(emigre.MenageId),
                    LogeId = Convert.ToInt32(emigre.LogeId),
                    BatimentId = Convert.ToInt32(emigre.BatimentId),
                    SdeId = emigre.SdeId,
                    Qn1numeroOrdre = Convert.ToByte(emigre.Qn1numeroOrdre),
                    Qn2aNomComplet = emigre.Qn2aNomComplet,
                    Qn2bSexe = Convert.ToByte(emigre.Qn2bSexe),
                    Qn2cAgeAuMomentDepart = emigre.Qn2cAgeAuMomentDepart,
                    Qn2dVivantToujours = Convert.ToByte(emigre.Qn2dVivantToujours),
                    Qn2eDernierPaysResidence = Convert.ToByte(emigre.Qn2eDernierPaysResidence),
                    Statut = Convert.ToByte(emigre.Statut),
                    IsFieldAllFilled = Convert.ToBoolean(emigre.IsFieldAllFilled),
                    DateDebutCollecte = emigre.DateDebutCollecte,
                    DateFinCollecte = emigre.DateFinCollecte,
                    DureeSaisie = Convert.ToInt32(emigre.DureeSaisie),
                    CodeAgentRecenceur = emigre.CodeAgentRecenceur,
                };
            }
            return new EmigreJson();
        }
        public static DecesJson MapToJson(DecesModel deces)
        {
            if (deces != null)
            {
                return new DecesJson
                {
                    DecesId = Convert.ToInt32(deces.DecesId),
                    MenageId = Convert.ToInt32(deces.MenageId),
                    LogeId = Convert.ToInt32(deces.LogeId),
                    BatimentId = Convert.ToInt32(deces.BatimentId),
                    SdeId = deces.SdeId,
                    Qd2NoOrdre = Convert.ToByte(deces.Qd2NoOrdre),
                    Qd2aSexe = Convert.ToByte(deces.Qd2aSexe),
                    Qd2bAgeDecede = deces.Qd2bAgeDecede,
                    Qd2c1CirconstanceDeces = Convert.ToByte(deces.Qd2c1CirconstanceDeces),
                    Qd2c2CauseDeces = Convert.ToByte(deces.Qd2c2CauseDeces),
                    Statut = Convert.ToByte(deces.Statut),
                    IsFieldAllFilled = Convert.ToBoolean(deces.IsFieldAllFilled),
                    DateDebutCollecte = deces.DateDebutCollecte,
                    DateFinCollecte = deces.DateFinCollecte,
                    DureeSaisie = Convert.ToInt32(deces.DureeSaisie),
                    IsContreEnqueteMade = Convert.ToBoolean(deces.IsContreEnqueteMade),
                    CodeAgentRecenceur = deces.CodeAgentRecenceur,
                };
            }
            return new DecesJson();
        }
        public static IndividuJson MapToJson(IndividuModel individu)
        {
            if (individu != null)
            {
                return new IndividuJson
                {

                    IndividuId = Convert.ToInt32(individu.IndividuId),
                    MenageId = Convert.ToInt32(individu.MenageId),
                    LogeId = Convert.ToInt32(individu.LogeId),
                    BatimentId = Convert.ToInt32(individu.BatimentId),
                    SdeId = individu.SdeId,
                    Q1NoOrdre = Convert.ToByte(individu.Q1NoOrdre),
                    Qp2APrenom = individu.Qp2APrenom,
                    Qp2BNom = individu.Qp2BNom,
                    Qp3LienDeParente = Convert.ToByte(individu.Qp3LienDeParente),
                    Qp3HabiteDansMenage = Convert.ToByte(individu.Qp3HabiteDansMenage),
                    Qp4Sexe = Convert.ToByte(individu.Qp4Sexe),
                    Qp5DateNaissanceJour = Convert.ToByte(individu.Qp5DateNaissanceJour),
                    Qp5DateNaissanceMois = Convert.ToByte(individu.Qp5DateNaissanceMois),
                    Qp5DateNaissanceAnnee = Convert.ToInt32(individu.Qp5DateNaissanceAnnee),
                    Qp5bAge = Convert.ToByte(individu.Qp5bAge),
                    Qp6religion = Convert.ToByte(individu.Qp6religion),
                    Qp6AutreReligion = individu.Qp6AutreReligion,
                    Qp7Nationalite = Convert.ToByte(individu.Qp7Nationalite),
                    Qp7PaysNationalite = individu.Qp7PaysNationalite,
                    Qp8MereEncoreVivante = Convert.ToByte(individu.Qp8MereEncoreVivante),
                    Qp9EstPlusAge = Convert.ToByte(individu.Qp9EstPlusAge),
                    Qp10LieuNaissance = Convert.ToByte(individu.Qp10LieuNaissance),
                    Qp10CommuneNaissance = individu.Qp10CommuneNaissance,
                    Qp10VqseNaissance = individu.Qp10VqseNaissance,
                    Qp10PaysNaissance = individu.Qp10PaysNaissance,
                    Qp11PeriodeResidence = Convert.ToByte(individu.Qp11PeriodeResidence),
                    Qp12DomicileAvantRecensement = Convert.ToByte(individu.Qp12DomicileAvantRecensement),
                    Qp12CommuneDomicileAvantRecensement = individu.Qp12CommuneDomicileAvantRecensement,
                    Qp12VqseDomicileAvantRecensement = individu.Qp12VqseDomicileAvantRecensement,
                    Qp12PaysDomicileAvantRecensement = individu.Qp12PaysDomicileAvantRecensement,
                    Qe1EstAlphabetise = Convert.ToByte(individu.Qe1EstAlphabetise),
                    Qe2FreqentationScolaireOuUniv = Convert.ToByte(individu.Qe2FreqentationScolaireOuUniv),
                    Qe3typeEcoleOuUniv = Convert.ToByte(individu.Qe3typeEcoleOuUniv),
                    Qe4aNiveauEtude = Convert.ToByte(individu.Qe4aNiveauEtude),
                    Qe4bDerniereClasseOUAneEtude = individu.Qe4bDerniereClasseOUAneEtude,
                    Qe5DiplomeUniversitaire = Convert.ToByte(individu.Qe5DiplomeUniversitaire),
                    Qe6DomaineEtudeUniversitaire = individu.Qe6DomaineEtudeUniversitaire,
                    Qaf1HandicapVoir = Convert.ToByte(individu.Qaf1HandicapVoir),
                    Qaf2HandicapEntendre = Convert.ToByte(individu.Qaf2HandicapEntendre),
                    Qaf3HandicapMarcher = Convert.ToByte(individu.Qaf3HandicapMarcher),
                    Qaf4HandicapSouvenir = Convert.ToByte(individu.Qaf4HandicapSouvenir),
                    Qaf5HandicapPourSeSoigner = Convert.ToByte(individu.Qaf5HandicapPourSeSoigner),
                    Qaf6HandicapCommuniquer = Convert.ToByte(individu.Qaf6HandicapCommuniquer),
                    Qt1PossessionTelCellulaire = Convert.ToByte(individu.Qt1PossessionTelCellulaire),
                    Qt2UtilisationInternet = Convert.ToByte(individu.Qt2UtilisationInternet),
                    Qem1DejaVivreAutrePays = Convert.ToByte(individu.Qem1DejaVivreAutrePays),
                    Qem1AutrePays = individu.Qem1AutrePays,
                    Qem2MoisRetour = Convert.ToByte(individu.Qem2MoisRetour),
                    Qem2AnneeRetour = Convert.ToInt32(individu.Qem2AnneeRetour),
                    Qsm1StatutMatrimonial = Convert.ToByte(individu.Qsm1StatutMatrimonial),
                    Qa1ActEconomiqueDerniereSemaine = Convert.ToByte(individu.Qa1ActEconomiqueDerniereSemaine),
                    Qa2ActAvoirDemele1 = Convert.ToByte(individu.Qa2ActAvoirDemele1),
                    Qa2ActDomestique2 = Convert.ToByte(individu.Qa2ActDomestique2),
                    Qa2ActCultivateur3 = Convert.ToByte(individu.Qa2ActCultivateur3),
                    Qa2ActAiderParent4 = Convert.ToByte(individu.Qa2ActAiderParent4),
                    Qa2ActAutre5 = Convert.ToByte(individu.Qa2ActAutre5),
                    Qa3StatutEmploie = Convert.ToByte(individu.Qa3StatutEmploie),
                    Qa4SecteurInstitutionnel = Convert.ToByte(individu.Qa4SecteurInstitutionnel),
                    Qa5TypeBienProduitParEntreprise = individu.Qa5TypeBienProduitParEntreprise,
                    Qa5PreciserTypeBienProduitParEntreprise = individu.Qa5PreciserTypeBienProduitParEntreprise,
                    Qa6LieuActDerniereSemaine = Convert.ToByte(individu.Qa6LieuActDerniereSemaine),
                    Qa7FoncTravail = Convert.ToByte(individu.Qa7FoncTravail),
                    Qa8EntreprendreDemarcheTravail = Convert.ToByte(individu.Qa8EntreprendreDemarcheTravail),
                    Qa9VouloirTravailler = Convert.ToByte(individu.Qa9VouloirTravailler),
                    Qa10DisponibilitePourTravail = Convert.ToByte(individu.Qa10DisponibilitePourTravail),
                    Qa11RecevoirTransfertArgent = Convert.ToByte(individu.Qa11RecevoirTransfertArgent),
                    Qf1aNbreEnfantNeVivantM = Convert.ToInt32(individu.Qf1aNbreEnfantNeVivantM),
                    Qf1bNbreEnfantNeVivantF = Convert.ToInt32(individu.Qf1bNbreEnfantNeVivantF),
                    Qf2aNbreEnfantVivantM = Convert.ToInt32(individu.Qf2aNbreEnfantVivantM),
                    Qf2bNbreEnfantVivantF = Convert.ToInt32(individu.Qf2bNbreEnfantVivantF),
                    Qf3DernierEnfantJour = Convert.ToByte(individu.Qf3DernierEnfantJour),
                    Qf3DernierEnfantMois = Convert.ToByte(individu.Qf3DernierEnfantMois),
                    Qf3DernierEnfantAnnee = Convert.ToInt32(individu.Qf3DernierEnfantAnnee),
                    Qf4DENeVivantVit = Convert.ToByte(individu.Qf4DENeVivantVit),
                    Statut = Convert.ToByte(individu.Statut),
                    IsFieldAllFilled = Convert.ToBoolean(individu.IsFieldAllFilled),
                    DateDebutCollecte = individu.DateDebutCollecte,
                    DateFinCollecte = individu.DateFinCollecte,
                    DureeSaisie = Convert.ToInt32(individu.DureeSaisie),
                    IsContreEnqueteMade = Convert.ToBoolean(individu.IsContreEnqueteMade),
                    CodeAgentRecenceur = individu.CodeAgentRecenceur
                };
            }
            return new IndividuJson();
        }

        public static List<BatimentJson> MapToListJson(List<BatimentModel> batiments)
        {
            if (batiments != null)
            {
                List<BatimentJson> lists = new List<BatimentJson>();
                foreach (BatimentModel batiment in batiments)
                {
                    BatimentJson batimentJson = ModelMapper.MapToJson(batiment);
                    lists.Add(batimentJson);
                }
                return lists;
            }
            return new List<BatimentJson>();
        }
        public static List<LogementJson> MapToListJson(List<LogementModel> logements)
        {
            if (logements != null)
            {
                List<LogementJson> lists = new List<LogementJson>();
                foreach (LogementModel logement in logements)
                {
                    LogementJson logementJson = ModelMapper.MapToJson(logement);
                    lists.Add(logementJson);
                }
                return lists;
            }
            return new List<LogementJson>();
        }
        public static List<MenageJson> MapToListJson(List<MenageModel> menages)
        {
            if (menages != null)
            {
                List<MenageJson> lists = new List<MenageJson>();
                foreach (MenageModel menage in menages)
                {
                    MenageJson menageJson = ModelMapper.MapToJson(menage);
                    lists.Add(menageJson);
                }
                return lists;
            }
            return new List<MenageJson>();
        }
        public static List<EmigreJson> MapToListJson(List<EmigreModel> emigres)
        {
            if (emigres != null)
            {
                List<EmigreJson> lists = new List<EmigreJson>();
                foreach (EmigreModel emigre in emigres)
                {
                    EmigreJson emigreJson = ModelMapper.MapToJson(emigre);
                    lists.Add(emigreJson);
                }
                return lists;
            }
            return new List<EmigreJson>();
        }
        public static List<DecesJson> MapToListJson(List<DecesModel> decess)
        {
            if (decess != null)
            {
                List<DecesJson> lists = new List<DecesJson>();
                foreach (DecesModel deces in decess)
                {
                    DecesJson decesJson = ModelMapper.MapToJson(deces);
                    lists.Add(decesJson);
                }
                return lists;
            }
            return new List<DecesJson>();
        }
        public static List<IndividuJson> MapToListJson(List<IndividuModel> individus)
        {
            if (individus != null)
            {
                List<IndividuJson> lists = new List<IndividuJson>();
                foreach (IndividuModel individu in individus)
                {
                    IndividuJson individuJson = ModelMapper.MapToJson(individu);
                    lists.Add(individuJson);
                }
                return lists;
            }
            return new List<IndividuJson>();
        }
        #endregion

        #region FOR SQLITE SCHEMA

        public static QuestionModule MapToQuestioModule(tbl_question_module module)
        {
            QuestionModule questionModule = new QuestionModule();
            if (module != null)
            {
                questionModule.codeModule = module.codeModule;
                questionModule.codeQuestion = module.codeQuestion;
                questionModule.estDebut = module.estDebut;
                questionModule.ordre = module.ordre;
            }
            return questionModule;
        }

        public static QuestionsModel MapToQuestionModel(tbl_question q)
        {
            QuestionsModel question = new QuestionsModel();
            if (q != null)
            {
                question.CodeQuestion = q.codeQuestion;
                question.Libelle = q.libelle;
            }
            return question;
        }
        public static List<QuestionsModel> MapToListQuestionModel(List<tbl_question> listOf)
        {
            List<QuestionsModel> questions = new List<QuestionsModel>();
            if (listOf != null)
            {
                foreach (tbl_question q in listOf)
                {
                    QuestionsModel question = new QuestionsModel();
                    question.CodeQuestion = q.codeQuestion;
                    question.Libelle = q.libelle;
                    questions.Add(question);
                }
            }
            return questions;
        }


        public static List<QuestionModule> MapToListQuestionModule(List<tbl_question_module> modules)
        {
            List<QuestionModule> list = new List<QuestionModule>();
            if (modules != null)
            {
                foreach (tbl_question_module md in modules)
                {
                    QuestionModule mod = MapToQuestioModule(md);
                    list.Add(mod);
                }

            }
            return list;
        }

        public LogementCollectifModel MapLogementCModel(LogementModel _logement)
        {
            return new LogementCollectifModel
            {
                BatimentId = _logement.BatimentId.ToString(),
                SdeId = _logement.SdeId,
                LogementCollectif = _logement.LogeId.ToString()
            };
        }
        public LogementIndividuelModel MapLogementIModel(LogementModel _logement)
        {
            return new LogementIndividuelModel
            {
                BatimentId = _logement.BatimentId.ToString(),
                SdeId = _logement.SdeId,
                LogementName = _logement.LogeId.ToString()
            };
        }

        public static BatimentModel MapToBatiment(tbl_batiment batiment)
        {
            if (batiment != null)
            {
                return new BatimentModel
                {
                    BatimentId = Convert.ToInt32(batiment.batimentId),
                    DeptId = batiment.deptId,
                    ComId = batiment.comId,
                    VqseId = batiment.vqseId,
                    SdeId = batiment.sdeId,
                    Zone = Convert.ToByte(batiment.zone),
                    DisctrictId = batiment.disctrictId,
                    Qhabitation = batiment.qhabitation,
                    Qlocalite = batiment.qlocalite,
                    Qadresse = batiment.qadresse,
                    Qrec = batiment.qrec,
                    Qrgph = batiment.qrgph,
                    Qb1Etat = Convert.ToByte(batiment.qb1Etat),
                    Qb2Type = Convert.ToByte(batiment.qb2Type),
                    Qb3NombreEtage = Convert.ToByte(batiment.qb3NombreEtage),
                    Qb4MateriauMur = Convert.ToByte(batiment.qb4MateriauMur),
                    Qb5MateriauToit = Convert.ToByte(batiment.qb5MateriauToit),
                    Qb6StatutOccupation = Convert.ToByte(batiment.qb6StatutOccupation),
                    Qb7Utilisation1 = Convert.ToByte(batiment.qb7Utilisation1),
                    Qb7Utilisation2 = Convert.ToByte(batiment.qb7Utilisation2),
                    Qb8NbreLogeCollectif = Convert.ToByte(batiment.qb8NbreLogeCollectif),
                    Qb8NbreLogeIndividuel = Convert.ToByte(batiment.qb8NbreLogeIndividuel),
                    Statut = Convert.ToByte(batiment.statut),
                    DateEnvoi = batiment.dateEnvoi,
                    IsValidated = Convert.ToBoolean(batiment.isValidated),
                    IsSynchroToAppSup = Convert.ToBoolean(batiment.isSynchroToAppSup),
                    IsSynchroToCentrale = Convert.ToBoolean(batiment.isSynchroToCentrale),
                    DateDebutCollecte = batiment.dateDebutCollecte,
                    DateFinCollecte = batiment.dateFinCollecte,
                    DureeSaisie = Convert.ToInt32(batiment.dureeSaisie),
                    IsFieldAllFilled = Convert.ToBoolean(batiment.isFieldAllFilled),
                    IsContreEnqueteMade = Convert.ToBoolean(batiment.isContreEnqueteMade),
                    Latitude = batiment.latitude,
                    Longitude = batiment.longitude,
                    CodeAgentRecenceur = batiment.codeAgentRecenceur

                };
            }
            return new BatimentModel();
        }


        public static LogementModel MapToLogement(tbl_logement logement)
        {
            if (logement != null)
            {
                return new LogementModel
                {
                    LogeId = Convert.ToInt32(logement.logeId),
                    BatimentId = Convert.ToInt32(logement.batimentId),
                    SdeId = logement.sdeId,
                    QlCategLogement = Convert.ToByte(logement.qlCategLogement),
                    Qlin1NumeroOrdre = Convert.ToByte(logement.qlin1NumeroOrdre),
                    Qlc1TypeLogement = Convert.ToByte(logement.qlc1TypeLogement),
                    QlcTotalIndividus=Convert.ToByte(logement.qlcTotalIndividus),
                    Qlc2bTotalGarcon = Convert.ToByte(logement.qlc2bTotalGarcon),
                    Qlc2bTotalFille = Convert.ToByte(logement.qlc2bTotalFille),
                    Qlin2StatutOccupation = Convert.ToByte(logement.qlin2StatutOccupation),
                    Qlin3ExistenceLogement = Convert.ToByte(logement.qlin3ExistenceLogement),
                    Qlin4TypeLogement = Convert.ToByte(logement.qlin4TypeLogement),
                    Qlin5MateriauSol = Convert.ToByte(logement.qlin5MateriauSol),
                    Qlin6NombrePiece = Convert.ToByte(logement.qlin6NombrePiece),
                    Qlin7NbreChambreACoucher = Convert.ToByte(logement.qlin7NbreChambreACoucher),
                    Qlin8NbreIndividuDepense = Convert.ToByte(logement.qlin8NbreIndividuDepense),
                    Qlin9NbreTotalMenage = Convert.ToByte(logement.qlin9NbreTotalMenage),
                    Statut = Convert.ToByte(logement.statut),
                    IsValidated = Convert.ToBoolean(logement.isValidated),
                    DateDebutCollecte = logement.dateDebutCollecte,
                    DateFinCollecte = logement.dateFinCollecte,
                    DureeSaisie = Convert.ToInt32(logement.dureeSaisie),
                    IsFieldAllFilled = Convert.ToBoolean(logement.isFieldAllFilled),
                    IsContreEnqueteMade = Convert.ToBoolean(logement.isContreEnqueteMade),
                    NbrTentative = Convert.ToByte(logement.nbrTentative),
                    CodeAgentRecenceur = logement.codeAgentRecenceur
                };
            }
            return new LogementModel();
        }

        public static MenageModel MapToMenage(tbl_menage menage)
        {
            if (menage != null)
            {
                return new MenageModel
                {
                    MenageId = Convert.ToInt32(menage.menageId),
                    LogeId = Convert.ToInt32(menage.logeId),
                    BatimentId = Convert.ToInt32(menage.batimentId),
                    SdeId = menage.sdeId,
                    Qm1NoOrdre = Convert.ToByte(menage.qm1NoOrdre),
                    Qm2ModeJouissance = Convert.ToByte(menage.qm2ModeJouissance),
                    Qm3ModeObtentionLoge = Convert.ToByte(menage.qm3ModeObtentionLoge),
                    Qm4_1ModeAprovEauABoire = Convert.ToByte(menage.qm4_1ModeAprovEauABoire),
                    Qm4_2ModeAprovEauAUsageCourant = Convert.ToByte(menage.qm4_2ModeAprovEauAUsageCourant),
                    Qm5SrcEnergieCuisson1 = Convert.ToByte(menage.qm5SrcEnergieCuisson1),
                    Qm5SrcEnergieCuisson2 = Convert.ToByte(menage.qm5SrcEnergieCuisson2),
                    Qm6TypeEclairage = Convert.ToByte(menage.qm6TypeEclairage),
                    Qm7ModeEvacDechet = Convert.ToByte(menage.qm7ModeEvacDechet),
                    Qm8EndroitBesoinPhysiologique = Convert.ToByte(menage.qm8EndroitBesoinPhysiologique),
                    Qm9NbreRadio1 = Convert.ToInt32(menage.qm9NbreRadio1),
                    Qm9NbreTelevision2 = Convert.ToInt32(menage.qm9NbreTelevision2),
                    Qm9NbreRefrigerateur3 = Convert.ToInt32(menage.qm9NbreRefrigerateur3),
                    Qm9NbreFouElectrique4 = Convert.ToInt32(menage.qm9NbreFouElectrique4),
                    Qm9NbreOrdinateur5 = Convert.ToInt32(menage.qm9NbreOrdinateur5),
                    Qm9NbreMotoBicyclette6 = Convert.ToInt32(menage.qm9NbreMotoBicyclette6),
                    Qm9NbreVoitureMachine7 = Convert.ToInt32(menage.qm9NbreVoitureMachine7),
                    Qm9NbreBateau8 = Convert.ToInt32(menage.qm9NbreBateau8),
                    Qm9NbrePanneauGeneratrice9 = Convert.ToInt32(menage.qm9NbrePanneauGeneratrice9),
                    Qm9NbreMilletChevalBourique10 = Convert.ToInt32(menage.qm9NbreMilletChevalBourique10),
                    Qm9NbreBoeufVache11 = Convert.ToInt32(menage.qm9NbreBoeufVache11),
                    Qm9NbreCochonCabrit12 = Convert.ToInt32(menage.qm9NbreCochonCabrit12),
                    Qm9NbreBeteVolaille13 = Convert.ToInt32(menage.qm9NbreBeteVolaille13),
                    Qm10AvoirPersDomestique = Convert.ToByte(menage.qm10AvoirPersDomestique),
                    Qm10TotalDomestiqueFille = Convert.ToByte(menage.qm10TotalDomestiqueFille),
                    Qm10TotalDomestiqueGarcon = Convert.ToByte(menage.qm10TotalDomestiqueGarcon),
                    Qm11TotalIndividuVivant = Convert.ToInt32(menage.qm11TotalIndividuVivant),
                    Qn1Emigration = Convert.ToByte(menage.qn1Emigration),
                    Qn1NbreEmigre = Convert.ToByte(menage.qn1NbreEmigre),
                    Qd1Deces = Convert.ToByte(menage.qd1Deces),
                    Qd1NbreDecede = Convert.ToByte(menage.qd1NbreDecede),
                    Statut = Convert.ToByte(menage.statut),
                    IsValidated = Convert.ToBoolean(menage.isValidated),
                    DateDebutCollecte = menage.dateDebutCollecte,
                    DateFinCollecte = menage.dateFinCollecte,
                    DureeSaisie = Convert.ToInt32(menage.dureeSaisie),
                    IsFieldAllFilled = Convert.ToBoolean(menage.isFieldAllFilled),
                    IsContreEnqueteMade = Convert.ToBoolean(menage.isContreEnqueteMade),
                    CodeAgentRecenceur = menage.codeAgentRecenceur

                };
            }
            return new MenageModel();
        }

        public static DecesModel MapToDeces(tbl_deces deces)
        {
            if (deces != null)
            {
                return new DecesModel
                {
                    DecesId = Convert.ToInt32(deces.decesId),
                    MenageId = Convert.ToInt32(deces.menageId),
                    LogeId = Convert.ToInt32(deces.logeId),
                    BatimentId = Convert.ToInt32(deces.batimentId),
                    SdeId = deces.sdeId,
                    Qd2NoOrdre = Convert.ToByte(deces.qd2NoOrdre),
                    Qd2aSexe = Convert.ToByte(deces.qd2aSexe),
                    Qd2bAgeDecede = deces.qd2bAgeDecede,
                    Qd2c1CirconstanceDeces = Convert.ToByte(deces.qd2c1CirconstanceDeces),
                    Qd2c2CauseDeces = Convert.ToByte(deces.qd2c2CauseDeces),
                    Statut = Convert.ToByte(deces.statut),
                    IsFieldAllFilled = Convert.ToBoolean(deces.isFieldAllFilled),
                    DateDebutCollecte = deces.dateDebutCollecte,
                    DateFinCollecte = deces.dateFinCollecte,
                    DureeSaisie = Convert.ToInt32(deces.dureeSaisie),
                    IsContreEnqueteMade = Convert.ToBoolean(deces.isContreEnqueteMade),
                    CodeAgentRecenceur = deces.codeAgentRecenceur
                };
            }
            return new DecesModel();
        }

        public static EmigreModel MapToEmigre(tbl_emigre emigre)
        {
            if (emigre != null)
            {
                return new EmigreModel
                {
                    EmigreId = Convert.ToInt32(emigre.emigreId),
                    MenageId = Convert.ToInt32(emigre.menageId),
                    LogeId = Convert.ToInt32(emigre.logeId),
                    BatimentId = Convert.ToInt32(emigre.batimentId),
                    SdeId = emigre.sdeId,
                    Qn1numeroOrdre = Convert.ToByte(emigre.qn1numeroOrdre),
                    Qn2aNomComplet = emigre.qn2aNomComplet,
                    Qn2bSexe = Convert.ToByte(emigre.qn2bSexe),
                    Qn2cAgeAuMomentDepart = emigre.qn2cAgeAuMomentDepart,
                    Qn2dVivantToujours = Convert.ToByte(emigre.qn2dVivantToujours),
                    Qn2eDernierPaysResidence = Convert.ToByte(emigre.qn2eDernierPaysResidence),
                    Statut = Convert.ToByte(emigre.statut),
                    IsFieldAllFilled = Convert.ToBoolean(emigre.isFieldAllFilled),
                    DateDebutCollecte = emigre.dateDebutCollecte,
                    DateFinCollecte = emigre.dateFinCollecte,
                    DureeSaisie = Convert.ToInt32(emigre.dureeSaisie),
                    CodeAgentRecenceur = emigre.codeAgentRecenceur
                };
            }
            return new EmigreModel();
        }


        public static IndividuModel MapToIndividu(tbl_individu individu)
        {
            if (individu != null)
            {
                return new IndividuModel
                {
                    IndividuId = Convert.ToInt32(individu.individuId),
                    MenageId = Convert.ToInt32(individu.menageId),
                    LogeId = Convert.ToInt32(individu.logeId),
                    BatimentId = Convert.ToInt32(individu.batimentId),
                    SdeId = individu.sdeId,
                    Q1NoOrdre = Convert.ToByte(individu.q1NoOrdre),
                    Qp2APrenom = individu.qp2APrenom,
                    Qp2BNom = individu.qp2BNom,
                    Qp3LienDeParente = Convert.ToByte(individu.qp3LienDeParente),
                    Qp3HabiteDansMenage = Convert.ToByte(individu.qp3HabiteDansMenage),
                    Qp4Sexe = Convert.ToByte(individu.qp4Sexe),
                    Qp5DateNaissanceJour = Convert.ToByte(individu.qp5DateNaissanceJour),
                    Qp5DateNaissanceMois = Convert.ToByte(individu.qp5DateNaissanceMois),
                    Qp5DateNaissanceAnnee = Convert.ToInt32(individu.Qp5DateNaissanceAnnee),
                    Qp5bAge = Convert.ToByte(individu.qp5bAge),
                    Qp6religion = Convert.ToByte(individu.qp6religion),
                    Qp6AutreReligion = individu.qp6AutreReligion,
                    Qp7Nationalite = Convert.ToByte(individu.qp7Nationalite),
                    Qp7PaysNationalite = individu.qp7PaysNationalite,
                    Qp8MereEncoreVivante = Convert.ToByte(individu.qp8MereEncoreVivante),
                    Qp9EstPlusAge = Convert.ToByte(individu.qp9EstPlusAge),
                    Qp10LieuNaissance = Convert.ToByte(individu.qp10LieuNaissance),
                    Qp10CommuneNaissance = individu.qp10CommuneNaissance,
                    Qp10VqseNaissance = individu.qp10VqseNaissance,
                    Qp10PaysNaissance = individu.qp10PaysNaissance,
                    Qp11PeriodeResidence = Convert.ToByte(individu.qp11PeriodeResidence),
                    Qp12DomicileAvantRecensement = Convert.ToByte(individu.qp12DomicileAvantRecensement),
                    Qp12CommuneDomicileAvantRecensement = individu.qp12CommuneDomicileAvantRecensement,
                    Qp12VqseDomicileAvantRecensement = individu.qp12VqseDomicileAvantRecensement,
                    Qp12PaysDomicileAvantRecensement = individu.qp12PaysDomicileAvantRecensement,
                    Qe1EstAlphabetise = Convert.ToByte(individu.qe1EstAlphabetise),
                    Qe2FreqentationScolaireOuUniv = Convert.ToByte(individu.qe2FreqentationScolaireOuUniv),
                    Qe3typeEcoleOuUniv = Convert.ToByte(individu.qe3typeEcoleOuUniv),
                    Qe4aNiveauEtude = Convert.ToByte(individu.qe4aNiveauEtude),
                    Qe4bDerniereClasseOUAneEtude = individu.qe4bDerniereClasseOUAneEtude,
                    Qe5DiplomeUniversitaire = Convert.ToByte(individu.qe5DiplomeUniversitaire),
                    Qe6DomaineEtudeUniversitaire = individu.qe6DomaineEtudeUniversitaire,
                    Qaf1HandicapVoir = Convert.ToByte(individu.qaf1HandicapVoir),
                    Qaf2HandicapEntendre = Convert.ToByte(individu.qaf2HandicapEntendre),
                    Qaf3HandicapMarcher = Convert.ToByte(individu.qaf3HandicapMarcher),
                    Qaf4HandicapSouvenir = Convert.ToByte(individu.qaf4HandicapSouvenir),
                    Qaf5HandicapPourSeSoigner = Convert.ToByte(individu.qaf5HandicapPourSeSoigner),
                    Qaf6HandicapCommuniquer = Convert.ToByte(individu.qaf6HandicapCommuniquer),
                    Qt1PossessionTelCellulaire = Convert.ToByte(individu.qt1PossessionTelCellulaire),
                    Qt2UtilisationInternet = Convert.ToByte(individu.qt2UtilisationInternet),
                    Qem1DejaVivreAutrePays = Convert.ToByte(individu.qem1DejaVivreAutrePays),
                    Qem1AutrePays = individu.qem1AutrePays,
                    Qem2MoisRetour = Convert.ToByte(individu.qem2MoisRetour),
                    Qem2AnneeRetour = Convert.ToInt32(individu.qem2AnneeRetour),
                    Qsm1StatutMatrimonial = Convert.ToByte(individu.qsm1StatutMatrimonial),
                    Qa1ActEconomiqueDerniereSemaine = Convert.ToByte(individu.qa1ActEconomiqueDerniereSemaine),
                    Qa2ActAvoirDemele1 = Convert.ToByte(individu.qa2ActAvoirDemele1),
                    Qa2ActDomestique2 = Convert.ToByte(individu.qa2ActDomestique2),
                    Qa2ActCultivateur3 = Convert.ToByte(individu.qa2ActCultivateur3),
                    Qa2ActAiderParent4 = Convert.ToByte(individu.qa2ActAiderParent4),
                    Qa2ActAutre5 = Convert.ToByte(individu.qa2ActAutre5),
                    Qa3StatutEmploie = Convert.ToByte(individu.qa3StatutEmploie),
                    Qa4SecteurInstitutionnel = Convert.ToByte(individu.qa4SecteurInstitutionnel),
                    Qa5TypeBienProduitParEntreprise = individu.qa5TypeBienProduitParEntreprise,
                    Qa5PreciserTypeBienProduitParEntreprise = individu.qa5PreciserTypeBienProduitParEntreprise,
                    Qa6LieuActDerniereSemaine = Convert.ToByte(individu.qa6LieuActDerniereSemaine),
                    Qa7FoncTravail = Convert.ToByte(individu.qa7FoncTravail),
                    Qa8EntreprendreDemarcheTravail = Convert.ToByte(individu.qa8EntreprendreDemarcheTravail),
                    Qa9VouloirTravailler = Convert.ToByte(individu.qa9VouloirTravailler),
                    Qa10DisponibilitePourTravail = Convert.ToByte(individu.qa10DisponibilitePourTravail),
                    Qa11RecevoirTransfertArgent = Convert.ToByte(individu.qa11RecevoirTransfertArgent),
                    Qf1aNbreEnfantNeVivantM = Convert.ToInt32(individu.qf1aNbreEnfantNeVivantM),
                    Qf1bNbreEnfantNeVivantF = Convert.ToInt32(individu.qf1bNbreEnfantNeVivantF),
                    Qf2aNbreEnfantVivantM = Convert.ToInt32(individu.qf2aNbreEnfantVivantM),
                    Qf2bNbreEnfantVivantF = Convert.ToInt32(individu.qf2bNbreEnfantVivantF),
                    Qf3DernierEnfantJour = Convert.ToByte(individu.qf3DernierEnfantJour),
                    Qf3DernierEnfantMois = Convert.ToByte(individu.qf3DernierEnfantMois),
                    Qf3DernierEnfantAnnee = Convert.ToInt32(individu.qf3DernierEnfantAnnee),
                    Qf4DENeVivantVit = Convert.ToByte(individu.qf4DENeVivantVit),
                    Statut = Convert.ToByte(individu.statut),
                    IsFieldAllFilled = Convert.ToBoolean(individu.isFieldAllFilled),
                    DateDebutCollecte = individu.dateDebutCollecte,
                    DateFinCollecte = individu.dateFinCollecte,
                    DureeSaisie = Convert.ToInt32(individu.dureeSaisie),
                    IsContreEnqueteMade = Convert.ToBoolean(individu.isContreEnqueteMade),
                    CodeAgentRecenceur = individu.codeAgentRecenceur
                };
            }
            return new IndividuModel();
        }
        public static List<BatimentModel> MapToListBatimentModel(List<tbl_batiment> listOfBatiment)
        {
            List<BatimentModel> listOfBatimentModel = new List<BatimentModel>();
            try
            {
                if (listOfBatiment != null)
                {
                    foreach (tbl_batiment bat in listOfBatiment)
                    {
                        BatimentModel model = new BatimentModel();
                        model = ModelMapper.MapToBatiment(bat);
                        listOfBatimentModel.Add(model);
                    }
                }

            }
            catch (Exception)
            {

            }

            return listOfBatimentModel;

        }
        public static List<LogementModel> MapToListLogementModel(List<tbl_logement> logements)
        {
            List<LogementModel> listOfLogement = new List<LogementModel>();
            try
            {
                if (logements != null)
                {
                    foreach (tbl_logement lg in logements)
                    {
                        LogementModel logement = new LogementModel();
                        logement = MapToLogement(lg);
                        listOfLogement.Add(logement);
                    }
                }
            }
            catch (Exception)
            {

            }
            return listOfLogement;
        }
        public static List<MenageModel> MapToListMenageModel(List<tbl_menage> menages)
        {
            List<MenageModel> listToMenage = new List<MenageModel>();
            try
            {
                if (menages != null)
                {
                    foreach (tbl_menage menage in menages)
                    {
                        MenageModel men = MapToMenage(menage);
                        listToMenage.Add(men);
                    }
                }
            }
            catch (Exception)
            {

            }
            return listToMenage;
        }
        public static List<DecesModel> MapToListDeces(List<tbl_deces> deces)
        {
            List<DecesModel> listOFDeces = new List<DecesModel>();
            try
            {
                if (deces != null)
                {
                    foreach (tbl_deces dec in deces)
                    {
                        DecesModel d = MapToDeces(dec);
                        listOFDeces.Add(d);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listOFDeces;
        }
        public static List<IndividuModel> MapToListIndividu(List<tbl_individu> individus)
        {
            List<IndividuModel> listOfIndividus = new List<IndividuModel>();
            try
            {
                if (individus != null)
                {
                    foreach (tbl_individu individu in individus)
                    {
                        IndividuModel ind = MapToIndividu(individu);
                        listOfIndividus.Add(ind);
                    }
                }
            }
            catch (Exception)
            {

            }
            return listOfIndividus;
        }
        public static List<EmigreModel> MapToListEmigre(List<tbl_emigre> emigres)
        {
            List<EmigreModel> listOfEmigres = new List<EmigreModel>();
            try
            {
                if (emigres != null)
                {
                    foreach (tbl_emigre emigre in emigres)
                    {
                        EmigreModel em = MapToEmigre(emigre);
                        listOfEmigres.Add(em);
                    }
                }
            }
            catch (Exception)
            {

            }
            return listOfEmigres;
        }
        public static MenageDetailsModel MapToMenageDetails<T>(T type)
        {
            MenageDetailsModel model = new MenageDetailsModel();
            if (type.ToString() == Constant.OBJET_MODEL_DECES)
            {
                DecesModel dec = type as DecesModel;
                model.Name = Constant.STR_TYPE_DECES + "-" + dec.Qd2NoOrdre.ToString();
                model.Id = dec.DecesId.ToString();
                model.Type = 1;
                model.LogementId = dec.LogeId;
                model.BatimentId = dec.BatimentId;
                model.MenageId = dec.MenageId;
                model.Statut = dec.Statut;
            }
            if (type.ToString() == Constant.OBJET_MODEL_INDIVIDU)
            {
                IndividuModel ind = type as IndividuModel;
                model.Name = Constant.STR_TYPE_ENVDIVIDI + "-" + ind.Q1NoOrdre.ToString();
                model.Id = ind.IndividuId.ToString();
                model.Type = 3;
                model.LogementId = ind.LogeId;
                model.BatimentId = ind.BatimentId;
                model.MenageId = ind.MenageId;
                model.Statut = ind.Statut;
            }
            if (type.ToString() == Constant.OBJET_MODEL_EMIGRE)
            {
                EmigreModel em = type as EmigreModel;
                model.Name = Constant.STR_TYPE_EMIGRE + "-" + em.Qn1numeroOrdre.ToString();
                model.Id = em.EmigreId.ToString();
                model.Type = 2;
                model.LogementId = em.LogeId;
                model.BatimentId = em.BatimentId;
                model.MenageId = em.MenageId;
                model.Statut = em.Statut;
            }
            return model;
        }

        public static tbl_batiment MapTo_tbl_batiment(BatimentModel batiment)
        {
            return new tbl_batiment
            {
                batimentId = Convert.ToInt32(batiment.BatimentId),
                sdeId = batiment.SdeId,
                isContreEnqueteMade = Convert.ToInt32(batiment.IsContreEnqueteMade),
                isValidated = Convert.ToInt32(batiment.IsValidated)

            };
        }
        public static tbl_logement MapTo_tbl_logement(LogementModel logment)
        {
            return new tbl_logement
            {
                logeId = logment.LogeId,
                isContreEnqueteMade = Convert.ToInt32(logment.IsContreEnqueteMade),
                isValidated = Convert.ToInt32(logment.IsValidated)
            };
        }
        public static tbl_menage MapTo_tbl_menage(MenageModel menage)
        {
            return new tbl_menage
            {
                menageId = menage.MenageId,
                isContreEnqueteMade = Convert.ToInt32(menage.IsContreEnqueteMade),
                isValidated = Convert.ToInt32(menage.IsValidated)
            };
        }

        public static tbl_deces MapTo_tbl_deces(DecesModel deces)
        {
            return new tbl_deces
            {
                decesId = deces.DecesId,
                isContreEnqueteMade = Convert.ToInt32(deces.IsContreEnqueteMade)

            };
        }
        public static tbl_individu MapTo_tbl_individu(IndividuModel ind)
        {
            return new tbl_individu
            {
                individuId = ind.IndividuId,
                isContreEnqueteMade = Convert.ToInt32(ind.IsContreEnqueteMade)
            };
        }
        #endregion

        #region Contre Enquete
        public static ContreEnqueteModel MapToContreEnqueteModel(Tbl_ContreEnquete ce)
        {
            return new ContreEnqueteModel
            {
                ContreEnqueteId = ce.ContreEnqueteId,
                BatimentId = ce.BatimentId,
                SdeId = ce.SdeId,
                CodeDistrict = ce.CodeDistrict,
                NomSuperviseur = ce.NomSuperviseur,
                PrenomSuperviseur = ce.PrenomSuperviseur,
                Raison = Convert.ToByte(ce.Raison.GetValueOrDefault()),
                Statut = Convert.ToByte(ce.Statut.GetValueOrDefault()),
                DateDebut = ce.DateDebut,
                DateFin = ce.DateFin,
                ContreEnqueteName = "Kont-ankèt-" + ce.ContreEnqueteId
            };
        }
        public static Tbl_ContreEnquete MapToTblContreEnquete(ContreEnqueteModel model)
        {
            Tbl_ContreEnquete ce = new Tbl_ContreEnquete();
            ce.BatimentId = model.BatimentId.GetValueOrDefault();
            ce.SdeId = model.SdeId;
            ce.CodeDistrict = model.CodeDistrict;
            ce.NomSuperviseur = model.NomSuperviseur;
            ce.PrenomSuperviseur = model.PrenomSuperviseur;
            ce.Raison = model.Raison.GetValueOrDefault();
            ce.Statut = model.Statut.GetValueOrDefault();
            ce.DateDebut = model.DateDebut;
            ce.DateFin = model.DateFin;
            ce.TypeContreEnquete = model.TypeContreEnquete.GetValueOrDefault();
            return ce;

        }
        public static List<ContreEnqueteModel> MapToListContreEnqueteModel(List<Tbl_ContreEnquete> ce)
        {
            List<ContreEnqueteModel> model = new List<ContreEnqueteModel>();
            foreach (Tbl_ContreEnquete e in ce)
            {
                ContreEnqueteModel c = ModelMapper.MapToContreEnqueteModel(e);
                model.Add(c);
            }
            return model;
        }
        public static BatimentCEModel MapToBatimentCEModel(Tbl_BatimentCE bat)
        {
            BatimentCEModel batModel = new BatimentCEModel();
            if (bat == null)
            {
                return new BatimentCEModel
                {
                    BatimentId = 0
                };
            }
            else
            {
                batModel.SdeId = bat.SdeId;
                batModel.Qrec = bat.Qrec;
                batModel.Qrgph = bat.Qrgph;
                batModel.Qlocalite = bat.Qlocalite;
                batModel.Qhabitation = bat.Qhabitation;
                batModel.Qb1Etat = Convert.ToByte(bat.Qb1Etat.GetValueOrDefault());
                batModel.Qb2Type = Convert.ToByte(bat.Qb2Type.GetValueOrDefault());
                batModel.DureeSaisie = Convert.ToByte(bat.DureeSaisie.GetValueOrDefault());
                batModel.DateDebutCollecte = bat.DateDebutCollecte;
                batModel.DateFinCollecte = bat.DateFinCollecte;
                batModel.Qb4MateriauMur = Convert.ToByte(bat.Qb4MateriauMur.GetValueOrDefault());
                batModel.Qb3NombreEtage = Convert.ToByte(bat.Qb3NombreEtage.GetValueOrDefault());
                batModel.Qb5MateriauToit = Convert.ToByte(bat.Qb5MateriauToit.GetValueOrDefault());
                batModel.Qb6StatutOccupation = Convert.ToByte(bat.Qb6StatutOccupation.GetValueOrDefault());
                batModel.Qb7Utilisation1 = Convert.ToByte(bat.Qb7Utilisation1.GetValueOrDefault());
                batModel.Qb7Utilisation2 = Convert.ToByte(bat.Qb7Utilisation2.GetValueOrDefault());
                batModel.Qb8NbreLogeCollectif = Convert.ToByte(bat.Qb8NbreLogeCollectif.GetValueOrDefault());
                batModel.Qb8NbreLogeIndividuel = Convert.ToByte(bat.Qb8NbreLogeIndividuel.GetValueOrDefault());
                batModel.IsContreEnqueteMade = Convert.ToBoolean(bat.IsContreEnqueteMade.GetValueOrDefault());
                batModel.IsValidated = Convert.ToBoolean(bat.IsValidated.GetValueOrDefault());
                batModel.Statut = Convert.ToByte(bat.Statut.GetValueOrDefault());
                batModel.BatimentId = bat.BatimentId;
                return batModel;
            }

        }
        public static Tbl_BatimentCE MapToTbl_BatimentCE(BatimentCEModel bat)
        {
            Tbl_BatimentCE batToSave = new Tbl_BatimentCE();
            batToSave.BatimentId = bat.BatimentId;
            batToSave.SdeId = bat.SdeId;
            batToSave.Qrec = bat.Qrec;
            batToSave.Qrgph = bat.Qrgph;
            batToSave.Qlocalite = bat.Qlocalite;
            batToSave.Qhabitation = bat.Qhabitation;
            batToSave.Qb1Etat = bat.Qb1Etat;
            batToSave.Qb2Type = bat.Qb2Type;
            batToSave.Qb3NombreEtage = bat.Qb3NombreEtage.GetValueOrDefault();
            batToSave.DureeSaisie = bat.DureeSaisie;
            batToSave.DateDebutCollecte = bat.DateDebutCollecte;
            batToSave.DateFinCollecte = bat.DateFinCollecte;
            batToSave.Qb4MateriauMur = bat.Qb4MateriauMur;
            batToSave.Qb5MateriauToit = bat.Qb5MateriauToit;
            batToSave.Qb6StatutOccupation = bat.Qb6StatutOccupation.GetValueOrDefault();
            batToSave.Qb7Utilisation1 = bat.Qb7Utilisation1;
            batToSave.Qb7Utilisation2 = bat.Qb7Utilisation2;
            batToSave.Qb8NbreLogeCollectif = bat.Qb8NbreLogeCollectif;
            batToSave.Qb8NbreLogeIndividuel = bat.Qb8NbreLogeIndividuel;
            batToSave.IsContreEnqueteMade = Convert.ToInt32(bat.IsContreEnqueteMade.GetValueOrDefault());
            batToSave.IsValidated = Convert.ToInt32(bat.IsValidated.GetValueOrDefault());
            batToSave.Statut = bat.Statut;
            return batToSave;
        }

        public static LogementCEModel MapToLogementCEModel(Tbl_LogementCE log)
        {
            LogementCEModel logement = new LogementCEModel();
            logement.BatimentId = log.BatimentId.GetValueOrDefault();
            logement.LogeId = log.LogeId;
            logement.SdeId = log.SdeId;
            logement.Qlin6NombrePiece = Convert.ToByte(log.Qlin6NombrePiece.GetValueOrDefault());
            logement.QlcTypeLogement = Convert.ToByte(log.QlcTypeLogement.GetValueOrDefault());
            logement.QlCategLogement = Convert.ToByte(log.QlCategLogement.GetValueOrDefault());
            logement.Qlc2bTotalFille = Convert.ToByte(log.Qlc2bTotalFille.GetValueOrDefault());
            logement.Qllc2bTotalGarcon = Convert.ToByte(log.Qllc2bTotalGarcon.GetValueOrDefault());
            logement.Qlin2StatutOccupation = Convert.ToByte(log.Qlin2StatutOccupation.GetValueOrDefault());
            logement.Qlin9NbreTotalMenage = Convert.ToByte(log.Qlin9NbreTotalMenage.GetValueOrDefault());
            logement.Qlin8NbreIndividuDepense = Convert.ToByte(log.Qlin8NbreIndividuDepense.GetValueOrDefault());
            logement.Qlin5MateriauSol = Convert.ToByte(log.Qlin5MateriauSol.GetValueOrDefault());
            logement.Qlin7NbreChambreACoucher = Convert.ToByte(log.Qlin7NbreChambreACoucher.GetValueOrDefault());
            logement.Qlin1NumeroOrdre = Convert.ToByte(log.Qlin1NumeroOrdre.GetValueOrDefault());
            logement.DureeSaisie = Convert.ToByte(log.DureeSaisie.GetValueOrDefault());
            logement.IsContreEnqueteMade = log.IsContreEnqueteMade.GetValueOrDefault();
            logement.IsValidated = log.IsValidated.GetValueOrDefault();
            return logement;
        }
        public static Tbl_LogementCE MapToTbl_LogementCE(LogementCEModel log)
        {
            Tbl_LogementCE logement = new Tbl_LogementCE();
            logement.BatimentId = log.BatimentId;
            logement.LogeId = log.LogeId;
            logement.SdeId = log.SdeId;
            logement.Qlin6NombrePiece = Convert.ToByte(log.Qlin6NombrePiece.GetValueOrDefault());
            logement.QlcTypeLogement = Convert.ToByte(log.QlcTypeLogement.GetValueOrDefault());
            logement.QlCategLogement = Convert.ToByte(log.QlCategLogement.GetValueOrDefault());
            logement.Qlc2bTotalFille = Convert.ToByte(log.Qlc2bTotalFille.GetValueOrDefault());
            logement.Qllc2bTotalGarcon = Convert.ToByte(log.Qllc2bTotalGarcon.GetValueOrDefault());
            logement.Qlin2StatutOccupation = Convert.ToByte(log.Qlin2StatutOccupation.GetValueOrDefault());
            logement.Qlin9NbreTotalMenage = Convert.ToByte(log.Qlin9NbreTotalMenage.GetValueOrDefault());
            logement.Qlin8NbreIndividuDepense = Convert.ToByte(log.Qlin8NbreIndividuDepense.GetValueOrDefault());
            logement.Qlin5MateriauSol = Convert.ToByte(log.Qlin5MateriauSol.GetValueOrDefault());
            logement.Qlin7NbreChambreACoucher = Convert.ToByte(log.Qlin7NbreChambreACoucher.GetValueOrDefault());
            logement.Qlin1NumeroOrdre = Convert.ToByte(log.Qlin1NumeroOrdre.GetValueOrDefault());
            logement.DureeSaisie = Convert.ToByte(log.DureeSaisie.GetValueOrDefault());
            logement.IsContreEnqueteMade = log.IsContreEnqueteMade.GetValueOrDefault();
            logement.IsValidated = log.IsValidated.GetValueOrDefault();
            return logement;
        }
        public static List<LogementCEModel> MapToListLogementCEModel(List<Tbl_LogementCE> listLog)
        {
            List<LogementCEModel> list = new List<LogementCEModel>();
            foreach (Tbl_LogementCE log in listLog)
            {
                LogementCEModel l = new LogementCEModel();
                l = ModelMapper.MapToLogementCEModel(log);
                list.Add(l);
            }
            return list;

        }

        public static MenageCEModel MapToMenageCEModel(Tbl_MenageCE _men)
        {
            MenageCEModel menage = new MenageCEModel();
            menage.MenageId = _men.MenageId;
            menage.LogeId = _men.LogeId.GetValueOrDefault();
            menage.BatimentId = _men.BatimentId.GetValueOrDefault();
            menage.SdeId = _men.SdeId;
            menage.Qm1NoOrdre = Convert.ToByte(_men.Qm1NoOrdre.GetValueOrDefault());
            menage.Qm2ModeJouissance = Convert.ToByte(_men.Qm2ModeJouissance.GetValueOrDefault());
            menage.Qm5SrcEnergieCuisson1 = Convert.ToByte(_men.Qm5SrcEnergieCuisson1.GetValueOrDefault());
            menage.Qm5SrcEnergieCuisson2 = Convert.ToByte(_men.Qm5SrcEnergieCuisson2.GetValueOrDefault());
            menage.Qm8EndroitBesoinPhysiologique = Convert.ToByte(_men.Qm8EndroitBesoinPhysiologique.GetValueOrDefault());
            menage.Qm11TotalIndividuVivant = Convert.ToByte(_men.Qm11TotalIndividuVivant.GetValueOrDefault());
            menage.DureeSaisie = Convert.ToInt32(_men.DureeSaisie.GetValueOrDefault());
            menage.IsContreEnqueteMade = Convert.ToBoolean(_men.IsContreEnqueteMade.GetValueOrDefault());
            menage.IsValidated = Convert.ToBoolean(_men.IsValidated.GetValueOrDefault());
            return menage;
        }
        public static Tbl_MenageCE MapToTbl_MenageCE(MenageCEModel _men)
        {
            Tbl_MenageCE menage = new Tbl_MenageCE();
            menage.MenageId = _men.MenageId;
            menage.LogeId = _men.LogeId;
            menage.BatimentId = _men.BatimentId;
            menage.SdeId = _men.SdeId;
            menage.Qm1NoOrdre = Convert.ToByte(_men.Qm1NoOrdre.GetValueOrDefault());
            menage.Qm2ModeJouissance = Convert.ToByte(_men.Qm2ModeJouissance.GetValueOrDefault());
            menage.Qm5SrcEnergieCuisson1 = Convert.ToByte(_men.Qm5SrcEnergieCuisson1.GetValueOrDefault());
            menage.Qm5SrcEnergieCuisson2 = Convert.ToByte(_men.Qm5SrcEnergieCuisson2.GetValueOrDefault());
            menage.Qm8EndroitBesoinPhysiologique = Convert.ToByte(_men.Qm8EndroitBesoinPhysiologique.GetValueOrDefault());
            menage.Qm11TotalIndividuVivant = Convert.ToByte(_men.Qm11TotalIndividuVivant.GetValueOrDefault());
            menage.DureeSaisie = Convert.ToInt32(_men.DureeSaisie.GetValueOrDefault());
            menage.IsContreEnqueteMade = Convert.ToInt32(_men.IsContreEnqueteMade.GetValueOrDefault());
            menage.IsValidated = Convert.ToInt32(_men.IsValidated.GetValueOrDefault());
            return menage;
        }
        public static List<MenageCEModel> MapToListMenageCEModel(List<Tbl_MenageCE> listOfMenages)
        {
            List<MenageCEModel> _menages = new List<MenageCEModel>();
            foreach (Tbl_MenageCE _m in listOfMenages)
            {
                MenageCEModel _men = new MenageCEModel();
                _men = MapToMenageCEModel(_m);
                _menages.Add(_men);
            }
            return _menages;
        }

        public static IndividuCEModel MapToIndividuCEModel(Tbl_IndividusCE _ind)
        {
            IndividuCEModel individu = new IndividuCEModel();
            individu.BatimentId = _ind.BatimentId.GetValueOrDefault();
            individu.LogeId = _ind.LogeId.GetValueOrDefault();
            individu.MenageId = _ind.MenageId.GetValueOrDefault();
            individu.IndividuId = _ind.IndividuId;
            individu.SdeId = _ind.SdeId;
            individu.Q6LienDeParente = Convert.ToByte(_ind.Q6LienDeParente.GetValueOrDefault());
            individu.Q5bAge = Convert.ToByte(_ind.Q5bAge.GetValueOrDefault());
            individu.Q2Nom = _ind.Q2Nom;
            individu.Q3Prenom = _ind.Q3Prenom;
            individu.Q4Sexe = Convert.ToByte(_ind.Q4Sexe.GetValueOrDefault());
            individu.Qp7Nationalite = Convert.ToByte(_ind.Qp7Nationalite.GetValueOrDefault());
            individu.Qp7PaysNationalite = _ind.Qp7PaysNationalite;
            individu.Q7DateNaissanceJour = Convert.ToByte(_ind.Q7DateNaissanceJour.GetValueOrDefault());
            individu.Q7DateNaissanceMois = Convert.ToByte(_ind.Q7DateNaissanceMois.GetValueOrDefault());
            individu.Q7DateNaissanceAnnee = Convert.ToInt32(_ind.Q7DateNaissanceAnnee.GetValueOrDefault());
            individu.Qp10LieuNaissance = Convert.ToByte(_ind.Qp10LieuNaissance.GetValueOrDefault());
            individu.Qp10CommuneNaissance = _ind.Qp10CommuneNaissance;
            individu.Qp10LieuNaissanceVqse = _ind.Qp10LieuNaissanceVqse;
            individu.Qp10PaysNaissance = _ind.Qp10PaysNaissance;
            individu.Qp11PeriodeResidence = Convert.ToByte(_ind.Qp11PeriodeResidence.GetValueOrDefault());
            individu.Qe2FreqentationScolaireOuUniv = Convert.ToByte(_ind.Qe2FreqentationScolaireOuUniv.GetValueOrDefault());
            individu.Qe4aNiveauEtude = Convert.ToByte(_ind.Qe4aNiveauEtude.GetValueOrDefault());
            individu.Qe4bDerniereClasseOUAneEtude = Convert.ToByte(_ind.Qe4bDerniereClasseOUAneEtude.GetValueOrDefault());
            individu.Qsm1StatutMatrimonial = Convert.ToByte(_ind.Qsm1StatutMatrimonial.GetValueOrDefault());
            individu.Qa1ActEconomiqueDerniereSemaine = Convert.ToByte(_ind.Qa1ActEconomiqueDerniereSemaine.GetValueOrDefault());
            individu.Qa2ActAvoirDemele1 = Convert.ToByte(_ind.Qa2ActAvoirDemele1.GetValueOrDefault());
            individu.Qa2ActDomestique2 = Convert.ToByte(_ind.Qa2ActDomestique2.GetValueOrDefault());
            individu.Qa2ActCultivateur3 = Convert.ToByte(_ind.Qa2ActCultivateur3.GetValueOrDefault());
            individu.Qa2ActAiderParent4 = Convert.ToByte(_ind.Qa2ActAiderParent4.GetValueOrDefault());
            individu.Qa2ActAutre5 = Convert.ToByte(_ind.Qa2ActAutre5.GetValueOrDefault());
            individu.Qa8EntreprendreDemarcheTravail = Convert.ToByte(_ind.Qa8EntreprendreDemarcheTravail.GetValueOrDefault());
            individu.Qf1aNbreEnfantNeVivantM = Convert.ToByte(_ind.Qf1aNbreEnfantNeVivantM.GetValueOrDefault());
            individu.Qf2bNbreEnfantNeVivantF = Convert.ToByte(_ind.Qf2bNbreEnfantNeVivantF.GetValueOrDefault());
            individu.Qf2aNbreEnfantVivantM = Convert.ToByte(_ind.Qf2aNbreEnfantVivantM.GetValueOrDefault());
            individu.Qf2bNbreEnfantVivantF = Convert.ToByte(_ind.Qf2bNbreEnfantVivantF.GetValueOrDefault());
            individu.Qf3DernierEnfantJour = Convert.ToByte(_ind.Qf3DernierEnfantJour.GetValueOrDefault());
            individu.Qf3DernierEnfantMois = Convert.ToByte(_ind.Qf3DernierEnfantMois.GetValueOrDefault());
            individu.Qf3DernierEnfantAnnee = Convert.ToInt32(_ind.Qf3DernierEnfantAnnee.GetValueOrDefault());
            individu.Qf4DENeVivantVit = Convert.ToByte(_ind.Qf4DENeVivantVit.GetValueOrDefault());
            individu.DureeSaisie = Convert.ToInt32(_ind.DureeSaisie.GetValueOrDefault());
            individu.IsContreEnqueteMade = _ind.IsContreEnqueteMade.GetValueOrDefault();
            individu.IsValidated = _ind.IsValidated.GetValueOrDefault();
            return individu;
        }

        public static Tbl_IndividusCE MapToTbl_IndividuCE(IndividuCEModel _ind)
        {
            Tbl_IndividusCE individu = new Tbl_IndividusCE();
            individu.BatimentId = _ind.BatimentId;
            individu.LogeId = _ind.LogeId;
            individu.MenageId = _ind.MenageId;
            individu.SdeId = _ind.SdeId;
            individu.IndividuId = _ind.IndividuId;
            individu.Q6LienDeParente = Convert.ToByte(_ind.Q6LienDeParente.GetValueOrDefault());
            individu.Q5bAge = Convert.ToByte(_ind.Q5bAge.GetValueOrDefault());
            individu.Q2Nom = _ind.Q2Nom;
            individu.Q3Prenom = _ind.Q3Prenom;
            individu.Q4Sexe = Convert.ToByte(_ind.Q4Sexe.GetValueOrDefault());
            individu.Qp7Nationalite = Convert.ToByte(_ind.Qp7Nationalite.GetValueOrDefault());
            individu.Qp7PaysNationalite = _ind.Qp7PaysNationalite;
            individu.Q7DateNaissanceJour = Convert.ToByte(_ind.Q7DateNaissanceJour.GetValueOrDefault());
            individu.Q7DateNaissanceMois = Convert.ToByte(_ind.Q7DateNaissanceMois.GetValueOrDefault());
            individu.Q7DateNaissanceAnnee = Convert.ToInt32(_ind.Q7DateNaissanceAnnee.GetValueOrDefault());
            individu.Qp10LieuNaissance = Convert.ToByte(_ind.Qp10LieuNaissance.GetValueOrDefault());
            individu.Qp10CommuneNaissance = _ind.Qp10CommuneNaissance;
            individu.Qp10LieuNaissanceVqse = _ind.Qp10LieuNaissanceVqse;
            individu.Qp10PaysNaissance = _ind.Qp10PaysNaissance;
            individu.Qp11PeriodeResidence = Convert.ToByte(_ind.Qp11PeriodeResidence.GetValueOrDefault());
            individu.Qe2FreqentationScolaireOuUniv = Convert.ToByte(_ind.Qe2FreqentationScolaireOuUniv.GetValueOrDefault());
            individu.Qe4aNiveauEtude = Convert.ToByte(_ind.Qe4aNiveauEtude.GetValueOrDefault());
            individu.Qe4bDerniereClasseOUAneEtude = Convert.ToByte(_ind.Qe4bDerniereClasseOUAneEtude.GetValueOrDefault());
            individu.Qsm1StatutMatrimonial = Convert.ToByte(_ind.Qsm1StatutMatrimonial.GetValueOrDefault());
            individu.Qa1ActEconomiqueDerniereSemaine = Convert.ToByte(_ind.Qa1ActEconomiqueDerniereSemaine.GetValueOrDefault());
            individu.Qa2ActAvoirDemele1 = Convert.ToByte(_ind.Qa2ActAvoirDemele1.GetValueOrDefault());
            individu.Qa2ActDomestique2 = Convert.ToByte(_ind.Qa2ActDomestique2.GetValueOrDefault());
            individu.Qa2ActCultivateur3 = Convert.ToByte(_ind.Qa2ActCultivateur3.GetValueOrDefault());
            individu.Qa2ActAiderParent4 = Convert.ToByte(_ind.Qa2ActAiderParent4.GetValueOrDefault());
            individu.Qa2ActAutre5 = Convert.ToByte(_ind.Qa2ActAutre5.GetValueOrDefault());
            individu.Qa8EntreprendreDemarcheTravail = Convert.ToByte(_ind.Qa8EntreprendreDemarcheTravail.GetValueOrDefault());
            individu.Qf1aNbreEnfantNeVivantM = Convert.ToByte(_ind.Qf1aNbreEnfantNeVivantM.GetValueOrDefault());
            individu.Qf2bNbreEnfantNeVivantF = Convert.ToByte(_ind.Qf2bNbreEnfantNeVivantF.GetValueOrDefault());
            individu.Qf2aNbreEnfantVivantM = Convert.ToByte(_ind.Qf2aNbreEnfantVivantM.GetValueOrDefault());
            individu.Qf2bNbreEnfantVivantF = Convert.ToByte(_ind.Qf2bNbreEnfantVivantF.GetValueOrDefault());
            individu.Qf3DernierEnfantJour = Convert.ToByte(_ind.Qf3DernierEnfantJour.GetValueOrDefault());
            individu.Qf3DernierEnfantMois = Convert.ToByte(_ind.Qf3DernierEnfantMois.GetValueOrDefault());
            individu.Qf3DernierEnfantAnnee = Convert.ToInt32(_ind.Qf3DernierEnfantAnnee.GetValueOrDefault());
            individu.Qf4DENeVivantVit = Convert.ToByte(_ind.Qf4DENeVivantVit.GetValueOrDefault());
            individu.DureeSaisie = Convert.ToInt32(_ind.DureeSaisie.GetValueOrDefault());
            individu.IsContreEnqueteMade = _ind.IsContreEnqueteMade.GetValueOrDefault();
            individu.IsValidated = _ind.IsValidated.GetValueOrDefault();
            return individu;
        }

        public static List<IndividuCEModel> MapToListIndividuCEModel(List<Tbl_IndividusCE> listOfInd)
        {
            List<IndividuCEModel> list = new List<IndividuCEModel>();
            foreach (Tbl_IndividusCE _ind in listOfInd)
            {
                IndividuCEModel i = MapToIndividuCEModel(_ind);
                list.Add(i);
            }
            return list;
        }

        public static DecesCEModel MapToDecesCEModel(Tbl_DecesCE _deces)
        {
            DecesCEModel _dec = new DecesCEModel();
            _dec.MenageId = _deces.MenageId.GetValueOrDefault();
            _dec.LogeId = _deces.LogeId.GetValueOrDefault();
            _dec.BatimentId = _deces.BatimentId.GetValueOrDefault();
            _dec.SdeId = _deces.SdeId;
            _dec.DecesId = _deces.DecesId;
            _dec.Qd2NoOrdre = Convert.ToByte(_deces.Qd2NoOrdre.GetValueOrDefault());
            _dec.Qd1Deces = Convert.ToByte(_deces.Qd1Deces.GetValueOrDefault());
            //_dec.Qd2bAgeDecede = Convert.ToByte(_deces.Qd2bAgeDecede.GetValueOrDefault());
            _dec.Qd1NbreDecedeFille = Convert.ToByte(_deces.Qd1NbreDecedeFille.GetValueOrDefault());
            _dec.Qd1NbreDecedeGarcon = Convert.ToByte(_deces.Qd1NbreDecedeGarcon.GetValueOrDefault());
            _dec.DureeSaisie = Convert.ToByte(_deces.DureeSaisie.GetValueOrDefault());
            _dec.IsContreEnqueteMade = _deces.IsContreEnqueteMade.GetValueOrDefault();
            _dec.IsValidated = _deces.IsValidated.GetValueOrDefault();
            return _dec;
        }
        public static Tbl_DecesCE MapToTbl_DecesCE(DecesCEModel _deces)
        {
            Tbl_DecesCE _dec = new Tbl_DecesCE();
            _dec.MenageId = _deces.MenageId;
            _dec.LogeId = _deces.LogeId;
            _dec.BatimentId = _deces.BatimentId;
            _dec.SdeId = _deces.SdeId;
            _dec.DecesId = _deces.DecesId;
            _dec.Qd2NoOrdre = Convert.ToByte(_deces.Qd2NoOrdre.GetValueOrDefault());
            _dec.Qd1Deces = Convert.ToByte(_deces.Qd1Deces.GetValueOrDefault());
            //_dec.Qd2bAgeDecede = Convert.ToByte(_deces.Qd2bAgeDecede.GetValueOrDefault());
            _dec.Qd1NbreDecedeFille = Convert.ToByte(_deces.Qd1NbreDecedeFille.GetValueOrDefault());
            _dec.Qd1NbreDecedeGarcon = Convert.ToByte(_deces.Qd1NbreDecedeGarcon.GetValueOrDefault());
            _dec.DureeSaisie = Convert.ToByte(_deces.DureeSaisie.GetValueOrDefault());
            _dec.IsContreEnqueteMade = _deces.IsContreEnqueteMade.GetValueOrDefault();
            _dec.IsValidated = _deces.IsValidated.GetValueOrDefault();
            return _dec;
        }
        public static List<DecesCEModel> MapToListDecesCEModel(List<Tbl_DecesCE> listOfDeces)
        {
            List<DecesCEModel> list = new List<DecesCEModel>();
            foreach (Tbl_DecesCE deces in listOfDeces)
            {
                DecesCEModel model = ModelMapper.MapToDecesCEModel(deces);
                list.Add(model);
            }
            return list;
        }
        #endregion

        #region Question
        public static QuestionsModel MaptoQuestionModel(Tbl_Questions question)
        {
            return new QuestionsModel
            {
                CodeCategorie = question.CodeCategorie,
                CodeQuestion = question.CodeQuestion,
                Libelle = question.Libelle,
                NomObjet = question.NomObjet,
                DetailsQuestion = question.DetailsQuestion,
                NomChamps = question.NomChamps,
                TypeQuestion = Convert.ToInt32(question.TypeQuestion.GetValueOrDefault()),
                ContrainteQuestion = Convert.ToInt32(question.ContrainteQuestion.GetValueOrDefault()),
                ValeurMaxPrChiffre = Convert.ToInt32(question.ValeurMaxParChiffre.GetValueOrDefault()),
                NbreCaratereMaximal = Convert.ToInt32(question.NbreCaratereMaximal.GetValueOrDefault()),
                EstSautReponse = Convert.ToBoolean(question.EstSautReponse.GetValueOrDefault()),
                QPrecedent = question.QPrecedent,
                QSuivant = question.QSuivant
            };
        }


        public static Tbl_Questions MapModelToTbl_Questions(QuestionsModel question)
        {
            return new Tbl_Questions
            {
                CodeCategorie = question.CodeCategorie,
                Libelle = question.Libelle,
                NomObjet = question.NomObjet,
                DetailsQuestion = question.DetailsQuestion,
                NomChamps = question.NomChamps,
                TypeQuestion = question.TypeQuestion.GetValueOrDefault(),
                ContrainteQuestion = question.ContrainteQuestion.GetValueOrDefault(),
                ValeurMaxParChiffre = question.ValeurMaxPrChiffre.GetValueOrDefault(),
                NbreCaratereMaximal = question.NbreCaratereMaximal,
                EstSautReponse = Convert.ToInt32(question.EstSautReponse.GetValueOrDefault()),
                QPrecedent = question.QPrecedent,
                QSuivant = question.QSuivant
            };
        }

        public static List<QuestionsModel> MapTolistOfQuestion(List<Tbl_Questions> questions)
        {
            List<QuestionsModel> list = new List<QuestionsModel>();
            foreach (Tbl_Questions question in questions)
            {
                QuestionsModel q = new QuestionsModel();
                q = MaptoQuestionModel(question);
                list.Add(q);
            }
            return list;
        }

        public static ReponseModel MapToReponseModel(Tbl_Reponses rep)
        {
            if (rep != null)
            {
                ReponseModel reponse = new ReponseModel();
                reponse.CodeReponse = rep.CodeReponse;
                reponse.CodeUniqueReponse = rep.CodeUniqueReponse;
                reponse.LibelleReponse = rep.LibelleReponse;
                reponse.Name = rep.LibelleReponse;
                return reponse;
            }
            return new ReponseModel();
        }
        public static List<ReponseModel> MapToListReponseModel(List<Tbl_Reponses> reponses)
        {
            List<ReponseModel> list = new List<ReponseModel>();
            foreach (Tbl_Reponses rep in reponses)
            {
                ReponseModel r = MapToReponseModel(rep);
                list.Add(r);
            }
            return list;
        }

        public static QuestionReponseModel MapToQuestionReponseModel(Tbl_Questions_Reponses qr)
        {
            return new QuestionReponseModel
            {
                CodeParent = qr.CodeParent,
                CodeQuestion = qr.CodeQuestion,
                CodeUniqueReponse = qr.CodeUniqueReponse,
                AvoirEnfant = Convert.ToBoolean(qr.AvoirEnfant.GetValueOrDefault()),
                EstEnfant = Convert.ToBoolean(qr.EstEnfant.GetValueOrDefault()),
                QPrecedent = qr.QPrecedent,
                QSuivant = qr.QSuivant

            };
        }
        public static List<QuestionReponseModel> MapToListQuestionReponseModel(List<Tbl_Questions_Reponses> qr)
        {
            List<QuestionReponseModel> listOfQR = new List<QuestionReponseModel>();
            foreach (Tbl_Questions_Reponses q in qr)
            {
                QuestionReponseModel r = MapToQuestionReponseModel(q);
                listOfQR.Add(r);
            }
            return listOfQR;
        }

        public static CategorieQuestionModel MapToCategorieQuestionModel(Tbl_CategorieQuestion cat)
        {
            try
            {
                return new CategorieQuestionModel
                {
                    CodeCategorie = cat.codeCategorie,
                    CategorieQuestion = cat.categorieQuestion
                };
            }
            catch (Exception)
            {

            }
            return null;
        }

        //List<tbl_question> listOfQuestion<T>(T obj)
        //{

        //}
        #endregion

        #region Geo Data
        public static List<CommuneModel> MapToListCommune(List<Tbl_Commune> listOfCommune)
        {
            List<CommuneModel> list = new List<CommuneModel>();
            foreach (Tbl_Commune com in listOfCommune)
            {
                CommuneModel model = new CommuneModel();
                model.ComID = com.ComId;
                model.ComNom = com.ComNom;
                model.DeptId = com.DeptId;
                list.Add(model);
            }
            return list;
        }
        public static List<PaysModel> MapToListPays(List<Tbl_Pays> listOfPays)
        {
            List<PaysModel> list = new List<PaysModel>();
            foreach (Tbl_Pays pays in listOfPays)
            {
                PaysModel model = new PaysModel();
                model.CodePays = pays.CodePays;
                model.NomPays = pays.NomPays;
                list.Add(model);
            }
            return list;
        }
        public static List<VqseModel> MapToListVqse(List<Tbl_VilleQuartierSectionCommunale> listOfVqse)
        {
            List<VqseModel> list = new List<VqseModel>();
            if (listOfVqse != null)
            {
                foreach (Tbl_VilleQuartierSectionCommunale vqse in listOfVqse)
                {
                    VqseModel model = new VqseModel();
                    model.ComID = vqse.ComId;
                    model.VqseId = vqse.VqseId;
                    model.VqseNom = vqse.VqseNom;
                    list.Add(model);
                }
            }

            return list;
        }
        public static List<DepartementModel> MapToListDepartement(List<Tbl_Departement> listOfDept)
        {
            List<DepartementModel> list = new List<DepartementModel>();
            if (listOfDept != null)
            {
                foreach (Tbl_Departement vqse in listOfDept)
                {
                    DepartementModel model = new DepartementModel();
                    model.DeptId = vqse.DeptId;
                    model.DeptNom = vqse.DeptNom;
                    list.Add(model);
                }
            }

            return list;
        }
        public static CommuneModel MapToCommune(Tbl_Commune com)
        {
            if (com != null)
            {
                return new CommuneModel
                {
                    ComID = com.ComId,
                    DeptId = com.DeptId,
                    ComNom = com.ComNom
                };
            }
            return new CommuneModel();
        }
        public static DepartementModel MapToDepartement(Tbl_Departement dept)
        {
            DepartementModel model = new DepartementModel();
            if (dept != null)
            {
                model.DeptId = dept.DeptId;
                model.DeptNom = dept.DeptNom;
            }
            return model;
        }
        public static VqseModel MapToVsqe(Tbl_VilleQuartierSectionCommunale vqse)
        {
            if (vqse != null)
            {
                return new VqseModel
                {
                    ComID = vqse.ComId,
                    VqseId = vqse.VqseId,
                    VqseNom = vqse.VqseNom
                };
            }
            return new VqseModel();
        }
        public static PaysModel MapToPays(Tbl_Pays pays)
        {
            return new PaysModel
            {
                CodePays = pays.CodePays,
                NomPays = pays.NomPays
            };
        }


        #endregion

        #region TBL_RAPPORT_PERSONNEL
        public static RapportPersonnelModel MapToRapportPersonnelModel(Tbl_RapportPersonnel rpt)
        {
            RapportPersonnelModel model = new RapportPersonnelModel();
            model.codeDistrict = rpt.codeDistrict;
            model.comId = rpt.comId;
            model.dateEvaluation = rpt.dateEvaluation;
            model.deptId = rpt.deptId;
            model.persId = rpt.persId;
            model.q1 = rpt.q1;
            model.q10 = rpt.q10;
            model.q11 = rpt.q11;
            model.q12 = rpt.q12;
            model.q13 = rpt.q13;
            model.q14 = rpt.q14;
            model.q15 = rpt.q15;
            model.q9 = rpt.q9;
            model.q8 = rpt.q8;
            model.q7 = rpt.q7;
            model.q6 = rpt.q6;
            model.q5 = rpt.q5;
            model.q4 = rpt.q4;
            model.q3 = rpt.q3;
            model.q2 = rpt.q2;
            model.q1 = rpt.q1;
            model.ReportSenderId = rpt.ReportSenderId;
            model.score = rpt.score;
            model.rapportId = rpt.rapportId;
            model.RapportName = "Rapport-" + rpt.rapportId;
            return model;
        }

        public static Tbl_RapportPersonnel MapToTbl_RapportPersonnel(RapportPersonnelModel rpt)
        {
            Tbl_RapportPersonnel entite = new Tbl_RapportPersonnel();
            entite.codeDistrict = rpt.codeDistrict;
            entite.comId = rpt.comId;
            entite.dateEvaluation = rpt.dateEvaluation;
            entite.deptId = rpt.deptId;
            entite.persId = rpt.persId;
            entite.q1 = rpt.q1;
            entite.q10 = rpt.q10;
            entite.q11 = rpt.q11;
            entite.q12 = rpt.q12;
            entite.q13 = rpt.q13;
            entite.q14 = rpt.q14;
            entite.q15 = rpt.q15;
            entite.q9 = rpt.q9;
            entite.q8 = rpt.q8;
            entite.q7 = rpt.q7;
            entite.q6 = rpt.q6;
            entite.q5 = rpt.q5;
            entite.q4 = rpt.q4;
            entite.q3 = rpt.q3;
            entite.q2 = rpt.q2;
            entite.q1 = rpt.q1;
            entite.ReportSenderId = rpt.ReportSenderId;
            entite.score = rpt.score;
            entite.rapportId = rpt.rapportId;
            return entite;
        }

        public static List<RapportPersonnelModel> MapToListRapportPersonnelModel(List<Tbl_RapportPersonnel> listOf)
        {
            List<RapportPersonnelModel> listofRpts = new List<RapportPersonnelModel>();
            foreach (Tbl_RapportPersonnel rpt in listOf)
            {
                RapportPersonnelModel model = MapToRapportPersonnelModel(rpt);
                listofRpts.Add(model);
            }
            return listofRpts;
        }
        #endregion

        #region RAPPORT DEROULEMENT COLLECTE
        public static DetailsRapportModel MapToDetailsRapportModel(Tbl_DetailsRapport rpt)
        {
            DetailsRapportModel model = new DetailsRapportModel();
            model.Commentaire = rpt.Commentaire;
            model.DetailsRapportId = rpt.DetailsRapportId;
            model.RapportId = rpt.RapportId.GetValueOrDefault();
            model.Precisions = rpt.Precisions;
            model.Probleme = rpt.Probleme;
            model.Solution = rpt.Solution;
            model.Domaine = rpt.Domaine;
            model.SousDomaine = rpt.SousDomaine;
            model.Suggestions = rpt.Suggestions;
            model.Suivi = rpt.Suivi;
            return model;
        }
        public static Tbl_DetailsRapport MapToTbl_DetailsRapport(DetailsRapportModel rpt)
        {
            Tbl_DetailsRapport entity = new Tbl_DetailsRapport();
            entity.Commentaire = rpt.Commentaire;
            entity.DetailsRapportId = rpt.DetailsRapportId;
            entity.RapportId = rpt.RapportId;
            entity.Precisions = rpt.Precisions;
            entity.Probleme = rpt.Probleme;
            entity.Solution = rpt.Solution;
            entity.Domaine = rpt.Domaine;
            entity.SousDomaine = rpt.SousDomaine;
            entity.Suggestions = rpt.Suggestions;
            entity.Suivi = rpt.Suivi;
            return entity;
        }

        public static DetailsRapportDeroulement MapToDetailsRapportDeroulementModel(DetailsRapportModel row)
        {
            DetailsRapportDeroulement model = new DetailsRapportDeroulement();
            model.Precisions = row.Precisions;
            model.Probleme.Key = Convert.ToInt32(row.Probleme.GetValueOrDefault());
            model.RapportId = row.RapportId;
            model.DetailsRapportId = row.DetailsRapportId;
            model.Solution.Key = Convert.ToInt32(row.Solution.GetValueOrDefault());
            model.Domaine.Key = Convert.ToInt32(row.Domaine);
            model.SousDomaine.Key = Convert.ToInt32(row.SousDomaine);
            model.Suivi.Key = Convert.ToInt32(row.Suivi);
            model.Commentaire = row.Commentaire;
            model.Suggestions = row.Suggestions;
            return model;
        }
        public static DetailsRapportModel MapToDetailsRapportModel(DetailsRapportDeroulement row)
        {
            DetailsRapportModel model = new DetailsRapportModel();
            model.Precisions = row.Precisions;
            model.Probleme = row.Probleme.Key;
            model.DetailsRapportId = row.DetailsRapportId;
            model.RapportId = row.RapportId;
            model.Solution = row.Solution.Key;
            model.Domaine = row.Domaine.Key;
            model.SousDomaine = row.SousDomaine.Key;
            model.Suivi = row.Suivi.Key.ToString();
            model.Commentaire = row.Commentaire;
            model.Suggestions = row.Suggestions;
            return model;
        }
        public static List<DetailsRapportModel> MapToListDetailsRapportModel(List<Tbl_DetailsRapport> listOfRpt)
        {
            List<DetailsRapportModel> listOf = new List<DetailsRapportModel>();
            foreach (Tbl_DetailsRapport rpt in listOfRpt)
            {
                DetailsRapportModel model = MapToDetailsRapportModel(rpt);
                listOf.Add(model);
            }
            return listOf;
        }
        public static RapportDeroulementModel MapToRapportDeroulementModel(Tbl_RprtDeroulement rpt)
        {
            RapportDeroulementModel model = new RapportDeroulementModel();
            model.DateRapport = rpt.DateRapport;
            model.RapportId = rpt.RapportId;
            model.CodeDistrict = rpt.CodeDistrict;
            model.RapportName = "Rapport-" + rpt.RapportId;
            return model;
        }

        public static Tbl_RprtDeroulement MapToTbl_RprtDeroulement(RapportDeroulementModel model)
        {
            Tbl_RprtDeroulement entity = new Tbl_RprtDeroulement();
            entity.DateRapport = model.DateRapport;
            entity.RapportId = model.RapportId;
            entity.CodeDistrict = model.CodeDistrict;
            return entity;
        }

        public static List<RapportDeroulementModel> MapToListRapportDeroulementModel(List<Tbl_RprtDeroulement> listOfRpt)
        {
            List<RapportDeroulementModel> listOf = new List<RapportDeroulementModel>();
            foreach (Tbl_RprtDeroulement rpt in listOfRpt)
            {
                RapportDeroulementModel model = MapToRapportDeroulementModel(rpt);
                listOf.Add(model);
            }
            return listOf;
        }
        #endregion

        #region RETOURS
        public static RetourModel MapToRetourModel(Tbl_Retour entite)
        {
            RetourModel model = new RetourModel();
            model.RetourId = entite.RetourId;
            model.BatimentId = entite.BatimentId.GetValueOrDefault();
            model.LogementId = entite.LogementId.GetValueOrDefault();
            model.MenageId = entite.MenageId.GetValueOrDefault();
            model.SdeId = entite.SdeId;
            model.DecesId = entite.DecesId.GetValueOrDefault();
            model.EmigreId = entite.EmigreId.GetValueOrDefault();
            model.IndividuId = entite.IndividuId.GetValueOrDefault();
            model.DateRetour = entite.DateRetour;
            model.Raison = entite.Raison;
            model.Statut = entite.Statut;
            return model;
        }
        public static Tbl_Retour MapToTbl_Retour(RetourModel model)
        {
            Tbl_Retour entite = new Tbl_Retour();
            entite.RetourId = model.RetourId;
            entite.BatimentId = model.BatimentId.GetValueOrDefault();
            entite.LogementId = model.LogementId.GetValueOrDefault();
            entite.SdeId = model.SdeId;
            entite.MenageId = model.MenageId.GetValueOrDefault();
            entite.DecesId = model.DecesId.GetValueOrDefault();
            entite.EmigreId = model.EmigreId.GetValueOrDefault();
            entite.IndividuId = model.IndividuId.GetValueOrDefault();
            entite.DateRetour = model.DateRetour;
            entite.Raison = model.Raison;
            entite.Statut = model.Statut;
            return entite;
        }
        public static List<RetourModel> MapToListRetourModel(List<Tbl_Retour> listOfEntities)
        {
            List<RetourModel> listOfModels = new List<RetourModel>();
            foreach (Tbl_Retour entity in listOfEntities)
            {
                RetourModel model = MapToRetourModel(entity);
                listOfModels.Add(model);
            }
            return listOfModels;
        }
        #endregion

        #region RAPPORT AGENT RECENSEUR
        public static RapportArModel MapToRapportARModel(tbl_rapportrar rapportrar)
        {
            if (rapportrar != null)
            {
                return new RapportArModel
                {
                    RapportId = Convert.ToInt32(rapportrar.rapportId),
                    BatimentId = Convert.ToInt32(rapportrar.batimentId),
                    LogeId = Convert.ToInt32(rapportrar.logeId),
                    MenageId = Convert.ToInt32(rapportrar.menageId),
                    EmigreId = Convert.ToInt32(rapportrar.emigreId),
                    DecesId = Convert.ToInt32(rapportrar.decesId),
                    IndividuId = Convert.ToInt32(rapportrar.individuId),
                    RapportModuleName = rapportrar.rapportModuleName,
                    CodeQuestionStop = rapportrar.codeQuestionStop,
                    VisiteNumber = Convert.ToInt32(rapportrar.visiteNumber),
                    RaisonActionId = Convert.ToInt32(rapportrar.raisonActionId),
                    AutreRaisonAction = rapportrar.autreRaisonAction,
                    IsFieldAllFilled = Convert.ToBoolean(rapportrar.isFieldAllFilled),
                    DateDebutCollecte = rapportrar.dateDebutCollecte,
                    DateFinCollecte = rapportrar.dateFinCollecte,
                    DureeSaisie = Convert.ToInt32(rapportrar.dureeSaisie),
                    IsContreEnqueteMade = Convert.ToBoolean(rapportrar.isContreEnqueteMade),
                    CodeAgentRecenceur = rapportrar.codeAgentRecenceur
                };
            }
            return new RapportArModel();
        }

        public static List<RapportArModel> MapToListRapportARModel(List<tbl_rapportrar> listOf)
        {
            List<RapportArModel> rapports = new List<RapportArModel>();
            if (listOf != null)
            {
                foreach (tbl_rapportrar rapt in listOf)
                {
                    rapports.Add(MapToRapportARModel(rapt));
                }

            }
            return rapports;
        }
        #endregion

        #region Probleme
        public static ProblemeModel MapToProblemeModel(Tbl_Probleme probleme)
        {
            ProblemeModel model = new ProblemeModel();
            if (probleme != null)
            {
                model.BatimentId = probleme.BatimentId;
                model.CodeQuestion = probleme.CodeQuestion;
                model.ProblemeId = probleme.ProblemeId;
                model.Domaine = probleme.Domaine;
                model.Nature = probleme.Nature;
                model.Objet = probleme.Objet;
                model.SdeId = probleme.SdeId;
                model.Statut = probleme.Statut;
            }
            return model;
        }
        public static Tbl_Probleme MapToTbl_Probleme(ProblemeModel probleme)
        {
            Tbl_Probleme model = new Tbl_Probleme();
            if (probleme != null)
            {
                model.BatimentId = probleme.BatimentId;
                model.CodeQuestion = probleme.CodeQuestion;
                model.ProblemeId = probleme.ProblemeId;
                model.Domaine = probleme.Domaine;
                model.Nature = probleme.Nature;
                model.Objet = probleme.Objet;
                model.SdeId = probleme.SdeId;
                model.Statut = probleme.Statut;
            }
            return model;
        }
        public static List<ProblemeModel> MapToListProblemeModel(List<Tbl_Probleme> listOf)
        {
            List<ProblemeModel> listOfProblemes = new List<ProblemeModel>();
            if (listOf != null)
            {
                foreach (Tbl_Probleme prob in listOf)
                {
                    ProblemeModel mod = MapToProblemeModel(prob);
                    listOfProblemes.Add(mod);
                }
            }
            return listOfProblemes;
        }
        #endregion

    }
}
