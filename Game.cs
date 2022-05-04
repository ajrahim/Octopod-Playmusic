using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
// using GoogleMobileAds.Api;

public class Game : MonoBehaviour{

    // private BannerView bannerView;
    public GameObject SecondChanceButton;

    public GameObject Player;
    public GameObject Grid;
    public GameObject Base;
    public GameObject GameOver;

    public GameObject EdgeCorner;
    public GameObject EdgeTop;
    public GameObject EdgeBottom;
    public GameObject EdgeLeft;
    public GameObject EdgeRight;

    public float GridSizeX = 10f;
    public float GridSizeY = 10f;
    public int OrbitCount = 20;

    public float MinSpawnTimeX = 0.1f;
    public float MaxSpawnTimeX = 0.3f;
    public float MinSpawnTimeY = 0.1f;
    public float MaxSpawnTimeY = 0.3f;
    private float SpawnTimeX = 0f;
    private float CurrentTimeX = 0f;
    private float SpawnTimeY = 0f;
    private float CurrentTimeY = 0f;

    public int Score = 0;
    public Text ScoreText;
    public TextMeshProUGUI ScoreTextMesh;

    public bool Playing = true;
    public AudioSource Pop;
    public AudioSource GameOverSound;
    public GameObject Fireworks;

    public GameObject MENU_SETTINGS;


    public PlusMusic_DJ theDJ;
    enum PlayState {start, play};
    PlayState theState = new PlayState();

    void Start(){

        float GridPositionX = -(GridSizeX / 2);
        float GridPositionY = -(GridSizeY / 2);

        for(float x = 0f; x < GridSizeX; x++){
            for(float y = 0f; y < GridSizeY; y++){           
                if(x == 0 && y == 0){
                    GameObject Object = Instantiate(EdgeCorner, new Vector3(x * 1f, y * 1f, 0f), Quaternion.identity);
                    Object.transform.parent = Grid.transform;
                }else if (x == (GridSizeX - 1) && y == 0){
                    GameObject Object = Instantiate(EdgeCorner, new Vector3(x * 1f, y * 1f, 0f), Quaternion.identity);
                    Object.transform.parent = Grid.transform;
                }else if(x == 0 && y == (GridSizeY - 1)){
                    GameObject Object = Instantiate(EdgeCorner, new Vector3(x * 1f, y * 1f, 0f), Quaternion.identity);
                    Object.transform.parent = Grid.transform;
                }else if(x == (GridSizeX - 1) && y == (GridSizeY - 1)){
                    GameObject Object = Instantiate(EdgeCorner, new Vector3(x * 1f, y * 1f, 0f), Quaternion.identity);
                    Object.transform.parent = Grid.transform;
                }else if(y == 0 && x > 0 && x < (GridSizeX - 1)){
                    GameObject Object = Instantiate(EdgeBottom, new Vector3(x * 1f, y * 1f, 0f), Quaternion.identity);
                    Object.transform.parent = Grid.transform;
                }else if(y == (GridSizeY - 1) && x > 0 && x < (GridSizeX - 1)){
                    GameObject Object = Instantiate(EdgeTop, new Vector3(x * 1f, y * 1f, 0f), Quaternion.identity);
                    Object.transform.parent = Grid.transform;
                }else if(x == 0 && y > 0 && y < (GridSizeY - 1)){
                    GameObject Object = Instantiate(EdgeLeft, new Vector3(x * 1f, y * 1f, 0f), Quaternion.identity);
                    Object.transform.parent = Grid.transform;
                }else if(x == (GridSizeX - 1) && y > 0 && y < (GridSizeY - 1)){
                    GameObject Object = Instantiate(EdgeRight, new Vector3(x * 1f, y * 1f, 0f), Quaternion.identity);
                    Object.transform.parent = Grid.transform;
                }else{
                    // GameObject Object = Instantiate(Base, new Vector3(x * 1f, y * 1f, 0f), Quaternion.identity);
                    // Object.transform.parent = Grid.transform;
                }
            }   
        }
        
        for(var z = 0; z < OrbitCount; z++){
            GameObject OrbitObject = Instantiate(Resources.Load("Orbit") as GameObject);
            OrbitObject.transform.position = new Vector3(Random.Range(2.5f, (GridSizeX - 2.5f)), Random.Range(2.5f, (GridSizeY - 2.5f)), 0f);
            OrbitObject.transform.parent = Grid.transform;
        }

        Grid.transform.position = new Vector3(GridPositionX, GridPositionY, 1f);

        SetRandomTimeY();
        SetRandomTimeX();

        string[] SongArray = new string[] { "347", "351", "350", "348", "349", "352", "353", "354", "355", "356", "357", "358", "359", "360" };
        theDJ.ChangeSoundtrack(SongArray[Random.Range(0,SongArray.Length)], true);
        theState = PlayState.start;

    }

