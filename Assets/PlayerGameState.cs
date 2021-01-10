using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

namespace SuperLabs
{
    public class PlayerGameState : NetworkBehaviour
    {
		int money;
		string powerName;

		/* Money */
		[Server]
		public void ServerGiveMoney(int amount)
		{
			money += amount;
			TargetSetMoney(money);
		}

		[TargetRpc]
		void TargetSetMoney(int money)
		{
			this.money = money;
			GameCanvas.singleton.transform.Find("MoneyLabel").GetComponent<TextMeshProUGUI>().text = "$" + money;
		}

		/* POWER */
		[Server]
		public void ServerGivePower(string powerName)
		{
			this.powerName = powerName;
			TargetGivePower(GetComponent<NetworkIdentity>().connectionToClient, powerName);
		}

		[TargetRpc]
		void TargetGivePower(NetworkConnection target, string powerName)
		{
			this.powerName = powerName;
			GameCanvas.singleton.transform.Find("PowerLabel").GetComponent<TextMeshProUGUI>().text = powerName;
		}

		/* END GAME */
		[TargetRpc]
		public void TargetOnWinner(NetworkConnection target, Vector3 position)
		{
			// Change position
			gameObject.transform.position = position;
			gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

			// Set our own model to show
			transform.Find("Model").gameObject.SetActive(true);
		}
	}
}