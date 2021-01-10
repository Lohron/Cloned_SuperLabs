using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
	// Prefabs
	public GameObject basic_floor;

	// Parents
	public GameObject floors;

	private void Start()
	{
		GenerateWorld(1);

	}
	void GenerateWorld(int seed)
	{
		Random.InitState(seed);

		Vector3 startPosition = new Vector3(12, 0, -12);
		Vector3 appendLength = new Vector3(4, -0, -4);
		GenerateLobby(startPosition, appendLength, 10, 15, 2, 4, 0);
	}

	void GenerateLobby(Vector3 startPosition, Vector3 appendLength, int minLength, int maxLength, int minBranch, int maxBranch, int branchNumber)
	{
		Debug.Log("Generating new lobby from :" + startPosition);

		List<Vector3> floorPositions = new List<Vector3>();

		// Create Lobby
		for (int length = 0; length < Random.Range(minLength, maxLength); length++)
		{
			for (int width = 0; width < 2; width++)
			{
				// Create floor from Vector3(12, 0, -8)
				GameObject floor = Object.Instantiate(basic_floor, floors.transform);

				if (branchNumber % 2 == 0)
				{
					floor.transform.position = new Vector3(appendLength.x * width + startPosition.x, 0, appendLength.z * length + startPosition.z);
					
				}
				else
				{
					floor.transform.position = new Vector3(appendLength.x * length + startPosition.x, 0, appendLength.z * width + startPosition.z);
				}

				floorPositions.Add(floor.transform.position);
			}
		}

		if (branchNumber >= 2)
			return;

		// Get amount of branch
		int branchCount = Random.Range(minBranch, maxBranch);
		List<Vector3> branchPositions = new List<Vector3>();

		// Get branch location
		for (int count = 0; count < branchCount; count++)
		{
			branchPositions.Add(floorPositions[Random.Range(0, floorPositions.Count)]);
		}

		// Delete branch if too near
		List<Vector3> safeBranchPositions = branchPositions.ConvertAll(vector => vector);
		foreach (Vector3 branchPosition in branchPositions)
		{
			foreach (Vector3 branchPosition2 in branchPositions)
			{
				if (branchPosition == branchPosition2) continue;

				Debug.Log("Distance of " + branchPosition + " and " + branchPosition2 + " is " + Vector3.Distance(branchPosition, branchPosition2));

				if (Vector3.Distance(branchPosition, branchPosition2) <= 8.0)
				{
					// Add to delete list
					branchPositions.Remove(branchPosition);
				}
			}
		}

		// Generate Branch
		foreach (Vector3 branchPosition in branchPositions)
		{
			Vector3 subAppendLength = new Vector3(4, 0, -4);

			GenerateLobby(branchPosition, subAppendLength, 10, 20, 0, 2, branchNumber + 1);
		}
	}
}
