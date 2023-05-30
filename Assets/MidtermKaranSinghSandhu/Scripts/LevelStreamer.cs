using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// // LevelStreamer:
// It’s up to you how you would like this to work. Consider having a series of
// prefabs for the different world ‘pieces’. Make sure to instantiate enough of them
// so that the player cannot see them ‘pop’ onto screen.
// There should be obstacles that the player MUST jump over to continue. Colliding
// with them will cause game over
// There should be obstacles that the player MUST slide under to continue. Colliding
// with them will cause gameover
// Game should get harder as progress continues… somehow!

public class LevelStreamer : MonoBehaviour
{
    public GameObject[] _levelPrefabs;
    public float _spawnDistance = 41.9f;
    public bool _doCreate = false;  
    public bool _isEnd = false;
    public int Score = 0;
    // count of levels created
    public int _levelCount = 0;

    public GameObject _player;
    public int _playerInitialPos = 0;


    // distance covered by the player
    public int _distanceCovered = 0;
    public TextMeshProUGUI distText;

    public static LevelStreamer instance;

    // Start is called before the first frame update
    void Start()
    {
       _playerInitialPos = (int) _player.transform.position.x;

       if(instance == null)
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
        if(_isEnd == true)
        {
            return;
        }
        
        // create level if player is 400 units away from the last level
        if (!_doCreate && _spawnDistance - _player.transform.position.x < 400)
        {
            StartCoroutine(CreateLevel());
            _doCreate = true;
        }

        // update distance covered
        _distanceCovered = (int)(_player.transform.position.x - _playerInitialPos);
        distText.text = "Distance: " + _distanceCovered.ToString();

        // increase speed of player

        if(_distanceCovered % 100 == 0 && _distanceCovered != 0)
        {
            MoveScript.instance.speed = MoveScript.instance.speed + 0.1f;
        }
        
    }

    IEnumerator CreateLevel()
    {
        
        int index = Random.Range(0, _levelPrefabs.Length);
        GameObject level = Instantiate(_levelPrefabs[index], new Vector3(_spawnDistance, 0, 0), Quaternion.identity);
        // set tag as generated
        level.tag = "Generated";
        _spawnDistance += 41.9f;
        _levelCount++;
        yield return new WaitForSeconds(2);
        _doCreate = false;
    }



}
