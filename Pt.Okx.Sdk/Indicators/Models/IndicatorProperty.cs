using Pt.Okx.Sdk.Indicators.Enums;

namespace Pt.Okx.Sdk.Indicators.Models
{


    /// <summary>
    /// Defines a shaded region drawn between two indicator buffers (e.g. Ichimoku Kumo cloud).
    /// </summary>
    public class IndicatorFillRegion
    {
        /// <summary>Gets or sets a human-readable label for this region (e.g. "Kumo").</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Gets or sets whether the fill is currently active.</summary>
        public bool Enabled { get; set; } = true;

        /// <summary>Gets or sets whether the fill is shown in the indicator settings dialog.</summary>
        public bool Visible { get; set; } = true;

        /// <summary>Gets or sets the buffer index whose values define the top boundary of the fill.</summary>
        public int UpperBufferIndex { get; set; }

        /// <summary>Gets or sets the buffer index whose values define the bottom boundary of the fill.</summary>
        public int LowerBufferIndex { get; set; }

        /// <summary>
        /// Gets or sets how the fill color is selected.
        /// Use <see cref="IndicatorFillColorMode.Fixed"/> for a constant color, or
        /// <see cref="IndicatorFillColorMode.BullishBearish"/> to switch between
        /// <see cref="BullishColor"/> and <see cref="BearishColor"/> based on the relative
        /// positions of the upper and lower buffers.
        /// </summary>
        public IndicatorFillColorMode ColorMode { get; set; } = IndicatorFillColorMode.Fixed;

        /// <summary>Gets or sets the color used when <see cref="ColorMode"/> is <see cref="IndicatorFillColorMode.Fixed"/>.</summary>
        public IndicatorColor FillColor { get; set; } = IndicatorColor.LightGreen;

        /// <summary>Gets or sets the color used when upper ≥ lower and <see cref="ColorMode"/> is <see cref="IndicatorFillColorMode.BullishBearish"/>.</summary>
        public IndicatorColor BullishColor { get; set; } = IndicatorColor.LightGreen;

        /// <summary>Gets or sets the color used when upper &lt; lower and <see cref="ColorMode"/> is <see cref="IndicatorFillColorMode.BullishBearish"/>.</summary>
        public IndicatorColor BearishColor { get; set; } = IndicatorColor.Pink;

        /// <summary>Gets or sets the fill transparency (0 = fully transparent, 255 = fully opaque). Default is 72.</summary>
        public byte Opacity { get; set; } = 72;
    }

    /// <summary>
    /// Optional display features for specific indicator families.
    /// These settings add extra chart elements such as bound lines, a zero line,
    /// histogram coloring, and fill regions between buffers.
    /// </summary>
    public class IndicatorSpecialFeatures
    {
        /// <summary>
        /// Gets or sets whether to lock the Y-axis range to <see cref="MinValue"/>/<see cref="MaxValue"/>
        /// rather than auto-scaling to visible data (e.g. RSI 0–100).
        /// </summary>
        public bool UseRangeValue { get; set; }

        /// <summary>Gets or sets the fixed minimum Y value when <see cref="UseRangeValue"/> is <c>true</c>.</summary>
        public double MinValue { get; set; } = double.MinValue;

        /// <summary>Gets or sets the fixed maximum Y value when <see cref="UseRangeValue"/> is <c>true</c>.</summary>
        public double MaxValue { get; set; } = double.MaxValue;

        // ── Bound lines (RSI, Stochastic, …) ───────────────────────────────────

        /// <summary>Gets or sets whether to draw a horizontal upper-bound line (e.g. RSI overbought at 70).</summary>
        public bool ShowUpperBound { get; set; }

        /// <summary>Gets or sets whether to draw a horizontal lower-bound line (e.g. RSI oversold at 30).</summary>
        public bool ShowLowerBound { get; set; }

        /// <summary>Gets or sets whether to draw a horizontal center line (e.g. RSI midpoint at 50).</summary>
        public bool ShowCenterLine { get; set; }

        /// <summary>Gets or sets the Y value of the upper-bound line. Default is 70.</summary>
        public double UpperBoundValue { get; set; } = 70;

        /// <summary>Gets or sets the stroke width of the upper-bound line. Default is 1.0.</summary>
        public double UpperBoundWidth { get; set; } = 1.0;

        /// <summary>Gets or sets the Y value of the lower-bound line. Default is 30.</summary>
        public double LowerBoundValue { get; set; } = 30;

        /// <summary>Gets or sets the stroke width of the lower-bound line. Default is 1.0.</summary>
        public double LowerBoundWidth { get; set; } = 1.0;

        /// <summary>Gets or sets the Y value of the center line. Default is 50.</summary>
        public double CenterLineValue { get; set; } = 50;

        /// <summary>Gets or sets the stroke width of the center line. Default is 1.0.</summary>
        public double CenterLineWidth { get; set; } = 1.0;

        /// <summary>Gets or sets the color of the upper-bound line. Default is <see cref="IndicatorColor.Red"/>.</summary>
        public IndicatorColor UpperBoundColor { get; set; } = IndicatorColor.Gray;

