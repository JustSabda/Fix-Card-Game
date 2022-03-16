using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    public GameObject[] battleZoneArray;

    // Start is called before the first frame update
    void Start()
    {
        battleZoneArray = GameObject.FindGameObjectsWithTag("Zone");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < battleZoneArray.Length; i++)
        {

        }
    }
}