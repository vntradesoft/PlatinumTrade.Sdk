# ![Pt.Okx.Sdk](https://raw.githubusercontent.com/vntradesoft/PlatinumTrade.Sdk/main/icon.png) Pt.Okx.Sdk

[English](README.md) | **Tiếng Việt**

Một bộ SDK .NET cung cấp các hợp đồng, lớp trừu tượng và các thành phần dùng chung để xây dựng các ứng dụng giao dịch thuật toán trên sàn giao dịch OKX.

SDK này được thiết kế dành riêng cho ứng dụng máy tính **Platinum Trade**. Tích hợp CLI hiện đang được phát triển để mở rộng khả năng tương thích ngoài môi trường ứng dụng máy tính.

> **Lưu ý:** Hiện tại chỉ hỗ trợ hợp đồng **OKX Perpetual Swap** trong chế độ ký quỹ cô lập (**Isolated Margin**).

> **Bạn đang tìm kiếm một nền tảng giao dịch đầy đủ?** Pt.Okx.Sdk là nhân của [Platinum Trade](https://github.com/vntradesoft/PlatinumTrade.App) — một ứng dụng máy tính giao dịch hoàn chỉnh được xây dựng trên SDK này. Gói thư viện này là miễn phí và được cấp phép MIT cho bất kỳ ai muốn tự xây dựng công cụ của riêng họ.

---

## Tại sao chọn Pt.Okx.Sdk?

Hãy thôi lo lắng về các kết nối API thô của sàn giao dịch.

Pt.Okx.Sdk cung cấp trải nghiệm phát triển tập trung vào giao dịch, nơi các nhà phát triển có thể tập trung vào những điều quan trọng nhất: chiến thuật, chỉ báo và logic thực thi.

Lấy cảm hứng từ sự đơn giản của MQL5 và các nền tảng giao dịch hiện đại, SDK ẩn đi các phức tạp đặc thù của sàn giao dịch đằng sau một mô hình lập trình nhất quán và sạch sẽ.

Cùng với PlatinumApp, người dùng có thể chuyển đổi liền mạch từ ý tưởng sang thực thi:

Chiến thuật (Strategy) → Kiểm thử lịch sử (Backtest) → Tối ưu hóa (Optimize) → Giao dịch thực tế (Live Trade)

Tất cả nằm trong một hệ sinh thái thống nhất được thiết kế cho các nhà giao dịch chuyên nghiệp và các nhà phát triển.

---

## Ảnh chụp màn hình

**Bảng điều khiển — Danh sách theo dõi & Biểu đồ**

![Dashboard](https://raw.githubusercontent.com/vntradesoft/PlatinumTrade.Sdk/main/docs/images/dashboard.png)

**Giao dịch thực tế (Live Trading)**

![Live Trading](https://raw.githubusercontent.com/vntradesoft/PlatinumTrade.Sdk/main/docs/images/livetrade.png)

**Backtest — Biểu đồ & Số dư**

![Backtest Chart](https://raw.githubusercontent.com/vntradesoft/PlatinumTrade.Sdk/main/docs/images/backtest1.png)

**Backtest — Các chỉ số & Báo cáo**

![Backtest Metrics](https://raw.githubusercontent.com/vntradesoft/PlatinumTrade.Sdk/main/docs/images/backtest.png)

---

## Các tính năng chính

- **Plugin Chỉ báo (Indicator)**: Cung cấp các lớp cơ sở có thể mở rộng (`CalcIndBase`) và dịch vụ để thiết kế các plugin chỉ báo tùy chỉnh.
- **Plugin Chiến thuật (Strategy)**: Giao diện tiêu chuẩn cho các plugin chiến thuật, cài đặt, tham số, dữ liệu thời gian thực và tương tác với sàn giao dịch.
- **Hỗ trợ Backtest và Realtime**: Các plugin được tạo ra có thể sử dụng liền mạch trong cả kiểm thử lịch sử và luồng giao dịch thực tế.
- **Hỗ trợ Thông báo**: Tích hợp sẵn các cổng để gửi thông báo thời gian chạy và tin nhắn chiến thuật.
- **Đối tượng vẽ (Drawing Objects)**: Hỗ trợ vẽ các đối tượng đồ họa trực tiếp trên biểu đồ.
- **Tích hợp giao diện UI**: Đầu ra của chỉ báo, chiến thuật và bản vẽ có thể được hiển thị trực quan trực tiếp trên giao diện của ứng dụng.

---

## Các Framework được hỗ trợ

| Phiên bản .NET | Phiên bản cụ thể | Trạng thái hỗ trợ |
| --- | --- | --- |
| .NET | 8.0 | Được hỗ trợ |
| .NET | 9.0 | Được hỗ trợ |
| .NET | 10.0 | Được hỗ trợ |

> Phiên bản package đích hiện tại là `net8.0;net9.0;net10.0`.

---

## Cài đặt

Thêm thư viện thông qua dotnet CLI:

```bash
dotnet add package Pt.Okx.Sdk
```

### Dự án mẫu (Templates)

Bạn có thể nhanh chóng tạo một cấu trúc dự án chiến thuật hoặc chỉ báo mới bằng cách sử dụng các `.NET Templates` chính thức của chúng tôi.

Đầu tiên, hãy cài đặt các templates trực tiếp từ gói NuGet (hoặc từ thư mục `templates` nếu bạn đã clone repo):

```bash
# Nếu cài đặt từ nguồn đã clone:
dotnet new install ./templates/StrategyTemplate
dotnet new install ./templates/IndicatorTemplate
```

Sau khi cài đặt, bạn có thể tạo dự án khung mẫu sẵn sàng chạy:

```bash
# Tạo một dự án Plugin Chiến thuật mới
dotnet new pt-strategy -n MyTradingBot

# Tạo một dự án Plugin Chỉ báo mới
dotnet new pt-indicator -n MyCustomIndicators
```

---

## Hướng dẫn nhanh (Getting Started)

### 1. Tạo một Plugin Chiến thuật tùy chỉnh (Strategy Plugin)

Để tạo một chiến thuật tùy chỉnh, hãy kế thừa lớp `StrategyBase` (hoặc triển khai giao diện `IStrategy`) và đăng ký dịch vụ của bạn thông qua `IStrategyPlugin`:

#### Triển khai Chiến thuật

```csharp
using Pt.Okx.Sdk.Strategy;
using Pt.Okx.Sdk.Strategy.Events;
using Pt.Okx.Sdk.Strategy.Settings;
using Microsoft.Extensions.Options;

public class SimpleMomentumStrategy : StrategyBase
{
    private readonly IStrategyLogger _logger;
    private readonly IOkxClient _client;
    private readonly StrategySettings _settings;

    public SimpleMomentumStrategy(
        IStrategyLogger logger,
        IOkxClient client,
        IOptions<StrategySettings> settings)
    {
        _logger = logger;
        _client = client;
        _settings = settings.Value;
    }

    public override async Task<bool> OnInitAsync(IStrategyStateStore state, CancellationToken ct)
    {
        _logger.LogInformation("Init", "Initializing strategy for {Symbol}", _settings.Symbol);
        return true;
    }

    public override async Task OnTickAsync(TickPhase tickPhase, CancellationToken ct)
    {
        // tickPhase cho biết đây là tick bên trong nến hay tick khi nến vừa đóng
        _logger.LogInformation("Tick", "Processing tick update for {Symbol}", _settings.Symbol);
        // Triển khai logic giao dịch của bạn tại đây
    }

    public override Task<bool> OnStopAsync(CancellationToken ct)
    {
        _logger.LogWarning("Stop", "Strategy is stopping");
        return Task.FromResult(true);
    }
    
    // Tùy chọn: ghi đè OnOrderAsync, OnPositionAsync, v.v. để phản hồi các sự kiện
    public override Task OnPositionAsync(IReadOnlyList<Pt.Okx.Sdk.Clients.Trading.Models.Position> positions, CancellationToken ct)
    {
        // Xử lý khi có cập nhật vị thế (position)
        return Task.CompletedTask;
    }
}
```

#### Đăng ký Plugin

```csharp
using Pt.Okx.Sdk.Strategy;
using Microsoft.Extensions.DependencyInjection;

public class MyStrategyPlugin : IStrategyPlugin, IStrategyPluginMetadata
{
    public string Name => "My Momentum Strategy";
    public string PluginVersion => "1.0.0";
    public string RequiredSdkVersion => "1.0";

    public void Register(IServiceCollection services)
    {
        services.AddSingleton<IStrategy, SimpleMomentumStrategy>();
    }

    public void RegisterForBacktest(IServiceCollection services)
    {
        services.AddTransient<IStrategy, SimpleMomentumStrategy>();
    }
}
```

---

### 2. Tạo một Chỉ báo tùy chỉnh (Custom Indicator)

Để xây dựng một chỉ báo tùy chỉnh, kế thừa từ lớp `CalcIndBase` và triển khai phương thức `OnCalculate`:

```csharp
using Pt.Okx.Sdk.Indicators.Base;
using Pt.Okx.Sdk.Indicators.Enums;
using Pt.Okx.Sdk.Indicators.Models;
using Pt.Okx.Sdk.Indicators.Services;

public class SimpleMomentumIndicator : CalcIndBase
{
    private IIndicatorBuffer? _rocBuffer;
    private int _period;

    public SimpleMomentumIndicator(
        IIndicatorFactory factory,
        IIndicatorManager manager,
        IndicatorConfig config)
        : base(factory, manager, config)
    {
    }

    protected override IndicatorProperty CreateDefaultProperty()
    {
        _period = GetParameter<int>("Period");

        return new IndicatorProperty(
            name: $"Momentum({_period})",
            window: IndicatorWindow.Separate, // Mở trong một bảng biểu đồ riêng biệt
            buffers: 1,
            plots: 1
        );
    }

    public override bool OnInit()
    {
        base.OnInit();
        SetBuffer(0, IndicatorBufferType.Data);
        _rocBuffer = GetBuffer(0);
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
        if (closes == null || ratesTotal <= _period) return 0;
        _rocBuffer ??= GetBuffer(0);

        int start = Math.Max(prevCalculated - 1, _period);
        for (int i = start; i < ratesTotal; i++)
        {
            double closeNow = closes[i];
            double closeOld = closes[i - _period];

            // Công thức ROC: ((Close - Close[N]) / Close[N]) * 100
            double value = closeOld != 0 ? ((closeNow - closeOld) / closeOld) * 100 : 0;
            _rocBuffer?.SetValue(i, value);
        }

        return ratesTotal;
    }
}
```

### 3. Đăng ký Chỉ báo trong Plugin

Đăng ký các chỉ báo tùy chỉnh của bạn qua lớp triển khai `IIndicatorPlugin`:

```csharp
using Pt.Okx.Sdk.Indicators.Plugin;

public class MyIndicatorPlugin : IIndicatorPlugin
{
    public string Name => "My Trading Indicators";
    public string PluginVersion => "1.0.0";
    public string RequiredSdkVersion => "1.0.0";
    public string Description => "Contains simple ROC indicators.";

    public void RegisterIndicators(IIndicatorRegistrationContext context)
    {
        context.Register(
            "SimpleMomentum",
            (factory, manager, config, options) =>
            {
                if (!config.Parameters.Contains("Period"))
                    config.SetParam("Period", null, 14);
                return new SimpleMomentumIndicator(factory, manager, config);
            },
            [
                new IndicatorParameterInfo("Period", "Period", typeof(int), 14, MinValue: 1, MaxValue: 500),
            ]);
    }
}
```

---

### 4. Chạy Plugin của bạn trong ứng dụng Platinum Trade

Sau khi biên dịch thành công dự án, bạn sẽ nhận được một tệp assembly `.dll` trong thư mục đầu ra của dự án (ví dụ: `bin/Debug/net8.0/MyPlugin.dll`).

Để tải nó vào giao diện GUI của **Platinum Trade App**:

- **Chỉ báo (Indicators)**:
  1. Trong menu chính của ứng dụng, chọn **Tools > Indicator Plugin Manager**.
  2. Bấm nút **Load Plugin**.
  3. Chọn tệp `.dll` chỉ báo đã biên dịch của bạn để tải và kích hoạt nó.
- **Chiến thuật (Strategies)**:
  1. Mở một tab **Strategy Workspace** trên giao diện chính.
  2. Trên tab **Settings**, tìm trường **Strategy File**.
  3. Click vào nút duyệt (**`...`**) và chọn tệp `.dll` chiến thuật đã biên dịch của bạn.
  4. Tùy chỉnh các thông số dưới tab **Input Parameters**, cấu hình tài khoản/symbol mục tiêu và nhấn **Start Trading** (hoặc **Start Backtest**).

---

## Tài liệu hướng dẫn (Documentation)

Tài liệu đầy đủ:
https://vntradesoft.github.io/PlatinumTrade.Docs/

Tài liệu tham khảo API:
https://vntradesoft.github.io/PlatinumTrade.Docs/sdk/api/index.html

<details>
<summary>Xây dựng tài liệu cục bộ (dành cho người đóng góp)</summary>

```bash
dotnet tool install -g docfx
docfx docs/docfx.json
```

</details>

---

## Các ví dụ mẫu (Examples)

Các mã nguồn triển khai mẫu sau đây có sẵn để tham khảo:

- **Ví dụ Chỉ báo**: Chi tiết mã nguồn trong thư mục [examples/Pt.Examples.Indicator/](examples/Pt.Examples.Indicator).
- **Ví dụ Chiến thuật**: Chi tiết mã nguồn trong thư mục [examples/Pt.Example.Stgy.UpTrend/](examples/Pt.Example.Stgy.UpTrend).

---

## Các nền tảng hỗ trợ

- .NET 8
- .NET 9
- .NET 10

---

## Cộng đồng & Hỗ trợ

- **Thảo luận GitHub:** [vntradesoft/PlatinumTrade.App/discussions](https://github.com/vntradesoft/PlatinumTrade.App/discussions)
- **Báo lỗi:** [Báo cáo lỗi hoặc đề xuất tính năng](https://github.com/vntradesoft/PlatinumTrade.Sdk/issues)
- **Discord:** [Tham gia cộng đồng chuyên nghiệp PlatinumTrade](https://discord.gg/UBV8YnMJs)

---

## Đóng góp (Contributing)

Mọi đóng góp, báo cáo lỗi và yêu cầu tính năng đều được hoan nghênh. Bạn có thể thoải mái mở issues hoặc gửi pull requests.

---

## Cấp phép (License)

Dự án này được cấp phép theo điều khoản của **MIT License** - xem chi tiết tại tệp [LICENSE](LICENSE).

---

## Nhật ký thay đổi (Changelog)

### [0.9.3-beta.2] - 2026-07-08

#### Tính năng mới
- **sdk:** Đồng bộ với phiên bản ứng dụng App và nâng cấp JK.OKX.Net lên 5.0.2 (Thay đổi lớn gây lỗi tương thích cũ).

### [0.9.0-beta.4] - 2026-07-08

#### Tính năng mới
- **sdk:** Đồng bộ với phiên bản ứng dụng App.

### [0.9.0-beta.3] - 2026-07-08

#### Tính năng mới
- **sdk:** Thêm các template dự án dotnet cho chiến thuật và chỉ báo.

### [0.9.0-beta.2] - 2026-07-06

#### Tính năng mới
- **sdk:** Cập nhật các lớp trừu tượng cho lõi chiến thuật, plugin, chỉ báo và backtest.

### [0.9.0-beta.1] - 2026-07-05

#### Tính năng mới
- **sdk:** Bản phát hành Beta đầu tiên.
  - Cung cấp các giao diện public để phát triển plugin.
  - Phát hành gói NuGet `Pt.Okx.Sdk` đầu tiên.
