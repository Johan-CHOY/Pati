using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class ArticuloController : Controller
    {
        // GET: Producto/Create
        public ActionResult Create()
        {
            
            return View();
        }
        //[CustomAuthorize((int)TipoUsuarioEnum.Administrador)]
        // POST: Producto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //[CustomAuthorize((int)TipoUsuarioEnum.Administrador)]
        // GET: Articulo
        public ActionResult Index()
        {
            IEnumerable<Articulo> lista = null;
            try
            {
                IServiceArticulo _ServiceArticulo = new ServiceArticulo();
                lista = _ServiceArticulo.ObtenerArticulos();

            }catch(Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }
        // GET: Articulo/Edit/5
        public ActionResult Edit(int id)
        {
            ServiceArticulo _ServiceArticulo = new ServiceArticulo();
            Articulo articulo = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                articulo = _ServiceArticulo.ObtenerArticuloPorID(id);
                if (articulo == null)
                {
                    TempData["Message"] = "No existe el articulo solicitado";
                    TempData["Redirect"] = "Articulo";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                
                return View(articulo);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Articulo";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        // GET: Articulo/Details/5
        public ActionResult Details(int? id)
        {
            ServiceArticulo _ServiceArticulo = new ServiceArticulo();
            Articulo articulo = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                articulo = _ServiceArticulo.ObtenerArticuloPorID(id.Value);
                if (articulo == null)
                {
                    TempData["Message"] = "No existe el articulo solicitado";
                    TempData["Redirect"] = "Articulo";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(articulo);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Articulo";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        // POST: Articulo/Edit/5
        [HttpPost]
        public ActionResult Save(Articulo articulo, string isEdit)
        {
            MemoryStream target = new MemoryStream();
            IServiceArticulo _ServiceArticulo = new ServiceArticulo();
            try
            {

                //if (ModelState.IsValid)
                //{
                    if (isEdit.Equals("1"))
                    {

                        Articulo oArticulo = _ServiceArticulo.SaveEdit(articulo);//, selectedCategorias
                    }
                    else
                    {

                        Articulo oArticulo = _ServiceArticulo.Save(articulo);//, selectedCategorias
                    }

                //}
                //else
                //{
                    // Valida Errores si Javascript está deshabilitado
                    //Util.ValidateErrors(this);
                    //ViewBag.ListaDeTipoArticulo = listaTipoArticulo(articulo.IdTipoArticulo);
                    //ViewBag.ListaDeProveedores = listaProveedor(articulo.IdArticulo);
                    if (isEdit.Equals("1"))
                    {
                        return View("Edit", articulo);
                    }
                    else
                    {
                        return View("Create", articulo);
                    }

                //}

               // return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Proveedor";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        // GET: Articulo/Delete/5
        public ActionResult Delete(int? id)
        {
            ServiceArticulo _ServiceArticulo = new ServiceArticulo();
            Articulo articulo = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                articulo = _ServiceArticulo.ObtenerArticuloPorID(id.Value);
                if (articulo == null)
                {
                    TempData["Message"] = "No existe el articulo solicitado";
                    TempData["Redirect"] = "Articulo";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                // ViewBag.ListaTipoArticulo = listaTipoArticulo();
                // ViewBag.ListaProveedores = listaProveedor(articulo.Proveedor);
                return View(articulo);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Articulo";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        //[CustomAuthorize((int)TipoUsuarioEnum.Administrador)]
        // POST: Articulo/Delete/5
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
                ServiceArticulo _ServiceArticulo = new ServiceArticulo();
                Articulo articuloDesactivado = null;

                articuloDesactivado = _ServiceArticulo.ObtenerArticuloPorID(id);
                if (articuloDesactivado.Activo == 1)
                {
                    articuloDesactivado.Activo = 0;
                }
                else
                {
                    articuloDesactivado.Activo = 1;
                }
                

                Articulo oArticuloI = _ServiceArticulo.SaveEdit(articuloDesactivado);



                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Articulo";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}