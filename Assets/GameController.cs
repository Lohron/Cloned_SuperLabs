using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using TMPro;

namespace SuperLabs
{
	public class GameController : NetworkBehaviour
	{
		// Singleton
		private static GameController _instance;
		public static GameController singleton { get { return _instance; } }

		// NetworkManagerCustom
		NetworkManagerCustom networkManagerCustom;

		// Power List
		public List<string> powerList = new List<string> { "Power 1", "Power 2", "Power 3", "Power 4" };
		public List<string> assignedPowers = new List<string> { };

		// Useful Data
		List<NetworkIdentity> players = new List<NetworkIdentity>();
		List<Vector3> spawnLocations = new List<Vector3>();
		List<PlayerDoor> playerDoors = new List<PlayerDoor>();

		// Status
		bool started = false;

		// Game Timer
		bool isDay;
		float timer;

		// Canvas
		public GameObject canvas;
		public GameCanvas gameCanvas;

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

		void OnEnable()
		{
			networkManagerCustom = gameObject.GetComponent<NetworkManagerCustom>();
			canvas = GameObject.FindGameObjectWithTag("Canvas");
			gameCanvas = canvas.GetComponent<GameCanvas>();

			// Get all player doors
			foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("PlayerDoor"))
			{
				playerDoors.Add(gameObject.GetComponent<PlayerDoor>());
			}

			// Get all spawn locations
			foreach (GameObject spawnObject in GameObject.FindGameObjectsWithTag("Respawn"))
			{
				spawnLocations.Add(spawnObject.transform.position);
			}
		}

		void Update()
		{
			if (started)
			{
				timer -= Time.deltaTime;
				
				// Sync client timer every 60s
				if ((int)timer % 60 == 0)
				{
					gameCanvas.timer = timer;
				}

				// Once timer is up
				if (timer <= 0)
				{
					isDay = !isDay;

					// Morning or Night rotine?
					if (isDay)
					{
						OnDayStarted();
					}
					else
					{
						OnNightStarted();
					}
				}
			}
			else
			{
				// Start game when enough players
				if (NetworkManager.singleton.maxConnections == NetworkManager.singleton.numPlayers)
				{
					started = true;
					isDay = true;
					timer = 10; //60 * 3;
					gameCanvas.timer = timer;

					Debug.Log("Game started.");

					OnGameStart();
					OnDayStarted();
				}
			}
		}

		void OnGameStart()
		{
			int spawnCounter = 0;

			// Get all available player
			foreach (KeyValuePair<int, NetworkConnectionToClient> conn in NetworkServer.connections)
			{
				foreach (NetworkIdentity identity in conn.Value.clientOwnedObjects)
				{
					// Add to players list
					players.Add(identity);

					// Give a random power
					string power;
					
					do
					{
						power = powerList[Random.Range(0, powerList.Count)];
					}
					while (assignedPowers.Contains(power));

					assignedPowers.Add(power);
					identity.gameObject.GetComponent<PlayerGameState>().ServerGivePower(power);

					// Assign to a random spawn location
					identity.gameObject.GetComponent<CharacterController>().enabled = false;
					identity.gameObject.transform.position = spawnLocations[spawnCounter++];
					identity.gameObject.GetComponent<CharacterController>().enabled = true;

					// Give everyone starting money
					identity.gameObject.GetComponent<PlayerGameState>().ServerGiveMoney(500);
				}
			}

			// Update clients on assignedPowers sorted.
			gameCanvas.ServerSetOptions(assignedPowers.OrderBy(p => p).ToList());

			// Delagate to gameCanvas
			gameCanvas.RpcOnGameStart();
		}

		void OnDayStarted()
		{
			// Reset timer
			timer = 60 * 3;
			gameCanvas.timer = timer;

			// Open all player doors
			foreach (PlayerDoor door in playerDoors)
			{
				door.Open();
			}
		}

		void OnNightStarted()
		{
			// Night started

			// Reset timer
			timer = 60;
			gameCanvas.timer = timer;
		}

		public void OnWinner(NetworkConnectionToClient winnerConn)
		{
			int spawnCounter = 0;

			foreach (NetworkIdentity player in players)
			{
				// Send winner to victory screen, send loser back to room
				if (player == winnerConn.identity)
				{
					gameCanvas.TargetOnWinner(player.connectionToClient, true);
					player.GetComponent<PlayerGameState>().TargetOnWinner(player.connectionToClient, new Vector3(14, gameObject.transform.position.y, 1));
				}
				else
				{
					gameCanvas.TargetOnWinner(winnerConn, false);
					player.GetComponent<PlayerGameState>().TargetOnWinner(player.connectionToClient, spawnLocations[spawnCounter++]);
				}
			}
		}
	}
}