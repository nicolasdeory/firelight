namespace FirelightUI
{
    public class UIError
    {
        /// <summary>
        /// Internal error ID
        /// </summary>
        public string Id { get; set; }
        public string Title { get; set; }

        /// <summary>
        /// More descriptive title 
        /// </summary>
        public string DetailedTitle { get; set; }

        /// <summary>
        /// Call to action button text
        /// </summary>
        public string CtaText { get; set; }

        /// <summary>
        /// Where the call to action button brings the user to
        /// </summary>
        public string CtaUrl { get; set; }

        /// <summary>
        /// Id of HTML element to insert the error div below
        /// </summary>
        public string CtaElemId { get; set; }

        /// <summary>
        /// HTML description for the error
        /// </summary>
        public string Description { get; set; }
        
    }
}