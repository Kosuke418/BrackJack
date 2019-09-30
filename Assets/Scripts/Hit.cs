using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public GameObject Card;

    CardManager cardManager;

    int cardIndex = 0;

    private void Awake()
    {
        cardManager = Card.GetComponent<CardManager>();
    }

    public void OnClick()
    {
        if (cardIndex >= cardManager.face.Length)
        {
            cardIndex = 0;
            cardManager.FaceOrBack(false);
        }
        else
        {
            cardManager.cardIndex = cardIndex;
            cardManager.FaceOrBack(true);
            cardIndex++;
        }
    }
}
