// Base implementation adapted from [How To Make a Rhythm Game Tutorial]] Tutorial Series by [gamesplusjames]
// URL: [https://www.youtube.com/watch?v=cZzf1FQQFA0&list=PLLPYMaP0tgFKZj5VG82316B63eet0Pvsv] (for part 1 of the series)
// Modifications: CSV data logging added independently for dissertation research purposes - Leeiam Magsipoc

using UnityEngine;

public class BeatScroller : MonoBehaviour
{

    public float beatTempo;
    public bool hasStarted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
           /* if (Input.GetKeyDown(KeyCode.Space))
            {
                hasStarted = true;
           }*/ 
        }
        else
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
}
