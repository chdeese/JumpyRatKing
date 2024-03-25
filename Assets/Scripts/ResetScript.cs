using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //loads up the current scene again.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
