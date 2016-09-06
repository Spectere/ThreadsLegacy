using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Threads.Editor.Objects.Action;
using Threads.Editor.Objects.Page;

namespace Threads.Editor.Objects {
    /// <summary>
    /// A class that allows lists of <see cref="EditorObject" />s to be returned to the editor application.
    /// </summary>
    internal static class EditorObjectList {
        /// <summary>
        /// Returns a collection of usable <see cref="ActionObject" />s.
        /// </summary>
        /// <returns>A collection of usable <see cref="ActionObject" />s.</returns>
        internal static ICollection<ActionObject> GetActionObjects() {
            var objects = Assembly.GetExecutingAssembly().GetTypes()
                                  .Where(t => t.Namespace == "Threads.Editor.Objects.Action"
                                              && t.IsAbstract == false && t.BaseType == typeof(ActionObject));

            return objects.Select(t => (ActionObject)Activator.CreateInstance(t, (ActionObject)null, null)).ToList();
        }


        /// <summary>
        /// Returns a collection of usable <see cref="PageObject" />s.
        /// </summary>
        /// <returns>A collection of usable <see cref="PageObject" />s.</returns>
        internal static ICollection<PageObject> GetPageObjects() {
            var objects = Assembly.GetExecutingAssembly().GetTypes()
                                  .Where(t => t.Namespace == "Threads.Editor.Objects.Page"
                                              && t.IsAbstract == false && t.BaseType == typeof(PageObject));

            return objects.Select(t => (PageObject)Activator.CreateInstance(t, (PageObject)null, null)).ToList();
        }
    }
}
