using System;
using System.Collections.Generic;
using System.Linq;
using Threads.Interpreter.Exceptions;
using Threads.Interpreter.Objects;
using Threads.Interpreter.Objects.Action;
using Threads.Interpreter.Objects.Page;
using StoryActionObject = Threads.Interpreter.Objects.Action.ActionObject;
using StoryPageObject = Threads.Interpreter.Objects.Page.PageObject;
using Threads.Interpreter.Schema;
using Threads.Interpreter.Types;
using Threads.Marker;
using Story = Threads.Interpreter.Types.Story;
using Variable = Threads.Interpreter.Objects.Action.Variable;

namespace Threads.Interpreter {
    internal static class Transform {
        /// <summary>
        /// Transforms a deserialized XML into a validated set of story objects.
        /// </summary>
        /// <param name="storyData">A set of deserialized story data.</param>
        /// <param name="engine">A reference to the active <see cref="Engine" />.</param>
        /// <returns>A <see cref="Story"/> object containing the validated story data.</returns>
        internal static Story TransformStory(Schema.Story storyData, Engine engine) {
            var story = new Story();

            // If our story element is null, uh...abort?
            if(storyData == null)
                throw new NullStoryException();

            // We can survive without an information block.
            if(storyData.Information != null)
                story.Information = TransformInformation(storyData.Information);

            // Bail if we don't have any pages.
            if(storyData.Pages == null)
                throw new NullPagesException();
            story.Pages = TransformPages(storyData.Pages, engine);
            if(story.Pages.Count == 0)
                throw new NoPagesFoundException();

            // Finally, we can make do without a configuration section.
            if(storyData.Configuration != null)
                story.Configuration = TransformConfiguration(storyData.Configuration, story.Pages);

            return story;
        }

        /// <summary>
        /// Transforms the configuration section of a deserialized XML into a validated story configuration.
        /// </summary>
        /// <param name="configuration">A set of deserialized configuration data.</param>
        /// <param name="pages">The set of pages used by the loaded story.</param>
        /// <returns>A <see cref="Configuration"/> object containing the validated configuration data.</returns>
        private static Configuration TransformConfiguration(ConfigurationType configuration, IEnumerable<Page> pages) {
            var pageList = pages.ToList();

            var newConfig = new Configuration {
                FirstPage = configuration.FirstPage == null ? pageList.First() : pageList.First(e => e.Name == configuration.FirstPage),
                StoryMarginLeft = configuration.StoryMarginLeftSpecified ? configuration.StoryMarginLeft : 40.0,
                StoryMarginRight = configuration.StoryMarginRightSpecified ? configuration.StoryMarginRight : 40.0
            };

            // Check to see if the first page is valid.
            if(newConfig.FirstPage == null)
                throw new PageNotFoundException(configuration.FirstPage);

            return newConfig;
        }

        /// <summary>
        /// Transforms a deserialized information block into a story object.
        /// </summary>
        /// <param name="information">The deserialized information block.</param>
        /// <returns>An <see cref="Information"/> object containing the story information.</returns>
        private static Information TransformInformation(InformationType information) {
            return new Information {
                Author = information.Author,
                Name = information.Name,
                Version = information.Version,
                Website = information.Website
            };
        }

