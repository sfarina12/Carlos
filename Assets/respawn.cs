using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawn : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(LoadAsyncronously(0));
    }

    IEnumerator LoadAsyncronously(int levelIndex)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(levelIndex);

        while (!op.isDone)
        {
            Debug.Log(op.progress);

            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
