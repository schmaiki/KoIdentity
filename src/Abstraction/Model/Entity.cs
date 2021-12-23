// KoIdentity Copyright (C) 2021 Tekoding. All Rights Reserved.
// 
// Created: 2021.12.17 05:05
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

using System.Diagnostics.CodeAnalysis;

namespace Tekoding.KoIdentity.Abstraction.Model;

/// <summary>
/// Provides an abstraction of entities.
/// </summary>
[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
public abstract class Entity
{
    /// <summary>
    /// The id of the <see cref="Entity"/>.
    /// </summary>
    public Guid Id { get;}
    /// <summary>
    /// The date, when the <see cref="Entity"/> was created.
    /// </summary>
    public DateTime CreationDate { get; }
        
    /// <summary>
    /// The date, when the <see cref="Entity"/> was changed.
    /// </summary>
    public DateTime ChangeDate { get; }
}