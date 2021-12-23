// KoIdentity Copyright (C) 2021 Tekoding. All Rights Reserved.
// 
// Created: 2021.12.23 04:18
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

using Tekoding.KoIdentity.Abstraction.Model;

namespace Tekoding.KoIdentity.Core.Model;

/// <summary>
/// Provides the default implementation of an user within the KoIdentity eco system.
/// </summary>
public class User: Entity
{
    #nullable disable
    
    /// <summary>
    /// Gets or sets the e-mail address for this <see cref="User"/>.
    /// </summary>
    public string MailAddress { get; set; }
    
    /// <summary>
    /// Gets or sets the password for this <see cref="User"/>.
    /// </summary>
    public string Password { get; set; }
    
    #nullable restore
}