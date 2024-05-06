static class Tiquetera {
    const double TIPO1_VALOR = 45000;
    const double TIPO2_VALOR = 60000;
    const double TIPO3_VALOR = 30000;
    const double TIPO4_VALOR = 100000;
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
    public static double CalcularValorEntrada()
    {
        if (tipoEntrada == TIPO1_OP)
            valor = TIPO1_VALOR;
        else if (tipoEntrada == TIPO2_OP)
            valor = TIPO2_VALOR;
        else if (tipoEntrada == TIPO3_OP)
            valor = TIPO3_VALOR;
        else
            valor = TIPO4_VALOR;
        return valor * cantidadEntrada;
    }
    // No se puede devolver true o false usando int.
    public static bool CambiarEntrada(int id, int tipo, int cantidad, )
    {
        double viejoImporte;
        double nuevoImporte;
        bool exitoso = false;
        
        if (DicClientes.ContainsKey(id))
        {
            DicClientes[id].TipoEntrada = tipo;
            DicClientes[id].Cantidad = cantidad;
            exitoso = true;
        }
        
        return exitoso;
    }
}