using Pt.Okx.Sdk.Indicators.Base;
using Pt.Okx.Sdk.Indicators.Enums;
using Pt.Okx.Sdk.Indicators.Models;
using Pt.Okx.Sdk.Indicators.Services;

namespace Pt.Examples.Indicator
{
    public interface IIndicatorMaCrossover : IIndicator
    {
        /// <summary>Cross signal at bar index: 1 = bullish, -1 = bearish</summary>
        double GetCrossSignal(int barIndex);
        /// <summary>Fast MA > Slow MA at the current bar</summary>
        bool IsBullishCross();
        /// <summary>Fast MA < Slow MA at the current bar</summary>
        bool IsBearishCross();
        /// <summary>Current Fast MA value</summary>
        IndicatorValue FindFastMA(int index = 0);
        /// <summary>Current Slow MA value</summary>
        IndicatorValue FindSlowMA(int index = 0);
    }


    /// <summary>
    /// Double MA Crossover that displays two MA lines (Fast/Slow) on the main chart
    /// and colors them based on crossover direction (Fast > Slow = green, otherwise red).
    /// </summary>
    public class CalcIndMACrossover : CalcIndBase, IIndicatorMaCrossover
    {
        private IIndicatorBuffer? _fastBuffer;
        private IIndicatorBuffer? _slowBuffer;
        private IIndicatorBuffer? _crossBuffer;

        private int _fastPeriod;
        private int _slowPeriod;

        public CalcIndMACrossover(
            IIndicatorFactory factory,
            IIndicatorManager manager,
            IndicatorConfig config,
            Action<IndicatorProperty>? propertyOptions = null)
            : base(factory, manager, config, propertyOptions)
        {
        }

        protected override IndicatorProperty CreateDefaultProperty()
        {
            _fastPeriod = GetParameter<int>("FastPeriod");
            _slowPeriod = GetParameter<int>("SlowPeriod");

            return new IndicatorProperty(
                name: $"MACross({_fastPeriod},{_slowPeriod})",
                window: IndicatorWindow.Main,   // Overlay on the price chart
                buffers: 3,
                plots: 2                  // 2 MA lines
            )
            {
                Labels = new Dictionary<int, IndicatorLabel>
                {
                    {
                        0, new IndicatorLabel
                        {
                            Label = $"Fast({_fastPeriod})",
                            Type = IndicatorDrawType.Line,
                            Color = IndicatorColor.Cyan,
                            Style = IndicatorStyle.Solid,
                            Width = 1.5
                        }
                    },
                    {
                        1, new IndicatorLabel
                        {
                            Label = $"Slow({_slowPeriod})",
                            Type = IndicatorDrawType.Line,
                            Color = IndicatorColor.Orange,
                            Style = IndicatorStyle.Solid,
                            Width = 1.5
                        }
                    }
                }
            };
        }

        /// <inheritdoc/>
        public override bool OnInit()
        {
            base.OnInit();

            SetBuffer(0, IndicatorBufferType.Data);          // Fast MA
            SetBuffer(1, IndicatorBufferType.Data);          // Slow MA
            SetBuffer(2, IndicatorBufferType.Calculations); // Cross signal

            _fastBuffer = GetBuffer(0);
            _slowBuffer = GetBuffer(1);
            _crossBuffer = GetBuffer(2);

            return true;
        }

        private void EnsureBuffers()
        {
            _fastBuffer ??= GetBuffer(0);
            _slowBuffer ??= GetBuffer(1);
            _crossBuffer ??= GetBuffer(2);
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
            if (_fastPeriod <= 0 || _slowPeriod <= 0 || ratesTotal < _slowPeriod
                || closes == null || datetime == null)
                return 0;

            // Lazy init - base.OnInit() triggers OnCalculate before field assignment.
            _fastBuffer ??= GetBuffer(0);
            _slowBuffer ??= GetBuffer(1);
            _crossBuffer ??= GetBuffer(2);
            if (_fastBuffer == null || _slowBuffer == null || _crossBuffer == null)
                return 0;

            var start = Math.Max(prevCalculated - 1, _slowPeriod - 1);

            for (var i = start; i < ratesTotal; i++)
            {
                // SMA Fast
                double sumFast = 0;
                for (var j = i - _fastPeriod + 1; j <= i; j++)
                    sumFast += closes[j];
                var fast = sumFast / _fastPeriod;
                _fastBuffer.Add(i, datetime[i], fast);

                // SMA Slow
                double sumSlow = 0;
                for (var j = i - _slowPeriod + 1; j <= i; j++)
                    sumSlow += closes[j];
                var slow = sumSlow / _slowPeriod;
                _slowBuffer.Add(i, datetime[i], slow);

                // Cross signal: 1 = bullish, -1 = bearish
                _crossBuffer.Add(i, datetime[i], fast > slow ? 1 : -1);
            }

            return ratesTotal;
        }

        /// <inheritdoc/>
        public double GetCrossSignal(int barIndex)
        {
            EnsureBuffers();
            return _crossBuffer!.At(barIndex).Value;
        }

        /// <inheritdoc/>
        public bool IsBullishCross()
        {
            EnsureBuffers();
            var v = _crossBuffer!.FindCurrent();
            return !v.IsEmpty && v.Value > 0;
        }

        /// <inheritdoc/>
        public bool IsBearishCross()
        {
            EnsureBuffers();
            var v = _crossBuffer!.FindCurrent();
            return !v.IsEmpty && v.Value < 0;
        }

        /// <inheritdoc/>
        public IndicatorValue FindFastMA(int index = 0)
        {
            EnsureBuffers();
            return _fastBuffer!.FindAtOrBeforeCurrent(index);
        }

        /// <inheritdoc/>
        public IndicatorValue FindSlowMA(int index = 0)
        {
            EnsureBuffers();
            return _slowBuffer!.FindAtOrBeforeCurrent(index);
        }
    }
}
