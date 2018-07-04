using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.SchemaTest;
using Ht.Ihsil.Rgph.App.Superviseur.Constants;
using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;

namespace Ht.Ihsil.Rgph.App.Superviseur.Mapper
{
    public class WsModelMapper
    {
        static Logger log = new Logger();
       
        #region MAPPING MOBILE TYPE IN XSD MOBILE TYPE 
        /// <summary>
        /// Mapper un reader Batiment en BatimentType pour le service
        /// </summary>
        /// <param Name="reader"></param>
        /// <returns></returns>
        public static BatimentType MapReaderToBatimentType(tbl_batiment batiment)
        {
            return new BatimentType
            {
                batimentId = Convert.ToInt32(batiment.batimentId),
                deptId = batiment.deptId,
                comId = batiment.comId,
                vqseId = batiment.vqseId,
                sdeId="0111-01-001",
                //sdeId = batiment.sdeId,
                //sectionEnumerationId = batiment.sectionEnumerationId,
                //zone = Convert.ToByte(batiment.zone),
                disctrictId = batiment.disctrictId,
                qhabitation = batiment.qhabitation,
                qlocalite = batiment.qlocalite,
                qadresse = batiment.qadresse,
                qrec = batiment.qrec,
                qrgph = batiment.qrgph,
                qb1Etat = Convert.ToByte(batiment.qb1Etat),
                qb2Type = Convert.ToByte(batiment.qb2Type),
                qb3NombreEtage = Convert.ToByte(batiment.qb3NombreEtage),
                qb5MateriauToit = Convert.ToByte(batiment.qb5MateriauToit),
                qb4MateriauMur = Convert.ToByte(batiment.qb4MateriauMur),
                qb6StatutOccupation = Convert.ToByte(batiment.qb6StatutOccupation),
                qb7Utilisation1 = Convert.ToByte(batiment.qb7Utilisation1),
                qb7Utilisation2 = Convert.ToByte(batiment.qb7Utilisation2),
                qb8NbreLogeCollectif = Convert.ToByte(batiment.qb8NbreLogeCollectif),
                qb8NbreLogeIndividuel = Convert.ToByte(batiment.qb8NbreLogeIndividuel),
                statut = Convert.ToByte(batiment.statut),
                dateEnvoi = batiment.dateEnvoi,
                isValidated = Convert.ToBoolean(batiment.isValidated),
                isSynchroToAppSup = Convert.ToBoolean(batiment.isSynchroToAppSup),
                isSynchroToCentrale = Convert.ToBoolean(batiment.isSynchroToCentrale),
                dateDebutCollecte = batiment.dateDebutCollecte,
                dateFinCollecte = batiment.dateFinCollecte,
                dureeSaisie = Convert.ToInt32(batiment.dureeSaisie),
                isFieldAllFilled = Convert.ToBoolean(batiment.isFieldAllFilled),
                isContreEnqueteMade = Convert.ToBoolean(batiment.isContreEnqueteMade),
                latitude = batiment.latitude,
                longitude = batiment.longitude,
                codeAgentRecenceur = batiment.codeAgentRecenceur
            };

        }

        public static LogementIType MapReaderToLogementIndType(tbl_logement logement)
        {
            return new LogementIType
            {
                logeId = Convert.ToInt32(logement.logeId),
                batimentId = Convert.ToInt32(logement.batimentId),
                //sdeId = logement.sdeId,
                sdeId = "0111-01-001",
                qlCategLogement = Convert.ToByte(logement.qlCategLogement),
                qlin1NumeroOrdre = Convert.ToByte(logement.qlin1NumeroOrdre),
                qlin2StatutOccupation = Convert.ToByte(logement.qlin2StatutOccupation),
                qlin3ExistenceLogement = Convert.ToByte(logement.qlin3ExistenceLogement),
                qlin4TypeLogement = Convert.ToByte(logement.qlin4TypeLogement),
                qlin5MateriauSol = Convert.ToByte(logement.qlin5MateriauSol),
                qlin6NombrePiece = Convert.ToByte(logement.qlin6NombrePiece),
                qlin7NbreChambreACoucher = Convert.ToByte(logement.qlin7NbreChambreACoucher),
                qlin8NbreIndividuDepense = Convert.ToByte(logement.qlin8NbreIndividuDepense),
                qlin9NbreTotalMenage = Convert.ToByte(logement.qlin9NbreTotalMenage),
                statut = Convert.ToByte(logement.statut),
                isValidated = Convert.ToBoolean(logement.isValidated),
                dateDebutCollecte = logement.dateDebutCollecte,
                dateFinCollecte = logement.dateFinCollecte,
                dureeSaisie = Convert.ToInt32(logement.dureeSaisie),
                isFieldAllFilled = Convert.ToBoolean(logement.isFieldAllFilled),
                isContreEnqueteMade = Convert.ToBoolean(logement.isContreEnqueteMade),
                nbrTentative = Convert.ToByte(logement.nbrTentative),
                codeAgentRecenceur = logement.codeAgentRecenceur

            };
        }

