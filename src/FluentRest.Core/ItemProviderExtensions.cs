﻿namespace KyubiCode.FluentRest
{
    using System;

    public static class ItemProviderExtensions
    {
        public static TItem GetItem<TItem>(this IItemProvider provider) =>
            (TItem)provider.GetItem(typeof(TItem));

        public static TItem GetRequiredItem<TItem>(this IItemProvider provider)
        {
            var item = provider.GetItem<TItem>();
            if (item != null)
            {
                return item;
            }

            throw new InvalidOperationException(
                $"Item of type {typeof(TItem)} is not available!");
        }
    }
}
