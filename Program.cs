const int OP_NUEVO = 1;
const int OP_ESTADISTICAS = 2;
const int OP_BUSCAR = 3;
const int OP_CAMBIAR = 4;
const int OP_SALIR = 5;
const int OP_MIN = OP_NUEVO, OP_MAX = OP_SALIR;
const int TIPO1_OP = 1, TIPO2_OP = 2, TIPO3_OP = 3;
const int TIPO_DESDE = 1, TIPO_HASTA = 4;
const double TIPO1_VALOR = 45000;
const double TIPO2_VALOR = 60000;
const double TIPO3_VALOR = 30000;
const double TIPO4_VALOR = 100000;
int opcion, dni, tipoEntrada, cantidadEntrada;
double importe, valor;
string apellido, nombre;
Cliente? clienteEncontrado;
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
static DateTime IngresarFecha(string mensaje)
{
    DateTime ingreso;
    bool esEntero = DateTime.TryParse(IngresarCadena(mensaje), out ingreso);

    while (!esEntero)
        esEntero = DateTime.TryParse(IngresarCadena("Error: ingrese una fecha válida.\n" + mensaje), out ingreso);

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
            fechaInscripcion = DateTime.Now;
            tipoEntrada = IngresarEnteroEntre("Tipo de entrada: ", TIPO_DESDE, TIPO_HASTA);
            cantidadEntrada = IngresarEnteroDesde("Cantidad de entradas: ", 1);
            
            

            Tiquetera.AgregarCliente(new Cliente(dni, apellido, nombre, fechaInscripcion, tipoEntrada, cantidadEntrada, importe));
            break;
        
        case OP_ESTADISTICAS:
            break;
        
        case OP_BUSCAR:
            clienteEncontrado = Tiquetera.BuscarCliente(IngresarEnteroDesde("DNI: ", 1));
            while (clienteEncontrado == null)
                clienteEncontrado = Tiquetera.BuscarCliente(IngresarEnteroDesde("Error: cliente no encontrado.\nDNI: ", 1));
            break;
        
        case OP_CAMBIAR:
            break;
    }

} while (opcion != OP_SALIR);