        public static LogementCType MapReaderToLogementCollectifType(tbl_logement logement)
        {
            return new LogementCType
            {
                logeId = logement.logeId,
                //sdeId = logement.sdeId,
                sdeId = "0111-01-001",
                batimentId = logement.batimentId.GetValueOrDefault(),
                qlc2bTotalFille = Convert.ToByte(logement.qlc2bTotalFille.GetValueOrDefault()),
                qlcTotalIndividus = Convert.ToByte(logement.qlcTotalIndividus),
                qllc2bTotalGarcon = Convert.ToByte(logement.qlc2bTotalGarcon.GetValueOrDefault()),
                qlCategLogement = Convert.ToByte(logement.qlCategLogement.GetValueOrDefault()),
                isContreEnqueteMade = Convert.ToBoolean(logement.isContreEnqueteMade.GetValueOrDefault()),
                isValidated = Convert.ToBoolean(logement.isValidated.GetValueOrDefault()),
                isFieldAllFilled = Convert.ToBoolean(logement.isFieldAllFilled.GetValueOrDefault()),
                dateDebutCollecte = logement.dateDebutCollecte,
                dateFinCollecte = logement.dateFinCollecte,
                dureeSaisie = Convert.ToInt32(logement.dureeSaisie.GetValueOrDefault()),
                statut = Convert.ToByte(logement.statut.GetValueOrDefault()),
                codeAgentRecenceur=logement.codeAgentRecenceur
            };

        }

        public static MenageType MapReaderToMenageType(tbl_menage menage)
        {
            return new MenageType
            {
                menageId = Convert.ToInt32(menage.menageId),
                logeId = Convert.ToInt32(menage.logeId),
                batimentId = Convert.ToInt32(menage.batimentId),
                //sdeId = menage.sdeId,
                sdeId = "0111-01-001",
                qm1NoOrdre = Convert.ToByte(menage.qm1NoOrdre),
                qm2ModeJouissance = Convert.ToByte(menage.qm2ModeJouissance),
                qm3ModeObtentionLoge = Convert.ToByte(menage.qm3ModeObtentionLoge),
                qm4_1ModeAprovEauABoire = Convert.ToByte(menage.qm4_1ModeAprovEauABoire),
                qm4_2ModeAprovEauAUsageCourant = Convert.ToByte(menage.qm4_2ModeAprovEauAUsageCourant),
                qm5SrcEnergieCuisson1 = Convert.ToByte(menage.qm5SrcEnergieCuisson1),
                qm5SrcEnergieCuisson2 = Convert.ToByte(menage.qm5SrcEnergieCuisson2),
                qm6TypeEclairage = Convert.ToByte(menage.qm6TypeEclairage),
                qm7ModeEvacDechet = Convert.ToByte(menage.qm7ModeEvacDechet),
                qm8EndroitBesoinPhysiologique = Convert.ToByte(menage.qm8EndroitBesoinPhysiologique),
                qm9NbreRadio1 = Convert.ToInt32(menage.qm9NbreRadio1),
                qm9NbreTelevision2 = Convert.ToInt32(menage.qm9NbreTelevision2),
                qm9NbreRefrigerateur3 = Convert.ToInt32(menage.qm9NbreRefrigerateur3),
                qm9NbreFouElectrique4 = Convert.ToInt32(menage.qm9NbreFouElectrique4),
                qm9NbreOrdinateur5 = Convert.ToInt32(menage.qm9NbreOrdinateur5),
                qm9NbreMotoBicyclette6 = Convert.ToInt32(menage.qm9NbreMotoBicyclette6),
                qm9NbreVoitureMachine7 = Convert.ToInt32(menage.qm9NbreVoitureMachine7),
                qm9NbreBateau8 = Convert.ToInt32(menage.qm9NbreBateau8),
                qm9NbrePanneauGeneratrice9 = Convert.ToInt32(menage.qm9NbrePanneauGeneratrice9),
                qm9NbreMilletChevalBourique10 = Convert.ToInt32(menage.qm9NbreMilletChevalBourique10),
                qm9NbreBoeufVache11 = Convert.ToInt32(menage.qm9NbreBoeufVache11),
                qm9NbreCochonCabrit12 = Convert.ToInt32(menage.qm9NbreCochonCabrit12),
                qm9NbreBeteVolaille13 = Convert.ToInt32(menage.qm9NbreBeteVolaille13),
                qm10AvoirPersDomestique = Convert.ToByte(menage.qm10AvoirPersDomestique),
                qm10TotalDomestiqueFille = Convert.ToByte(menage.qm10TotalDomestiqueFille),
                qm10TotalDomestiqueGarcon = Convert.ToByte(menage.qm10TotalDomestiqueGarcon),
                qm11TotalIndividuVivant = Convert.ToInt32(menage.qm11TotalIndividuVivant),
                qn1Emigration = Convert.ToByte(menage.qn1Emigration),
                //qn1NbreEmigreFille = Convert.ToByte(menage.qn1NbreEmigreFille),
                //qn1NbreEmigreGarcon = Convert.ToByte(menage.qn1NbreEmigreGarcon),
                qd1Deces = Convert.ToByte(menage.qd1Deces),
                //qd1NbreDecedeFille = Convert.ToByte(menage.qd1NbreDecedeFille),
                //qd1NbreDecedeGarcon = Convert.ToByte(menage.qd1NbreDecedeGarcon),
                statut = Convert.ToByte(menage.statut),
                isValidated = Convert.ToBoolean(menage.isValidated),
                dateDebutCollecte = menage.dateDebutCollecte,
                dateFinCollecte = menage.dateFinCollecte,
                dureeSaisie = Convert.ToInt32(menage.dureeSaisie),
                isFieldAllFilled = Convert.ToBoolean(menage.isFieldAllFilled),
                isContreEnqueteMade = Convert.ToBoolean(menage.isContreEnqueteMade),
                codeAgentRecenceur = menage.codeAgentRecenceur

            };

        }

