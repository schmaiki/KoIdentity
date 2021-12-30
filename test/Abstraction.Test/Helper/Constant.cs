// KoIdentity Copyright (C) 2021 Tekoding. All Rights Reserved.
// 
// Created: 2021.12.28 18:03
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

using System.Collections.Generic;
using Tekoding.KoIdentity.Abstraction.Test.Helper.Model;

namespace Tekoding.KoIdentity.Abstraction.Test.Helper;

internal static class Constant
{
    internal static List<DefaultEntity> DatabaseMainEntities { get; set; } = new();

    internal static readonly DefaultEntity NotExistingEntity = new();
}