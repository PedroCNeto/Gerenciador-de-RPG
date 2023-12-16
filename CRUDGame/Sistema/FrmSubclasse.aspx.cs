using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUDGame
{
    public partial class FrmSubclasse : System.Web.UI.Page
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
                List<Class> classes = ClasseDAO.ListarClasses();
                PreencherDDLClasse(classes);
                PopularLVs();
            }
        }

        private void PreencherDados(int id, bool edit)
        {
            var subclasse = SubclasseDAO.ListarSubclasse(id);
            txtDescricao.Text = subclasse.Descricao;
            //Verifica se iremos editar os dados ou não
            if (edit)
            {
                //Editando
                btnConfirmar.Text = "Alterar";
                DDLClasse.SelectedValue = subclasse.ClasseID.ToString();
            }
            else
            {
                //Visualizando
                btnConfirmar.Visible = false;
                txtDescricao.Enabled = false;
                DDLClasse.SelectedValue = subclasse.ClasseID.ToString();
                DDLClasse.Enabled = false;
            }


        }
        private void PopularLVs()
        {
            var subclasses = SubclasseDAO.ListarSubclasses();
            lvSubclasses.DataSource = subclasses;
            lvSubclasses.DataBind();
        }

        private void PreencherDDLClasse(List<Class> classes)
        {
            DDLClasse.DataSource = classes;
            DDLClasse.DataTextField = "Descricao";
            DDLClasse.DataValueField = "IdClasse";
            DDLClasse.DataBind();
            DDLClasse.Items.Insert(0, "Selecione..");
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            string descricao = txtDescricao.Text;
            int index = DDLClasse.SelectedIndex;
            var cadastrando = btnConfirmar.Text == "Cadastrar";
            if (index == 0)
            {
                lblMensagem.InnerText = 
                    "Você precisa selecionar uma classe!";
            }
            else if (descricao != "")
            {

                Subclass novaSubclasse = null;
                //Caso id seja -1, não existe uma classe para ser alterada
                int id = -1;



                if (cadastrando) {
                    //Criando uma instância da classe (objeto)
                    novaSubclasse = new Subclass();
                    //Preencher o objeto
                    novaSubclasse.Descricao = descricao;

                    //Capturando o id da Classe dessa subclasse
                    int idClasse = Convert.ToInt32(
                        DDLClasse.SelectedValue);
                    novaSubclasse.ClasseID = idClasse;

                    string mensagem =
                        SubclasseDAO.CadastrarSubclasse(novaSubclasse);
                    //Limpando o campo de texto
                    txtDescricao.Text = "";
                    DDLClasse.SelectedIndex = 0;
                    lblMensagem.InnerText = mensagem;
                    PopularLVs();
                }
                else
                {

                    var idQuery = Request.QueryString["id"];
                    if (idQuery != null)
                    {
                        novaSubclasse = new Subclass();
                        id = Convert.ToInt32(idQuery);
                        novaSubclasse = SubclasseDAO.ListarSubclasse(id);
                        novaSubclasse.Descricao = descricao;
                        novaSubclasse.ClasseID = Convert.ToInt32(DDLClasse.SelectedValue);
                        string mensagem = SubclasseDAO.AlterarSubclasse(novaSubclasse);
                        txtDescricao.Text = "";
                        DDLClasse.SelectedIndex = 0;
                        btnConfirmar.Text = "Cadastrar";
                        lblMensagem.InnerText = mensagem;
                        PopularLVs();
                    }

                }
            }
        }

        protected void lvSubclasses_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if(e.CommandName == "Excluir")
            {

                try
                {
                    var id = e.CommandArgument;
                    if (id != null)
                    {
                        int idSubclasse = Convert.ToInt32(id);
                        Subclass subExcluida =
                            SubclasseDAO.Remover(idSubclasse);
                        if (subExcluida != null)
                        {
                            lblMensagem.InnerText = "Subclasse " +
                                subExcluida.Descricao +
                                " excluída com sucesso!";
                            txtDescricao.Text = "";
                            DDLClasse.SelectedIndex = 0;
                            btnConfirmar.Text = "Cadastrar";
                            PopularLVs();
                        }
                    }
                }
                catch (Exception)
                {
                    lblMensagem.InnerText = "Erro na exclusão (Provavelmente essa subclasse está em uso)!";
                }

            }
            else if (e.CommandName == "Visualizar")
            {
                var id = e.CommandArgument;
                if (id != null)
                {
                    Response.Redirect("~/SubClasses?id=" + id + "&edit=false");
                }
            }
            else if (e.CommandName == "Editar")
            {
                var id = e.CommandArgument;
                if (id != null)
                {
                    Response.Redirect("~/SubClasses?id=" + id + "&edit=true");
                }
            }
        }
    }
}