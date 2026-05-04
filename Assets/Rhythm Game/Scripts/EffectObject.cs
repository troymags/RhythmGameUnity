// Base implementation adapted from [How To Make a Rhythm Game Tutorial]] Tutorial Series by [gamesplusjames]
// URL: [https://www.youtube.com/watch?v=cZzf1FQQFA0&list=PLLPYMaP0tgFKZj5VG82316B63eet0Pvsv] (for part 1 of the series)
// Modifications: CSV data logging added independently for dissertation research purposes - Leeiam Magsipoc

using UnityEngine;

public class EffectObject : MonoBehaviour
{

    public float lifetime = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
