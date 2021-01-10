using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperLabs
{
	public class PlayerCamera : MonoBehaviour
	{
		public float mouseSensitivity;
		private Transform playerBody;
		private Transform cameraTransform;

		public void SetPlayer(GameObject player)
		{
			playerBody = player.transform;

			transform.parent = player.transform.Find("CameraLocation");
			transform.localPosition = new Vector3(0, 0, 0);
			transform.localRotation = Quaternion.identity;
		}
	}
}