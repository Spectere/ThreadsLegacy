using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Threads.Interpreter.Types;
using Threads.Marker;
using Threads.Marker.Commands;
using Threads.Marker.Commands.SubstitutionProperties;

namespace Threads.Interpreter.Objects.Page {
    /// <summary>
    /// A class implementing <see cref="IPageObject" />. This class must be inherited.
    /// </summary>
    public abstract class PageObject : IPageObject {
        public string Name { get; set; }
        public string HideIf { get; set; }
        public string ShowIf { get; set; }

        /// <summary>
        /// The formatted <see cref="TextSequence" /> for this <see cref="PageObject" />.
        /// </summary>
        public virtual TextSequence FormattedText { get; set; }

        /// <summary>
        /// A textual description of this <see cref="PageObject" />.
        /// </summary>
        public virtual string Text => FormattedText.ToString();

        /// <summary>
        /// The default style for this <see cref="PageObject" />.
        /// </summary>
        public abstract Style DefaultStyle { get; }

        /// <summary>
        /// The style that should be applied to this <see cref="PageObject" />.
        /// </summary>
        public Style Style { get; set; }

        /// <summary>
        /// Initializes a new instance of this <see cref="PageObject"/>.
        /// </summary>
        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        [SuppressMessage("ReSharper", "DoNotCallOverridableMethodsInConstructor")]
        protected PageObject() {
            Style = DefaultStyle;
            FormattedText = new TextSequence();
        }

        /// <summary>
        /// Returns a <see cref="TextSequence" /> object with variable substitutions performed.
        /// </summary>
        /// <param name="storyData">The <see cref="Data" /> object associated with the active story.</param>
        /// <returns>A <see cref="TextSequence" /> with variable substitutions performed.</returns>
        public TextSequence DisplayText(Data storyData) {
            return DisplayText(FormattedText, storyData);
        }

        /// <summary>
        /// Returns a <see cref="TextSequence" /> object with variable substitutions performed.
        /// </summary>
        /// <param name="textSequence">A <see cref="TextSequence" /> object to perform substitutions on.</param>
        /// <param name="storyData">The <see cref="Data" /> object associated with the active story.</param>
        /// <returns>A <see cref="TextSequence" /> with variable substitutions performed.</returns>
        protected TextSequence DisplayText(TextSequence textSequence, Data storyData) {
            var newSequence = new TextSequence();

            foreach(var instruction in textSequence.Instructions) {
                switch(instruction.Command) {
                    case Command.TextStyle:
                    case Command.Text:
                        newSequence.Instructions.Add(instruction);
                        break;
                    case Command.Substitution:
                        var substitution = (SubstitutionCommand)instruction;
                        var newInstruction = new TextCommand();
                        var value = storyData.GetVariable(substitution.Variable);

                        // Handle flag settings.
                        switch(substitution.Flag) {
                            case FlagProperty.TrueFalse:
                                newInstruction.Text = value != 0 ? "true" : "false";
                                break;
                            case FlagProperty.YesNo:
                                newInstruction.Text = value != 0 ? "yes" : "no";
                                break;
                            case FlagProperty.OneZero:
                                newInstruction.Text = value;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        // Handle transformation (if applicable).
                        if(newInstruction.Text.Length > 0) {
                            switch(substitution.Caps) {
                                case CapsProperty.First:
                                    newInstruction.Text = newInstruction.Text.First().ToString().ToUpper() +
                                                          newInstruction.Text.Substring(1);
                                    break;
                                case CapsProperty.Lower:
                                    newInstruction.Text = newInstruction.Text.ToLower();
                                    break;
                                case CapsProperty.Upper:
                                    newInstruction.Text = newInstruction.Text.ToUpper();
                                    break;
                            }
                        }

                        newSequence.Instructions.Add(newInstruction);
                        break;
                }
            }

            return newSequence;
        }

        /// <summary>
        /// Exports this <see cref="PageObject" /> instance into an XML object.
        /// </summary>
        /// <returns>An XML <see cref="Schema.PageObject" />.</returns>
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public Schema.Object Export() {
            var xmlObject = ExportObject();

            // Export the name of this object.
            xmlObject.Name = Name;

            // Export the text in this object.
            xmlObject.Value = FormattedText.MarkupText;

            // Only export style if it doesn't match the default.
            xmlObject.MarginBottomSpecified = Style.MarginBottom != DefaultStyle.MarginBottom;
            xmlObject.MarginLeftSpecified = Style.MarginLeft != DefaultStyle.MarginLeft;
            xmlObject.MarginRightSpecified = Style.MarginRight != DefaultStyle.MarginRight;
            xmlObject.MarginTopSpecified = Style.MarginTop != DefaultStyle.MarginTop;

            // Export the style values.
            xmlObject.MarginBottom = Style.MarginBottom.GetValueOrDefault();
            xmlObject.MarginLeft = Style.MarginLeft.GetValueOrDefault();
            xmlObject.MarginRight = Style.MarginRight.GetValueOrDefault();
            xmlObject.MarginTop = Style.MarginTop.GetValueOrDefault();

            // Export the conditionals.
            xmlObject.ShowIf = ShowIf;
            xmlObject.HideIf = HideIf;

            return xmlObject;
        }

        /// <summary>
        /// Exports this <see cref="PageObject" /> instance into an XML object. This method must be implemented.
        /// </summary>
        /// <returns>An XML <see cref="Schema.PageObject" />.</returns>
        internal abstract Schema.PageObject ExportObject();

        public override string ToString() {
            var typeName = GetType().ToString().Split('.').Last();
            return string.IsNullOrWhiteSpace(Name) ? typeName : string.Format($"{typeName} - {Name}");
        }
    }
}
