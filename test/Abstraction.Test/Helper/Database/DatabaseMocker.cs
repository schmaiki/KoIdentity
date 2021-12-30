// KoIdentity Copyright (C) 2021 Tekoding. All Rights Reserved.
// 
// Created: 2021.12.28 22:29
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

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tekoding.KoIdentity.Abstraction.Test.Helper.Model;

namespace Tekoding.KoIdentity.Abstraction.Test.Helper.Database;

internal static class DatabaseMocker
{
    internal static async Task LoadDatabase(DbContextOptions dbContextOptions)
    {
        await using var ctx = new DatabaseContext(dbContextOptions);

        Constant.DatabaseMainEntities = new List<DefaultEntity>(await ctx.MainEntities.ToListAsync());

        await ctx.SaveChangesAsync();
    }
        
    internal static async Task ResetDatabase(DbContextOptions dbContextOptions)
    {
        await using var ctx = new DatabaseContext(dbContextOptions);

        await ClearMainEntities(dbContextOptions);

        await ctx.AddRangeAsync(Constant.DatabaseMainEntities);
        await ctx.SaveChangesAsync();
    }

    private static async Task ClearMainEntities(DbContextOptions dbContextOptions)
    {
        await using var ctx = new DatabaseContext(dbContextOptions);
        ctx.MainEntities.RemoveRange(await ctx.MainEntities.ToListAsync());
        await ctx.SaveChangesAsync();
    }
}