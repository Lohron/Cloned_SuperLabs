using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using SuperLabs;

namespace SuperLabs
{
	public class PlayerMovement : NetworkBehaviour
	{
		public float speed = 12f;
		float gravity;
		float turnSmoothTime = 0.1f;
		float turnSmoothVelocity;

		CharacterController controller;
		GameObject model;

		public override void OnStartAuthority()
		{
			controller = GetComponent<CharacterController>();
			model = transform.Find("Model").gameObject;
			GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
			camera.GetComponent<PlayerCamera>().SetPlayer(this.gameObject);
		}

		// Update is called once per frame
		void Update()
		{
			if (!hasAuthority) return;

			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");

			gravity -= 9.81f * Time.deltaTime;

			Vector3 move = new Vector3(Input.GetAxis("Vertical"), gravity, -Input.GetAxis("Horizontal"));

			controller.Move(move * speed * Time.deltaTime);

			// Rotation
			if (controller.velocity != Vector3.zero)
				model.transform.rotation = Quaternion.Slerp(model.transform.rotation, Quaternion.LookRotation(controller.velocity), 0.15f);
		}
	}
}