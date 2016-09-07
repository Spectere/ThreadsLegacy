using System;
using System.Linq;
using Threads.Interpreter.Types;

namespace Threads.Interpreter.Objects.Action {
    public abstract class ActionObject : IActionObject {
        public string Name { get; set; }
        public string HideIf { get; set; }
        public string ShowIf { get; set; }
        public abstract void Activate();

        /// <summary>
        /// A reference to the active <see cref="Story" />.
        /// </summary>
        public Story Story { get; set; }

        /// <summary>
        /// Exports this <see cref="ActionObject" /> instance into an XML object.
        /// </summary>
        /// <returns>An XML <see cref="Schema.ActionObject" />.</returns>
        public Schema.Object Export() {
            var xmlObject = ExportObject();

            // Export the name of this object.
            xmlObject.Name = Name;

            // Export the conditionals.
            xmlObject.ShowIf = ShowIf;
            xmlObject.HideIf = HideIf;

            return xmlObject;
        }

        /// <summary>
        /// Exports this <see cref="ActionObject" /> instance into an XML object. This method must be implemented.
        /// </summary>
        /// <returns>An XML <see cref="Schema.ActionObject" />.</returns>
        internal abstract Schema.ActionObject ExportObject();

        public override string ToString() {
            return GetType().ToString().Split('.').Last();
        }
    }
}
