using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataFlowLINQ
{
    internal class XmlManager
    {
        private string xmlPath = "productos.xml";

        public void GuardarXml(List<Producto> productos)
        {
            XDocument xmlDoc = new XDocument(new XElement("Productos",
                productos.Select(p => new XElement("Producto",
                    new XElement("Id", p.Id),
                    new XElement("Nombre", p.Nombre),
                    new XElement("Precio", p.Precio),
                    new XElement("Stock", p.Stock)
                ))
            ));
            xmlDoc.Save(xmlPath);
        }

        public void MostrarProductosXML()
        {
            XDocument xmlRead = XDocument.Load(xmlPath);
            Console.WriteLine("\n📄 Contenido del archivo XML:");
            Console.WriteLine(xmlRead);
        }

        public void FiltrarProductosPorPrecio()
        {
            XDocument xmlRead = XDocument.Load(xmlPath);
            var productosFiltrados = xmlRead.Descendants("Producto")
                .Where(p => (decimal)p.Element("Precio") > 50);

            Console.WriteLine("\n💰 Productos con precio mayor a 50 en XML:");
            foreach (var p in productosFiltrados)
                Console.WriteLine(p.Element("Nombre")?.Value);
        }

        public void ActualizarStock(int id, int nuevoStock)
        {
            XDocument xmlRead = XDocument.Load(xmlPath);
            var producto = xmlRead.Descendants("Producto").FirstOrDefault(p => (int)p.Element("Id") == id);
            if (producto != null)
            {
                producto.Element("Stock").Value = nuevoStock.ToString();
                xmlRead.Save(xmlPath);
                Console.WriteLine($"\n📦 Stock del producto {id} actualizado a {nuevoStock}");
            }
        }

        public void EliminarProducto(int id)
        {
            XDocument xmlRead = XDocument.Load(xmlPath);
            var producto = xmlRead.Descendants("Producto").FirstOrDefault(p => (int)p.Element("Id") == id);
            if (producto != null)
            {
                producto.Remove();
                xmlRead.Save(xmlPath);
                Console.WriteLine($"\n❌ Producto {id} eliminado del XML.");
            }
        }

        public void AgregarProducto(Producto nuevoProducto)
        {
            XDocument xmlRead = XDocument.Load(xmlPath);
            xmlRead.Root.Add(new XElement("Producto",
                new XElement("Id", nuevoProducto.Id),
                new XElement("Nombre", nuevoProducto.Nombre),
                new XElement("Precio", nuevoProducto.Precio),
                new XElement("Stock", nuevoProducto.Stock)
            ));
            xmlRead.Save(xmlPath);
            Console.WriteLine($"\n✅ Producto '{nuevoProducto.Nombre}' agregado al XML.");
        }
    }
}