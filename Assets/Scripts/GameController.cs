using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private static GameController _instance;
    public static GameController GetInstance() {
        return GameController._instance;
    }

    public GameObject WhiteScaryImage;
    public GameObject BlackScaryImage;

    public AudioClip hitClip;
    public AudioClip screamClip;
    public AudioClip zombieClip;

    private AudioSource audio;



    // Start is called before the first frame update
    void Start()
    {
        GameController._instance = this;
        WhiteScaryImage.SetActive(false);
        BlackScaryImage.SetActive(false);
        var playerAnimController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        playerAnimController.SetTrigger("StartEngine");
        audio = GetComponent<AudioSource>();
    }

    public void HitSome()
    {
        audio.PlayOneShot(hitClip,1);
    }
    public void TurnOnScary()
    {
        audio.PlayOneShot(screamClip,1);
        WhiteScaryImage.SetActive(true);
    }
    public void TurnOffScary()
    {
        WhiteScaryImage.SetActive(false);
    }

    public void SetGameOver()
    {
        audio.PlayOneShot(zombieClip, 1);
        WhiteScaryImage.SetActive(false);
        BlackScaryImage.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
