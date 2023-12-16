using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUDGame
{
    public partial class FrmHabPerson : System.Web.UI.Page
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
                List<Personagen> personagems = PersonagemDAO.ListarPersonagens();
                List<Habilidade> habilidades = HabilidadeDAO.ListarHabilidades();
                PreencherDDLPersonagens(personagems);
                PreencherDDLHabilidades(habilidades);

                PopularLVs();
            }
        }

        private void PreencherDDLHabilidades(List<Habilidade> habilidades)
        {
            DDLHabilidade.DataSource = habilidades;
            DDLHabilidade.DataTextField = "Descricao";
            DDLHabilidade.DataValueField = "IdHabilidade";
            DDLHabilidade.DataBind();
            DDLHabilidade.Items.Insert(0, "Selecione..");
        }

        private void PreencherDDLPersonagens(List<Personagen> personagems)
        {
            DDLPersonagens.DataSource = personagems;
            DDLPersonagens.DataTextField = "NomePersonagem";
            DDLPersonagens.DataValueField = "IdPersonagem";
            DDLPersonagens.DataBind();
            DDLPersonagens.Items.Insert(0, "Selecione..");
        }

        private void PopularLVs()
        {
            var habspern = HabilidadesPersonagensDAO.ListarCombinacoes();
            PopularLVClasses(habspern);
        }

        private void PopularLVClasses(object habilidades)
        {
            lvHabPersons.DataSource = habilidades;
            lvHabPersons.DataBind();
        }

        private void PreencherDados(int id, bool edit)
        {
            var hp = HabilidadesPersonagensDAO.ListarCombinacoes(id);

            DDLHabilidade.SelectedValue = Convert.ToString(hp.Habilidades_IdHabilidade);
            DDLPersonagens.SelectedValue = Convert.ToString(hp.Personagens_IdPersonagem);

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
                DDLHabilidade.Enabled = false;
                DDLPersonagens.Enabled = false;
            }
        }

        protected void lvHabPersons_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                var id = e.CommandArgument;
                if (id != null)
                {
                    int idHabsPerson = Convert.ToInt32(id);
                    Personagem_Habilidade hpExcluida = HabilidadesPersonagensDAO.Remover(idHabsPerson);
                    if (hpExcluida != null)
                    {
                        lblMensagem.InnerText = "Combinação excluida com sucesso!";
                        btnConfirmar.Text = "Cadastrar";
                        DDLHabilidade.SelectedIndex = 0;
                        DDLPersonagens.SelectedIndex = 0;
                        PopularLVs();
                    }
                }
            }
            else if (e.CommandName == "Visualizar")
            {
                var id = e.CommandArgument;
                if (id != null)
                {
                    Response.Redirect("~/HabPersons?id=" + id + "&edit=false");
                }
            }
            else if (e.CommandName == "Editar")
            {
                var id = e.CommandArgument;
                if (id != null)
                {
                    Response.Redirect("~/HabPersons?id=" + id + "&edit=true");
                }
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {

            int index = DDLPersonagens.SelectedIndex;
            int index2 = DDLHabilidade.SelectedIndex;

            Personagen personagem = null;
            Habilidade habilidade = null;

            if (index != 0 && index2 != 0)
            {
                int Habilidade = Convert.ToInt32(DDLHabilidade.SelectedValue);
                int Personagem = Convert.ToInt32(DDLPersonagens.SelectedValue);
                personagem = PersonagemDAO.ListarPersonagens(Personagem);
                habilidade = HabilidadeDAO.ListarHabilidades(Habilidade);
            }
          
            var cadastrando = btnConfirmar.Text == "Cadastrar";



            if (index == 0 || index2 == 0)
            {
                lblMensagem.InnerText =
                    "Você precisa selecionar!";
            }






                else if (cadastrando)
                {
                Personagem_Habilidade novaPersonHab = null;
                //Caso id seja -1, não existe uma classe para ser alterada
                int id = -1;
                //Criando uma instância da classe (objeto)
                novaPersonHab = new Personagem_Habilidade();
                //Preencher o objeto
                    novaPersonHab.Habilidades_IdHabilidade = habilidade.IdHabilidade;
                    novaPersonHab.Personagens_IdPersonagem = personagem.IdPersonagem;

                    string mensagem = HabilidadesPersonagensDAO.CadastrarHabsPerson(novaPersonHab);
                    //Limpando o campo de texto
                    DDLHabilidade.SelectedIndex = 0;
                    DDLPersonagens.SelectedIndex = 0;

                    lblMensagem.InnerText = mensagem;
                    PopularLVs();
                }
                else
                {
                Personagem_Habilidade novaPersonHab = null;
                //Caso id seja -1, não existe uma classe para ser alterada
                int id = -1;
                var idQuery = Request.QueryString["id"];
                    if (idQuery != null)
                    {
                        novaPersonHab = new Personagem_Habilidade();
                        id = Convert.ToInt32(idQuery);
                        novaPersonHab = HabilidadesPersonagensDAO.ListarCombinacoes(id);
                        novaPersonHab.Habilidades_IdHabilidade = Convert.ToInt32(DDLHabilidade.SelectedValue);
                        novaPersonHab.Personagens_IdPersonagem = Convert.ToInt32(DDLPersonagens.SelectedValue);
                        string mensagem = HabilidadesPersonagensDAO.AlterarHabsPerson(novaPersonHab);
                        
                        btnConfirmar.Text = "Cadastrar";
                        lblMensagem.InnerText = mensagem;
                        DDLHabilidade.SelectedIndex = 0;
                        DDLPersonagens.SelectedIndex = 0;
                    PopularLVs();
                    } 
            }

        }
    }
}