using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUDGame
{
    public partial class FrmCores : System.Web.UI.Page
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
            var cor = CoresDAO.ListarCores(id);
            txtDescricao.Text = cor.Descricao;

            //Verifica se iremos editar os dados ou não
            if (edit)
            {
                //Editando
                btnConfirmar.Text = "Alterar";
            }
            else
            {
                //Visualizando
                btnConfirmar.Visible = false;
                txtDescricao.Enabled = false;
            }
        }

        private void PopularLVs()
        {
            var cores = CoresDAO.ListarCores();
            PopularLVCores(cores);
        }

        private void PopularLVCores(List<Core> cores)
        {
            lvCores.DataSource = cores;
            lvCores.DataBind();
        }

        protected void lvCores_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                try
                {
                    var id = e.CommandArgument;
                    if (id != null)
                    {
                        int idCor = Convert.ToInt32(id);
                        Core subExcluida =
                            CoresDAO.Remover(idCor);
                        if (subExcluida != null)
                        {
                            lblMensagem.InnerText = "Cor " +
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
                    lblMensagem.InnerText = "Erro na exclusão (Provavelmente essa cor está em uso)!";
                }

            }
            else if (e.CommandName == "Visualizar")
            {
                var id = e.CommandArgument;
                if (id != null)
                {
                    Response.Redirect("~/Cores?id=" + id + "&edit=false");
                }
            }
            else if (e.CommandName == "Editar")
            {
                var id = e.CommandArgument;
                if (id != null)
                {
                    Response.Redirect("~/Cores?id=" + id + "&edit=true");
                }
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            string descricao = txtDescricao.Text;
            var cadastrando = btnConfirmar.Text == "Cadastrar";

            if (descricao != "")
            {
                //Criando uma instância da classe (objeto)
                Core novaCor = null;
                //Caso id seja -1, não existe uma classe para ser alterada
                int id = -1;

                if (cadastrando)
                {
                    novaCor = new Core();
                }
                else
                {
                    //Alterando
                    var idQuery = Request.QueryString["id"];
                    if (idQuery != null)
                    {
                        id = Convert.ToInt32(idQuery);
                        novaCor = CoresDAO.ListarCores(id);
                    }
                }
                //Preencher o objeto
                novaCor.Descricao = descricao;


                string mensagem = "";

                if (cadastrando)
                {
                    mensagem = CoresDAO.CadastrarCor(novaCor);
                }
                else
                {
                    mensagem = CoresDAO.AlterarCor(novaCor);
                    btnConfirmar.Text = "Cadastrar";
                }

                //Limpando o campo de texto
                txtDescricao.Text = "";

                lblMensagem.InnerText = mensagem;
                PopularLVs();
            }
        }
    }
}