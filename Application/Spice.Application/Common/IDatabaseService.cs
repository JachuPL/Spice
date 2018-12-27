﻿using Microsoft.EntityFrameworkCore;
using Spice.Domain;
using Spice.Domain.Plants;
using System.Threading.Tasks;

namespace Spice.Application.Common
{
    public interface IDatabaseService
    {
        DbSet<Plant> Plants { get; set; }
        DbSet<Field> Fields { get; set; }
        DbSet<Domain.Plants.Species> Species { get; set; }
        DbSet<Nutrient> Nutrients { get; set; }

        int Save();

        Task<int> SaveAsync();
    }
}