using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCrawlBookStore.Models
{
    public class Publisher
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }

    public static class FamousPublisher
    {
        public static List<string> list = new List<string>()
        {
            "Cambridge",
            "Cengage",
            "HarperCollins",
            "Hachette",
            "McGrawHill",
            "Macmillan",
            "Oxford",
            "Parragon",
            "Pearson",
            "Penguin",
            "Sterling",
            "Usborne"
        };
    }
}
