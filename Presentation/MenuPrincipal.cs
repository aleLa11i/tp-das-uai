using Application.Abstractions;
using Application.Commands;
using Application.DTOs;
using Microsoft.Extensions.Logging;

namespace Presentation
{
    public class MenuPrincipal(ICreatePintorHandler createPintorHandler, IUpdatePintorHandler updatePintorHandler, IGetPintoresHandler getPintoresQuery, IGetPintorByIdHandler getPintorByIdQuery, IDeletePintorHandler deletePintorHandler, ILogger<MenuPrincipal> logger)
    {
        public async Task EjecutarAsync()
        {
            var salir = false;

            while (!salir)
            {
                MostrarMenu();
                var opcion = System.Console.ReadLine();

                try
                {
                    switch (opcion)
                    {
                        case "1":
                            await AltaPintorAsync();
                            break;
                        case "2":
                            await ListarPintoresAsync();
                            break;
                        case "3":
                            await ModificarPintorAsync();
                            break;
                        case "4":
                            await EliminarPintorAsync();
                            break;
                        case "5":
                            salir = true;
                            break;
                        default:
                            System.Console.WriteLine("Opción inválida.\n");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error ejecutando la opción {Opcion}", opcion);
                    System.Console.WriteLine($"Ocurrió un error: {ex.Message}\n");
                }
            }
        }

        private static void MostrarMenu()
        {
            System.Console.WriteLine("===== Gestión de Pintores =====");
            System.Console.WriteLine("1. Alta de pintor");
            System.Console.WriteLine("2. Listado de pintores");
            System.Console.WriteLine("3. Modificación de pintor");
            System.Console.WriteLine("4. Baja de pintor");
            System.Console.WriteLine("5. Salir");
            System.Console.Write("Seleccione una opción: ");
        }

        private async Task AltaPintorAsync()
        {
            var nombre = LeerTexto("Nombre: ");
            var apellido = LeerTexto("Apellido: ");

            var id = await createPintorHandler.HandleAsync(new CreatePintorCommand(nombre, apellido));

            System.Console.WriteLine($"Pintor creado con Id {id}.\n");
        }

        private async Task ListarPintoresAsync()
        {
            var pintores = await getPintoresQuery.HandleAsync(new GetPintoresQuery());

            if (pintores.Count == 0)
            {
                System.Console.WriteLine("No hay pintores cargados.\n");
                return;
            }

            foreach (var p in pintores)
            {
                MostrarPintor(p);
            }

            System.Console.WriteLine();
        }

        private async Task ModificarPintorAsync()
        {
            var id = LeerEntero("Id del pintor a modificar: ");

            var existente = await getPintorByIdQuery.HandleAsync(new GetPintorByIdQuery(id));

            if (existente is null)
            {
                System.Console.WriteLine($"No existe un pintor con Id {id}.\n");
                return;
            }

            System.Console.WriteLine("Datos actuales:");
            MostrarPintor(existente);

            var nombre = LeerTexto("Nuevo nombre: ");
            var apellido = LeerTexto("Nuevo apellido: "); ;

            await updatePintorHandler.HandleAsync(new UpdatePintorCommand(id, nombre, apellido));

            System.Console.WriteLine("Pintor actualizado.\n");
        }

        private async Task EliminarPintorAsync()
        {
            var id = LeerEntero("Id del pintor a eliminar: ");

            var existente = await getPintorByIdQuery.HandleAsync(new GetPintorByIdQuery(id));
            if (existente is null)
            {
                System.Console.WriteLine($"No existe un pintor con Id {id}.\n");
                return;
            }

            await deletePintorHandler.HandleAsync(new DeletePintorCommand(id));

            System.Console.WriteLine("Pintor eliminado.\n");
        }

        private static void MostrarPintor(PintorDTO p)
        {
            System.Console.WriteLine(
                $"[{p.Id}] {p.Nombre} {p.Apellido}");
        }

        private static string LeerTexto(string etiqueta)
        {
            string? valor;
            do
            {
                System.Console.Write(etiqueta);
                valor = System.Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(valor));

            return valor.Trim();
        }

        private static int LeerEntero(string etiqueta)
        {
            int valor;
            while (true)
            {
                System.Console.Write(etiqueta);
                if (int.TryParse(System.Console.ReadLine(), out valor))
                {
                    return valor;
                }
                System.Console.WriteLine("Ingrese un número válido.");
            }
        }
    }
}
