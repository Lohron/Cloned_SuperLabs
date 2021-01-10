using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperLabs
{
	public class ComputerTaskUI : MonoBehaviour
	{
		private ComputerTask callback;
		RectTransform transformArea;

		private void Start()
		{
			transformArea = transform.Find("Image").GetComponent<RectTransform>();
		}

		public void SetCallback(ComputerTask callback)
		{
			this.callback = callback;
		}

		public void OnButtonSubmit()
		{
			// Get all values
			List<int> answers = new List<int> { 10, 20, 30 };
			callback.CmdSubmit(answers);
		}

		private void Update()
		{
			if (Input.GetMouseButton(0) && !RectTransformUtility.RectangleContainsScreenPoint(transformArea, Input.mousePosition, null))
			{
				gameObject.SetActive(false);
				Cursor.lockState = CursorLockMode.Locked;
			}
		}
	}
}