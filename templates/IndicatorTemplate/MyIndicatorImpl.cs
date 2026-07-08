using System;
using System.Collections.Generic;
using Pt.Okx.Sdk.Indicators.Base;
using Pt.Okx.Sdk.Indicators.Models;
using Pt.Okx.Sdk.Indicators.Services;

namespace MyCompany.MyIndicator
{
    public class MyIndicatorImpl : CalcIndBase
    {
        private IIndicatorBuffer _mainBuffer;

        public MyIndicatorImpl(IIndicatorFactory factory, IIndicatorManager manager, IndicatorConfig config, Action<IndicatorProperty>? propertyOptions = null)
            : base(factory, manager, config, propertyOptions)
        {
            // Set any parameters
            // int period = GetParameter<int>("Period");
        }

        protected override IndicatorProperty CreateDefaultProperty()
        {
            return new IndicatorProperty(Identity.IndicatorType, 1)
            {
                ShortName = "MyIndicator",
                Precision = 2,
                Levels = new List<double>(),
                Colors = new List<System.Drawing.Color> { System.Drawing.Color.Blue }
            };
        }

        public override bool OnInit()
        {
            if (!base.OnInit())
                return false;

            _mainBuffer = _manager.CreateBuffer(this, 0);
            return true;
        }

        public override int OnCalculate(in int ratesTotal, in int prevCalculated, in int begin, in double[] prices)
        {
            int start = prevCalculated == 0 ? begin : prevCalculated - 1;

            for (int i = start; i < ratesTotal; i++)
            {
                // Simple pass-through example
                _mainBuffer.Set(i, prices[i]);
            }

            return ratesTotal;
        }
    }
}