        public static EmigreType MapReaderToEmigreType(tbl_emigre emigre)
        {
            return new EmigreType
            {
                emigreId = Convert.ToInt32(emigre.emigreId),
                menageId = Convert.ToInt32(emigre.menageId),
                logeId = Convert.ToInt32(emigre.logeId),
                batimentId = Convert.ToInt32(emigre.batimentId),
                //sdeId = emigre.sdeId,
                sdeId = "0111-01-001",
                qn1numeroOrdre = Convert.ToByte(emigre.qn1numeroOrdre),
                qn2aNomComplet = emigre.qn2aNomComplet,
                //qn2bResidenceActuelle = Convert.ToByte(emigre.qn2bResidenceActuelle),
                qn2cSexe = Convert.ToByte(emigre.qn2bSexe),
                //qn2dAgeAuMomentDepart = emigre.qn2dAgeAuMomentDepart.ToString(),
                qn2eDernierPaysResidence = Convert.ToByte(emigre.qn2eDernierPaysResidence),
                statut = Convert.ToByte(emigre.statut),
                isFieldAllFilled = Convert.ToBoolean(emigre.isFieldAllFilled),
                dateDebutCollecte = emigre.dateDebutCollecte,
                dateFinCollecte = emigre.dateFinCollecte,
                dureeSaisie = Convert.ToInt32(emigre.dureeSaisie),
                codeAgentRecenceur = emigre.codeAgentRecenceur,
            };
        }

        public static DecesType MapReaderToDecesType(tbl_deces deces)
        {
            return new DecesType
            {
                decesId = Convert.ToInt32(deces.decesId),
                menageId = Convert.ToInt32(deces.menageId),
                logeId = Convert.ToInt32(deces.logeId),
                batimentId = Convert.ToInt32(deces.batimentId),
                //sdeId = deces.sdeId,
                sdeId = "0111-01-001",
                qd2NoOrdre = Convert.ToByte(deces.qd2NoOrdre),
                qd2aSexe = Convert.ToByte(deces.qd2aSexe),
                qd2bAgeDecede = deces.qd2bAgeDecede.ToString(),
                qd2c1CirconstanceDeces = Convert.ToByte(deces.qd2c1CirconstanceDeces),
                qd2c2CauseDeces = Convert.ToByte(deces.qd2c2CauseDeces),
                statut = Convert.ToByte(deces.statut),
                isFieldAllFilled = Convert.ToBoolean(deces.isFieldAllFilled),
                dateDebutCollecte = deces.dateDebutCollecte,
                dateFinCollecte = deces.dateFinCollecte,
                dureeSaisie = Convert.ToInt32(deces.dureeSaisie),
                isContreEnqueteMade = Convert.ToBoolean(deces.isContreEnqueteMade),
                codeAgentRecenceur = deces.codeAgentRecenceur
            };
        }