    void Update(){
        
        if(theDJ.AllFilesLoaded()){
            if(theState == PlayState.start){
                theState = PlayState.play;
                theDJ.PlaySoundPM("backing_track", "hardstop", "beats", false);
            }
        }
        

        if(Playing){
            CurrentTimeX += Time.deltaTime;
            CurrentTimeY += Time.deltaTime;

            if(CurrentTimeX >= SpawnTimeX){
                SpawnBladeX();
                SetRandomTimeX();
            }

            if(CurrentTimeY >= SpawnTimeY){
                SpawnBladeY();
                SetRandomTimeY();
            }
        }
        
    }

    void SpawnBladeX(){
        CurrentTimeX = 0f;
        GameObject Blade = Instantiate(Resources.Load("Blade") as GameObject);
        Blade.GetComponent<Blade>().Direction = "Left";
        // Blade.transform.position = new Vector3(((GridSizeY / 2f) * 1.5f) + WallDistance - 0.25f + BladeDistance, 5f, Random.Range(-(((GridSizeY * 1.5f) / 2f) - (1.5f / 2)), (((GridSizeY * 1.5f) / 2f) - (1.5f / 2))));

        float GridSizeMaxY = (GridSizeY / 2);
        float GridSizeMaxX = (GridSizeX / 2);
        Blade.transform.position = new Vector3(GridSizeMaxX, Random.Range(-GridSizeMaxY, GridSizeMaxY), 0f);

    }

    void SetRandomTimeX(){
        SpawnTimeX = Random.Range(MinSpawnTimeX, MaxSpawnTimeX);
    }

    void SpawnBladeY(){
        CurrentTimeY = 0f;
        GameObject Blade = Instantiate(Resources.Load("Blade") as GameObject);
        Blade.GetComponent<Blade>().Direction = "Down";
        float GridSizeMaxY = (GridSizeY / 2);
        float GridSizeMaxX = (GridSizeX / 2);
        Blade.transform.position = new Vector3(Random.Range(-GridSizeMaxX, GridSizeMaxX), GridSizeMaxY, 0f);

    }

    void SetRandomTimeY(){
        SpawnTimeY = Random.Range(MinSpawnTimeY, MaxSpawnTimeY);
    }

    public void AddPoint(){
        if(Playing){

            GameObject Fireworks = Instantiate(Resources.Load("Spark") as GameObject);
            Fireworks.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, 0f);
            Pop.GetComponent<AudioSource>().Play(0);

            Score += 1;
            ScoreText.text = Score.ToString();
            ScoreTextMesh.GetComponent<TextMeshProUGUI>().SetText(Score.ToString());
            GameObject OrbitObject = Instantiate(Resources.Load("Orbit") as GameObject);
            OrbitObject.transform.parent = Grid.transform;
            float GridXSpawn = (GridSizeX / 2) - 2.5f;
            float GridYSpawn = (GridSizeY / 2) - 2.5f;
            OrbitObject.transform.position = new Vector3(Random.Range(-GridXSpawn, GridXSpawn), Random.Range(-GridYSpawn, GridYSpawn), 0f);
        }
    }

    public void Finished(){
        Playing = false;
        GameOver.SetActive(true);
        GetComponent<AudioSource>().Pause();
        GameOverSound.GetComponent<AudioSource>().Play(0);


        if(theDJ.AllFilesLoaded()){
            theDJ.PlaySoundPM("failure", "hardstop", "beats", false);
        }
        

        Fireworks = Instantiate(Resources.Load("Fireworks") as GameObject);
        Fireworks.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, 0f);

        // Destroy(Player);
    }
    
    public void Restart(){
        SceneManager.LoadScene("Game");
    }

    public void MainMenu(){
        SceneManager.LoadScene("Main Menu");
    }

    public void MENU_SHOW(){
        MENU_SETTINGS.SetActive(true);
        Playing = false;
    }

    public void MENU_HIDE(){
        MENU_SETTINGS.SetActive(false);
        Playing = true;
    }

}
