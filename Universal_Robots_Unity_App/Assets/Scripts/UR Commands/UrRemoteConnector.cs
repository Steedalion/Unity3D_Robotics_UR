using UnityEngine;

namespace Unity_UniversalRobot.com.ur.controller.Scripts.UR3
{
    public class UrRemoteConnector : MonoBehaviour
    {
        /// <summary>
        /// A script for connecting to the robot easily.
        /// </summary>
        public string ip_address = "192.168.121.131";

        public bool autoConnect = false;

        private void Start()
        {
            if (autoConnect)
            {
                ConnectToRobot();
            }
        }

        private void OnDestroy()
        {
            Disconnect();
        }


        [ContextMenu(nameof(ConnectToRobot))]
        public void ConnectToRobot()
        {
            GlobalVariables_Main_Control.ur3_tcpip_read_config_str = ip_address;
            GlobalVariables_Main_Control.ur3_tcpip_write_config_str = ip_address;
            GlobalVariables_Main_Control.connect = true;
            GlobalVariables_Main_Control.disconnect = false;
        }

        public void Disconnect()
        {
            GlobalVariables_Main_Control.connect = false;
            GlobalVariables_Main_Control.disconnect = true;
        }
    }
}