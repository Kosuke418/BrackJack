using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Sprite[] face;
    public Sprite back;
    public int cardIndex;
    public int cardNum = 0;

    SpriteRenderer spriteRenderer;

    public void FaceOrBack(bool showFace)
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
