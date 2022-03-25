using System.Data.Entity;
using Falcon.Web.Core.Auth;
using Falcon.Web.Core.Settings;
using Phoenix.Server.Data.Entity;

namespace Phoenix.Server.Services.Database
{
    public class DataContext : DbContext
    {
        #region Init
        public DataContext() : base("DataContext") { }
        #endregion

        #region Datasets
        //framework
        public virtual DbSet<ImageRecord> ImageRecords { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        //modules

        public virtual DbSet<Vendor> Vendors { get; set; }

        public virtual DbSet<ProductType> ProductTypes { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductSKU> ProductSKUs { get; set; }

        public virtual DbSet<Warehouse> Warehouses { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetail> OrderDatails { get; set; }

        public virtual DbSet<Rating> Ratings { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<WarehouseMenu> WarehouseMenus { get; set; }

        #endregion

    }
}