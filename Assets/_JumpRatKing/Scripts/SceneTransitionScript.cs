using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionScript : MonoBehaviour
{
    //the scene loaded by this script must be set inside of the inspector.
    [SerializeField]
    //store the name of the scene we want to load.
    private string _sceneName = "";

    //upon collision with a player, transition the scene if the has been intialized.
    private void OnCollisionEnter(Collision collision)
    {
        //return if the scene is null.
        if (_sceneName == "" || collision.gameObject.TryGetComponent(out PlayerControllerScript script) == false)
            return;

        //find the build index of the scene we want to transition to by first getting the scene and getting its index.
        int buildIndex = SceneManager.GetSceneByName(_sceneName).buildIndex;

        //load the new scene.
        SceneManager.LoadScene(buildIndex);
    }
}
