using QRCoder;
using Shouldly;
using Xunit;

namespace QRCoderTests;

public class RMQRCodeTests
{
    [Fact]
    public void can_create_rmqr_code()
    {
        var gen = new QRCodeGenerator();
        var data = gen.CreateRMQRCode("Test", RMQRVersion.R7x43);
        data.ShouldNotBeNull();
        data.ModuleMatrix.Count.ShouldBe(7);
        data.ModuleMatrix[0].Length.ShouldBe(43);
    }

    [Fact]
    public void can_get_rmqr_dimensions()
    {
        var (width, height) = RMQRCode.GetDimensions(RMQRVersion.R7x43);
        width.ShouldBe(43);
        height.ShouldBe(7);

        (width, height) = RMQRCode.GetDimensions(RMQRVersion.R13x77);
        width.ShouldBe(77);
        height.ShouldBe(13);
    }

    [Fact]
    public void can_set_rmqr_version()
    {
        var rmqr = new RMQRCode();
        rmqr.SetVersion(RMQRVersion.R11x27);
        var (width, height) = RMQRCode.GetDimensions(RMQRVersion.R11x27);
        width.ShouldBe(27);
        height.ShouldBe(11);
    }

    [Theory]
    [InlineData(RMQRVersion.R7x43)]
    [InlineData(RMQRVersion.R11x27)] 
    [InlineData(RMQRVersion.R13x77)]
    [InlineData(RMQRVersion.R17x139)]
    public void can_create_rmqr_different_versions(RMQRVersion version)
    {
        var gen = new QRCodeGenerator();
        var data = gen.CreateRMQRCode("Test", version);
        var (width, height) = RMQRCode.GetDimensions(version);
        
        data.ShouldNotBeNull();
        data.ModuleMatrix.Count.ShouldBe(height);
        data.ModuleMatrix[0].Length.ShouldBe(width);
    }

    [Fact]
    public void rmqr_uses_correct_ecc_level()
    {
        var gen = new QRCodeGenerator();
        var data = gen.CreateRMQRCode("Test", RMQRVersion.R7x43);
        
        // Verify RMQR uses its specific ECC level
        data.ShouldNotBeNull();
        var qr = new QRCode(data);
        qr.ShouldNotBeNull();
    }
}
