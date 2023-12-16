using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUDGame
{
    public partial class FrmRaca : System.Web.UI.Page
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
            var raca = RacaDAO.ListarRacas(id);
            txtDescricao.Text = raca.Descricao;

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
            var raca = RacaDAO.ListarRacas();
            PopularLVRacas(raca);
        }

        private void PopularLVRacas(List<Raca> racas)
        {
            lvRaca.DataSource = racas;
            lvRaca.DataBind();
        }
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            string descricao = txtDescricao.Text;
            var cadastrando = btnConfirmar.Text == "Cadastrar";

            if (descricao != "")
            {
                //Criando uma instância da classe (objeto)
                Raca novaRaca = null;
                //Caso id seja -1, não existe uma classe para ser alterada
                int id = -1;

                if (cadastrando)
                {
                    novaRaca = new Raca();
                }
                else
                {
                    //Alterando
                    var idQuery = Request.QueryString["id"];
                    if (idQuery != null)
                    {
                        id = Convert.ToInt32(idQuery);
                        novaRaca = RacaDAO.ListarRacas(id);
                    }
                }
                //Preencher o objeto
                novaRaca.Descricao = descricao;


                string mensagem = "";

                if (cadastrando)
                {
                    mensagem = RacaDAO.CadastrarRaca(novaRaca);
                }
                else
                {
                    mensagem = RacaDAO.AlterarRaca(novaRaca);
                    btnConfirmar.Text = "Cadastrar";
                }

                //Limpando o campo de texto
                txtDescricao.Text = "";

                lblMensagem.InnerText = mensagem;
                PopularLVs();
            }
        }
        protected void lvRacas_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                try
                {
                    var id = e.CommandArgument;
                    if (id != null)
                    {
                        int idRaca = Convert.ToInt32(id);
                        Raca subExcluida =
                            RacaDAO.Remover(idRaca);
                        if (subExcluida != null)
                        {
                            lblMensagem.InnerText = "Raca " +
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
                    lblMensagem.InnerText = "Erro na exclusão (Provavelmente essa raca está em uso)!";
                }

            }
            else if (e.CommandName == "Visualizar")
            {
                var id = e.CommandArgument;
                if (id != null)
                {
                    Response.Redirect("~/Racas?id=" + id + "&edit=false");
                }
            }
            else if (e.CommandName == "Editar")
            {
                var id = e.CommandArgument;
                if (id != null)
                {
                    Response.Redirect("~/Racas?id=" + id + "&edit=true");
                }
            }
        }
    }
}