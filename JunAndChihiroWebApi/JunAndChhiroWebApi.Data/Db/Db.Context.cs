﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JunAndChhiroWebApi.Data.Db
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JunAndChihiroEntities : DbContext
    {
        public JunAndChihiroEntities()
            : base("name=JunAndChihiroEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CoreRelationInfo> CoreRelationInfoes { get; set; }
        public virtual DbSet<CoreTableInfo> CoreTableInfoes { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Folder> Folders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<JFile> JFiles { get; set; }
        public virtual DbSet<JFolder> JFolders { get; set; }
        public virtual DbSet<JTableInfo> JTableInfoes { get; set; }
        public virtual DbSet<JUser> JUsers { get; set; }
        public virtual DbSet<XFolderHierarchy> XFolderHierarchies { get; set; }
        public virtual DbSet<XOwnFile> XOwnFiles { get; set; }
    }
}