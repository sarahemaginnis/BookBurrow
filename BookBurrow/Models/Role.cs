using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Newtonsoft.Json;

namespace BookBurrow.Models
{
    public class Role
    {
        public static Role User { get; } = new Role(0, "User");
        public static Role Author { get; } = new Role(1, "Author");
        public static Role Librarian { get; } = new Role(2, "Librarian");

        public string Name { get; private set; }
        public int Value { get; private set; }

        private Role(int val, string name)
        {
            Value = val;
            Name = name;
        }

        [JsonConstructor]
        public Role(string name, int value)
        {
            Value = value;
            Name = name;
        }

        public static List<Role> ListRoles()
        {
            return typeof(Role).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.PropertyType == typeof(Role))
                .Select(pi => (Role)pi.GetValue(null, null))
                .OrderBy(p => p.Name)
                .ToList();
        }

        public static Role FromString(string statusString)
        {
            return ListRoles().Single(r => String.Equals(r.Name, statusString, StringComparison.OrdinalIgnoreCase));
        }

        public static Role FromValue(int value)
        {
            return ListRoles().Single(r => r.Value == value);
        }
    }
}
