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
using Ht.Ihsil.Rgph.App.Superviseur.services;

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
                //if (type == Constant.CODE_TYPE_ENVDIVIDI)
                //{
                //    countf = Convert.ToInt32(s.TotalIndFRecense.GetValueOrDefault());
                //    counth = Convert.ToInt32(s.TotalIndGRecense.GetValueOrDefault());
                //}
                //else if (type == Constant.CODE_TYPE_EMIGRE)
                //{
                //    countf = Convert.ToInt32(s.TotalEmigreFRecense.GetValueOrDefault());
                //    counth = Convert.ToInt32(s.TotalEmigreGRecense.GetValueOrDefault());
                //}
                //else
                //{
                //    countf = Convert.ToInt32(s.TotalDecesFRecense.GetValueOrDefault());
                //    counth = Convert.ToInt32(s.TotalDecesGRecense.GetValueOrDefault());
                //}

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
                model.SdeName = Utilities.getGeoInformationCommune(sde.SdeId);
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
                model.TotalIndRecense = Convert.ToInt32(sde.TotalIndRecense.GetValueOrDefault());
                model.TotalEmigreRecense = Convert.ToInt32(sde.TotalEmigreRecense.GetValueOrDefault());
                model.TotalDecesRecense = Convert.ToInt32(sde.TotalDecesRecense.GetValueOrDefault());
                model.TotalLogeIRecense = Convert.ToInt32(sde.TotalLogeIRecense.GetValueOrDefault());
                model.SdeName = Utilities.getGeoInformation(sde.SdeId);
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
                entity.TotalIndRecense = sde.TotalIndRecense.GetValueOrDefault();
                entity.TotalEmigreRecense = sde.TotalEmigreRecense.GetValueOrDefault();
                entity.TotalDecesRecense = sde.TotalDecesRecense.GetValueOrDefault();
                entity.TotalLogeIRecense = sde.TotalLogeIRecense.GetValueOrDefault();
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
        public static BatimentJson MapToJson(BatimentCEModel batiment)
        {
            if (batiment != null)
            {
                BatimentJson batimentJson = new BatimentJson();
                batimentJson.batimentId = Convert.ToInt32(batiment.BatimentId);
                batimentJson.deptId = batiment.DeptId;
                batimentJson.comId = batiment.ComId;
                batimentJson.vqseId = batiment.VqseId;
                batimentJson.sdeId = Utilities.getSdeFormatSent(batiment.SdeId);
                batimentJson.zone = Convert.ToByte(batiment.Zone);
                batimentJson.districtId = batiment.District;
                batimentJson.qhabitation = batiment.Qhabitation;
                batimentJson.qlocalite = batiment.Qlocalite;
                batimentJson.qadresse = batiment.Qadresse;
                batimentJson.qrec = batiment.Qrec;
                batimentJson.qrgph = batiment.Qrgph;
                batimentJson.qb1Etat = Convert.ToByte(batiment.Qb1Etat);
                batimentJson.qb2Type = Convert.ToByte(batiment.Qb2Type);
                batimentJson.qb3NombreEtage = Convert.ToByte(batiment.Qb3NombreEtage);
                batimentJson.qb4MateriauMur = Convert.ToByte(batiment.Qb4MateriauMur);
                batimentJson.qb5MateriauToit = Convert.ToByte(batiment.Qb5MateriauToit);
                batimentJson.qb6StatutOccupation = Convert.ToByte(batiment.Qb6StatutOccupation);
                batimentJson.qb7Utilisation1 = Convert.ToByte(batiment.Qb7Utilisation1);
                batimentJson.qb7Utilisation2 = Convert.ToByte(batiment.Qb7Utilisation2);
                batimentJson.qb8NbreLogeCollectif = Convert.ToByte(batiment.Qb8NbreLogeCollectif);
                batimentJson.qb8NbreLogeIndividuel = Convert.ToByte(batiment.Qb8NbreLogeIndividuel);
                batimentJson.statut = Convert.ToByte(batiment.Statut);
                //batimentJson.dateEnvoi = DateTime.ParseExact(batiment.DateEnvoi, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                batimentJson.isValidated = Convert.ToBoolean(batiment.IsValidated);
                batimentJson.isSynchroToCentrale = Convert.ToBoolean(batiment.IsSynchroToCentrale);
                batimentJson.dateDebutCollecte = DateTime.ParseExact(batiment.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                if (batiment.DateFinCollecte != null)
                {
                    batimentJson.dateFinCollecte = DateTime.ParseExact(batiment.DateFinCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToUniversalTime().ToString();
                }
                batimentJson.dureeSaisie = Convert.ToInt32(batiment.DureeSaisie);
                batimentJson.isContreEnqueteMade = Convert.ToBoolean(batiment.IsContreEnqueteMade);
                return batimentJson;
            }
            return new BatimentJson();
        }
        public static LogementCJson MapToCLJson(LogementCEModel logement)
        {
            LogementCJson logementJson = new LogementCJson();
            if (logement != null)
            {
                logementJson.logeId = Convert.ToInt32(logement.LogeId);
                logementJson.batimentId = Convert.ToInt32(logement.BatimentId);
                logementJson.sdeId = Utilities.getSdeFormatSent(logement.SdeId);
                logementJson.qlin1NumeroOrdre = Convert.ToByte(logement.Qlin1NumeroOrdre);
                logementJson.qlc1TypeLogement = Convert.ToByte(logement.QlcTypeLogement);
                logementJson.qlc2bTotalGarcon = Convert.ToByte(logement.Qllc2bTotalGarcon);
                logementJson.qlc2bTotalFille = Convert.ToByte(logement.Qlc2bTotalFille);
                logementJson.statut = Convert.ToByte(logement.Statut);
                logementJson.isValidated = Convert.ToBoolean(logement.IsValidated);
                if (logement.DateDebutCollecte != null)
                {
                    logementJson.dateDebutCollecte = logementJson.dateFinCollecte = DateTime.ParseExact(logement.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                if (logement.DateFinCollecte != null)
                {
                    logementJson.dateFinCollecte = DateTime.ParseExact(logement.DateFinCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                logementJson.dureeSaisie = Convert.ToInt32(logement.DureeSaisie);
                logementJson.isContreEnqueteMade = Convert.ToBoolean(logement.IsContreEnqueteMade);
                return logementJson;
            }
            return new LogementCJson();
        }
        public static LogementIsJson MapToILJson(LogementCEModel logement)
        {
            if (logement != null)
            {
                LogementIsJson logementJson = new LogementIsJson();
                logementJson.logeId = Convert.ToInt32(logement.LogeId);
                logementJson.batimentId = Convert.ToInt32(logement.BatimentId);
                logementJson.sdeId = Utilities.getSdeFormatSent(logement.SdeId);
                logementJson.qlCategLogement = Convert.ToByte(logement.QlCategLogement);
                logementJson.qlin1NumeroOrdre = Convert.ToByte(logement.Qlin1NumeroOrdre);
                logementJson.qlc1TypeLogement = Convert.ToByte(logement.QlcTypeLogement);
                logementJson.qlc2bTotalGarcon = Convert.ToByte(logement.Qllc2bTotalGarcon);
                logementJson.qlc2bTotalFille = Convert.ToByte(logement.Qlc2bTotalFille);
                logementJson.qlin2StatutOccupation = Convert.ToByte(logement.Qlin2StatutOccupation);
                logementJson.qlin4TypeLogement = Convert.ToByte(logement.Qlin4TypeLogement);
                logementJson.qlin5MateriauSol = Convert.ToByte(logement.Qlin5MateriauSol);
                logementJson.qlin6NombrePiece = Convert.ToByte(logement.Qlin6NombrePiece);
                logementJson.qlin7NbreChambreACoucher = Convert.ToByte(logement.Qlin7NbreChambreACoucher);
                logementJson.qlin8NbreIndividuDepense = Convert.ToByte(logement.Qlin8NbreIndividuDepense);
                logementJson.qlin9NbreTotalMenage = Convert.ToByte(logement.Qlin9NbreTotalMenage);
                logementJson.statut = Convert.ToByte(logement.Statut);
                logementJson.isValidated = Convert.ToBoolean(logement.IsValidated);
                if (logement.DateDebutCollecte != null)
                {
                    logementJson.dateDebutCollecte = logementJson.dateFinCollecte = DateTime.ParseExact(logement.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                if (logement.DateFinCollecte != null)
                {
                    logementJson.dateFinCollecte = DateTime.ParseExact(logement.DateFinCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                logementJson.dureeSaisie = Convert.ToInt32(logement.DureeSaisie);
                logementJson.isContreEnqueteMade = Convert.ToBoolean(logement.IsContreEnqueteMade);
                return logementJson;
            }
            return new LogementIsJson();
        }
        public static MenageJson MapToJson(MenageCEModel menage)
        {
            if (menage != null)
            {
                MenageJson menageJson = new MenageJson();
                menageJson.menageId = Convert.ToInt32(menage.MenageId);
                menageJson.logeId = Convert.ToInt32(menage.LogeId);
                menageJson.batimentId = Convert.ToInt32(menage.BatimentId);
                menageJson.sdeId = Utilities.getSdeFormatSent(menage.SdeId);
                menageJson.qm1NoOrdre = Convert.ToByte(menage.Qm1NoOrdre);
                menageJson.qm2ModeJouissance = Convert.ToByte(menage.Qm2ModeJouissance);
                menageJson.qm5SrcEnergieCuisson1 = Convert.ToByte(menage.Qm5SrcEnergieCuisson1);
                menageJson.qm5SrcEnergieCuisson2 = Convert.ToByte(menage.Qm5SrcEnergieCuisson2);
                menageJson.qm8EndroitBesoinPhysiologique = Convert.ToByte(menage.Qm8EndroitBesoinPhysiologique);
                menageJson.qm11TotalIndividuVivant = Convert.ToInt32(menage.Qm11TotalIndividuVivant);
               menageJson.statut = Convert.ToByte(menage.Statut);
                menageJson.isValidated = Convert.ToBoolean(menage.IsValidated);
                if (menage.DateDebutCollecte != null)
                {
                    menageJson.dateDebutCollecte = DateTime.ParseExact(menage.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                if (menage.DateFinCollecte != null)
                {
                    menageJson.dateFinCollecte = DateTime.ParseExact(menage.DateFinCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                menageJson.dureeSaisie = Convert.ToInt32(menage.DureeSaisie);
                menageJson.isContreEnqueteMade = Convert.ToBoolean(menage.IsContreEnqueteMade);
                return menageJson;
            }
            return new MenageJson();
        }
        public static DecesJson MapToJson(DecesCEModel deces)
        {
            if (deces != null)
            {
                DecesJson decesJson = new DecesJson();
                decesJson.decesId = Convert.ToInt32(deces.DecesId);
                decesJson.menageId = Convert.ToInt32(deces.MenageId);
                decesJson.logeId = Convert.ToInt32(deces.LogeId);
                decesJson.batimentId = Convert.ToInt32(deces.BatimentId);
                decesJson.sdeId = Utilities.getSdeFormatSent(deces.SdeId);
                decesJson.qd2NoOrdre = Convert.ToByte(deces.Qd2NoOrdre);
                decesJson.qd1aNbreDecesF = Convert.ToByte(deces.Qd1aNbreDecesF);
                decesJson.qd1aNbreDecesG = Convert.ToByte(deces.Qd1aNbreDecesG);
                decesJson.statut = Convert.ToByte(deces.Statut);
                if (deces.DateDebutCollecte != null)
                {
                    decesJson.dateDebutCollecte = DateTime.ParseExact(deces.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                if (deces.DateFinCollecte != null)
                {
                    decesJson.dateFinCollecte = DateTime.ParseExact(deces.DateFinCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                decesJson.dureeSaisie = Convert.ToInt32(deces.DureeSaisie);
                decesJson.isContreEnqueteMade = Convert.ToBoolean(deces.IsContreEnqueteMade);
                return decesJson;
            }
            return new DecesJson();
        }
        public static EmigreJson MapToJson(EmigreCEModel emigre)
        {
            if (emigre != null)
            {
                EmigreJson emigreJson = new EmigreJson();
                emigreJson.emigreId = Convert.ToInt32(emigre.EmigreId);
                emigreJson.menageId = Convert.ToInt32(emigre.MenageId);
                emigreJson.logeId = Convert.ToInt32(emigre.LogeId);
                emigreJson.batimentId = Convert.ToInt32(emigre.BatimentId);
                emigreJson.sdeId = Utilities.getSdeFormatSent(emigre.SdeId);
                emigreJson.qn1numeroOrdre = Convert.ToByte(emigre.Qn1numeroOrdre);
                emigreJson.qn1Emigration = Convert.ToByte(emigre.Qn1Emigration);
                emigreJson.qn1NbreEmigreG = Convert.ToByte(emigre.Qn1NbreEmigreG);
                emigreJson.qn1NbreEmigreF = Convert.ToByte(emigre.Qn1NbreEmigreF);
                emigreJson.statut = Convert.ToByte(emigre.Statut);
                if (emigre.DateDebutCollecte != null)
                {
                    emigreJson.dateDebutCollecte = DateTime.ParseExact(emigre.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                if (emigre.DateFinCollecte != null)
                {
                    emigreJson.dateFinCollecte = DateTime.ParseExact(emigre.DateFinCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                return emigreJson;
            }
            return new EmigreJson();
        }
        public static IndividuJson MapToJson(IndividuCEModel individu)
        {
            if (individu != null)
            {
                IndividuJson individuJson = new IndividuJson();
                individuJson.individuId = Convert.ToInt32(individu.IndividuId);
                individuJson.menageId = Convert.ToInt32(individu.MenageId);
                individuJson.logeId = Convert.ToInt32(individu.LogeId);
                individuJson.batimentId = Convert.ToInt32(individu.BatimentId);
                individuJson.sdeId = Utilities.getSdeFormatSent(individu.SdeId);
                individuJson.q1NoOrdre = Convert.ToByte(individu.Qp1NoOrdre);
                individuJson.qp2APrenom = individu.Q3Prenom;
                individuJson.qp2BNom = individu.Q2Nom;
                individuJson.qp3LienDeParente = Convert.ToByte(individu.Q3LienDeParente);
                individuJson.q3aRaisonChefMenage = Convert.ToByte(individu.Q3aRaisonChefMenage);
                individuJson.qp4Sexe = Convert.ToByte(individu.Q4Sexe);
                individuJson.qp5DateNaissanceJour = Convert.ToByte(individu.Q7DateNaissanceJour);
                individuJson.qp5DateNaissanceMois = Convert.ToByte(individu.Q7DateNaissanceMois);
                individuJson.qp5DateNaissanceAnnee = Convert.ToInt32(individu.Q7DateNaissanceAnnee);
                individuJson.qp5bAge = Convert.ToByte(individu.Q5bAge);
                individuJson.qp7Nationalite = Convert.ToByte(individu.Qp7Nationalite);
                individuJson.qp7PaysNationalite = individu.Qp7PaysNationalite;
                individuJson.qp10LieuNaissance = Convert.ToByte(individu.Qp10LieuNaissance);
                individuJson.qp10CommuneNaissance = individu.Qp10CommuneNaissance;
                individuJson.qp10VqseNaissance = individu.Qp10LieuNaissanceVqse;
                individuJson.qp10PaysNaissance = individu.Qp10PaysNaissance;
                individuJson.qp11PeriodeResidence = Convert.ToByte(individu.Qp11PeriodeResidence);
                individuJson.qe2FreqentationScolaireOuUniv = Convert.ToByte(individu.Qe2FreqentationScolaireOuUniv);
                individuJson.qe4aNiveauEtude = Convert.ToByte(individu.Qe4aNiveauEtude);
                individuJson.qe4bDerniereClasseOUAneEtude = ""+individu.Qe4bDerniereClasseOUAneEtude;
                individuJson.qsm1StatutMatrimonial = Convert.ToByte(individu.Qsm1StatutMatrimonial);
                individuJson.qa1ActEconomiqueDerniereSemaine = Convert.ToByte(individu.Qa1ActEconomiqueDerniereSemaine);
                individuJson.qa2ActAvoirDemele1 = Convert.ToByte(individu.Qa2ActAvoirDemele1);
                individuJson.qa2ActDomestique2 = Convert.ToByte(individu.Qa2ActDomestique2);
                individuJson.qa2ActCultivateur3 = Convert.ToByte(individu.Qa2ActCultivateur3);
                individuJson.qa2ActAiderParent4 = Convert.ToByte(individu.Qa2ActAiderParent4);
                individuJson.qa2ActAutre5 = Convert.ToByte(individu.Qa2ActAutre5);
                individuJson.qa8EntreprendreDemarcheTravail = Convert.ToByte(individu.Qa8EntreprendreDemarcheTravail);
                individuJson.qf1aNbreEnfantNeVivantM = Convert.ToInt32(individu.Qf1aNbreEnfantNeVivantM);
                individuJson.qf1bNbreEnfantNeVivantF = Convert.ToInt32(individu.Qf2bNbreEnfantNeVivantF);
                individuJson.qf2aNbreEnfantVivantM = Convert.ToInt32(individu.Qf2aNbreEnfantVivantM);
                individuJson.qf2bNbreEnfantVivantF = Convert.ToInt32(individu.Qf2bNbreEnfantVivantF);
                individuJson.qf3DernierEnfantJour = Convert.ToByte(individu.Qf3DernierEnfantJour);
                individuJson.qf3DernierEnfantMois = Convert.ToByte(individu.Qf3DernierEnfantMois);
                individuJson.qf3DernierEnfantAnnee = Convert.ToInt32(individu.Qf3DernierEnfantAnnee);
                individuJson.qf4DENeVivantVit = Convert.ToByte(individu.Qf4DENeVivantVit);
                individuJson.statut = Convert.ToByte(individu.Statut);
                if (individu.DateDebutCollecte != null)
                {
                    individuJson.dateDebutCollecte = DateTime.ParseExact(individu.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToUniversalTime().ToString();
                }
                if (individu.DateFinCollecte != null)
                {
                    individuJson.dateFinCollecte = DateTime.ParseExact(individu.DateFinCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToUniversalTime().ToString();
                }
                individuJson.dureeSaisie = Convert.ToInt32(individu.DureeSaisie);
                individuJson.isContreEnqueteMade = Convert.ToBoolean(individu.IsContreEnqueteMade);
                return individuJson;
            }
            return new IndividuJson();
        }

        public static BatimentJson MapToJson(BatimentModel batiment)
        {
            if (batiment != null)
            {
                BatimentJson batimentJson = new BatimentJson();
                batimentJson.batimentId = Convert.ToInt32(batiment.BatimentId);
                batimentJson.deptId = batiment.DeptId;
                batimentJson.comId = batiment.ComId;
                batimentJson.vqseId = batiment.VqseId;
                batimentJson.sdeId = Utilities.getSdeFormatSent(batiment.SdeId);
                batimentJson.zone = Convert.ToByte(batiment.Zone);
                batimentJson.districtId = batiment.DistrictId;
                batimentJson.qhabitation = batiment.Qhabitation;
                batimentJson.qlocalite = batiment.Qlocalite;
                batimentJson.qadresse = batiment.Qadresse;
                batimentJson.qrec = batiment.Qrec;
                batimentJson.qrgph = batiment.Qrgph;
                batimentJson.qb1Etat = Convert.ToByte(batiment.Qb1Etat);
                batimentJson.qb2Type = Convert.ToByte(batiment.Qb2Type);
                batimentJson.qb3NombreEtage = Convert.ToByte(batiment.Qb3NombreEtage);
                batimentJson.qb4MateriauMur = Convert.ToByte(batiment.Qb4MateriauMur);
                batimentJson.qb5MateriauToit = Convert.ToByte(batiment.Qb5MateriauToit);
                batimentJson.qb6StatutOccupation = Convert.ToByte(batiment.Qb6StatutOccupation);
                batimentJson.qb7Utilisation1 = Convert.ToByte(batiment.Qb7Utilisation1);
                batimentJson.qb7Utilisation2 = Convert.ToByte(batiment.Qb7Utilisation2);
                batimentJson.qb8NbreLogeCollectif = Convert.ToByte(batiment.Qb8NbreLogeCollectif);
                batimentJson.qb8NbreLogeIndividuel = Convert.ToByte(batiment.Qb8NbreLogeIndividuel);
                batimentJson.statut = Convert.ToByte(batiment.Statut);
                //batimentJson.dateEnvoi = DateTime.ParseExact(batiment.DateEnvoi, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                batimentJson.isValidated = Convert.ToBoolean(batiment.IsValidated);
                batimentJson.isSynchroToAppSup = Convert.ToBoolean(batiment.IsSynchroToAppSup);
                batimentJson.isSynchroToCentrale = Convert.ToBoolean(batiment.IsSynchroToCentrale);
                batimentJson.dateDebutCollecte = DateTime.ParseExact(batiment.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                if (batiment.DateFinCollecte != null)
                {
                    batimentJson.dateFinCollecte = DateTime.ParseExact(batiment.DateFinCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToUniversalTime().ToString();
                }
                batimentJson.dureeSaisie = Convert.ToInt32(batiment.DureeSaisie);
                batimentJson.isFieldAllFilled = Convert.ToBoolean(batiment.IsFieldAllFilled);
                batimentJson.isContreEnqueteMade = Convert.ToBoolean(batiment.IsContreEnqueteMade);
                batimentJson.latitude = batiment.Latitude;
                batimentJson.longitude = batiment.Longitude;
                batimentJson.codeAgentRecenceur = batiment.CodeAgentRecenceur;
                return batimentJson;
            }
            return new BatimentJson();
        }
        public static LogementJson MapToJson(LogementModel logement)
        {
            if (logement != null)
            {
                LogementJson logementJson = new LogementJson();
                logementJson.logeId = Convert.ToInt32(logement.LogeId);
                logementJson.batimentId = Convert.ToInt32(logement.BatimentId);
                logementJson.sdeId = Utilities.getSdeFormatSent(logement.SdeId);
                logementJson.qlCategLogement = Convert.ToByte(logement.QlCategLogement);
                logementJson.qlin1NumeroOrdre = Convert.ToByte(logement.Qlin1NumeroOrdre);
                logementJson.qlc1TypeLogement = Convert.ToByte(logement.Qlc1TypeLogement);
                logementJson.qlc2bTotalGarcon = Convert.ToByte(logement.Qlc2bTotalGarcon);
                logementJson.qlc2bTotalFille = Convert.ToByte(logement.Qlc2bTotalFille);
                logementJson.qlcTotalIndividus = Convert.ToByte(logement.QlcTotalIndividus);
                logementJson.qlin2StatutOccupation = Convert.ToByte(logement.Qlin2StatutOccupation);
                logementJson.qlin3ExistenceLogement = Convert.ToByte(logement.Qlin3ExistenceLogement);
                logementJson.qlin4TypeLogement = Convert.ToByte(logement.Qlin4TypeLogement);
                logementJson.qlin5MateriauSol = Convert.ToByte(logement.Qlin5MateriauSol);
                logementJson.qlin6NombrePiece = Convert.ToByte(logement.Qlin6NombrePiece);
                logementJson.qlin7NbreChambreACoucher = Convert.ToByte(logement.Qlin7NbreChambreACoucher);
                logementJson.qlin8NbreIndividuDepense = Convert.ToByte(logement.Qlin8NbreIndividuDepense);
                logementJson.qlin9NbreTotalMenage = Convert.ToByte(logement.Qlin9NbreTotalMenage);
                logementJson.statut = Convert.ToByte(logement.Statut);
                logementJson.isValidated = Convert.ToBoolean(logement.IsValidated);
                if (logement.DateDebutCollecte!=null)
                {
                    logementJson.dateDebutCollecte = logementJson.dateFinCollecte = DateTime.ParseExact(logement.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                if (logement.DateFinCollecte != null)
                {
                    logementJson.dateFinCollecte = DateTime.ParseExact(logement.DateFinCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                logementJson.dureeSaisie = Convert.ToInt32(logement.DureeSaisie);
                logementJson.isFieldAllFilled = Convert.ToBoolean(logement.IsFieldAllFilled);
                logementJson.isContreEnqueteMade = Convert.ToBoolean(logement.IsContreEnqueteMade);
                logementJson.nbrTentative = Convert.ToByte(logement.NbrTentative);
                logementJson.codeAgentRecenceur = logement.CodeAgentRecenceur;
                return logementJson;
            }
            return new LogementJson();
        }
        public static LogementCJson MapToLCJson(LogementModel logement)
        {
            LogementCJson logementJson = new LogementCJson();
            if (logement != null)
            {
                logementJson.logeId = Convert.ToInt32(logement.LogeId);
                logementJson.batimentId = Convert.ToInt32(logement.BatimentId);
                logementJson.sdeId = Utilities.getSdeFormatSent(logement.SdeId);
                logementJson.qlin1NumeroOrdre = Convert.ToByte(logement.Qlin1NumeroOrdre);
                logementJson.qlc1TypeLogement = Convert.ToByte(logement.Qlc1TypeLogement);
                logementJson.qlc2bTotalGarcon = Convert.ToByte(logement.Qlc2bTotalGarcon);
                logementJson.qlc2bTotalFille = Convert.ToByte(logement.Qlc2bTotalFille);
                logementJson.qlcTotalIndividus = Convert.ToByte(logement.QlcTotalIndividus);
                logementJson.statut = Convert.ToByte(logement.Statut);
                logementJson.isValidated = Convert.ToBoolean(logement.IsValidated);
                if (logement.DateDebutCollecte != null)
                {
                    logementJson.dateDebutCollecte = logementJson.dateFinCollecte = DateTime.ParseExact(logement.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                if (logement.DateFinCollecte != null)
                {
                    logementJson.dateFinCollecte = DateTime.ParseExact(logement.DateFinCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                logementJson.dureeSaisie = Convert.ToInt32(logement.DureeSaisie);
                logementJson.isFieldAllFilled = Convert.ToBoolean(logement.IsFieldAllFilled);
                logementJson.isContreEnqueteMade = Convert.ToBoolean(logement.IsContreEnqueteMade);
                logementJson.codeAgentRecenceur = logement.CodeAgentRecenceur;
                return logementJson;
            }
            return new LogementCJson();
        }
        public static LogementIsJson MapToLIJson(LogementModel logement)
        {
            if (logement != null)
            {
                LogementIsJson logementJson = new LogementIsJson();
                logementJson.logeId = Convert.ToInt32(logement.LogeId);
                logementJson.batimentId = Convert.ToInt32(logement.BatimentId);
                logementJson.sdeId = Utilities.getSdeFormatSent(logement.SdeId);
                logementJson.qlCategLogement = Convert.ToByte(logement.QlCategLogement);
                logementJson.qlin1NumeroOrdre = Convert.ToByte(logement.Qlin1NumeroOrdre);
                logementJson.qlc1TypeLogement = Convert.ToByte(logement.Qlc1TypeLogement);
                logementJson.qlc2bTotalGarcon = Convert.ToByte(logement.Qlc2bTotalGarcon);
                logementJson.qlc2bTotalFille = Convert.ToByte(logement.Qlc2bTotalFille);
                logementJson.qlcTotalIndividus = Convert.ToByte(logement.QlcTotalIndividus);
                logementJson.qlin2StatutOccupation = Convert.ToByte(logement.Qlin2StatutOccupation);
                logementJson.qlin3ExistenceLogement = Convert.ToByte(logement.Qlin3ExistenceLogement);
                logementJson.qlin4TypeLogement = Convert.ToByte(logement.Qlin4TypeLogement);
                logementJson.qlin5MateriauSol = Convert.ToByte(logement.Qlin5MateriauSol);
                logementJson.qlin6NombrePiece = Convert.ToByte(logement.Qlin6NombrePiece);
                logementJson.qlin7NbreChambreACoucher = Convert.ToByte(logement.Qlin7NbreChambreACoucher);
                logementJson.qlin8NbreIndividuDepense = Convert.ToByte(logement.Qlin8NbreIndividuDepense);
                logementJson.qlin9NbreTotalMenage = Convert.ToByte(logement.Qlin9NbreTotalMenage);
                logementJson.statut = Convert.ToByte(logement.Statut);
                logementJson.isValidated = Convert.ToBoolean(logement.IsValidated);
                if (logement.DateDebutCollecte != null)
                {
                    logementJson.dateDebutCollecte = logementJson.dateFinCollecte = DateTime.ParseExact(logement.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                if (logement.DateFinCollecte != null)
                {
                    logementJson.dateFinCollecte = DateTime.ParseExact(logement.DateFinCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                logementJson.dureeSaisie = Convert.ToInt32(logement.DureeSaisie);
                logementJson.isFieldAllFilled = Convert.ToBoolean(logement.IsFieldAllFilled);
                logementJson.isContreEnqueteMade = Convert.ToBoolean(logement.IsContreEnqueteMade);
                logementJson.nbrTentative = Convert.ToByte(logement.NbrTentative);
                logementJson.codeAgentRecenceur = logement.CodeAgentRecenceur;
                return logementJson;
            }
            return new LogementIsJson();
        }
        public static MenageJson MapToJson(MenageModel menage)
        {
            if (menage != null)
            {
                MenageJson menageJson = new MenageJson();
                menageJson.menageId = Convert.ToInt32(menage.MenageId);
                menageJson.logeId = Convert.ToInt32(menage.LogeId);
                menageJson.batimentId = Convert.ToInt32(menage.BatimentId);
                menageJson.sdeId = Utilities.getSdeFormatSent(menage.SdeId);
                menageJson.qm1NoOrdre = Convert.ToByte(menage.Qm1NoOrdre);
                menageJson.qm2ModeJouissance = Convert.ToByte(menage.Qm2ModeJouissance);
                menageJson.qm3ModeObtentionLoge = Convert.ToByte(menage.Qm3ModeObtentionLoge);
                menageJson.qm4_1ModeAprovEauABoire = Convert.ToByte(menage.Qm4_1ModeAprovEauABoire);
                menageJson.qm4_2ModeAprovEauAUsageCourant = Convert.ToByte(menage.Qm4_2ModeAprovEauAUsageCourant);
                menageJson.qm5SrcEnergieCuisson1 = Convert.ToByte(menage.Qm5SrcEnergieCuisson1);
                menageJson.qm5SrcEnergieCuisson2 = Convert.ToByte(menage.Qm5SrcEnergieCuisson2);
                menageJson.qm6TypeEclairage = Convert.ToByte(menage.Qm6TypeEclairage);
                menageJson.qm7ModeEvacDechet = Convert.ToByte(menage.Qm7ModeEvacDechet);
                menageJson.qm8EndroitBesoinPhysiologique = Convert.ToByte(menage.Qm8EndroitBesoinPhysiologique);
                menageJson.qm9NbreRadio1 = Convert.ToInt32(menage.Qm9NbreRadio1);
                menageJson.qm9NbreTelevision2 = Convert.ToInt32(menage.Qm9NbreTelevision2);
                menageJson.qm9NbreRefrigerateur3 = Convert.ToInt32(menage.Qm9NbreRefrigerateur3);
                menageJson.qm9NbreFouElectrique4 = Convert.ToInt32(menage.Qm9NbreFouElectrique4);
                menageJson.qm9NbreOrdinateur5 = Convert.ToInt32(menage.Qm9NbreOrdinateur5);
                menageJson.qm9NbreMotoBicyclette6 = Convert.ToInt32(menage.Qm9NbreMotoBicyclette6);
                menageJson.qm9NbreVoitureMachine7 = Convert.ToInt32(menage.Qm9NbreVoitureMachine7);
                menageJson.qm9NbreBateau8 = Convert.ToInt32(menage.Qm9NbreBateau8);
                menageJson.qm9NbrePanneauGeneratrice9 = Convert.ToInt32(menage.Qm9NbrePanneauGeneratrice9);
                menageJson.qm9NbreMilletChevalBourique10 = Convert.ToInt32(menage.Qm9NbreMilletChevalBourique10);
                menageJson.qm9NbreBoeufVache11 = Convert.ToInt32(menage.Qm9NbreBoeufVache11);
                menageJson.qm9NbreCochonCabrit12 = Convert.ToInt32(menage.Qm9NbreCochonCabrit12);
                menageJson.qm9NbreBeteVolaille13 = Convert.ToInt32(menage.Qm9NbreBeteVolaille13);
                menageJson.qm10AvoirPersDomestique = Convert.ToByte(menage.Qm10AvoirPersDomestique);
                menageJson.qm10TotalDomestiqueFille = Convert.ToByte(menage.Qm10TotalDomestiqueFille);
                menageJson.qm10TotalDomestiqueGarcon = Convert.ToByte(menage.Qm10TotalDomestiqueGarcon);
                menageJson.qm11TotalIndividuVivant = Convert.ToInt32(menage.Qm11TotalIndividuVivant);
                menageJson.qn1Emigration = Convert.ToByte(menage.Qn1Emigration);
                menageJson.qn1NbreEmigre = Convert.ToByte(menage.Qn1NbreEmigre);
                menageJson.qd1Deces = Convert.ToByte(menage.Qd1Deces);
                menageJson.qd1NbreDecede = Convert.ToByte(menage.Qd1NbreDecede);
                menageJson.statut = Convert.ToByte(menage.Statut);
                menageJson.isValidated = Convert.ToBoolean(menage.IsValidated);
                if (menage.DateDebutCollecte != null)
                {
                    menageJson.dateDebutCollecte = DateTime.ParseExact(menage.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                if (menage.DateFinCollecte != null)
                {
                    menageJson.dateFinCollecte = DateTime.ParseExact(menage.DateFinCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                menageJson.dureeSaisie = Convert.ToInt32(menage.DureeSaisie);
                menageJson.isFieldAllFilled = Convert.ToBoolean(menage.IsFieldAllFilled);
                menageJson.isContreEnqueteMade = Convert.ToBoolean(menage.IsContreEnqueteMade);
                menageJson.codeAgentRecenceur = menage.CodeAgentRecenceur;
                return menageJson;
            }
            return new MenageJson();
        }
        public static EmigreJson MapToJson(EmigreModel emigre)
        {
            if (emigre != null)
            {
                EmigreJson emigreJson = new EmigreJson();
                emigreJson.emigreId = Convert.ToInt32(emigre.EmigreId);
                emigreJson.menageId = Convert.ToInt32(emigre.MenageId);
                emigreJson.logeId = Convert.ToInt32(emigre.LogeId);
                emigreJson.batimentId = Convert.ToInt32(emigre.BatimentId);
                emigreJson.sdeId = Utilities.getSdeFormatSent(emigre.SdeId);
                emigreJson.qn1numeroOrdre = Convert.ToByte(emigre.Qn1numeroOrdre);
                emigreJson.qn2aNomComplet = emigre.Qn2aNomComplet;
                emigreJson.qn2bSexe = Convert.ToByte(emigre.Qn2bSexe);
                emigreJson.qn2cAgeAuMomentDepart = emigre.Qn2cAgeAuMomentDepart;
                emigreJson.qn2dVivantToujours = Convert.ToByte(emigre.Qn2dVivantToujours);
                emigreJson.qn2eDernierPaysResidence = Convert.ToByte(emigre.Qn2eDernierPaysResidence);
                emigreJson.statut = Convert.ToByte(emigre.Statut);
                emigreJson.isFieldAllFilled = Convert.ToBoolean(emigre.IsFieldAllFilled);
                if (emigre.DateDebutCollecte != null)
                {
                    emigreJson.dateDebutCollecte = DateTime.ParseExact(emigre.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                if (emigre.DateFinCollecte != null)
                {
                    emigreJson.dateFinCollecte = DateTime.ParseExact(emigre.DateFinCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                emigreJson.codeAgentRecenceur = emigre.CodeAgentRecenceur;
                return emigreJson;
            }
            return new EmigreJson();
        }
        public static DecesJson MapToJson(DecesModel deces)
        {
            if (deces != null)
            {
                DecesJson decesJson = new DecesJson();
                decesJson.decesId = Convert.ToInt32(deces.DecesId);
                decesJson.menageId = Convert.ToInt32(deces.MenageId);
                decesJson.logeId = Convert.ToInt32(deces.LogeId);
                decesJson.batimentId = Convert.ToInt32(deces.BatimentId);
                decesJson.sdeId = Utilities.getSdeFormatSent(deces.SdeId);
                decesJson.qd2NoOrdre = Convert.ToByte(deces.Qd2NoOrdre);
                decesJson.qd2aSexe = Convert.ToByte(deces.Qd2aSexe);
                decesJson.qd2bAgeDecede = deces.Qd2bAgeDecede;
                decesJson.qd2c1CirconstanceDeces = Convert.ToByte(deces.Qd2c1CirconstanceDeces);
                decesJson.qd2c2CauseDeces = Convert.ToByte(deces.Qd2c2CauseDeces);
                decesJson.statut = Convert.ToByte(deces.Statut);
                decesJson.isFieldAllFilled = Convert.ToBoolean(deces.IsFieldAllFilled);
                if (deces.DateDebutCollecte != null)
                {
                    decesJson.dateDebutCollecte = DateTime.ParseExact(deces.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                if (deces.DateFinCollecte != null)
                {
                    decesJson.dateFinCollecte = DateTime.ParseExact(deces.DateFinCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToString();
                }
                decesJson.dureeSaisie = Convert.ToInt32(deces.DureeSaisie);
                decesJson.isContreEnqueteMade = Convert.ToBoolean(deces.IsContreEnqueteMade);
                decesJson.codeAgentRecenceur = deces.CodeAgentRecenceur;
                return decesJson;
            }
            return new DecesJson();
        }
        public static IndividuJson MapToJson(IndividuModel individu)
        {
            if (individu != null)
            {
                IndividuJson individuJson = new IndividuJson();
                individuJson.individuId = Convert.ToInt32(individu.IndividuId);
                individuJson.menageId = Convert.ToInt32(individu.MenageId);
                individuJson.logeId = Convert.ToInt32(individu.LogeId);
                individuJson.batimentId = Convert.ToInt32(individu.BatimentId);
                individuJson.sdeId = Utilities.getSdeFormatSent(individu.SdeId);
                individuJson.q1NoOrdre = Convert.ToByte(individu.Q1NoOrdre);
                individuJson.qp2APrenom = individu.Qp2APrenom;
                individuJson.qp2BNom = individu.Qp2BNom;
                individuJson.qp3LienDeParente = Convert.ToByte(individu.Qp3LienDeParente);
                individuJson.qp3HabiteDansMenage = Convert.ToByte(individu.Qp3HabiteDansMenage);
                individuJson.qp4Sexe = Convert.ToByte(individu.Qp4Sexe);
                individuJson.qp5DateNaissanceJour = Convert.ToByte(individu.Qp5DateNaissanceJour);
                individuJson.qp5DateNaissanceMois = Convert.ToByte(individu.Qp5DateNaissanceMois);
                individuJson.qp5DateNaissanceAnnee = Convert.ToInt32(individu.Qp5DateNaissanceAnnee);
                individuJson.qp5bAge = Convert.ToByte(individu.Qp5bAge);
                individuJson.qp6religion = Convert.ToByte(individu.Qp6religion);
                individuJson.qp6AutreReligion = individu.Qp6AutreReligion;
                individuJson.qp7Nationalite = Convert.ToByte(individu.Qp7Nationalite);
                individuJson.qp7PaysNationalite = individu.Qp7PaysNationalite;
                individuJson.qp8MereEncoreVivante = Convert.ToByte(individu.Qp8MereEncoreVivante);
                individuJson.qp9EstPlusAge = Convert.ToByte(individu.Qp9EstPlusAge);
                individuJson.qp10LieuNaissance = Convert.ToByte(individu.Qp10LieuNaissance);
                individuJson.qp10CommuneNaissance = individu.Qp10CommuneNaissance;
                individuJson.qp10VqseNaissance = individu.Qp10VqseNaissance;
                individuJson.qp10PaysNaissance = individu.Qp10PaysNaissance;
                individuJson.qp11PeriodeResidence = Convert.ToByte(individu.Qp11PeriodeResidence);
                individuJson.qp12DomicileAvantRecensement = Convert.ToByte(individu.Qp12DomicileAvantRecensement);
                individuJson.qp12CommuneDomicileAvantRecensement = individu.Qp12CommuneDomicileAvantRecensement;
                individuJson.qp12VqseDomicileAvantRecensement = individu.Qp12VqseDomicileAvantRecensement;
                individuJson.qp12PaysDomicileAvantRecensement = individu.Qp12PaysDomicileAvantRecensement;
                individuJson.qe1EstAlphabetise = Convert.ToByte(individu.Qe1EstAlphabetise);
                individuJson.qe2FreqentationScolaireOuUniv = Convert.ToByte(individu.Qe2FreqentationScolaireOuUniv);
                individuJson.qe3typeEcoleOuUniv = Convert.ToByte(individu.Qe3typeEcoleOuUniv);
                individuJson.qe4aNiveauEtude = Convert.ToByte(individu.Qe4aNiveauEtude);
                individuJson.qe4bDerniereClasseOUAneEtude = individu.Qe4bDerniereClasseOUAneEtude;
                individuJson.qe5DiplomeUniversitaire = Convert.ToByte(individu.Qe5DiplomeUniversitaire);
                individuJson.qe6DomaineEtudeUniversitaire = individu.Qe6DomaineEtudeUniversitaire;
                individuJson.qaf1HandicapVoir = Convert.ToByte(individu.Qaf1HandicapVoir);
                individuJson.qaf2HandicapEntendre = Convert.ToByte(individu.Qaf2HandicapEntendre);
                individuJson.qaf3HandicapMarcher = Convert.ToByte(individu.Qaf3HandicapMarcher);
                individuJson.qaf4HandicapSouvenir = Convert.ToByte(individu.Qaf4HandicapSouvenir);
                individuJson.qaf5HandicapPourSeSoigner = Convert.ToByte(individu.Qaf5HandicapPourSeSoigner);
                individuJson.qaf6HandicapCommuniquer = Convert.ToByte(individu.Qaf6HandicapCommuniquer);
                individuJson.qt1PossessionTelCellulaire = Convert.ToByte(individu.Qt1PossessionTelCellulaire);
                individuJson.qt2UtilisationInternet = Convert.ToByte(individu.Qt2UtilisationInternet);
                individuJson.qem1DejaVivreAutrePays = Convert.ToByte(individu.Qem1DejaVivreAutrePays);
                individuJson.qem1AutrePays = individu.Qem1AutrePays;
                individuJson.qem2MoisRetour = Convert.ToByte(individu.Qem2MoisRetour);
                individuJson.qem2AnneeRetour = Convert.ToInt32(individu.Qem2AnneeRetour);
                individuJson.qsm1StatutMatrimonial = Convert.ToByte(individu.Qsm1StatutMatrimonial);
                individuJson.qa1ActEconomiqueDerniereSemaine = Convert.ToByte(individu.Qa1ActEconomiqueDerniereSemaine);
                individuJson.qa2ActAvoirDemele1 = Convert.ToByte(individu.Qa2ActAvoirDemele1);
                individuJson.qa2ActDomestique2 = Convert.ToByte(individu.Qa2ActDomestique2);
                individuJson.qa2ActCultivateur3 = Convert.ToByte(individu.Qa2ActCultivateur3);
                individuJson.qa2ActAiderParent4 = Convert.ToByte(individu.Qa2ActAiderParent4);
                individuJson.qa2ActAutre5 = Convert.ToByte(individu.Qa2ActAutre5);
                individuJson.qa3StatutEmploie = Convert.ToByte(individu.Qa3StatutEmploie);
                individuJson.qa4SecteurInstitutionnel = Convert.ToByte(individu.Qa4SecteurInstitutionnel);
                individuJson.qa5TypeBienProduitParEntreprise = individu.Qa5TypeBienProduitParEntreprise;
                individuJson.qa5PreciserTypeBienProduitParEntreprise = individu.Qa5PreciserTypeBienProduitParEntreprise;
                individuJson.qa6LieuActDerniereSemaine = Convert.ToByte(individu.Qa6LieuActDerniereSemaine);
                individuJson.qa7FoncTravail = Convert.ToByte(individu.Qa7FoncTravail);
                individuJson.qa8EntreprendreDemarcheTravail = Convert.ToByte(individu.Qa8EntreprendreDemarcheTravail);
                individuJson.qa9VouloirTravailler = Convert.ToByte(individu.Qa9VouloirTravailler);
                individuJson.qa10DisponibilitePourTravail = Convert.ToByte(individu.Qa10DisponibilitePourTravail);
                individuJson.qa11RecevoirTransfertArgent = Convert.ToByte(individu.Qa11RecevoirTransfertArgent);
                individuJson.qf1aNbreEnfantNeVivantM = Convert.ToInt32(individu.Qf1aNbreEnfantNeVivantM);
                individuJson.qf1bNbreEnfantNeVivantF = Convert.ToInt32(individu.Qf1bNbreEnfantNeVivantF);
                individuJson.qf2aNbreEnfantVivantM = Convert.ToInt32(individu.Qf2aNbreEnfantVivantM);
                individuJson.qf2bNbreEnfantVivantF = Convert.ToInt32(individu.Qf2bNbreEnfantVivantF);
                individuJson.qf3DernierEnfantJour = Convert.ToByte(individu.Qf3DernierEnfantJour);
                individuJson.qf3DernierEnfantMois = Convert.ToByte(individu.Qf3DernierEnfantMois);
                individuJson.qf3DernierEnfantAnnee = Convert.ToInt32(individu.Qf3DernierEnfantAnnee);
                individuJson.qf4DENeVivantVit = Convert.ToByte(individu.Qf4DENeVivantVit);
                individuJson.statut = Convert.ToByte(individu.Statut);
                individuJson.isFieldAllFilled = Convert.ToBoolean(individu.IsFieldAllFilled);
                if (individu.DateDebutCollecte != null)
                {
                    individuJson.dateDebutCollecte = DateTime.ParseExact(individu.DateDebutCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToUniversalTime().ToString();
                }
                if (individu.DateFinCollecte != null)
                {
                    individuJson.dateFinCollecte = DateTime.ParseExact(individu.DateFinCollecte, "ddd MMM dd HH:mm:ss EDT yyyy", null).ToUniversalTime().ToString();
                }
                individuJson.dureeSaisie = Convert.ToInt32(individu.DureeSaisie);
                individuJson.isContreEnqueteMade = Convert.ToBoolean(individu.IsContreEnqueteMade);
                individuJson.codeAgentRecenceur = individu.CodeAgentRecenceur;
                return individuJson;
            }
            return new IndividuJson();
        }
        public static RapportPersonnelJson MapToJson(RapportPersonnelModel rpt)
        {
            RapportPersonnelJson model = new RapportPersonnelJson();
            if (rpt != null)
            {
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
                model.reportSenderId = rpt.ReportSenderId;
                model.score = rpt.score;
                model.rapportId = rpt.rapportId;
                model.rapportName = "Rapport-" + rpt.rapportId;
            }
            return model;
        }
        public static ProblemeJson MapToJson(ProblemeModel probleme)
        {
            ProblemeJson model = new ProblemeJson();
            if (probleme != null)
            {
                model.questionnaireId = probleme.BatimentId;
                model.codeQuestion = probleme.CodeQuestion;
                model.problemeId = probleme.ProblemeId;
                model.domaine = probleme.Domaine;
                model.descProbleme = probleme.Nature;
                model.objet = probleme.Objet;
                model.sdeId = Utilities.getSdeFormatSent(probleme.SdeId);
                model.statut = probleme.Statut;
            }
            return model;
        }
        public static DetailsRapportJson MapToJson(DetailsRapportModel rpt )
        {
            DetailsRapportJson model = new DetailsRapportJson();
            if (rpt != null)
            {
                model.commentaire = rpt.Commentaire;
                model.detailsRapportId = rpt.DetailsRapportId;
                model.rapportId = rpt.RapportId;
                model.precisions = rpt.Precisions;
                model.probleme = rpt.Probleme;
                model.solution = rpt.Solution;
                model.domaine = rpt.Domaine;
                model.sousDomaine = rpt.SousDomaine;
                model.suggestions = rpt.Suggestions;
                model.suivi = rpt.Suivi;
            }
           return model;
        }
        public static RapportDeroulementJson MapToJson(RapportDeroulementModel rpt)
        {
            RapportDeroulementJson json = new RapportDeroulementJson();
            if (rpt != null)
            {
                json.rapportId = rpt.RapportId;
                json.rapportName = rpt.RapportName;
                json.dateRapport = rpt.DateRapport;
                json.districtId = rpt.CodeDistrict;
                json.rdcDetails = MapToListJson(rpt.RdcDetails);
            }
            return json;
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
        public static List<LogementCJson> MapToListLCJson(List<LogementModel> logements)
        {
            if (logements != null)
            {
                List<LogementCJson> lists = new List<LogementCJson>();
                foreach (LogementModel logement in logements)
                {
                    LogementCJson logementJson = ModelMapper.MapToLCJson(logement);
                    lists.Add(logementJson);
                }
                return lists;
            }
            return new List<LogementCJson>();
        }
        public static List<LogementIsJson> MapToListLIJson(List<LogementModel> logements)
        {
            if (logements != null)
            {
                List<LogementIsJson> lists = new List<LogementIsJson>();
                foreach (LogementModel logement in logements)
                {
                    LogementIsJson logementJson = ModelMapper.MapToLIJson(logement);
                    lists.Add(logementJson);
                }
                return lists;
            }
            return new List<LogementIsJson>();
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
        public static List<ProblemeJson> MapToListJson(List<ProblemeModel> problemes)
        {
            if (problemes != null)
            {
                List<ProblemeJson> lists = new List<ProblemeJson>();
                foreach (ProblemeModel prob in problemes)
                {
                    ProblemeJson json = ModelMapper.MapToJson(prob);
                    lists.Add(json);
                }
                return lists;
            }
            return new List<ProblemeJson>();
        }
        public static List<DetailsRapportJson> MapToListJson(List<DetailsRapportModel> details)
        {
            if (details != null)
            {
                List<DetailsRapportJson> lists = new List<DetailsRapportJson>();
                foreach (DetailsRapportModel dt in details)
                {
                    DetailsRapportJson json = ModelMapper.MapToJson(dt);
                    lists.Add(json);
                }
                return lists;
            }
            return new List<DetailsRapportJson>();
        }
        public static List<RapportDeroulementJson> MapToListJson(List<RapportDeroulementModel> rapports)
        {
            if (rapports != null)
            {
                List<RapportDeroulementJson> lists = new List<RapportDeroulementJson>();
                foreach(RapportDeroulementModel rpt in rapports)
                {
                    RapportDeroulementJson json = ModelMapper.MapToJson(rpt);
                    lists.Add(json);
                }
                return lists;
            }
            return new List<RapportDeroulementJson>();
        }
        public static List<RapportPersonnelJson> MapToListJson(List<RapportPersonnelModel> rapports)
        {
            if (rapports != null)
            {
                List<RapportPersonnelJson> lists = new List<RapportPersonnelJson>();
                foreach (RapportPersonnelModel rpt in rapports)
                {
                    RapportPersonnelJson json = ModelMapper.MapToJson(rpt);
                    lists.Add(json);
                }
                return lists;
            }
            return new List<RapportPersonnelJson>();
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
                    DistrictId = batiment.disctrictId,
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
                    QlcTotalIndividus = Convert.ToByte(logement.qlcTotalIndividus),
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
                batModel.Id = bat.Id;
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
            batToSave.Id = bat.Id;
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

        public static List<BatimentCEModel> MapToListBatimentCEModel(List<Tbl_BatimentCE> batiments)
        {
            List<BatimentCEModel> bats = new List<BatimentCEModel>();
            if (batiments.Count != 0)
            {
                foreach (Tbl_BatimentCE bat in batiments)
                {
                    BatimentCEModel bati = MapToBatimentCEModel(bat);
                    bats.Add(bati);
                }
            }
            return bats;
        }

        public static LogementCEModel MapToLogementCEModel(Tbl_LogementCE log)
        {
            LogementCEModel logement = new LogementCEModel();
            logement.Id = log.Id;
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
            logement.Id = log.Id;
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
            if (_men != null)
            {
                menage.Id = _men.Id;
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
            }
            
            return menage;
        }
        public static Tbl_MenageCE MapToTbl_MenageCE(MenageCEModel _men)
        {
            Tbl_MenageCE menage = new Tbl_MenageCE();
            if (_men != null)
            {
                menage.Id = _men.Id;
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
            }
            
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
            if (_ind != null)
            {
                individu.Id = _ind.Id;
                individu.BatimentId = _ind.BatimentId.GetValueOrDefault();
                individu.LogeId = _ind.LogeId.GetValueOrDefault();
                individu.MenageId = _ind.MenageId.GetValueOrDefault();
                individu.IndividuId = _ind.IndividuId;
                individu.SdeId = _ind.SdeId;
                individu.Qp1NoOrdre = _ind.Qp1NoOrdre;
                individu.Q3LienDeParente = Convert.ToByte(_ind.Q3LienDeParente.GetValueOrDefault());
                individu.Q3aRaisonChefMenage = Convert.ToByte(_ind.Q3aRaisonChefMenage.GetValueOrDefault());
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
            }
            
            return individu;
        }

        public static Tbl_IndividusCE MapToTbl_IndividuCE(IndividuCEModel _ind)
        {
            Tbl_IndividusCE individu = new Tbl_IndividusCE();
            if (_ind != null)
            {
                individu.Id = _ind.Id;
                individu.BatimentId = _ind.BatimentId;
                individu.LogeId = _ind.LogeId;
                individu.MenageId = _ind.MenageId;
                individu.Qp1NoOrdre = _ind.Qp1NoOrdre;
                individu.SdeId = _ind.SdeId;
                individu.IndividuId = _ind.IndividuId;
                individu.Q3LienDeParente = Convert.ToByte(_ind.Q3LienDeParente.GetValueOrDefault());
                individu.Q3aRaisonChefMenage = Convert.ToByte(_ind.Q3aRaisonChefMenage.GetValueOrDefault());
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
            }
            
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
            if (_deces != null)
            {
                _dec.Id = _deces.Id;
                _dec.MenageId = _deces.MenageId.GetValueOrDefault();
                _dec.LogeId = _deces.LogeId.GetValueOrDefault();
                _dec.BatimentId = _deces.BatimentId.GetValueOrDefault();
                _dec.SdeId = _deces.SdeId;
                _dec.DecesId = _deces.DecesId;
                _dec.Qd2NoOrdre = Convert.ToByte(_deces.Qd2NoOrdre.GetValueOrDefault());
                _dec.Qd1Deces = Convert.ToByte(_deces.Qd1Deces.GetValueOrDefault());
                _dec.Qd1aNbreDecesF = Convert.ToByte(_deces.Qd1aNbreDecesF.GetValueOrDefault());
                _dec.Qd1aNbreDecesG = Convert.ToByte(_deces.Qd1aNbreDecesG.GetValueOrDefault());
                _dec.DureeSaisie = Convert.ToByte(_deces.DureeSaisie.GetValueOrDefault());
                _dec.IsContreEnqueteMade = _deces.IsContreEnqueteMade.GetValueOrDefault();
                _dec.IsValidated = _deces.IsValidated.GetValueOrDefault();
            }
           
            return _dec;
        }
        public static Tbl_DecesCE MapToTbl_DecesCE(DecesCEModel _deces)
        {
            Tbl_DecesCE _dec = new Tbl_DecesCE();
            if (_deces!=null)
            {
                _dec.Id = _deces.Id;
                _dec.MenageId = _deces.MenageId;
                _dec.LogeId = _deces.LogeId;
                _dec.BatimentId = _deces.BatimentId;
                _dec.SdeId = _deces.SdeId;
                _dec.DecesId = _deces.DecesId;
                _dec.Qd2NoOrdre = Convert.ToByte(_deces.Qd2NoOrdre.GetValueOrDefault());
                _dec.Qd1Deces = Convert.ToByte(_deces.Qd1Deces.GetValueOrDefault());
                _dec.Qd1aNbreDecesF = Convert.ToByte(_deces.Qd1aNbreDecesF.GetValueOrDefault());
                _dec.Qd1aNbreDecesG = Convert.ToByte(_deces.Qd1aNbreDecesG.GetValueOrDefault());
                _dec.DureeSaisie = Convert.ToByte(_deces.DureeSaisie.GetValueOrDefault());
                _dec.IsContreEnqueteMade = _deces.IsContreEnqueteMade.GetValueOrDefault();
                _dec.IsValidated = _deces.IsValidated.GetValueOrDefault();
            }
            
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

        public static Tbl_EmigreCE MapToTbl_EmigreCE(EmigreCEModel _emigre)
        {
            Tbl_EmigreCE _em = new Tbl_EmigreCE();
            if (_emigre != null)
            {
                _em.Id = _emigre.Id;
                _em.BatimentId = _emigre.BatimentId;
                _em.LogeId = _emigre.LogeId;
                _em.MenageId = _emigre.MenageId;
                _em.EmigreId = _emigre.EmigreId;
                _em.DateDebutCollecte = _emigre.DateDebutCollecte;
                _em.DateFinCollecte = _emigre.DateFinCollecte;
                _em.IsContreEnqueteMade = _emigre.IsContreEnqueteMade;
                _em.IsValidated = _emigre.IsValidated;
                _em.Qn1NbreEmigreF = _emigre.Qn1NbreEmigreF;
                _em.Qn1Emigration = _emigre.Qn1Emigration;
                _emigre.Qn1numeroOrdre = _emigre.Qn1numeroOrdre;
                _em.SdeId = _emigre.SdeId;
            }
            return _em;
        }

        public static EmigreCEModel MapToEmigreCEModel(Tbl_EmigreCE _emigre)
        {
            EmigreCEModel _em = new EmigreCEModel();
            if (_emigre != null)
            {
                _em.Id = _emigre.Id;
                _em.BatimentId = _emigre.BatimentId;
                _em.LogeId = _emigre.LogeId;
                _em.MenageId = _emigre.MenageId;
                _em.EmigreId = _emigre.EmigreId;
                _em.DateDebutCollecte = _emigre.DateDebutCollecte;
                _em.DateFinCollecte = _emigre.DateFinCollecte;
                _em.IsContreEnqueteMade = _emigre.IsContreEnqueteMade;
                _em.IsValidated = _emigre.IsValidated;
                _em.Qn1NbreEmigreF = _emigre.Qn1NbreEmigreF;
                _em.Qn1Emigration = _emigre.Qn1Emigration;
                _emigre.Qn1numeroOrdre = _emigre.Qn1numeroOrdre;
                _em.SdeId = _emigre.SdeId;
            }
            return _em;
        }
        public static List<EmigreCEModel> MapToListEmigreCEModel(List<Tbl_EmigreCE> list)
        {
            List<EmigreCEModel> listOfEmigres = new List<EmigreCEModel>();
            foreach (Tbl_EmigreCE _em in list)
            {
                EmigreCEModel emigre = MapToEmigreCEModel(_em);
                listOfEmigres.Add(emigre);
            }
            return listOfEmigres;
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

        #region MATERIELS
        public static MaterielModel MapToMateriel(Tbl_Materiels mat)
        {
            IConfigurationService service = new ConfigurationService();
            MaterielModel materiel = new MaterielModel();
            if (mat.MaterielId!=0)
            {
                materiel.Model = mat.Model;
                materiel.MaterielId = mat.MaterielId;
                materiel.Serial = mat.Serial;
                materiel.Synchronisation = mat.LastSynchronisation;
                materiel.DateAssignation = mat.DateAssignation;
                materiel.Agent = mat.AgentId.GetValueOrDefault().ToString();
                materiel.Configure = mat.IsConfigured.GetValueOrDefault().ToString();
                materiel.Version = mat.Version;
                materiel.Imei = mat.Imei;
                materiel.Agent = service.findAgentSderById(mat.AgentId.GetValueOrDefault()).AgentName;
                if (mat.IsConfigured == 1)
                {
                    materiel.Configure = "OUI";
                }
                else
                {
                    materiel.Configure = "NON";
                }
            }
            return materiel;
        }
        public static List<MaterielModel> MapToList(List<Tbl_Materiels> materiels)
        {
            List<MaterielModel> listOf=new List<MaterielModel>();
            if (materiels != null)
            {
                foreach (Tbl_Materiels materiel in materiels)
                {
                    MaterielModel mat = MapToMateriel(materiel);
                   
                    listOf.Add(mat);
                }
            }
            return listOf;
        }
        #endregion

    }
}
