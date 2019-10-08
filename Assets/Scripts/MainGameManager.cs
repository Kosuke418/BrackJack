using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    List<int> shuffleDeck = new List<int>();
    List<GameObject> playerCard = new List<GameObject>();
    List<GameObject> dealerCard = new List<GameObject>();
    int cardNum = 0;
    public Text playerNumText;
    int playerNum = 0;
    int playerGenerateNum = 0;
    public Text dealerNumText;
    int dealerNum = 0;
    int dealerGenerateNum = 0;
    int turn = 1;

    bool playerAce = false;
    bool dealerAce = false;

    CardManager cardManager;

    int cardIndex = 0;

    public GameObject card;

    void shuffle()
    {
        //startからendまで
        int start = 0;
        int end = 51;
        int num = 52;

        List<int> deck = new List<int>();

        for (int i = start; i < end + 1; i++)
        {
            deck.Add(i);
        }
        while (num-- > 0)
        {
            int index = Random.Range(0, num+1);

            shuffleDeck.Add(deck[index]);
            deck.RemoveAt(index);
        }
    }

    public void OnClickHit()
    {
        if (turn == 1)
        {
            if (cardNum >= cardManager.face.Length)
            {
                shuffleDeck.Clear();
                playerCard.Clear();
                shuffle();
                cardNum = 0;
            }
            else
            {
                GenerateCard(1);
            }
        }
        BlackJack();
    }
    public void OnClickStand()
    {
        turn = 2;
        BlackJack();
    }

    void BlackJack()
    {
        if (turn == 1)
        {
            if (playerNum >= 21)
            {
                Results();
            }
        }

        if (turn == 2)
        {
            while (true)
            {
                if (dealerNum >= 17)
                {
                    Results();
                    break;
                }
                else
                {
                    GenerateCard(2);
                }
            }
        }
    }

    void Results()
    {
        if(playerNum ==21 && dealerNum == 21)
        {
            if (playerGenerateNum == dealerGenerateNum)
            {
                Draw();
            }
            else if (playerGenerateNum > dealerGenerateNum)
            {
                PlayerLose();
            }
            else
            {
                PlayerWin();
            }
        }
        else if (playerNum > 21 && dealerNum > 21)
        {
            PlayerLose();
        }
        else if (dealerNum > 21)
        {
            PlayerWin();
        }
        else if (playerNum > 21)
        {
            PlayerLose();
        }
        else if (playerNum == dealerNum)
        {
            Draw();
        }
        else if (playerNum > dealerNum)
        {
            PlayerWin();
        }
        else if (playerNum < dealerNum)
        {
            PlayerLose();
        }
    }

    void PlayerWin()
    {
        dealerNumText.enabled = true;
        dealerCard[0].GetComponent<CardManager>().FaceOrBack(true);
        playerNumText.text = playerNum.ToString() + "-Player win!!";
    }

    void PlayerLose()
    {
        dealerNumText.enabled = true;
        dealerCard[0].GetComponent<CardManager>().FaceOrBack(true);
        playerNumText.text = playerNum.ToString() + "-Player lose!!";
    }

    void Draw()
    {
        dealerNumText.enabled = true;
        dealerCard[0].GetComponent<CardManager>().FaceOrBack(true);
        playerNumText.text = playerNum.ToString() + "-Draw!!";
    }

    void GenerateCard(int player)
    {
        if (player == 1)
        {
            playerCard.Add((GameObject)Instantiate(card, new Vector3(-5.24f + playerGenerateNum, -3.28f, 0f), Quaternion.identity));
            cardIndex = shuffleDeck[cardNum];
            playerCard[playerGenerateNum].GetComponent<CardManager>().cardIndex = cardIndex;
            if ((cardIndex + 1) % 13 == 11 || (cardIndex + 1) % 13 == 12 || (cardIndex + 1) % 13 == 0)
            {
                playerNum += 10;
            }
            else if ((cardIndex + 1) % 13 == 1)
            {
                playerNum += 11;
                playerAce = true;
            }
            else
            {
                playerNum += (cardIndex + 1) % 13;
            }
            if (playerNum >= 22 && playerAce)
            {
                playerNum -= 10;
                playerAce = false;
            }
            playerCard[playerGenerateNum].GetComponent<CardManager>().FaceOrBack(true);
            playerGenerateNum++;
            cardNum++;
            if (playerAce)
            {
                playerNumText.text = playerNum.ToString() + "(" + (playerNum - 10).ToString() + ")";
            }
            else
            {
                playerNumText.text = playerNum.ToString();
            }
           // BlackJack();
        }
        else
        {
            dealerCard.Add((GameObject)Instantiate(card, new Vector3(-5.24f + dealerGenerateNum, 3.28f, 0f), Quaternion.identity));
            cardIndex = shuffleDeck[cardNum];
            dealerCard[dealerGenerateNum].GetComponent<CardManager>().cardIndex = cardIndex;
            if ((cardIndex + 1) % 13 == 11 || (cardIndex + 1) % 13 == 12 || (cardIndex + 1) % 13 == 0)
            {
                dealerNum += 10;
            }
            else if ((cardIndex + 1) % 13 == 1)
            {
                dealerNum += 11;
                dealerAce = true;
            }
            else
            {
                dealerNum += (cardIndex + 1) % 13;
            }
            if (dealerNum >= 22 && dealerAce)
            {
                dealerNum -= 10;
                dealerAce = false;
            }
            dealerCard[dealerGenerateNum].GetComponent<CardManager>().FaceOrBack(true);
            dealerGenerateNum++;
            cardNum++;
            if (dealerAce)
            {
                dealerNumText.text = dealerNum.ToString() + "(" + (dealerNum - 10).ToString() + ")";
            }
            else
            {
                dealerNumText.text = dealerNum.ToString();
            }
        }
    }

    void Start()
    {
        cardManager = card.GetComponent<CardManager>();
        shuffle();
        GenerateCard(1);
        GenerateCard(1);
        GenerateCard(2);
        GenerateCard(2);
        dealerCard[0].GetComponent<CardManager>().FaceOrBack(false);
        BlackJack();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel("Main");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            OnClickHit();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnClickStand();
        }
    }
}
