using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceUsuario
    {
        Usuario GetUsuarioByID(int id);
        Usuario Save(Usuario usuario);
        Usuario SaveEdit(Usuario usuario);
        Usuario GetUsuario(string user, string password);
        //Usuario SendEmail(Usuario usuario);
        IEnumerable<Usuario> GetAllUsers();
        IEnumerable<Usuario> GetAllDeactivatedUsers();
    }
}

