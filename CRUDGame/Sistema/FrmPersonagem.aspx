<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmPersonagem.aspx.cs" Inherits="CRUDGame.FrmPersonagem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Gerenciar Personagens</title>
    <link href="../css/styleGerenciar.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        
    <h1>Gerenciar Personagens</h1>
    <form id="form1" runat="server">
        <div>   
            <p><a href="~/Inicio" runat="server" class="border">Início</a></p>
            <p><a href="~/Personagens" runat="server" class="border">Voltar a Cadastrar</a></p>
        </div>

        <fieldset>

            
            <legend>
                Criar novo personagem
            </legend>
            <p>
                <label>Nome do Personagem:</label>
                <asp:TextBox runat="server" id="txtNome" class="classBorder"/>
            </p>
            <p>
                <label>Data de nascimento:</label>
                <asp:TextBox runat="server" id="txtDataNasc" TextMode="Date" class="classBorder"/>
            </p>

            <p>
                <label>Nível:</label>
                <asp:TextBox runat="server" id="txtNivel" class="classBorder"/>
            </p>

            <p>
                <label>Sexo:</label>
                <asp:TextBox runat="server" id="txtSexo" class="classBorder"/>
            </p>

             <p>
                <label>Força:</label>
                <asp:TextBox runat="server" id="txtForca" class="classBorder"/>
            </p>

            <p>
                <label>Destreza:</label>
                <asp:TextBox runat="server" id="txtDestreza" class="classBorder"/>
            </p>

            <p>
                <label>Sabedoria:</label>
                <asp:TextBox runat="server" id="txtSabedoria" class="classBorder"/>
            </p>

            <p>
                <label>Constituicao:</label>
                <asp:TextBox runat="server" id="txtConstituicao" class="classBorder"/>
            </p>

            <p>
                <label>Inteligencia:</label>
                <asp:TextBox runat="server" id="txtInteligencia" class="classBorder"/>
            </p>

            <p>
                <label>Carisma:</label>
                <asp:TextBox runat="server" id="txtCarisma" class="classBorder"/>
            </p>

            <p>
                <label>Peso:</label>
                <asp:TextBox runat="server" id="txtPeso" class="classBorder"/>
            </p>

            <p>
                <label>Altura:</label>
                <asp:TextBox runat="server" id="txtAltura" class="classBorder"/>
            </p>

            <p>
                <label>Cor do cabelo:</label>
                <asp:DropDownList runat="server" ID="ddlCoresCab" class="classBorder">
                </asp:DropDownList>
            </p>

            <p>
                <label>Estilo do Cabelo:</label>
                <asp:TextBox runat="server" id="txtEstiloCab" class="classBorder"/>
            </p>

            <p>
                <label>Cor dos olhos:</label>
                <asp:DropDownList runat="server" ID="ddlCoresOlhos" class="classBorder">
                </asp:DropDownList>
            </p>

            <p>
                <label>Cor da pele:</label>
                <asp:DropDownList runat="server" ID="ddlCoresPele" class="classBorder">
                </asp:DropDownList>
            </p>

            <p>
                <label>Raça:</label>
                <asp:DropDownList runat="server" ID="ddlRaca" class="classBorder">
                </asp:DropDownList>
            </p>

            <p>
                <label>Subclasse:</label>
                <asp:DropDownList runat="server" ID="ddlSubclasse" class="classBorder">
                </asp:DropDownList>
            </p>

            <p>
                <asp:Button Text="Cadastrar" 
                    runat="server" 
                    ID="btnConfirmar" OnClick="btnConfirmar_Click" class="classBorder"/>
            </p>
            <p>
                <label id="lblMensagem" runat="server"></label>
            </p>

        </fieldset>
        <h2>Personagens Cadastrados</h2>
                <table border="1" class="tabela">
            <tr>
                <th>Código</th>
                <th>Nome</th>
                <th>Nível</th>
                <th>Ações</th>
            </tr>

            <asp:ListView runat="server" ID="lvPersonagens" OnItemCommand="lvPersonagens_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("IdPersonagem") %>
                        </td>
                        <td>
                            <%# Eval("NomePersonagem") %>
                        </td>
                        <td>
                            <%# Eval("Nivel") %>
                        </td>
                        <td>
                            <asp:ImageButton ID="btnVisualizar"
                                runat="server"
                                ImageUrl="../img/view.svg" 
                                CommandName="Visualizar"
                                CommandArgument='<%# Eval("IdPersonagem") %>'
                                />
                            <asp:ImageButton ID="btnEditar" 
                                runat="server" 
                                ImageUrl="../img/edit.svg" 
                                CommandName="Editar"
                                CommandArgument='<%# Eval("IdPersonagem") %>'
                                />
                            <asp:ImageButton ID="btnDeletar"
                                runat="server"
                                ImageUrl="../img/delete.svg"
                                CommandName="Excluir"
                                CommandArgument='<%# Eval("IdPersonagem") %>' 
                                OnClientClick="return confirm('Deseja realmente excluir essa classe?');"
                                />
                        </td>
                    </tr>

                </ItemTemplate>
            </asp:ListView>
        </table>

    </form>
        </div>
</body>
</html>
