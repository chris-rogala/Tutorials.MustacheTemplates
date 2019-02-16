namespace Tutorials.MustacheTemplates.Service.V1.ResourceModels
{
    /// <summary>
    /// The notification with the email address and text
    /// </summary>
    public class BlogPostNotification
    {
        /// <summary>
        /// The email address for the notification
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// The text for the email
        /// </summary>
        public string Text { get; set; }
    }
}
