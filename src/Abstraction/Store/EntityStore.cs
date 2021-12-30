// KoIdentity Copyright (C) 2021 Tekoding. All Rights Reserved.
// 
// Created: 2021.12.17 07:23
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
using Tekoding.KoIdentity.Abstraction.Extension.Exception;
using Tekoding.KoIdentity.Abstraction.Model;
using Tekoding.KoIdentity.Abstraction.Store.Interface;

namespace Tekoding.KoIdentity.Abstraction.Store;

/// <summary>
/// Represents a persistence store abstraction for <typeparamref name="TEntity"/>´s of type <see cref="Entity"/>.
/// </summary>
/// <typeparam name="TDbContext">The type encapsulating a <see cref="Microsoft.EntityFrameworkCore.DbContext"/>.</typeparam>
/// <typeparam name="TEntity">The type encapsulating a <see cref="Entity"/>.</typeparam>
public abstract class EntityStore<TDbContext, TEntity> : IEntityStore<TEntity>
    where TDbContext : DbContext
    where TEntity : Entity
{
    private TDbContext DbContext { get; }
    private ErrorDescriber ErrorDescriber { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="EntityStore{TDbContext,TEntity}"/>.
    /// </summary>
    /// <param name="dbContext">The <see cref="Microsoft.EntityFrameworkCore.DbContext"/> used to access the store.</param>
    /// <param name="errorDescriber">The <see cref="ErrorDescriber"/> used to describe errors.</param>
    protected EntityStore(TDbContext dbContext, ErrorDescriber errorDescriber)
    {
        DbContext = dbContext;
        ErrorDescriber = errorDescriber;
    }

    /// <inheritdoc />
    public virtual async Task<OperationResult> CreateAsync(TEntity entity,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        DbContext.Add(entity);
        await DbContext.SaveChangesAsync(cancellationToken);

        return OperationResult.Success;
    }

    /// <inheritdoc />
    public virtual async Task<OperationResult> UpdateAsync(TEntity entity,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        if (DbContext.Entry(entity).State != EntityState.Modified)
        {
            return OperationResult.Failed(ErrorDescriber.EntityStateUnmodified());
        }

        DbContext.Attach(entity);
        DbContext.Update(entity);

        try
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            return OperationResult.Failed(ErrorDescriber.ConcurrencyFailure());
        }

        return OperationResult.Success;
    }

    /// <inheritdoc />
    public virtual async Task<OperationResult> DeleteAsync(TEntity entity,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        DbContext.Remove(entity);
        
        try
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            return OperationResult.Failed(ErrorDescriber.ConcurrencyFailure());
        }
        
        return OperationResult.Success;
    }

    /// <inheritdoc />
    public virtual async Task<TEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        if (id == Guid.Empty)
        {
            throw new InvalidOperationException("Empty GUID provided.");
        }

        return await DbContext.FindAsync<TEntity>(new object[] {id}, cancellationToken: cancellationToken);
    }
}