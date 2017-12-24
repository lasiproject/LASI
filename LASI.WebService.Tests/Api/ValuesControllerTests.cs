using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LASI.Utilities.Validation;
using LASI.WebService.Api;
using LASI.WebService.Services;
using Microsoft.Extensions.DependencyInjection;
using NFluent;
using Xunit;

namespace LASI.WebService.Tests.Api
{
    public class ValuesControllerTests
    {
        private static IServiceProvider ConfigureServices(IEnumerable<(string, int)> values)
        {
            var services = new ServiceCollection();
            services
                .AddSingleton(values.AsEnumerable())
                .AddSingleton<ValuesController>()
                .AddSingleton<IValuesService, ValuesService>()
                .AddMvc()
                .AddMvcOptions(options => { })
                .AddControllersAsServices();
            var provider = services.BuildServiceProvider(validateScopes: true);
            return provider;
        }

        [Theory]
        [InlineData(new object[] { "Carol", 37, "Bob", 1, "Ted", 6600, "Alice", 89 })]
        [InlineData(new object[] { "Bob", 37, "Carol", 1, "Ted", 6600, "Alice", 89 })]
        [InlineData(new object[] { "Carol", 37, "Bob", 1, "Alice", 6600, "Ted", 89 })]
        [InlineData(new object[] { "Carol", 37, "Alice", 1, "Ted", 6600, "Bob", 89 })]
        public async Task GetTest(params object[] data)
        {
            var values = data
                .PairWise()
                .TakeEveryOther()
                .Select(x => ((string)x.Item1, (int)x.Item2));

            var provider = ConfigureServices(values);

            using (var controller = provider.GetService<ValuesController>())
            {
                var results = await controller.GetAsync();
                Check.That(results).HasSize(4).And.Contains(values);

                var nounPhrase = controller.Tester();
                Check.That(nounPhrase.Text).IsNotNull().And.IsNotEmpty().And.IsEqualIgnoringCase("tester");
            }
        }
    }


    static class Extensions
    {
        public static IEnumerable<T> TakeEveryOther<T>(this IEnumerable<T> source)
        {
            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                    if (!enumerator.MoveNext())
                    {
                        yield break;
                    }
                }
            }
        }
        public static IEnumerable<(T, T)> PairWise<T>(this IEnumerable<T> source)
        {
            Validate.NotNull(source, nameof(source));
            if (source.Count() == 1)
            { throw new InvalidOperationException("If source is not empty, it must have more than 1 element."); }
            var first = source.First();
            foreach (var next in source.Skip(1))
            {
                yield return (first, next);
                first = next;
            }
        }
    }
}
