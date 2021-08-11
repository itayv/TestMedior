using Microsoft.EntityFrameworkCore;
using OnlineShopping.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.API.DbContexts
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserName).IsUnique();
            });


            builder.Entity<SaleOrdersLine>(entity =>
            {
                entity.Property(e => e.Quantity).HasPrecision(38, 18);
            });

            builder.Entity<PurchaseOrdersLine>(entity =>
            {
                entity.Property(e => e.Quantity).HasPrecision(38, 18);
            });


            builder.Entity<User>().HasData(
                new User
                {
                    ID = 1,
                    FullName = "U1",
                    UserName = "U1",
                    Password = "P1",
                    Active = true
                },
                new User
                {
                    ID = 2,
                    UserName = "U2",
                    FullName = "U2",
                    Password = "P2",
                    Active = false
                }
                );

            builder.Entity<BPType>().HasData(
                new BPType
                {
                    TypeCode = "C",
                    TypeName = "Customer"

                },
                new BPType
                {
                    TypeCode = "V",
                    TypeName = "Vendor"
                }
                );

            builder.Entity<BusinessPartner>().HasData(
                new BusinessPartner
                {
                    BPCode = "C0001",
                    BPName = "Customer 1",
                    BPTypeId = "C",
                    Active = true
                },
                new BusinessPartner
                {
                    BPCode = "C0002",
                    BPName = "Customer 2",
                    BPTypeId = "C",
                    Active = false
                },
                new BusinessPartner
                {
                    BPCode = "V0001",
                    BPName = "Vendor 1",
                    BPTypeId = "V",
                    Active = true
                },
                new BusinessPartner
                {
                    BPCode = "V0002",
                    BPName = "Vendor 2",
                    BPTypeId = "V",
                    Active = false
                }
                );

            builder.Entity<Item>().HasData(
                new Item
                {
                    ItemCode = "Itm1",
                    ItemName = "Item 1",
                    Active = true

                },
                new Item
                {
                    ItemCode = "Itm2",
                    ItemName = "Item 2",
                    Active = true

                },
                new Item
                {
                    ItemCode = "Itm3",
                    ItemName = "Item 3",
                    Active = false
                }

                );



            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<BPType> BPTypes { get; set; }

        public DbSet<BusinessPartner> BusinessPartners { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<SaleOrder> SaleOrders { get; set; }

        public DbSet<SaleOrdersLine> SaleOrdersLines { get; set; }

        public DbSet<SaleOrdersLinesComment> SaleOrdersLinesComments { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public DbSet<PurchaseOrdersLine> PurchaseOrdersLines { get; set; }

    }
}