        public static IndividuType MapReaderToIndividuType(tbl_individu individu)
        {
            if (individu != null)
            {
                return new IndividuType
                {
                    individuId = Convert.ToInt32(individu.individuId),
                    menageId = Convert.ToInt32(individu.menageId),
                    logeId = Convert.ToInt32(individu.logeId),
                    batimentId = Convert.ToInt32(individu.batimentId),
                    //sdeId = individu.sdeId,
                    sdeId = "0111-01-001",
                    q1NoOrdre = Convert.ToByte(individu.q1NoOrdre),
                    q3Prenom = individu.qp2APrenom,
                    q2Nom = individu.qp2BNom,
                    q6LienDeParente = Convert.ToByte(individu.qp3LienDeParente),
                    q5HabiteDansMenage = Convert.ToByte(individu.qp3HabiteDansMenage),
                    q4Sexe = Convert.ToByte(individu.qp4Sexe),
                    q7DateNaissanceJour = Convert.ToByte(individu.qp5DateNaissanceJour),
                    q7DateNaissanceMois = Convert.ToByte(individu.qp5DateNaissanceMois),
                    q7DateNaissanceAnnee = Convert.ToInt32(individu.Qp5DateNaissanceAnnee),
                    q5bAge = Convert.ToByte(individu.qp5bAge),
                    qp6religion = Convert.ToByte(individu.qp6religion),
                    qp6AutreReligion = individu.qp6AutreReligion,
                    qp7Nationalite = Convert.ToByte(individu.qp7Nationalite),
                    qp7PaysNationalite = individu.qp7PaysNationalite,
                    qp8MereEncoreVivante = Convert.ToByte(individu.qp8MereEncoreVivante),
                    qp9EstPlusAge = Convert.ToByte(individu.qp9EstPlusAge),
                    qp10LieuNaissance = Convert.ToByte(individu.qp10LieuNaissance),
                    qp10CommuneNaissance = individu.qp10CommuneNaissance,
                    qp10VqseNaissance = individu.qp10VqseNaissance,
                    qp10PaysNaissance = individu.qp10PaysNaissance,
                    qp11PeriodeResidence = Convert.ToByte(individu.qp11PeriodeResidence),
                    qp12DomicileAvantRecensement = Convert.ToByte(individu.qp12DomicileAvantRecensement),
                    qp12CommuneDomicileAvantRecensement = individu.qp12CommuneDomicileAvantRecensement,
                    qp12VqseDomicileAvantRecensement = individu.qp12VqseDomicileAvantRecensement,
                    qp12PaysDomicileAvantRecensement = individu.qp12PaysDomicileAvantRecensement,
                    qe1EstAlphabetise = Convert.ToByte(individu.qe1EstAlphabetise),
                    qe2FreqentationScolaireOuUniv = Convert.ToByte(individu.qe2FreqentationScolaireOuUniv),
                    qe3typeEcoleOuUniv = Convert.ToByte(individu.qe3typeEcoleOuUniv),
                    qe4aNiveauEtude = Convert.ToByte(individu.qe4aNiveauEtude),
                    qe4bDerniereClasseOUAneEtude = individu.qe4bDerniereClasseOUAneEtude,
                    qe5DiplomeUniversitaire = Convert.ToByte(individu.qe5DiplomeUniversitaire),
                    qe6DomaineEtudeUniversitaire = individu.qe6DomaineEtudeUniversitaire,
                    qaf1HandicapVoir = Convert.ToByte(individu.qaf1HandicapVoir),
                    qaf2HandicapEntendre = Convert.ToByte(individu.qaf2HandicapEntendre),
                    qaf3HandicapMarcher = Convert.ToByte(individu.qaf3HandicapMarcher),
                    qaf4HandicapSouvenir = Convert.ToByte(individu.qaf4HandicapSouvenir),
                    qaf5HandicapPourSeSoigner = Convert.ToByte(individu.qaf5HandicapPourSeSoigner),
                    qaf6HandicapCommuniquer = Convert.ToByte(individu.qaf6HandicapCommuniquer),
                    qt1PossessionTelCellulaire = Convert.ToByte(individu.qt1PossessionTelCellulaire),
                    qt2UtilisationInternet = Convert.ToByte(individu.qt2UtilisationInternet),
                    qem1DejaVivreAutrePays = Convert.ToByte(individu.qem1DejaVivreAutrePays),
                    qem1AutrePays = individu.qem1AutrePays,
                    qem2MoisRetour = Convert.ToByte(individu.qem2MoisRetour),
                    qem2AnneeRetour = Convert.ToInt32(individu.qem2AnneeRetour),
                    qsm1StatutMatrimonial = Convert.ToByte(individu.qsm1StatutMatrimonial),
                    qa1ActEconomiqueDerniereSemaine = Convert.ToByte(individu.qa1ActEconomiqueDerniereSemaine),
                    qa2ActAvoirDemele1 = Convert.ToByte(individu.qa2ActAvoirDemele1),
                    qa2ActDomestique2 = Convert.ToByte(individu.qa2ActDomestique2),
                    qa2ActCultivateur3 = Convert.ToByte(individu.qa2ActCultivateur3),
                    qa2ActAiderParent4 = Convert.ToByte(individu.qa2ActAiderParent4),
                    qa2ActAutre5 = Convert.ToByte(individu.qa2ActAutre5),
                    qa3StatutEmploie = Convert.ToByte(individu.qa3StatutEmploie),
                    qa4SecteurInstitutionnel = Convert.ToByte(individu.qa4SecteurInstitutionnel),
                    qa5TypeBienProduitParEntreprise = individu.qa5TypeBienProduitParEntreprise,
                    qa6LieuActDerniereSemaine = Convert.ToByte(individu.qa6LieuActDerniereSemaine),
                    qa7FoncTravail = Convert.ToByte(individu.qa7FoncTravail),
                    qa8EntreprendreDemarcheTravail = Convert.ToByte(individu.qa8EntreprendreDemarcheTravail),
                    qa9VouloirTravailler = Convert.ToByte(individu.qa9VouloirTravailler),
                    qa10DisponibilitePourTravail = Convert.ToByte(individu.qa10DisponibilitePourTravail),
                    qa11RecevoirTransfertArgent = Convert.ToByte(individu.qa11RecevoirTransfertArgent),
                    qf1aNbreEnfantNeVivantM = Convert.ToInt32(individu.qf1aNbreEnfantNeVivantM),
                    //qf1bNbreEnfantNeVivantF=Convert.ToInt32(individu.qf1bNbreEnfantNeVivantF),
                    qf2aNbreEnfantVivantM = Convert.ToInt32(individu.qf2aNbreEnfantVivantM),
                    qf2bNbreEnfantVivantF = Convert.ToInt32(individu.qf2bNbreEnfantVivantF),
                    qf3DernierEnfantJour = Convert.ToByte(individu.qf3DernierEnfantJour),
                    qf3DernierEnfantMois = Convert.ToByte(individu.qf3DernierEnfantMois),
                    qf3DernierEnfantAnnee = Convert.ToInt32(individu.qf3DernierEnfantAnnee),
                    qf4DENeVivantVit = Convert.ToByte(individu.qf4DENeVivantVit),
                    statut = Convert.ToByte(individu.statut),
                    isFieldAllFilled = Convert.ToBoolean(individu.isFieldAllFilled),
                    dateDebutCollecte = individu.dateDebutCollecte,
                    dateFinCollecte = individu.dateFinCollecte,
                    dureeSaisie = Convert.ToInt32(individu.dureeSaisie),
                    isContreEnqueteMade = Convert.ToBoolean(individu.isContreEnqueteMade),
                    codeAgentRecenceur = individu.codeAgentRecenceur
                };
            }
            return new IndividuType();

        }

        #endregion

