﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineShopping.API.DbContexts;

namespace OnlineShopping.API.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    [Migration("20210811100018_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineShopping.API.Entities.BPType", b =>
                {
                    b.Property<string>("TypeCode")
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("TypeCode");

                    b.ToTable("BPTypes");

                    b.HasData(
                        new
                        {
                            TypeCode = "C",
                            TypeName = "Customer"
                        },
                        new
                        {
                            TypeCode = "V",
                            TypeName = "Vendor"
                        });
                });

            modelBuilder.Entity("OnlineShopping.API.Entities.BusinessPartner", b =>
                {
                    b.Property<string>("BPCode")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("BPName")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<string>("BPTypeId")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("BPCode");

                    b.HasIndex("BPTypeId");

                    b.ToTable("BusinessPartners");

                    b.HasData(
                        new
                        {
                            BPCode = "C0001",
                            Active = true,
                            BPName = "Customer 1",
                            BPTypeId = "C"
                        },
                        new
                        {
                            BPCode = "C0002",
                            Active = false,
                            BPName = "Customer 2",
                            BPTypeId = "C"
                        },
                        new
                        {
                            BPCode = "V0001",
                            Active = true,
                            BPName = "Vendor 1",
                            BPTypeId = "V"
                        },
                        new
                        {
                            BPCode = "V0002",
                            Active = false,
                            BPName = "Vendor 2",
                            BPTypeId = "V"
                        });
                });

            modelBuilder.Entity("OnlineShopping.API.Entities.Item", b =>
                {
                    b.Property<string>("ItemCode")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.HasKey("ItemCode");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            ItemCode = "Itm1",
                            Active = true,
                            ItemName = "Item 1"
                        },
                        new
                        {
                            ItemCode = "Itm2",
                            Active = true,
                            ItemName = "Item 2"
                        },
                        new
                        {
                            ItemCode = "Itm3",
                            Active = false,
                            ItemName = "Item 3"
                        });
                });

            modelBuilder.Entity("OnlineShopping.API.Entities.PurchaseOrder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BusinessPartnerId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedByID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastUpdatedByID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BusinessPartnerId");

                    b.HasIndex("CreatedByID");

                    b.HasIndex("LastUpdatedByID");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("OnlineShopping.API.Entities.PurchaseOrdersLine", b =>
                {
                    b.Property<int>("LineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedByID")
                        .HasColumnType("int");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastUpdatedByID")
                        .HasColumnType("int");

                    b.Property<int>("PurchaseOrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasPrecision(38, 18)
                        .HasColumnType("decimal(38,18)");

                    b.HasKey("LineID");

                    b.HasIndex("CreatedByID");

                    b.HasIndex("ItemId");

                    b.HasIndex("LastUpdatedByID");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("PurchaseOrdersLines");
                });

            modelBuilder.Entity("OnlineShopping.API.Entities.SaleOrder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BusinessPartnerId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedByID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastUpdatedByID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BusinessPartnerId");

                    b.HasIndex("CreatedByID");

                    b.HasIndex("LastUpdatedByID");

                    b.ToTable("SaleOrders");
                });

            modelBuilder.Entity("OnlineShopping.API.Entities.SaleOrdersLine", b =>
                {
                    b.Property<int>("LineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedByID")
                        .HasColumnType("int");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastUpdatedByID")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasPrecision(38, 18)
                        .HasColumnType("decimal(38,18)");

                    b.Property<int>("SaleOrderId")
                        .HasColumnType("int");

                    b.HasKey("LineID");

                    b.HasIndex("CreatedByID");

                    b.HasIndex("ItemId");

                    b.HasIndex("LastUpdatedByID");

                    b.HasIndex("SaleOrderId");

                    b.ToTable("SaleOrdersLines");
                });

            modelBuilder.Entity("OnlineShopping.API.Entities.SaleOrdersLinesComment", b =>
                {
                    b.Property<int>("CommentLineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DocIDID")
                        .HasColumnType("int");

                    b.Property<int>("SaleOrdersLineId")
                        .HasColumnType("int");

                    b.HasKey("CommentLineID");

                    b.HasIndex("DocIDID");

                    b.HasIndex("SaleOrdersLineId");

                    b.ToTable("SaleOrdersLinesComments");
                });

            modelBuilder.Entity("OnlineShopping.API.Entities.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.HasKey("ID");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Active = true,
                            FullName = "U1",
                            Password = "P1",
                            UserName = "U1"
                        },
                        new
                        {
                            ID = 2,
                            Active = false,
                            FullName = "U2",
                            Password = "P2",
                            UserName = "U2"
                        });
                });

            modelBuilder.Entity("OnlineShopping.API.Entities.BusinessPartner", b =>
                {
                    b.HasOne("OnlineShopping.API.Entities.BPType", "BPType")
                        .WithMany()
                        .HasForeignKey("BPTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BPType");
                });

            modelBuilder.Entity("OnlineShopping.API.Entities.PurchaseOrder", b =>
                {
                    b.HasOne("OnlineShopping.API.Entities.BusinessPartner", "BPCode")
                        .WithMany()
                        .HasForeignKey("BusinessPartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShopping.API.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID");

                    b.HasOne("OnlineShopping.API.Entities.User", "LastUpdatedBy")
                        .WithMany()
                        .HasForeignKey("LastUpdatedByID");

                    b.Navigation("BPCode");

                    b.Navigation("CreatedBy");

                    b.Navigation("LastUpdatedBy");
                });

            modelBuilder.Entity("OnlineShopping.API.Entities.PurchaseOrdersLine", b =>
                {
                    b.HasOne("OnlineShopping.API.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID");

                    b.HasOne("OnlineShopping.API.Entities.Item", "Itemcode")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShopping.API.Entities.User", "LastUpdatedBy")
                        .WithMany()
                        .HasForeignKey("LastUpdatedByID");

                    b.HasOne("OnlineShopping.API.Entities.PurchaseOrder", "DocID")
                        .WithMany()
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("DocID");

                    b.Navigation("Itemcode");

                    b.Navigation("LastUpdatedBy");
                });

            modelBuilder.Entity("OnlineShopping.API.Entities.SaleOrder", b =>
                {
                    b.HasOne("OnlineShopping.API.Entities.BusinessPartner", "BPCode")
                        .WithMany()
                        .HasForeignKey("BusinessPartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShopping.API.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID");

                    b.HasOne("OnlineShopping.API.Entities.User", "LastUpdatedBy")
                        .WithMany()
                        .HasForeignKey("LastUpdatedByID");

                    b.Navigation("BPCode");

                    b.Navigation("CreatedBy");

                    b.Navigation("LastUpdatedBy");
                });

            modelBuilder.Entity("OnlineShopping.API.Entities.SaleOrdersLine", b =>
                {
                    b.HasOne("OnlineShopping.API.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID");

                    b.HasOne("OnlineShopping.API.Entities.Item", "Itemcode")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShopping.API.Entities.User", "LastUpdatedBy")
                        .WithMany()
                        .HasForeignKey("LastUpdatedByID");

                    b.HasOne("OnlineShopping.API.Entities.SaleOrder", "DocID")
                        .WithMany()
                        .HasForeignKey("SaleOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("DocID");

                    b.Navigation("Itemcode");

                    b.Navigation("LastUpdatedBy");
                });

            modelBuilder.Entity("OnlineShopping.API.Entities.SaleOrdersLinesComment", b =>
                {
                    b.HasOne("OnlineShopping.API.Entities.SaleOrder", "DocID")
                        .WithMany()
                        .HasForeignKey("DocIDID");

                    b.HasOne("OnlineShopping.API.Entities.SaleOrdersLine", "Lineid")
                        .WithMany()
                        .HasForeignKey("SaleOrdersLineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocID");

                    b.Navigation("Lineid");
                });
#pragma warning restore 612, 618
        }
    }
}
