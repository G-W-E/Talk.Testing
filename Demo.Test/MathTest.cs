namespace Demo.Test;

[TestFixture]
public class Tests
{
    private Math _math;
    [SetUp]
    public void Setup()
    {
        _math = new Math();
    }

    [Test]
    public void Add_IntParameters_ReturnsSum()
    {
        // Arrange
        float a = 5;
        float b = 3;
        // Act 
        var result = _math.Add(a,b);

        Assert.AreEqual(8,result);
    }

      [Test]
    public void Add_FloatParameters_ReturnsSum()
    {
        // Arrange
        int a = 5;
        int b = 3;
        // Act 
        var result = _math.Add(a,b);

        Assert.AreEqual(8f,result);
    }
    [Test] 
    public void ConvertToInt_Success()
    {
        // Arrange
        int org = 1000;
        // Act
        var result = _math.ConvertToInt(org);
        // Assert
        Assert.AreEqual(1000,result);
    }

     [Test] 
    public void ConvertToInt_UnSuccess()
    {
        // Arrange
        long org = 1000000000000000;
        // Act
        var result = _math.ConvertToInt(org);
        // Assert
        Assert.AreEqual(1000000000000000,result);
    }
}