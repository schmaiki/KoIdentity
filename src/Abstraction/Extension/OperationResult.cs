// KoIdentity Copyright (C) 2021 Tekoding. All Rights Reserved.
// 
// Created: 2021.12.17 06:34
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

using System.Globalization;
using Tekoding.KoIdentity.Abstraction.Extension.Exception;

namespace Tekoding.KoIdentity.Abstraction.Extension;

/// <summary>
/// Represents the result of an operation within the Tekoding subsystem.
/// </summary>
public class OperationResult
{
    /// <summary>
    /// Flag indicating whether if the operation succeeded or not.
    /// </summary>
    /// <value>True if the operation succeeded, otherwise false.</value>
    private bool Succeeded { get; init; }

    /// <summary>
    /// Returns an <see cref="OperationResult"/> indicating the success of an operation.
    /// </summary>
    /// <returns>An <see cref="OperationResult"/> indicating the success of an operation.</returns>
    public static OperationResult Success => new() {Succeeded = true};

    /// <summary>
    /// An <see cref="IEnumerable{T}"/> of <see cref="Error"/> instances containing errors
    /// that occurred during the operation.
    /// </summary>
    /// <value>An <see cref="IEnumerable{T}"/> of <see cref="Error"/> instances.</value>
    private IEnumerable<Error>? Errors { get; init; }

    /// <summary>
    /// Creates an <see cref="OperationResult"/> indicating a failed entity operation,
    /// with a list of <paramref name="errors"/> if applicable.
    /// </summary>
    /// <param name="errors">An optional array of <see cref="Error"/>s which caused the operation to fail.</param>
    /// <returns>
    /// An <see cref="OperationResult"/> indicating a failed entity operation, with a list of <paramref name="errors"/>
    /// if applicable.
    /// </returns>
    public static OperationResult Failed(params Error[]? errors) => errors != null
        ? new OperationResult {Succeeded = false, Errors = errors}
        : new OperationResult {Succeeded = false};

    /// <summary>
    /// Converts the value of the current <see cref="OperationResult"/> object to its equivalent string representation.
    /// </summary>
    /// <returns>A string representation of the current <see cref="OperationResult"/> object.</returns>
    /// <remarks>
    /// If the operation was successful the ToString() will return "Succeeded" otherwise it returns
    /// "Failed : " followed by a comma delimited list of error codes from its <see cref="Errors"/> collection, if any.
    /// </remarks>
    public override string ToString()
    {
        if (Succeeded)
        {
            return "Succeeded";
        }

        if (Errors == null)
        {
            return "Failed without Errors";
        }

        return string.Format(CultureInfo.InvariantCulture, "{0} : {1}", "Failed",
            string.Join(",",
                "Error: " + Errors.Select(x => x.Code).ToList() + " Description: " +
                Errors.Select(x => x.Description).ToList()));
    }
}