using ApplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        public IEnumerable<Usuario> GetAllUsers()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetAllUsers();
        }
        public IEnumerable<Usuario> GetAllDeactivatedUsers()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetAllDeactivatedUsers();
        }

        public Usuario GetUsuario(string user, string password)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();

            // Encriptar el password para poder compararlo
            string crytpPasswd = Cryptography.EncrypthAES(password);

            return repository.GetUsuario(user, crytpPasswd);
        }

        public Usuario GetUsuarioByID(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            Usuario oUsuario = repository.GetUsuarioByID(id);
            if (oUsuario != null) oUsuario.Contrasennia = Cryptography.DecrypthAES(oUsuario.Contrasennia);
            return oUsuario;
        }

        public Usuario Save(Usuario usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            usuario.Contrasennia = Cryptography.EncrypthAES(usuario.Contrasennia);
            return repository.Save(usuario);
        }
        public Usuario SaveEdit(Usuario Usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            Usuario.Contrasennia = Cryptography.EncrypthAES(Usuario.Contrasennia);
            return repository.SaveEdit(Usuario); ///, selectedCategorias
        }
        //public Usuario SendEmail(Usuario Usuario)
        //{
        //    IRepositoryUsuario repository = new RepositoryUsuario();
        //    Usuario.Contrasennia = Cryptography.EncrypthAES(Usuario.Contrasennia);
        //    return repository.SendEmail(Usuario); ///, selectedCategorias
        //}
    }
}
