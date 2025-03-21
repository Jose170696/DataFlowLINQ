using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataFlowLINQ
{
    internal class Program
    {
        static List<Producto> productos = new List<Producto>
    {
        new Producto { Id = 1, Nombre = "Producto A", Precio = 100.5m, Stock = 25 },
        new Producto { Id = 2, Nombre = "Producto B", Precio = 45.0m, Stock = 10 },
        new Producto { Id = 3, Nombre = "Producto C", Precio = 60.0m, Stock = 5 }
    };

        static void Main()
        {
            // Asegurar compatibilidad con caracteres especiales
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            XmlManager xmlManager = new XmlManager();

            // 1️⃣ Guardar datos en XML
            xmlManager.GuardarXml(productos);

            // 2️⃣ Consultas LINQ
            var productosCaros = productos.Where(p => p.Precio > 50);
            var productosBajoStock = productos.Where(p => p.Stock < 10);
            var productoMasCaro = productos.OrderByDescending(p => p.Precio).FirstOrDefault();
            var promedioPrecio = productos.Average(p => p.Precio);

            Console.WriteLine("\n🔍 Productos con precio mayor a 50:");
            foreach (var p in productosCaros) Console.WriteLine(p.Nombre);

            Console.WriteLine("\n📉 Productos con stock menor a 10:");
            foreach (var p in productosBajoStock) Console.WriteLine(p.Nombre);

            Console.WriteLine($"\n💎 Producto más caro: {productoMasCaro.Nombre}, Precio: {productoMasCaro.Precio}");
            Console.WriteLine($"\n📊 Promedio de precios: {promedioPrecio}");

            // 3️⃣ Manipulación de XML
            xmlManager.MostrarProductosXML();
            xmlManager.FiltrarProductosPorPrecio();
            xmlManager.ActualizarStock(2, 15);
            xmlManager.EliminarProducto(3);
            xmlManager.AgregarProducto(new Producto { Id = 4, Nombre = "Producto D", Precio = 75.0m, Stock = 20 });

            xmlManager.MostrarProductosXML();
        }
    }
}
