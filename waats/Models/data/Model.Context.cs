﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace waats.Models.data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class waatsEntities : DbContext
    {
        public waatsEntities()
            : base("name=waatsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<BrainFitness> BrainFitness { get; set; }
        public virtual DbSet<EOT> EOT { get; set; }
        public virtual DbSet<EST> EST { get; set; }
        public virtual DbSet<GenderT> GenderT { get; set; }
        public virtual DbSet<GratitudeJournal> GratitudeJournal { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<MemoryMaker> MemoryMaker { get; set; }
        public virtual DbSet<MindFullAttention> MindFullAttention { get; set; }
        public virtual DbSet<ProfilePicT> ProfilePicT { get; set; }
        public virtual DbSet<ScheduleTask> ScheduleTask { get; set; }
        public virtual DbSet<SubScheduleTask> SubScheduleTask { get; set; }
        public virtual DbSet<Trigger> Trigger { get; set; }
        public virtual DbSet<waatsForm> waatsForm { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
    }
}