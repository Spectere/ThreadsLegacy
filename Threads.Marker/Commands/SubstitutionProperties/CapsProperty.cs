namespace Threads.Marker.Commands.SubstitutionProperties {
    public enum CapsProperty {
        /// <summary>
        /// Capitalization on the substituted variable is not changed.
        /// </summary>
        None,

        /// <summary>
        /// The first letter of the substituted value is capitalized.
        /// </summary>
        First,

        /// <summary>
        /// The entire substituted value is displayed in uppercase.
        /// </summary>
        Upper,

        /// <summary>
        /// The entire substituted value is displayed in lowercase.
        /// </summary>
        Lower
    }
}
