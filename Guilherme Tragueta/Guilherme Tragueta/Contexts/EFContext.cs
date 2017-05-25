using Guilherme_Tragueta.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Guilherme_Tragueta.Contexts
{
    public class EFContext : DbContext
    {
        #region Construtor
        public EFContext() : base("Asp_Net_MVC_CS"){
            Database.SetInitializer<EFContext>(
                new DropCreateDatabaseIfModelChanges<EFContext>());
        } 
        #endregion

        public DbSet<Category> Categories { get; set;}
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}