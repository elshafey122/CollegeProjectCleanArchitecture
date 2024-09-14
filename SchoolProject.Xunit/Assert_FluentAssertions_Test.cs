
using FluentAssertions;
namespace CollegeProject.XUnitTest;
public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        int x = 5;
        int y = 3;
        int z;
        // Act
        z = x + y;
        //Assert
        Assert.Equal(8, z);
    }
    [Fact]
    public void Test1withfluentasseration()
    {
        // Arrange
        int x = 5;
        int y = 3;
        int z;
        // Act
        z = x + y;
        //Assert
        z.Should().Be(8,"sum 5 with 3 not equal 89"); // Assert.Equal(8, z);
    }

    [Fact]
    public void Test1shouldstartwithwe()
    {
        string word = "welcome";
        word.Should().StartWith("we");   // Assert.StartsWith("we", word);
    }
    [Fact]
    public void Test1shouldendwithme()
    {
        string word = "welcome";
        word.Should().EndWith("me");   //Assert.EndsWith("me", word);
    }

    [Fact]
    public void Test1shouldhaslength7()
    {
        string word = "welcome";
        word.Should().HaveLength(7);  
    }

    [Fact]
    public void Test1shouldhaslength7andstartwithweandendwithme()
    {
        string word = "welcome";
        word.Should().HaveLength(7).And.EndWith("me").And.StartWith("we"); 
    }
    [Fact]
    public void stringnotnullorempty()
    {
        string word = "welcome";
        word.Should().NotBeNullOrWhiteSpace(); //Assert.NotEmpty(word)
    }

    [Fact]
    public void string_should_type_string()
    {
        string word = "welcome";
        word.Should().BeOfType<string>(); //Assert.NotEmpty(word)
    }

    [Fact]
    public void verify_is_true()
    {
        bool word = true;
        word.Should().BeTrue(); //Assert.NotEmpty(word)
    }

    [Fact]
    public void verify_num_is_postive()
    {
        int x = 6;
        x.Should().BePositive();
        //x.Should().BeNegative();
        x.Should().BeGreaterThan(2);
        x.Should().BeGreaterThanOrEqualTo(2);
        x.Should().BeInRange(1,9);
    }
}
