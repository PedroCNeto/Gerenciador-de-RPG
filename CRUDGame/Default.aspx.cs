using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUDGame
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [Obsolete]
        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            var user = txtUsuario.Value;
            var pass = txtSenha.Value;

            if (user != null && pass != null)
            {
                var passCript =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "SHA1");
                Usuario userAutenticado = UsuarioDAO.Autenticar(user, passCript);

                if (userAutenticado != null)
                {
                    //Registrá-lo como válido
                    FormsAuthentication.SetAuthCookie(userAutenticado.Nome, true);

                    LogAcessoDAO.RegistrarAcesso(userAutenticado, DateTime.Now);

                    var perfil = userAutenticado.GetPerfil.Descricao;



                    if (perfil == "Usuario")
                    {
                        Response.Redirect("~/Sistema");
                    }
                }
                else
                {
                    lblMensagem.InnerText = "Erro de login!";
                }
            }
        }
    }
}