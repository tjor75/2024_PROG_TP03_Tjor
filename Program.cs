const int OP_NUEVO = 1;
const int OP_ESTADISTICAS = 2;
const int OP_BUSCAR = 3;
const int OP_CAMBIAR = 4;
const int OP_SALIR = 5;
const int OP_MIN = OP_NUEVO, OP_MAX = OP_SALIR;
const int TIPO_DESDE = 1, TIPO_HASTA = 4;
const char MONEDA = '$';
bool cambioExitoso;
int opcion, dni, tipoEntrada, cantidadEntrada, id;
double importe;
string apellido, nombre;
Cliente? cliente;
DateTime fechaInscripcion;


static string IngresarCadena(string mensaje)
{
    Console.Write(mensaje);
    return Console.ReadLine();
}
static string IngresarCadenaNoVacia(string mensaje)
{
    string ingreso = IngresarCadena(mensaje);
    while (ingreso.Length == 0)
        ingreso = IngresarCadena("Error: el ingreso no puede quedar vacío.\n" + mensaje);
    return ingreso;
}
static int IngresarEntero(string mensaje)
{
    int ingreso;
    bool esEntero = int.TryParse(IngresarCadena(mensaje), out ingreso);

    while (!esEntero)
        esEntero = int.TryParse(IngresarCadena("Error: ingrese un entero.\n" + mensaje), out ingreso);

    return ingreso;
}
static int IngresarEnteroEntre(string mensaje, int min, int max)
{
    int ingreso = IngresarEntero(mensaje);
    while (ingreso < min || ingreso > max)
        ingreso = IngresarEntero("Error: entero fuera de rango.\n" + mensaje);
    return ingreso;
}
static int IngresarEnteroDesde(string mensaje, int min)
{
    int ingreso = IngresarEntero(mensaje);
    while (ingreso < min)
        ingreso = IngresarEntero("Error: entero menor a " + min + ".\n" + mensaje);
    return ingreso;
}


do
{
    Console.WriteLine("1. Nueva Inscripción\n" +
        "2. Obtener Estadísticas del Evento\n" +
        "3. Buscar Cliente\n" +
        "4. Cambiar entrada de un Cliente\n" +
        "5. Salir");
    opcion = IngresarEnteroEntre("> ", OP_MIN, OP_MAX);

    switch (opcion)
    {
        case OP_NUEVO:
            dni = IngresarEnteroDesde("DNI: ", 1);
            apellido = IngresarCadenaNoVacia("Apellido: ");
            nombre = IngresarCadenaNoVacia("Nombre: ");
            tipoEntrada = IngresarEnteroEntre("Tipo de entrada: ", TIPO_DESDE, TIPO_HASTA);
            cantidadEntrada = IngresarEnteroDesde("Cantidad de entradas: ", 1);
            importe = Tiquetera.CalcularImporte(tipoEntrada, cantidadEntrada);
            fechaInscripcion = DateTime.Now;

            Tiquetera.AgregarCliente(new Cliente(dni, apellido, nombre, fechaInscripcion, tipoEntrada, cantidadEntrada, importe));
            break;
        
        case OP_ESTADISTICAS:
            Tiquetera.EstadisticasTiquetera();
            break;
        
        case OP_BUSCAR:
            id = IngresarEnteroDesde("ID: ", 1);
            cliente = Tiquetera.BuscarCliente(id);
            while (cliente == null)
            {
                id = IngresarEnteroDesde("Error: cliente no encontrado.\nID: ", 1);
                cliente = Tiquetera.BuscarCliente(id);
            }

            Console.WriteLine("DNI: " + cliente.DNI);
            Console.WriteLine("Apellido: " + cliente.Apellido);
            Console.WriteLine("Nombre: " + cliente.Nombre);
            Console.WriteLine("Fecha de inscripción: " + cliente.FechaInscripcion.ToLongDateString());
            Console.WriteLine("Tipo de entrada: " + cliente.TipoEntrada);
            Console.WriteLine("Cantidad: " + cliente.Cantidad);
            Console.WriteLine("Importe a abonar: " + MONEDA + cliente.Importe);
            break;
        
        case OP_CAMBIAR:
            id = IngresarEnteroDesde("ID: ", 1);
            cliente = Tiquetera.BuscarCliente(id);
            while (cliente == null)
            {
                id = IngresarEnteroDesde("Error: cliente no encontrado.\nID: ", 1);
                cliente = Tiquetera.BuscarCliente(id);
            }

            Console.WriteLine("Tipo original: " + cliente.TipoEntrada);
            Console.WriteLine("Cantidad original: " + cliente.Cantidad);
            Console.WriteLine("Importe original: " + MONEDA + cliente.Importe);

            tipoEntrada = IngresarEnteroEntre("Nuevo tipo de entrada: ", TIPO_DESDE, TIPO_HASTA);
            cantidadEntrada = IngresarEnteroDesde("Nueva cantidad: ", 1);
            cambioExitoso = Tiquetera.CambiarEntrada(id, tipoEntrada, cantidadEntrada);
            while (!cambioExitoso)
            {
                Console.WriteLine("Error: el importe resultante debe ser mayor a lo anteriormente comprado.");
                tipoEntrada = IngresarEnteroEntre("Nuevo tipo de entrada: ", TIPO_DESDE, TIPO_HASTA);
                cantidadEntrada = IngresarEnteroDesde("Nueva cantidad: ", 1);
                cambioExitoso = Tiquetera.CambiarEntrada(id, tipoEntrada, cantidadEntrada);
            }

            cliente.FechaInscripcion = DateTime.Now;
            break;
    }

} while (opcion != OP_SALIR);