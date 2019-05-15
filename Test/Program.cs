using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //SearchAndDelete();
            //AgregarCliente();
            //Orders();
            AddProductOrders();
            //AgregarProducto();
            //RetrieveAndUpdate();
            //List();
            Console.WriteLine("Presiones<enter> para finalizar");
            Console.ReadLine();
        }
        static void AgregarCliente()
        {
            Customer c = new Customer()
            {
                Id=5,
                FirstName = "Fulani",
                LastName = "Algui",
                StreetAddress = "Lan",
                City = "San Miguel",
                Phone = "9456456",
                Email = "fulani@gmail.com"
               

            };
            
            //Order Cereal = new Order
            //{
            //    OrderFullFilled = true,
                
                
            //};
            //c.Orders.Add(Cereal);

            using (var r = RepositoryFactory.CreateRepository())//Todo lo que esta en el using se libera los recurso, se invoca el dispos
            {
                r.Create(c);
            }
            Console.WriteLine
                    ($"Cliente: {c.FirstName}" + $" Apellido: {c.LastName}" /*+ $"Producto:{Cereal.OrderFullFilled}"*/);
            Console.WriteLine("cliente creado exitosamente");
        }

        static void Orders()
        {
            Order O = new Order
            {
                Id = 2,
                OrderPlaced = TimeSpan.Parse("10:45"),
                OrderFullFilled = true,
                Customerid = 4
                
                
            };
            using (var r = RepositoryFactory.CreateRepository())
            {
                r.Create(O);
            }
            Console.WriteLine($"Producto:{O.Id}");
            Console.WriteLine("Orden creada exitosamente");
        }

        static void AddProductOrders()
        {
            ProductOrder PO = new ProductOrder
            {
                Id = 2,
                Quantity = 34,
                Productid = 1,
                Orderid = 2


            };
            using (var r = RepositoryFactory.CreateRepository())
            {
                r.Create(PO);
            }
            Console.WriteLine($"Producto:{PO.Id}");
            Console.WriteLine("orden de producto creado exitosamente");
        }

        static void AgregarProducto()
        {
            Product p = new Product()
            {
                Id = 1,
                Name = "Mango",
                Price = 23

            };

         
            using (var r = RepositoryFactory.CreateRepository())//Todo lo que esta en el using se libera los recurso, se invoca el dispos
            {
                r.Create(p);
            }
            Console.WriteLine
                    ($"Producto: {p.Name}" + $" Precio: {p.Price}" /*+ $"Producto:{Cereal.OrderFullFilled}"*/);
            Console.WriteLine("Product creado exitosamente");
        }

        //Buscar y Modificar
        static void RetrieveAndUpdate()
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                //Buscar el ultimo producto agregado
                Customer P = r.Retrieve<Customer>(p => p.Id== 0);
                if (P != null)
                {
                    Console.Write(P.City + "es ahora: ");
                    P.City = "San Miguel";
                    Console.WriteLine(P.City);
                    r.Update(P);
                    Console.WriteLine("Sea modificado correctamente");

                }
            }
        }

        static void List()
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var customer = r.Filter<Customer>(c => c.City.Contains("China"))
                .OrderByDescending(p => p.City);
                //var products = r.Filter<Product>(p => p.ProductName.Contains("ae"))
                //.OrderByDescending(p => p.ProductName);
                //var order = r.Filter<Order>(p => true);
                //foreach (var P in products)
                //{
                //    Console.WriteLine($"{P.ProductName}");
                //}
                //Inne Join
                var ListProduct = from prod in customer
                                  join cate in customer on prod.Id equals cate.Id
                                  select new
                                  {
                                      productsname = prod.FirstName,
                                      categoriesname = cate.City
                                  };
                foreach (var P in ListProduct)
                {
                    Console.WriteLine($"{P.productsname} , {P.categoriesname}");
                }
            }

        }

        //static void List()
        //{
        //    using (var r = RepositoryFactory.CreateRepository())
        //    {
        //        var customer = r.Filter<Customer>(c => true);
        //        //var products = r.Filter<Product>(p => p.ProductName.Contains("ae"))
        //        //.OrderByDescending(p => p.ProductName);
        //        var order = r.Filter<Order>(p => true);
        //        //foreach (var P in products)
        //        //{
        //        //    Console.WriteLine($"{P.ProductName}");
        //        //}
        //        //Inne Join
        //        var ListProduct = from prod in order
        //                          join cate in customer on prod.Id equals cate.Id
        //                          select new
        //                          {
        //                              productsname = prod.OrderFullFilled,
        //                              categoriesname = cate.FirstName
        //                          };
        //        foreach (var P in ListProduct)
        //        {
        //            Console.WriteLine($"{P.productsname},{P.categoriesname}");
        //        }
        //    }

        //}
        //
        static void SearchAndDelete()
        {
            using (var R = RepositoryFactory.CreateRepository())
            {
                var P = R.Retrieve<Customer>(p => p.Id == 0);
                if (P != null)
                {
                    Console.WriteLine(P.Id);
                    R.Delete(P);
                    Console.WriteLine("Pro" +
                        "ducto eliminado");
                }
                else
                {
                    Console.WriteLine("Product no encontrado");
                }
            }
        }

    }
}
