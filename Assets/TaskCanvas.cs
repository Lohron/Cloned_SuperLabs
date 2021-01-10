using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace SuperLabs
{
	public class TaskCanvas : MonoBehaviour
	{
		// Singleton
		public static TaskCanvas singleton { get { return _instance; } }
		private static TaskCanvas _instance;

		GameObject computerTask;

		private void Awake()
		{
			if (_instance != null && _instance != this)
			{
				Destroy(this.gameObject);
			}
			else
			{
				_instance = this;
			}
		}

		private void Start()
		{
			computerTask = transform.Find("ComputerTask").gameObject;
		}

		public void StartComputerTask(List<int> numbers, ComputerTask callback)
		{
			computerTask.GetComponentInChildren<TextMeshProUGUI>().text = numbers[0] + "   " + numbers[1] + "   " + numbers[2];
			computerTask.SetActive(true);
			computerTask.GetComponent<ComputerTaskUI>().SetCallback(callback);

			Cursor.lockState = CursorLockMode.None;
		}
	}
}