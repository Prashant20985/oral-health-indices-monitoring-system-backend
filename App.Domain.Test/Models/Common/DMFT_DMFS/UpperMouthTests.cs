using App.Domain.Models.Common.DMFT_DMFS;
using App.Domain.Models.Common.Tooth;

namespace App.Domain.Test.Models.Common.DMFT_DMFS;

public class UpperMouthTests
{
    [Fact]
    public void Properties_ShouldBeSetCorrectly()
    {
        // Arrange
        var tooth18 = new SixSurfaceTooth();
        var tooth17 = new SixSurfaceTooth();
        var tooth16 = new SixSurfaceTooth();
        var tooth15 = new SixSurfaceTooth();
        var tooth14 = new SixSurfaceTooth();
        var tooth13 = new FiveSurfaceToothDMFT_DMFS();
        var tooth12 = new FiveSurfaceToothDMFT_DMFS();
        var tooth11 = new FiveSurfaceToothDMFT_DMFS();
        var tooth21 = new FiveSurfaceToothDMFT_DMFS();
        var tooth22 = new FiveSurfaceToothDMFT_DMFS();
        var tooth23 = new FiveSurfaceToothDMFT_DMFS();
        var tooth24 = new SixSurfaceTooth();
        var tooth25 = new SixSurfaceTooth();
        var tooth26 = new SixSurfaceTooth();
        var tooth27 = new SixSurfaceTooth();
        var tooth28 = new SixSurfaceTooth();
        var tooth55 = "tooth55";
        var tooth54 = "tooth54";
        var tooth53 = "tooth53";
        var tooth52 = "tooth52";
        var tooth51 = "tooth51";
        var tooth61 = "tooth61";
        var tooth62 = "tooth62";
        var tooth63 = "tooth63";
        var tooth64 = "tooth64";
        var tooth65 = "tooth65";

        var upperMouth = new UpperMouth
        {
            Tooth_18 = tooth18,
            Tooth_17 = tooth17,
            Tooth_16 = tooth16,
            Tooth_15 = tooth15,
            Tooth_14 = tooth14,
            Tooth_13 = tooth13,
            Tooth_12 = tooth12,
            Tooth_11 = tooth11,
            Tooth_21 = tooth21,
            Tooth_22 = tooth22,
            Tooth_23 = tooth23,
            Tooth_24 = tooth24,
            Tooth_25 = tooth25,
            Tooth_26 = tooth26,
            Tooth_27 = tooth27,
            Tooth_28 = tooth28,
            Tooth_55 = tooth55,
            Tooth_54 = tooth54,
            Tooth_53 = tooth53,
            Tooth_52 = tooth52,
            Tooth_51 = tooth51,
            Tooth_61 = tooth61,
            Tooth_62 = tooth62,
            Tooth_63 = tooth63,
            Tooth_64 = tooth64,
            Tooth_65 = tooth65
        };

        // Assert
        Assert.Equal(tooth18, upperMouth.Tooth_18);
        Assert.Equal(tooth17, upperMouth.Tooth_17);
        Assert.Equal(tooth16, upperMouth.Tooth_16);
        Assert.Equal(tooth15, upperMouth.Tooth_15);
        Assert.Equal(tooth14, upperMouth.Tooth_14);
        Assert.Equal(tooth13, upperMouth.Tooth_13);
        Assert.Equal(tooth12, upperMouth.Tooth_12);
        Assert.Equal(tooth11, upperMouth.Tooth_11);
        Assert.Equal(tooth21, upperMouth.Tooth_21);
        Assert.Equal(tooth22, upperMouth.Tooth_22);
        Assert.Equal(tooth23, upperMouth.Tooth_23);
        Assert.Equal(tooth24, upperMouth.Tooth_24);
        Assert.Equal(tooth25, upperMouth.Tooth_25);
        Assert.Equal(tooth26, upperMouth.Tooth_26);
        Assert.Equal(tooth27, upperMouth.Tooth_27);
        Assert.Equal(tooth28, upperMouth.Tooth_28);
        Assert.Equal(tooth55, upperMouth.Tooth_55);
        Assert.Equal(tooth54, upperMouth.Tooth_54);
        Assert.Equal(tooth53, upperMouth.Tooth_53);
        Assert.Equal(tooth52, upperMouth.Tooth_52);
        Assert.Equal(tooth51, upperMouth.Tooth_51);
        Assert.Equal(tooth61, upperMouth.Tooth_61);
        Assert.Equal(tooth62, upperMouth.Tooth_62);
        Assert.Equal(tooth63, upperMouth.Tooth_63);
        Assert.Equal(tooth64, upperMouth.Tooth_64);
        Assert.Equal(tooth65, upperMouth.Tooth_65);
    }

    [Fact]
    public void Properties_ShouldBeNullByDefault()
    {
        // Arrange
        var upperMouth = new UpperMouth();

        // Assert
        Assert.Null(upperMouth.Tooth_18);
        Assert.Null(upperMouth.Tooth_17);
        Assert.Null(upperMouth.Tooth_16);
        Assert.Null(upperMouth.Tooth_15);
        Assert.Null(upperMouth.Tooth_14);
        Assert.Null(upperMouth.Tooth_13);
        Assert.Null(upperMouth.Tooth_12);
        Assert.Null(upperMouth.Tooth_11);
        Assert.Null(upperMouth.Tooth_21);
        Assert.Null(upperMouth.Tooth_22);
        Assert.Null(upperMouth.Tooth_23);
        Assert.Null(upperMouth.Tooth_24);
        Assert.Null(upperMouth.Tooth_25);
        Assert.Null(upperMouth.Tooth_26);
        Assert.Null(upperMouth.Tooth_27);
        Assert.Null(upperMouth.Tooth_28);
        Assert.Null(upperMouth.Tooth_55);
        Assert.Null(upperMouth.Tooth_54);
        Assert.Null(upperMouth.Tooth_53);
        Assert.Null(upperMouth.Tooth_52);
        Assert.Null(upperMouth.Tooth_51);
        Assert.Null(upperMouth.Tooth_61);
        Assert.Null(upperMouth.Tooth_62);
        Assert.Null(upperMouth.Tooth_63);
        Assert.Null(upperMouth.Tooth_64);
        Assert.Null(upperMouth.Tooth_65);
    }
}