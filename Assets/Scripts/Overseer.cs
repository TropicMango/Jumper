using UnityEngine;
using System.Collections;

public class Constants {

    public const string PlayerTag = "Player";

    public const string TileTag = "Tile";
    public const int TileSize = 5;
    public const int TileAltitude = 5;
    public const float TileStep = 2.5F;

    public const int CharacterAltitude = 8;
}

public class Overseer : MonoBehaviour {

    public GameObject floorBlock;
    private Vector3 currentLocation;
    public float cooldown = 0.61F;
    private float nextGen = 0;

    private bool isGameOver = false;

    private GameObject firstBlock;

    void Start () {
        int startingLine = 3;

        for (int i = 0; i < startingLine; i++) {
            currentLocation = new Vector3 (i * Constants.TileSize, Constants.TileAltitude, 0);
            GameObject obj = (GameObject)Instantiate (floorBlock, currentLocation, Quaternion.identity);
           
            if (i == 0) {
                firstBlock = obj;
            }
        }

        for (int i = 0; i < 7; i++) {
            GenerateBlock ();
        }
    }

    void Update () {
        if (isGameOver)
            return;
       
        if (Time.time > nextGen)
            GenerateBlock ();

        if (firstBlock != null)
            firstBlock.transform.Translate (new Vector3 (0, -3 * Time.deltaTime, 0));
    }
       
    void GenerateBlock(){
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
            GenerateBlock ();
        }
    }

}
