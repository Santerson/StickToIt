using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnim : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite hitSprite;
    public Sprite hurtSprite;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = idleSprite;
    }

    public void SetIdle()
    {
        spriteRenderer.sprite = idleSprite;
    }

    public void SetHit()
    {
        spriteRenderer.sprite = hitSprite;
    }

    public void SetHurt()
    {
        spriteRenderer.sprite = hurtSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            SetHit();
        }
        if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            SetIdle();
        }
    }
}