using discipline.core.Persistence.Documents;
using discipline.core.Persistence.Documents.Mappers;
using discipline.tests.shared.Documents;
using discipline.tests.shared.Entities;
using Shouldly;
using Xunit;

namespace discipline.core.unit_tests.Persistence.Documents.Mappers;

public sealed class NotificationDefinitionDocumentMapperExtensionsTests
{
    [Fact]
    public void AsEntity_GivenNotificationDefinitionDocument_ShouldReturnNotificationDefinition()
    {
        //arrange
        var document = NotificationDefinitionDocumentFactory.Get();
        
        //act
        var result = document.AsEntity();
        
        //assert
        result.Id.Value.ShouldBe(document.Id);
        result.Context!.Value.ShouldBe(document.Context);
        result.Title!.Value.ShouldBe(document.Title);
        result.Content!.Value.ShouldBe(document.Content);
        result.Content!.ParamCount.ShouldBe(0);
    }
    
    [Fact]
    public void AsDocument_GivenNotificationDefinition_ShouldReturnNotificationDefinitionDocument()
    {
        //arrange
        var document = NotificationDefinitionFactory.Get();
        
        //act
        var result = document.AsDocument();
        
        //assert
        result.Id.ShouldBe(document.Id.Value);
        result.Context.ShouldBe(document.Context!.Value);
        result.Title.ShouldBe(document.Title!.Value);
        result.Content.ShouldBe(document.Content!.Value);
    }
}