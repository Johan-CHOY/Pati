using ApplicationCore.Services;
using Infraestructure;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                IServiceUsuario _SeviceUsuario = new ServiceUsuario();
                lista = _SeviceUsuario.GetAllUsers();
                ViewBag.tituloPag = "Lista de Usuarios";
                ViewBag.ListaUsuario = listaUsuario();
                return View(lista);

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }
        //[CustomAuthorize((int)TipoUsuarioEnum.Administrador, (int)TipoUsuarioEnum.Colaborador)]
        private IEnumerable<Usuario> listaUsuario()
        {
            //Lista de Usuario
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<Usuario> listaTipoUsuario = _ServiceUsuario.GetAllUsers();
            //Autor SelectAutor = listaAutores.Where(c => c.IdAutor == idAutor).FirstOrDefault();
            return listaTipoUsuario;


        }
        private SelectList listaTipoUsuario(int idTipoUsuario = 0)
        {
            //Lista de tipos de Usuario
            IServiceTipoUsuario _ServiceTipoUsuario = new ServiceTipoUsuario();
            IEnumerable<TipoUsuario> listaTipoUsuario = _ServiceTipoUsuario.GetTipoUsuario();
            //Autor SelectAutor = listaAutores.Where(c => c.IdAutor == idAutor).FirstOrDefault();
            return new SelectList(listaTipoUsuario, "IdTipoUsuario", "Descripcion", idTipoUsuario);


        }
        //[CustomAuthorize((int)TipoUsuarioEnum.Administrador, (int)TipoUsuarioEnum.Colaborador)]
        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.ListaUsuario = listaUsuario();
            ViewBag.ListaTipoUsuario = listaTipoUsuario();
            //ViewBag.IdCategoria = listaCategorias(null);

            return View();
        }
        //[CustomAuthorize((int)TipoUsuarioEnum.Administrador, (int)TipoUsuarioEnum.Colaborador)]
        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                try
                {

                    if (ModelState.IsValid)
                    {
                        Infraestructure.Models.Usuario oUsuario1;
                        oUsuario1 = (Infraestructure.Models.Usuario)Session["User"];
                        
                            usuario.Activo = 1;
                        

                        Usuario oUsuario2 = _ServiceUsuario.Save(usuario);


                    }
                    else
                    {

                        // Valida Errores si Javascript está deshabilitado
                        Util.ValidateErrors(this);
                        ViewBag.ListaUsuario = listaUsuario();
                        ViewBag.ListaTipoUsuario = listaTipoUsuario();
                        return View("Index");

                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Salvar el error en un archivo 
                    Log.Error(ex, MethodBase.GetCurrentMethod());
                    TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                    TempData["Redirect"] = "Producto";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }


            }
            catch
            {
                return View();
            }
        }
        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            ServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario usuario = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                usuario = _ServiceUsuario.GetUsuarioByID(id.Value);
                if (usuario == null)
                {
                    TempData["Message"] = "No existe el usuario solicitado";
                    TempData["Redirect"] = "Usuario";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                // ViewBag.ListaTipoUsuario = listaTipoUsuario();
                // ViewBag.ListaProveedores = listaProveedor(usuario.Proveedor);
                return View(usuario);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Usuario";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        //[CustomAuthorize((int)TipoUsuarioEnum.Administrador)]
        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {

            try
            {


                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                ServiceUsuario _ServiceUsuario = new ServiceUsuario();
                Usuario usuarioDesactivado = null;

                usuarioDesactivado = _ServiceUsuario.GetUsuarioByID(id);
                if (usuarioDesactivado.Activo == 1)
                {
                    usuarioDesactivado.Activo = 0;
                }
                else
                {
                    usuarioDesactivado.Activo = 1;
                }


                Usuario oUsuarioI = _ServiceUsuario.SaveEdit(usuarioDesactivado);



                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Usuario";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}