using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.Json;
using Ht.Ihsil.Rgph.App.Superviseur.Schema;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Ht.Ihsil.Rgph.App.Superviseur.services
{
   public class TransfertService
    {
       MqttClient mqttClient = null;
       string adrSvrMqtt = "";
       Logger log;
       string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Configuration\";
       string file = "";
       XmlUtils configuration = null;
       public TransfertService()
       {
           log = new Logger();
           file = basePath + "configuration.xml";
           configuration = new XmlUtils(file);
           adrSvrMqtt = configuration.getAdrServer();
           log.Info("SERVER ADDRESS:" + adrSvrMqtt);
           mqttClient = new MqttClient(adrSvrMqtt);
           string clientId = Guid.NewGuid().ToString();
           mqttClient.Connect(clientId);
           mqttClient.MqttMsgPublished += mqttClient_MqttMsgPublished;
           mqttClient.MqttMsgPublishReceived += mqttClient_MqttMsgPublishReceived;
       }

       void mqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
       {
           log.Info("RECEIVED============>Message:" + e.Message.ToString());
       }

       void mqttClient_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
       {
           if(e.IsPublished==true){
               log.Info("PUBLISH============>Message:" + e.MessageId);
           }
           else
           {
               log.Info("NOT PUBLISH============>Message:" + e.MessageId);
           }
           
       }

       
       public bool publishBatimentData(string data)
       {

           try
           {

              if (Utilities.pingTheServer(adrSvrMqtt) == true)
              {
                  log.Info("PING THE SERVER================<>RESPONSE:" + true);
                  if (data != null)
                  {
                      mqttClient.Publish(Constant.TOPIC_COLLECT_DATA, Encoding.UTF8.GetBytes(data), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
                      return true;
                  }
              }
              else
              {
                  log.Info("PING THE SERVER================<>RESPONSE:" + false);
              }
           }
           catch (Exception ex)
           {
               log.Info("ERROR=============================<>:" + ex.Message);
           }
          return false;
       }
       public bool publishContreEntreData(BatimentData data)
       {
           try
           {
               if (Utilities.pingTheServer(adrSvrMqtt) == true)
               {
                   log.Info("PING THE SERVER================<>RESPONSE:" + true);
                   if (data != null)
                   {
                       string strXml = XmlUtils.GetXMLFromObject(data);
                       mqttClient.Publish(Constant.TOPIC_CONTRE_ENQUETE_DATA, Encoding.UTF8.GetBytes(strXml), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
                       return true;
                   }
               }
               else
               {
                   log.Info("PING THE SERVER================<>RESPONSE:" + false);
               }
           }
           catch (Exception ex)
           {
               log.Info("ERROR=============================<>:" + ex.Message);
           }
           return false;
       }

       public bool publishRapportSupervisionDirect(string rapport)
       {
           try
           {
               if (Utilities.pingTheServer(adrSvrMqtt) == true)
               {
                   if (rapport != null)
                   {
                       mqttClient.Publish(Constant.TOPIC_RAPPORT_SUPERVISION_DIRECTE, Encoding.UTF8.GetBytes(rapport), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
                       return true;
                   }
               }
               else
               {
                   log.Info("PING THE SERVER================<>RESPONSE:" + false);
               }
           }
           catch (Exception ex)
           {
               log.Info("ERROR=============================<>:" + ex.Message);
           }
           return false;
       }
       public bool publishRapporDeroulementCollecte(string rapport)
       {
           try
           {
               if (Utilities.pingTheServer(adrSvrMqtt) == true)
               {
                   if (rapport != null)
                   {
                       mqttClient.Publish(Constant.TOPIC_RAPPORT_DEROULEMENTCOLLECTE, Encoding.UTF8.GetBytes(rapport), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
                       return true;
                   }
               }
               else
               {
                   log.Info("PING THE SERVER================<>RESPONSE:" + false);
               }
           }
           catch (Exception ex)
           {
               log.Info("ERROR=============================<>:" + ex.Message);
           }
           return false;
       }
       public bool publishRapportProbleme(string rapport)
       {
           try
           {
               if (Utilities.pingTheServer(adrSvrMqtt) == true)
               {
                   if (rapport != null)
                   {
                       mqttClient.Publish(Constant.TOPIC_PROBLEME_RENCONTREE, Encoding.UTF8.GetBytes(rapport), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
                       return true;
                   }
               }
               else
               {
                   log.Info("PING THE SERVER================<>RESPONSE:" + false);
               }
           }
           catch (Exception ex)
           {
               log.Info("ERROR=============================<>:" + ex.Message);
           }
           return false;
       }
    }
}
