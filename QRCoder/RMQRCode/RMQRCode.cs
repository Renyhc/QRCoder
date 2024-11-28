#if SYSTEM_DRAWING
using System;
using System.Drawing;

namespace QRCoder;

#if NET6_0_OR_GREATER
[System.Runtime.Versioning.SupportedOSPlatform("windows")]
#endif
/// <summary>
/// Provides functionality to render rMQR codes as bitmap images.
/// </summary>
public class RMQRCode : IDisposable
{
    private readonly RMQRCodeData _rmqrCodeData;

    public RMQRCode(RMQRCodeData data)
    {
        _rmqrCodeData = data;
    }

    /// <summary>
    /// Creates a bitmap image of the rMQR code.
    /// </summary>
    /// <param name="pixelsPerModule">The size of each module in pixels</param>
    /// <param name="darkColor">The color of the dark modules</param>
    /// <param name="lightColor">The color of the light modules</param>
    /// <returns>A bitmap containing the rMQR code</returns>
    public Bitmap GetGraphic(int pixelsPerModule, Color darkColor, Color lightColor)
    {
        var moduleMatrix = _rmqrCodeData.ModuleMatrix;
        var width = moduleMatrix[0].Length * pixelsPerModule;
        var height = moduleMatrix.Count * pixelsPerModule;

        var bmp = new Bitmap(width, height);
        using (var gfx = Graphics.FromImage(bmp))
        using (var darkBrush = new SolidBrush(darkColor))
        using (var lightBrush = new SolidBrush(lightColor))
        {
            for (var x = 0; x < width; x += pixelsPerModule)
            {
                for (var y = 0; y < height; y += pixelsPerModule)
                {
                    var module = moduleMatrix[y / pixelsPerModule][x / pixelsPerModule];
                    var brush = module ? darkBrush : lightBrush;
                    gfx.FillRectangle(brush, x, y, pixelsPerModule, pixelsPerModule);
                }
            }
        }

        return bmp;
    }

    public void Dispose()
    {
        // Cleanup if needed
    }
}
#endif