using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibreriaMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace LibreriaMVC.Controllers
{
    public class ProductoController : Controller
    {
        // GET: ClienteController
        private LibreriaContext db = new LibreriaContext();
        public ActionResult ListarProducto()
        {
            var listado_productos = db.Productos.Include(p=>p.IdProveedorNavigation).ToList();

            return View(listado_productos);
        }
        public ActionResult ListarProductoProveedor(string proveedor = " ")
        {
            var listado = db.PA_PRODUCTOS_PROVEEDOR.FromSqlRaw
                <PA_PRODUCTOS_PROVEEDOR>("EXEC dbo.PA_PRODUCTOS_PROVEEDOR {0}", proveedor).ToList();

            ViewData["PROVEEDORES"] = new SelectList(db.Proveedors.Select(p => p.Nombre).Distinct().ToList());


            ViewBag.FILAS = listado.Count();
            ViewBag.TOTAL = listado.Sum(p => p.total);

            return View(listado);

        }

        public ActionResult ListarProductoStock(int min = 0, int max = 0)
        {
            var listado = db.PA_PRODUCTOS_STOCK.FromSqlRaw
                <PA_PRODUCTOS_STOCK>("EXEC dbo.PA_PRODUCTOS_STOCK {0},{1}",min,max).ToList();


            ViewBag.MIN = min;
            ViewBag.MAX = max;
            ViewBag.FILAS = listado.Count();
            ViewBag.TOTAL = listado.Sum(p => p.total);

            return View(listado);

        }

        // GET: ClienteController/Create
        public ActionResult CreateProducto()
        {
            Producto newproducto = new Producto();
            ViewBag.PROVEEDORES = new SelectList(db.Proveedors.ToList(), "Id", "Nombre");
            return View(newproducto);

        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProducto(Producto newProducto)
        {
            ViewBag.PROVEEDORES = new SelectList(db.Proveedors.ToList(), "Id", "Nombre");
            try
            {

                    db.Productos.Add(newProducto);
                    db.SaveChanges();
                    ViewData["MENSAJE"] = "PRODUCTO REGISTRADO CORRECTAMENTE";
                

                return View(newProducto);
            }
            catch (Exception ex)
            {
                ViewData["MENSAJE"] = ex.Message;
                return View(newProducto);
            }
        }

        // GET: ClienteController/Edit/5
        public ActionResult UpdateProducto(int id)
        {
            var pro = db.Productos.Where(p => p.Id == id).FirstOrDefault();

            if (pro != null){
                ViewBag.PROVEEDORES = new SelectList(db.Proveedors.ToList(), "Id", "Nombre");
                return View(pro); 
            }else return RedirectToAction(nameof(ListarProducto));
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProducto(int id, Producto updateProducto)
        {
            ViewBag.PROVEEDORES = new SelectList(db.Proveedors.ToList(), "Id", "Nombre");
            try
            {
                Producto producto = db.Productos.Find(id);
                producto.Marca = updateProducto.Marca;
                producto.Descripcion = updateProducto.Descripcion;
                producto.Stock = updateProducto.Stock;
                producto.Precio = updateProducto.Precio;
                producto.IdProveedor = updateProducto.IdProveedor;
                db.SaveChanges();
                ViewBag.MENSAJE = "PRODUCTO ACTUALIZADO CORRECTAMENTE";
                return View(updateProducto);
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
                return View(updateProducto);
            }
        }

        // GET: ClienteController/Delete/5
        public ActionResult DeleteProducto(int id = 0)
        {
            var pro = db.Productos.Include(p => p.IdProveedorNavigation).Where(p => p.Id == id).FirstOrDefault();
            if (pro != null)
            {
                ViewBag.PROVEEDORES = new SelectList(db.Proveedors.ToList(), "Id", "Nombre");
                return View(pro);
            }
            else return RedirectToAction(nameof(ListarProducto));
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProducto(int id, IFormCollection collection)
        {
            ViewBag.PROVEEDORES = new SelectList(db.Proveedors.ToList(), "Id", "Nombre");

            try
            {
                var pro = db.Productos.Where(p => p.Id == id).FirstOrDefault();

                if (pro != null)
                {
                    db.Productos.Remove(pro);
                    db.SaveChanges();
                    ViewData["MENSAJE"] = "PRODUCTOS ELIMINADO CORRECTAMENTE";
                }
                else
                {
                    ViewData["MENSAJE"] = "PRODUCTO NO ENCONTRADO";

                }
                return View(pro);
            }
            catch (Exception ex)
            {
                ViewData["MENSAJE"] = ex.Message;
                return View(new Producto());
            }
        }
    }
}
