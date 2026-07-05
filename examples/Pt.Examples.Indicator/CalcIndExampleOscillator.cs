using Pt.Okx.Sdk.Indicators.Base;
using Pt.Okx.Sdk.Indicators.Enums;
using Pt.Okx.Sdk.Indicators.Models;
using Pt.Okx.Sdk.Indicators.Services;

namespace Pt.Examples.Indicator
{
    /// <summary>
    /// Example indicator that uses SpecialFeatures to verify the Advanced tab
    /// appears correctly in CustomIndicatorDialog when editing.
    ///
    /// This indicator computes a simple RSI-style value (0-100) with:
    ///   - Bound lines at 70 (overbought) / 30 (oversold) / 50 (center)
    ///   - BoundFill (gray tint between the two bound lines)
    ///   - Zero line disabled (range is 0-100, not oscillating around 0)
    ///
    /// When users open the Edit dialog for this indicator, the Advanced tab should
    /// automatically display "Bound Lines" and "Bound Fill" sections.
    /// </summary>
    public class CalcIndExampleOscillator : CalcIndBase
    {
        private IIndicatorBuffer _rsiBuffer = null!;
        private int _period;

        public CalcIndExampleOscillator(
            IIndicatorFactory factory,
            IIndicatorManager manager,
            IndicatorConfig config,
            Action<IndicatorProperty>? propertyOptions = null)
            : base(factory, manager, config, propertyOptions)
        {
        }

        protected override IndicatorProperty CreateDefaultProperty()
        {
            _period = GetParameter<int>("Period");

            return new IndicatorProperty(
                name: $"ExRSI({_period})",
                window: IndicatorWindow.Separate,
                buffers: 1,
                plots: 1)
            {
                Labels = new Dictionary<int, IndicatorLabel>
                {
                    {
                        0, new IndicatorLabel
                        {
                            Label = "RSI",
                            Type = IndicatorDrawType.Line,
                            Color = IndicatorColor.Purple,
                            Style = IndicatorStyle.Solid,
                            Width = 1.5
                        }
                    }
                },
                SpecialFeatures = new IndicatorSpecialFeatures
                {
                    // Lock the Y-axis to 0–100
                    UseRangeValue = true,
                    MinValue = 0,
                    MaxValue = 100,

                    // Overbought line at 70 (red, dashed)
                    ShowUpperBound = true,
                    UpperBoundValue = 70,
                    UpperBoundColor = IndicatorColor.Red,
                    UpperBoundWidth = 1.0,

                    // Oversold line at 30 (green, dashed)
                    ShowLowerBound = true,
                    LowerBoundValue = 30,
                    LowerBoundColor = IndicatorColor.Green,
                    LowerBoundWidth = 1.0,

                    // Center line at 50 (gray)
                    ShowCenterLine = true,
                    CenterLineValue = 50,
                    CenterLineColor = IndicatorColor.Gray,
                    CenterLineWidth = 0.5,

                    // Shared dash style for all bound lines
                    BoundLineStyle = IndicatorStyle.Dashed,

                    // BoundFill: tô nền xám nhạt giữa đường 30 và 70
                    ShowBoundFill = true,
                    BoundFillColor = IndicatorColor.Gray,
                    BoundFillOpacity = 25
                }
            };
        }

        public override bool OnInit()
        {
            base.OnInit();
            SetBuffer(0, IndicatorBufferType.Data);
            _rsiBuffer = GetBuffer(0);
            return true;
        }

        public override int OnCalculate(
            in int ratesTotal,
            in int prevCalculated,
            in DateTime[] datetime,
            in double[] opens,
            in double[] highs,
            in double[] lows,
            in double[] closes,
            in double[] volumes,
            in double spreads)
        {
            if (ratesTotal < _period + 1)
                return 0;

            var start = Math.Max(prevCalculated - 1, _period);

            for (var i = start; i < ratesTotal; i++)
            {
                double gain = 0, loss = 0;
                for (var j = i - _period + 1; j <= i; j++)
                {
                    var delta = closes[j] - closes[j - 1];
                    if (delta > 0) gain += delta;
                    else loss -= delta;
                }

                var avgGain = gain / _period;
                var avgLoss = loss / _period;

                double rsi;
                if (avgLoss == 0)
                    rsi = 100;
                else
                {
                    var rs = avgGain / avgLoss;
                    rsi = 100 - 100 / (1 + rs);
                }

                _rsiBuffer.Add(i, datetime[i], rsi);
            }

            return ratesTotal;
        }
    }
}
