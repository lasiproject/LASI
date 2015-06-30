using System;
using System.Collections.Generic;
using System.Windows;
using LASI.Utilities;

namespace LASI.App.Helpers
{
    internal static class UiElementExtensions
    {
        public static void Hide(this UIElement element) => setVisibility(element, Visibility.Hidden);
        public static void Show(this UIElement element) => setVisibility(element, Visibility.Visible);

        public static void Hide(this IEnumerable<UIElement> elements) => SetVisibility(elements, Visibility.Hidden);
        public static void Show(this IEnumerable<UIElement> elements) => SetVisibility(elements, Visibility.Visible);

        private static readonly Action<UIElement, Visibility> setVisibility = (element, visibility) => element.Visibility = visibility;
        private static void SetVisibility(IEnumerable<UIElement> elements, Visibility visibility)
        {
            var changeVisibility = setVisibility.Apply(visibility);
            foreach (var element in elements)
            {
                changeVisibility(element);
            }
        }
    }
}
