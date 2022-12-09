using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BookBurrow.Models
{
    public class BookStatus
    {
        public static BookStatus TBR { get; } = new BookStatus(0, "To be read");
        public static BookStatus CurrentlyReading { get; } = new BookStatus(1, "Currently reading");
        public static BookStatus Read { get; } = new BookStatus(2, "Read");
        public static BookStatus DNF { get; } = new BookStatus(3, "Did not finish");

        public string Name { get; private set; }
        public int Value { get; private set; }

        private BookStatus(int val, string name)
        {
            Value = val;
            Name = name;
        }

        [JsonConstructor]
        public BookStatus(string name, int value)
        {
            Value = value;
            Name = name;
        }

        private static List<BookStatus> ListBookStatuses()
        {
            return typeof(BookStatus).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.PropertyType == typeof(BookStatus))
                .Select(pi => (BookStatus)pi.GetValue(null, null))
                .OrderBy(p => p.Name)
                .ToList();
        }

        public static BookStatus FromString(string statusString)
        {
            return ListBookStatuses().Single(r => String.Equals(r.Name, statusString, StringComparison.OrdinalIgnoreCase));
        }

        public static BookStatus FromValue(int value)
        {
            return ListBookStatuses().Single(r => r.Value == value);
        }
    }
}
