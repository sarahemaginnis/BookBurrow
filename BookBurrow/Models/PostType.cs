using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Newtonsoft.Json;

namespace BookBurrow.Models
{
    public class PostType
    {
        public static PostType Text { get; } = new PostType(0, "Text");
        public static PostType Photo { get; } = new PostType(1, "Photo");
        public static PostType Quote { get; } = new PostType(2, "Quote");
        public static PostType Link { get; } = new PostType(3, "Link");
        public static PostType Chat { get; } = new PostType(4, "Chat");
        public static PostType Audio { get; } = new PostType(5, "Audio");
        public static PostType Video { get; } = new PostType(6, "Video");

        public string Name { get; private set; }
        public int Value { get; private set; }

        private PostType(int val, string name)
        {
            Value = val;
            Name = name;
        }

        [JsonConstructor]
        public PostType(string name, int value)
        {
            Value = value;
            Name = name;
        }

        private static List<PostType> ListPostTypes()
        {
            return typeof(PostType).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.PropertyType == typeof(PostType))
                .Select(pi => (PostType)pi.GetValue(null, null))
                .OrderBy(p => p.Name)
                .ToList();
        }

        public static PostType FromString(string statusString)
        {
            return ListPostTypes().Single(r => String.Equals(r.Name, statusString, StringComparison.OrdinalIgnoreCase));
        }

        public static PostType FromValue(int value)
        {
            return ListPostTypes().Single(r => r.Value == value);
        }
    }
}
