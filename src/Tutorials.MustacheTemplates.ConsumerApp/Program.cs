using MustacheTemplateService.Sdk.ApiAccessors;
using MustacheTemplateService.Sdk.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Tutorials.MustacheTemplates.ConsumerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var notification = new NewBlogPostNotification
            {
                BlogName = "MustacheTemplates",
                EmailAddresses = new List<string>
                {
                    "hello@notyourdadscode.com",
                    "chris@notyourdadscode.com"
                }
            };
            var client = new BlogPostNotificationsApi("https://localhost:44363");

            var result = client.GenerateWithHttpInfo(notification);

            Console.Write(JsonConvert.SerializeObject(result, Formatting.Indented));
            Console.ReadKey();
        }
    }
}
