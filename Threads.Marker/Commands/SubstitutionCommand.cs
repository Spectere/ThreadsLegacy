using Threads.Marker.Commands.SubstitutionProperties;

namespace Threads.Marker.Commands {
    class SubstitutionCommand : IInstruction {
        public Command Command => Command.Substitution;

        /// <summary>
        /// The variable name that should be displayed in place of this token.
        /// </summary>
        public string Variable { get; set; }

        /// <summary>
        /// Indicates to the interpreter how the substituted value should be capitalized.
        /// </summary>
        public CapsProperty Caps { get; set; }

        /// <summary>
        /// Indicates to the interpreter how boolean values should be handled.
        /// </summary>
        public FlagProperty Flag { get; set; }
    }
}
