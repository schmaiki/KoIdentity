// KoIdentity Copyright (C) 2021 Tekoding. All Rights Reserved.
// 
// Created: 2021.12.23 01:07
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
using Tekoding.KoIdentity.Abstraction.Extension;
using Tekoding.KoIdentity.Abstraction.Model;

namespace Tekoding.KoIdentity.Abstraction;

    /// <summary>
    /// Provides the base abstraction of the EntityFramework database context, which is using the default implementation
    /// for KoIdentity.
    /// </summary>
    /// <typeparam name="TDefaultEntity">The type encapsulating a default <see cref="Entity"/>.</typeparam>
    public abstract class DatabaseContext<TDefaultEntity> : DbContext
        where TDefaultEntity : Entity
    {
        private string DatabaseSchema { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="DatabaseContext{TDefaultEntity}"/>.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        /// <param name="databaseSchema">The database schema name.</param>
        protected DatabaseContext(DbContextOptions options, string databaseSchema = "KoIdentity.Abstraction") :
            base(options)
        {
            DatabaseSchema = databaseSchema;
        }

        /// <summary>
        /// Further configuration of the model that was discovered by convention from the entity types
        /// exposed in <see cref="DbSet{TDefaultEntity}"/> properties on the derived context.
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
            modelBuilder.Entity<TDefaultEntity>().BuildDefaultEntity();

            modelBuilder.HasDefaultSchema(DatabaseSchema);
        }


        #nullable disable
        
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TDefaultEntity}"/> of the main entity.
        /// </summary>
        public DbSet<TDefaultEntity> MainEntities { get; set; }
        
        #nullable restore
    }