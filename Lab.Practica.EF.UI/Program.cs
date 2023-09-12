using Lab.Practica.EF.Entities;
using Lab.Practica.EF.Logic;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab.Practica.EF.UI
{
    internal class Program
    {
        static ProvedoresLogic supplierLogic = new ProvedoresLogic();
        static TransportistasLogic shippersLogic = new TransportistasLogic();
        static bool finalizar = false;
        static void Main(string[] args)
        {
            while (!finalizar)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                GenerarMenuPrincipal();
                string eleccion = Console.ReadLine();
                Console.Clear();

                switch (eleccion)
                {
                    case "1":
                        GestionarTransportistas();
                        break;

                    case "2":
                        GestionarProveedores();
                        break;

                    case "3":
                        finalizar = true;
                        break;

                    default:
                        Console.WriteLine("Error, ingrese una opcion valida");
                        break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Aplicación finalizada.");
        }
        static void GestionarTransportistas()
        {
            bool finalizarTransportistas = false;

            while (!finalizarTransportistas)
            {

                GenerarMenuTransportistas();
                int opcionTransportistas;

                bool entradaValida = false;
                do
                {
                    string input = Console.ReadLine();
                    entradaValida = int.TryParse(input, out opcionTransportistas);

                    if (!entradaValida)
                    {
                        Console.WriteLine("Por favor, ingrese una opcion válida.");
                        GenerarMenuTransportistas();
                    }
                } while (!entradaValida);
                try
                {
                    switch (opcionTransportistas)
                    {
                        case 1:
                            Console.Write("\nIngrese nombre de la compañía: ");
                            string companyName = Console.ReadLine();

                            string phone;
                            bool telefonoValido;

                            do
                            {
                                telefonoValido = true;

                                Console.Write("Ingrese el teléfono de la compañía (10 dígitos, por ejemplo, 5552229800): ");
                                phone = Console.ReadLine();

                                if (phone.Length == 10 && phone.All(char.IsDigit))
                                {

                                    string formattedPhone = string.Format("({0}) {1}-{2}", phone.Substring(0, 3), phone.Substring(3, 3), phone.Substring(6));
                                    Console.WriteLine("Teléfono formateado: " + formattedPhone);


                                    var nuevoShiper = new Shippers()
                                    {
                                        CompanyName = companyName,
                                        Phone = formattedPhone
                                    };
                                    shippersLogic.Add(nuevoShiper);
                                    Console.WriteLine("El transportista fue adherido correctamente.");
                                }
                                else
                                {
                                    Console.WriteLine("Teléfono inválido. Debe contener 10 dígitos numéricos.");
                                    telefonoValido = false;
                                }
                            } while (!telefonoValido);

                            break;




                        case 2:
                            Console.Write("\nFavor de ingresar el ID del transportista a eliminar: ");
                            int idTransportista = int.Parse(Console.ReadLine());
                            var transportistaAEliminar = shippersLogic.Encontrar(idTransportista);

                            if (transportistaAEliminar != null)
                            {
                                Console.WriteLine($"ID: {transportistaAEliminar.ShipperID} | Nombre: {transportistaAEliminar.CompanyName}");
                                Console.Write("¿Está seguro de que desea eliminar este transportista? (S/N): ");
                                string confirmacion = Console.ReadLine();

                                if (confirmacion.ToLower() == "s")
                                {
                                    shippersLogic.Delete(idTransportista);
                                    Console.WriteLine("El transportista se ha eliminado correctamente.");
                                }
                                else
                                {
                                    Console.WriteLine("Eliminación cancelada.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No se encontró ningún transportista con el ID proporcionado.");
                            }
                            break;

                        case 3:
                            Console.Write("\nIngrese el ID del transportista a modificar: ");
                            int idTransportistaModificar = int.Parse(Console.ReadLine());
                            var transportistaAModificar = shippersLogic.Encontrar(idTransportistaModificar);

                            if (transportistaAModificar != null)
                            {
                                Console.WriteLine($"Información actual del transportista:");
                                Console.WriteLine($"ID: {transportistaAModificar.ShipperID} | Nombre: {transportistaAModificar.CompanyName} | Teléfono: {transportistaAModificar.Phone}");

                                Console.WriteLine("\nIngrese los nuevos datos del transportista:");

                                Console.Write("Nombre de la compañía: ");
                                string nuevoNombre = Console.ReadLine();

                                string nuevoTelefono;

                                do
                                {
                                    telefonoValido = true;

                                    Console.Write("Teléfono (10 dígitos, por ejemplo, 5552229800): ");
                                    nuevoTelefono = Console.ReadLine();

                                    if (nuevoTelefono.Length == 10 && nuevoTelefono.All(char.IsDigit))
                                    {

                                        string formattedPhone = string.Format("({0}) {1}-{2}", nuevoTelefono.Substring(0, 3), nuevoTelefono.Substring(3, 3), nuevoTelefono.Substring(6));
                                        Console.WriteLine("Teléfono formateado: " + formattedPhone);


                                        transportistaAModificar.CompanyName = nuevoNombre;
                                        transportistaAModificar.Phone = formattedPhone;

                                        shippersLogic.Update(transportistaAModificar);
                                        Console.WriteLine("El transportista se ha actualizado correctamente.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Teléfono inválido. Debe contener 10 dígitos numéricos.");
                                        telefonoValido = false;
                                    }
                                } while (!telefonoValido);
                            }
                            else
                            {
                                Console.WriteLine("No se encontró ningún transportista con el ID proporcionado.");
                            }

                            Console.ReadKey();
                            Console.Clear();
                            break;


                        case 4:
                            ListarTransportistas(shippersLogic);
                            break;

                        case 5:
                            Console.Write("\nFavor de ingresar el id del transportista a encontrar: ");
                            var ship = shippersLogic.Encontrar(int.Parse(Console.ReadLine()));
                            Console.WriteLine($"ID: {ship.ShipperID} | Nombre: {ship.CompanyName} | Teléfono: {ship.Phone}");
                            break;

                        case 6:
                            finalizarTransportistas = true;
                            break;

                        case 7:
                            finalizarTransportistas = true;
                            finalizar = true;
                            break;

                        default:
                            Console.WriteLine("Error, ingrese nuevamente el número");
                            break;
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Se detectó una excepción => {0} : {1}", ex.Message, ex.GetType());
                }
            }
        }
        static void GestionarProveedores()
        {
            bool finalizarProveedores = false;

            while (!finalizarProveedores)
            {
                GenerarMenuProveedores();
                int opcionSuppliers;

                bool entradaValida = false;
                do
                {
                    string input = Console.ReadLine();
                    entradaValida = int.TryParse(input, out opcionSuppliers);

                    if (!entradaValida)
                    {
                        Console.WriteLine("Por favor, ingrese una opcion válida.");
                        GenerarMenuProveedores();
                    }
                } while (!entradaValida);
                try
                {
                    switch (opcionSuppliers)
                    {
                        case 1:
                            Console.Write("\nIngrese nombre de la compañía: ");
                            string companyName = Console.ReadLine();
                            Console.Write("\nIngrese la dirección de la compañía: ");
                            string address = Console.ReadLine();

                            string country;

                            bool countryValido;

                            do
                            {
                                countryValido = true;

                                Console.Write("Ingrese el país de la compañía (solo letras): ");
                                country = Console.ReadLine();

                                if (Regex.IsMatch(country, @"^[a-zA-Z]+$"))
                                {

                                }
                                else
                                {
                                    Console.WriteLine("País inválido. Debe contener solo letras.");
                                    countryValido = false;
                                }
                            } while (!countryValido);

                            var nuevoSupplier = new Suppliers()
                            {
                                CompanyName = companyName,
                                Address = address,
                                Country = country
                            };
                            supplierLogic.Add(nuevoSupplier);
                            Console.WriteLine("El proveedor fue adherido correctamente.");
                            break;


                        case 2:
                            Console.Write("\nFavor de ingresar el ID del proveedor a eliminar: ");
                            int idProveedor = int.Parse(Console.ReadLine());
                            var proveedorAEliminar = supplierLogic.Encontrar(idProveedor);

                            if (proveedorAEliminar != null)
                            {
                                Console.WriteLine($"ID: {proveedorAEliminar.SupplierID} | Nombre de compañía: {proveedorAEliminar.CompanyName}");
                                Console.Write("¿Está seguro de que desea eliminar este proveedor? (S/N): ");
                                string confirmacion = Console.ReadLine();

                                if (confirmacion.ToLower() == "s")
                                {
                                    supplierLogic.Delete(idProveedor);
                                    Console.WriteLine("El proveedor se ha eliminado correctamente.");
                                }
                                else
                                {
                                    Console.WriteLine("Eliminación cancelada.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No se encontró ningún proveedor con el ID proporcionado.");
                            }
                            break;

                        case 3:
                            Console.Write("\nIngrese el ID del proveedor a modificar: ");
                            int idProveedorModificar = int.Parse(Console.ReadLine());
                            var proveedorAModificar = supplierLogic.Encontrar(idProveedorModificar);

                            if (proveedorAModificar != null)
                            {
                                Console.WriteLine($"Información actual del proveedor:");
                                Console.WriteLine($"ID: {proveedorAModificar.SupplierID} | Nombre de compañía: {proveedorAModificar.CompanyName} | País: {proveedorAModificar.Country} | Dirección: {proveedorAModificar.Address}");

                                Console.WriteLine("\nIngrese los nuevos datos del proveedor:");

                                Console.Write("Nuevo nombre de la compañía: ");
                                string nuevoNombre = Console.ReadLine();

                                Console.Write("Nueva dirección: ");
                                string nuevaDireccion = Console.ReadLine();

                                string nuevoPais; // Declaración sin inicialización
                                bool paisValido;

                                do
                                {
                                    paisValido = true; // Suponemos que el país es válido a menos que se demuestre lo contrario

                                    Console.Write("Nuevo país: ");
                                    nuevoPais = Console.ReadLine();

                                    if (nuevoPais.All(char.IsLetter))
                                    {
                                        // El país es válido, lo asignamos al proveedor
                                        proveedorAModificar.CompanyName = nuevoNombre;
                                        proveedorAModificar.Address = nuevaDireccion;
                                        proveedorAModificar.Country = nuevoPais;

                                        supplierLogic.Update(proveedorAModificar);
                                        Console.WriteLine("El proveedor se ha actualizado correctamente.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("País inválido. Debe contener solo letras.");
                                        paisValido = false; // Establecemos la bandera en falso para repetir el bucle
                                    }
                                } while (!paisValido);
                            }
                            else
                            {
                                Console.WriteLine("No se encontró ningún proveedor con el ID proporcionado.");
                            }

                            Console.ReadKey();
                            Console.Clear();
                            break;


                        case 4:
                            ListarSuppliers(supplierLogic);
                            break;

                        case 5:
                            Console.Write("\nFavor de ingresar el id del proveedor a encontrar: ");
                            var supplier = supplierLogic.Encontrar(int.Parse(Console.ReadLine()));
                            Console.WriteLine($"ID: {supplier.SupplierID} | Nombre de compañía: {supplier.CompanyName} | País: {supplier.Country} | Dirección: {supplier.Address}");
                            break;

                        case 6:
                            finalizarProveedores = true;
                            break;

                        case 7:
                            finalizarProveedores = true;
                            finalizar = true;
                            break;

                        default:
                            Console.WriteLine("Error, ingrese nuevamente el número");
                            break;
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Se detectó una excepción => {0} : {1}", ex.Message, ex.GetType() + ex.InnerException.Message);
                }
            }
        }
        static void ListarSuppliers(ProvedoresLogic supplierLogic)
        {
            foreach (var supplier in supplierLogic.GetAll())
            {
                Console.WriteLine($"ID: {supplier.SupplierID} | Nombre de compañía: {supplier.CompanyName} | País: {supplier.Country} | Dirección de compañía: {supplier.Address}");
            }
        }
        static void ListarTransportistas(TransportistasLogic shippersLogic)
        {
            foreach (var shipper in shippersLogic.GetAll())
            {
                Console.WriteLine($"ID: {shipper.ShipperID} | Nombre: {shipper.CompanyName} | Teléfono: {shipper.Phone}");
            }
        }
        static void GenerarMenuPrincipal()
        {
            Console.WriteLine("Bienvenido!!!, seleccione una opción o 3 para finalizar el programa: ");
            Console.WriteLine("1-Transportistas.");
            Console.WriteLine("2-Proveedores.");
            Console.WriteLine("3-Finalizar Programa.");
        }
        static void GenerarMenuTransportistas()
        {
            Console.WriteLine("Usted ha seleccionado Transportistas, favor de elegir la acción a realizar: ");
            Console.WriteLine("1-Alta.");
            Console.WriteLine("2-Baja");
            Console.WriteLine("3-Modificación");
            Console.WriteLine("4-Listar");
            Console.WriteLine("5-Buscar elemento con id");
            Console.WriteLine("6-Volver al menú de opciones.");
            Console.WriteLine("7-Finalizar Programa");
        }
        static void GenerarMenuProveedores()
        {
                Console.WriteLine("Usted ha seleccionado Proveedores, favor de elegir la acción a realizar: ");
                Console.WriteLine("1-Alta.");
                Console.WriteLine("2-Baja");
                Console.WriteLine("3-Modificación");
                Console.WriteLine("4-Listar");
                Console.WriteLine("5-Buscar elemento con id");
                Console.WriteLine("6-Volver al menú de opciones.");
                Console.WriteLine("7-Finalizar Programa");
        }
    }
}
