// KoIdentity Copyright (C) 2021 Tekoding. All Rights Reserved.
// 
// Created: 2021.12.29 17:12
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


using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Tekoding.KoIdentity.Abstraction.Extension.Exception;
using Tekoding.KoIdentity.Abstraction.Test.Helper;
using Tekoding.KoIdentity.Abstraction.Test.Helper.Database;
using Tekoding.KoIdentity.Abstraction.Test.Helper.Model;
using Tekoding.KoIdentity.Abstraction.Test.Helper.Store;

namespace Tekoding.KoIdentity.Abstraction.Test;

public class EntityTests
{
#nullable disable
    private ErrorDescriber _errorDescriber;
    private DbContextOptions _dbContextOptions;
# nullable restore

    #region SetUp & TearDown

    [SetUp]
    public async Task Setup()
    {
        _errorDescriber = new ErrorDescriber();

        _dbContextOptions = new DbContextOptionsBuilder<DatabaseContext<DefaultEntity>>()
            .UseSqlServer(
                Environment.GetEnvironmentVariable("AbstractionUnitTestConnectionString") ??
                throw new InvalidOperationException())
            .Options;

        await DatabaseMocker.LoadDatabase(_dbContextOptions);
    }

    [TearDown]
    public async Task Teardown()
    {
        await DatabaseMocker.ResetDatabase(_dbContextOptions);
    }

    #endregion
    
    [Test]
    public async Task CreateAsync_ThrowsArgumentNullExceptionIfNoEntityIsProvided()
    {
        await using var ctx = new DatabaseContext(_dbContextOptions);
        var entityStore = new DefaultEntityStore(ctx, _errorDescriber);

        var mainEntitiesCountBeforeCreation = await ctx.MainEntities.CountAsync();

#pragma warning disable CS8625
        await FluentActions.Invoking(() => entityStore.CreateAsync(null)).Should().ThrowAsync<ArgumentNullException>();
#pragma warning restore CS8625

        var mainEntitiesCountAfterCreation = await ctx.MainEntities.CountAsync();
        mainEntitiesCountAfterCreation.Should().Be(mainEntitiesCountBeforeCreation);

        await DatabaseMocker.ResetDatabase(_dbContextOptions);
    }
    
    [Test]
    public async Task CreateAsync_SuccessIfEntityIsCreated()
    {
        await using var ctx = new DatabaseContext(_dbContextOptions);
        var entityStore = new DefaultEntityStore(ctx, _errorDescriber);

        var mainEntitiesCountBeforeCreation = await ctx.MainEntities.CountAsync();
        var creationResult = await entityStore.CreateAsync(Constant.NotExistingEntity);
        var mainEntitiesCountAfterCreation = await ctx.MainEntities.CountAsync();
        
        creationResult.State.Should().BeTrue();
        creationResult.ErrorCount.Should().Be(0);
        mainEntitiesCountAfterCreation.Should().Be(mainEntitiesCountBeforeCreation + 1);

        await DatabaseMocker.ResetDatabase(_dbContextOptions);
    }
    
    [Test]
    public async Task UpdateAsync_ThrowsArgumentNullExceptionIfNoEntityIsProvided()
    {
        await using var ctx = new DatabaseContext(_dbContextOptions);
        var entityStore = new DefaultEntityStore(ctx, _errorDescriber);

        var mainEntitiesCountBeforeUpdate = await ctx.MainEntities.CountAsync();
        
#pragma warning disable CS8625
        await FluentActions.Invoking(() => entityStore.UpdateAsync(null)).Should().ThrowAsync<ArgumentNullException>();
#pragma warning restore CS8625
        
        var mainEntitiesCountAfterUpdate = await ctx.MainEntities.CountAsync();
        
        mainEntitiesCountAfterUpdate.Should().Be(mainEntitiesCountBeforeUpdate);

        await DatabaseMocker.ResetDatabase(_dbContextOptions);
    }
    
    [Test]
    public async Task UpdateAsync_ReturnsFailedWithEntityStateNotModifiedErrorIfEntityDoesNotExist()
    {
        await using var ctx = new DatabaseContext(_dbContextOptions);
        var entityStore = new DefaultEntityStore(ctx, _errorDescriber);

        var mainEntitiesCountBeforeUpdate = await ctx.MainEntities.CountAsync();
        var entityToBeUpdated = new DefaultEntity();
        var updateResult = await entityStore.UpdateAsync(entityToBeUpdated);
        var mainEntitiesCountAfterUpdate = await ctx.MainEntities.CountAsync();

        updateResult.State.Should().BeFalse();
        updateResult.ErrorCount.Should().Be(1);
        updateResult.ToString().Should().Contain("EntityStateUnmodified");
        mainEntitiesCountAfterUpdate.Should().Be(mainEntitiesCountBeforeUpdate);

        await DatabaseMocker.ResetDatabase(_dbContextOptions);
    }
    
