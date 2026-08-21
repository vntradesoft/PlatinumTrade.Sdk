namespace Pt.Okx.Sdk.Enums;

/// <summary>Kết quả của thao tác unload plugin.</summary>
public enum UnloadPluginResult
{
    /// <summary>Gỡ khỏi bộ nhớ và xoá file thành công.</summary>
    Unloaded,
    /// <summary>Đã gỡ đăng ký nhưng file còn khoá — sẽ xoá khi khởi động lại.</summary>
    PendingRestart,
    /// <summary>Không tìm thấy plugin đang load với đường dẫn này.</summary>
    NotFound
}
