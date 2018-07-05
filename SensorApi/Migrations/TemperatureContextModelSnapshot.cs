﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SensorApi.Models;
using System;

namespace SensorApi.Migrations
{
    [DbContext(typeof(TemperatureContext))]
    partial class TemperatureContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("SensorApi.Models.TemperatureItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Temperature");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.ToTable("TemperatureItems");
                });
#pragma warning restore 612, 618
        }
    }
}