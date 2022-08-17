using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibreriaMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace LibreriaMVC.Controllers
{
    public class ClienteController : Controller
    {
        // GET: ClienteController
        private LibreriaContext db = new LibreriaContext();
        public ActionResult ListarCliente()
        {
            var listado_cliente = db.Clientes.ToList();
            foreach (var cliente in listado_cliente)
            {
                cliente.Genero = (cliente.Genero.ToString().Equals("M")) ? "MASCULINO" : "FEMENINO";
            }

            return View(listado_cliente);
        }

        // GET: ClienteController/Create
        public ActionResult CreateCliente()
        {
            Cliente newalumno = new Cliente();
            return View(newalumno);

        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCliente(Cliente newCliente)
        {
            try
            {
                var cli = db.Clientes.Where(c => c.Dni.Equals(newCliente.Dni)).FirstOrDefault();
                if (cli == null)
                {
                    db.Clientes.Add(newCliente);
                    db.SaveChanges();
                    ViewData["MENSAJE"] = "ALUMNO REGISTRADO CORRECTAMENTE";
                }
                else
                {
                    ViewData["MENSAJE"] = "NO SE REALIZO EL REGISTRO (CODIGO EXISTENTE), DIGITE OTRO CODIGO";
                }
                return View(newCliente);
            }
            catch (Exception ex)
            {
                ViewData["MENSAJE"] = ex.Message;
                return View(newCliente);
            }
        }

        // GET: ClienteController/Edit/5
        public ActionResult UpdateCliente(int id)
        {
            var cli = db.Clientes.Where(c => c.Id == id).FirstOrDefault();
            return (cli != null) ? View(cli) : RedirectToAction(nameof(ListarCliente));
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCliente(int id, Cliente updateCliente)
        {
            try
            {
                Cliente cliente = db.Clientes.Find(id);
                cliente.Dni = updateCliente.Dni;
                cliente.Nombres = updateCliente.Nombres;
                cliente.ApPaterno = updateCliente.ApPaterno;
                cliente.ApMaterno = updateCliente.ApMaterno;
                cliente.Estado = updateCliente.Estado;
                cliente.Genero = updateCliente.Genero;
                db.SaveChanges();
                ViewBag.MENSAJE = "CLIENTE ACTUALIZADO CORRECTAMENTE";
                return View(updateCliente);
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
                return View(updateCliente);
            }
        }

        // GET: ClienteController/Delete/5
        public ActionResult DeleteCliente(int id = 0)
        {
            var cli = db.Clientes.Where(c => c.Id == id).FirstOrDefault();
            return (cli != null) ? View(cli) : RedirectToAction(nameof(ListarCliente));
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCliente(int id, IFormCollection collection)
        {
            try
            {
                var cli = db.Clientes.Where(c => c.Id == id).FirstOrDefault();

                if (cli != null)
                {
                    db.Clientes.Remove(cli);
                    db.SaveChanges();
                    ViewData["MENSAJE"] = "ALUMNO ELIMINADO CORRECTAMENTE";
                }
                else
                {
                    ViewData["MENSAJE"] = "NO SE REALIZO EL REGISTRO (CODIGO INEXISTENTE), DIGITE OTRO CODIGO";

                }
                return View(cli);
            }
            catch (Exception ex)
            {
                ViewData["MENSAJE"] = ex.Message;
                return View(new Cliente());
            }
        }
    }
}
