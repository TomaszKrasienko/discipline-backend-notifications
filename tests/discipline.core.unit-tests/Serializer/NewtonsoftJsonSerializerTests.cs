using discipline.core.Serializer;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Shouldly;
using Xunit;

namespace discipline.core.unit_tests.Serializer;

public sealed class NewtonsoftJsonSerializerTests
{
    [Fact]
    public void ToJson_GivenObject_ShouldReturnSerializedObject()
    {
        //arrange
        var myObject = new { Test = "test" };

        //act
        var result = _serializer.ToJson(myObject);

        //assert
        result.ShouldBe("{\"Test\":\"test\"}");
    }

    [Fact]
    public void ToJson_GivenObjectWithNull_ShouldReturnSerializedObjectWithoutNullField()
    {
        //arrange
        var myObject = new TestObject();

        //act
        var result = _serializer.ToJson(myObject);

        //assert
        result.ShouldBe("{\"Test\":\"test\"}");
    }
    
    private class TestObject
    {
        public string Test { get; set; } = "test";
        public string? NullField { get; set; }
    }

    #region arrange

    private readonly ISerializer _serializer;

    public NewtonsoftJsonSerializerTests()
        => _serializer = new NewtonsoftJsonSerializer();

    #endregion
}