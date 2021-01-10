using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;

namespace SuperLabs
{
	public class ComputerTask : Interactable
	{
		Dictionary<NetworkConnectionToClient, List<int>> clientNumbers = new Dictionary<NetworkConnectionToClient, List<int>>();

		public override string GetHintText()
		{
			return "Press 'E' to interact.";
		}

		public override void Interate()
		{
			CmdInterate();
		}

		[Command(ignoreAuthority = true)]
		void CmdInterate(NetworkConnectionToClient sender = null)
		{
			if (!clientNumbers.ContainsKey(sender))
			{
				List<int> numbers = new List<int> { Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100) };
				clientNumbers.Add(sender, numbers);
			}

			RpcOnInterate(clientNumbers[sender]);
		}

		[ClientRpc]
		void RpcOnInterate(List<int> numbers)
		{
			TaskCanvas.singleton.StartComputerTask(numbers, this);
		}

		[Command(ignoreAuthority = true)]
		public void CmdSubmit(List<int> answers, NetworkConnectionToClient sender = null)
		{
			// TODO: Process logic to verify
			bool correct = false;

			if (correct)
			{
				// Show transition
				Debug.Log("Correct!");
			}
			else
			{
				// Play sound?
				Debug.Log("Wrong!");
			}
		}
	}
}