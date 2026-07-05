using System.Diagnostics.CodeAnalysis;

namespace Pt.Okx.Sdk.Enums
{
    /// <summary>
    /// Position side in long/short or net position mode.
    /// </summary>
    public enum PositionSide
    {
        /// <summary>Long position side.</summary>
        [SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Standard trading terminology")]
        Long,

        /// <summary>Short position side.</summary>
        [SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Standard trading terminology")]
        Short,

        /// <summary>Net position side.</summary>
        Net
    }
}