        #region MAPPING CONTREENQUETE TYPE IN XSD CONTREENQUETE TYPE
        public static BatimentType MapReaderToBatimentType(Tbl_BatimentCE batiment)
        {
            BatimentType bat = new BatimentType();
            bat.batimentId = batiment.BatimentId.GetValueOrDefault();
            bat.sdeId = batiment.SdeId;
            bat.qrec = batiment.Qrec;
            bat.qrgph = batiment.Qrgph;
            bat.qadresse = batiment.Qadresse;
            bat.qhabitation = batiment.Qhabitation;
            bat.qlocalite = batiment.Qlocalite;
            bat.qb1Etat = Convert.ToByte(batiment.Qb1Etat.GetValueOrDefault());
            bat.qb3NombreEtage = Convert.ToByte(batiment.Qb3NombreEtage);
            bat.qb4MateriauMur = Convert.ToByte(batiment.Qb4MateriauMur);
            bat.qb5MateriauToit = Convert.ToByte(batiment.Qb5MateriauToit);
            bat.qb6StatutOccupation = Convert.ToByte(batiment.Qb6StatutOccupation.GetValueOrDefault());
            bat.qb7Utilisation1 = Convert.ToByte(batiment.Qb7Utilisation1.GetValueOrDefault());
            bat.qb7Utilisation2 = Convert.ToByte(batiment.Qb7Utilisation2.GetValueOrDefault());
            bat.qb8NbreLogeCollectif = Convert.ToByte(batiment.Qb8NbreLogeCollectif.GetValueOrDefault());
            bat.qb8NbreLogeIndividuel = Convert.ToByte(batiment.Qb8NbreLogeIndividuel.GetValueOrDefault());
            bat.isContreEnqueteMade = Convert.ToBoolean(batiment.IsContreEnqueteMade.GetValueOrDefault());
            bat.isValidated = Convert.ToBoolean(batiment.IsValidated.GetValueOrDefault());
            return bat;
        }
       
        public static LogementIType MapReaderToLogementIndType(Tbl_LogementCE logement)
        {
            return new LogementIType
            {
                logeId = Convert.ToInt32(logement.LogeId),
                sdeId = logement.SdeId,
                batimentId = logement.BatimentId.GetValueOrDefault(),
                qlCategLogement = Convert.ToByte(logement.QlCategLogement.GetValueOrDefault()),
                qlin1NumeroOrdre = Convert.ToByte(logement.Qlin1NumeroOrdre),
                qlin6NombrePiece = Convert.ToByte(logement.Qlin6NombrePiece.GetValueOrDefault()),
                //qlin7NbreChambreACoucher = Convert.ToByte(logement.Qlin7NbreChambreACoucher.GetValueOrDefault()),
                ////qlin11NbreIndividuVivant = Convert.ToByte(logement.Qlin11NbreIndividuVivant.GetValueOrDefault()),
                //qlin8NbreIndividuDepense = Convert.ToByte(logement.Qlin8NbreIndividuDepense),
                //qlin9NbreTotalMenage = Convert.ToByte(logement.Qlin9NbreTotalMenage.GetValueOrDefault()),
                isContreEnqueteMade = Convert.ToBoolean(logement.IsContreEnqueteMade.GetValueOrDefault()),
                isValidated = Convert.ToBoolean(logement.IsValidated.GetValueOrDefault()),
                statut = Convert.ToByte(logement.Statut.GetValueOrDefault()),
                dateDebutCollecte = logement.DateDebutCollecte,
                dateFinCollecte = logement.DateFinCollecte,
                dureeSaisie = Convert.ToInt32(logement.DureeSaisie)

            };
        }
        
