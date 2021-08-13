<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuariosFrm.aspx.cs" Inherits="WebApplicationCMS.UsuariosFrm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="PnlDatosUsuario" runat="server">
                <asp:Label ID="Label1" runat="server" Text="Nombres:"></asp:Label>
                <asp:Label ID="LblUsuario" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:Label ID="Label2" runat="server" Text="Nombres Completos:"></asp:Label>
                <asp:Label ID="LblNombresCompletos" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:Label ID="Label3" runat="server" Text="Dirección:"></asp:Label>
                <asp:Label ID="LblDireccion" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:Label ID="Label4" runat="server" Text="Teléfono:"></asp:Label>
                <asp:Label ID="LblTelefono" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:Label ID="Label5" runat="server" Text="Email:"></asp:Label>
                <asp:Label ID="LblEmail" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:Label ID="Label6" runat="server" Text="Edad:"></asp:Label>
                <asp:Label ID="LblEdad" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:Label ID="Label7" runat="server" Text="Rol:"></asp:Label>
                <asp:Label ID="LblRolNombre" runat="server" Text="Label"></asp:Label>
            </asp:Panel>
            <br />
            <asp:Panel ID="PnlMensajeBienvenida" runat="server" Visible="false">
                <asp:Label ID="LblMensaje" runat="server" Text="Hola Bienvenido"></asp:Label>
            </asp:Panel>
            <br />
            <asp:Panel ID="PnlNoticias" runat="server" Visible="false">
                <asp:Label ID="Label8" runat="server" Text="Noticias:"></asp:Label>
                <asp:Label ID="LblNoticias" runat="server" Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."></asp:Label>
            </asp:Panel>
            <br />
            <asp:Panel ID="PnlListar" runat="server" Visible="false">
                <asp:Button ID="BtnCrearUsuario" runat="server" Text="Crear Usuario" Visible="false" OnClick="BtnCrearUsuario_Click" />
                <asp:Panel ID="PnlEditar" runat="server" Visible="false">
                    <asp:Label ID="LblTituloEditar" runat="server" Font-Bold="True"></asp:Label>
                    <br />
                    <asp:Table ID="Table1" runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label13" runat="server" Text="Nombre"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="Label12" runat="server" Text="Nombres Compretos"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="Label14" runat="server" Text="Dirección"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="Label15" runat="server" Text="Teléfono"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="Label16" runat="server" Text="Email"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="Label17" runat="server" Text="Edad"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="Label18" runat="server" Text="Rol"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="TBNombreEdit" runat="server" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                <asp:CustomValidator ID="cv_name" runat="server" ControlToValidate="TBNombreEdit"
                                    Text="Required" ValidateEmptyText="true" ErrorMessage="Debe ingresar un nombre"
                                    ValidationGroup="validate" ForeColor="Red"></asp:CustomValidator>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="TBNombresCompletosEdit" runat="server" MaxLength="200"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="TBDireccionEdit" runat="server" MaxLength="200"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="TBTelefonoEdit" runat="server" MaxLength="50"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="TBEmailEdit" runat="server" MaxLength="50"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="TBEmailEdit" runat="server" ErrorMessage="Email no valido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="TBEdadEdit" runat="server" MaxLength="2"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="TBEdadEdit" runat="server" ErrorMessage="Solo se permiten números" ValidationExpression="\d+"> </asp:RegularExpressionValidator>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="LstRolEdit" runat="server"></asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="BtnGuardarEdit" runat="server" Text="Guardar" OnClick="BtnGuardarEdit_Click" />
                                <asp:Button ID="BtnCancelarEdit" runat="server" Text="Cancelar" OnClick="BtnCancelarEdit_Click" />
                            </asp:TableCell>
                            <asp:TableCell>
                                 
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    

                </asp:Panel>
                 <br />
                <asp:Label ID="LblMensajeEdit" runat="server" BackColor="#6699FF"></asp:Label>
                <br /><br />
                <asp:Panel ID="PnlFiltro" runat="server">
                    <asp:Label ID="Label9" runat="server" Text="Filtrar" Font-Bold="True"></asp:Label>
                    <br />
                    <asp:Label ID="Label10" runat="server" Text="Nombre: "></asp:Label>
                    <asp:TextBox ID="TBNombre" runat="server"></asp:TextBox>
                    <asp:Label ID="Label11" runat="server" Text="Rol: "></asp:Label>
                    <asp:DropDownList ID="LstRoles" runat="server"></asp:DropDownList>
                    <asp:Button ID="BtnFiltrar" runat="server" Text="Filtrar" OnClick="BtnFiltrar_Click" />
                    <br />
                </asp:Panel>
                <asp:GridView ID="GVUsuarios" runat="server" AutoGenerateColumns="False" OnRowCommand="GVUsuarios_RowCommand" PageSize="50">
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="NombresCompletos" HeaderText="Nombres Completos" />
                        <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                        <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Edad" HeaderText="Edad" />
                        <asp:BoundField DataField="RolNombre" HeaderText="Rol" />
                        <asp:ButtonField Text="Editar" CommandName="Editar" />
                        <asp:ButtonField Text="Eliminar" CommandName="Eliminar" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
