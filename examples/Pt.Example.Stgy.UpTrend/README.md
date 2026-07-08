# Pt.Example.Stgy.UpTrend

Simple strategy example for PlatinumTrade SDK.

Behavior:
- Create SuperTrend indicator in `InitializeAsync`.
- On bullish reversal (confirmed 1 bar): place one market buy.
- On bearish reversal (confirmed 1 bar): close net position.

Notes:
- This is a learning/demo example.
- No advanced risk engine, no TP/SL manager, no telegram command extension.
- Use carefully before any live deployment.

SDK architecture note:
- The engine owns internal state and event dispatch.
- Strategy market callback is `OnTickAsync(TickPhase tickPhase, CancellationToken ct)`.
- `TickPhase.Tick` is an intra-bar update.
- `TickPhase.BarClose` is a closed-bar update.
