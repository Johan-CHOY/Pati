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
    public class RepositoryUsuario : IRepositoryUsuario
    {

        public Usuario GetUsuarioByID(int id)
        {
            Usuario usuario = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    usuario = ctx.Usuario.
                              Include("TipoUsuario").
                              Where(p => p.IdUsuario == id).
                              FirstOrDefault<Usuario>();
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
        public Usuario Save(Usuario usuario)
        {
            int retorno = 0;
            Usuario oUsuario = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = GetUsuarioByID(usuario.IdUsuario);
                    if (oUsuario == null)
                    {
                        ctx.Usuario.Add(usuario);
                    }
                    else
                    {
                        ctx.Entry(usuario).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oUsuario = GetUsuarioByID(usuario.IdUsuario);

                return oUsuario;
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
        public Usuario GetUsuario(string email, string password)
        {

            Usuario oUsuario = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.Usuario.
                                 Where(p => p.Usuario1.Equals(email) && p.Contrasennia == password).
                                 FirstOrDefault<Usuario>();
                }

                if (oUsuario != null)
                    oUsuario = GetUsuarioByID(oUsuario.IdUsuario);

                return oUsuario;
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

        //método para obtener todos los usuarios 
        public IEnumerable<Usuario> GetAllUsers()
        {
            IEnumerable<Usuario> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Usuario.Include(t => t.TipoUsuario).ToList();

            }
            return lista;
        }
        //método para obtener todos los usuarios desactivados
        public IEnumerable<Usuario> GetAllDeactivatedUsers()
        {
            IEnumerable<Usuario> lista = null;
            IList<Usuario> listaDesactivada = null;
            IEnumerable<Usuario> listaDesactivadaIE = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Usuario.Include(t => t.TipoUsuario).ToList();

            }
            foreach (Usuario u in lista)
            {
                if (u.Activo == 0) listaDesactivada.Add(u);
            }
            listaDesactivadaIE = listaDesactivada.ToList();
            return listaDesactivadaIE;
        }
        public Usuario SaveEdit(Usuario Usuario)
        {
            try
            {
                int retorno = 0;
                Usuario oUsuario = null;

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = GetUsuarioByID(Usuario.IdUsuario);

                    oUsuario.Activo = Usuario.Activo;

                    ctx.Entry(Usuario).State = EntityState.Modified;
                    ctx.SaveChanges();


                }

                return oUsuario;
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
        //public Usuario SendEmail(Usuario Usuario)
        //{
        //    try
        //    {
        //        int retorno = 0;
        //        Usuario oUsuario = null;

        //        using (MyContext ctx = new MyContext())
        //        {
        //            ctx.Configuration.LazyLoadingEnabled = false;
        //            oUsuario = GetUsuarioByID(Usuario.IdUsuario);
        //            Random rdn = new Random();
        //            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
        //            int longitud = caracteres.Length;
        //            char letra;
        //            int longitudContrasenia = 10;
        //            string contraseniaAleatoria = string.Empty;
        //            for (int i = 0; i < longitudContrasenia; i++)
        //            {
        //                letra = caracteres[rdn.Next(longitud)];
        //                contraseniaAleatoria += letra.ToString();
        //            }

        //            GmailHelper.GmailUsername = "neosportsystem@gmail.com";
        //            GmailHelper.GmailPassword = "123Queso";

        //            GmailHelper mailer = new GmailHelper();
        //            mailer.ToEmail = oUsuario.IdUsuario;
        //            mailer.Subject = "Reestablecimiento de contraseña";
        //            mailer.Body = "Su contraseña ha sido reestablecida<br> Ahora es: " + contraseniaAleatoria + " <br> Si usted no reestableció su contraseña, por favor contáctese con soporte";
        //            mailer.IsHtml = true;
        //            mailer.Send();
        //            Usuario.Contrasena = Cryptography.EncrypthAES(contraseniaAleatoria);

        //            ctx.Entry(Usuario).State = EntityState.Modified;
        //            ctx.SaveChanges();


        //        }

        //        return oUsuario;
        //    }

        //    catch (DbUpdateException dbEx)
        //    {
        //        string mensaje = "";
        //        Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw new Exception(mensaje);
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = "";
        //        Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw;
        //    }
        //}
    }
}
