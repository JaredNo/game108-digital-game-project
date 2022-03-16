using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levelloader : MonoBehaviour
{
    public Animator transistion;
    public float transistionTime = 0.5f;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel("Main"));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transistion.SetTrigger("Start");

        yield return new WaitForSeconds(transistionTime);

        SceneManager.LoadScene(sceneName);
    }
}
