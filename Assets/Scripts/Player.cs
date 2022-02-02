using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private float speed = 0.5f;

    private int backpackCapacity = 5;
    public List<GameObject> backpack = new List<GameObject>();


    void Start()
    {
        Instance = this;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }


    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(VirtualJoystick.direction * speed, ForceMode.VelocityChange);
        if (VirtualJoystick.direction == Vector3.zero)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "BlueForRed":
                {
                    BlueForRed();
                    break;
                }
            case "BlueCreated":
                {
                    GetBlueProduct();
                    break;
                }
            case "GreenForRed":
                {
                    GreenForRed();
                    break;
                }
            case "GreenCreated":
                {
                    GetGreenProduct();
                    break;
                }
            case "RedCreated":
                {
                    GetRedProduct();
                    break;
                }
            case "BlueForGreen":
                {
                    BlueForGreen();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }


    // Подбор синего ресурса
    private void GetBlueProduct()
    {
        if ((backpack.Count < backpackCapacity) && (GameManager.Instance.Blue.Count > 0))
        {
            backpack.Add(GameManager.Instance.Blue[GameManager.Instance.Blue.Count - 1]);
            GameManager.Instance.Blue.RemoveAt(GameManager.Instance.Blue.Count - 1);
            backpack[backpack.Count - 1].GetComponent<Product>().moveToBackpackFlag = true;
            backpack[backpack.Count - 1].transform.SetParent(gameObject.transform);
        }
    }


    // Подбор зеленого ресурса
    private void GetGreenProduct()
    {
        if ((backpack.Count < backpackCapacity) && (GameManager.Instance.Green.Count > 0))
        {
            backpack.Add(GameManager.Instance.Green[GameManager.Instance.Green.Count - 1]);
            GameManager.Instance.Green.RemoveAt(GameManager.Instance.Green.Count - 1);
            backpack[backpack.Count - 1].GetComponent<Product>().moveToBackpackFlag = true;
            backpack[backpack.Count - 1].transform.SetParent(gameObject.transform);
        }
    }


    // Подбор красного ресурса
    private void GetRedProduct()
    {
        if ((backpack.Count < backpackCapacity) && (GameManager.Instance.Red.Count > 0))
        {
            backpack.Add(GameManager.Instance.Red[GameManager.Instance.Red.Count - 1]);
            GameManager.Instance.Red.RemoveAt(GameManager.Instance.Red.Count - 1);
            backpack[backpack.Count - 1].GetComponent<Product>().moveToBackpackFlag = true;
            backpack[backpack.Count - 1].transform.SetParent(gameObject.transform);
        }
    }


    // Выгрузка на склад синих у зеленой фабрики
    private void BlueForGreen()
    {
        if ((GameManager.Instance.BlueForGreen.Count < GameManager.Instance.storageCapacity) & (backpack.Count > 0))
        {
            for (int i = 0; i < backpack.Count; i++)
            {
                if(backpack[i].GetComponent<Product>().type == 0)
                {
                    GameManager.Instance.BlueForGreen.Add(backpack[i]);
                    backpack.RemoveAt(i);
                    GameManager.Instance.BlueForGreen[GameManager.Instance.BlueForGreen.Count - 1].transform.SetParent(null);
                    GameManager.Instance.BlueForGreen[GameManager.Instance.BlueForGreen.Count - 1].GetComponent<Product>().moveTo = new Vector3(15f, 0.1f + GameManager.Instance.BlueForGreen.Count * 0.15f, 1.5f);
                    GameManager.Instance.BlueForGreen[GameManager.Instance.BlueForGreen.Count - 1].GetComponent<Product>().moveFromBackpackFlag = true;
                    return;
                }
            }
        }
    }


    // Выгрузка на склад синих у красной фабрики
    private void BlueForRed()
    {
        if ((GameManager.Instance.BlueForRed.Count < GameManager.Instance.storageCapacity) & (backpack.Count > 0))
        {
            for (int i = 0; i < backpack.Count; i++)
            {
                if (backpack[i].GetComponent<Product>().type == 0)
                {
                    GameManager.Instance.BlueForRed.Add(backpack[i]);
                    backpack.RemoveAt(i);
                    GameManager.Instance.BlueForRed[GameManager.Instance.BlueForRed.Count - 1].transform.SetParent(null);
                    GameManager.Instance.BlueForRed[GameManager.Instance.BlueForRed.Count - 1].GetComponent<Product>().moveTo = new Vector3(-15f, 0.1f + GameManager.Instance.BlueForRed.Count * 0.15f, 11.5f);
                    GameManager.Instance.BlueForRed[GameManager.Instance.BlueForRed.Count - 1].GetComponent<Product>().moveFromBackpackFlag = true;
                    return;
                }
            }
        }
    }


    // Выгрузка на склад синих у красной фабрики
    private void GreenForRed()
    {
        if ((GameManager.Instance.GreenForRed.Count < GameManager.Instance.storageCapacity) & (backpack.Count > 0))
        {
            for (int i = 0; i < backpack.Count; i++)
            {
                if (backpack[i].GetComponent<Product>().type == 1)
                {
                    GameManager.Instance.GreenForRed.Add(backpack[i]);
                    backpack.RemoveAt(i);
                    GameManager.Instance.GreenForRed[GameManager.Instance.GreenForRed.Count - 1].transform.SetParent(null);
                    GameManager.Instance.GreenForRed[GameManager.Instance.GreenForRed.Count - 1].GetComponent<Product>().moveTo = new Vector3(-9.5f, 0.1f + GameManager.Instance.GreenForRed.Count * 0.15f, 11.5f);
                    GameManager.Instance.GreenForRed[GameManager.Instance.GreenForRed.Count - 1].GetComponent<Product>().moveFromBackpackFlag = true;
                    return;
                }
            }
        }
    }
}
