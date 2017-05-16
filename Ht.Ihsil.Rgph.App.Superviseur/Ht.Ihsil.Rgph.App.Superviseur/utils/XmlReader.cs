using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsi.Rgph.Utility.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Ht.Ihsi.Rgph.Utility.Utils;
using System.Windows.Forms;

namespace Ht.Ihsil.Rgph.App.Superviseur.utils
{
    public class XmlReader
    {
        public static string XML_BATIMENT_FILE = Application.StartupPath + "\\Data\\xml\\batiments.xml";
        public static string XML_LOGEMENT_FILE =Application.StartupPath + "\\Data\\xml\\logement.xml";
        public static string XML_MENAGE_FILE =Application.StartupPath + "\\Data\\xml\\menage.xml";
        public static string XML_INDIVIDU_FILE = Application.StartupPath + "\\Data\\xml\\individu.xml";
        public static string XML_DECES_FILE = Application.StartupPath + "\\Data\\xml\\deces.xml";
        public static string XML_EMIGRE_FILE = Application.StartupPath + "\\Data\\xml\\emigre.xml";
        public static string XML_PAYS_FILE = Application.StartupPath + "\\Data\\xml\\pays.xml";
        public static string XML_DOMAINE_ETUDE_FILE = Application.StartupPath + "\\Data\\xml\\domaine_etude.xml";
        public static string XML_PROFESSIOn_FILE = Application.StartupPath + "\\Data\\xml\\profession.xml";
        public static string XML_AUTRE_FILE = Application.StartupPath + "\\Data\\xml\\autres.xml";
        public static string XML_BRANCHE_FILE = Application.StartupPath + "\\Data\\xml\\branche_activite.xml";
        public static string XML_COMMUNE_FILE = Application.StartupPath + "\\Data\\xml\\commune.xml";
        public static string XML_COMMUNE_SIMPLE_FILE = Application.StartupPath + "\\Data\\xml\\communes.xml";

        private XElement xelement = null;
        Logger log = null;

        public XmlReader(int fileType)
        {
            xelement = this.GetRootXmlElement(fileType);
            log = new Logger();
        }
        //public List<ReponseModel> read(string qCode)
        //{
        //    List<ReponseModel> reponses = null;
        //    var question = from questions in xelement.Elements()
        //                   where (string)questions.Attribute("code") == qCode
        //                   select questions.Elements("reponse");
        //    int i = 0;
        //    foreach (var reponse in question)
        //    {
        //        foreach (var r in reponse)
        //        {
        //            if (i == 0)
        //            {
        //                reponses = new List<ReponseModel>();
        //                i++;
        //            }
        //            ReponseModel m = new ReponseModel();
        //            m.Code = (string)r.Attribute("code");
        //            m.Value = r.Value;
        //            reponses.Add(m);
        //        }
        //    }
        //    return reponses;
        //}
        ///// <summary>
        ///// Lire tous les communes
        ///// </summary>
        ///// <param Name="qCode"></param>
        ///// <returns></returns>
        //public List<ReponseModel> readAllCommune(string qCode)
        //{
        //    List<ReponseModel> reponses = null;
        //    var question = from questions in xelement.Elements()
        //                   where (string)questions.Attribute("code") == qCode
        //                   select questions.Elements("reponse");
        //    int i = 0;
        //    foreach (var reponse in question)
        //    {
        //        foreach (var r in reponse)
        //        {
        //            if (i == 0)
        //            {
        //                reponses = new List<ReponseModel>();
        //                i++;
        //            }
        //            ReponseModel m = new ReponseModel();
        //            m.Code = (string)r.Attribute("code");
        //            m.Value = r.Value;
        //            reponses.Add(m);
        //        }
        //    }
        //    return reponses;
        //}

