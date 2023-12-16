<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmCores.aspx.cs" Inherits="CRUDGame.FrmCores" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gerenciamento de Cores</title>
    <link href="../css/styleGerenciar.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
    <h1>Gerenciar Cores</h1>
    <form id="form1" runat="server">
        <div>
            <p><a href="~/Inicio" runat="server" class="border">Início</a></p>
            <p><a href="~/Cores" runat="server" class="border">Voltar a Cadastrar</a></p>
        </div>

        <fieldset>
            <legend>Criar nova Cor
            </legend>

            <p>
                <label>Nome da Cor:</label>
                <asp:TextBox runat="server" ID="txtDescricao"  class="classBorder"/>
            </p>
            <p>
                <asp:Button Text="Cadastrar"
                    runat="server"
                    ID="btnConfirmar" OnClick="btnConfirmar_Click"  class="classBorder"/>
            </p>
            <p>
                <label id="lblMensagem" runat="server"></label>
            </p>
        </fieldset>

        <h2>Cores cadastradas</h2>

        <table border="1" class="tabela">
            <tr>
                <th>Código</th>
                <th>Descrição</th>
                <th>Ações</th>
            </tr>

            <asp:ListView runat="server" ID="lvCores" OnItemCommand="lvCores_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("IdCor") %>
                        </td>
                        <td>
                            <%# Eval("Descricao") %>
                        </td>
                        <td>
                            <asp:ImageButton ID="btnVisualizar"
                                runat="server"
                                ImageUrl="../img/view.svg" 
                                CommandName="Visualizar"
                                CommandArgument='<%# Eval("IdCor") %>'
                                />
                            <asp:ImageButton ID="btnEditar" 
                                runat="server" 
                                ImageUrl="../img/edit.svg" 
                                CommandName="Editar"
                                CommandArgument='<%# Eval("IdCor") %>'
                                />
                            <asp:ImageButton ID="btnDeletar"
                                runat="server"
                                ImageUrl="../img/delete.svg"
                                CommandName="Excluir"
                                CommandArgument='<%# Eval("IdCor") %>' 
                                OnClientClick="return confirm('Deseja realmente excluir essa cor?');"
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
