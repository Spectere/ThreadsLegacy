﻿using Threads.Interpreter.Types;
using Threads.Marker;

namespace Threads.Interpreter.Objects.Page {
    public interface IPageObject : IObject {
        /// <summary>
        /// The formatted text sequence for this <see cref="IPageObject" />.
        /// </summary>
        TextSequence FormattedText { get; set; }

        /// <summary>
        /// A textual description of this <see cref="IPageObject" />.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// The style that should be applied to this <see cref="IPageObject" />.
        /// </summary>
        Style Style { get; set; }

        /// <summary>
        /// Returns a <see cref="TextSequence" /> object with variable substitutions performed.
        /// </summary>
        /// <param name="storyData"></param>
        /// <returns></returns>
        TextSequence DisplayText(Data storyData);
    }
}
