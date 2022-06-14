using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        // GET: Recovery
        public ActionResult Recovery()
        {
            return View();
        }
        // POST: Recovery
        public ActionResult RecoverySent(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario = _ServiceUsuario.GetUsuarioByID(usuario.IdUsuario); ;
            if (oUsuario != null) usuario.Contrasennia = oUsuario.Contrasennia;
            try
            {



                if (oUsuario == null)
                {
                    Utils.Log.Warn($"{usuario.IdUsuario} se intentó conectar  y falló");
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Ese usuario no existe", "Por favor ingrese el dato correcto", SweetAlertMessageType.warning);

                }
                else
                {
                    //_ServiceUsuario.SendEmail(oUsuario);
                    Utils.Log.Info($"Se reestablece la contraseña para: {oUsuario.Nombre} ");
                    TempData["mensaje"] = Utils.SweetAlertHelper.Mensaje("Login", "Usario autenticado satisfactoriamente", SweetAlertMessageType.success);
                    return RedirectToAction("Index");

                }


                return View("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult Login(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario = null;
            try
            {
                if (ModelState.IsValid)
                {
                    oUsuario = _ServiceUsuario.GetUsuario(usuario.Usuario1, usuario.Contrasennia);

                    if (oUsuario == null)
                    {
                        Utils.Log.Warn($"{usuario.IdUsuario} se intentó conectar  y falló");
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Usuario incorrecto", "Por favor ingrese los datos correctos", SweetAlertMessageType.warning);

                    }
                    else if (oUsuario.Activo == 0)
                    {
                        Utils.Log.Warn($"{usuario.IdUsuario} se intentó conectar  y falló");
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Usuario no activo", "Por favor contacte a soporte", SweetAlertMessageType.warning);

                    }
                    else
                    {
                        Session["User"] = oUsuario;
                        Utils.Log.Info($"Accede {oUsuario.Nombre} con el rol {oUsuario.TipoUsuario.IdTipoUsuario}-{oUsuario.TipoUsuario.Descripcion}");
                        TempData["mensaje"] = Utils.SweetAlertHelper.Mensaje("Login", "Usario autenticado satisfactoriamente", SweetAlertMessageType.success);
                        return RedirectToAction("Index", "Home");

                    }
                }

                return View("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult UnAuthorized()
        {
            try
            {
                ViewBag.Message = "No autorizado";

                if (Session["User"] != null)
                {
                    Usuario oUsuario = Session["User"] as Usuario;
                    Utils.Log.Warn($"El usuario {oUsuario.Nombre}  con el rol {oUsuario.TipoUsuario.IdTipoUsuario}-{oUsuario.TipoUsuario.Descripcion}, intentó acceder una página sin permisos  ");
                }

                return View();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "Login";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult Logout()
        {
            Usuario oUsuario = Session["User"] as Usuario;
            try
            {
                if (oUsuario != null)
                {
                    Utils.Log.Info($"Se desconecta {oUsuario.Nombre} con el rol {oUsuario.TipoUsuario.IdTipoUsuario}-{oUsuario.TipoUsuario.Descripcion}");
                    Session["User"] = null;
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "Login";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }
    }
}