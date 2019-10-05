﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TkdScoringApp.API.Data;

namespace TkdScoringApp.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TkdAPI.Entities.KickBody", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("JudgeId");

                    b.Property<int>("MatchId");

                    b.Property<int>("NoOfConfirmation");

                    b.Property<int>("NoOfConsecutiveTaps");

                    b.Property<int>("PlayerId");

                    b.Property<int>("Score");

                    b.Property<DateTime>("time");

                    b.HasKey("Id");

                    b.ToTable("kickbody");
                });

            modelBuilder.Entity("TkdAPI.Entities.Kickhead", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("JudgeId");

                    b.Property<int>("MatchId");

                    b.Property<int>("NoOfConfirmation");

                    b.Property<int>("NoOfConsecutiveTaps");

                    b.Property<int>("PlayerId");

                    b.Property<int>("Score");

                    b.Property<DateTime>("time");

                    b.HasKey("Id");

                    b.ToTable("kickhead");
                });

            modelBuilder.Entity("TkdAPI.Entities.Punch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("JudgeId");

                    b.Property<int>("MatchId");

                    b.Property<int>("NoOfConfirmation");

                    b.Property<int>("NoOfConsecutiveTaps");

                    b.Property<int>("PlayerId");

                    b.Property<int>("Score");

                    b.Property<DateTime>("time");

                    b.HasKey("Id");

                    b.ToTable("punch");
                });

            modelBuilder.Entity("TkdAPI.Entities.TurningKickBody", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("JudgeId");

                    b.Property<int>("MatchId");

                    b.Property<int>("NoOfConfirmation");

                    b.Property<int>("NoOfConsecutiveTaps");

                    b.Property<int>("PlayerId");

                    b.Property<int>("Score");

                    b.Property<DateTime>("time");

                    b.HasKey("Id");

                    b.ToTable("turningKickBody");
                });

            modelBuilder.Entity("TkdAPI.Entities.TurningKickHead", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("JudgeId");

                    b.Property<int>("MatchId");

                    b.Property<int>("NoOfConfirmation");

                    b.Property<int>("NoOfConsecutiveTaps");

                    b.Property<int>("PlayerId");

                    b.Property<int>("Score");

                    b.Property<DateTime>("time");

                    b.HasKey("Id");

                    b.ToTable("turningKickHead");
                });

            modelBuilder.Entity("TkdScoringApp.API.Entities.Admin", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Username");

                    b.HasKey("id");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("TkdScoringApp.API.Entities.Judge", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LoginTime");

                    b.Property<int>("MatchId");

                    b.Property<string>("Name");

                    b.HasKey("id");

                    b.HasIndex("MatchId");

                    b.ToTable("Judge");
                });

            modelBuilder.Entity("TkdScoringApp.API.Entities.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdminId");

                    b.Property<int>("NoOfJudges");

                    b.Property<bool>("isPause");

                    b.HasKey("Id");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("TkdScoringApp.API.Entities.Player", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MatchId");

                    b.Property<string>("Name");

                    b.Property<int>("Totalfoul");

                    b.Property<int>("Totalscore");

                    b.HasKey("id");

                    b.HasIndex("MatchId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("TkdScoringApp.API.Entities.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MatchId");

                    b.Property<int>("PlayerId");

                    b.Property<int>("ScoreValue");

                    b.HasKey("Id");

                    b.ToTable("Score");
                });

            modelBuilder.Entity("TkdScoringApp.API.Entities.Judge", b =>
                {
                    b.HasOne("TkdScoringApp.API.Entities.Match")
                        .WithMany("Judges")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TkdScoringApp.API.Entities.Player", b =>
                {
                    b.HasOne("TkdScoringApp.API.Entities.Match")
                        .WithMany("Players")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
