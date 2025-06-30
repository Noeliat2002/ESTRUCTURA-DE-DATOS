using System;
using System.Collections.Generic;

class Loteria
{
    public List<int> NumerosGanadores { get; set; }

    public Loteria()
    {
        NumerosGanadores = new List<int>();
    }

    public void PedirNumeros()
    {
        Console.WriteLine("Introduce 6 números ganadores:");
        for (int i = 0; i < 6; i++)
        {
            int num;
            Console.Write($"Número {i + 1}: ");
            while (!int.TryParse(Console.ReadLine(), out num))
            {
                Console.WriteLine("Introduce un número válido.");
                Console.Write($"Número {i + 1}: ");
            }
            NumerosGanadores.Add(num);
        }
    }

    public void MostrarNumerosOrdenados()
    {
        NumerosGanadores.Sort();
        Console.WriteLine("Números ganadores ordenados:");
        foreach (var num in NumerosGanadores)
        {
            Console.Write(num + " ");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        Loteria loteria = new Loteria();
        loteria.PedirNumeros();
        loteria.MostrarNumerosOrdenados();
    }
}
