﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RgphContext : DbContext
    {
        public RgphContext()
            : base("name=RgphContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Tbl_Agent> Tbl_Agent { get; set; }
        public virtual DbSet<Tbl_BatimentCE> Tbl_BatimentCE { get; set; }
        public virtual DbSet<Tbl_ContreEnquete> Tbl_ContreEnquete { get; set; }
        public virtual DbSet<Tbl_DecesCE> Tbl_DecesCE { get; set; }
        public virtual DbSet<Tbl_DetailsRapport> Tbl_DetailsRapport { get; set; }
        public virtual DbSet<Tbl_IndividusCE> Tbl_IndividusCE { get; set; }
        public virtual DbSet<Tbl_LogementCE> Tbl_LogementCE { get; set; }
        public virtual DbSet<Tbl_Materiels> Tbl_Materiels { get; set; }
        public virtual DbSet<Tbl_MenageCE> Tbl_MenageCE { get; set; }
        public virtual DbSet<Tbl_Probleme> Tbl_Probleme { get; set; }
        public virtual DbSet<Tbl_RapportPersonnel> Tbl_RapportPersonnel { get; set; }
        public virtual DbSet<Tbl_Retour> Tbl_Retour { get; set; }
        public virtual DbSet<Tbl_RprtComparaison> Tbl_RprtComparaison { get; set; }
        public virtual DbSet<Tbl_RprtDeroulement> Tbl_RprtDeroulement { get; set; }
        public virtual DbSet<Tbl_Sde> Tbl_Sde { get; set; }
        public virtual DbSet<Tbl_Utilisateur> Tbl_Utilisateur { get; set; }
        public virtual DbSet<Tbl_Utilisation> Tbl_Utilisation { get; set; }
        public virtual DbSet<Tbl_CategorieQuestion> Tbl_CategorieQuestion { get; set; }
        public virtual DbSet<Tbl_Commune> Tbl_Commune { get; set; }
        public virtual DbSet<Tbl_Departement> Tbl_Departement { get; set; }
        public virtual DbSet<Tbl_Pays> Tbl_Pays { get; set; }
        public virtual DbSet<Tbl_Questions> Tbl_Questions { get; set; }
        public virtual DbSet<Tbl_Questions_Reponses> Tbl_Questions_Reponses { get; set; }
        public virtual DbSet<Tbl_Reponses> Tbl_Reponses { get; set; }
        public virtual DbSet<Tbl_VilleQuartierSectionCommunale> Tbl_VilleQuartierSectionCommunale { get; set; }
    }
}