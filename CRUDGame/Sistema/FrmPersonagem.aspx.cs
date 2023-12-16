using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUDGame
{
    public partial class FrmPersonagem : System.Web.UI.Page
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
                    PopularDDLs();
            }

        }
  

        private void PopularLVs()
        {
            var personagems = PersonagemDAO.ListarPersonagens();
            PopularLVClasses(personagems);
        }

        private void PopularLVClasses(object personagens)
        {
            lvPersonagens.DataSource = personagens;
            lvPersonagens.DataBind();
        }

        private void PreencherDados(int id, bool edit)
        {
            var personagem = PersonagemDAO.ListarPersonagens(id);
            txtNome.Text = personagem.NomePersonagem;
            txtAltura.Text = Convert.ToString(personagem.Altura);
            txtCarisma.Text = Convert.ToString(personagem.Carisma);
            txtConstituicao.Text = Convert.ToString(personagem.Constituicao);
            txtDataNasc.Text = DateTime.Parse(personagem.DataNasc.ToString()).ToString("yyyy-MM-dd");
            txtDestreza.Text = Convert.ToString(personagem.Destreza);
            txtEstiloCab.Text = personagem.EstiloCabelo;
            txtForca.Text = Convert.ToString(personagem.Forca);
            txtInteligencia.Text = Convert.ToString(personagem.Inteligencia);
            txtNivel.Text = Convert.ToString(personagem.Nivel);
            txtPeso.Text = Convert.ToString(personagem.Peso);
            txtSabedoria.Text = Convert.ToString(personagem.Sabedoria);
            txtSexo.Text = personagem.Sexo;
            ddlCoresCab.SelectedValue = Convert.ToString(personagem.CorCabelo);
            ddlCoresOlhos.SelectedValue = Convert.ToString(personagem.CorOlho);
            ddlCoresPele.SelectedValue = Convert.ToString(personagem.CorPele);
            ddlRaca.SelectedValue = Convert.ToString(personagem.RacaID);
            ddlSubclasse.SelectedValue = Convert.ToString(personagem.SubclasseID);

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
                txtNome.Enabled = false;
                txtAltura.Enabled = false;
                txtCarisma.Enabled = false;
                txtConstituicao.Enabled = false;
                txtDataNasc.Enabled = false;
                txtDestreza.Enabled = false;
                txtEstiloCab.Enabled = false;
                txtForca.Enabled = false;
                txtInteligencia.Enabled = false;
                txtNivel.Enabled = false;
                txtPeso.Enabled = false;
                txtSabedoria.Enabled = false;
                txtSexo.Enabled = false;
                ddlCoresCab.Enabled = false;
                ddlCoresOlhos.Enabled = false;
                ddlCoresPele.Enabled = false;
                ddlRaca.Enabled = false;
                ddlSubclasse.Enabled = false;
            }
        }

        private void PopularDDLs()
        {
            try
            {
                List<Raca> racas = RacaDAO.ListarRacas();
                List<Subclass> subClasses = SubclasseDAO.ListarSubclasses();
                List<Core> cores = CoresDAO.ListarCores();
                PopularDDLRaca(racas);
                PopularDDlSubclasse(subClasses);
                PopularDDLCores(cores);

            }
            catch (Exception ex)
            {
                lblMensagem.InnerText = ex.Message;
            }
        }



        private void PopularDDlSubclasse(List<Subclass> subClasses)
        {
            ddlSubclasse.DataSource = subClasses;
            ddlSubclasse.DataTextField = "Descricao";
            ddlSubclasse.DataValueField = "IdSubclasse";
            ddlSubclasse.DataBind();
            ddlSubclasse.Items.Insert(0, "Selecione..");

        }

        private void PopularDDLRaca(List<Raca> racas)
        {
            ddlRaca.DataSource = racas;
            ddlRaca.DataTextField = "Descricao";
            ddlRaca.DataValueField = "IdRaca";
            ddlRaca.DataBind();
            ddlRaca.Items.Insert(0, "Selecione..");
        }

        private void PopularDDLCores(List<Core> cores) {
            ddlCoresCab.DataSource = cores;
            ddlCoresCab.DataTextField = "Descricao";
            ddlCoresCab.DataValueField = "IdCor";
            ddlCoresCab.DataBind();
            ddlCoresCab.Items.Insert(0, "Selecione..");

            ddlCoresOlhos.DataSource = cores;
            ddlCoresOlhos.DataTextField = "Descricao";
            ddlCoresOlhos.DataValueField = "IdCor";
            ddlCoresOlhos.DataBind();
            ddlCoresOlhos.Items.Insert(0, "Selecione..");

            ddlCoresPele.DataSource = cores;
            ddlCoresPele.DataTextField = "Descricao";
            ddlCoresPele.DataValueField = "IdCor";
            ddlCoresPele.DataBind();
            ddlCoresPele.Items.Insert(0, "Selecione..");

        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            string descricao = txtNome.Text;
            int index1 = ddlCoresCab.SelectedIndex;
            int index2 = ddlCoresOlhos.SelectedIndex;
            int index3 = ddlCoresPele.SelectedIndex;
            int index4 = ddlRaca.SelectedIndex;
            int index5 = ddlSubclasse.SelectedIndex;

            var cadastrando = btnConfirmar.Text == "Cadastrar";
            if (index1 == 0 || index2 == 0 || index3 == 0 || index4 == 0 || index5 == 0)
            {
                lblMensagem.InnerText = "Você precisa fazer a selecao!";
            }
            else if (descricao != "")
            {

                Personagen novoPersonagem = null;
                //Caso id seja -1, não existe uma classe para ser alterada
                int id = -1;



                if (cadastrando)
                { 
                    try
                    {
                        //Criando uma instância da classe (objeto)
                        novoPersonagem = new Personagen();
                        //Preencher o objeto
                        novoPersonagem.NomePersonagem = descricao;

                        //Capturando o id da Classe dessa subclasse
                        int subclasse = Convert.ToInt32(
                            ddlSubclasse.SelectedValue);
                        novoPersonagem.SubclasseID = subclasse;

                        int raca = Convert.ToInt32(
                            ddlRaca.SelectedValue);
                        novoPersonagem.RacaID = raca;

                        int corOlhos = Convert.ToInt32(
                            ddlCoresOlhos.SelectedValue);
                        novoPersonagem.CorOlho = corOlhos;

                        int corPele = Convert.ToInt32(
                            ddlCoresPele.SelectedValue);
                        novoPersonagem.CorPele = corPele;

                        int corCabelo = Convert.ToInt32(
                            ddlCoresCab.SelectedValue);
                        novoPersonagem.CorCabelo = corCabelo;

                        novoPersonagem.Forca = Convert.ToInt32(txtForca.Text);
                        novoPersonagem.Destreza = Convert.ToInt32(txtDestreza.Text);
                        novoPersonagem.Sabedoria = Convert.ToInt32(txtSabedoria.Text);
                        novoPersonagem.Constituicao = Convert.ToInt32(txtConstituicao.Text);
                        novoPersonagem.Inteligencia = Convert.ToInt32(txtInteligencia.Text);
                        novoPersonagem.Carisma = Convert.ToInt32(txtCarisma.Text);
                        novoPersonagem.Peso = Convert.ToDecimal(txtPeso.Text);
                        novoPersonagem.Altura = Convert.ToDecimal(txtAltura.Text);
                        novoPersonagem.DataNasc = Convert.ToDateTime(txtDataNasc.Text);
                        novoPersonagem.Nivel = Convert.ToInt32(txtNivel.Text);
                        novoPersonagem.Sexo = txtSexo.Text;
                        novoPersonagem.EstiloCabelo = txtEstiloCab.Text;


                        string mensagem = PersonagemDAO.CadastrarPersonagem(novoPersonagem);
                        //Limpando o campo de texto

                        lblMensagem.InnerText = mensagem;
                        PopularLVs();
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.InnerText = "Ocorreu um erro!";
                    }
                }
                else
                {
                    var idQuery = Request.QueryString["id"];
                    if (idQuery != null)
                    {
                        novoPersonagem = new Personagen();
                        id = Convert.ToInt32(idQuery);
                        novoPersonagem = PersonagemDAO.ListarPersonagens(id);
                        novoPersonagem.NomePersonagem = descricao;
                        //Capturando o id da Classe dessa subclasse
                        int subclasse = Convert.ToInt32(
                            ddlSubclasse.SelectedValue);
                        novoPersonagem.SubclasseID = subclasse;

                        int raca = Convert.ToInt32(
                            ddlRaca.SelectedValue);
                        novoPersonagem.RacaID = raca;

                        int corOlhos = Convert.ToInt32(
                            ddlCoresOlhos.SelectedValue);
                        novoPersonagem.CorOlho = corOlhos;

                        int corPele = Convert.ToInt32(
                            ddlCoresPele.SelectedValue);
                        novoPersonagem.CorPele = corPele;

                        int corCabelo = Convert.ToInt32(
                            ddlCoresCab.SelectedValue);
                        novoPersonagem.CorCabelo = corCabelo;

                        novoPersonagem.Forca = Convert.ToInt32(txtForca.Text);
                        novoPersonagem.Destreza = Convert.ToInt32(txtDestreza.Text);
                        novoPersonagem.Sabedoria = Convert.ToInt32(txtSabedoria.Text);
                        novoPersonagem.Constituicao = Convert.ToInt32(txtConstituicao.Text);
                        novoPersonagem.Inteligencia = Convert.ToInt32(txtInteligencia.Text);
                        novoPersonagem.Carisma = Convert.ToInt32(txtCarisma.Text);
                        novoPersonagem.Peso = Convert.ToDecimal(txtPeso.Text);
                        novoPersonagem.Altura = Convert.ToDecimal(txtAltura.Text);
                        novoPersonagem.DataNasc = Convert.ToDateTime(txtDataNasc.Text);
                        novoPersonagem.Nivel = Convert.ToInt32(txtNivel.Text);
                        novoPersonagem.Sexo = txtSexo.Text;
                        novoPersonagem.EstiloCabelo = txtEstiloCab.Text;
                        
                        string mensagem = PersonagemDAO.AlterarPersonagem(novoPersonagem);

                        btnConfirmar.Text = "Cadastrar";
                        lblMensagem.InnerText = mensagem;
                        PopularLVs();
                    }
                }
                txtNome.Text = "";
                txtAltura.Text = "";
                txtCarisma.Text = "";
                txtConstituicao.Text = "";
                txtDataNasc.Text = "";
                txtDestreza.Text = "";
                txtEstiloCab.Text = "";
                txtForca.Text = "";
                txtInteligencia.Text = "";
                txtNivel.Text = "";
                txtPeso.Text = "";
                txtSabedoria.Text = "";
                txtSexo.Text = "";
                ddlCoresCab.SelectedIndex = 0;
                ddlCoresOlhos.SelectedIndex = 0;
                ddlCoresPele.SelectedIndex = 0;
                ddlRaca.SelectedIndex = 0;
                ddlSubclasse.SelectedIndex = 0;
            }
        }

        protected void lvPersonagens_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                var id = e.CommandArgument;
                if (id != null)
                {
                    int idPersonagem = Convert.ToInt32(id);
                    Personagen personagemExc =
                        PersonagemDAO.Remover(idPersonagem);
                    if (personagemExc != null)
                    {
                        lblMensagem.InnerText = "Personagem " +
                            personagemExc.NomePersonagem +
                            " excluída com sucesso!";
                        btnConfirmar.Text = "Cadastrar";
                        txtNome.Text = "";
                        txtAltura.Text = "";
                        txtCarisma.Text = "";
                        txtConstituicao.Text = "";
                        txtDataNasc.Text = "";
                        txtDestreza.Text = "";
                        txtEstiloCab.Text = "";
                        txtForca.Text = "";
                        txtInteligencia.Text = "";
                        txtNivel.Text = "";
                        txtPeso.Text = "";
                        txtSabedoria.Text = "";
                        txtSexo.Text = "";
                        ddlCoresCab.SelectedIndex = 0;
                        ddlCoresOlhos.SelectedIndex = 0;
                        ddlCoresPele.SelectedIndex = 0;
                        ddlRaca.SelectedIndex = 0;
                        ddlSubclasse.SelectedIndex = 0;
                        PopularLVs();
                    }
                }
            }
            else if (e.CommandName == "Visualizar")
            {
                var id = e.CommandArgument;
                if (id != null)
                {
                    Response.Redirect("~/Personagens?id=" + id + "&edit=false");
                }
            }
            else if (e.CommandName == "Editar")
            {
                var id = e.CommandArgument;
                if (id != null)
                {
                    Response.Redirect("~/Personagens?id=" + id + "&edit=true");
                }
            }
        }
    }
}