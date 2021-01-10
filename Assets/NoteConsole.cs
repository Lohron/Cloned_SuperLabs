using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

namespace SuperLabs
{
	public class NoteConsole : Interactable
	{
		public override string GetHintText()
		{
			return "Press 'E' to validate your notebook.";
		}

		public override void Interate()
		{
			// Get noteUI
			GameObject noteUI = GameCanvas.singleton.noteUI;
			GameObject playerSection = noteUI.transform.Find("PlayerSection").gameObject;
			Dropdown[] dropdowns = playerSection.GetComponentsInChildren<Dropdown>();

			// Get as a list
			List<int> answers = dropdowns.Select(d => d.value).ToList();

			CmdInterate(answers);
		}

		[Command(ignoreAuthority = true)]
		void CmdInterate(List<int> answers, NetworkConnectionToClient sender = null)
		{
			bool correct = true;
			List<string> assignedPowers = GameController.singleton.assignedPowers;
			List<string> assignedPowersSorted = assignedPowers.OrderBy(p => p).ToList();

			if (answers.Count != assignedPowers.Count) return;

			for (int count = 0; count < assignedPowers.Count; count++)
			{
				if (assignedPowersSorted[answers[count]] != assignedPowers[count])
				{
					correct = false;
					break;
				}
			}

			if (correct)
			{
				GameController.singleton.OnWinner(sender);
			}
		}
	}
}