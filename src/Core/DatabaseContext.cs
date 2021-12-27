// KoIdentity Copyright (C) 2021 Tekoding. All Rights Reserved.
// 
// Created: 2021.12.23 04:25
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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Tekoding.KoIdentity.Abstraction.Extension;
using Tekoding.KoIdentity.Core.Model;

namespace Tekoding.KoIdentity.Core;

/// <summary>
/// Provides the base implementation of the <see cref="Abstraction.DatabaseContext{TDefaultEntity}"/>, which is using
/// the default implementation for KoIdentity.
/// </summary>
/// <typeparam name="TUser">The type encapsulating a default <see cref="User"/>.</typeparam>
public class DatabaseContext<TUser> : Tekoding.KoIdentity.Abstraction.DatabaseContext<TUser>
    where TUser : User
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseContext"/>.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
    /// <param name="databaseSchema">The database schema name. Default is "KoIdentity".</param>
    public DatabaseContext(DbContextOptions options, string databaseSchema = "KoIdentity") :
        base(options, databaseSchema)
    {
    }

    /// <summary>
    /// Further configuration of the model that was discovered by convention from the entity types
    /// exposed in <see cref="DbSet{TEntity}"/> properties on the derived context.
    /// </summary>
    /// <remarks>
    /// If a model is explicitly set on the options for this context then this method will not be run.
    /// </remarks>
    /// <param name="modelBuilder">
    /// The builder being used to construct the model for this context. Databases (and other extensions) typically
    /// define extension methods on this object that allow you to configure aspects of the model that are specific
    /// to a given database.
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TUser>().BuildUniqueIndex(u => u.MailAddress);
        modelBuilder.Entity<TUser>().BuildUniqueIndex(u => u.Password);

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}"/> of <see cref="User"/>s.
    /// </summary>
    /// <remarks>
    /// Since the <see cref="Abstraction.DatabaseContext{TDefaultEntity}"/> is having a <see cref="DbSet{TEntity}"/> for
    /// <see cref="Abstraction.DatabaseContext{TDefaultEntity}.MainEntities"/> this one gets encapsulated within this
    /// property.
    /// </remarks>
    public DbSet<TUser> Users
    {
        get => MainEntities;
        set => MainEntities = value;
    }

    /// <inheritdoc/>
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        /// <summary>
        /// Creates the <see cref="DatabaseContext"/>.
        /// </summary>
        /// <param name="args">Optional arguments, which are not used.</param>
        /// <returns>The <see cref="DatabaseContext"/>.</returns>
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            optionsBuilder.UseSqlServer(
                "Server=127.0.0.1;Database=Tekoding;User Id=sa;Password=yourStrong(:)Password;");
            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}

/// <summary>
/// Provides the base implementation of the database context for KoIdentity.
/// </summary>
public class DatabaseContext : DatabaseContext<User>
{
    /// <summary>
    /// Initializes a new instance of <see cref="DatabaseContext"/>.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
    /// <param name="databaseSchema">The database schema name. Default is "KoIdentity".</param>
    public DatabaseContext(DbContextOptions options, string databaseSchema = "KoIdentity") : base(options,
        databaseSchema)
    {
    }
}