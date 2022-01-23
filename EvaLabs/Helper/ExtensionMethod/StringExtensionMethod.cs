using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvaLabs.Helper.ExtensionMethod
{
    public static class StringExtensionMethod
    {
        public static string AddSpacesToSentence(this string value)
        {
            return Regex.Replace(value ?? string.Empty, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
        }

        public static string CSharpName(this Type type)
        {
            var sb = new StringBuilder();
            var name = type.Name;
            if (!type.IsGenericType) return name;
            sb.Append(name[..name.IndexOf('`')]);
            sb.Append('<');
            sb.Append(string.Join(", ", type.GetGenericArguments()
                .Select(t => t.CSharpName())));
            sb.Append('>');
            return sb.ToString();
        }

        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                select new { Id = e, Name = e.ToString() };
            return new SelectList(values, "Id", "Name", enumObj);
        }
    }
}