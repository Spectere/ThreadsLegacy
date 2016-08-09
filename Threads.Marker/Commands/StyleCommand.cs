namespace Threads.Marker.Commands {
    public class StyleCommand : IInstruction {
        public Command Command => Command.TextStyle;
        public TextStyle TextStyle { get; set; }
    }
}
