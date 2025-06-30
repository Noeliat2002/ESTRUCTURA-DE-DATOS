using System;
using System.Collections.Generic;

class Curso
{
    public List<string> Asignaturas { get; set; }

    public Curso()
    {
        Asignaturas = new List<string> { "Matemáticas", "Física", "Química", "Historia", "Lengua" };
    }

    public void MostrarAsignaturasEstudio()
    {
        foreach (var asignatura in Asignaturas)
        {
            Console.WriteLine($"Yo estudio {asignatura}");
        }
    }
}

class Program
{
    static void Main()
    {
        Curso curso = new Curso();
        curso.MostrarAsignaturasEstudio();
    }
}
