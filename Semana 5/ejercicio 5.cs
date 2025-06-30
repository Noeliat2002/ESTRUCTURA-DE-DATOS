using System;
using System.Collections.Generic;

class Numeros
{
    public List<int> NumerosLista { get; set; }

    public Numeros()
    {
        NumerosLista = new List<int>();
        for (int i = 1; i <= 10; i++)
        {
            NumerosLista.Add(i);
        }
    }

    public void MostrarInvertido()
    {
        NumerosLista.Reverse();
        Console.WriteLine(string.Join(", ", NumerosLista));
    }
}

class Program
{
    static void Main()
    {
        Numeros numeros = new Numeros();
        numeros.MostrarInvertido();
    }
}
