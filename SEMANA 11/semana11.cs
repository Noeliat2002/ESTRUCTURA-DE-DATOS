using System;
using System.Collections.Generic;

class TraductorBasico
{
    static void Main(string[] args)
    {
        Dictionary<string, string> diccionario = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"time", "tiempo"},
            {"person", "persona"},
            {"year", "año"},
            {"way", "camino"},
            {"day", "día"},
            {"thing", "cosa"},
            {"man", "hombre"},
            {"world", "mundo"},
            {"life", "vida"},
            {"eye", "ojo"}
        };

        int opcion;
        do
        {
            Console.WriteLine("\n==================== MENÚ ====================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabras al diccionario");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                opcion = -1;
            }

            switch (opcion)
            {
                case 1:
                    Console.Write("\nIngrese una frase: ");
                    string frase = Console.ReadLine();
                    string[] palabras = frase.Split(' ');
                    for (int i = 0; i < palabras.Length; i++)
                    {
                        string limpia = palabras[i].Trim(new char[] { '.', ',', ';', '!' , '?' });
                        string traduccion;
                        if (diccionario.TryGetValue(limpia.ToLower(), out traduccion))
                        {
                            palabras[i] = palabras[i].Replace(limpia, traduccion);
                        }
                    }
                    Console.WriteLine("Traducción: " + string.Join(" ", palabras));
                    break;

                case 2:
                    Console.Write("\nIngrese la palabra en inglés: ");
                    string eng = Console.ReadLine().ToLower();
                    Console.Write("Ingrese la traducción al español: ");
                    string esp = Console.ReadLine();
                    if (!diccionario.ContainsKey(eng))
                    {
                        diccionario.Add(eng, esp);
                        Console.WriteLine("Palabra agregada exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("La palabra ya existe en el diccionario.");
                    }
                    break;

                case 0:
                    Console.WriteLine("Saliendo...");
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

        } while (opcion != 0);
    }
}