        /// <summary>Gets or sets the color of the lower-bound line. Default is <see cref="IndicatorColor.Green"/>.</summary>
        public IndicatorColor LowerBoundColor { get; set; } = IndicatorColor.Gray;

        /// <summary>Gets or sets the color of the center line. Default is <see cref="IndicatorColor.Gray"/>.</summary>
        public IndicatorColor CenterLineColor { get; set; } = IndicatorColor.Gray;

        /// <summary>Gets or sets the dash style shared by all bound lines. Default is <see cref="IndicatorStyle.Dashed"/>.</summary>
        public IndicatorStyle BoundLineStyle { get; set; } = IndicatorStyle.Dashed;

        // ── Bound fill (optional shade between upper and lower bound lines) ────

        /// <summary>
        /// Gets or sets whether to fill the area between the upper-bound line and the
        /// lower-bound line. Only rendered when both <see cref="ShowUpperBound"/> and
        /// <see cref="ShowLowerBound"/> are <c>true</c>.
        /// </summary>
        public bool ShowBoundFill { get; set; }

        /// <summary>
        /// Gets or sets the fill color used between the upper and lower bound lines.
        /// Default is <see cref="IndicatorColor.Gray"/>.
        /// </summary>
        public IndicatorColor BoundFillColor { get; set; } = IndicatorColor.Gray;

        /// <summary>
        /// Gets or sets the fill transparency for the bound area (0 = fully transparent, 255 = fully opaque).
        /// Default is 30.
        /// </summary>
        public byte BoundFillOpacity { get; set; } = 30;

        // ── Histogram (MACD, …) ─────────────────────────────────────────────────

        /// <summary>Gets or sets whether to render the primary buffer as a histogram.</summary>
        public bool ShowHistogram { get; set; }

        /// <summary>Gets or sets the color for histogram bars with positive values. Default is <see cref="IndicatorColor.Green"/>.</summary>
        public IndicatorColor HistogramPositiveColor { get; set; } = IndicatorColor.Green;

        /// <summary>Gets or sets the color for histogram bars with negative values. Default is <see cref="IndicatorColor.Red"/>.</summary>
        public IndicatorColor HistogramNegativeColor { get; set; } = IndicatorColor.Red;

        /// <summary>Gets or sets whether to draw a horizontal zero line across the panel.</summary>
        public bool ShowZeroLine { get; set; }

        /// <summary>Gets or sets the stroke width of the zero line. Default is 1.0.</summary>
        public double ZeroLineWidth { get; set; } = 1.0;

        /// <summary>Gets or sets the color of the zero line. Default is <see cref="IndicatorColor.Gray"/>.</summary>
        public IndicatorColor ZeroLineColor { get; set; } = IndicatorColor.Gray;

        // ── Fill regions ────────────────────────────────────────────────────────

        private readonly List<IndicatorFillRegion> _fillRegions = new();

        /// <summary>
        /// Gets the list of shaded regions drawn between pairs of buffers.
        /// Each entry is rendered independently and can use a different
        /// color mode and opacity.
        /// </summary>
        public IEnumerable<IndicatorFillRegion> FillRegions
        {

            get => _fillRegions;
            init
            {
                if (value != null)
                {
                    _fillRegions.Clear();
                    _fillRegions.AddRange(value);
                }
            }
        }

        /// <summary>
        /// Sets the list of shaded regions drawn between pairs of buffers.
        /// </summary>
        /// <param name="regions">The list of shaded regions.</param>
        public void SetFillRegions(IEnumerable<IndicatorFillRegion> regions)
        {
            ArgumentNullException.ThrowIfNull(regions);

            _fillRegions.Clear();
            _fillRegions.AddRange(regions);
        }

    }

    /// <summary>
    /// Rendering metadata for a single indicator plot (line, histogram, dots, …).
    /// Each visible buffer should have a corresponding <see cref="IndicatorLabel"/> entry
    /// in <see cref="IndicatorProperty.Labels"/>.
    /// </summary>
    public class IndicatorLabel
    {
        /// <summary>Gets or sets the human-readable name shown in the crosshair tooltip and legend.</summary>
        public string Label { get; set; } = string.Empty;

        /// <summary>Gets or sets the draw primitive used to render this plot. Default is <see cref="IndicatorDrawType.Line"/>.</summary>
        public IndicatorDrawType Type { get; set; } = IndicatorDrawType.Line;

        /// <summary>
        /// Gets or sets the fallback color for this plot.
        /// Used when <see cref="ColorIndexBuffer"/> is not set or yields no valid palette entry.
        /// Default is <see cref="IndicatorColor.Blue"/>.
        /// </summary>
        public IndicatorColor Color { get; set; } = IndicatorColor.Blue;

        /// <summary>Gets or sets the dash style for line-type plots. Default is <see cref="IndicatorStyle.Solid"/>.</summary>
        public IndicatorStyle Style { get; set; } = IndicatorStyle.Solid;

        /// <summary>Gets or sets the stroke width. Default is 1.0.</summary>
        public double Width { get; set; } = 1.0;

