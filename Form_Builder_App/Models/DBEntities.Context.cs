﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Form_Builder_App.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class formBuilderDBEntities1 : DbContext
    {
        public formBuilderDBEntities1()
            : base("name=formBuilderDBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblForm> tblForms { get; set; }
        public virtual DbSet<tblInputInForm> tblInputInForms { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblUserSubmission> tblUserSubmissions { get; set; }
    }
}