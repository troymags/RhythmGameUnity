using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canbePressed;

    public KeyCode keyToPress;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canbePressed)
            {
                gameObject.SetActive(false);

                GameManager.instance.NoteHit();
            }
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canbePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.activeInHierarchy)
        {
            if (gameObject.activeSelf)
            {
                if (other.tag == "Activator")
                {
                    canbePressed = false;
                    GameManager.instance.NoteMissed();
                }          
            }
            
        }
    }   
}
