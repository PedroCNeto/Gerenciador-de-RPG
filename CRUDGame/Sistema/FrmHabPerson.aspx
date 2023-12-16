<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmHabPerson.aspx.cs" Inherits="CRUDGame.FrmHabPerson" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Habilidade Personagem</title>
    <link href="../css/styleGerenciar.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <h1>Habilidade Personagem</h1>
    <form id="form1" runat="server">
<div>
            <p><a href="~/Inicio" runat="server" class="border">Início</a></p>
            <p><a href="~/HabPersons" runat="server" class="border">Voltar a Cadastrar</a></p>
        </div>

        <fieldset>
            <legend>
                Combinar personagens e habilidades
            </legend>

            <p>
                <label>Habilidade: </label>
                <asp:DropDownList runat="server" ID="DDLHabilidade" class="classBorder">
                </asp:DropDownList>
            </p>
            
            <p>
                <label>Personagem: </label>
                <asp:DropDownList runat="server" ID="DDLPersonagens" class="classBorder">
                </asp:DropDownList>
            </p>

            <p>
                <asp:Button Text="Cadastrar" runat="server" ID="btnConfirmar" OnClick="btnConfirmar_Click" class="classBorder"/>
            </p>
            <p>
                <label id="lblMensagem" runat="server"></label>
            </p>
        </fieldset>
        <h2>Classes cadastradas</h2>

        <table border="1" class="tabela">
            <tr>
                <th>Código</th>
                <th>Personagem</th>
                <th>Habilidade</th>
                <th>Ações</th>
            </tr>

            <asp:ListView runat="server" ID="lvHabPersons" OnItemCommand="lvHabPersons_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("IdPersonHab") %>
                        </td>
                        <td>
                            <%# Eval("GetPersonagem.NomePersonagem") %>
                        </td>
                        <td>
                            <%# Eval("GetHabilidade.Descricao") %>
                        </td>

                        <td>
                            <asp:ImageButton ID="btnVisualizar"
                                runat="server"
                                ImageUrl="../img/view.svg" 
                                CommandName="Visualizar"
                                CommandArgument='<%# Eval("IdPersonHab") %>'
                                />
                            <asp:ImageButton ID="btnEditar" 
                                runat="server" 
                                ImageUrl="../img/edit.svg" 
                                CommandName="Editar"
                                CommandArgument='<%# Eval("IdPersonHab") %>'
                                />
                            <asp:ImageButton ID="btnDeletar"
                                runat="server"
                                ImageUrl="../img/delete.svg"
                                CommandName="Excluir"
                                CommandArgument='<%# Eval("IdPersonHab") %>' 
                                OnClientClick="return confirm('Deseja realmente excluir essa habilidade?');"
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
