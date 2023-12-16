<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CRUDGame.Default" %>

<!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Fomulário de Login</title>
        <link href="../css/styleDefault.css" rel="stylesheet" />
    </head>
    <body>
    
    
    <form id="form1" runat="server">
        

        <div class="container">
            <div class="fundo">
        <asp:LoginStatus ID="LoginStatus1" CssClass="login" runat="server" LoginText="Entrar" LogoutText="Sair" />
        <h1>Sistema de Acesso</h1>
        <div>
            <label>Usuário:</label>
            <p>
                <input type="text" name="name" runat="server" id="txtUsuario" required />
            </p>
            <label>Senha:</label>
            <p>
                <input type="password" name="name" runat="server" id="txtSenha" required />
            </p>
            <p>
                <asp:Button Text="Entrar" 
                    runat="server" 
                    id="btnEntrar"
                    OnClick="btnEntrar_Click"
                    />
                <p>Não possui conta? <a href="FrmCadastro.aspx" id="btnCliqueAqui" >Clique Aqui</a></p>
                <p id="espacoBottom"></p>
            </p>
            <p>
                <label id="lblMensagem" runat="server"></label>
            </p>
                </div>
            </div>
            
        </div>
    </form>
</body>
</html>
