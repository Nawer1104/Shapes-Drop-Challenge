using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    int ID;
    public GameObject MergedObject;
    Transform Block1;
    Transform Block2;
    public float Distance;
    public float MergeSpeed;
    public GameObject vfxMerge;
    bool CanMerge;
    bool canCheck = false;

    Vector3 startPos;
    public bool isFalled = false;

    void Start()
    {
        ID = GetInstanceID();
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        MoveTowards();
    }
    public void MoveTowards()
    {
        if (CanMerge)
        {
            transform.position = Vector2.MoveTowards(Block1.position, Block2.position, MergeSpeed);
            if (Vector2.Distance(Block1.position, Block2.position) < Distance)
            {
                if (ID < Block2.gameObject.GetComponent<Merge>().ID) { return; }
                GameObject vfx = Instantiate(vfxMerge, transform.position, Quaternion.identity) as GameObject;
                GameObject O = Instantiate(MergedObject, transform.position, Quaternion.identity) as GameObject;
                O.GetComponent<Rigidbody2D>().gravityScale = 1;
                O.GetComponent<Merge>().isFalled = true;
                Destroy(vfx, 1f);
                Destroy(Block2.gameObject);
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        canCheck = true;
        if (MergedObject == null) return;
        if (collision.gameObject.CompareTag("Block"))
        {
            if (collision.gameObject.GetComponent<SpriteRenderer>().sprite == GetComponent<SpriteRenderer>().sprite)
            {
                Block1 = transform;
                Block2 = collision.transform;
                CanMerge = true;
                Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
                Destroy(GetComponent<Rigidbody2D>());
            }
        }

        if (collision.gameObject.CompareTag("GreenZone") && !isFalled)
        {
            transform.position = startPos;
            canCheck = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("RedZone") && isFalled && canCheck)
        {
            GameManager.Instance.ResetGame();
        }
    }
}