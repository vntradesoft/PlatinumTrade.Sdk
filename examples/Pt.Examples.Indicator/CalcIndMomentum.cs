using Pt.Okx.Sdk.Indicators.Base;
using Pt.Okx.Sdk.Indicators.Enums;
using Pt.Okx.Sdk.Indicators.Models;
using Pt.Okx.Sdk.Indicators.Services;

namespace Pt.Examples.Indicator
{
    /// <summary>
    /// Defines the contract for a Momentum (ROC) indicator.
    /// </summary>
    public interface IIndicatorMomentum : IIndicator
    {
        /// <summary>
        /// Gets the Rate of Change (ROC) value at the specified index.
        /// </summary>
        IndicatorValue FindROC(int index = 0);

        /// <summary>
        /// Determines whether momentum is bullish (ROC > 0).
        /// </summary>
        bool IsBullish();

        /// <summary>
        /// Determines whether momentum is bearish (ROC < 0).
        /// </summary>
        bool IsBearish();
    }

    /// <summary>
    /// Momentum oscillator that measures the rate of price change.
    /// Displays on a separate panel with a zero line.
    ///
    /// Formula: ROC = ((Close - Close[N]) / Close[N]) * 100
    /// </summary>
    public class CalcIndMomentum : CalcIndBase, IIndicatorMomentum
    {
        private IIndicatorBuffer? _rocBuffer;

        private int _period;

        public CalcIndMomentum(
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
                name: $"Momentum({_period})",
                window: IndicatorWindow.Separate,
                buffers: 1,
                plots: 1
            )
            {
                Labels = new Dictionary<int, IndicatorLabel>
                {
                    {
                        0, new IndicatorLabel
                        {
                            Label = "ROC",
                            Type = IndicatorDrawType.Histogram,   // Histogram style
                            Color = IndicatorColor.Green,
                            Style = IndicatorStyle.Solid,
                            Width = 2.0
                        }
                    }
                },
                SpecialFeatures = new IndicatorSpecialFeatures
                {
                    ShowZeroLine = true,
                    ZeroLineColor = IndicatorColor.Gray,
                    ZeroLineWidth = 1.0
                }
            };
        }

        /// <inheritdoc/>
        public override bool OnInit()
        {
            base.OnInit();

            SetBuffer(0, IndicatorBufferType.Data);
            _rocBuffer = GetBuffer(0);

            return true;
        }

        private void EnsureBuffers()
        {
            _rocBuffer ??= GetBuffer(0);
        }

        /// <inheritdoc/>
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
            if (_period <= 0 || ratesTotal <= _period || closes == null || datetime == null)
                return 0;

            // Lazy init - base.OnInit() triggers OnCalculate before field assignment.
            _rocBuffer ??= GetBuffer(0);
            if (_rocBuffer == null)
                return 0;

            var start = Math.Max(prevCalculated - 1, _period);

            for (var i = start; i < ratesTotal; i++)
            {
                var current = GetSourceValue(0, i, datetime[i], opens, highs, lows, closes);
                var previous = GetSourceValue(0, i - _period, datetime[i - _period], opens, highs, lows, closes);
                if (current.IsEmpty || previous.IsEmpty)
                {
                    _rocBuffer.MarkEmpty(i, datetime[i]);
                    continue;
                }

                var roc = previous.Value != 0
                    ? ((current.Value - previous.Value) / previous.Value) * 100
                    : 0;

                _rocBuffer.ForceAdd(i, datetime[i], roc);
            }

            return ratesTotal;
        }

        /// <inheritdoc/>
        public IndicatorValue FindROC(int index = 0)
        {
            EnsureBuffers();
            return _rocBuffer!.FindAtOrBeforeCurrent(index);
        }

        /// <inheritdoc/>
        public bool IsBullish()
        {
            var v = FindROC();
            return !v.IsEmpty && v.Value > 0;
        }

        /// <inheritdoc/>
        public bool IsBearish()
        {
            var v = FindROC();
            return !v.IsEmpty && v.Value < 0;
        }
    }
}
