using Microsoft.AspNetCore.Mvc;
using REGIS_DATOS.Models;
using REGIS_NEG.Login_Neg;
using REGIS_NEG.UtilidadServicio;

namespace REGISTRADURIA.Controllers
{
    public class InicioController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        NLogin objLogin = new NLogin();
        Util objUtilidad = new Util();
        REGIS_NEG.Login_Neg.NPersons BussinesPerson = new REGIS_NEG.Login_Neg.NPersons();
        REGIS_DATOS.Models.Persons objPersona = new REGIS_DATOS.Models.Persons();

        public InicioController(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string correo, string clave)
        {
            Users usuario = objLogin.ValidateLogin(correo, objUtilidad.ConvertirHA256(clave));

            if (usuario != null)
            {
                if (usuario.isActive == true)//Valida que el usuario este activo
                {
                    if (usuario.Reinstate)
                    {
                        ViewBag.Mensaje = $"Se ha solicitado restablecer su cuenta, por favor revise su bandeja de correo: {correo}";
                    }
                    else 
                    {
                        if (usuario.Confirmed)
                        {
                            objPersona = BussinesPerson.GetPersonById(usuario.IdPerson);


                            //Lo envia al Home
                            return RedirectToAction("Menu", "Inicio");
                        }
                        else
                        {
                            ViewBag.Mensaje = $"Por favor confirme su cuenta; Se le envio a el correo: {correo}";
                        }
                    }
                }
                else
                {
                    ViewBag.Mensaje = $"El usuario: {correo}, no se encuentra activo.";
                }
            }
            else
            {
                ViewBag.Mensaje = "No se encontraron coincidencias";
            }
            return View();
        }


    }
}
