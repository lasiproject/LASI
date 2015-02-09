using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LASI.Utilities;

namespace LASI.App.UIElementHelpers
{
    internal static class UiElementExtensions
    {
        public static void Hide(this UIElement element) => setVisibility(element, Visibility.Hidden);
        public static void Show(this UIElement element) => setVisibility(element, Visibility.Visible);

        public static void Hide(this IEnumerable<UIElement> elements) => SetVisibility(elements, Visibility.Hidden);
        public static void Show(this IEnumerable<UIElement> elements) => SetVisibility(elements, Visibility.Visible);

        private static Action<UIElement, Visibility> setVisibility = (element, visibility) => element.Visibility = visibility;
        private static void SetVisibility(IEnumerable<UIElement> elements, Visibility visibility) => elements.ForEach(setVisibility.Apply(visibility));
        private static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var e in source) action(e);
        }
    }
}
