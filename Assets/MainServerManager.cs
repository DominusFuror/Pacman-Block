using UnityEngine;
using System.IO;
using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Client.Options;
using System.Threading.Tasks;
using System.Text;
using System.Threading;
using System;
using System.Collections.Generic;

public class MainServerManager : MonoBehaviour
{
  static  IMqttClient mqttClient;
    TextMesh debText;

   static ServerConfig serverConfig ;
  

    public void FuncMQTT()
    {


        var factory = new MqttFactory();
        mqttClient = factory.CreateMqttClient();


        string JSONDeviceConfig = " {\"CommandId\" : 1 , \"Data\" : [\"" + serverConfig.deviceName + "\",\"2\"] } ";

        var options = new MqttClientOptionsBuilder()

             .WithTcpServer(serverConfig.mqttHostName, serverConfig.mqttPort)
             .WithCredentials(serverConfig.mqttUsername, serverConfig.mqttPassword)

       .Build();


        mqttClient.ConnectAsync(options);

        Thread.Sleep(1000);
        mqttClient.SubscribeAsync(serverConfig.placementName + "/" + serverConfig.serverName + "/" + serverConfig.outputTopicName);
        mqttClient.PublishAsync(serverConfig.placementName + "/" + serverConfig.serverName + "/" + serverConfig.inputTopicName, JSONDeviceConfig);
        mqttClient.SubscribeAsync(serverConfig.placementName + "/" + serverConfig.deviceName + "/" + serverConfig.inputTopicName);


        mqttClient.UseApplicationMessageReceivedHandler(MqttMessGet);


  


    }
        


    public void MqttMessGet(MqttApplicationMessageReceivedEventArgs e)
    {

        if (e.ApplicationMessage.Topic == serverConfig.placementName +"/" + serverConfig.deviceName + "/" + serverConfig.inputTopicName)
        {
           
            string json = (Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
        

            ServerCommand s = JsonUtility.FromJson<ServerCommand>(json);

            Text = DateTime.Now + "  :  " + json;
            if (s.CommandId == 1)
            {
                // Send Ping Command
                SendMQTTMess(1,1);

            }
            //Check Status request
            if (s.CommandId == 12)
            {

                SendMQTTMess(12, 1, Phase);

            }
            if (s.CommandId == 3)
            {

                Phase = s.Data[0];

            }


        }
       
     }

    public string Text=":";
    private void Update()
    {
   

        if (Phase != lastPhase)
        {
            try
            {
                Destroy(lastPhaseGo);
            }
            catch (Exception)
            {

                print("Nothing  to  Destory");
            }
            lastPhase = Phase;

            try
            {
                lastPhaseGo = Instantiate(Phases[Phase], Vector3.zero, this.transform.rotation);
                lastPhaseGo.name = Phase;
            }
            catch (Exception)
            {

                print("Cant make new Prefab");
            }

            if(Phase == "Deactivate Device")
            {

                System.Diagnostics.Process.Start("cmd", "/c shutdown -s -f -t 01");

            }
        }
        

    }

    
    public static void SendMQTTMess(string mess)
    {
        string m = " {\"CommandId\" : 2 , \"SubcommandId\": 0,  \"Data\" : [\"" + mess+"\"] } ";

        Task.Run (() =>  mqttClient.PublishAsync(serverConfig.placementName +"/"+ serverConfig.deviceName + "/" + serverConfig.outputTopicName, m));
    }
    public static void SendMQTTMess(int commandId, int subCommandIdm , string data)
    {
        string m = " {\"CommandId\" : "+commandId+" , \"SubcommandId\": "+subCommandIdm+",  \"Data\" : [\""+ data +"\"] } ";

        Task.Run(() => mqttClient.PublishAsync(serverConfig.placementName +"/" + serverConfig.deviceName +"/"+serverConfig.outputTopicName, m));
    }
    public static void SendMQTTMess(int commandId, int subCommandIdm)
    {
        string m = " {\"CommandId\" : " + commandId + " , \"SubcommandId\": " + subCommandIdm + ",  \"Data\" : [] } ";

        Task.Run(() => mqttClient.PublishAsync(serverConfig.placementName +"/" + serverConfig.deviceName + "/" + serverConfig.outputTopicName, m));
    }


    public GameObject WeaponLoadPhase;
    public GameObject WeaponUnloadPhase;
    public GameObject CodePhase;
    public GameObject VirusPhase;
    public GameObject InfinityArmorPhase;
    public GameObject PackManPhase;

    public GameObject StartGamePhase;
    public GameObject DefeatPhase;
    public GameObject VictoryPhase;


    public Dictionary<string, GameObject> Phases = new Dictionary<string, GameObject>();
    void PhaseGen()
    {

     
        Phases.Add("PackManPhase", PackManPhase);
        Phases.Add("StartGamePhase", StartGamePhase);
        Phases.Add("InfinityArmorPhase", InfinityArmorPhase);
        Phases.Add("WeaponLoadPhase", WeaponLoadPhase);
        Phases.Add("WeaponUnloadPhase", WeaponUnloadPhase);
        Phases.Add("CodePhase", CodePhase);
        Phases.Add("VirusPhase", VirusPhase);
        Phases.Add("VictoryPhase", VictoryPhase);
        Phases.Add("DefeatPhase", DefeatPhase);
    }
    [Serializable]
    public class Command
    {
        public int CommandId;
        public int SubcommandId;
        public string[] Data;
    }
    [Serializable]
    public class ServerCommand
    {
        public int CommandId;   
        public string[] Data;
    }
    public string Phase="no";
    public string lastPhase="no";
    public GameObject lastPhaseGo;

    void Start()
    {
        PhaseGen();

        serverConfig = JsonUtility.FromJson<ServerConfig>(new StreamReader("ServerConfigFile.txt").ReadToEnd());
 
        debText = this.GetComponentInChildren<TextMesh>();


     
        FuncMQTT();

    }
    [Serializable]
   public  class ServerConfig
    {
        public string deviceName;
         public string mqttHostName;
        public int mqttPort;
        public string mqttUsername;
        public string mqttPassword;

        public string inputTopicName;
        public string outputTopicName;
        public string serverName;
        public string placementName;



    }


}
