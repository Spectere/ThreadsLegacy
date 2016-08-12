﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Threads.Interpreter.PageObject;
using Threads.Marker;

namespace Threads.Player.UIObjects {
    /// <summary>
    /// An interface describing a visible page object.
    /// </summary>
    internal abstract class UIPageObject : ContentControl {
        private TextSequence _formattedText;
        
        /// <summary>
        /// A <see cref="TextBlock" /> containing the data in <see cref="FormattedText" />.
        /// </summary>
        protected TextBlock TextBlock;

        /// <summary>
        /// The type of <see cref="PageObject" /> that this control handles.
        /// </summary>
        public virtual PageObjectType HandledType => PageObjectType.Unknown;

        /// <summary>
        /// A <see cref="TextSequence" /> that should be associated with this page object.
        /// </summary>
        public TextSequence FormattedText {
            get { return _formattedText; }
            set {
                _formattedText = value;
                TextBlock.Inlines.Clear();
                Formatting.FormatTextBlock(_formattedText, ref TextBlock);
            }
        }

        /// <summary>
        /// Applies a <see cref="PageObjectStyle" /> to this object instance.
        /// </summary>
        public PageObjectStyle PageObjectStyle {
            set { Margin = new Thickness(value.MarginLeft, value.MarginTop, value.MarginRight, value.MarginBottom); }
        }

        /// <summary>
        /// Initializes a new <see cref="UIPageObject" /> with a <see cref="TextSequence" />.
        /// </summary>
        /// <param name="pageObject">A <see cref="PageObject" /> to use to populate and style this control.</param>
        protected UIPageObject(IPageObject pageObject) {
            TextBlock = new TextBlock {
                FontFamily = new FontFamily("Cambria"),
                FontSize = 24.0,
                TextWrapping = TextWrapping.WrapWithOverflow
            };

            FormattedText = pageObject.FormattedText;
            PageObjectStyle = pageObject.Style;
        }
    }
}