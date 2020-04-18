using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour {

    public void playm()
    {
        GetComponent<AudioSource> ().Play ();
    }

    void Update()
    {
        if (Input.GetKey (KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
