using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace SuperLabs
{
	public class TaskDoor : Interactable
	{
		private Animator animator;

		[SyncVar]
		bool opened = false;

		float countdown;

		private void Start()
		{
			animator = this.GetComponent<Animator>();
		}

		public override string GetHintText()
		{
			if (opened) return "";

			return "Press 'E' to open.";
		}

		public override void Interate()
		{
			CmdInterate();
		}

		[Command(ignoreAuthority = true)]
		void CmdInterate()
		{
			if (opened) return;

			Open();
		}

		[Server]
		public void Open()
		{
			opened = true;
			countdown = 3f;
			animator.Play("door_1_open", 0, 0f);
		}

		public void Update()
		{
			if (!isServer) return;

			if (opened && countdown > 0)
			{
				countdown -= Time.deltaTime;
			}

			if (opened && countdown <= 0)
			{
				Close();
			}
		}

		[Server]
		public void Close()
		{
			opened = false;
			animator.Play("door_1_close", 0, 0f);
		}
	}
}