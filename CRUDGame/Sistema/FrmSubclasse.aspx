<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmSubclasse.aspx.cs" Inherits="CRUDGame.FrmSubclasse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gerenciar Subclasses</title>
    <link href="../css/styleGerenciar.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
    <h1>Gerenciar Subclasses</h1>
    <form id="form1" runat="server">
        <div>
            <p><a href="~/Inicio" runat="server" class="border">Início</a></p>
            <p><a href="~/SubClasses" runat="server" class="border">Voltar a Cadastrar</a></p>
        </div>

        <fieldset>
            <legend>Criar nova Subclasse
            </legend>

            <p>
                <label>Subclasse:</label>
                <asp:TextBox runat="server" ID="txtDescricao" class="classBorder"/>
            </p>
            <p>
                <label runat="server" id="classeSub" >Selecione uma classe: </label>
                <asp:DropDownList runat="server" ID="DDLClasse" class="classBorder">
                </asp:DropDownList >
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

   
        <h2>SubClasses cadastradas</h2>

        <table border="1" class="tabela">
            <tr>
                <th>Código</th>
                <th>Descrição</th>
                <th>Classe</th>
                <th>Ações</th>
            </tr>

            <asp:ListView runat="server" ID="lvSubclasses" OnItemCommand="lvSubclasses_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("IdSubclasse") %></td>
                        <td><%# Eval("Descricao") %></td>
                        <td><%# Eval("GetClasse.Descricao") %></td>
                        <td>
                            <asp:ImageButton ID="btnVisualizar"
                                runat="server"
                                ImageUrl="../img/view.svg" 
                                CommandName="Visualizar"
                                CommandArgument='<%# Eval("IdSubclasse") %>'
                                />
                            <asp:ImageButton ID="btnEditar" 
                                runat="server" 
                                ImageUrl="../img/edit.svg" 
                                CommandName="Editar"
                                CommandArgument='<%# Eval("IdSubclasse") %>'
                                />
                            <asp:ImageButton ID="btnDeletar"
                                runat="server"
                                ImageUrl="../img/delete.svg"
                                CommandName="Excluir"
                                CommandArgument='<%# Eval("IdSubclasse") %>' 
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
