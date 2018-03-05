using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class LevelLoader : MonoBehaviour {

	public string LevelFile;
	public GameObject PlayerPrefab;
	public GameObject LaserPrefab;
	public GameObject FloorTilePrefab;
	public GameObject WallTilePrefab;
	public GameObject MirrorPrefab;

	public Transform PlayerParent;
	public Transform WallParent;
	public Transform FloorParent;
	public Transform LaserParent;
	public Transform MirrorParent;

	void Awake() {
		if (LevelFile == string.Empty) {
			throw new FileNotFoundException ("Empty string invalid");
		}

		StreamReader reader = new StreamReader(LevelFile, Encoding.Default);
		string line = reader.ReadLine();
		int x = 0;
		int z = 0;
		while (line != null) {
			++z;
			x = 0;
			foreach (char c in line) {
				//Always add a floor tile
				GameObject temp = Instantiate(FloorTilePrefab, new Vector3(x, -0.05f, z), Quaternion.identity);
				temp.transform.parent = FloorParent;
				switch (c) {
				case ' ':
					//Empty floor tile
					break;
				case 'x':
					//Wall tile
					temp = (GameObject)Instantiate (WallTilePrefab, new Vector3 (x, 0.125f, z), Quaternion.identity);
					temp.transform.parent = WallParent;
					break;
				case 'r':
					//Right facing laser
					temp = (GameObject)Instantiate (LaserPrefab, new Vector3 (x, 0.5f, z), Quaternion.identity);
					temp.GetComponent<LaserShooter> ().LaserDirection = Vector3.right;
					temp.transform.parent = LaserParent;
					break;
				case 'l':
					//Left facing laser
					temp = (GameObject)Instantiate (LaserPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
					temp.GetComponent<LaserShooter> ().LaserDirection = Vector3.left;
					temp.transform.parent = LaserParent;
					break;
				case 'u':
					//Up facing laser
					temp = (GameObject)Instantiate (LaserPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
					temp.GetComponent<LaserShooter> ().LaserDirection = Vector3.forward;
					temp.transform.parent = LaserParent;
					break;
				case 'd':
					//Down facing laser
					temp = (GameObject)Instantiate (LaserPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
					temp.GetComponent<LaserShooter> ().LaserDirection = Vector3.back;
					temp.transform.parent = LaserParent;
					break;
				case 'p':
					//Player
					temp = (GameObject)Instantiate (PlayerPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
					temp.transform.parent = PlayerParent;
					break;
				case '/':
					//Mirror one way
					temp = (GameObject)Instantiate (MirrorPrefab, new Vector3(x, 0.5f, z), Quaternion.AngleAxis(-45f, Vector3.up));
					temp.transform.parent = MirrorParent;
					break;
				case '\\':
					//Mirror the other way
					temp = (GameObject)Instantiate (MirrorPrefab, new Vector3(x, 0.5f, z), Quaternion.AngleAxis(45f, Vector3.up));
					temp.transform.parent = MirrorParent;
					break;
				case 'g':
					//Goal
					break;
				default:
					throw new KeyNotFoundException("Character '" + c + "' is not a valid input.");
				}
				++x;
			}
			line = reader.ReadLine();
		}


	}
}
