using System.Diagnostics.Tracing;

namespace Threads.Marker {
    /// <summary>
    /// Indicates the type of command that is being passed.
    /// </summary>
    public enum Command {
        None,
        Text,
        Escape,
        TextStyle
    }
}
