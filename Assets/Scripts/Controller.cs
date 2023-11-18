using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG.Infrastructure.Utils.Swipe;

public class Controller : MonoBehaviour
{
    [SerializeField] private SwipeListener swipeListener;

    private void OnEnable()
    {
        swipeListener.OnSwipe.AddListener(OnSwipe);
    }

    private void OnSwipe(string swipe)
    {
        GameObject currentBlock = GameManager.Instance.GetCurrentBlock();
        if (currentBlock == null) return;
        switch (swipe)
        {
            case "Left":
                currentBlock.transform.position -= new Vector3(0.5f, 0f, 0f);
                break;
            case "Right":
                currentBlock.transform.position += new Vector3(0.5f, 0f, 0f);
                break;
            case "Up":
                break;
            case "Down":
                GameManager.Instance.GetCurrentBlock().GetComponent<Rigidbody2D>().gravityScale = 1;
                GameManager.Instance.GetCurrentBlock().GetComponent<Merge>().isFalled = true;
                GameManager.Instance.SpawnObject();
                break;
        }
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }
}
