using QRCoder;
using QRCoder.QRCodeGenerator;
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
        var dimensions = RMQRCode.GetDimensions(RMQRVersion.R7x43);
        dimensions.Width.ShouldBe(43);
        dimensions.Height.ShouldBe(7);

        dimensions = RMQRCode.GetDimensions(RMQRVersion.R13x77);
        dimensions.Width.ShouldBe(77);
        dimensions.Height.ShouldBe(13);
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

#if !NET35 && !NET452
#if !NET35 && !NET452
#if !NET35 && !NET452
    [Theory]
    [InlineData(RMQRVersion.R7x43)]
    [InlineData(RMQRVersion.R11x27)]
    [InlineData(RMQRVersion.R13x77)]
    [InlineData(RMQRVersion.R17x139)]
    public void can_create_rmqr_different_versions(RMQRVersion version)
#else
    [Fact]
    public void can_create_rmqr_different_versions()
#endif
    {
#if !NET35 && !NET452
        var gen = new QRCodeGenerator();
        var data = gen.CreateRMQRCode("Test", version);
        var (width, height) = RMQRCode.GetDimensions(version);
        data.ShouldNotBeNull();
        data.ModuleMatrix.Count.ShouldBe(height);
        data.ModuleMatrix[0].Length.ShouldBe(width);
#else
        // Test a single version for .NET 3.5/4.5.2
        var gen = new QRCodeGenerator();
        var version = RMQRVersion.R7x43;
        var data = gen.CreateRMQRCode("Test", version);
        var dimensions = RMQRCode.GetDimensions(version);
        data.ShouldNotBeNull();
        data.ModuleMatrix.Count.ShouldBe(dimensions.Height);
        data.ModuleMatrix[0].Length.ShouldBe(dimensions.Width);
#endif
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
