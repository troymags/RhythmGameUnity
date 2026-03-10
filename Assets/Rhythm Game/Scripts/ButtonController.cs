using UnityEngine;

public class ButtonController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    public Sprite defaultSprite;
    public Sprite pressedSprite;

    public KeyCode keyToPress;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer  = GetComponent<SpriteRenderer>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
            if (Input.GetKeyDown(keyToPress))
            {
                spriteRenderer.sprite = pressedSprite;
            }
            
            if (Input.GetKeyUp(keyToPress))
            {
                spriteRenderer.sprite = defaultSprite;
            }

    }
}
