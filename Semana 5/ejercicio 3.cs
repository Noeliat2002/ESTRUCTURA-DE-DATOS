using System;
using System.Collections.Generic;

class Curso
{
    public List<string> Asignaturas { get; set; }
    public Dictionary<string, double> Notas { get; set; }

    public Curso()
    {
        Asignaturas = new List<string> { "Matemáticas", "Física", "Química", "Historia", "Lengua" };
        Notas = new Dictionary<string, double>();
    }

    public void PedirNotas()
    {
        foreach (var asignatura in Asignaturas)
        {
            Console.Write($"Nota en {asignatura}: ");
            double nota;
            while (!double.TryParse(Console.ReadLine(), out nota))
            {
                Console.WriteLine("Introduce un número válido.");
                Console.Write($"Nota en {asignatura}: ");
            }
            Notas[asignatura] = nota;
        }
    }

    public void MostrarNotas()
    {
        foreach (var asignatura in Asignaturas)
        {
            Console.WriteLine($"En {asignatura} has sacado {Notas[asignatura]}");
        }
    }
}

class Program
{
    static void Main()
    {
        Curso curso = new Curso();
        curso.PedirNotas();
        curso.MostrarNotas();
    }
}
