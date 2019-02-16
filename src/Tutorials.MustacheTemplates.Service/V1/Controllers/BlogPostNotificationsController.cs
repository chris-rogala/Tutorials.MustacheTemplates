using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stubble.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tutorials.MustacheTemplates.Service.V1.ResourceModels;

namespace Tutorials.MustacheTemplates.Service.V1.Controllers
{
    [Route("api/v1/blog-post-notifications")]
    [ApiController]
#if VERSIONEDSWAGGER
    [ApiVersion("1.0")]
#endif
    public class BlogPostNotificationsController : ControllerBase
    {
        //Notes: This is a tutorial.  It is NOT an implementation example.

        /// <summary>
        /// Accepts a collection of email addresses and a name for a blog to generate a collection of messages for each email
        /// </summary>
        /// <param name="blogPostNotification">The model containing the email addresses and the name of the blog</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A collection of <see cref="BlogPostNotification"/>s</returns>
        [HttpPost]
        [ProducesResponseType(typeof(List<BlogPostNotification>), 200)]
        public async Task<IActionResult> Generate([FromBody] NewBlogPostNotification blogPostNotification,
            CancellationToken cancellationToken)
        {
            List<BlogPostNotification> result = new List<BlogPostNotification>();

            //Reads the Mustache template and the data to render
            string template = await GetTemplateAsync();
            object view = await GetTemplateDataAsync(blogPostNotification.BlogName);

            //Creates the message using Stubble
            string message = StaticStubbleRenderer.Render(template, view);

            //Adds the results
            result.AddRange(blogPostNotification.EmailAddresses
                .Select(x => new BlogPostNotification
                {
                    EmailAddress = x,
                    Text = message
                }));

            return Ok(result);
        }

        private async Task<string> GetTemplateAsync()
        {
            try
            {
                using (StreamReader r = new StreamReader($"Templates/NewBlogPostTemplate.txt"))
                {
                    return await r.ReadToEndAsync();
                }
            }
            catch
            {
                return null;
            }
        }

        private async Task<object> GetTemplateDataAsync(string name)
        {
            try
            {
                using (StreamReader r = new StreamReader($"Templates/Blogs/{name}.json"))
                {
                    string json = await r.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
