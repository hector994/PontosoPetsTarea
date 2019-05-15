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
            //RetrieveAndUpdate();
            List();
            Console.WriteLine("Presiones<enter> para finalizar");
            Console.ReadLine();
        }
        static void AgregarCliente()
        {
            Customer c = new Customer()
            {
                Id=4,
                FirstName = "Fulanito",
                LastName = "Alguien",
                StreetAddress = "Landia",
                City = "San Miguel",
                Phone = "90909090",
                Email = "fulanito@gmail.com"
               

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

        static void AddProduct()
        {
            Order Avena = new Order
            {
                Id = 1,
                OrderPlaced = System.TimeSpan.Parse("2/2/2019"),
                OrderFullFilled = false,
                Customerid = 1
                
            };
            using (var r = RepositoryFactory.CreateRepository())
            {
                r.Create(Avena);
            }
            Console.WriteLine($"Producto:{Avena.Id}");
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
