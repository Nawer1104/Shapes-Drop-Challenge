using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> blocks;

    public Transform spawnPoint;

    private GameObject currentBlock;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SpawnObject();
    }

    public void SpawnObject()
    {
        StartCoroutine(Wait(1));
    }

    public GameObject GetCurrentBlock()
    {
        return currentBlock;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Wait(float delay)
    {
        currentBlock = null;

        yield return new WaitForSeconds(delay);

        GameObject gameObject = Instantiate(blocks[Random.Range(0, blocks.Count)], spawnPoint.position, Quaternion.identity) as GameObject;
        currentBlock = gameObject;
    }
}