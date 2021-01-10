using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

namespace SuperLabs
{
	public class GameCanvas : NetworkBehaviour
	{
		// Singleton
		public static GameCanvas singleton { get { return _instance; } }
		private static GameCanvas _instance;

		// Sync variables
		[SyncVar(hook = nameof(SetPlayerCounter))]
		int playerCounter = 0;

		[SyncVar(hook = nameof(SetTimer))]
		public float timer = 0;
		bool startCountdown = false;

		// References
		TextMeshProUGUI playerCounterUI;
		TextMeshProUGUI timerUI;
		TextMeshProUGUI hintLabel;
		TextMeshProUGUI victoryLabel;
		public GameObject noteUI;

		// Prefabs
		public GameObject notePlayerRow_prefab;

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
			playerCounterUI = transform.Find("PlayerCounter").GetComponent<TextMeshProUGUI>();
			timerUI = transform.Find("GameTimer").GetComponent<TextMeshProUGUI>();
			hintLabel = transform.Find("HintLabel").GetComponent<TextMeshProUGUI>();
			victoryLabel = transform.Find("VictoryLabel").GetComponent<TextMeshProUGUI>();
			noteUI = transform.Find("NoteUI").gameObject;
		}

		private void Update()
		{
			if (isServer)
			{
				if (NetworkManager.singleton.numPlayers != playerCounter)
				{
					playerCounter = NetworkManager.singleton.numPlayers;
				}
			}
			
			if (isClient)
			{
				ClientUpdate();
			}
		}

		[Client]
		void ClientUpdate()
		{
			if (startCountdown)
			{
				timer -= Time.deltaTime;
			}

			// If 'Q' pressed, show note ui
			if (Input.GetKeyDown("q"))
			{
				noteUI.SetActive(!noteUI.activeSelf);
			}

			// If Esc is pressed, quit game
			if (Input.GetKey("escape"))
			{
				Application.Quit();
			}
		}

		// Player Counter
		public void SetPlayerCounter(int prevNumber, int newNumber)
		{
			if (playerCounterUI is null) return;

			playerCounterUI.text = "Waiting " + newNumber + "/4 Players";
		}

		// SetTimer
		void SetTimer(float prevNumber, float newNumber)
		{
			int timeInSecondsInt = (int)newNumber;
			int minutes = (int)newNumber / 60;
			int seconds = timeInSecondsInt - (minutes * 60);
			timerUI.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");

			startCountdown = true;
		}

		// Power Label
		void SetPowerLabel(string prevPower, string newPower)
		{
			transform.Find("PowerLabel").GetComponent<TextMeshProUGUI>().text = newPower;
		}

		// Set noteUI Options
		[Server]
		public void ServerSetOptions(List<string> assignedPowers)
		{
			RpcSetOptions(assignedPowers);
		}

		[ClientRpc]
		void RpcSetOptions(List<string> assignedPowers)
		{
			List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
			List<GameObject> notePlayerRows = new List<GameObject>();

			// Generate Dropdown UI
			foreach (string power in assignedPowers)
			{
				options.Add(new Dropdown.OptionData(power));
			}

			// Create player section
			for (int count = 0; count < assignedPowers.Count; count++)
			{
				GameObject notePlayerRow = Instantiate(notePlayerRow_prefab, noteUI.transform.Find("PlayerSection"));

				notePlayerRow.transform.Find("PlayerName").GetComponent<TextMeshProUGUI>().text = "Player " + count;

				Dropdown dropdown = notePlayerRow.transform.Find("Dropdown").GetComponent<Dropdown>();
				dropdown.ClearOptions();
				dropdown.AddOptions(options);
			}
		}

		// SetHintText
		public void SetHintLabel(string text)
		{
			hintLabel.text = text;
		}

		[ClientRpc]
		public void RpcOnGameStart()
		{
			playerCounterUI.text = "";
		}

		[TargetRpc]
		public void TargetOnWinner(NetworkConnection target, bool win)
		{
			victoryLabel.text = win ? "Victory!" : "Game Over!";

			// Cameras
			GameObject victoryCamera = GameObject.FindGameObjectWithTag("VictoryCamera");
			GameObject playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
			playerCamera.GetComponent<Camera>().enabled = false;
			playerCamera.GetComponent<AudioListener>().enabled = false;
			victoryCamera.GetComponent<Camera>().enabled = true;
			victoryCamera.GetComponent<AudioListener>().enabled = true;
		}
	}
}