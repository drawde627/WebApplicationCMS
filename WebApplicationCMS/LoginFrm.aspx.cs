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
    public partial class LoginFrm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            LblMensaje.Text = string.Empty;
            string respuesta = string.Empty;
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            string requestUrl = ConfigurationManager.AppSettings["UrlServicio"] + "login";
            HttpWebRequest httpWR = WebRequest.Create(new Uri(requestUrl)) as HttpWebRequest;
            httpWR.Timeout = 180000;
            httpWR.ContentType = "application/json";
            httpWR.Method = "POST";

            Models.Usuario usu = new Models.Usuario();
            usu.Nombre = TBUsuario.Text;
            usu.Password = TBPass.Text;

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
                    Models.Login respuestaLogin = jsonSerializer.Deserialize<Models.Login>(respuesta);
                    if (respuestaLogin.Usuario != null)
                    {
                        Session["Usuario"] = respuestaLogin.Usuario.Nombre;
                        Session["Login"] = respuestaLogin;
                        Response.Redirect("UsuariosFrm.aspx");

                    }
                    else
                    {
                        LblMensaje.Text = "Datos no encontrados";
                        Session["Usuario"] = "";
                        Session.Abandon();

                    }
                }
            }
            catch (System.Exception exc)
            {
                //throw exc;
                LblMensaje.Text = "Servicio no disponible";
            }
            finally
            {
                if (httpResponse != null)
                {
                    httpResponse.Close();
                }
            }


        }
    }
}