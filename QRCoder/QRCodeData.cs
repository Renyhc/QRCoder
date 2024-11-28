using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Drawing; // Added for Bitmap, Color, Graphics, SolidBrush

namespace QRCoder;

/// <summary>
/// Represents the data structure of a QR code.
/// </summary>
public class QRCodeData : IDisposable
{
    /// <summary>
    /// Gets or sets the module matrix of the QR code.
    /// </summary>
    public List<BitArray> ModuleMatrix { get; set; }

    /// <summary>
    /// Gets or sets the encoding mode of the QR code.
    /// </summary>
    public EncodingMode Mode { get; set; } // New property to specify encoding mode

    /// <summary>
    /// Specifies the encoding mode of the QR code.
    /// </summary>
    public enum EncodingMode
    {
        Numeric,
        Alphanumeric,
        Byte,
        Kanji,
        RMQR // New encoding mode for rMQR
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="QRCodeData"/> class with the specified version.
    /// </summary>
    /// <param name="version">The version of the QR code.</param>
    public QRCodeData(int version)
    {
        Version = version;
        var size = ModulesPerSideFromVersion(version);
        ModuleMatrix = new List<BitArray>(size);
        for (var i = 0; i < size; i++)
            ModuleMatrix.Add(new BitArray(size));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="QRCodeData"/> class with raw data from a specified path and compression mode.
    /// </summary>
    /// <param name="pathToRawData">The path to the raw data file.</param>
    /// <param name="compressMode">The compression mode used for the raw data.</param>
    public QRCodeData(string pathToRawData, Compression compressMode) : this(File.ReadAllBytes(pathToRawData), compressMode)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="QRCodeData"/> class with raw data and compression mode.
    /// </summary>
    /// <param name="rawData">The raw data of the QR code.</param>
    /// <param name="compressMode">The compression mode used for the raw data.</param>
    public QRCodeData(byte[] rawData, Compression compressMode)
    {
        var bytes = new List<byte>(rawData);

        //Decompress
        if (compressMode == Compression.Deflate)
        {
            using var input = new MemoryStream(bytes.ToArray());
            using var output = new MemoryStream();
            using (var dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }
            bytes = new List<byte>(output.ToArray());
        }
        else if (compressMode == Compression.GZip)
        {
            using var input = new MemoryStream(bytes.ToArray());
            using var output = new MemoryStream();
            using (var dstream = new GZipStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }
            bytes = new List<byte>(output.ToArray());
        }

        if (bytes[0] != 0x51 || bytes[1] != 0x52 || bytes[2] != 0x52)
            throw new Exception("Invalid raw data file. Filetype doesn't match \"QRR\".");

        //Set QR code version
        var sideLen = (int)bytes[4];
        bytes.RemoveRange(0, 5);
        Version = (sideLen - 21 - 8) / 4 + 1;

        //Unpack
        var modules = new Queue<bool>(8 * bytes.Count);
        foreach (var b in bytes)
        {
            var bArr = new BitArray(new byte[] { b });
            for (int i = 7; i >= 0; i--)
            {
                modules.Enqueue((b & (1 << i)) != 0);
            }
        }

        //Build module matrix
        ModuleMatrix = new List<BitArray>(sideLen);
        for (int y = 0; y < sideLen; y++)
        {
            ModuleMatrix.Add(new BitArray(sideLen));
            for (int x = 0; x < sideLen; x++)
            {
                ModuleMatrix[y][x] = modules.Dequeue();
            }
        }

    }

    /// <summary>
    /// Gets the raw data of the QR code with the specified compression mode.
    /// </summary>
    /// <param name="compressMode">The compression mode used for the raw data.</param>
    /// <returns>Returns the raw data of the QR code as a byte array.</returns>
    public byte[] GetRawData(Compression compressMode)
    {
        var bytes = new List<byte>();

        //Add header - signature ("QRR")
        bytes.AddRange(new byte[] { 0x51, 0x52, 0x52, 0x00 });

        //Add header - rowsize
        bytes.Add((byte)ModuleMatrix.Count);

        //Build data queue
        var dataQueue = new Queue<int>();
        foreach (var row in ModuleMatrix)
        {
            foreach (var module in row)
            {
                dataQueue.Enqueue((bool)module ? 1 : 0);
            }
        }
        for (int i = 0; i < 8 - (ModuleMatrix.Count * ModuleMatrix.Count) % 8; i++)
        {
            dataQueue.Enqueue(0);
        }

        //Process queue
        while (dataQueue.Count > 0)
        {
            byte b = 0;
            for (int i = 7; i >= 0; i--)
            {
                b += (byte)(dataQueue.Dequeue() << i);
            }
            bytes.Add(b);
        }
        var rawData = bytes.ToArray();

        //Compress stream (optional)
        if (compressMode == Compression.Deflate)
        {
            using var output = new MemoryStream();
            using (var dstream = new DeflateStream(output, CompressionMode.Compress))
            {
                dstream.Write(rawData, 0, rawData.Length);
            }
            rawData = output.ToArray();
        }
        else if (compressMode == Compression.GZip)
        {
            using var output = new MemoryStream();
            using (var gzipStream = new GZipStream(output, CompressionMode.Compress, true))
            {
                gzipStream.Write(rawData, 0, rawData.Length);
            }
            rawData = output.ToArray();
        }
        return rawData;
    }

    /// <summary>
    /// Saves the raw data of the QR code to a specified file with the specified compression mode.
    /// </summary>
    /// <param name="filePath">The path to the file where the raw data will be saved.</param>
    /// <param name="compressMode">The compression mode used for the raw data.</param>
    public void SaveRawData(string filePath, Compression compressMode)
        => File.WriteAllBytes(filePath, GetRawData(compressMode));

    /// <summary>
    /// Gets the version of the QR code.
    /// </summary>
    public int Version { get; private set; }

    /// <summary>
    /// Gets the number of modules per side from the specified version.
    /// </summary>
    /// <param name="version">The version of the QR code.</param>
    /// <returns>Returns the number of modules per side.</returns>
    private static int ModulesPerSideFromVersion(int version)
        => 21 + (version - 1) * 4;

    /// <summary>
    /// Releases all resources used by the <see cref="QRCodeData"/>.
    /// </summary>
    public void Dispose()
    {
        ModuleMatrix = null!;
        Version = 0;

    }

    /// <summary>
    /// Specifies the compression mode used for the raw data.
    /// </summary>
    public enum Compression
    {
        /// <summary>
        /// No compression.
        /// </summary>
        Uncompressed,
        /// <summary>
        /// Deflate compression.
        /// </summary>
        Deflate,
        /// <summary>
        /// GZip compression.
        /// </summary>
        GZip
    }

    /// <summary>
    /// Generates a graphic representation of the QR code.
    /// </summary>
    /// <param name="pixelsPerModule">The number of pixels per module.</param>
    /// <param name="darkColor">The color of the dark modules.</param>
    /// <param name="lightColor">The color of the light modules.</param>
    /// <param name="drawQuietZones">Indicates whether to draw quiet zones around the QR code.</param>
    /// <returns>Returns a Bitmap representing the QR code.</returns>
    public Bitmap GetGraphic(int pixelsPerModule, Color darkColor, Color lightColor, bool drawQuietZones = true)
    {
        var size = (ModuleMatrix.Count - (drawQuietZones ? 0 : 8)) * pixelsPerModule;
        var offset = drawQuietZones ? 0 : 4 * pixelsPerModule;

        var bmp = new Bitmap(size, size);
        using (var gfx = Graphics.FromImage(bmp))
        using (var lightBrush = new SolidBrush(lightColor))
        using (var darkBrush = new SolidBrush(darkColor))
        {
            if (Mode == EncodingMode.RMQR)
            {
                // Adjust rendering logic for rMQR specific module matrix
            }

            for (var x = 0; x < size + offset; x += pixelsPerModule)
            {
                for (var y = 0; y < size + offset; y += pixelsPerModule)
                {
                    var module = ModuleMatrix[(y + pixelsPerModule) / pixelsPerModule - 1][(x + pixelsPerModule) / pixelsPerModule - 1];

                    if (module)
                    {
                        gfx.FillRectangle(darkBrush, new Rectangle(x - offset, y - offset, pixelsPerModule, pixelsPerModule));
                    }
                    else
                    {
                        gfx.FillRectangle(lightBrush, new Rectangle(x - offset, y - offset, pixelsPerModule, pixelsPerModule));
                    }
                }
            }

            gfx.Save();
        }

        return bmp;
    }

    public QRCodeData CreateQrCode(string plainText, ECCLevel eccLevel, bool forceUtf8 = false, bool utf8BOM = false, EciMode eciMode = EciMode.Default, int requestedVersion = -1)
    {
        return GenerateQrCode(plainText, eccLevel, forceUtf8, utf8BOM, eciMode, requestedVersion);
    }

    private static QRCodeData GenerateQrCode(string plainText, ECCLevel eccLevel, bool forceUtf8 = false, bool utf8BOM = false, EciMode eciMode = EciMode.Default, int requestedVersion = -1)
    {
        if (version == -1)
        {
            // Logic for determining version
        }
        else
        {
            if (minVersion > version)
            {
                // Logic for handling version
            }
        }
        if (eciMode != EciMode.Default)
        {
            // Logic for handling ECI mode
        }
    }
}
