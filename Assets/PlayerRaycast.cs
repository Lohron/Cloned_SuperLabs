using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace SuperLabs
{
	public class PlayerRaycast : NetworkBehaviour
	{
		private List<GameObject> interactables = new List<GameObject>();

		public float useDistance;

		public override void OnStartAuthority()
		{
			foreach (GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)))
			{
				if (go.layer == LayerMask.NameToLayer("Intractable"))
				{
					interactables.Add(go);
				}
			}
		}

		private void Update()
		{
			if (!hasAuthority) return;

			foreach (GameObject go in interactables)
			{
				if (Vector3.Distance(transform.position, go.transform.position) < useDistance)
				{
					GameCanvas.singleton.SetHintLabel(go.GetComponent<Interactable>().GetHintText());

					if (Input.GetKeyDown("e"))
					{
						go.GetComponent<Interactable>().Interate();
					}

					break;
				}
				else
				{
					GameCanvas.singleton.SetHintLabel("");
				}
			}
		}
	}
}