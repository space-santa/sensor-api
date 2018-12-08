﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SensorApi.Models
{
    public class TemperatureContext : DbContext
    {
        public TemperatureContext(DbContextOptions<TemperatureContext> options)
            : base(options)
        {
        }

        public DbSet<TemperatureItem> TemperatureItems { get; set; }
        public DbSet<Device> Devices { get; set; }
    }
}
