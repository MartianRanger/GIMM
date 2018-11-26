using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GUIText gameOverText;
    public GameObject enemy;

    public Camera failCam;
    public static GameManager instance = null;
    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        gameOverText.text = " ";
        failCam.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            playerLose();
        }

        if (enemy == null)
        {
            playerWin();
        }
    }
    private void playerWin()
    {
        Debug.Log("YOU WON!");
        //gameOverText.text = "YOU WIN!";
        //SceneManager.LoadScene("Main");

        StartCoroutine("waitAndLoad", 2.0f);
    }
    private void playerLose()
    {
        Debug.Log("SHIOW");
        //gameOverText.text = "YOU LOSE!";
        GameObject.Destroy(player);
	    failCam.enabled = true;
        StartCoroutine("waitAndLoad", 2.0f);
    }

    IEnumerator waitAndLoad()
    {
        SceneManager.LoadScene("Main"); //load scene

        yield return new WaitForSeconds(.02f); //wait for load to happen

        //Get references
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        gameOverText.text = " ";

    }
}

