using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public int Score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalDistText;
    public TextMeshProUGUI finalTimeText;
    public GameObject endPanel;
    public GameObject gamePanel;
    private bool _isEnd = false;

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
    public void StartGame(){
        MoveScript.instance.move = true;
    }

    public void Restart(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void EndGame(){
        MoveScript.instance.move = false;
        finalScoreText.text = "Score: "+Score.ToString();
        finalDistText.text = "Distance: "+LevelStreamer.instance._distanceCovered.ToString();
        finalTimeText.text = timeText.text;
        _isEnd = true;
        endPanel.SetActive(true);
        gamePanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(_isEnd == true)
        {
            return;
        }
        timeText.text = "Time: " +(int)Time.timeSinceLevelLoad + "s " + Time.timeSinceLevelLoad.ToString("f2").Split('.')[1] + "ms";
    }

    // update the score in the UI
    public void UpdateScore()
    {
        scoreText.text = "Score: "+Score.ToString();
        
    }
}
