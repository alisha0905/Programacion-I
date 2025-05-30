﻿using System;
// Clase base abstracta para llamadas
abstract class Llamada
{
    public string Origen { get; set;}
    public string Destino { get; set;}
    public int Duracion { get; set;} // en segundos

    protected Llamada(string origen, string destino, int duracion)
    {
        Origen = origen;
        Destino = destino;
        Duracion = duracion;
    }

    public abstract double CalcularCosto();

    public override string ToString() =>
        $"Origen: {Origen}, Destino: {Destino}, Duración: {Duracion} seg, Costo: {CalcularCosto()} céntimos";
}

// Llamada local con tarifa fija
class LlamadaLocal : Llamada
{
    private const double TARIFA = 15.0;

    public LlamadaLocal(string origen, string destino, int duracion)
        : base(origen, destino, duracion) { }

    public override double CalcularCosto() => Duracion * TARIFA;
}

// Llamada provincial con tarifa según franja horaria
class LlamadaProvincial : Llamada
{
    private readonly double tarifa;

    public LlamadaProvincial(string origen, string destino, int duracion, int franjaHoraria)
        : base(origen, destino, duracion)
    {
        tarifa = franjaHoraria switch
        {
            1 => 20.0,
            2 => 25.0,
            3 => 30.0,
            _ => throw new ArgumentException("Franja horaria inválida")
        };
    }

    public override double CalcularCosto() => Duracion * tarifa;
}

// Clase que gestiona las llamadas
class Centralita
{
    private readonly List<Llamada> llamadas = new();

    public void RegistrarLlamada(Llamada llamada) => llamadas.Add(llamada);

    public void MostrarLlamadas() => llamadas.ForEach(llamada => Console.WriteLine(llamada));

    public void GenerarInforme()
    {
        double totalFacturado = 0;
        llamadas.ForEach(llamada => totalFacturado += llamada.CalcularCosto());

        Console.WriteLine($"\nTotal de llamadas: {llamadas.Count}");
        Console.WriteLine($"Facturación total: {totalFacturado} céntimos");
    }
}