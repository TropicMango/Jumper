using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Constants {
    public const string PlayerTag = "Player";
	public const int PlayerAltitude = 6;

    public const string TileTag = "Tile";
    public const string EndTileTag = "EndTile";
    public const int TileSize = 5;
    public const int TileAltitude = 5;
    public const float TileStep = 2.5F;

    public const int CharacterAltitude = 8;
}

public class Overseer : MonoBehaviour {

    public GameObject floorBlock;
    private Vector3 currentLocation;
    public float cooldown = 3.0F;
    private float nextGen = 0;

    private bool isGameOver = false;
//    public int score;
//    public Text scoreLabel; 

//    private GameObject firstBlock;
    private Transform lastEndPoint;
    private GameObject lastChallenge;

    private string[] possibleChallenges = new string[] { "Challenge_1", "Challenge_2" };

//    void Awake() {
//        score = 0;
//        scoreLabel = GetComponent<Text> ();
//    }

    void Start () {

        for(int i = 0; i < 5; i++)
            InstantiateRandomChallenge ();

//        int startingLine = 3;
//
//        for (int i = 0; i < startingLine; i++) {
//            currentLocation = new Vector3 ((i * Constants.TileSize) - 1, Constants.TileAltitude, 0);
//            GameObject obj = (GameObject)Instantiate (floorBlock, currentLocation, Quaternion.identity);
//           
//            if (i == 0) {
//                firstBlock = obj;
//            }
//        }
//
//        for (int i = 0; i < 7; i++) {
//            GenerateBlock ();
//        }
    }

    void Update () {
        if (isGameOver)
            return;
       
        if (Time.time > nextGen)
            InstantiateRandomChallenge ();

//        if (firstBlock != null)
//            firstBlock.transform.Translate (new Vector3 (0, -3 * Time.deltaTime, 0));

    }

    void InstantiateChallenge(string challengeName) {
        if (lastChallenge == null) {
            lastChallenge = Instantiate (Resources.Load (challengeName), new Vector3 (-1, 5, 0), Quaternion.identity) as GameObject;
        }

        lastEndPoint = lastChallenge.GetComponentInChildren<Transform> ().Find ("EndTile");

        Vector3 newStartPoint = Vector3.zero;

        if (Random.Range (0, 2) == 0) {
            newStartPoint = new Vector3 (lastEndPoint.position.x + Constants.TileSize,
                Constants.TileAltitude, lastEndPoint.position.z);
        } else {
            newStartPoint = new Vector3 (lastEndPoint.position.x, Constants.TileAltitude, 
                lastEndPoint.position.z + Constants.TileSize);
        }

        lastChallenge = Instantiate (Resources.Load (challengeName), newStartPoint, Quaternion.identity) as GameObject;
        nextGen = Time.time + cooldown;
    }

    void InstantiateRandomChallenge() {
        int randomIndex = Random.Range (0, possibleChallenges.Length);
        InstantiateChallenge (possibleChallenges [randomIndex]);
    }
        
//    public void UpdateScore() {
//        score += 1;
//        scoreLabel.text = "" + score;
//    }
       
    void GenerateBlock() {
        Instantiate (floorBlock, currentLocation, Quaternion.identity);
        if (Random.Range (0, 2) == 0) {
            currentLocation.x += Constants.TileSize;
        } else {
            currentLocation.z += Constants.TileSize;
        }
        nextGen = Time.time + cooldown;
    }

    void GameOver() {
        isGameOver = true;
    }

    void OnEnable() {
        CharacterMovement.OnGameOver += GameOver;
    }

    void OnDisable() {
        CharacterMovement.OnGameOver -= GameOver;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag (Constants.PlayerTag)) {
//            GenerateBlock ();
        }
    }

}