        /// <summary>Gets or sets whether this plot is currently visible on the chart.</summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Gets or sets the optional number of decimal places shown for this plot in crosshair and
        /// tooltip overlays. When <c>null</c>, the indicator-level default precision is used.
        /// </summary>
        public int? Digit { get; set; }

        /// <summary>
        /// Gets or sets the optional buffer index that contains per-point color indexes for this plot.
        /// The referenced buffer must be registered as <see cref="IndicatorBufferType.ColorIndex"/>.
        /// When <c>null</c>, <see cref="Color"/> is used uniformly for every point.
        /// Each indicator label may reference a different color-index buffer, so a multi-line
        /// indicator can have each line change color independently.
        /// </summary>
        public int? ColorIndexBuffer { get; set; }

        /// <summary>
        /// Gets the color palette used when <see cref="ColorIndexBuffer"/> is set.
        /// The key is an integer color index written into the color buffer by <c>OnCalculate</c>;
        /// the value is the <see cref="IndicatorColor"/> to render for that index.
        /// Points whose index is not present in the palette fall back to <see cref="Color"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// ColorPalette = new Dictionary&lt;int, IndicatorColor&gt;
        /// {
        ///     { 0, IndicatorColor.Red },   // bearish
        ///     { 1, IndicatorColor.Lime }   // bullish
        /// }
        /// </code>
        /// </example>
#pragma warning disable CA2227
        public IDictionary<int, IndicatorColor> ColorPalette { get; set; }
            = new Dictionary<int, IndicatorColor>();
#pragma warning restore CA2227
    }

    /// <summary>
    /// Display and buffer metadata for an indicator.
    /// Passed to the chart engine to control how the indicator is rendered, which
    /// timeframes it appears on, and which additional UI elements (bounds, fills, …) are shown.
    /// </summary>
    public class IndicatorProperty
    {
        /// <summary>
        /// Gets or sets the display name shown on the chart legend and in dialogs
        /// (e.g. <c>"RSI(14)"</c>, <c>"SuperTrend(10,3)"</c>).
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Gets or sets whether the indicator is rendered on the chart. Default is <c>true</c>.</summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// Gets or sets whether this indicator is hidden from the indicator selection menus.
        /// Set to <c>true</c> for internally chained indicators that should not be added manually.
        /// </summary>
        public bool IsInternal { get; set; }

        /// <summary>
        /// Gets the chart panel where the indicator is drawn.
        /// <see cref="IndicatorWindow.Main"/> renders over the price bars;
        /// <see cref="IndicatorWindow.Separate"/> renders in its own panel below the chart.
        /// </summary>
        public IndicatorWindow Window { get; private set; } = IndicatorWindow.Main;

        /// <summary>
        /// Gets the total number of buffers allocated for this indicator, including
        /// <see cref="IndicatorBufferType.Data"/>, <see cref="IndicatorBufferType.ColorIndex"/>,
        /// and <see cref="IndicatorBufferType.Calculations"/> buffers.
        /// Must match the number of <c>SetBuffer</c> calls in <c>OnInit</c>.
        /// </summary>
        public int Buffers { get; private set; }

        /// <summary>
        /// Gets the number of visible plots (i.e. entries in <see cref="Labels"/>).
        /// Used for informational purposes; the renderer drives visibility from <see cref="Labels"/> directly.
        /// </summary>
        public int Plots { get; private set; }

        /// <summary>
        /// Gets or sets the per-buffer rendering metadata.
        /// Key: zero-based buffer index (must correspond to a <see cref="IndicatorBufferType.Data"/> buffer).
        /// Buffers without an entry are not rendered.
        /// </summary>
        public IReadOnlyDictionary<int, IndicatorLabel> Labels { get; set; } = new Dictionary<int, IndicatorLabel>();

        /// <summary>
        /// Gets or sets the set of timeframes on which this indicator is displayed.
        /// Default is <see cref="TimeFrameOptions.AllPeriods"/>.
        /// </summary>
        public TimeFrameOptions VisibleTimeframes { get; set; } = TimeFrameOptions.AllPeriods;

        /// <summary>
        /// Gets or sets additional rendering features such as bound lines, zero line,
        /// histogram colors, and fill regions between buffers.
        /// </summary>
        public IndicatorSpecialFeatures SpecialFeatures { get; set; } = new IndicatorSpecialFeatures();

        /// <summary>
        /// Initializes a new <see cref="IndicatorProperty"/> instance.
        /// </summary>
        /// <param name="name">Display name shown on the chart (e.g. <c>"RSI(14)"</c>).</param>
        /// <param name="window">Panel where the indicator is rendered.</param>
        /// <param name="buffers">
        /// Total buffer count, including data, color-index, and calculation buffers.
        /// Must match the <c>SetBuffer</c> calls made in <c>OnInit</c>.
        /// </param>
        /// <param name="plots">Number of visible plots (informational).</param>
        public IndicatorProperty(string name, IndicatorWindow window, int buffers, int plots)
        {
            Name = name;
            Window = window;
            Buffers = buffers;
            Plots = plots;
        }
    }
}
