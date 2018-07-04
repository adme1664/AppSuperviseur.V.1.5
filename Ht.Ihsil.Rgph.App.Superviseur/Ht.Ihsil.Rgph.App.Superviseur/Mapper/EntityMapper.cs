using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
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
        public static tbl_batiment mapTo(BatimentModel batiment)
        {
            tbl_batiment entity = new tbl_batiment();
            if (batiment != null)
            {
                entity.batimentId = Convert.ToInt32(batiment.BatimentId);
                entity.deptId = batiment.DeptId;
                entity.comId = batiment.ComId;
                entity.vqseId = batiment.VqseId;
                entity.sdeId = batiment.SdeId;
                entity.zone = Convert.ToByte(batiment.Zone);
                entity.disctrictId = batiment.DistrictId;
                entity.qhabitation = batiment.Qhabitation;
                entity.qlocalite = batiment.Qlocalite;
                entity.qadresse = batiment.Qadresse;
                entity.qrec = batiment.Qrec;
                entity.qrgph = batiment.Qrgph;
                entity.qb1Etat = Convert.ToByte(batiment.Qb1Etat);
                entity.qb2Type = Convert.ToByte(batiment.Qb2Type);
                entity.qb3NombreEtage = Convert.ToByte(batiment.Qb3NombreEtage);
                entity.qb4MateriauMur = Convert.ToByte(batiment.Qb4MateriauMur);
                entity.qb5MateriauToit = Convert.ToByte(batiment.Qb5MateriauToit);
                entity.qb6StatutOccupation = Convert.ToByte(batiment.Qb6StatutOccupation);
                entity.qb7Utilisation1 = Convert.ToByte(batiment.Qb7Utilisation1);
                entity.qb7Utilisation2 = Convert.ToByte(batiment.Qb7Utilisation2);
                entity.qb8NbreLogeCollectif = Convert.ToByte(batiment.Qb8NbreLogeCollectif);
                entity.qb8NbreLogeIndividuel = Convert.ToByte(batiment.Qb8NbreLogeIndividuel);
                entity.isFieldAllFilled = Convert.ToInt32(batiment.IsFieldAllFilled);
                entity.statut = Convert.ToByte(batiment.Statut);
                //entity.isValidated = Convert.ToInt32(batiment.IsValidated);
                entity.dateDebutCollecte = batiment.DateDebutCollecte;
                entity.dateFinCollecte = batiment.DateFinCollecte;
                entity.dateEnvoi = entity.dateEnvoi;
                entity.dureeSaisie = Convert.ToInt32(batiment.DureeSaisie);
                //entity.isContreEnqueteMade = Convert.ToInt32(batiment.IsContreEnqueteMade);
                //entity.isVerified = batiment.IsVerified;
            }

            return entity;
        }
        public static tbl_logement mapTo(LogementModel logement)
        {
            tbl_logement entity = new tbl_logement();
            if (logement != null)
            {
                entity.logeId = Convert.ToInt32(logement.LogeId);
                entity.batimentId = Convert.ToInt32(logement.BatimentId);
                entity.sdeId = logement.SdeId;
                entity.qlCategLogement = Convert.ToByte(logement.QlCategLogement);
                entity.qlin1NumeroOrdre = Convert.ToByte(logement.Qlin1NumeroOrdre);
                entity.qlc1TypeLogement = Convert.ToByte(logement.Qlc1TypeLogement);
                entity.qlc2bTotalGarcon = Convert.ToByte(logement.Qlc2bTotalGarcon);
                entity.qlc2bTotalFille = Convert.ToByte(logement.Qlc2bTotalFille);
                entity.qlcTotalIndividus = Convert.ToByte(logement.QlcTotalIndividus);
                entity.qlin2StatutOccupation = Convert.ToByte(logement.Qlin2StatutOccupation);
                entity.qlin3ExistenceLogement = Convert.ToByte(logement.Qlin3ExistenceLogement);
                entity.qlin4TypeLogement = Convert.ToByte(logement.Qlin4TypeLogement);
                entity.qlin5MateriauSol = Convert.ToByte(logement.Qlin5MateriauSol);
                entity.qlin6NombrePiece = Convert.ToByte(logement.Qlin6NombrePiece);
                entity.qlin7NbreChambreACoucher = Convert.ToByte(logement.Qlin7NbreChambreACoucher);
                entity.qlin8NbreIndividuDepense = Convert.ToByte(logement.Qlin8NbreIndividuDepense);
                entity.qlin9NbreTotalMenage = Convert.ToByte(logement.Qlin9NbreTotalMenage);
                entity.statut = Convert.ToByte(logement.Statut);
                //entity.isValidated = Convert.ToInt32(logement.IsValidated);
                entity.dateDebutCollecte = logement.DateDebutCollecte;
                entity.dateFinCollecte = logement.DateFinCollecte;
                entity.dureeSaisie = Convert.ToInt32(logement.DureeSaisie);
                entity.isFieldAllFilled = Convert.ToInt32(logement.IsFieldAllFilled);
                entity.isContreEnqueteMade = Convert.ToInt32(logement.IsContreEnqueteMade);
                entity.nbrTentative = Convert.ToByte(logement.NbrTentative);
                entity.codeAgentRecenceur = logement.CodeAgentRecenceur;
                //entity.isVerified = Convert.ToInt32(logement.IsVerified);
            }
            return entity;
        }
        public static tbl_menage mapTo(MenageModel menage)
        {
            tbl_menage entity = new tbl_menage();
            if (menage != null)
            {
                entity.batimentId = Convert.ToInt32(menage.BatimentId);
                entity.logeId = Convert.ToInt32(menage.LogeId);
                entity.menageId = Convert.ToInt32(menage.MenageId);
                entity.sdeId = menage.SdeId;
                entity.qm1NoOrdre = Convert.ToByte(menage.Qm1NoOrdre);
                entity.qm2ModeJouissance = Convert.ToByte(menage.Qm2ModeJouissance);
                entity.qm3ModeObtentionLoge = Convert.ToByte(menage.Qm3ModeObtentionLoge);
                entity.qm4_1ModeAprovEauABoire = Convert.ToByte(menage.Qm4_1ModeAprovEauABoire);
                entity.qm4_2ModeAprovEauAUsageCourant = Convert.ToByte(menage.Qm4_2ModeAprovEauAUsageCourant);
                entity.qm5SrcEnergieCuisson1 = Convert.ToByte(menage.Qm5SrcEnergieCuisson1);
                entity.qm5SrcEnergieCuisson2 = Convert.ToByte(menage.Qm5SrcEnergieCuisson2);
                entity.qm6TypeEclairage = Convert.ToByte(menage.Qm6TypeEclairage);
                entity.qm7ModeEvacDechet = Convert.ToByte(menage.Qm7ModeEvacDechet);
                entity.qm8EndroitBesoinPhysiologique = Convert.ToByte(menage.Qm8EndroitBesoinPhysiologique);
                entity.qm9NbreRadio1 = Convert.ToInt32(menage.Qm9NbreRadio1);
                entity.qm9NbreTelevision2 = Convert.ToInt32(menage.Qm9NbreTelevision2);
                entity.qm9NbreRefrigerateur3 = Convert.ToInt32(menage.Qm9NbreRefrigerateur3);
                entity.qm9NbreFouElectrique4 = Convert.ToInt32(menage.Qm9NbreFouElectrique4);
                entity.qm9NbreOrdinateur5 = Convert.ToInt32(menage.Qm9NbreOrdinateur5);
                entity.qm9NbreMotoBicyclette6 = Convert.ToInt32(menage.Qm9NbreMotoBicyclette6);
                entity.qm9NbreVoitureMachine7 = Convert.ToInt32(menage.Qm9NbreVoitureMachine7);
                entity.qm9NbreBateau8 = Convert.ToInt32(menage.Qm9NbreBateau8);
                entity.qm9NbrePanneauGeneratrice9 = Convert.ToInt32(menage.Qm9NbrePanneauGeneratrice9);
                entity.qm9NbreMilletChevalBourique10 = Convert.ToInt32(menage.Qm9NbreMilletChevalBourique10);
                entity.qm9NbreBoeufVache11 = Convert.ToInt32(menage.Qm9NbreBoeufVache11);
                entity.qm9NbreCochonCabrit12 = Convert.ToInt32(menage.Qm9NbreCochonCabrit12);
                entity.qm9NbreBeteVolaille13 = Convert.ToInt32(menage.Qm9NbreBeteVolaille13);
                entity.qm10AvoirPersDomestique = Convert.ToByte(menage.Qm10AvoirPersDomestique);
                entity.qm10TotalDomestiqueFille = Convert.ToByte(menage.Qm10TotalDomestiqueFille);
                entity.qm10TotalDomestiqueGarcon = Convert.ToByte(menage.Qm10TotalDomestiqueGarcon);
                entity.qm11TotalIndividuVivant = Convert.ToInt32(menage.Qm11TotalIndividuVivant);
                entity.qn1Emigration = Convert.ToByte(menage.Qn1Emigration);
                entity.qn1NbreEmigre = Convert.ToByte(menage.Qn1NbreEmigre);
                entity.qd1Deces = Convert.ToByte(menage.Qd1Deces);
                entity.qd1NbreDecede = Convert.ToByte(menage.Qd1NbreDecede);
                entity.statut = Convert.ToByte(menage.Statut);
                //entity.isValidated = Convert.ToInt32(menage.IsValidated);
                entity.dateDebutCollecte = menage.DateDebutCollecte;
                entity.dateFinCollecte = menage.DateFinCollecte;
                entity.dureeSaisie = Convert.ToInt32(menage.DureeSaisie);
                entity.isFieldAllFilled = Convert.ToInt32(menage.IsFieldAllFilled);
                //entity.isContreEnqueteMade = Convert.ToInt32(menage.IsContreEnqueteMade);
                entity.codeAgentRecenceur = menage.CodeAgentRecenceur;
                //entity.isVerified = menage.IsVerified;
            }
            return entity;
        }
        public static tbl_emigre mapTo(EmigreModel emigre)
        {
            tbl_emigre entity = new tbl_emigre();
            if (emigre != null)
            {
                entity.batimentId = emigre.BatimentId;
                entity.logeId = emigre.LogeId;
                entity.menageId=emigre.MenageId;
                entity.emigreId = emigre.EmigreId;
                entity.sdeId = emigre.SdeId;
                entity.qn1numeroOrdre = Convert.ToByte(emigre.Qn1numeroOrdre);
                entity.qn2aNomComplet = emigre.Qn2aNomComplet;
                entity.qn2bSexe = Convert.ToByte(emigre.Qn2bSexe);
                entity.qn2cAgeAuMomentDepart = emigre.Qn2cAgeAuMomentDepart;
                entity.qn2dVivantToujours = Convert.ToByte(emigre.Qn2dVivantToujours);
                entity.qn2eDernierPaysResidence = Convert.ToByte(emigre.Qn2eDernierPaysResidence);
                entity.statut = Convert.ToByte(emigre.Statut);
                entity.isFieldAllFilled = Convert.ToInt32(emigre.IsFieldAllFilled);
                entity.dateDebutCollecte = emigre.DateDebutCollecte;
                entity.dateFinCollecte = emigre.DateFinCollecte;
                entity.dureeSaisie = Convert.ToInt32(emigre.DureeSaisie);
                entity.codeAgentRecenceur = emigre.CodeAgentRecenceur;
                //entity.isVerified = emigre.IsVerified;
            }
            return entity;
        }
        public static tbl_deces mapTo(DecesModel deces)
        {
            tbl_deces entity = new tbl_deces();
            if (deces != null)
            {
                entity.decesId = Convert.ToInt32(deces.DecesId);
                entity.menageId = Convert.ToInt32(deces.MenageId);
                entity.logeId = Convert.ToInt32(deces.LogeId);
                entity.batimentId = Convert.ToInt32(deces.BatimentId);
                entity.sdeId = deces.SdeId;
                entity.qd2NoOrdre = Convert.ToByte(deces.Qd2NoOrdre);
                entity.qd2aSexe = Convert.ToByte(deces.Qd2aSexe);
                entity.qd2bAgeDecede = deces.Qd2bAgeDecede;
                entity.qd2c1CirconstanceDeces = Convert.ToByte(deces.Qd2c1CirconstanceDeces);
                entity.qd2c2CauseDeces = Convert.ToByte(deces.Qd2c2CauseDeces);
                entity.statut = Convert.ToByte(deces.Statut);
                entity.isFieldAllFilled = Convert.ToInt32(deces.IsFieldAllFilled);
                entity.dateDebutCollecte = deces.DateDebutCollecte;
                entity.dateFinCollecte = deces.DateFinCollecte;
                entity.dureeSaisie = Convert.ToInt32(deces.DureeSaisie);
                //entity.isContreEnqueteMade = Convert.ToInt32(deces.IsContreEnqueteMade);
                entity.codeAgentRecenceur = deces.CodeAgentRecenceur;
                //entity.isVerified = deces.IsVerified;
            }
            return entity;
        }
        public static tbl_individu mapTo(IndividuModel individu)
        {
            tbl_individu entity = new tbl_individu();
            if (individu != null)
            {
                entity.individuId = Convert.ToInt32(individu.IndividuId);
                entity.menageId = Convert.ToInt32(individu.MenageId);
                entity.logeId = Convert.ToInt32(individu.LogeId);
                entity.batimentId = Convert.ToInt32(individu.BatimentId);
                entity.sdeId = individu.SdeId;
                entity.q1NoOrdre = Convert.ToByte(individu.Q1NoOrdre);
                entity.qp2APrenom = individu.Qp2APrenom;
                entity.qp2BNom = individu.Qp2BNom;
                entity.qp3LienDeParente = Convert.ToByte(individu.Qp3LienDeParente);
                entity.qp3HabiteDansMenage = Convert.ToByte(individu.Qp3HabiteDansMenage);
                entity.qp4Sexe = Convert.ToByte(individu.Qp4Sexe);
                entity.qp5DateNaissanceJour = Convert.ToByte(individu.Qp5DateNaissanceJour);
                entity.qp5DateNaissanceMois = Convert.ToByte(individu.Qp5DateNaissanceMois);
                entity.Qp5DateNaissanceAnnee = Convert.ToInt32(individu.Qp5DateNaissanceAnnee);
                entity.qp5bAge = Convert.ToByte(individu.Qp5bAge);
                entity.qp6religion = Convert.ToByte(individu.Qp6religion);
                entity.qp6AutreReligion = individu.Qp6AutreReligion;
                entity.qp7Nationalite = Convert.ToByte(individu.Qp7Nationalite);
                entity.qp7PaysNationalite = individu.Qp7PaysNationalite;
                entity.qp8MereEncoreVivante = Convert.ToByte(individu.Qp8MereEncoreVivante);
                entity.qp9EstPlusAge = Convert.ToByte(individu.Qp9EstPlusAge);
                entity.qp10LieuNaissance = Convert.ToByte(individu.Qp10LieuNaissance);
                entity.qp10CommuneNaissance = individu.Qp10CommuneNaissance;
                entity.qp10VqseNaissance = individu.Qp10VqseNaissance;
                entity.qp10PaysNaissance = individu.Qp10PaysNaissance;
                entity.qp11PeriodeResidence = Convert.ToByte(individu.Qp11PeriodeResidence);
                entity.qp12DomicileAvantRecensement = Convert.ToByte(individu.Qp12DomicileAvantRecensement);
                entity.qp12CommuneDomicileAvantRecensement = individu.Qp12CommuneDomicileAvantRecensement;
                entity.qp12VqseDomicileAvantRecensement = individu.Qp12VqseDomicileAvantRecensement;
                entity.qp12PaysDomicileAvantRecensement = individu.Qp12PaysDomicileAvantRecensement;
                entity.qe1EstAlphabetise = Convert.ToByte(individu.Qe1EstAlphabetise);
                entity.qe2FreqentationScolaireOuUniv = Convert.ToByte(individu.Qe2FreqentationScolaireOuUniv);
                entity.qe3typeEcoleOuUniv = Convert.ToByte(individu.Qe3typeEcoleOuUniv);
                entity.qe4aNiveauEtude = Convert.ToByte(individu.Qe4aNiveauEtude);
                entity.qe4bDerniereClasseOUAneEtude = individu.Qe4bDerniereClasseOUAneEtude;
                entity.qe5DiplomeUniversitaire = Convert.ToByte(individu.Qe5DiplomeUniversitaire);
                entity.qe6DomaineEtudeUniversitaire = individu.Qe6DomaineEtudeUniversitaire;
                entity.qaf1HandicapVoir = Convert.ToByte(individu.Qaf1HandicapVoir);
                entity.qaf2HandicapEntendre = Convert.ToByte(individu.Qaf2HandicapEntendre);
                entity.qaf3HandicapMarcher = Convert.ToByte(individu.Qaf3HandicapMarcher);
                entity.qaf4HandicapSouvenir = Convert.ToByte(individu.Qaf4HandicapSouvenir);
                entity.qaf5HandicapPourSeSoigner = Convert.ToByte(individu.Qaf5HandicapPourSeSoigner);
                entity.qaf6HandicapCommuniquer = Convert.ToByte(individu.Qaf6HandicapCommuniquer);
                entity.qt1PossessionTelCellulaire = Convert.ToByte(individu.Qt1PossessionTelCellulaire);
                entity.qt2UtilisationInternet = Convert.ToByte(individu.Qt2UtilisationInternet);
                entity.qem1DejaVivreAutrePays = Convert.ToByte(individu.Qem1DejaVivreAutrePays);
                entity.qem1AutrePays = individu.Qem1AutrePays;
                entity.qem2MoisRetour = Convert.ToByte(individu.Qem2MoisRetour);
                entity.qem2AnneeRetour = Convert.ToInt32(individu.Qem2AnneeRetour);
                entity.qsm1StatutMatrimonial = Convert.ToByte(individu.Qsm1StatutMatrimonial);
                entity.qa1ActEconomiqueDerniereSemaine = Convert.ToByte(individu.Qa1ActEconomiqueDerniereSemaine);
                entity.qa2ActAvoirDemele1 = Convert.ToByte(individu.Qa2ActAvoirDemele1);
                entity.qa2ActDomestique2 = Convert.ToByte(individu.Qa2ActDomestique2);
                entity.qa2ActCultivateur3 = Convert.ToByte(individu.Qa2ActCultivateur3);
                entity.qa2ActAiderParent4 = Convert.ToByte(individu.Qa2ActAiderParent4);
                entity.qa2ActAutre5 = Convert.ToByte(individu.Qa2ActAutre5);
                entity.qa3StatutEmploie = Convert.ToByte(individu.Qa3StatutEmploie);
                entity.qa4SecteurInstitutionnel = Convert.ToByte(individu.Qa4SecteurInstitutionnel);
                entity.qa5TypeBienProduitParEntreprise = individu.Qa5TypeBienProduitParEntreprise;
                entity.qa5PreciserTypeBienProduitParEntreprise = individu.Qa5PreciserTypeBienProduitParEntreprise;
                entity.qa6LieuActDerniereSemaine = Convert.ToByte(individu.Qa6LieuActDerniereSemaine);
                entity.qa7FoncTravail = Convert.ToByte(individu.Qa7FoncTravail);
                entity.qa8EntreprendreDemarcheTravail = Convert.ToByte(individu.Qa8EntreprendreDemarcheTravail);
                entity.qa9VouloirTravailler = Convert.ToByte(individu.Qa9VouloirTravailler);
                entity.qa10DisponibilitePourTravail = Convert.ToByte(individu.Qa10DisponibilitePourTravail);
                entity.qa11RecevoirTransfertArgent = Convert.ToByte(individu.Qa11RecevoirTransfertArgent);
                entity.qf1aNbreEnfantNeVivantM = Convert.ToInt32(individu.Qf1aNbreEnfantNeVivantM);
                entity.qf1bNbreEnfantNeVivantF = Convert.ToInt32(individu.Qf1bNbreEnfantNeVivantF);
                entity.qf2aNbreEnfantVivantM = Convert.ToInt32(individu.Qf2aNbreEnfantVivantM);
                entity.qf2bNbreEnfantVivantF = Convert.ToInt32(individu.Qf2bNbreEnfantVivantF);
                entity.qf3DernierEnfantJour = Convert.ToByte(individu.Qf3DernierEnfantJour);
                entity.qf3DernierEnfantMois = Convert.ToByte(individu.Qf3DernierEnfantMois);
                entity.qf3DernierEnfantAnnee = Convert.ToInt32(individu.Qf3DernierEnfantAnnee);
                entity.qf4DENeVivantVit = Convert.ToByte(individu.Qf4DENeVivantVit);
                entity.statut = Convert.ToByte(individu.Statut);
                entity.isFieldAllFilled = Convert.ToInt32(individu.IsFieldAllFilled);
                entity.dateDebutCollecte = individu.DateDebutCollecte;
                entity.dateFinCollecte = individu.DateFinCollecte;
                entity.dureeSaisie = Convert.ToInt32(individu.DureeSaisie);
                //entity.isContreEnqueteMade = Convert.ToInt32(individu.IsContreEnqueteMade);
                entity.codeAgentRecenceur = individu.CodeAgentRecenceur;
                //entity.isVerified = individu.IsVerified;
            }
            return entity;
        }
    }
}
