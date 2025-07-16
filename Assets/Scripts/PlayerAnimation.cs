using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnim : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite RunSprite;
    public Sprite JumpSprite;

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

    public void SetRun()
    {
        spriteRenderer.sprite = RunSprite;
    }

    public void SetJump()
    {
        spriteRenderer.sprite = JumpSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Space Pressed
        {
            SetJump();
        }
        if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            SetIdle();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        { 
            SetRun();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetRun();
        }
    }
}