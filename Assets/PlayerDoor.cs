using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace SuperLabs
{
	public class PlayerDoor : NetworkBehaviour
	{
		private Animator animator;

		private void Start()
		{
			animator = this.GetComponent<Animator>();
		}

		[Server]
		public void Open()
		{
			animator.Play("door_2_open", 0, 0f);
		}

		[Server]
		public void Close()
		{
			animator.Play("door_2_close", 0, 0f);
		}
	}
}