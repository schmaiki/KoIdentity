// KoIdentity Copyright (C) 2021 Tekoding. All Rights Reserved.
// 
// Created: 2021.12.28 17:44
// 
// Authors: TheRealLenon
// 
// Licensed under the MIT License. See LICENSE.md in the project root for license
// information.
// 
// KoIdentity is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the MIT
// License for more details.

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Tekoding.KoIdentity.Abstraction.Test.Helper.Model;

namespace Tekoding.KoIdentity.Abstraction.Test.Helper.Database;

internal class DatabaseContext : DatabaseContext<DefaultEntity>
{
    internal DatabaseContext(DbContextOptions options, string databaseSchema = "KoIdentity.Abstraction") : base(options,
        databaseSchema)
    {
    }

    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            optionsBuilder.UseSqlServer(
                "Server=127.0.0.1;Database=Tekoding;User Id=sa;Password=yourStrong(:)Password;");
            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}