using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUDGame.Sistema
{
    public partial class FrmClasse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var queryString_ID = Request.QueryString["id"];
                var queryString_Edit = Request.QueryString["edit"];

                if (queryString_ID != null && queryString_Edit != null)
                {
                    int id = Convert.ToInt32(queryString_ID);
                    PreencherDados(id, queryString_Edit == "true");
                }
                PopularLVs();
            }
        }

        private void PreencherDados(int id, bool edit)
        {
            var classe = ClasseDAO.ListarClasses(id);
            txtDescricao.Text = classe.Descricao;

            //Verifica se iremos editar os dados ou não
            if (edit) {
                //Editando
                btnConfirmar.Text = "Alterar";
            }
            else {
                //Visualizando
                btnConfirmar.Visible = false;
                txtDescricao.Enabled = false;
            }


        }

        private void PopularLVs()
        {
            var classes = ClasseDAO.ListarClasses();
            PopularLVClasses(classes);
        }

        private void PopularLVClasses(List<Class> classes)
        {
            lvClasses.DataSource = classes;
            lvClasses.DataBind();
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            string descricao = txtDescricao.Text;
            var cadastrando = btnConfirmar.Text == "Cadastrar";

            if (descricao != "")
            {
                //Criando uma instância da classe (objeto)
                Class novaClasse = null;
                //Caso id seja -1, não existe uma classe para ser alterada
                int id = -1;
                
                if (cadastrando)
                {
                    novaClasse = new Class();
                }
                else
                {
                    //Alterando
                    var idQuery = Request.QueryString["id"];
                    if (idQuery != null)
                    {
                        id = Convert.ToInt32(idQuery);
                        novaClasse = ClasseDAO.ListarClasses(id);
                    }
                }
                //Preencher o objeto
                novaClasse.Descricao = descricao;


                string mensagem = "";

                if (cadastrando)
                {
                    mensagem = ClasseDAO.CadastrarClasse(novaClasse);
                }
                else
                {
                    mensagem = ClasseDAO.AlterarClasse(novaClasse);
                    btnConfirmar.Text = "Cadastrar";
                }

                //Limpando o campo de texto
                txtDescricao.Text = "";

                lblMensagem.InnerText = mensagem;
                PopularLVs();
            }
        }

        protected void lvClasses_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                try
                {
                    var id = e.CommandArgument;
                    if (id != null)
                    {
                        int idClasse = Convert.ToInt32(id);
                        Class subExcluida =
                            ClasseDAO.Remover(idClasse);
                        if (subExcluida != null)
                        {
                            lblMensagem.InnerText = "Classe " +
                                subExcluida.Descricao +
                                " excluída com sucesso!";
                            btnConfirmar.Text = "Cadastrar";
                            txtDescricao.Text = "";
                            PopularLVs();
                        }
                    }
                }
                catch (Exception)
                {
                    lblMensagem.InnerText = "Erro na exclusão (Provavelmente essa classe está em uso)!";
                }

            }
            else if (e.CommandName == "Visualizar")
            {
                var id = e.CommandArgument;
                if (id != null)
                {
                    Response.Redirect("~/Classes?id=" + id + "&edit=false");
                }
            }
            else if (e.CommandName == "Editar")
            {
                var id = e.CommandArgument;
                if (id != null)
                {
                    Response.Redirect("~/Classes?id=" + id + "&edit=true");
                }
            }
        }
    }
}