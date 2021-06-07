using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moneytext : MonoBehaviour
{
    public Text moneytext;
    public GameObject gamemanager;
    void Update()
    {
        moneytext.text = gamemanager.GetComponent<GameManager>().Money.ToString();
    }
}
