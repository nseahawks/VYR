/*using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Services
{
    public class MockImageService
    {
        private static MockImageService _instance;

        public static MockImageService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MockImageService();
                return _instance;
            }
        }

        public List<Models.Image> GetCommunityPosts()
        {
            return new List<Models.Image>
            {
                new Models.Image { Source = "template.jpg" },
                new Models.Image { Source = "template2.jpg" },
                new Models.Image { Source = "template3.jpg" }
            };
        }
    }
}*/
