using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceArticulo : IServiceArticulo
    {
        public double Costo()
        {
            throw new NotImplementedException();
        }

        public Articulo Create(Articulo articulo)
        {
            throw new NotImplementedException();
        }

        public string Nombre()
        {
            throw new NotImplementedException();
        }

        public Articulo ObtenerArticuloPorID(int id)
        {
            IRepositoryArticulo repository = new RepositoryArticulo();
            return repository.ObtenerArticuloPorID(id);
        }

        public IEnumerable<Articulo> ObtenerArticulos()
        {
            IRepositoryArticulo repository = new RepositoryArticulo();
            return repository.ObtenerArticulos();
        }

        public double ObtenerPrecio()
        {
            throw new NotImplementedException();
        }
        public Articulo Save(Articulo articulo)
        {
            IRepositoryArticulo repository = new RepositoryArticulo();
            return repository.Save(articulo); 
        }


        public Articulo SaveEdit(Articulo articulo)
        {
            IRepositoryArticulo repository = new RepositoryArticulo();
            return repository.SaveEdit(articulo); 
        }
    }
}

