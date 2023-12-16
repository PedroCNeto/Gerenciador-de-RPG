<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CRUDGame.Sistema.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Página Inicial</title>
    <link href="../css/stylePaginaInicial.css" rel="stylesheet" />
    

</head>
<body>
    <div class="container">
    <h1>Página Inicial</h1>
    <form id="form2" runat="server">
        <div class="buttons">
            <p><a href="~/Classes" runat="server">Gerenciar Classes</a></p>
            <p><a runat="server" href="~/Racas">Gerenciar Raças</a></p>
            <p><a runat="server" href="~/Habilidades">Gerenciar Habilidades</a></p>
            <p><a runat="server" href="~/Subclasses">Gerenciar Subclasses</a></p>
            <p><a href="~/Personagens" runat="server">Gerenciar Personagens</a></p>
            <p><a href="~/HabPersons" runat="server">Gerenciar habilidades dos personagens</a></p>
            <p><a href="~/Cores" runat="server">Gerenciar cores</a></p>
           
            <asp:LoginStatus ID="LoginStatus1" CssClass="login" runat="server" LoginText="Entrar" LogoutText="Sair" />
            
        </div>
    </form>
        </div>
</body>
</html>

