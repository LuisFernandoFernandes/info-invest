using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Threading.Tasks;

namespace trade.application.entity_framework
{
    public class TradeContexto : DbContext, IDisposable
    {
        public TradeContexto() : base("name=trade")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 9999;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
            base.OnModelCreating(modelBuilder);
        }

        protected override void Dispose(bool disposing) { if (Database.Connection.State == ConnectionState.Open && Database.CurrentTransaction == null) { Database.Connection.Close(); } }

        public override async Task<int> SaveChangesAsync()
        { try { return await base.SaveChangesAsync(); } catch (DbEntityValidationException e) { throw new Exception(e.Message); } }

        public override int SaveChanges()
        { try { return base.SaveChanges(); } catch (DbEntityValidationException e) { throw new Exception(e.Message); } }

    }

}