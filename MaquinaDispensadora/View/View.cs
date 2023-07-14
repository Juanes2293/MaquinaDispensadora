using Control;
using Model;

namespace View
{
    public class View
    {
        static void Main(string[] args)
        {
            Controller controller = Controller.GetInstance();

            Console.WriteLine("Bienvenido a la maquina expendora");
            string input_cliente = "";
            do
            {
                Console.WriteLine("Ingrese si es cliente o proveedor: [C] o [P]");

                input_cliente = Console.ReadLine();

                if (input_cliente == "C" || input_cliente == "P")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ingrese un valor correcto");
                }

            } while (true);

            Console.WriteLine("Estos son nuestros productos:");

            Action<string> print = (texto) => Console.WriteLine(texto);

            foreach (Consumable producto in controller.Product_List)
            {
                Console.WriteLine(producto.ToString());
            }
            Queue<int> billetes = new Queue<int>();

            if (input_cliente == "C")
            {
                Consumable producto_escogido = null;

                while (true)
                {
                    int total_cash = controller.enqueue_cash();

                    Console.WriteLine("El total de dinero ingresado es: " + total_cash);

                    Console.WriteLine("Seleccione uno de los productos indicando su nombre. Si desea ingresar mas billetes, escriba [B]");

                    string input_seleccion = Console.ReadLine();

                    if (input_seleccion == "B")
                    {
                        continue;
                    }

                    foreach(Consumable producto in controller.Product_List)
                    {
                        if(producto.Name == input_seleccion)
                        {
                            producto_escogido = producto;
                            break;
                        }
                    }

                    if(producto_escogido != null)
                    {
                        break;
                    }

                }

                int devuelta = 0;
                int precio_producto = producto_escogido.Price;
                int total_pagado = controller.enqueue_cash();
                if (total_pagado >= precio_producto)
                {
                    devuelta = total_pagado - precio_producto;
                }
                else
                {
                    Console.WriteLine("El monto ingresado es menor al precio del producto seleccionado");
                }
                Console.WriteLine("La devuelta es: " + devuelta);

            }
            else if (input_cliente == "P")
            {
                Console.WriteLine("Los productos existentes en este momento son: ");

                foreach(Consumable producto in controller.Product_List)
                {
                    Console.WriteLine(producto.ToString);
                }


                Console.WriteLine("Seleccione que producto desea surtir");

                string product_name = Console.ReadLine();

                Console.WriteLine("Ingrese el precio del producto:");

                string product_price_input = Console.ReadLine();

                if (int.TryParse(product_price_input, out int product_price))
                {
                    Console.WriteLine("Ingrese la cantidad a llenar:");
                    string product_quantity_input = Console.ReadLine();
                    if (int.TryParse(product_quantity_input, out int product_quantity))
                    {
                        Model.Consumable product = new Model.Consumable(product_name, product_price, product_quantity);
                        controller.AddProduct(product);
                        Console.WriteLine("Producto agregado exitosamente");

                        Console.WriteLine("Lista actualizada de productos:");

                        foreach (Model.Consumable producto in controller.Product_List)
                        {
                            Console.WriteLine(producto.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ingrese una cantidad válida");
                    }
                }
                else
                {
                    Console.WriteLine("Ingrese un precio válido");
                }


            }


        }

        static int GetTotalAmountPaid(Queue<int> billetes)
        {
            int totalAmountPaid = 0;
            while (billetes.Count > 0)
            {
                totalAmountPaid += billetes.Dequeue();
            }
            return totalAmountPaid;
        }

        static int GetCoinCount(int change, int coinValue)
        {
            int coinCount = change / coinValue;
            return coinCount;
        }
    }
}