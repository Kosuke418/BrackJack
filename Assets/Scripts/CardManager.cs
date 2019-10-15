using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // カードの表のスプライト
    public Sprite[] face;
    // カードの裏のスプライト
    public Sprite back;
    
    private SpriteRenderer spriteRenderer;
    // カードの数字(face[]のIndex)
    public int cardIndex;
    // デッキにおけるカードの順番
    private int cardNum;

    // 裏表の変更
    public void FaceorBack(bool showFace)
    {
        if (showFace)
        {
            spriteRenderer.sprite = face[cardIndex];
        }
        else
        {
            spriteRenderer.sprite = back;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
