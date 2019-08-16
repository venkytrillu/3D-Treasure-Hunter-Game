using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
{
    idle,play,gameover
}
public class Controller : MonoBehaviour
{
    public GameState gameState;
    public static Controller instance;
    public GameObject oxygenPrefab, crystalPrefab;
    public GameObject PanelHome, PanelGamePlay, PanelGameOver;
    public Slider slider;
    public Text scoreTxt,bestScore,oxygenCount,crystalCount,timeTxt;
    int currentScore,oxyCount;
    public int maxOxygens;
    public float Timeleft;
    private GameObject player;
    public Terrain terrain;
    public int numberOfObjects; // number of objects to place
    [HideInInspector]
    public int currentObjects; // number of placed objects
    public GameObject[] objectToPlace; // GameObject to place
    private int terrainWidth; // terrain size (x)
    private int terrainLength; // terrain size (z)
    private int terrainPosX; // terrain position x
    private int terrainPosZ; // terrain position z
    float timeOffset;
    float timeEverySec=1;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }

        bestScore.text = PlayerPrefs.GetInt(Tags.BestScore).ToString();
    }

    private void Start()
    {
        gameState = GameState.idle;
        player = GameObject.Find(Tags.Player);
        timeOffset = Timeleft;
        timeTxt.text = Timeleft.ToString();
        // terrain size x
        terrainWidth = (int)terrain.terrainData.size.x;
        // terrain size z
        terrainLength = (int)terrain.terrainData.size.z;
        // terrain x position
        terrainPosX = (int)terrain.transform.position.x;
        // terrain z position
        terrainPosZ = (int)terrain.transform.position.z;
        DisablePlayerControls();
    }


    private void Update()
    {
        if(gameState == GameState.play)
             Timer();
        // generate objects
        if (currentObjects <= numberOfObjects)
        {
            int randomNum = Random.Range(0, 2);
            PlaceObject(randomNum);
            currentObjects += 1;
        }
        if (currentObjects == numberOfObjects)
        {
            Debug.Log("Generate objects complete!");
        }
    }

    void Timer()
    {
        timeEverySec -= Time.deltaTime;
        if (timeEverySec <= 0)
        {
            Timeleft--;
            timeTxt.text = Timeleft.ToString();
            if (Timeleft < 10)
            {
                timeTxt.color = Color.red;
            }
            timeEverySec = 1;
        }
        
    }

  public void PlaceObject(int randomNum)
    {
        // generate random x position
        int posx = Random.Range(terrainPosX, terrainPosX + terrainWidth);
        // generate random z position
        int posz = Random.Range(terrainPosZ, terrainPosZ + terrainLength);
        // get the terrain height at the random position
        float posy = Terrain.activeTerrain.SampleHeight(new Vector3(posx, 0, posz));
        // create new gameObject on random position
        GameObject newObject = (GameObject)Instantiate(objectToPlace[randomNum], new Vector3(posx, posy, posz), Quaternion.identity);
    }


    void CreateOxygen(Vector3 pos)
    {
      GameObject oxygenObj=  Instantiate(oxygenPrefab, pos, Quaternion.identity);
    }

    void CreateCrystal(Vector3 pos)
    {
        GameObject oxygenObj = Instantiate(crystalPrefab, pos, Quaternion.identity);
    }

    public void SetSlider(float value)
    {
        slider.value = value;
    }


   public void PlyerPref_SetInt( string key,int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public int PlyerPref_GetInt(string key)
    {
       return PlayerPrefs.GetInt(key);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   public void GameOver()
    {
       
        PanelGamePlay.SetActive(false);
        PanelGameOver.SetActive(true);
        GetScore();
    }

    public void GamePlay()
    {
        gameState = GameState.play;
        PanelHome.SetActive(false);
        PanelGamePlay.SetActive(true);
        EnablePlayerControls();
    }

    void DisablePlayerControls()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
    }
    void EnablePlayerControls()
    {
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    public void SetScore(int score)
    {
        currentScore += score;
        crystalCount.text = currentScore.ToString();
        GetScore();
        int bestscore = PlyerPref_GetInt(Tags.BestScore);
        if (currentScore> bestscore)
            PlyerPref_SetInt(Tags.BestScore, currentScore);
    }

    void GetScore()
    {
        scoreTxt.text = currentScore.ToString();
        bestScore.text= PlyerPref_GetInt(Tags.BestScore).ToString();
    }

    public void SetOxygenCount(int count)
    {
        if(GetoxygenCount()<=maxOxygens)
        {
            oxyCount += count;
        }
        if (oxyCount >= maxOxygens)
            oxyCount = maxOxygens;
        print(oxyCount);
        if (oxyCount>=0&& oxyCount<= maxOxygens)
        {
            oxygenCount.text = oxyCount.ToString();
            
        }
    }

    void OxygenCount(int value)
    {
        oxyCount += value;
    }

    public int GetoxygenCount()
    {
        return int.Parse(oxygenCount.text);
    }

    public int GetCrystalCount()
    {
        return int.Parse(crystalCount.text);
    }



} // class
