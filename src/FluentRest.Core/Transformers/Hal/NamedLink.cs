﻿namespace KyubiCode.FluentRest.Transformers.Hal
{
    using System.Collections.Generic;
    using System.Linq;

    public class NamedLink
    {
        public NamedLink(string name, Link link)
        {
            this.Name = name;
            this.Link = link;
        }

        public string Name { get; }

        public Link Link { get; }

        public bool IsLinkList { get; set; }

        public static IDictionary<string, object> BuildLinks(IEnumerable<NamedLink> links) =>
            links?.GroupBy(l => l.Name)
                .ToDictionary(g => g.Key, ToSingleOrList);

        private static object ToSingleOrList(IEnumerable<NamedLink> links)
        {
            var linkList = links.ToList();
            if (linkList.Count == 1 && !linkList.First().IsLinkList)
            {
                return linkList.First().Link;
            }

            return linkList.Select(l => l.Link)
                .ToList();
        }
    }
}
