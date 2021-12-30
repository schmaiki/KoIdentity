// KoIdentity Copyright (C) 2021 Tekoding. All Rights Reserved.
// 
// Created: 2021.12.28 18:16
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

using Tekoding.KoIdentity.Abstraction.Extension.Exception;
using Tekoding.KoIdentity.Abstraction.Store;
using Tekoding.KoIdentity.Abstraction.Test.Helper.Database;
using Tekoding.KoIdentity.Abstraction.Test.Helper.Model;

namespace Tekoding.KoIdentity.Abstraction.Test.Helper.Store;

internal class DefaultEntityStore : EntityStore<DatabaseContext, DefaultEntity>
{
    public DefaultEntityStore(DatabaseContext dbContext, ErrorDescriber errorDescriber) : base(dbContext,
        errorDescriber)
    {
    }
}