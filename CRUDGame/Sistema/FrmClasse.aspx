<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmClasse.aspx.cs" Inherits="CRUDGame.Sistema.FrmClasse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gerenciamento de Classes</title>
    <link href="../css/styleGerenciar.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        
    <h1>Gerenciar Classes</h1>
    <form id="form1" runat="server">
        <div>
            <p><a href="~/Inicio" runat="server" class="border">Início</a></p>
             <p><a href="~/Classes" runat="server" class="border">Voltar a Cadastrar</a></p>
        </div>

        <fieldset>
            <legend>Criar nova Classe
            </legend>

            <p>
                <label>Nome da Classe:</label>
                <asp:TextBox runat="server" ID="txtDescricao" class="classBorder"/>
            </p>
            <p>
                <asp:Button Text="Cadastrar"
                    runat="server"
                    ID="btnConfirmar" OnClick="btnConfirmar_Click" class="classBorder" />
            </p>
            <p>
                <label id="lblMensagem" runat="server"></label>
            </p>
        </fieldset>

        <h2>Classes cadastradas</h2>

        <div class="containerTable">
        <table border="1" class="tabela">
            <tr>
                <th>Código</th>
                <th>Descrição</th>
                <th>Ações</th>
            </tr>

            <asp:ListView runat="server" ID="lvClasses" OnItemCommand="lvClasses_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("IdClasse") %>
                        </td>
                        <td>
                            <%# Eval("Descricao") %>
                        </td>
                        <td>
                            <asp:ImageButton ID="btnVisualizar"
                                runat="server"
                                ImageUrl="../img/view.svg" 
                                CommandName="Visualizar"
                                CommandArgument='<%# Eval("IdClasse") %>'
                                />
                            <asp:ImageButton ID="btnEditar" 
                                runat="server" 
                                ImageUrl="../img/edit.svg" 
                                CommandName="Editar"
                                CommandArgument='<%# Eval("IdClasse") %>'
                                />
                            <asp:ImageButton ID="btnDeletar"
                                runat="server"
                                ImageUrl="../img/delete.svg"
                                CommandName="Excluir"
                                CommandArgument='<%# Eval("IdClasse") %>' 
                                OnClientClick="return confirm('Deseja realmente excluir essa classe?');"
                                />
                        </td>
                    </tr>

                </ItemTemplate>
            </asp:ListView>
        </table>
            </div>
    </form>
            </div>

</body>
</html>
