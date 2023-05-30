using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{

    public GameObject cam1;
    public GameObject cam2;

    // Start is called before the first frame update
    void Start()
    {
        cam2.SetActive(false);
    }

    void CameraChange()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (cam1.activeSelf)
            {
                cam1.SetActive(false);
                cam2.SetActive(true);
            }
            else
            {
                cam1.SetActive(true);
                cam2.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CameraChange();
    }
}