        //public List<SelectModel> readForSelect(string comCod)
        //{
        //    log.Info("<>===============Inside service:" + comCod);
        //    List<SelectModel> reponses = null;
        //    var question = from questions in xelement.Elements()
        //                   where (string)questions.Attribute("code") == comCod
        //                   select questions.Elements("reponse");
        //    int i = 0;
        //    foreach (var reponse in question)
        //    {
        //        foreach (var r in reponse)
        //        {
        //            if (i == 0)
        //            {
        //                reponses = new List<SelectModel>();
        //                i++;
        //            }
        //            SelectModel m = new SelectModel();
        //            m.id = (string)r.Attribute("code");
        //            m.text = r.Value;
        //            reponses.Add(m);
        //        }
        //    }
        //    return reponses;
        //}
        //public ReponseModel readName(string qCode)
        //{
        //    log.Info("<>=====================Code Commune:" + qCode);
        //    ReponseModel rep = null;
        //    var question = from questions in xelement.Elements()
        //                   where (string)questions.Attribute("code") == qCode
        //                   select questions;
        //    XElement qElement = question.FirstOrDefault();
        //    if (Utils.IsNotNull(qElement))
        //    {
        //        rep = new ReponseModel();
        //        rep.Code = (string)qElement.Attribute("code");
        //        rep.Value = (string)qElement.Attribute("Name");
        //        log.Info("<>==============File Commune XML" + rep.Code);
        //        log.Info("<>==============File Commune XML" + rep.Value);

        //    }
        //    return rep;
        //}

        //public ReponseModel readSectionCommunale(string qCode)
        //{
        //    log.Info("<>=====================Code section Communal:" + qCode);
        //    ReponseModel rep = null;
        //    var question = from questions in xelement.Elements()
        //                   from reponse in questions.Elements()
        //                   where (string)reponse.Attribute("code") == qCode
        //                   select reponse;
        //    XElement qElement = question.FirstOrDefault();
        //    if (Utils.IsNotNull(qElement))
        //    {
        //        rep = new ReponseModel();
        //        rep.Code = (string)qElement.Attribute("code");
        //        rep.Value = (string)qElement.Value;
        //        log.Info("<>==============File SC XML" + rep.Code);
        //        log.Info("<>==============File SC XML" + rep.Value);

        //    }
        //    return rep;
        //}
        public  string read(string qCode, int code)
        {
            var question = from questions in xelement.Elements()
                           where (string)questions.Attribute("code") == qCode
                           select questions.Elements("reponse");
            bool i = false;
            foreach (var reponse in question)
            {
                foreach (var r in reponse)
                {
                    var c = (string)r.Attribute("code");
                    if (c == "" + code)
                    {
                        i = true;
                        return r.Value;
                    }

                }

            }
            if (i == false)
            {
                return "" + code;
            }
            return "";
        }

        private XElement GetRootXmlElement(int typeFile)
        {
            if (typeFile == FormConstant.FILE_BATIMENT)
            {
                return XElement.Load(XML_BATIMENT_FILE);
            }
            else if (typeFile == FormConstant.FILE_LOGEMENT)
            {
                return XElement.Load(XML_LOGEMENT_FILE);
            }
            else if (typeFile == FormConstant.FILE_MENAGE)
            {
                return XElement.Load(XML_MENAGE_FILE);
            }
            else if (typeFile == FormConstant.FILE_INDIVIDU)
            {
                return XElement.Load(XML_INDIVIDU_FILE);
            }
            else if (typeFile == FormConstant.FILE_DECES)
            {
                return XElement.Load(XML_DECES_FILE);
            }
            else if (typeFile == FormConstant.FILE_EMIGRE)
            {
                return XElement.Load(XML_EMIGRE_FILE);
            }
            else if (typeFile == FormConstant.XML_PAYS_FILE)
            {
                return XElement.Load(XML_PAYS_FILE);
            }
            else if (typeFile == FormConstant.FILE_DOMAINE_ETUDE)
            {
                return XElement.Load(XML_DOMAINE_ETUDE_FILE);
            }
            else if (typeFile == FormConstant.FILE_PROFESSION)
            {
                return XElement.Load(XML_PROFESSIOn_FILE);
            }
            else if (typeFile == FormConstant.FILE_BRANCHE)
            {
                return XElement.Load(XML_BRANCHE_FILE);
            }
            else if (typeFile == FormConstant.FILE_COMMUNE)
            {
                return XElement.Load(XML_COMMUNE_FILE);
            }
            else if (typeFile == FormConstant.FILE_COMMUNE_SIMPLE)
            {
                return XElement.Load(XML_COMMUNE_SIMPLE_FILE);
            }
            else
            {
                return XElement.Load(XML_AUTRE_FILE);
            }

        }
    }
}