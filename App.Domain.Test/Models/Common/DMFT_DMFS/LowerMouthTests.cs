using App.Domain.Models.Common.DMFT_DMFS;
using App.Domain.Models.Common.Tooth;

namespace App.Domain.Test.Models.Common.DMFT_DMFS;

public class LowerMouthTests
{
    [Fact]
    public void Properties_ShouldBeSetCorrectly()
    {
        // Arrange
        var tooth48 = new SixSurfaceTooth();
        var tooth47 = new SixSurfaceTooth();
        var tooth46 = new SixSurfaceTooth();
        var tooth45 = new SixSurfaceTooth();
        var tooth44 = new SixSurfaceTooth();
        var tooth43 = new FiveSurfaceToothDMFT_DMFS();
        var tooth42 = new FiveSurfaceToothDMFT_DMFS();
        var tooth41 = new FiveSurfaceToothDMFT_DMFS();
        var tooth31 = new FiveSurfaceToothDMFT_DMFS();
        var tooth32 = new FiveSurfaceToothDMFT_DMFS();
        var tooth33 = new FiveSurfaceToothDMFT_DMFS();
        var tooth34 = new SixSurfaceTooth();
        var tooth35 = new SixSurfaceTooth();
        var tooth36 = new SixSurfaceTooth();
        var tooth37 = new SixSurfaceTooth();
        var tooth38 = new SixSurfaceTooth();
        var tooth85 = "tooth85";
        var tooth84 = "tooth84";
        var tooth83 = "tooth83";
        var tooth82 = "tooth82";
        var tooth81 = "tooth81";
        var tooth71 = "tooth71";
        var tooth72 = "tooth72";
        var tooth73 = "tooth73";
        var tooth74 = "tooth74";
        var tooth75 = "tooth75";

        var lowerMouth = new LowerMouth
        {
            Tooth_48 = tooth48,
            Tooth_47 = tooth47,
            Tooth_46 = tooth46,
            Tooth_45 = tooth45,
            Tooth_44 = tooth44,
            Tooth_43 = tooth43,
            Tooth_42 = tooth42,
            Tooth_41 = tooth41,
            Tooth_31 = tooth31,
            Tooth_32 = tooth32,
            Tooth_33 = tooth33,
            Tooth_34 = tooth34,
            Tooth_35 = tooth35,
            Tooth_36 = tooth36,
            Tooth_37 = tooth37,
            Tooth_38 = tooth38,
            Tooth_85 = tooth85,
            Tooth_84 = tooth84,
            Tooth_83 = tooth83,
            Tooth_82 = tooth82,
            Tooth_81 = tooth81,
            Tooth_71 = tooth71,
            Tooth_72 = tooth72,
            Tooth_73 = tooth73,
            Tooth_74 = tooth74,
            Tooth_75 = tooth75
        };

        // Assert
        Assert.Equal(tooth48, lowerMouth.Tooth_48);
        Assert.Equal(tooth47, lowerMouth.Tooth_47);
        Assert.Equal(tooth46, lowerMouth.Tooth_46);
        Assert.Equal(tooth45, lowerMouth.Tooth_45);
        Assert.Equal(tooth44, lowerMouth.Tooth_44);
        Assert.Equal(tooth43, lowerMouth.Tooth_43);
        Assert.Equal(tooth42, lowerMouth.Tooth_42);
        Assert.Equal(tooth41, lowerMouth.Tooth_41);
        Assert.Equal(tooth31, lowerMouth.Tooth_31);
        Assert.Equal(tooth32, lowerMouth.Tooth_32);
        Assert.Equal(tooth33, lowerMouth.Tooth_33);
        Assert.Equal(tooth34, lowerMouth.Tooth_34);
        Assert.Equal(tooth35, lowerMouth.Tooth_35);
        Assert.Equal(tooth36, lowerMouth.Tooth_36);
        Assert.Equal(tooth37, lowerMouth.Tooth_37);
        Assert.Equal(tooth38, lowerMouth.Tooth_38);
        Assert.Equal(tooth85, lowerMouth.Tooth_85);
        Assert.Equal(tooth84, lowerMouth.Tooth_84);
        Assert.Equal(tooth83, lowerMouth.Tooth_83);
        Assert.Equal(tooth82, lowerMouth.Tooth_82);
        Assert.Equal(tooth81, lowerMouth.Tooth_81);
        Assert.Equal(tooth71, lowerMouth.Tooth_71);
        Assert.Equal(tooth72, lowerMouth.Tooth_72);
        Assert.Equal(tooth73, lowerMouth.Tooth_73);
        Assert.Equal(tooth74, lowerMouth.Tooth_74);
        Assert.Equal(tooth75, lowerMouth.Tooth_75);
    }

    [Fact]
    public void Properties_ShouldBeNullByDefault()
    {
        // Arrange
        var lowerMouth = new LowerMouth();

        // Assert
        Assert.Null(lowerMouth.Tooth_48);
        Assert.Null(lowerMouth.Tooth_47);
        Assert.Null(lowerMouth.Tooth_46);
        Assert.Null(lowerMouth.Tooth_45);
        Assert.Null(lowerMouth.Tooth_44);
        Assert.Null(lowerMouth.Tooth_43);
        Assert.Null(lowerMouth.Tooth_42);
        Assert.Null(lowerMouth.Tooth_41);
        Assert.Null(lowerMouth.Tooth_31);
        Assert.Null(lowerMouth.Tooth_32);
        Assert.Null(lowerMouth.Tooth_33);
        Assert.Null(lowerMouth.Tooth_34);
        Assert.Null(lowerMouth.Tooth_35);
        Assert.Null(lowerMouth.Tooth_36);
        Assert.Null(lowerMouth.Tooth_37);
        Assert.Null(lowerMouth.Tooth_38);
        Assert.Null(lowerMouth.Tooth_85);
        Assert.Null(lowerMouth.Tooth_84);
        Assert.Null(lowerMouth.Tooth_83);
        Assert.Null(lowerMouth.Tooth_82);
        Assert.Null(lowerMouth.Tooth_81);
        Assert.Null(lowerMouth.Tooth_71);
        Assert.Null(lowerMouth.Tooth_72);
        Assert.Null(lowerMouth.Tooth_73);
        Assert.Null(lowerMouth.Tooth_74);
        Assert.Null(lowerMouth.Tooth_75);
    }
}