    [Test]
    public async Task DeleteAsync_ThrowsArgumentNullExceptionIfNoEntityIsProvided()
    {
        await using var ctx = new DatabaseContext(_dbContextOptions);
        var entityStore = new DefaultEntityStore(ctx, _errorDescriber);

        var mainEntitiesCountBeforeRemoval = await ctx.MainEntities.CountAsync();
        
#pragma warning disable CS8625
        await FluentActions.Invoking(() => entityStore.DeleteAsync(null)).Should().ThrowAsync<ArgumentNullException>();
#pragma warning restore CS8625
        
        var mainEntitiesCountAfterRemoval = await ctx.MainEntities.CountAsync();
        
        mainEntitiesCountAfterRemoval.Should().Be(mainEntitiesCountBeforeRemoval);

        await DatabaseMocker.ResetDatabase(_dbContextOptions);
    }
    
    [Test]
    public async Task DeleteAsync_ReturnsFailedWithConcurrencyFailureIfEntityDoesNotExist()
    {
        await using var ctx = new DatabaseContext(_dbContextOptions);
        var entityStore = new DefaultEntityStore(ctx, _errorDescriber);

        var mainEntitiesCountBeforeRemoval = await ctx.MainEntities.CountAsync();
        var removalResult = await entityStore.DeleteAsync(Constant.NotExistingEntity);
        var mainEntitiesCountAfterRemoval = await ctx.MainEntities.CountAsync();

        removalResult.State.Should().BeFalse();
        removalResult.ErrorCount.Should().Be(1);
        removalResult.ToString().Should().Contain("ConcurrencyFailure");
        mainEntitiesCountAfterRemoval.Should().Be(mainEntitiesCountBeforeRemoval);

        await DatabaseMocker.ResetDatabase(_dbContextOptions);
    }

    [Test]
    public async Task DeleteAsync_SuccessIfEntityIsDeleted()
    {
        await using var ctx = new DatabaseContext(_dbContextOptions);
        var entityStore = new DefaultEntityStore(ctx, _errorDescriber);

        await entityStore.CreateAsync(Constant.NotExistingEntity);
        var mainEntitiesCountBeforeDeletion = await ctx.MainEntities.CountAsync();
        var removalResult = await entityStore.DeleteAsync(Constant.NotExistingEntity);
        var mainEntitiesCountAfterDeletion = await ctx.MainEntities.CountAsync();

        removalResult.State.Should().BeTrue();
        removalResult.ErrorCount.Should().Be(0);
        mainEntitiesCountAfterDeletion.Should().Be(mainEntitiesCountBeforeDeletion - 1);
        
        await DatabaseMocker.ResetDatabase(_dbContextOptions);
    }

    [Test]
    public async Task FindByIdAsync_ThrowsInvalidOperationException()
    {
        await using var ctx = new DatabaseContext(_dbContextOptions);
        var entityStore = new DefaultEntityStore(ctx, _errorDescriber);

        var mainEntitiesBeforeGettingById = await ctx.MainEntities.CountAsync();
#pragma warning disable CS8625
        await FluentActions.Invoking(() => entityStore.FindByIdAsync(Guid.Empty)).Should().ThrowAsync<InvalidOperationException>();
#pragma warning restore CS8625
        var mainEntitiesAfterGettingById = await ctx.MainEntities.CountAsync();

        mainEntitiesBeforeGettingById.Should().Be(mainEntitiesAfterGettingById);

        await DatabaseMocker.ResetDatabase(_dbContextOptions);
    }

    [Test]
    public async Task FindByIdAsync_ReturnsNullIfEntityWithDesiredIdDoesNotExist()
    {
        await using var ctx = new DatabaseContext(_dbContextOptions);
        var entityStore = new DefaultEntityStore(ctx, _errorDescriber);

        var mainEntitiesBeforeGettingById = await ctx.MainEntities.CountAsync();
        var entity = await entityStore.FindByIdAsync(Guid.NewGuid());
        var mainEntitiesAfterGettingById = await ctx.MainEntities.CountAsync();

        entity.Should().BeNull();
        mainEntitiesBeforeGettingById.Should().Be(mainEntitiesAfterGettingById);

        await DatabaseMocker.ResetDatabase(_dbContextOptions);
    }

    [Test]
    public async Task FindByIdAsync_ReturnsEntityIfEntityWithDesiredIdWasFound()
    {
        await using var ctx = new DatabaseContext(_dbContextOptions);
        var entityStore = new DefaultEntityStore(ctx, _errorDescriber);
        
        var entityToCreate = new DefaultEntity();
        await entityStore.CreateAsync(entityToCreate);
        var mainEntitiesBeforeGettingById = await ctx.MainEntities.CountAsync();
        var entity = await entityStore.FindByIdAsync(entityToCreate.Id);
        var mainEntitiesAfterGettingById = await ctx.MainEntities.CountAsync();

        entity.Should().NotBeNull();
        entity?.Id.Should().Be(entityToCreate.Id);
        mainEntitiesBeforeGettingById.Should().Be(mainEntitiesAfterGettingById);

        await DatabaseMocker.ResetDatabase(_dbContextOptions);
    }
}