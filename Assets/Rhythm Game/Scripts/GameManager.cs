using UnityEngine;


public class GameManager : MonoBehaviour
{

public AudioSource theMusic; // Reference to the 
// 
public bool startPlaying;

public BeatScroller theBS;

public static GameManager instance;

public int currentScore;
public int scorePerNote = 100;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();
            }
        }
        
    }

    public void NoteHit()
    {
        Debug.Log ("Hit on time");

        currentScore += scorePerNote;
    }

    public void NoteMissed()    
    {
        Debug.Log ("Missed Note");
    }
}
