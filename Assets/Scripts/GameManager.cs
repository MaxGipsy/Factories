using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float timeStartBlue = 0;
    private float timeStartGreen = 0;
    private float timeStartRed = 0;
    [SerializeField] private List<GameObject> prefbs = new List<GameObject>(); // 0 - Blue; 1 - Green; 2 - Red

    public List<GameObject> BlueForRed = new List<GameObject>(); // Склад синих для производства красных
    public List<GameObject> BlueForGreen = new List<GameObject>(); // Склад сининх для производства зеленых
    public List<GameObject> GreenForRed = new List<GameObject>(); // Склад зеленых для производства красных

    public List<GameObject> Blue = new List<GameObject>(); // Склад синих
    public List<GameObject> Green = new List<GameObject>(); // Склад зеленых
    public List<GameObject> Red = new List<GameObject>(); // Склад красных

    public int storageCapacity = 10;

    private bool greenFlag = true;
    private bool redFlag = true;


    void Start()
    {
        Instance = this;
        timeStartBlue = Time.time;
    }

    
    void FixedUpdate()
    {
        ProductionOfBlue();
        if ((BlueForGreen.Count > 0) && (greenFlag))
        {
            timeStartGreen = Time.time;
            greenFlag = false;
        }
        ProductionOfGreen();
        if ((BlueForRed.Count > 0) && (GreenForRed.Count > 0) && (redFlag))
        {
            timeStartRed = Time.time;
            redFlag = false;
        }
        ProductionOfRed();
    }


    // Производство красных ресурсов
    private void ProductionOfRed()
    {
        if ((Time.time - timeStartRed >= 3) && (Red.Count < storageCapacity) && (BlueForRed.Count > 0) && (GreenForRed.Count > 0))
        {
            BlueForRed[BlueForRed.Count - 1].GetComponent<Product>().moveToFactoryFlag = true;
            BlueForRed[BlueForRed.Count - 1].GetComponent<Product>().moveTo = new Vector3(-15f, 2.5f, 17f);
            BlueForRed.RemoveAt(BlueForRed.Count - 1);

            GreenForRed[GreenForRed.Count - 1].GetComponent<Product>().moveToFactoryFlag = true;
            GreenForRed[GreenForRed.Count - 1].GetComponent<Product>().moveTo = new Vector3(-15f, 2.5f, 17f);
            GreenForRed.RemoveAt(GreenForRed.Count - 1);

            GameObject bufObj = Instantiate(prefbs[2]);
            bufObj.GetComponent<Product>().moveFromFactoryFlag = true;
            bufObj.GetComponent<Product>().moveToBackpackFlag = false;
            bufObj.GetComponent<Product>().moveFromBackpackFlag = false;
            bufObj.GetComponent<Product>().moveToFactoryFlag = false;
            bufObj.GetComponent<Product>().moveTo = new Vector3(-9.5f, 0.1f + Red.Count * 0.15f, 17f);
            timeStartRed = Time.time;
            Red.Add(bufObj);
        }
        else if ((BlueForRed.Count == 0) || (GreenForRed.Count == 0))
        {
            redFlag = true;
        }
    }


    // Производство синих ресурсов
    private void ProductionOfBlue()
    {
        if ((Time.time - timeStartBlue >= 3) && (Blue.Count < storageCapacity))
        {
            GameObject bufObj = Instantiate(prefbs[0]);
            bufObj.GetComponent<Product>().moveFromFactoryFlag = true;
            bufObj.GetComponent<Product>().moveToBackpackFlag = false;
            bufObj.GetComponent<Product>().moveFromBackpackFlag = false;
            bufObj.GetComponent<Product>().moveToFactoryFlag = false;
            bufObj.GetComponent<Product>().moveTo = new Vector3(-3.5f, 0.1f + Blue.Count * 0.15f, -10f);
            timeStartBlue = Time.time;
            Blue.Add(bufObj);
        }
    }


    // Производство зеленых ресурсов
    private void ProductionOfGreen()
    {
        if ((Time.time - timeStartGreen >= 3) && (Green.Count < storageCapacity) && (BlueForGreen.Count > 0))
        {
            BlueForGreen[BlueForGreen.Count - 1].GetComponent<Product>().moveToFactoryFlag = true;
            BlueForGreen[BlueForGreen.Count - 1].GetComponent<Product>().moveTo = new Vector3(15f, 2.5f, 7f);
            BlueForGreen.RemoveAt(BlueForGreen.Count - 1);
            GameObject bufObj = Instantiate(prefbs[1]);
            bufObj.GetComponent<Product>().moveFromFactoryFlag = true;
            bufObj.GetComponent<Product>().moveToBackpackFlag = false;
            bufObj.GetComponent<Product>().moveFromBackpackFlag = false;
            bufObj.GetComponent<Product>().moveToFactoryFlag = false;
            bufObj.GetComponent<Product>().moveTo = new Vector3(9.6f, 0.1f + Green.Count * 0.15f, 7f);
            timeStartGreen = Time.time;
            Green.Add(bufObj);
        }
        else if (BlueForGreen.Count == 0)
        {
            greenFlag = true;
        }
    }
}