        public static LogementCType MapReaderToLogementCollectifType(Tbl_LogementCE logement)
        {
            return new LogementCType
            {
                logeId = logement.LogeId.GetValueOrDefault(),
                sdeId = logement.SdeId,
                batimentId = logement.BatimentId.GetValueOrDefault(),
                qlc2bTotalFille = Convert.ToByte(logement.Qlc2bTotalFille.GetValueOrDefault()),
                qllc2bTotalGarcon = Convert.ToByte(logement.Qllc2bTotalGarcon.GetValueOrDefault()),
                //qlcTotalIndividus = Convert.ToByte(logement.Qlin11NbreIndividuVivant.GetValueOrDefault()),
                qlCategLogement = Convert.ToByte(logement.QlCategLogement.GetValueOrDefault()),
                isContreEnqueteMade = Convert.ToBoolean(logement.IsContreEnqueteMade.GetValueOrDefault()),
                isValidated = Convert.ToBoolean(logement.IsValidated.GetValueOrDefault()),
                dureeSaisie = Convert.ToInt32(logement.DureeSaisie.GetValueOrDefault()),
                qlc1TypeLogement = Convert.ToByte(logement.QlcTypeLogement.GetValueOrDefault()),
                dateDebutCollecte = logement.DateDebutCollecte,
                dateFinCollecte = logement.DateFinCollecte,
                statut = Convert.ToByte(logement.Statut.GetValueOrDefault())
            };
        }
        public static List<LogementCType> MapToListLogementCType(List<Tbl_LogementCE> listOfLC)
        {
            try
            {
                List<LogementCType> list = new List<LogementCType>();
                foreach (Tbl_LogementCE lce in listOfLC)
                {
                    LogementCType lct = MapReaderToLogementCollectifType(lce);
                    list.Add(lct);
                }
                return list;
            }
            catch (Exception)
            {

            }
            return null;
        }
        public static List<LogementIType> MapToListLogementIType(List<Tbl_LogementCE> listOfLC)
        {
            try
            {
                List<LogementIType> list = new List<LogementIType>();
                foreach (Tbl_LogementCE lce in listOfLC)
                {
                    LogementIType lct = MapReaderToLogementIndType(lce);
                    list.Add(lct);
                }
                return list;
            }
            catch (Exception)
            {

            }
            return null;
        }
        public static MenageType MapReaderToMenageType(Tbl_MenageCE menage)
        {
            return new MenageType
          {
              menageId = Convert.ToInt32(menage.MenageId),
              logeId = Convert.ToInt32(menage.LogeId),
              batimentId = Convert.ToInt32(menage.BatimentId),
              sdeId = menage.SdeId,
              qm1NoOrdre = Convert.ToByte(menage.Qm1NoOrdre),
              qm2ModeJouissance = Convert.ToByte(menage.Qm2ModeJouissance),
              //qm3ModeObtentionLoge = Convert.ToByte(menage.Qm2ModeObtentionLoge),
              //qm11TotalIndividuVivant = Convert.ToByte(menage.Qm10TotalIndividuVivant.GetValueOrDefault()),
              //qd1Deces = Convert.ToByte(menage.Qm12Deces.GetValueOrDefault()),
              //qd1NbreDecedeFille = Convert.ToByte(menage.()),
              //qd1NbreDecede = Convert.ToByte(menage.Qm12NbreDecede.GetValueOrDefault()),
              dateDebutCollecte = menage.DateDebutCollecte,
              dateFinCollecte = menage.DateFinCollecte,
              dureeSaisie = Convert.ToInt32(menage.DureeSaisie.GetValueOrDefault()),
              isValidated = Convert.ToBoolean(menage.IsValidated.GetValueOrDefault()),
              isContreEnqueteMade = Convert.ToBoolean(menage.IsContreEnqueteMade.GetValueOrDefault())
          };
        }
        public static List<MenageType> MapToListMenageType(List<Tbl_MenageCE> menages)
        {
            try
            {
                List<MenageType> list = new List<MenageType>();
                foreach (Tbl_MenageCE men in menages)
                {
                    MenageType m = MapReaderToMenageType(men);
                    list.Add(m);
                }
                return list;
            }
            catch (Exception)
            {

            }
            return null;
        }
        
        public static DecesType MapReaderToDecesType(Tbl_DecesCE deces)
        {
            return new DecesType
            {
                decesId = Convert.ToInt32(deces.DecesId),
                menageId = Convert.ToInt32(deces.MenageId),
                logeId = Convert.ToInt32(deces.LogeId),
                batimentId = Convert.ToInt32(deces.BatimentId),
                sdeId = deces.SdeId,
                qd2NoOrdre = Convert.ToByte(deces.Qd2NoOrdre),
                //qd2aSexe = Convert.ToByte(deces.Qd2aSexe),
                //qd2bAgeDecede = deces.Qd2bAgeDecede.GetValueOrDefault().ToString(),
                //qd2c1CirconstanceDeces = Convert.ToByte(deces.Qd2c1CirconstanceDeces),
                //qd2c2CauseDeces = Convert.ToByte(deces.Qd2c2CauseDeces),
                dureeSaisie = Convert.ToInt32(deces.DureeSaisie.GetValueOrDefault()),
                //isValidated =deces.IsValidate.GetValueOrDefault(),
                isContreEnqueteMade = Convert.ToBoolean(deces.IsContreEnqueteMade.GetValueOrDefault()),
            };
        }
        public static List<DecesType> MapToListDecesType(List<Tbl_DecesCE> deces)
        {
            try
            {
                List<DecesType> list = new List<DecesType>();
                foreach (Tbl_DecesCE dec in deces)
                {
                    DecesType de = MapReaderToDecesType(dec);
                    list.Add(de);
                }
                return list;
            }
            catch (Exception)
            {

            }
            return null;
        }
        
