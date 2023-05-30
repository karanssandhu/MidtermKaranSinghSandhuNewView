using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerLogic : MonoBehaviour
{
    public static DestroyerLogic instance;
    public GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // get all the objects that are tagged as "Generated"
        GameObject[] generatedObjects = GameObject.FindGameObjectsWithTag("Generated");
        // loop through all the objects
        foreach(GameObject obj in generatedObjects){
            // if the object is behind the player
            if(obj.transform.position.x + 200 < _player.transform.position.x){
                // destroy the object
                Destroy(obj);
            }
        }
        
    }
}
