using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovePredictionLogic : MonoBehaviour
{


    public List<GameObject> _collisionPredictorList = new List<GameObject>();
    public TextMeshProUGUI _warningText;

    public static MovePredictionLogic instance;
    
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
       
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "RedCoin" || other.gameObject.tag == "BlueCoin"){
            return;
        }
        _collisionPredictorList.Add(other.gameObject);
        
    }
    
    void PredictionUpate() {
        if (_collisionPredictorList == null) {
            return;
        }
        while (_collisionPredictorList.Count > 0 && _collisionPredictorList[0] == null) {
            _collisionPredictorList.RemoveAt(0);
        }       
        if (_collisionPredictorList.Count == 0) {
            _warningText.text = "Updating... ";
            return;
        }
        if (_collisionPredictorList[0].tag == "Obstacle") {
            _warningText.text = "Jump - Space";
        }
        if (_collisionPredictorList[0].tag == "Enemy") {
            _warningText.text = "Fight - Left Ctrl";
        }
        if (_collisionPredictorList[0].tag == "SlideObstacle") {
            _warningText.text = "Slide - Left Shift";
        }
    }

    void ColliderMoveLogic(){
        // such that it is always in front of the player 
        // and it is always in the same direction as the player
        // and it is always at the same height as the player
        // and it is always at the same distance from the player

        // if the player is moving forward then the collider should also move forward
        
        gameObject.transform.position = new Vector3(
            MoveScript.instance._player.transform.position.x+5,
            gameObject.transform.position.y,
            MoveScript.instance._player.transform.position.z
        );

    

    }

    // Update is called once per frame
    void Update()
    {
        if(MoveScript.instance.move == false){
            _warningText.text = "You Lost!";
            return;
        }
        PredictionUpate();
        ColliderMoveLogic();

    }
}
