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
public sealed class OperationResult
{
    private readonly IEnumerable<Error>? _errors;
    
    /// <summary>
    /// Gets the state of the <see cref="OperationResult"/> indicating if the operation succeeded or not.
    /// </summary>
    /// <example><b>TRUE</b> when the operation succeeded, <b>FALSE</b> if not.</example>
    public bool State { get; }

    private OperationResult(bool succeeded, IEnumerable<Error>? errors = null)
    {
        State = succeeded;
        _errors = errors;
    }

    /// <summary>
    /// Returns an <see cref="OperationResult"/> indicating the success of an operation.
    /// </summary>
    /// <returns>An <see cref="OperationResult"/> indicating the success of an operation.</returns>
    public static OperationResult Success => new(true);

    /// <summary>
    /// Creates an <see cref="OperationResult"/> indicating a failed entity operation,
    /// with a list of <paramref name="errors"/> if applicable.
    /// </summary>
    /// <param name="errors">An optional array of <see cref="Error"/>s which caused the operation to fail.</param>
    /// <returns>
    /// An <see cref="OperationResult"/> indicating a failed entity operation, with a list of <paramref name="errors"/>
    /// if applicable.
    /// </returns>
    public static OperationResult Failed(params Error[]? errors) => new(false);

    /// <summary>
    /// Converts the value of the current <see cref="OperationResult"/> object to its equivalent string representation.
    /// </summary>
    /// <returns>A string representation of the current <see cref="OperationResult"/> object.</returns>
    /// <remarks>
    /// If the operation was successful the ToString() will return "Succeeded" otherwise it returns
    /// "Failed : " followed by a comma delimited list of error codes from its error collection, if any.
    /// </remarks>
    public override string ToString()
    {
        if (State)
        {
            return "Succeeded";
        }

        if (_errors == null)
        {
            return "Failed without Errors";
        }

        return string.Format(CultureInfo.InvariantCulture, "{0} : {1}", "Failed",
            string.Join(",", _errors.Select(x => x.Code).ToList()));
    }
}