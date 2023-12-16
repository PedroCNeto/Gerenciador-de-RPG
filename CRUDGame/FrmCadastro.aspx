<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmCadastro.aspx.cs" Inherits="CRUDGame.FrmCadastro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Gerenciar Usuário</title>
    <link href="css/styleCadastro.css" rel="stylesheet" />
</head>
<body>

    
    <form id="form1" runat="server">
        <div class="container">
            <a href="Default.aspx" id="buttonVoltar">Voltar</a>
            <h1>Gerenciar Usuário</h1>
            <label>Nome do usuário:</label>
            <p>
                <input type="text" required name="name" runat="server" id="txtNomeUsuario" class="border"/>
            </p>

            <label>Perfil do usuário:</label>
            <p>
                <asp:DropDownList runat="server" ID="ddlPerfilUsuario" class="border">
                </asp:DropDownList>
            </p>

            <label>Data de Nascimento:</label>
            <p>
                <input type="date" required name="name" runat="server" id="txtDataNasc" class="border"/>
            </p>

            <label>Login:</label>
            <p>
                <input type="text" required name="name" runat="server" id="txtLogin" class="border"/>
            </p>

            <label>Senha:</label>
            <p>
                <input type="password" required name="name" runat="server" id="txtSenha" class="border"/>
            </p>

            <label>Repita a senha:</label>
            <p>
                <input type="password" required name="name" runat="server" id="txtRepetirSenha" class="border"/>
            </p>
                        
            <p>
                <asp:Button Text="Cadastrar" 
                    runat="server" 
                    ID="btnCadastrar" 
                    OnClick="btnCadastrar_Click" class="border"/>
            </p>

            <p>
                <label id="lblMensagem" runat="server"></label>
            </p>
        </div>
    </form>
</body>
</html>
