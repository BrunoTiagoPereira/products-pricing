﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductsPricing.Data.SqlServer.Provider.Persistence;

#nullable disable

namespace ProductsPricing.Data.SqlServer.Provider.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20221108165915_unity-of-measure-as-entity")]
    partial class unityofmeasureasentity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.ImpactedProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ImportId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TargetProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ImportId");

                    b.HasIndex("TargetProductId");

                    b.ToTable("ImpactedProducts", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("ImpactedProduct");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.Import", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime>("FinishedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Imports", (string)null);
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.ImportItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ImportId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ImportId");

                    b.ToTable("ImportItems", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("ImportItem");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.ImportLog", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ImportId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ImportId");

                    b.ToTable("ImportLogs", (string)null);
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.PendingProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PendingImportItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PendingImportItemId");

                    b.HasIndex("ProductId");

                    b.ToTable("PendingProducts", (string)null);
                });

            modelBuilder.Entity("ProductsPricing.Domain.Ncms.Entities.Ncm", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.HasKey("Id");

                    b.ToTable("Ncms", (string)null);
                });

            modelBuilder.Entity("ProductsPricing.Domain.Ncms.Entities.NcmProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("NcmId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("NcmId");

                    b.HasIndex("ProductId");

                    b.ToTable("NcmProducts", (string)null);
                });

            modelBuilder.Entity("ProductsPricing.Domain.Products.Entities.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DependencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RootProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DependencyId");

                    b.HasIndex("RootProductId");

                    b.ToTable("Ingredients", (string)null);
                });

            modelBuilder.Entity("ProductsPricing.Domain.Products.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AdditionalValue")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)")
                        .HasDefaultValue(0m);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Value")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Products", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("Product");
                });

            modelBuilder.Entity("ProductsPricing.Domain.UnitOfMeasures.Entities.UnitOfMeasure", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UnitOfMeasures", (string)null);
                });

            modelBuilder.Entity("ProductsPricing.Domain.UnitOfMeasures.Entities.UnitOfMeasureConversion", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("GramsByUnit")
                        .HasPrecision(9, 4)
                        .HasColumnType("decimal(9,4)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProductsCount")
                        .HasColumnType("int");

                    b.Property<Guid>("UnitOfMeasureId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UnitOfMeasureId");

                    b.ToTable("UnitOfMeasureConversions", (string)null);
                });

            modelBuilder.Entity("ProductsPricing.Domain.Users.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Roles")
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.DecodedImportItem", b =>
                {
                    b.HasBaseType("ProductsPricing.Domain.Imports.Entities.ImportItem");

                    b.Property<int>("FileLineReference")
                        .HasColumnType("int");

                    b.Property<decimal>("NewValue")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("DecodedImportItem_NewValue");

                    b.Property<Guid>("UnitOfMeasureId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("UnitOfMeasureId");

                    b.HasDiscriminator().HasValue("DecodedImportItem");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.EvaluatedImportItem", b =>
                {
                    b.HasBaseType("ProductsPricing.Domain.Imports.Entities.ImportItem");

                    b.Property<decimal>("NewValue")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("ProductId");

                    b.HasDiscriminator().HasValue("EvaluatedImportItem");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.ImpactedByProduct", b =>
                {
                    b.HasBaseType("ProductsPricing.Domain.Imports.Entities.ImpactedProduct");

                    b.Property<Guid>("RootProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("RootProductId");

                    b.HasDiscriminator().HasValue("ImpactedByProduct");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Products.Entities.PrimaryProduct", b =>
                {
                    b.HasBaseType("ProductsPricing.Domain.Products.Entities.Product");

                    b.HasDiscriminator().HasValue("PrimaryProduct");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Products.Entities.RefinedProduct", b =>
                {
                    b.HasBaseType("ProductsPricing.Domain.Products.Entities.Product");

                    b.HasDiscriminator().HasValue("RefinedProduct");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.PendingImportItem", b =>
                {
                    b.HasBaseType("ProductsPricing.Domain.Imports.Entities.DecodedImportItem");

                    b.Property<Guid?>("SelectedProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("SelectedProductId");

                    b.HasDiscriminator().HasValue("PendingImportItem");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.ProcessedImportItem", b =>
                {
                    b.HasBaseType("ProductsPricing.Domain.Imports.Entities.DecodedImportItem");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProcessedImportItem_ProductId");

                    b.HasIndex("ProductId");

                    b.HasDiscriminator().HasValue("ProcessedImportItem");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.ImpactedProduct", b =>
                {
                    b.HasOne("ProductsPricing.Domain.Imports.Entities.Import", "Import")
                        .WithMany("ImpactedProducts")
                        .HasForeignKey("ImportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductsPricing.Domain.Products.Entities.Product", "TargetProduct")
                        .WithMany()
                        .HasForeignKey("TargetProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("ProductsPricing.Domain.Imports.ValueObjects.ImpactedProductStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("ImpactedProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("ImpactedProductId");

                            b1.ToTable("ImpactedProducts");

                            b1.WithOwner()
                                .HasForeignKey("ImpactedProductId");
                        });

                    b.Navigation("Import");

                    b.Navigation("Status")
                        .IsRequired();

                    b.Navigation("TargetProduct");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.Import", b =>
                {
                    b.HasOne("ProductsPricing.Domain.Users.Entities.User", "User")
                        .WithMany("Imports")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ProductsPricing.Domain.Imports.ValueObjects.ImportStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("ImportId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("ImportId");

                            b1.ToTable("Imports");

                            b1.WithOwner()
                                .HasForeignKey("ImportId");
                        });

                    b.Navigation("Status")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.ImportItem", b =>
                {
                    b.HasOne("ProductsPricing.Domain.Imports.Entities.Import", "Import")
                        .WithMany("Items")
                        .HasForeignKey("ImportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Import");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.ImportLog", b =>
                {
                    b.HasOne("ProductsPricing.Domain.Imports.Entities.Import", "Import")
                        .WithMany("Logs")
                        .HasForeignKey("ImportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ProductsPricing.Core.ValueObjects.LogLevel", "LogLevel", b1 =>
                        {
                            b1.Property<Guid>("ImportLogId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("ImportLogId");

                            b1.ToTable("ImportLogs");

                            b1.WithOwner()
                                .HasForeignKey("ImportLogId");
                        });

                    b.Navigation("Import");

                    b.Navigation("LogLevel")
                        .IsRequired();
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.PendingProduct", b =>
                {
                    b.HasOne("ProductsPricing.Domain.Imports.Entities.PendingImportItem", "PendingImportItem")
                        .WithMany("PendingProducts")
                        .HasForeignKey("PendingImportItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductsPricing.Domain.Products.Entities.PrimaryProduct", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("PendingImportItem");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Ncms.Entities.Ncm", b =>
                {
                    b.OwnsOne("ProductsPricing.Domain.Products.ValueObjects.Code", "Code", b1 =>
                        {
                            b1.Property<Guid>("NcmId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(8)
                                .HasColumnType("nvarchar(8)");

                            b1.HasKey("NcmId");

                            b1.ToTable("Ncms");

                            b1.WithOwner()
                                .HasForeignKey("NcmId");
                        });

                    b.Navigation("Code")
                        .IsRequired();
                });

            modelBuilder.Entity("ProductsPricing.Domain.Ncms.Entities.NcmProduct", b =>
                {
                    b.HasOne("ProductsPricing.Domain.Ncms.Entities.Ncm", "Ncm")
                        .WithMany()
                        .HasForeignKey("NcmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductsPricing.Domain.Products.Entities.PrimaryProduct", "Product")
                        .WithMany("NcmProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ncm");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Products.Entities.Ingredient", b =>
                {
                    b.HasOne("ProductsPricing.Domain.Products.Entities.Product", "Dependency")
                        .WithMany()
                        .HasForeignKey("DependencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductsPricing.Domain.Products.Entities.RefinedProduct", "RootProduct")
                        .WithMany("Ingredients")
                        .HasForeignKey("RootProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Dependency");

                    b.Navigation("RootProduct");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Products.Entities.Product", b =>
                {
                    b.HasOne("ProductsPricing.Domain.Users.Entities.User", "User")
                        .WithMany("Products")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProductsPricing.Domain.UnitOfMeasures.Entities.UnitOfMeasure", b =>
                {
                    b.HasOne("ProductsPricing.Domain.Users.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProductsPricing.Domain.UnitOfMeasures.Entities.UnitOfMeasureConversion", b =>
                {
                    b.HasOne("ProductsPricing.Domain.Products.Entities.PrimaryProduct", "Product")
                        .WithMany("UnitOfMeasureConversions")
                        .HasForeignKey("ProductId");

                    b.HasOne("ProductsPricing.Domain.UnitOfMeasures.Entities.UnitOfMeasure", "UnitOfMeasure")
                        .WithMany()
                        .HasForeignKey("UnitOfMeasureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("UnitOfMeasure");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Users.Entities.User", b =>
                {
                    b.OwnsOne("ProductsPricing.Core.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(300)
                                .HasColumnType("nvarchar(300)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("ProductsPricing.Core.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Hash")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Password")
                        .IsRequired();
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.DecodedImportItem", b =>
                {
                    b.HasOne("ProductsPricing.Domain.UnitOfMeasures.Entities.UnitOfMeasure", "UnitOfMeasure")
                        .WithMany()
                        .HasForeignKey("UnitOfMeasureId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UnitOfMeasure");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.EvaluatedImportItem", b =>
                {
                    b.HasOne("ProductsPricing.Domain.Products.Entities.PrimaryProduct", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.ImpactedByProduct", b =>
                {
                    b.HasOne("ProductsPricing.Domain.Products.Entities.Product", "RootProduct")
                        .WithMany()
                        .HasForeignKey("RootProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("RootProduct");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.PendingImportItem", b =>
                {
                    b.HasOne("ProductsPricing.Domain.Products.Entities.PrimaryProduct", "SelectedProduct")
                        .WithMany()
                        .HasForeignKey("SelectedProductId");

                    b.OwnsOne("ProductsPricing.Domain.Imports.ValueObjects.PendingImportItemStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("PendingImportItemId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("PendingImportItemId");

                            b1.ToTable("ImportItems");

                            b1.WithOwner()
                                .HasForeignKey("PendingImportItemId");
                        });

                    b.Navigation("SelectedProduct");

                    b.Navigation("Status")
                        .IsRequired();
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.ProcessedImportItem", b =>
                {
                    b.HasOne("ProductsPricing.Domain.Products.Entities.PrimaryProduct", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.Import", b =>
                {
                    b.Navigation("ImpactedProducts");

                    b.Navigation("Items");

                    b.Navigation("Logs");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Users.Entities.User", b =>
                {
                    b.Navigation("Imports");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Products.Entities.PrimaryProduct", b =>
                {
                    b.Navigation("NcmProducts");

                    b.Navigation("UnitOfMeasureConversions");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Products.Entities.RefinedProduct", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("ProductsPricing.Domain.Imports.Entities.PendingImportItem", b =>
                {
                    b.Navigation("PendingProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
