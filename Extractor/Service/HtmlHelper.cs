namespace Extractor.Service
{
    static class HtmlHelper
    {
        public static string EndTag(string tagName)
        {
            return $"</{tagName}>";
        }

        public static string StartTag(string tagName)
        {
            return $"<{tagName}>";
        }
        public static string StartTagWithAttribute(string tagName, string attrName, string attrValue)
        {
            return $"<{tagName} {attrName}=\"{attrValue}\">";
        }
        public static string StartTagWithClass(string tagName, string className)
        {
            return StartTagWithAttribute(tagName, "class", className);
        }
        public static string StartTagWithId(string tagName, string idValue)
        {
            return StartTagWithAttribute(tagName, "id", idValue);
        }
        public static string PairTags(string tagName, string content)
        {
            return $"{StartTag(tagName)}{content}{EndTag(tagName)}";
        }
        public static string PairTagsWithClass(string tagName, string className, string content)
        {
            return $"{StartTagWithClass(tagName, className)}{content}{EndTag(tagName)}";
        }
        public static string PairTagsWithId(string tagName, string idValue, string content)
        {
            return $"{StartTagWithId(tagName, idValue)}{content}{EndTag(tagName)}";
        }
        public static string PairTagsWithAttribute(string tagName, string attrName, string attrValue, string content)
        {
            return $"{StartTagWithAttribute(tagName, attrName, attrValue)}{content}{EndTag(tagName)}";
        }
        public static string StartTagWithManyAttributes(string tagName, (string attrName, string attrValue)[] attributes)
        {
            var s = $"<{tagName}";
            foreach (var (attrName, attrValue) in attributes)
                s += $" {attrName}=\"{attrValue}\"";
            return s += ">";
        }
        public static string PairTagsWithManyAttributes(string tagName, (string attrName, string attrValue)[] attributes, string content)
        {
            return $"{StartTagWithManyAttributes(tagName, attributes)}{content}{EndTag(tagName)}";
        }
        public static string Link(string src, string content)
        {
            return PairTagsWithAttribute("a", "href", src, content);
        }
        public static string LinkWithClass(string src, string className, string content)
        {
            return PairTagsWithManyAttributes("a", new[]{("href", src), ("class", className)}, content);
        }
    }
}