        public static List<IndividuType> MapToListIndividuType(List<Tbl_IndividusCE> individus)
        {
            try
            {
                List<IndividuType> list = new List<IndividuType>();
                foreach (Tbl_IndividusCE ind in individus)
                {
                    IndividuType individu = MapReaderToIndividuType(ind);
                    list.Add(individu);
                }
                return list;
            }
            catch (Exception)
            {

            }
            return null;
        }
        public static IndividuType MapReaderToIndividuType(Tbl_IndividusCE ind)
        {
            return new IndividuType
            {
                logeId = ind.LogeId.GetValueOrDefault(),
                individuId = ind.IndividuId.GetValueOrDefault(),
                batimentId = ind.BatimentId.GetValueOrDefault(),
                menageId = ind.MenageId.GetValueOrDefault(),
                sdeId = ind.SdeId,
                q1NoOrdre = Convert.ToByte(ind.Qp1NoOrdre),
                q2Nom = ind.Q2Nom,
                q3Prenom = ind.Q3Prenom,
                //q4HabiteDansMenage=ind.hab
                q4Sexe = Convert.ToByte(ind.Q4Sexe),
                //q6DateNaissanceAnnee=ind.Q7DateNaissanceAnnee,
                //q6DateNaissanceJour=ind.Q7DateNaissanceJour,
                //q6DateNaissanceMois=ind.Q7DateNaissanceMois,
                q5bAge = Convert.ToByte(ind.Q5bAge.GetValueOrDefault()),
                q6LienDeParente = Convert.ToByte(ind.Q3LienDeParente),
                qp7Nationalite = Convert.ToByte(ind.Qp7Nationalite.GetValueOrDefault()),
                qp7PaysNationalite = ind.Qp7PaysNationalite,
                qp10LieuNaissance = Convert.ToByte(ind.Qp10LieuNaissance.GetValueOrDefault()),
                qp10CommuneNaissance = ind.Qp10CommuneNaissance,
                qp10PaysNaissance = ind.Qp10PaysNaissance,
                qp10VqseNaissance = ind.Qp10LieuNaissanceVqse,
                qp11PeriodeResidence = Convert.ToByte(ind.Qp11PeriodeResidence.GetValueOrDefault()),
                //qp12DomicileAvantRecensement = Convert.ToByte(ind.Qp12DomicileAvantRecensement.GetValueOrDefault()),
                //qp12CommuneDomicileAvantRecensement = ind.Qp12DomicileAvantRecensementComm,
                //qp12PaysDomicileAvantRecensement = ind.Qp12DomicileAvantRecensementPays,
                //qp12VqseDomicileAvantRecensement = ind.Qp12DomicileAvantRecensementVqse,
                //qe1EstAlphabetise = Convert.ToByte(ind.Qe1EstAlphabetise),
                qe2FreqentationScolaireOuUniv = Convert.ToByte(ind.Qe2FreqentationScolaireOuUniv),
                qe4aNiveauEtude = Convert.ToByte(ind.Qe4aNiveauEtude.GetValueOrDefault()),
                qe4bDerniereClasseOUAneEtude = ind.Qe4bDerniereClasseOUAneEtude.GetValueOrDefault().ToString(),
                //qe5DiplomeUniversitaire = Convert.ToByte(ind.Qe5DiplomeUniversitaire.GetValueOrDefault()),
                //qe7FormationProf = Convert.ToByte(ind.Qe7FormationProf.GetValueOrDefault()),
                //qaf1HandicapVoir = Convert.ToByte(ind.Qaf1aHandicapVoir.GetValueOrDefault()),
                //qaf2HandicapEntendre = Convert.ToByte(ind.Qaf2aHandicapEntendre.GetValueOrDefault()),
                //qaf3HandicapMarcher = Convert.ToByte(ind.Qaf3aHandicapMarcher.GetValueOrDefault()),
                //qaf4HandicapSouvenir = Convert.ToByte(ind.Qaf4aHandicapSouvenir.GetValueOrDefault()),
                //qaf5HandicapPourSeSoigner = Convert.ToByte(ind.Qaf5aHandicapPourSeSoigner.GetValueOrDefault()),
                //qaf6HandicapCommuniquer = Convert.ToByte(ind.Qaf6aHandicapCommuniquer.GetValueOrDefault()),
                qsm1StatutMatrimonial = Convert.ToByte(ind.Qsm1StatutMatrimonial),
                qa1ActEconomiqueDerniereSemaine = Convert.ToByte(ind.Qa1ActEconomiqueDerniereSemaine.GetValueOrDefault()),
                qa2ActAvoirDemele1 = Convert.ToByte(ind.Qa2ActAvoirDemele1.GetValueOrDefault()),
                qa2ActDomestique2 = Convert.ToByte(ind.Qa2ActDomestique2.GetValueOrDefault()),
                qa2ActCultivateur3 = Convert.ToByte(ind.Qa2ActCultivateur3.GetValueOrDefault()),
                qa2ActAiderParent4 = Convert.ToByte(ind.Qa2ActAiderParent4.GetValueOrDefault()),
                qa2ActAutre5 = Convert.ToByte(ind.Qa2ActAutre5.GetValueOrDefault()),
                //qa3StatutEmploie = Convert.ToByte(ind.Qa3StatutEmploie.GetValueOrDefault()),
                //qa4SecteurInstitutionnel = Convert.ToByte(ind.Qa4SecteurInstitutionnel.GetValueOrDefault()),
                //qa5TypeBienProduitParEntreprise = ind.Qa5TypeBienProduitParEntreprise,
                //qa6LieuActDerniereSemaine = Convert.ToByte(ind.Qa7FoncTravail.GetValueOrDefault()),
                //qa7FoncTravail = Convert.ToByte(ind.Qa7FoncTravail.GetValueOrDefault()),
                qa8EntreprendreDemarcheTravail = Convert.ToByte(ind.Qa8EntreprendreDemarcheTravail.GetValueOrDefault()),
                //qa9VouloirTravailler = Convert.ToByte(ind.Qa9VouloirTravailler.GetValueOrDefault()),
                //qa10DisponibilitePourTravail = Convert.ToByte(ind.Qa10DisponibilitePourTravail.GetValueOrDefault()),
                //qa11RecevoirTransfertArgent = Convert.ToByte(ind.Qa11RecevoirTransfertArgent.GetValueOrDefault()),
                qf1aNbreEnfantNeVivantM = Convert.ToByte(ind.Qf1aNbreEnfantNeVivantM.GetValueOrDefault()),
                qf1bNbreEnfantNeVivantF = Convert.ToByte(ind.Qf2bNbreEnfantNeVivantF.GetValueOrDefault()),
                qf2aNbreEnfantVivantM = Convert.ToByte(ind.Qf2aNbreEnfantVivantM.GetValueOrDefault()),
                qf2bNbreEnfantVivantF = Convert.ToByte(ind.Qf2bNbreEnfantNeVivantF.GetValueOrDefault()),
                qf3DernierEnfantAnnee = Convert.ToByte(ind.Qf3DernierEnfantAnnee.GetValueOrDefault()),
                qf3DernierEnfantJour = Convert.ToByte(ind.Qf3DernierEnfantJour.GetValueOrDefault()),
                qf3DernierEnfantMois = Convert.ToByte(ind.Qf3DernierEnfantMois.GetValueOrDefault()),
                qf4DENeVivantVit = Convert.ToByte(ind.Qf4DENeVivantVit.GetValueOrDefault()),
                dureeSaisie = Convert.ToInt32(ind.DureeSaisie.GetValueOrDefault()),
                isContreEnqueteMade = Convert.ToBoolean(ind.IsContreEnqueteMade.GetValueOrDefault()),
                dateDebutCollecte = ind.DateDebutCollecte,
                dateFinCollecte = ind.DateFinCollecte,
                statut = Convert.ToByte(ind.Statut.GetValueOrDefault())

            };
        }
        
