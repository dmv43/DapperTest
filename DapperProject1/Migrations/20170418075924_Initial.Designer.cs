using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DapperProject1.Models;

namespace DapperProject1.Migrations
{
    [DbContext(typeof(TeacherContext))]
    [Migration("20170418075924_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DapperProject1.Models.Teacher", b =>
                {
                    b.Property<int>("TeacherID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Location");

                    b.Property<int>("NumberOfLessons");

                    b.Property<int>("NumberOfStudents");

                    b.Property<double>("Rating");

                    b.Property<string>("URL");

                    b.HasKey("TeacherID");

                    b.ToTable("Teachers");
                });
        }
    }
}
