// KoIdentity Copyright (C) 2021 Tekoding. All Rights Reserved.
// 
// Created: 2021.12.17 06:18
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

namespace Tekoding.KoIdentity.Abstraction.Extension.Exception;

/// <summary>
/// Encapsulates an error used within the Tekoding subsystem.
/// </summary>
public struct Error
{
    /// <summary>
    /// Gets or sets the code for this error.
    /// </summary>
    /// <value>
    /// The code for this error.
    /// </value>
    public string Code { get; init; }

    /// <summary>
    /// Gets or sets the description for this error.
    /// </summary>
    /// <value>
    /// The description for this error.
    /// </value>
    public string Description { get; init; }
}