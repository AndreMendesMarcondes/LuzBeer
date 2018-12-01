using LuzBeer.Models;
using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace LuzBeer.Business
{
    public static class Util
    {
        public static string CaminhoXML { get { return $@"{HttpContext.Current.Request.PhysicalApplicationPath}"; } }

        public static T DeserializarXML<T>()
        {
            T retorno = default(T);
            var xml = File.ReadAllText($@"{HttpContext.Current.Request.PhysicalApplicationPath}/produtosSistema.xml");

            try
            {
                using (var reader = new StringReader(xml))
                {
                    try
                    {
                        retorno = (T)new XmlSerializer(typeof(T)).Deserialize(reader);
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }

        public static string SerializarXML<T>(T objeto)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            var xml = "";

            using (var sww = new UFT8StringWriter())
            {
                var xmlWriterSettings = new XmlWriterSettings
                {
                    Indent = true,
                    OmitXmlDeclaration = false,
                    Encoding = Encoding.UTF8
                };
                using (XmlWriter writer = XmlWriter.Create(sww, xmlWriterSettings))
                {
                    xsSubmit.Serialize(writer, objeto);
                    xml = sww.ToString();
                }
            }

            return xml;
        }

        public static T DeserializarXML<T>(string xml)
        {
            T retorno = default(T);

            try
            {
                using (var reader = new StringReader(xml))
                {
                    try
                    {
                        retorno = (T)new XmlSerializer(typeof(T)).Deserialize(reader);
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }

        public static Models.LuzBeer CarregarXML()
        {
            DirectoryInfo caminhoArquivo = new DirectoryInfo(CaminhoXML);

            if (!caminhoArquivo.Exists)
            {
                caminhoArquivo.Create();
                DirectorySecurity dSecurity = caminhoArquivo.GetAccessControl();
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                caminhoArquivo.SetAccessControl(dSecurity);
            }

            FileInfo arquivoNomeCompleto = new FileInfo($@"{CaminhoXML}/LuzBeer.xml");

            if (!arquivoNomeCompleto.Exists)
            {
                var arq = File.Create($@"{CaminhoXML}/LuzBeer.xml");
                arq.Close();

                var admin = new Admin() { Login = "luzbeeradmin", Senha = "luzbeer2018" };
                var luz = new Models.LuzBeer()
                {
                    Admin = admin
                };

                string xmls = SerializarXML<Models.LuzBeer>(luz);
                SalvarXML(xmls);
            }
            
            
            System.IO.StreamReader reader = new StreamReader($@"{CaminhoXML}/LuzBeer.xml");
            var xml = reader.ReadToEnd();
            reader.Close();

            return DeserializarXML<Models.LuzBeer>(xml);
        }

        public static void SalvarXML(string xml)
        {
            System.IO.StreamWriter file = new StreamWriter($@"{CaminhoXML}/LuzBeer.xml");
            file.WriteLine(xml);
            file.Close();
        }

        public static void SalvarXML(Models.LuzBeer luzbeer)
        {
            var xml = SerializarXML<Models.LuzBeer>(luzbeer);
            System.IO.StreamWriter file = new StreamWriter($@"{CaminhoXML}/LuzBeer.xml");
            file.WriteLine(xml);
            file.Close();
        }

    }
    public class UFT8StringWriter : StringWriter
    {
        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }
}