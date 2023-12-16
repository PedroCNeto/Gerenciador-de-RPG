using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUDGame
{
    public partial class FrmCadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<PerfilUsuario> perfis =
                    PerfilUsuarioDAO.ListarPerfis();
                AtualizarDDLPerfil(perfis);
            }
        }

        private void AtualizarDDLPerfil(List<PerfilUsuario> perfis)
        {
            ddlPerfilUsuario.DataSource = perfis;
            ddlPerfilUsuario.DataTextField = "Descricao";
            ddlPerfilUsuario.DataValueField = "IdPerfilUsuario";
            ddlPerfilUsuario.DataBind();
        }

        [Obsolete]
        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
                        try
            {
                var senha = txtSenha.Value;
                var repetirSenha = txtRepetirSenha.Value;

                if (senha == repetirSenha)
                {
                    if (ddlPerfilUsuario.SelectedIndex == 0)
                    {
                        Usuario usuario = new Usuario();
                        usuario.DataNasc = Convert.ToDateTime(txtDataNasc.Value);
                        usuario.Login = txtLogin.Value;
                        usuario.Nome = txtNomeUsuario.Value;
                        usuario.PerfilUsuarioId =
                            Convert.ToInt32(ddlPerfilUsuario.SelectedValue);

                        //Criptografando a senha
                        var senhaCriptografada =
                                FormsAuthentication.
                                HashPasswordForStoringInConfigFile(senha, "SHA1");
                        usuario.Senha = senhaCriptografada;

                        string mensagem = UsuarioDAO.CadastrarUsuario(usuario);

                        lblMensagem.InnerText = mensagem;
                        txtDataNasc.Value = "";
                        txtLogin.Value = "";
                        txtNomeUsuario.Value = "";
                        txtRepetirSenha.Value = "";
                        txtSenha.Value = "";
                    }
                    else
                    {
                        //Exibir mensagem informando para selecionar o perfil
                    }
                }
                else
                {
                    //Disparar mensagem de erro sobre as senhas
                    lblMensagem.InnerText = "Erro no cadastro, tente novamente";
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}