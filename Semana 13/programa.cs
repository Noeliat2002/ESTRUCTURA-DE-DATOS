
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CatalogoRevistasV2
{
    // ---------- Infra de normalización ----------
    public static class TextNormalizer
    {
        /// <summary>
        /// Normaliza: minúsculas + quita acentos/diacríticos + trim.
        /// </summary>
        public static string Normalize(string? s)
        {
            if (string.IsNullOrWhiteSpace(s)) return string.Empty;
            string lower = s.ToLowerInvariant();
            string formD = lower.Normalize(NormalizationForm.FormD);
            var filtered = formD.Where(c =>
                CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);
            return new string(filtered.ToArray()).Normalize(NormalizationForm.FormC).Trim();
        }
    }

    // ---------- Estrategias de búsqueda ----------
    public interface ISearchStrategy
    {
        string Name { get; }
        bool Exists(string[] haystack, string needleNormalized);
    }

    /// <summary>
    /// Búsqueda lineal recursiva sobre el arreglo original.
    /// Usa valores normalizados para comparar 1 a 1.
    /// </summary>
    public sealed class RecursiveLinearSearch : ISearchStrategy
    {
        public string Name => "Lineal Recursiva";

        public bool Exists(string[] haystack, string needleNormalized)
            => Recurse(haystack, needleNormalized, 0);

        private bool Recurse(string[] data, string needle, int index)
        {
            if (index >= data.Length) return false;
            if (TextNormalizer.Normalize(data[index]) == needle) return true;
            return Recurse(data, needle, index + 1);
        }
    }

    /// <summary>
    /// Búsqueda binaria iterativa sobre un arreglo previamente ordenado y normalizado.
    /// </summary>
    public sealed class IterativeBinarySearch : ISearchStrategy
    {
        public string Name => "Binaria Iterativa (sobre normalizados)";

        public bool Exists(string[] haystack, string needleNormalized)
        {
            // El "haystack" aquí debe venir normalizado y ordenado alfabéticamente.
            int low = 0, high = haystack.Length - 1;
            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                int cmp = string.CompareOrdinal(haystack[mid], needleNormalized);
                if (cmp == 0) return true;
                if (cmp < 0) low = mid + 1;
                else high = mid - 1;
            }
            return false;
        }
    }

    // ---------- Catálogo ----------
    public sealed class MagazineCatalog
    {
        private readonly List<string> _original;        // para mostrar y búsqueda lineal recursiva
        private readonly string[] _normalizedSorted;    // para binaria iterativa

        public MagazineCatalog(IEnumerable<string> titles)
        {
            _original = titles
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Select(t => t.Trim())
                .ToList();

            _normalizedSorted = _original
                .Select(TextNormalizer.Normalize)
                .OrderBy(t => t, StringComparer.Ordinal)
                .ToArray();
        }

        public void Print()
        {
            Console.WriteLine("\nCatálogo de Revistas:");
            int n = 1;
            foreach (var t in _original.OrderBy(x => x, StringComparer.CurrentCultureIgnoreCase))
            {
                Console.WriteLine($"  {n,2}. {t}");
                n++;
            }
            Console.WriteLine();
        }

        public bool Exists(string query, ISearchStrategy strategy)
        {
            string needle = TextNormalizer.Normalize(query);
            if (strategy is IterativeBinarySearch)
            {
                return strategy.Exists(_normalizedSorted, needle);
            }
            // Para lineal recursiva usamos la lista tal cual
            return strategy.Exists(_original.ToArray(), needle);
        }
    }

    // ---------- Menú ----------
    internal static class Menu
    {
        private static readonly ISearchStrategy LinearRecursive = new RecursiveLinearSearch();
        private static readonly ISearchStrategy BinaryIterative = new IterativeBinarySearch();

        public static void Show()
        {
            var titulos = new[]
            {
                "National Geographic",
                "Scientific American",
                "The Economist",
                "Nature",
                "Time",
                "Forbes",
                "WIRED",
                "Harvard Business Review",
                "El Malpensante",
                "Muy Interesante",
                "Rolling Stone",
                "New Scientist"
            };

            var catalog = new MagazineCatalog(titulos);

            while (true)
            {
                Console.WriteLine("==============================================");
                Console.WriteLine("  Catálogo de Revistas - Versión 2 (Strategy) ");
                Console.WriteLine("==============================================");
                Console.WriteLine("1) Ver catálogo");
                Console.WriteLine("2) Buscar (Lineal Recursiva)");
                Console.WriteLine("3) Buscar (Binaria Iterativa)");
                Console.WriteLine("0) Salir");
                Console.Write("Elige una opción: ");
                var input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        catalog.Print();
                        break;

                    case "2":
                        RunSearch(catalog, LinearRecursive);
                        break;

                    case "3":
                        RunSearch(catalog, BinaryIterative);
                        break;

                    case "0":
                        Console.WriteLine("¡Hasta luego!");
                        return;

                    default:
                        Console.WriteLine("Opción no válida.\n");
                        break;
                }
            }
        }

        private static void RunSearch(MagazineCatalog catalog, ISearchStrategy strategy)
        {
            Console.Write("Ingresa el título a buscar: ");
            string? q = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(q))
            {
                Console.WriteLine("Entrada vacía. Intenta de nuevo.\n");
                return;
            }

            bool ok = catalog.Exists(q, strategy);
            Console.WriteLine(ok ? "Encontrado" : "No encontrado");
            Console.WriteLine();
        }
    }

    // ---------- Programa ----------
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Menu.Show();
        }
    }
}

