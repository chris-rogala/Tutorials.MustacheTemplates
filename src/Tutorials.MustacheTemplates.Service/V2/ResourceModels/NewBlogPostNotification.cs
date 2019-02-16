using System.Collections.Generic;

namespace Tutorials.MustacheTemplates.Service.V2.ResourceModels
{
    /// <summary>
    /// Model used to generate the <see cref="BlogPostNotification"/>
    /// </summary>
    public class NewBlogPostNotification
    {
        /// <summary>
        /// Collection of email addresses
        /// </summary>
        public List<string> EmailAddresses { get; set; }
        /// <summary>
        /// The name of the blog to locate the mustache template
        /// </summary>
        public string BlogName { get; set; }
    }
}
