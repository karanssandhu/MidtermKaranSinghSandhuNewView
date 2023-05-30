using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveScript : MonoBehaviour
{
    public static MoveScript instance;
    // speed of the camera moving from left to right
    public float speed;
    public Animator _anim;
    public bool move = true;
    public bool isJumping = false;
    public bool isFalling = false;
    public bool isSliding = false;
    public bool isDucking = false;
    public bool isFighting = false;
    public bool isAutoPlay = false;
    public bool isGrounded = true;

    [SerializeField] public GameObject _player;
    public AudioClip _failSound;

    public GameObject _collisionPredictor;

    public TextMeshProUGUI achievementText;

    private float velocity;

    private float jumpHeight = 8.0f;

    public float jumpForce = 10f;
    public float jumpDuration = 1f;
    public float jumpDelay = 0.5f;
   
    // side-scrolling view, with a constant speed moving from left to right.
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    void HeroMove(){
        if(move){

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
           
            Jump();

            if(Input.GetKeyDown(KeyCode.LeftShift)){
                isSliding= true;
                StartCoroutine(DelaySlide());
            }

            if(Input.GetKeyDown(KeyCode.LeftControl)){
                isFighting = true;
            }
            
            if(Input.GetKeyUp(KeyCode.LeftControl)){
                isFighting = false;
            }


        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Enemy") {
            if(isFighting) {
                // get parent of the object that collided with the player              
                other.gameObject.transform.parent.gameObject.SetActive(false);

                if(MovePredictionLogic.instance._collisionPredictorList.Count > 0){
                    if(MovePredictionLogic.instance._collisionPredictorList[0].tag == "Enemy"){
                        MovePredictionLogic.instance._collisionPredictorList.RemoveAt(0);
                    }
                }

                achievementText.text = "You just killed " +  other.gameObject.transform.parent.gameObject.name + "!";
                StartCoroutine(RemoveAchievementText());

                return;
            }
            _anim.SetTrigger("Hit");
            _player.transform.position = new Vector3(_anim.transform.position.x, 0, _anim.transform.position.z);
            move = false; 
            LevelStreamer.instance._isEnd = true;
            AudioSource.PlayClipAtPoint(_failSound, transform.position, 0.2f);
            GameManager.instance.EndGame();
        }
        else if (other.gameObject.tag == "Obstacle") {
            if( isJumping) {
                if(MovePredictionLogic.instance._collisionPredictorList.Count > 0){
                    if(MovePredictionLogic.instance._collisionPredictorList[0].tag == "Obstacle"){
                        MovePredictionLogic.instance._collisionPredictorList.RemoveAt(0);
                    }
                }
                return;
            }
            _anim.SetTrigger("Hit");
            _player.transform.position = new Vector3(_anim.transform.position.x, 0, _anim.transform.position.z);
            move = false; 
            LevelStreamer.instance._isEnd = true;
            AudioSource.PlayClipAtPoint(_failSound, transform.position, 0.2f);
            GameManager.instance.EndGame();
        }
        else if (other.gameObject.tag == "SlideObstacle") {
           if(isSliding) {
                if(MovePredictionLogic.instance._collisionPredictorList.Count > 0){
                    if(MovePredictionLogic.instance._collisionPredictorList[0].tag == "SlideObstacle"){
                        MovePredictionLogic.instance._collisionPredictorList.RemoveAt(0);
                    }
                }
                return;
            }
            _anim.SetTrigger("Hit");
            _player.transform.position = new Vector3(_anim.transform.position.x-0.5f, 0, _anim.transform.position.z);
            move = false; 
            LevelStreamer.instance._isEnd = true;
            AudioSource.PlayClipAtPoint(_failSound, transform.position, 0.2f);
            GameManager.instance.EndGame();
        }

    }

    public void Jump(){
        if(isJumping){
                if(isFalling == false){
                    transform.Translate(Vector3.up * jumpHeight * Time.deltaTime, Space.World);
                   if(MovePredictionLogic.instance._collisionPredictorList.Count > 0){
                    if(MovePredictionLogic.instance._collisionPredictorList[0].tag == "Obstacle"){
                        MovePredictionLogic.instance._collisionPredictorList.RemoveAt(0);
                    }
                }
                    StartCoroutine(DelayJump());
                }
                if(isFalling == true){
                    if(transform.position.y >= 1.405f){
                        transform.Translate(Vector3.up * -(jumpHeight) * Time.deltaTime, Space.World);
                    }
                }
            }
    }

    IEnumerator DelayJump(){
        yield return new WaitForSeconds(0.5f);
        isFalling = true;
        yield return new WaitForSeconds(0.5f);
        isJumping = false;
        isFalling = false;
        transform.position = new Vector3(transform.position.x, 1.405f, transform.position.z);
    }

    IEnumerator DelaySlide(){
        yield return new WaitForSeconds(1f);
        isSliding = false;
    }

    IEnumerator RemoveAchievementText(){
        yield return new WaitForSeconds(2f);
        achievementText.text = "";
    }



    // Update is called once per frame
    void Update()
    {
        HeroMove();    
    }
}

















