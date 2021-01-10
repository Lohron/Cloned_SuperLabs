using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace SuperLabs
{
	public class NetworkManagerCustom : NetworkManager
	{
		public override void OnServerSceneChanged(string sceneName)
		{
			if (sceneName == "Assets/Scenes/SnowLab.unity")
			{
				GetComponent<GameController>().enabled = true;
			}
		}
	}
}