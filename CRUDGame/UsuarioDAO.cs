using System;
using System.Linq;

namespace CRUDGame
{
    internal class UsuarioDAO
    {

        internal static string CadastrarUsuario(Usuario usuario)
        {
            string mensagem = "";

            try
            {
                using (var ctx = new RPGDBEntities())
                {
                    ctx.Usuarios.Add(usuario);
                    ctx.SaveChanges();
                }
                mensagem = "Usuario cadastrado com sucesso!";
            }
            catch (Exception ex)
            {
                mensagem = ex.Message;
            }

            return mensagem;
        }


        internal static Usuario Autenticar(string user, string passCript)
        {
            Usuario userAutenticado = null;

            try
            {
                using (var ctx = new RPGDBEntities())
                {
                    userAutenticado = ctx.Usuarios.FirstOrDefault(
                            x => x.Login == user && x.Senha == passCript
                        );
                }
            }
            catch (Exception ex)
            {
                //Erro
            }

            return userAutenticado;
        }
    }
}