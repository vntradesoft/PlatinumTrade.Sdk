using Pt.Okx.Sdk.Drawing.Enums;
using Pt.Okx.Sdk.Drawing.Objects;

namespace Pt.Okx.Sdk.Drawing.Events;

/// <summary>
/// Provides data for drawing change events,
/// describing what changed and the affected object.
/// </summary>
/// <remarks>
/// Depending on the <see cref="ChangeType"/>:
/// <list type="bullet">
/// <item>
/// <see cref="DrawingChangeType.Added"/> or <see cref="DrawingChangeType.Updated"/>:
/// <see cref="Object"/> is populated.
/// </item>
/// <item>
/// <see cref="DrawingChangeType.Removed"/>:
/// <see cref="ObjectId"/> is typically used.
/// </item>
/// </list>
/// </remarks>
public class DrawingChangeEventArgs : EventArgs
{
    /// <summary>
    /// Gets the type of change (Added, Updated, or Removed).
    /// </summary>
    public DrawingChangeType ChangeType { get; }

    /// <summary>
    /// Gets the affected drawing object.
    /// This is populated for Added and Updated events.
    /// </summary>
    public DrawingObject? Object { get; }

    /// <summary>
    /// Gets the identifier of the affected object.
    /// This is typically used for Removed events.
    /// </summary>
    public string? ObjectId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawingChangeEventArgs"/> class.
    /// </summary>
    /// <param name="changeType">The type of change.</param>
    /// <param name="obj">
    /// The drawing object involved in the change (for Added/Updated).
    /// </param>
    /// <param name="objectId">
    /// The identifier of the removed object (for Removed).
    /// </param>
    public DrawingChangeEventArgs(
        DrawingChangeType changeType,
        DrawingObject? obj = null,
        string? objectId = null)
    {
        ChangeType = changeType;
        Object = obj;
        ObjectId = objectId;
    }
}
