using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUDGame
{
    public partial class FrmHabilidade : System.Web.UI.Page
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
            var habilidade = HabilidadeDAO.ListarHabilidades(id);
            txtDescricao.Text = habilidade.Descricao;

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
            var habilidades = HabilidadeDAO.ListarHabilidades();
            PopularLVClasses(habilidades);
        }

        private void PopularLVClasses(List<Habilidade> habilidades)
        {
            lvHabilidades.DataSource = habilidades;
            lvHabilidades.DataBind();
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            string descricao = txtDescricao.Text;
            var cadastrando = btnConfirmar.Text == "Cadastrar";

            if (descricao != "")
            {
                //Criando uma instância da classe (objeto)
                Habilidade novaHabilidade = null;
                //Caso id seja -1, não existe uma classe para ser alterada
                int id = -1;

                if (cadastrando)
                {
                    novaHabilidade = new Habilidade();
                }
                else
                {
                    //Alterando
                    var idQuery = Request.QueryString["id"];
                    if (idQuery != null)
                    {
                        id = Convert.ToInt32(idQuery);
                        novaHabilidade = HabilidadeDAO.ListarHabilidades(id);
                    }
                }
                //Preencher o objeto
                novaHabilidade.Descricao = descricao;


                string mensagem = "";

                if (cadastrando)
                {
                    mensagem = HabilidadeDAO.CadastrarHabilidade(novaHabilidade);
                }
                else
                {
                    mensagem = HabilidadeDAO.AlterarHabilidade(novaHabilidade);
                    btnConfirmar.Text = "Cadastrar";
                }

                //Limpando o campo de texto
                txtDescricao.Text = "";

                lblMensagem.InnerText = mensagem;
                PopularLVs();
            }
        }

        protected void lvHabilidades_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {

                var id = e.CommandArgument;
                if (id != null)
                {
                    int idHabilidade = Convert.ToInt32(id);
                    Habilidade subExcluida =
                        HabilidadeDAO.Remover(idHabilidade);
                    if (subExcluida != null)
                    {
                        lblMensagem.InnerText = "Habilidade " +
                            subExcluida.Descricao +
                            " excluída com sucesso!";
                        btnConfirmar.Text = "Cadastrar";
                        txtDescricao.Text = "";
                        PopularLVs();
                    }
                }
            }
            else if (e.CommandName == "Visualizar")
            {
                var id = e.CommandArgument;
                if (id != null)
                {
                    Response.Redirect("~/Habilidades?id=" + id + "&edit=false");
                }
            }
            else if (e.CommandName == "Editar")
            {
                var id = e.CommandArgument;
                if (id != null)
                {
                    Response.Redirect("~/Habilidades?id=" + id + "&edit=true");
                }
            }
        }

    }
}