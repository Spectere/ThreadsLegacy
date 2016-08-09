using System;
using System.Collections.Generic;
using System.Linq;
using Threads.Interpreter.Exceptions;
using Threads.Interpreter.PageObject;
using Threads.Interpreter.Types;

namespace Threads.Interpreter {
    internal static class Transform {
        /// <summary>
        /// Transforms a deserialized XML into a validated set of story objects.
        /// </summary>
        /// <param name="storyData">A set of deserialized story data.</param>
        /// <returns>A <see cref="Story"/> object containing the validated story data.</returns>
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
        private static Configuration TransformConfiguration(Schema.ConfigurationType configuration, IEnumerable<Page> pages) {
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
        private static Information TransformInformation(Schema.InformationType information) {
            return new Information {
                Author = information.Author,
                Name = information.Name,
                Version = information.Version,
                Website = information.Website
            };
        }

        /// <summary>
        /// Transforms deserialized page data into a story object and validates the contained choices.
        /// </summary>
        /// <param name="pages">A set of deserialized page data.</param>
        /// <returns>A collection of <see cref="Page"/> objects containing the story information.</returns>
        private static IEnumerable<Page> TransformPages(IEnumerable<Schema.PageType> pages) {
            var output = new List<Page>();
            var pageList = pages.ToList();

            // Convert page data.
            foreach(var page in pageList) {
                var newPage = new Page {
                    Name = page.Name,
                    Choices = new List<Choice>()
                };
                foreach(var text in page.Text) {
                    var para = new Paragraph { FormattedText = Marker.Parser.Parse(text) };
                    newPage.Objects.Add(para);
                }
                output.Add(newPage);
            }

            // Convert choices.
            foreach(var page in output) {
                var xmlPage = pageList.First(e => e.Name == page.Name);
                if(xmlPage.Choice == null) continue;
                foreach(var choice in xmlPage.Choice) {
                    if(output.Count(e => e.Name == choice.Target) == 0) {
                        // Specified Page could not be found!
                        throw new PageNotFoundException(choice.Target);
                    }

                    page.Choices.Add(new Choice {
                        FormattedText = Marker.Parser.Parse(choice.Value),
                        Shortcut = Convert.ToChar(choice.Shortcut.Substring(0, 1)),
                        Target = output.First(e => e.Name == choice.Target)
                    });
                }
            }

            return output;
        }
    }
}