        /// <summary>
        /// Transform an XML object into a page object.
        /// </summary>
        /// <param name="obj">The XML object to transform.</param>
        /// <param name="engine">A reference to the active <see cref="Engine" />.</param>
        /// <returns>A page object based on the XML object data.</returns>
        private static IObject TransformObject(Schema.Object obj, Engine engine) {
            IObject newObject;

            // Determine the specific type of Object and add it to the list.
            // TODO: This sucks. Make it suck less.
            if(obj.GetType() == typeof(FlagObject)) {
                var flag = (FlagObject)obj;
                newObject = new Flag();
                if(flag.SettingSpecified) {
                    switch(flag.Setting) {
                        case FlagObjectSetting.set:
                            ((Flag)newObject).Setting = Flag.FlagAction.Set;
                            break;
                        case FlagObjectSetting.clear:
                            ((Flag)newObject).Setting = Flag.FlagAction.Clear;
                            break;
                        case FlagObjectSetting.toggle:
                            ((Flag)newObject).Setting = Flag.FlagAction.Toggle;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            } else if(obj.GetType() == typeof(VariableObject)) {
                var variable = (VariableObject)obj;
                newObject = new Variable { Operation = Variable.VariableAction.Set, Expression = variable.Expression };
                if(variable.OperationSpecified) {
                    switch(variable.Operation) {
                        case VariableObjectOperation.set:
                            ((Variable)newObject).Operation = Variable.VariableAction.Set;
                            break;
                        case VariableObjectOperation.add:
                            ((Variable)newObject).Operation = Variable.VariableAction.Add;
                            break;
                        case VariableObjectOperation.subtract:
                            ((Variable)newObject).Operation = Variable.VariableAction.Subtract;
                            break;
                        case VariableObjectOperation.multiply:
                            ((Variable)newObject).Operation = Variable.VariableAction.Multiply;
                            break;
                        case VariableObjectOperation.divide:
                            ((Variable)newObject).Operation = Variable.VariableAction.Divide;
                            break;
                        case VariableObjectOperation.modulus:
                            ((Variable)newObject).Operation = Variable.VariableAction.Modulus;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            } else if(obj.GetType() == typeof(RedirectObject)) {
                var redirect = (RedirectObject)obj;
                newObject = new Redirect {
                    TargetName = redirect.Target
                };
            } else if(obj.GetType() == typeof(ChoiceObject)) {
                var choice = (ChoiceObject)obj;
                newObject = new Choice {
                    FormattedText = new TextSequence(choice.Value), Shortcut = string.IsNullOrEmpty(choice.Shortcut) ? (char?)null : Convert.ToChar(choice.Shortcut.Substring(0, 1)), TargetName = choice.Target
                };
            } else if(obj.GetType() == typeof(ImageObject)) {
                var image = (ImageObject)obj;
                newObject = new Image {
                    FormattedText = new TextSequence(image.Value), Source = image.Source
                };
            } else {
                // If all else fails, it's probably a paragraph.
                newObject = new Paragraph {
                    FormattedText = new TextSequence(obj.Value)
                };
            }

            // Transform common properties.
            newObject.Name = obj.Name;
            newObject.ShowIf = obj.ShowIf;
            newObject.HideIf = obj.HideIf;

            // Apply style (if this is a PaegObject)
            if(obj.GetType().BaseType == typeof(Schema.PageObject)) {
                var pageObject = (StoryPageObject)newObject;
                pageObject.Style = TransformStyle((Schema.PageObject)obj, pageObject.Style);
            }

            // Apply ActionObject properties (if applicable).
            if(obj.GetType().BaseType == typeof(Schema.ActionObject)) {
                var actionObject = (StoryActionObject)newObject;
                actionObject.Engine = engine;
            }

            return newObject;
        }

        /// <summary>
        /// Transforms deserialized page data into a story object and validates the contained choices.
        /// </summary>
        /// <param name="pages">A set of deserialized page data.</param>
        /// <param name="engine">A reference to the active <see cref="Engine" />.</param>
        /// <returns>A collection of <see cref="Page"/> objects containing the story information.</returns>
        private static ICollection<Page> TransformPages(IEnumerable<PageType> pages, Engine engine) {
            var output = new List<Page>();
            var pageList = pages.ToList();

            // Convert page objects.
            foreach(var page in pageList) {
                var newPage = new Page {
                    Name = page.Name
                };

                if(page.Items != null) {
                    foreach(var obj in page.Items) {
                        newPage.Objects.Add(TransformObject(obj, engine));
                    }
                }

                output.Add(newPage);
            }

            // Make sure that choices and redirects are tied to valid pages.
            // TODO: This implementation kind of sucks. Come up with a better way.
            foreach(var page in output) {
                foreach(var choiceObject in page.Objects.Where(e => e.GetType() == typeof(Choice))) {
                    var choice = (Choice)choiceObject;
                    choice.Target = output.First(c => c.Name == choice.TargetName);
                }

                foreach(var redirectObject in page.Objects.Where(e => e.GetType() == typeof(Redirect))) {
                    var redirect = (Redirect)redirectObject;
                    redirect.Target = output.First(r => r.Name == redirect.TargetName);
                }
            }

            return output;
        }

        /// <summary>
        /// Apples the style of an XML <see cref="Schema.PageObject" /> on top of a default style.
        /// </summary>
        /// <param name="pageObject">The <see cref="Schema.PageObject" /> to pull the style values from.</param>
        /// <param name="defaultStyle">The <see cref="Style" /> to apply the updated style to.</param>
        /// <returns>A <see cref="Style" /> containing the merged style data.</returns>
        private static Style TransformStyle(Schema.PageObject pageObject, Style defaultStyle) {
            var newStyle = defaultStyle;

            if(pageObject.MarginBottomSpecified) newStyle.MarginBottom = pageObject.MarginBottom;
            if(pageObject.MarginLeftSpecified) newStyle.MarginLeft = pageObject.MarginLeft;
            if(pageObject.MarginRightSpecified) newStyle.MarginRight = pageObject.MarginRight;
            if(pageObject.MarginTopSpecified) newStyle.MarginTop = pageObject.MarginTop;

            return newStyle;
        }
    }
}
