// KoIdentity Copyright (C) 2021 Tekoding. All Rights Reserved.
// 
// Created: 2021.12.17 07:06
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
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tekoding.KoIdentity.Abstraction.Model;

namespace Tekoding.KoIdentity.Abstraction.Extension;

/// <summary>
/// Relational database specific extension methods for <see cref="EntityTypeBuilder"/>.
/// </summary>
public static class ModelBuilderExtension
{
    /// <summary>
    /// Building the default <see cref="Entity"/> and setting the default key, the default values for the
    /// <see cref="Entity.CreationDate"/>, the <see cref="Entity.ChangeDate"/>, as well as the default update
    /// behaviour of the <see cref="Entity.ChangeDate"/>.
    /// </summary>
    /// <param name="entityTypeBuilder">The <see cref="EntityTypeBuilder"/> used.</param>
    /// <typeparam name="TEntityModel">The type encapsulating an <see cref="Entity"/>.</typeparam>
    /// <returns>The <see cref="EntityTypeBuilder{TEntity}"/> used to build the default entity.</returns>
    public static EntityTypeBuilder<TEntityModel> BuildDefaultEntity<TEntityModel>(
        this EntityTypeBuilder<TEntityModel> entityTypeBuilder)
        where TEntityModel : Entity
    {
        entityTypeBuilder.HasKey(e => e.Id);
        entityTypeBuilder.Property(e => e.CreationDate).HasDefaultValueSql("getDate()");
        entityTypeBuilder.Property(e => e.ChangeDate).HasDefaultValueSql("getDate()");
        entityTypeBuilder.Property(e => e.ChangeDate).ValueGeneratedOnAddOrUpdate();

        entityTypeBuilder.ToTable(typeof(TEntityModel).Name);

        return entityTypeBuilder;
    }
}