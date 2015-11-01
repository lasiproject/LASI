using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LASI.Utilities;

namespace LASI.App.Helpers
{
    internal static class UiElementExtensions
    {
        public static void Hide(this UIElement element) => element.Visibility = Visibility.Hidden;
        public static void Show(this UIElement element) => element.Visibility = Visibility.Visible;

        public static void Hide(this IEnumerable<UIElement> elements) => elements.ToList().ForEach(Hide);
        public static void Show(this IEnumerable<UIElement> elements) => elements.ToList().ForEach(Show);
    }
}
