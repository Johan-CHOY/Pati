using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryTipoUsuario : IRepositoryTipoUsuario
    {
        public IEnumerable<TipoUsuario> GetTipoUsuario()
        {
            try
            {

                IEnumerable<TipoUsuario> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.TipoUsuario.ToList<TipoUsuario>();
                }
                return lista;
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

        public TipoUsuario GetTipoUsuarioByID(int id)
        {
            TipoUsuario usuario = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    usuario = ctx.TipoUsuario.Find(id);
                }

                return usuario;
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
