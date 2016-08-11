using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Threads.Marker;
using Threads.Marker.Commands;

namespace Threads.Player {
    internal static class Formatting {
        internal static void FormatTextBlock(TextSequence sequence, ref TextBlock textBlock) {
            bool isBold = false, isItalic = false;
            foreach(var seq in sequence.Instructions) {
                switch(seq.Command) {
                    case Command.TextStyle:
                        var styleCmd = (StyleCommand) seq;
                        switch(styleCmd.TextStyle) {
                            case TextStyle.Bold:
                                isBold = !isBold;
                                break;
                            case TextStyle.Italic:
                                isItalic = !isItalic;
                                break;
                        }
                        break;
                    case Command.Text:
                        var textCmd = (TextCommand) seq;
                        var run = new Run(textCmd.Text);
                        if(isBold) run.FontWeight = FontWeights.Bold;
                        if(isItalic) run.FontStyle = FontStyles.Italic;
                        textBlock.Inlines.Add(run);
                        break;
                }
            }
        }
    }
}
