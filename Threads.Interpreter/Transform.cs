﻿using System;
using System.Collections.Generic;
using System.Linq;
using Threads.Interpreter.Exceptions;
using Threads.Interpreter.PageObject;
using Threads.Interpreter.Schema;
using Threads.Interpreter.Types;
using Story = Threads.Interpreter.Types.Story;

namespace Threads.Interpreter {
    internal static class Transform {
        /// <summary>
        /// Transforms a deserialized XML into a validated set of story objects.
        /// </summary>
        /// <param name="storyData">A set of deserialized story data.</param>
        /// <returns>A <see cref="Types.Story"/> object containing the validated story data.</returns>
        internal static Story TransformStory(Schema.Story storyData) {
            var story = new Story {
                Information = TransformInformation(storyData.Information),
                Pages = TransformPages(storyData.Pages)
            };
            story.Configuration = TransformConfiguration(storyData.Configuration, story.Pages);

            return story;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="pages"></param>
        /// <returns></returns>
        private static Configuration TransformConfiguration(ConfigurationType configuration, IEnumerable<Page> pages) {
            var pageList = pages.ToList();

            // Throw an exception if the page doesn't exist.
            if(pageList.Count(e => e.Name == configuration.FirstPage) == 0) {
                throw new PageNotFoundException(configuration.FirstPage);
            }

            return new Configuration { FirstPage = pageList.First(e => e.Name == configuration.FirstPage) };
 ;       }

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
        /// <param name="pageObject">The XML object to transform.</param>
        /// <returns>A page object based on the XML object data.</returns>
        private static IPageObject TransformPageObject(Schema.PageObject pageObject) {
            IPageObject newObject;

            // Determine the specific type of PageObject and add it to the list.
            if(pageObject.GetType() == typeof(PageTypeChoice)) {
                var choice = (PageTypeChoice) pageObject;
                newObject = new Choice {
                    FormattedText = Marker.Parser.Parse(choice.Value),
                    Shortcut = Convert.ToChar(choice.Shortcut.Substring(0, 1)),
                    TargetName = choice.Target
                };
            } else {
                // If all else fails, it's probably a paragraph.
                newObject = new Paragraph {
                    FormattedText = Marker.Parser.Parse(pageObject.Value)
                };
            }

            // Apply style.
            newObject.Style = TransformStyle(pageObject, newObject.Style);

            return newObject;
        }

        /// <summary>
        /// Transforms deserialized page data into a story object and validates the contained choices.
        /// </summary>
        /// <param name="pages">A set of deserialized page data.</param>
        /// <returns>A collection of <see cref="Page"/> objects containing the story information.</returns>
        private static IEnumerable<Page> TransformPages(IEnumerable<PageType> pages) {
            var output = new List<Page>();
            var pageList = pages.ToList();

            // Convert page objects.
            foreach(var page in pageList) {
                var newPage = new Page {
                    Name = page.Name
                };

                foreach(var obj in page.Items) {
                    newPage.Objects.Add(TransformPageObject(obj));
                }
                output.Add(newPage);
            }

            // Make sure that choices are tied to valid pages.
            foreach(var page in output) {
                foreach(var choiceObject in page.Objects.Where(e => e.Type == PageObjectType.Choice)) {
                    var choice = (Choice)choiceObject;
                    choice.Target = output.First(c => c.Name == choice.TargetName);
                }
            }

            return output;
        }

        /// <summary>
        /// Apples the style of an XML <see cref="Schema.PageObject" /> on top of a default style.
        /// </summary>
        /// <param name="pageObject">The <see cref="Schema.PageObject" /> to pull the style values from.</param>
        /// <param name="defaultStyle">The <see cref="PageObjectStyle" /> to apply the updated style to.</param>
        /// <returns>A <see cref="PageObjectStyle" /> containing the merged style data.</returns>
        private static PageObjectStyle TransformStyle(Schema.PageObject pageObject, PageObjectStyle defaultStyle) {
            var newStyle = defaultStyle;

            if(pageObject.MarginBottomSpecified) newStyle.MarginBottom = pageObject.MarginBottom;
            if(pageObject.MarginTopSpecified) newStyle.MarginTop = pageObject.MarginTop;

            return newStyle;
        }
    }
}
