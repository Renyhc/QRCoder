namespace QRCoder;

/// <summary>
/// Defines the error correction levels available for rMQR codes.
/// Each level specifies the proportion of data that can be recovered if the code is damaged.
/// </summary>
public enum RMQRErrorCorrectionLevel
{
    /// <summary>
    /// Level M: Medium error correction (approximately 15% of data can be recovered)
    /// </summary>
    M = 0,
    /// <summary>
    /// Level H: High error correction (approximately 30% of data can be recovered)
    /// </summary>
    H = 1
}