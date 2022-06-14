using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceArticulo
    {
        string Nombre();
        double Costo();
        double ObtenerPrecio();
        IEnumerable<Articulo> ObtenerArticulos();
        Articulo ObtenerArticuloPorID(int id);
        Articulo Save(Articulo articulo);
        Articulo SaveEdit(Articulo articulo);
        Articulo Create(Articulo articulo);
    }
}
