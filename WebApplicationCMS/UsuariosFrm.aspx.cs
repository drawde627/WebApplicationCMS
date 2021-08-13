using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Net;
using System.Text;
using System.IO;
using System.Configuration;

namespace WebApplicationCMS
{
    public partial class UsuariosFrm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"].ToString() == "")
            {
                Response.Redirect("LoginFrm.aspx");
            }


            LblUsuario.Text = Session["Usuario"].ToString();

            Models.Login login = (Models.Login)Session["Login"];
            LblNombresCompletos.Text = login.Usuario.NombresCompletos;
            LblDireccion.Text = login.Usuario.Direccion;
            LblTelefono.Text = login.Usuario.Telefono;
            LblEmail.Text = login.Usuario.Email;
            LblEdad.Text = login.Usuario.Edad.ToString();
            LblRolNombre.Text = login.Usuario.RolNombre;

            bool quitarColumnaEditar = true;
            bool quitarColumnaEliminar = true;
            foreach (Permiso per in login.Permisos)
            {


                if (per.Id == 1)
                {
                    PnlMensajeBienvenida.Visible = true;
                }

                if (per.Id == 2)
                {
                    PnlNoticias.Visible = true;

                }
                if (per.Id == 4)
                {
                    PnlListar.Visible = true;

                    if (!IsPostBack)
                    {

                    }
                }

                if (per.Id == 5)
                {
                    PnlListar.Visible = true;

                    //if (!IsPostBack)
                    //{
                    quitarColumnaEditar = false;

                    //}
                }

                if (per.Id == 6)
                {
                    PnlListar.Visible = true;

                    //if (!IsPostBack)
                    //{
                    quitarColumnaEditar = false;
                    quitarColumnaEliminar = false;
                    BtnCrearUsuario.Visible = true;

                    //}
                }


            }
            if (!IsPostBack)
            {
                if (quitarColumnaEditar)
                    GVUsuarios.Columns[7].Visible = false;
                if (quitarColumnaEliminar)
                    //GVUsuarios.Columns.RemoveAt(8);
                    GVUsuarios.Columns[8].Visible = false;

                ListarUsuarios("0", 0);
                ListarRoles();
            }



        }

        private void ListarUsuarios(string nombre, int rolId)
        {
            string respuesta = string.Empty;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            string requestUrl = ConfigurationManager.AppSettings["UrlServicio"] + "usuario";
            HttpWebRequest httpWR = WebRequest.Create(new Uri(requestUrl)) as HttpWebRequest;
            httpWR.Timeout = 180000;
            httpWR.ContentType = "application/json";
            httpWR.Method = "POST";

            Models.Usuario usu = new Models.Usuario();
            usu.Nombre = nombre;
            usu.RolId = rolId;

            string json = string.Empty;
            json = jsonSerializer.Serialize(usu);

            byte[] postBytes = UTF8Encoding.UTF8.GetBytes(json);
            httpWR.ContentLength = postBytes.Length;

            // Write postBytes to request stream
            using (Stream reqStream = httpWR.GetRequestStream())
            {
                reqStream.Write(postBytes, 0, postBytes.Length);
                reqStream.Close();
            }

            HttpWebResponse httpResponse = null;

            try
            {
                httpResponse = (HttpWebResponse)httpWR.GetResponse();
            }
            catch (System.Net.WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
                if (httpResponse == null)
                {
                    throw;
                }
            }

            try
            {
                bool error = (httpResponse.StatusCode != HttpStatusCode.OK);

                using (var streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    respuesta = streamReader.ReadToEnd();
                }

                if (!error)
                {
                    List<Models.Usuario> respuestaUsuario = jsonSerializer.Deserialize<List<Models.Usuario>>(respuesta);
                    if (respuestaUsuario != null)
                    {
                        GVUsuarios.DataSource = respuestaUsuario;
                        GVUsuarios.DataBind();

                    }
                    else
                    {
                        GVUsuarios.DataSource = null;
                        GVUsuarios.DataBind();

                    }
                }
            }
            catch (System.Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (httpResponse != null)
                {
                    httpResponse.Close();
                }
            }

        }

        protected void BtnFiltrar_Click(object sender, EventArgs e)
        {
            FiltrarUsuarios();

        }

        private void FiltrarUsuarios()
        {
            string nombre = "0";
            int rolId = 0;

            if (!string.IsNullOrEmpty(TBNombre.Text))
                nombre = TBNombre.Text;
            if (LstRoles.SelectedValue != "0")
                rolId = Convert.ToInt32(LstRoles.SelectedValue);

            ListarUsuarios(nombre, rolId);
        }

        private void ListarRoles()
        {
            string respuesta = string.Empty;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            string requestUrl = ConfigurationManager.AppSettings["UrlServicio"] + "rol";
            HttpWebRequest httpWR = WebRequest.Create(new Uri(requestUrl)) as HttpWebRequest;
            httpWR.Timeout = 180000;
            httpWR.ContentType = "application/json";
            httpWR.Method = "POST";

            //Models.Usuario usu = new Models.Usuario();
            //usu.Nombre = nombre;
            //usu.RolId = rolId;

            string json = string.Empty;
            //json = jsonSerializer.Serialize(usu);

            byte[] postBytes = UTF8Encoding.UTF8.GetBytes(json);
            httpWR.ContentLength = postBytes.Length;

            // Write postBytes to request stream
            using (Stream reqStream = httpWR.GetRequestStream())
            {
                reqStream.Write(postBytes, 0, postBytes.Length);
                reqStream.Close();
            }

            HttpWebResponse httpResponse = null;

            try
            {
                httpResponse = (HttpWebResponse)httpWR.GetResponse();
            }
            catch (System.Net.WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
                if (httpResponse == null)
                {
                    throw;
                }
            }

            try
            {
                bool error = (httpResponse.StatusCode != HttpStatusCode.OK);

                using (var streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    respuesta = streamReader.ReadToEnd();
                }

                if (!error)
                {
                    List<Models.Rol> respuestaUsuario = jsonSerializer.Deserialize<List<Models.Rol>>(respuesta);
                    if (respuestaUsuario != null)
                    {
                        LstRoles.DataValueField = "Id";
                        LstRoles.DataTextField = "Nombre";
                        LstRoles.DataSource = respuestaUsuario;
                        LstRoles.DataBind();
                        LstRoles.Items.Insert(0, new ListItem("", "0"));

                        LstRolEdit.DataValueField = "Id";
                        LstRolEdit.DataTextField = "Nombre";
                        LstRolEdit.DataSource = respuestaUsuario;
                        LstRolEdit.DataBind();
                    }
                    else
                    {
                        LstRoles.DataSource = null;
                        LstRoles.DataBind();

                    }
                }
            }
            catch (System.Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (httpResponse != null)
                {
                    httpResponse.Close();
                }
            }

        }

        protected void GVUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                LblTituloEditar.Text = "Editar";
                TBNombreEdit.Enabled = false;
                TBNombreEdit.ReadOnly = true;
                LblMensajeEdit.Text = "";

                int numRow = Convert.ToInt32(e.CommandArgument);
                TBNombreEdit.Text = GVUsuarios.Rows[numRow].Cells[0].Text;
                TBNombresCompletosEdit.Text = GVUsuarios.Rows[numRow].Cells[1].Text;
                TBDireccionEdit.Text = GVUsuarios.Rows[numRow].Cells[2].Text;
                TBTelefonoEdit.Text = GVUsuarios.Rows[numRow].Cells[3].Text;
                TBEmailEdit.Text = GVUsuarios.Rows[numRow].Cells[4].Text;
                TBEdadEdit.Text = GVUsuarios.Rows[numRow].Cells[5].Text;
                string rolId = "";
                foreach (ListItem item in LstRolEdit.Items)
                {
                    if (item.Text == GVUsuarios.Rows[numRow].Cells[6].Text)
                        rolId = item.Value;

                }
                LstRolEdit.SelectedValue = rolId;

                if (GVUsuarios.Rows[numRow].Cells[0].Text == LblUsuario.Text)
                {
                    LstRolEdit.Enabled = false;
                }
                else
                { LstRolEdit.Enabled = true; }

                PnlEditar.Visible = true;
            }

            if (e.CommandName == "Eliminar")
            {

                LblMensajeEdit.Text = "";
                int numRow = Convert.ToInt32(e.CommandArgument);

                if (GVUsuarios.Rows[numRow].Cells[0].Text == LblUsuario.Text)
                {
                    LblMensajeEdit.Text = "Acción no permitida";
                }
                else
                {
                    Models.Usuario usu = new Models.Usuario();
                    usu.Nombre = GVUsuarios.Rows[numRow].Cells[0].Text;
                    usu.NombresCompletos = GVUsuarios.Rows[numRow].Cells[1].Text;
                    usu.Direccion = GVUsuarios.Rows[numRow].Cells[2].Text;
                    usu.Telefono = GVUsuarios.Rows[numRow].Cells[3].Text;
                    usu.Email = GVUsuarios.Rows[numRow].Cells[4].Text;
                    usu.Edad = Convert.ToInt32(GVUsuarios.Rows[numRow].Cells[5].Text);
                    int rolId = 0;
                    foreach (ListItem item in LstRolEdit.Items)
                    {
                        if (item.Text == GVUsuarios.Rows[numRow].Cells[6].Text)
                            rolId = Convert.ToInt32(item.Value);

                    }
                    usu.RolId = rolId;
                    EliminarUsuario(usu);
                }
            }
        }

        protected void BtnCancelarEdit_Click(object sender, EventArgs e)
        {
            LblMensajeEdit.Text = "";
            PnlEditar.Visible = false;
        }


        protected void BtnGuardarEdit_Click(object sender, EventArgs e)
        {
            LblMensajeEdit.Text = "";
            if (LblTituloEditar.Text == "Editar")
            {

                ActuliazarUsuario();
                PnlEditar.Visible = false;
            }

            if (LblTituloEditar.Text == "Crear")
            {
                CrearUsuario();
                PnlEditar.Visible = false;
            }

        }

        private void ActuliazarUsuario()
        {
            LblMensajeEdit.Text = string.Empty;
            string respuesta = string.Empty;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            string requestUrl = ConfigurationManager.AppSettings["UrlServicio"] + "usuario/update";
            HttpWebRequest httpWR = WebRequest.Create(new Uri(requestUrl)) as HttpWebRequest;
            httpWR.Timeout = 180000;
            httpWR.ContentType = "application/json";
            httpWR.Method = "POST";

            Models.Usuario usu = new Models.Usuario();
            usu.Nombre = TBNombreEdit.Text;
            usu.NombresCompletos = TBNombresCompletosEdit.Text;
            usu.Direccion = TBDireccionEdit.Text;
            usu.Telefono = TBTelefonoEdit.Text;
            usu.Email = TBEmailEdit.Text;
            if (TBEdadEdit.Text.Trim() == "")
            {
                usu.Edad = 0;
            }
            else { usu.Edad = Convert.ToInt32(TBEdadEdit.Text); }
            usu.RolId = Convert.ToInt32(LstRolEdit.SelectedValue);

            string json = string.Empty;
            json = jsonSerializer.Serialize(usu);

            byte[] postBytes = UTF8Encoding.UTF8.GetBytes(json);
            httpWR.ContentLength = postBytes.Length;

            // Write postBytes to request stream
            using (Stream reqStream = httpWR.GetRequestStream())
            {
                reqStream.Write(postBytes, 0, postBytes.Length);
                reqStream.Close();
            }

            HttpWebResponse httpResponse = null;

            try
            {
                httpResponse = (HttpWebResponse)httpWR.GetResponse();
            }
            catch (System.Net.WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
                if (httpResponse == null)
                {
                    throw;
                }
            }

            try
            {
                bool error = (httpResponse.StatusCode != HttpStatusCode.OK);

                using (var streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    respuesta = streamReader.ReadToEnd();
                }

                if (!error)
                {
                    Models.Usuario respuestaUsuario = jsonSerializer.Deserialize<Models.Usuario>(respuesta);
                    if (respuestaUsuario != null)
                    {
                        if (respuestaUsuario.Nombre != "")
                        {
                            LblMensajeEdit.Text = "Se guardaron datos de " + respuestaUsuario.Nombre;
                            Models.Login login = (Models.Login)Session["Login"];
                            if (login.Usuario.Nombre == respuestaUsuario.Nombre)
                            {

                                string rolNombre = "";
                                foreach (ListItem item in LstRolEdit.Items)
                                {
                                    if (item.Value == respuestaUsuario.RolId.ToString())
                                        rolNombre = item.Text;

                                }
                                respuestaUsuario.RolNombre = rolNombre;

                                login.Usuario = respuestaUsuario;
                                Session["Login"] = login;
                            }
                        }
                        FiltrarUsuarios();
                    }

                }
            }
            catch (System.Exception exc)
            {
                //throw exc;
                LblMensajeEdit.Text = "Servicio no disponible";
            }
            finally
            {
                if (httpResponse != null)
                {
                    httpResponse.Close();
                }
            }


        }


        private void EliminarUsuario(Models.Usuario usu)
        {
            LblMensajeEdit.Text = string.Empty;
            string respuesta = string.Empty;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            string requestUrl = ConfigurationManager.AppSettings["UrlServicio"] + "usuario/delete";
            HttpWebRequest httpWR = WebRequest.Create(new Uri(requestUrl)) as HttpWebRequest;
            httpWR.Timeout = 180000;
            httpWR.ContentType = "application/json";
            httpWR.Method = "POST";

            //Models.Usuario usu = new Models.Usuario();
            //usu.Nombre = TBNombreEdit.Text;
            //usu.NombresCompletos = TBNombresCompletosEdit.Text;
            //usu.Direccion = TBDireccionEdit.Text;
            //usu.Telefono = TBTelefonoEdit.Text;
            //usu.Email = TBEmailEdit.Text;
            //usu.Edad = Convert.ToInt32(TBEdadEdit.Text);
            //usu.RolId = Convert.ToInt32(LstRolEdit.SelectedValue);

            string json = string.Empty;
            json = jsonSerializer.Serialize(usu);

            byte[] postBytes = UTF8Encoding.UTF8.GetBytes(json);
            httpWR.ContentLength = postBytes.Length;

            // Write postBytes to request stream
            using (Stream reqStream = httpWR.GetRequestStream())
            {
                reqStream.Write(postBytes, 0, postBytes.Length);
                reqStream.Close();
            }

            HttpWebResponse httpResponse = null;

            try
            {
                httpResponse = (HttpWebResponse)httpWR.GetResponse();
            }
            catch (System.Net.WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
                if (httpResponse == null)
                {
                    throw;
                }
            }

            try
            {
                bool error = (httpResponse.StatusCode != HttpStatusCode.OK);

                using (var streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    respuesta = streamReader.ReadToEnd();
                }

                if (!error)
                {
                    Models.Usuario respuestaUsuario = jsonSerializer.Deserialize<Models.Usuario>(respuesta);
                    if (respuestaUsuario != null)
                    {
                        if (respuestaUsuario.Nombre != "")
                            LblMensajeEdit.Text = "Se eliminaron datos de " + respuestaUsuario.Nombre;
                        FiltrarUsuarios();
                    }

                }
            }
            catch (System.Exception exc)
            {
                //throw exc;
                LblMensajeEdit.Text = "Servicio no disponible";
            }
            finally
            {
                if (httpResponse != null)
                {
                    httpResponse.Close();
                }
            }
        }

        private void CrearUsuario()
        {
            if (TBNombreEdit.Text.Trim() != "")
            {

                LblMensajeEdit.Text = string.Empty;
                string respuesta = string.Empty;
                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                string requestUrl = ConfigurationManager.AppSettings["UrlServicio"] + "usuario/create";
                HttpWebRequest httpWR = WebRequest.Create(new Uri(requestUrl)) as HttpWebRequest;
                httpWR.Timeout = 180000;
                httpWR.ContentType = "application/json";
                httpWR.Method = "POST";

                Models.Usuario usu = new Models.Usuario();
                usu.Nombre = TBNombreEdit.Text.Trim();
                usu.NombresCompletos = TBNombresCompletosEdit.Text;
                usu.Direccion = TBDireccionEdit.Text;
                usu.Telefono = TBTelefonoEdit.Text;
                usu.Email = TBEmailEdit.Text;
                if (TBEdadEdit.Text.Trim() == "")
                {
                    usu.Edad = 0;
                }
                else { usu.Edad = Convert.ToInt32(TBEdadEdit.Text); }

                usu.RolId = Convert.ToInt32(LstRolEdit.SelectedValue);

                string json = string.Empty;
                json = jsonSerializer.Serialize(usu);

                byte[] postBytes = UTF8Encoding.UTF8.GetBytes(json);
                httpWR.ContentLength = postBytes.Length;

                // Write postBytes to request stream
                using (Stream reqStream = httpWR.GetRequestStream())
                {
                    reqStream.Write(postBytes, 0, postBytes.Length);
                    reqStream.Close();
                }

                HttpWebResponse httpResponse = null;

                try
                {
                    httpResponse = (HttpWebResponse)httpWR.GetResponse();
                }
                catch (System.Net.WebException ex)
                {
                    httpResponse = (HttpWebResponse)ex.Response;
                    if (httpResponse == null)
                    {
                        throw;
                    }
                }

                try
                {
                    bool error = (httpResponse.StatusCode != HttpStatusCode.OK);

                    using (var streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                    {
                        respuesta = streamReader.ReadToEnd();
                    }

                    if (!error)
                    {
                        Models.Usuario respuestaUsuario = jsonSerializer.Deserialize<Models.Usuario>(respuesta);
                        if (respuestaUsuario != null)
                        {

                            if (respuestaUsuario.Nombre != "")
                                LblMensajeEdit.Text = "Se registraron datos de " + respuestaUsuario.Nombre;

                            FiltrarUsuarios();
                        }

                    }
                }
                catch (System.Exception exc)
                {
                    //throw exc;
                    LblMensajeEdit.Text = "Servicio no disponible";
                }
                finally
                {
                    if (httpResponse != null)
                    {
                        httpResponse.Close();
                    }
                }
            }
            else
            {
                LblMensajeEdit.Text = "Nombre no valido";
            }
        }

        protected void BtnCrearUsuario_Click(object sender, EventArgs e)
        {
            LblMensajeEdit.Text = "";

            LblTituloEditar.Text = "Crear";
            TBNombreEdit.Enabled = true;
            TBNombreEdit.ReadOnly = false;
            TBNombreEdit.Text = "";
            TBNombresCompletosEdit.Text = "";
            TBDireccionEdit.Text = "";
            TBTelefonoEdit.Text = "";
            TBEmailEdit.Text = "";
            TBEdadEdit.Text = "";
            LstRolEdit.Enabled = true;
            PnlEditar.Visible = true;
        }
    }
}