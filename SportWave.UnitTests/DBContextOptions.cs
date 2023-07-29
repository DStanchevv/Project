﻿using Microsoft.EntityFrameworkCore;
using SportWave.Data;

namespace SportWave.UnitTests
{
    public static class DbContextOptions
    {
        private static int dbIndex = 0;

        public static DbContextOptions<SportWaveDbContext> Options =>
            new DbContextOptionsBuilder<SportWaveDbContext>()
                .UseInMemoryDatabase("TestDb" + dbIndex++)
                .Options;
    }
}

