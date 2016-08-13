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
    public GameObject character;

    private Vector3 currentLocation;
    public float cooldown = 3.0F;
    private float nextGen = 0;

    private bool isGameOver = false;
//    public int score;
//    public Text scoreLabel; 

    private Transform lastEndPoint;
    private GameObject lastChallenge;

    private string[] possibleChallenges = new string[] { "Challenge_1", "Challenge_2" };

//    void Awake() {
//        score = 0;
//        scoreLabel = GetComponent<Text> ();
//    }

    void Start () {
        character.transform.position = new Vector3 (20 * Constants.TileSize, 6, 20 * Constants.TileSize);

        Level level = new Level (40, 40);
        GenerateLevel (level);
    }

    void Update () {
        if (isGameOver)
            return;
       
    }
        
    void GameOver() {
        isGameOver = true;
    }

    void GenerateLevel(Level level) {
        bool[,] grid = level.GetLevel ();
        for (int y = 0; y < level.Height; y++) {
            for(int x = 0; x < level.Width; x++) { // y is z in this case
                if (grid [y, x]) {
                    Vector3 position = new Vector3 (x * Constants.TileSize, Constants.TileAltitude, y * Constants.TileSize);
                    Instantiate (floorBlock, position, Quaternion.identity);
                }
            }
        }
    }

    void OnEnable() {
        CharacterMovement.OnGameOver += GameOver;
    }

    void OnDisable() {
        CharacterMovement.OnGameOver -= GameOver;
    }

    void OnTriggerEnter(Collider other) {
    }

}
