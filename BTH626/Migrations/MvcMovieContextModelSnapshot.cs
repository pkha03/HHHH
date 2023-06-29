﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcMovie.Data;

#nullable disable

namespace BTH626.Migrations
{
    [DbContext(typeof(MvcMovieContext))]
    partial class MvcMovieContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("BTH626.Models.Lophoc", b =>
                {
                    b.Property<string>("tenlop")
                        .HasColumnType("TEXT");

                    b.Property<string>("malop")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("tenlop");

                    b.ToTable("Lophoc");
                });

            modelBuilder.Entity("BTH626.Models.Sinhvien", b =>
                {
                    b.Property<string>("masinhvien")
                        .HasColumnType("TEXT");

                    b.Property<string>("malop")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("tensinhvien")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("masinhvien");

                    b.HasIndex("malop");

                    b.ToTable("Sinhvien");
                });

            modelBuilder.Entity("BTH626.Models.Sinhvien", b =>
                {
                    b.HasOne("BTH626.Models.Lophoc", "Lophoc")
                        .WithMany()
                        .HasForeignKey("malop")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lophoc");
                });
#pragma warning restore 612, 618
        }
    }
}
