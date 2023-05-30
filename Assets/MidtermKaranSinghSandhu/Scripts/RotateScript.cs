using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
     public AudioClip collectSound;
    // coin 
    [SerializeField] private float _rotationSpeed = 4f;
    [SerializeField] private GameObject _coin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void coinRotation()
    {
        _coin.transform.Rotate( _rotationSpeed, 0, 0);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            Destroy(gameObject);
            // play collect sound
            AudioSource.PlayClipAtPoint(collectSound, transform.position, 0.2f);   
            // if tag is red coin then add 5 points
            // if tag is blue coin then add 10 points
            if (gameObject.tag == "RedCoin") {
                GameManager.instance.Score += 5;
            } else if (gameObject.tag == "BlueCoin") {
                GameManager.instance.Score += 3;
            }
            GameManager.instance.UpdateScore();
        }
    }

    // Update is called once per frame
    void Update()
    {
        coinRotation();
    }
}
