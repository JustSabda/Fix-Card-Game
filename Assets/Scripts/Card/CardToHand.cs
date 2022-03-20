using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToHand : MonoBehaviour
{
    public static CardToHand Instance;
    [SerializeField] public GameObject Hand;
    [SerializeField] public GameObject It;
    // Start is called before the first frame update

    void Awake()
    {
        Instance = this;
        
    }
    /*private void Start()
    {
        Hand = GameObject.Find("Hand");
    }*/
    public void Start()
    {
        Hand = GameObject.Find("Hand");
        It.transform.SetParent(Hand.transform);
        It.transform.tag = "Untagged";
        It.transform.localScale = Vector3.one;
        It.transform.position = new Vector3(transform.position.x, transform.position.y, -3);
        //It.transform.eulerAngles = new Vector3(25, 0, 0);

        
        //GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
