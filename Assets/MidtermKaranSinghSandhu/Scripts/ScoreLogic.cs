using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreLogic : MonoBehaviour
{
    public static ScoreLogic instance;
    public TextMeshProUGUI scoreText;
    // audio clip for the score
    public AudioClip scoreSound;

    [SerializeField] private GameObject gameOverPanel;
    // text to show the game is over
    [SerializeField] private GameObject gameOverText;
    // array of all the objects that need to be destroyed when the game is over
    [SerializeField] private GameObject[] objectsToDestroy;
    // setActive(false) the canvas
    [SerializeField] private GameObject canvas;

    [SerializeField] private AudioClip gameOverSound;

    [SerializeField] private GameObject global;
    private  int maxScore = 20;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        // update the score in the UI
        scoreText.text = "Score: "+score.ToString();
        // play a sound effect
        AudioSource.PlayClipAtPoint(scoreSound, Camera.main.transform.position);

        if(score >= maxScore)
        {
            gameOverPanel.SetActive(true);
            gameOverText.SetActive(true);
            canvas.SetActive(false);
            foreach (GameObject obj in objectsToDestroy)
            {
                Destroy(obj);
            }

             // decrease the sound of the background music which is in the global 
            global.GetComponent<AudioSource>().volume = 0.1f;


            AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);
        }
    }
}
