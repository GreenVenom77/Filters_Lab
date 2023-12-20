using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace FishNet.Discovery
{
	public sealed class NetworkDiscoveryHud : MonoBehaviour
	{
		[SerializeField]
		private NetworkDiscovery networkDiscovery;

		private readonly List<IPEndPoint> _endPoints = new List<IPEndPoint>();

		private Vector2 _serversListScrollVector;

        private bool isGUIActive = true;

		private void Start()
		{
			if (networkDiscovery == null) networkDiscovery = FindObjectOfType<NetworkDiscovery>();

            TurnOnGUI();

			networkDiscovery.ServerFoundCallback += endPoint =>
			{
				if (!_endPoints.Contains(endPoint)) _endPoints.Add(endPoint);
			};
		}

		private void OnGUI()
        {
            if (!isGUIActive)
                return;

            GUILayoutOption buttonHeight = GUILayout.Height(70.0f);

            // Adjust the padding and width based on your requirements
            float padding = 30.0f;
            float boxWidth = 400.0f;

            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = 35; // Adjust the font size for labels

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 35; // Adjust the font size for buttons

            GUILayout.BeginArea(new Rect(Screen.width - boxWidth - padding, padding, boxWidth, Screen.height - (2 * padding)));

            GUILayout.Box("Server", labelStyle);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Start", buttonStyle, buttonHeight))
            {
                InstanceFinder.ServerManager.StartConnection();
            }

            if (GUILayout.Button("Stop", buttonStyle, buttonHeight))
            {
                InstanceFinder.ServerManager.StopConnection(true);
            }

            GUILayout.EndHorizontal();

            GUILayout.Box("Advertising", labelStyle);

            GUILayout.BeginHorizontal();

            if (networkDiscovery.IsAdvertising)
            {
                if (GUILayout.Button("Stop", buttonStyle, buttonHeight))
                {
                    networkDiscovery.StopSearchingOrAdvertising();
                }
            }
            else
            {
                if (GUILayout.Button("Start", buttonStyle, buttonHeight))
                {
                    networkDiscovery.AdvertiseServer();
                }
            }

            GUILayout.EndHorizontal();

            GUILayout.Box("Searching", labelStyle);

            GUILayout.BeginHorizontal();

            if (networkDiscovery.IsSearching)
            {
                if (GUILayout.Button("Stop", buttonStyle, buttonHeight))
                {
                    networkDiscovery.StopSearchingOrAdvertising();
                }
            }
            else
            {
                if (GUILayout.Button("Start", buttonStyle, buttonHeight))
                {
                    networkDiscovery.SearchForServers();
                }
            }

            GUILayout.EndHorizontal();

            if (_endPoints.Count > 0)
            {
                GUILayout.Box("Servers", labelStyle);

                _serversListScrollVector = GUILayout.BeginScrollView(_serversListScrollVector);

                foreach (IPEndPoint endPoint in _endPoints)
                {
                    string ipAddress = endPoint.Address.ToString();
                    if (GUILayout.Button(ipAddress, buttonStyle))
                    {
                        networkDiscovery.StopSearchingOrAdvertising();
                        InstanceFinder.ClientManager.StartConnection(ipAddress);
                        TurnOffGUI();
                    }
                }

                GUILayout.EndScrollView();
            }

            GUILayout.EndArea();
        }

        public void TurnOffGUI()
        {
            isGUIActive = false;
        }

        public void TurnOnGUI()
        {
            isGUIActive = true;
        }
	}
}