        public static ContreEnqueteType MapToContreEnqeuteType(Tbl_ContreEnquete contreEnquete)
        {
            ContreEnqueteType cet = new ContreEnqueteType();
            cet.batimentId = contreEnquete.BatimentId.GetValueOrDefault();
            cet.codeDistrict = contreEnquete.CodeDistrict;
            cet.dateDebut = contreEnquete.DateDebut;
            cet.dateFin = contreEnquete.DateFin;
            cet.nomSuperviseur = contreEnquete.NomSuperviseur;
            cet.prenomSuperviseur = contreEnquete.PrenomSuperviseur;
            cet.raison = Convert.ToInt32(contreEnquete.Raison.GetValueOrDefault());
            cet.sdeId = contreEnquete.SdeId;
            cet.statut = Convert.ToInt32(contreEnquete.Statut.GetValueOrDefault());
            return cet;
        }
        public static List<ContreEnqueteType> MapToListContreEnqueteType(List<Tbl_ContreEnquete> listOfCE)
        {
            try
            {
                List<ContreEnqueteType> listCET = new List<ContreEnqueteType>();
                foreach (Tbl_ContreEnquete ce in listOfCE)
                {
                    ContreEnqueteType cet = MapToContreEnqeuteType(ce);
                    listCET.Add(cet);
                }
                return listCET;
            }
            catch (Exception)
            {

            }
            return null;
        }

        #endregion

        #region RAPPORTS

        public static RapportDeroulementCollecte MapToRapportDeroulementCollecte(Tbl_RprtDeroulement rprt)
        {
            RapportDeroulementCollecte rapport = new RapportDeroulementCollecte();
            rapport.detailsRapportId = rprt.RapportId;
            rapport.districtId = rprt.CodeDistrict;
            rapport.dateRapport = rprt.DateRapport;
            return rapport;
        }

        public static RapportDeroulementCollecteType MapToRapportDeroulementCollecteType(Tbl_DetailsRapport details)
        {
            RapportDeroulementCollecteType rapport = new RapportDeroulementCollecteType();
            rapport.detailsRapportId = Convert.ToInt32(details.DetailsRapportId);
            rapport.rapportId = details.RapportId.GetValueOrDefault();
            rapport.domaine = Convert.ToByte(details.Domaine);
            rapport.sousDomaine = Convert.ToByte(details.SousDomaine);
            rapport.probleme = details.Probleme.GetValueOrDefault().ToString();
            rapport.solution = Convert.ToByte(details.Solution.GetValueOrDefault());
            rapport.precisions = details.Precisions;
            rapport.suggestions = details.Suggestions;
            rapport.suivi = details.Suivi;
            return rapport;
        }

        public static RapportSuperviseurDirectType MapToRapportSuperviseurDirectType(Tbl_RapportPersonnel rprt)
        {
            RapportSuperviseurDirectType rapport = new RapportSuperviseurDirectType();
            rapport.rapportId = rprt.rapportId;
            rapport.codeAgentRecenseur = Convert.ToInt32(rprt.persId.GetValueOrDefault());
            rapport.q1 = Convert.ToByte(rprt.q1.GetValueOrDefault());
            rapport.q2 = Convert.ToByte(rprt.q2.GetValueOrDefault());
            rapport.q3 = Convert.ToByte(rprt.q3.GetValueOrDefault());
            rapport.q4 = Convert.ToByte(rprt.q4.GetValueOrDefault());
            rapport.q5 = Convert.ToByte(rprt.q5.GetValueOrDefault());
            rapport.q6 = Convert.ToByte(rprt.q6.GetValueOrDefault());
            rapport.q7 = Convert.ToByte(rprt.q7.GetValueOrDefault());
            rapport.q8 = Convert.ToByte(rprt.q8.GetValueOrDefault());
            rapport.q9 = Convert.ToByte(rprt.q9.GetValueOrDefault());
            rapport.q10 = Convert.ToByte(rprt.q10.GetValueOrDefault());
            rapport.q11 = Convert.ToByte(rprt.q11.GetValueOrDefault());
            rapport.q12 = Convert.ToByte(rprt.q12.GetValueOrDefault());
            rapport.q13 = Convert.ToByte(rprt.q13.GetValueOrDefault());
            rapport.q14 = rprt.q14;
            rapport.q15 = Convert.ToByte(rprt.q15.GetValueOrDefault());
            //rapport.dateEvaluation = rprt.dateEvaluation.GetValueOrDefault();
            return rapport;

        }
        #endregion
    }
}
