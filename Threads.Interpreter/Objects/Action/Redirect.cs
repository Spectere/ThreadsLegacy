namespace Threads.Interpreter.Objects.Action {
    public class Redirect : ActionObject {
        private Types.Page _target;

        /// <summary>
        /// The target <see cref="Types.Page" /> that this redirect should point to.
        /// </summary>
        public Types.Page Target {
            get { return _target; }
            set {
                _target = value;
                TargetName = _target.Name;
            }
        }

        /// <summary>
        /// The name of the target page that this redirect leads to.
        /// </summary>
        public string TargetName { get; set; }

        public override void Activate() {
            Engine.CurrentPage = Target;
        }

        internal override Schema.ActionObject ExportObject() {
            return new Schema.RedirectObject {
                Target = Target.Name
            };
        }
    }
}
