using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryArticulo : IRepositoryArticulo
    {
        public double Costo()
        {
            throw new NotImplementedException();
        }

        public string Nombre()
        {
            throw new NotImplementedException();
        }

        public Articulo ObtenerArticuloPorID(int id)
        {
            Articulo oArticulo = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oArticulo = ctx.Articulo.
                    Where(l => l.IdArticulo.Equals(id))
                    .First<Articulo>();
            }
            return oArticulo;
        }

        public IEnumerable<Articulo> ObtenerArticulos()
        {
            IEnumerable<Articulo> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Articulo.ToList<Articulo>();
            }
            return lista;
        }

        public double ObtenerPrecio()
        {
            throw new NotImplementedException();
        }

        public Articulo Save(Articulo articulo)
        {
            try
            {
                int retorno = 0;
                Articulo oArticulo = null;

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    
                    articulo.Activo = 1;

                    
                    ctx.Articulo.Add(articulo);
                    //SaveChanges
                    //guarda todos los cambios realizados en el contexto de la base de datos.
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas


                }

                if (retorno >= 0)
                    oArticulo = ObtenerArticuloPorID(articulo.IdArticulo);

                return oArticulo;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Articulo SaveEdit(Articulo articulo)
        {
            try
            {
                int retorno = 0;
                Articulo oArticulo = null;

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oArticulo = ObtenerArticuloPorID(articulo.IdArticulo);
                    ctx.Entry(articulo).State = EntityState.Modified;
                    ctx.SaveChanges();


                }

                return oArticulo;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}
