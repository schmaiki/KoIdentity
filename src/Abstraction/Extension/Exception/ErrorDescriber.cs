// KoIdentity Copyright (C) 2021 Tekoding. All Rights Reserved.
// 
// Created: 2021.12.17 06:22
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
/// Service to enable localization for applications within the Tekoding subsystem facing errors.
/// </summary>
/// <remarks>
/// These errors are returned to controllers or services and are generally used to display messages to end users.
/// </remarks>
public class ErrorDescriber
{
    /// <summary>
    /// Returns the default <see cref="Error"/>.
    /// </summary>
    /// <returns>The default <see cref="Error"/>.</returns>
    public virtual Error DefaultError()
    {
        return new Error
        {
            Code = nameof(DefaultError),
            Description = ErrorResources.UnknownError,
        };
    }

    /// <summary>
    /// Returns an <see cref="Error"/> indicating a concurrency failure.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating a concurrency failure.</returns>
    public virtual Error ConcurrencyFailure()
    {
        return new Error
        {
            Code = nameof(ConcurrencyFailure),
            Description = ErrorResources.ConcurrencyFailure
        };
    }
}