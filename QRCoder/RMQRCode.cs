using System;
using System.Collections;
using System.Collections.Generic;

namespace QRCoder;

/// <summary>
/// Represents a Rectangular Micro QR (rMQR) code generator.
/// </summary>
public class RMQRCode : AbstractQRCode, IDisposable
{
    /// <summary>
    /// The version of the rMQR code, which determines its size and data capacity.
    /// </summary>
    public RMQRVersion Version { get; private set; }

    /// <summary>
    /// Initializes a new instance of the RMQRCode class.
    /// </summary>
    public RMQRCode() { }

    /// <summary>
    /// Initializes a new instance of the RMQRCode class with the specified QRCodeData.
    /// </summary>
    /// <param name="data">QRCodeData containing the data to encode.</param>
    public RMQRCode(QRCodeData data) : base(data)
    {
        Version = RMQRVersion.R7x43;  // Default version
    }

    /// <summary>
    /// Sets the version for the rMQR code.
    /// </summary>
    /// <param name="version">The desired version.</param>
    public void SetVersion(RMQRVersion version) => Version = version;

    /// <summary>
    /// Gets the dimensions for a specific rMQR version.
    /// </summary>
    /// <param name="version">The rMQR version.</param>
    /// <param name="width">Output parameter that receives the width in modules.</param>
    /// <param name="height">Output parameter that receives the height in modules.</param>
    public static void GetDimensions(RMQRVersion version, out int width, out int height)
    {
        switch (version)
        {
            case RMQRVersion.R7x43:
                width = 43; height = 7;
                break;
            case RMQRVersion.R7x59:
                width = 59; height = 7;
                break;
            case RMQRVersion.R7x77:
                width = 77; height = 7;
                break;
            case RMQRVersion.R7x99:
                width = 99; height = 7;
                break;
            case RMQRVersion.R7x139:
                width = 139; height = 7;
                break;
            case RMQRVersion.R9x43:
                width = 43; height = 9;
                break;
            case RMQRVersion.R9x59:
                width = 59; height = 9;
                break;
            case RMQRVersion.R9x77:
                width = 77; height = 9;
                break;
            case RMQRVersion.R9x99:
                width = 99; height = 9;
                break;
            case RMQRVersion.R9x139:
                width = 139; height = 9;
                break;
            case RMQRVersion.R11x27:
                width = 27; height = 11;
                break;
            case RMQRVersion.R11x43:
                width = 43; height = 11;
                break;
            case RMQRVersion.R11x59:
                width = 59; height = 11;
                break;
            case RMQRVersion.R11x77:
                width = 77; height = 11;
                break;
            case RMQRVersion.R11x99:
                width = 99; height = 11;
                break;
            case RMQRVersion.R11x139:
                width = 139; height = 11;
                break;
            case RMQRVersion.R13x27:
                width = 27; height = 13;
                break;
            case RMQRVersion.R13x43:
                width = 43; height = 13;
                break;
            case RMQRVersion.R13x59:
                width = 59; height = 13;
                break;
            case RMQRVersion.R13x77:
                width = 77; height = 13;
                break;
            case RMQRVersion.R13x99:
                width = 99; height = 13;
                break;
            case RMQRVersion.R13x139:
                width = 139; height = 13;
                break;
            case RMQRVersion.R15x43:
                width = 43; height = 15;
                break;
            case RMQRVersion.R15x59:
                width = 59; height = 15;
                break;
            case RMQRVersion.R15x77:
                width = 77; height = 15;
                break;
            case RMQRVersion.R15x99:
                width = 99; height = 15;
                break;
            case RMQRVersion.R15x139:
                width = 139; height = 15;
                break;
            case RMQRVersion.R17x43:
                width = 43; height = 17;
                break;
            case RMQRVersion.R17x59:
                width = 59; height = 17;
                break;
            case RMQRVersion.R17x77:
                width = 77; height = 17;
                break;
            case RMQRVersion.R17x99:
                width = 99; height = 17;
                break;
            case RMQRVersion.R17x139:
                width = 139; height = 17;
                break;
            default:
                throw new ArgumentException("Invalid rMQR version", nameof(version));
        }
    }

    /// <summary>
    /// Gets the dimensions for a specific rMQR version.
    /// </summary>
    /// <param name="version">The rMQR version.</param>
    /// <returns>A DimensionTuple containing the width and height in modules.</returns>
    public static DimensionTuple GetDimensions(RMQRVersion version)
    {
        GetDimensions(version, out int width, out int height);
        return new DimensionTuple(width, height);
    }

    /// <summary>
    /// Represents the dimensions of an rMQR code.
    /// </summary>
    public struct DimensionTuple
    {
        /// <summary>
        /// Gets the width in modules.
        /// </summary>
        public readonly int Width;

        /// <summary>
        /// Gets the height in modules.
        /// </summary>
        public readonly int Height;

        /// <summary>
        /// Initializes a new instance of the DimensionTuple struct.
        /// </summary>
        /// <param name="width">The width in modules.</param>
        /// <param name="height">The height in modules.</param>
        public DimensionTuple(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Deconstructs the tuple into its components.
        /// </summary>
        /// <param name="width">Output parameter that receives the width.</param>
        /// <param name="height">Output parameter that receives the height.</param>
        public void Deconstruct(out int width, out int height)
        {
            width = Width;
            height = Height;
        }
    }
}

/// <summary>
/// Defines the available versions for rMQR codes.
/// </summary>
public enum RMQRVersion
{
    // 7-module height versions
    R7x43,
    R7x59,
    R7x77,
    R7x99,
    R7x139,
    // 9-module height versions
    R9x43,
    R9x59,
    R9x77,
    R9x99,
    R9x139,
    // 11-module height versions
    R11x27,
    R11x43,
    R11x59,
    R11x77,
    R11x99,
    R11x139,
    // 13-module height versions
    R13x27,
    R13x43,
    R13x59,
    R13x77,
    R13x99,
    R13x139,
    // 15-module height versions
    R15x43,
    R15x59,
    R15x77,
    R15x99,
    R15x139,
    // 17-module height versions
    R17x43,
    R17x59,
    R17x77,
    R17x99,
    R17x139
}
