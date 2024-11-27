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
    public void SetVersion(RMQRVersion version)
    {
        Version = version;
    }

    /// <summary>
    /// Gets the dimensions for a specific rMQR version.
    /// </summary>
    /// <param name="version">The rMQR version.</param>
    /// <returns>A tuple containing the width and height in modules.</returns>
    public static (int Width, int Height) GetDimensions(RMQRVersion version)
    {
        return version switch
        {
            RMQRVersion.R7x43 => (43, 7),
            RMQRVersion.R7x59 => (59, 7),
            RMQRVersion.R7x77 => (77, 7),
            RMQRVersion.R7x99 => (99, 7),
            RMQRVersion.R7x139 => (139, 7),
            RMQRVersion.R9x43 => (43, 9),
            RMQRVersion.R9x59 => (59, 9),
            RMQRVersion.R9x77 => (77, 9),
            RMQRVersion.R9x99 => (99, 9),
            RMQRVersion.R9x139 => (139, 9),
            RMQRVersion.R11x27 => (27, 11),
            RMQRVersion.R11x43 => (43, 11),
            RMQRVersion.R11x59 => (59, 11),
            RMQRVersion.R11x77 => (77, 11),
            RMQRVersion.R11x99 => (99, 11),
            RMQRVersion.R11x139 => (139, 11),
            RMQRVersion.R13x27 => (27, 13),
            RMQRVersion.R13x43 => (43, 13),
            RMQRVersion.R13x59 => (59, 13),
            RMQRVersion.R13x77 => (77, 13),
            RMQRVersion.R13x99 => (99, 13),
            RMQRVersion.R13x139 => (139, 13),
            RMQRVersion.R15x43 => (43, 15),
            RMQRVersion.R15x59 => (59, 15),
            RMQRVersion.R15x77 => (77, 15),
            RMQRVersion.R15x99 => (99, 15),
            RMQRVersion.R15x139 => (139, 15),
            RMQRVersion.R17x43 => (43, 17),
            RMQRVersion.R17x59 => (59, 17),
            RMQRVersion.R17x77 => (77, 17),
            RMQRVersion.R17x99 => (99, 17),
            RMQRVersion.R17x139 => (139, 17),
            _ => throw new ArgumentException("Invalid rMQR version", nameof(version))
        };
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
