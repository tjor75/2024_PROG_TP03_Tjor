static class Tiquetera {
    public static double[] tiposValores = { 45000, 60000, 30000, 100000 };
    private static Dictionary<int, Cliente> DicClientes = new Dictionary<int, Cliente>();
    private static int UltimoIDEntrada = 0;
    
    public static int DevolverUltimoID()
    {
        return UltimoIDEntrada;
    }
    public static int AgregarCliente(Cliente nuevoCliente)
    {
        UltimoIDEntrada++;
        DicClientes.Add(UltimoIDEntrada, nuevoCliente);
        return UltimoIDEntrada;
    }
    public static Cliente? BuscarCliente(int id)
    {
        Cliente? resultado = null;
        if (DicClientes.ContainsKey(id))
            resultado = DicClientes[id];
        return resultado;
    }
    public static double CalcularImporte(int tipo, int cantidad)
    {
        return tiposValores[tipo - 1] * cantidad;
    }

    public static bool CambiarEntrada(int id, int tipo, int cantidad)
    {
        bool exitoso = false;
        
        if (DicClientes.ContainsKey(id) && CalcularImporte(tipo, cantidad) > DicClientes[id].Importe)
        {
            DicClientes[id].TipoEntrada = tipo;
            DicClientes[id].Cantidad = cantidad;
            DicClientes[id].Importe = CalcularImporte(tipo, cantidad);
            exitoso = true;
        }
        
        return exitoso;
    }
    public static void EstadisticasTiquetera()
    {
        int cantClientes = DicClientes.Count();
        int cantEntradas;
        double recaudacionTotal = 0;
        int[] cantClientesTipos = new int[tiposValores.Length];
        int[] cantEntradasTipos = new int[tiposValores.Length];
        double[] recaudacionTipos = new double[tiposValores.Length];
        double[] porcentajeEntradasTipos = new double[tiposValores.Length];

        if (cantClientes > 0) {
            cantEntradas = ObtenerTotalEntradas();

            for (int i = 0; i < tiposValores.Length; i++)
            {
                cantClientesTipos[i] = ObtenerCantidadClientesTipo(i + 1);
                cantEntradasTipos[i] = ObtenerCantidadEntradasTipo(i + 1);
                recaudacionTipos[i] = ObtenerRecaudacionTipo(i + 1);
                recaudacionTotal += recaudacionTipos[i];
            }
            for (int i = 0; i < tiposValores.Length; i++)
                porcentajeEntradasTipos[i] = CalcularPorcentaje(cantEntradasTipos[i], cantEntradas);


            Console.WriteLine("Cantidad de clientes: " + cantClientes);
            for (int i = 0; i < tiposValores.Length; i++)
            {
                Console.WriteLine("Tipo de entrada " + (i + 1) + " ===");
                Console.WriteLine("- Cantidad de clientes: " + cantClientesTipos[i]);
                Console.WriteLine("- Porcentaje de entradas: " + porcentajeEntradasTipos[i] + "(" + cantEntradasTipos[i] + "%)");
                Console.WriteLine("- Recaudación: " + recaudacionTipos[i]);
            }
            Console.WriteLine("Cantidad de clientes: " + cantClientes);
            Console.WriteLine("Recaudación total: " + recaudacionTotal);
        }
        else
            Console.WriteLine("Error: Aún no se anotó nadie.");


    }

    private static int ObtenerTotalEntradas()
    {
        int cantidad = 0;
        foreach (int key in DicClientes.Keys)
        {
            cantidad += DicClientes[key].Cantidad;
        }
        return cantidad;
    }
    private static int ObtenerCantidadClientesTipo(int tipo)
    {
        int cantidad = 0;
        foreach (int key in DicClientes.Keys)
        {
            if (DicClientes[key].TipoEntrada == tipo)
                cantidad++;
        }
        return cantidad;
    }
    private static int ObtenerCantidadEntradasTipo(int tipo)
    {
        int cantidad = 0;
        foreach (int key in DicClientes.Keys)
        {
            if (DicClientes[key].TipoEntrada == tipo)
                cantidad += DicClientes[key].Cantidad;
        }
        return cantidad;
    }
    private static double ObtenerRecaudacionTipo(int tipo)
    {
        double recaudacion = 0;
        foreach (int key in DicClientes.Keys)
        {
            if (DicClientes[key].TipoEntrada == tipo)
                recaudacion += DicClientes[key].Importe;
        }
        return recaudacion;
    }
    private static double CalcularPorcentaje(double cantidad, double total)
    {
        return cantidad * 100 / total;
    }
}