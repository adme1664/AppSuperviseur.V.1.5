using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Ht.Ihsil.Rgph.App.Superviseur.utils
{
    public class XmlUtils
    {

        XElement element;
        private string adrServer;
        private string uri;

        public string Uri
        {
            get { return uri; }
            set { uri = value; }
        }

        public string AdrServer
        {
            get { return adrServer; }
            set { adrServer = value; }
        }
        public XmlUtils()
        {

        }
        public XmlUtils(string uri)
        {
            element = getRootElement(uri);
            this.uri = uri;
        }
        public XElement getRootElement(string uri)
        {
            if (uri != null)
            {
                return XElement.Load(uri);
            }
            return null;
        }
        public string getAdrServer()
        {
            if (element != null)
            {
                XElement adrServer = element.Element(Constant.XML_ELEMENT_ADR_SERVER);
                if (adrServer != null)
                {
                    return adrServer.Value;
                }
            }
            return null;
        }
        public string getString(string balise)
        {
            if (element != null)
            {
                XElement elmnt = element.Element(balise);
                if (elmnt != null)
                {
                    return elmnt.Value;
                }
            }                                                                                       
            return null;
        }
        public void UpdateAdrServer(string newAdress)
        {
            if (element != null)
            {
                XElement ele = element.Element(Constant.XML_ELEMENT_ADR_SERVER);
                if (ele != null)
                {
                    ele.Value = newAdress;
                    element.Save(uri);
                }
            }
        }
        public bool updateEnvironnementVariable(string variable)
        {
            if (element != null)
            {
                XElement element1 = element.Element(Constant.XML_ELEMENT_VARIABLE);
                if (element1 != null)
                {
                    element1.Value = variable;
                    element.Save(uri);
                    return true;
                }
                else
                {
                    element1.Value = variable;
                    element.Save(uri);
                    return true;
                }
            }
            return false;
        }

        public static string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = new XmlTextWriter(sw);
            Logger log = new Logger();
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                // System.IO.FileStream file=File.Create("C://data//batiment.xml");
                serializer.Serialize(tw, o);
                //serializer.Serialize(file, o);
                return sw.ToString();
            }
            catch (Exception ex)
            {
                log.Info("XML/ERROR:" + ex.Message);
                log.Info("INner:" + ex.InnerException);
                return "Error : " + ex.Message;
            }
            finally
            {
                sw.Close();
                tw.Close();
            }
        }

        #region RAPPORT DEROULEMENT COLLECTE
        /// <summary>
        /// Retourne la liste des sous domaines
        /// </summary>
        /// <returns>List<KeyValue></returns>
        public List<KeyValue> listOfDomaines()
        {
            List<KeyValue> listof = new List<KeyValue>();
            if (element != null)
            {
                var domaines = element.Elements("domaine");
                foreach (var d in domaines)
                {
                    KeyValue val = new KeyValue((int)d.Attribute("code"), (string)d.Attribute("name"));
                    listof.Add(val);
                }
            }
            return listof;
        }
        public List<KeyValue> listOfSousDomaines()
        {
            List<KeyValue> listof = new List<KeyValue>();
            if (element != null)
            {
                var domaines = element.Elements("domaine");
                foreach (var d in domaines)
                {
                    KeyValue val = new KeyValue((int)d.Attribute("code"), (string)d.Attribute("name"));
                    listof.Add(val);
                }
            }
            return listof;
        }

        public List<KeyValue> listOfSousDomaines(string codeDomaine)
        {
            List<KeyValue> listOf = new List<KeyValue>();
            if (element != null)
            {
                var sousDomaines = from rapport in element.Elements("domaine")
                                   where (string)rapport.Attribute("code") == codeDomaine
                                   select rapport.Elements("sousDomaine");
                foreach (var sousDomaine in sousDomaines)
                {
                    foreach (var sd in sousDomaine)
                    {
                        KeyValue val = new KeyValue((int)sd.Attribute("code"), (string)sd.Attribute("name"));
                        listOf.Add(val);
                    }
                }
            }
            return listOf;
        }

        public List<KeyValue> listOfProblemes(string codeDomaine,string codeSousDomaine)
        {
            List<KeyValue> listOf = new List<KeyValue>();
            if (element != null)
            {
                var sousDomaines = from rapport in element.Elements("domaine")
                                   where (string)rapport.Attribute("code") == codeDomaine
                                   select rapport.Elements("sousDomaine");
                foreach (var sousDomaine in sousDomaines)
                {
                    foreach (var sd in sousDomaine)
                    {
                        if ((string)sd.Attribute("code") == codeSousDomaine)
                        {
                            var problemes = sd.Elements("probleme");
                            foreach (var probleme in problemes)
                            {
                                KeyValue prob = new KeyValue((int)probleme.Attribute("code"), probleme.Value);
                                listOf.Add(prob);
                            }
                        }
                    }
                }
            }
            return listOf;
        }

        public List<KeyValue> listOfSolutions(string codeDomaine, string codeSousDomaine)
        {
            List<KeyValue> listOf = new List<KeyValue>();
            if (element != null)
            {
                var sousDomaines = from rapport in element.Elements("domaine")
                                   where (string)rapport.Attribute("code") == codeDomaine
                                   select rapport.Elements("sousDomaine");
                foreach (var sousDomaine in sousDomaines)
                {
                    foreach (var sd in sousDomaine)
                    {
                        if ((string)sd.Attribute("code") == codeSousDomaine)
                        {
                            var solutions = sd.Elements("solution");
                            foreach (var solution in solutions)
                            {
                                KeyValue sol = new KeyValue((int)solution.Attribute("code"), solution.Value);
                                listOf.Add(sol);
                            }
                        }
                    }
                }
            }
            return listOf;
        }

        public List<KeyValue> listOfSuivi()
        {
            List<KeyValue> listOf=new List<KeyValue>();
            if (element != null)
            {
                var suivis = element.Elements("suivi");
                foreach (var suivi in suivis)
                {
                    KeyValue key = new KeyValue((int)suivi.Attribute("code"), suivi.Value);
                    listOf.Add(key);
                }
            }
            return listOf;
        }

        public KeyValue getDomaine(int code)
        {
            KeyValue domaine = new KeyValue();
            if (element != null)
            {
                var domaines = element.Elements("domaine");
                foreach (var d in domaines)
                {
                    if ((int)d.Attribute("code") == code)
                    {
                        domaine = new KeyValue((int)d.Attribute("code"), (string)d.Attribute("name"));
                        break;
                    }
                }
            }
            return domaine;
        }

        public KeyValue getSousDomaine(int codeDomaine, int codeSousDomaine)
        {
            KeyValue sousDomaine = new KeyValue();
            if (element != null)
            {
                var domaines = element.Elements("domaine");
                foreach (var d in domaines)
                {
                    if ((int)d.Attribute("code") == codeDomaine)
                    {
                        var sousDomaines = d.Elements("sousDomaine");
                        foreach (var sd in sousDomaines)
                        {
                            if ((int)sd.Attribute("code") == codeSousDomaine)
                            {
                                sousDomaine =  new KeyValue((int)sd.Attribute("code"), (string)sd.Attribute("name"));
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            return sousDomaine;
        }

        public KeyValue getProbleme(int codeDomaine, int codeSousDomaine, int codeProbleme)
        {
            KeyValue probleme = new KeyValue();
            if (element != null)
            {
                var domaines = element.Elements("domaine");
                foreach (var d in domaines)
                {
                    if ((int)d.Attribute("code") == codeDomaine)
                    {
                        var sousDomaines = d.Elements("sousDomaine");
                        foreach (var sd in sousDomaines)
                        {
                            if ((int)sd.Attribute("code") == codeSousDomaine)
                            {
                                var problemes = sd.Elements("probleme");
                                foreach (var prob in problemes)
                                {
                                    if ((int)prob.Attribute("code") == codeProbleme)
                                    {
                                        probleme = new KeyValue((int)prob.Attribute("code"), prob.Value);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return probleme;
       }

        public KeyValue getSolution(int codeDomaine, int codeSousDomaine, int codeSolution)
        {
            KeyValue solution = new KeyValue();
            if (element != null)
            {
                var domaines = element.Elements("domaine");
                foreach (var d in domaines)
                {
                    if ((int)d.Attribute("code") == codeDomaine)
                    {
                        var sousDomaines = d.Elements("sousDomaine");
                        foreach (var sd in sousDomaines)
                        {
                            if ((int)sd.Attribute("code") == codeSousDomaine)
                            {
                                var solutions = sd.Elements("solution");
                                foreach (var sol in solutions)
                                {
                                    if ((int)sol.Attribute("code") == codeSolution)
                                    {
                                        solution = new KeyValue((int)sol.Attribute("code"), sol.Value);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return solution;
        }

        public KeyValue getSuivi(int codeSuivi)
        {
                KeyValue suivi = new KeyValue();
                if (element != null)
                {
                    var suivis = element.Elements("suivi");
                    foreach (var sv in suivis)
                    {
                        if ((int)sv.Attribute("code") == codeSuivi)
                        {
                            suivi = new KeyValue((int)sv.Attribute("code"), sv.Value);
                            break;
                        }
                     }
                }
                return suivi;
        }
        #endregion


    }
}
