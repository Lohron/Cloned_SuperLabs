using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace SuperLabs
{
	public class Interactable : NetworkBehaviour
	{
		public virtual string GetHintText()
		{
			return "Press 'E' to use.";
		}

		public virtual void Interate()
		{
		}
	}
}