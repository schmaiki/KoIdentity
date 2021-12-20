// KoIdentity Copyright (C) 2021 Tekoding. All Rights Reserved.
// 
// Created: 2021.12.17 05:56
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

using Tekoding.KoIdentity.Abstraction.Extension;
using Tekoding.KoIdentity.Abstraction.Model;

namespace Tekoding.KoIdentity.Abstraction.Store.Interface;

/// <summary>
/// Represents a persistence store abstraction API for Entities of type <see cref="Entity"/>.
/// </summary>
/// <typeparam name="TEntity">The type encapsulating a <see cref="Entity"/>.</typeparam>
public interface IEntityStore<TEntity>
    where TEntity : Entity
{
        /// <summary>
        /// Creates the specified <paramref name="entity"/> in the store.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to create.</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.
        /// </param>
        /// <returns>
        /// The <see cref="Task{TResult}"/> that represents the asynchronous operation, containing the
        /// <see cref="OperationResult"/> of the create operation.
        /// </returns>
        public Task<OperationResult> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the specified <paramref name="entity"/> in the store.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to update.</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.
        /// </param>
        /// <returns>
        /// The <see cref="Task{TResult}"/> that represents the asynchronous operation, containing the
        /// <see cref="OperationResult"/> of the update operation.
        /// </returns>
        public Task<OperationResult> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes the specified <paramref name="entity"/> in the store.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to delete.</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.
        /// </param>
        /// <returns>
        /// The <see cref="Task{TResult}"/> that represents the asynchronous operation, containing the
        /// <see cref="OperationResult"/> of the removal operation.
        /// </returns>
        public Task<OperationResult> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds and returns an entity, if any, based on the specified <paramref name="id"/> from the store.
        /// </summary>
        /// <param name="id">The id of the entity to search for.</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.
        /// </param>
        /// <returns>
        /// The <see cref="Task{TResult}"/> that represents the asynchronous operation, containing the
        /// <typeparamref name="TEntity"/> matching the specified <paramref name="id"/> if it exists.
        /// </returns>
        public Task<TEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
}