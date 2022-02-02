using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public bool moveFromFactoryFlag = false;
    public bool moveToBackpackFlag = false;
    public bool moveFromBackpackFlag = false;
    public bool moveToFactoryFlag = false;

    public bool moveFlag = false;

    public int type; // 0 - Blue; 1 - Green; 2 - Red; 

    public Vector3 moveTo;

    void Start()
    {

    }


    void FixedUpdate()
    {
        Move();
    }


    public void Move()
    {
        if (moveFromFactoryFlag)
        {
            MoveFromFactory();
        }
        else if (moveToBackpackFlag)
        {
            MoveToBackpack();
        }
        else if (moveFromBackpackFlag)
        {
            MoveFromBackpack();
        }
        else if (moveToFactoryFlag)
        {
            MoveToFactory();
        }
        else
        {
            return;
        }        
    }


    private void MoveFromFactory()
    {
        transform.position = Vector3.Lerp(transform.position, moveTo, 10 * Time.deltaTime);
        if (transform.position == moveTo)
        {
            moveFromFactoryFlag = false;
        }
    }


    private void MoveToBackpack()
    {
        moveTo = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.15f * Player.Instance.backpack.Count, Player.Instance.transform.position.z - 0.7f);
        transform.position = Vector3.Lerp(transform.position, moveTo, 10 * Time.deltaTime);
        if(transform.position == moveTo)
        {
            moveToBackpackFlag = false;
        }
    }


    private void MoveFromBackpack()
    {
        transform.position = Vector3.Lerp(transform.position, moveTo, 10 * Time.deltaTime);
        if (transform.position == moveTo)
        {
            moveFromFactoryFlag = false;
        }
    }

    private void MoveToFactory()
    {
        transform.position = Vector3.Lerp(transform.position, moveTo, 10 * Time.deltaTime);
        if (transform.position == moveTo)
        {
            moveFromFactoryFlag = false;
            Destroy(gameObject);
        }
    }
}
