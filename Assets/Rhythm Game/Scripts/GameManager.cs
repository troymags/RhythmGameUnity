// Base implementation adapted from [How To Make a Rhythm Game Tutorial]] Tutorial Series by [gamesplusjames]
// URL: [https://www.youtube.com/watch?v=cZzf1FQQFA0&list=PLLPYMaP0tgFKZj5VG82316B63eet0Pvsv] (for part 1 of the series)
// Modifications: CSV data logging added independently for dissertation research purposes - Leeiam Magsipoc

using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

public AudioSource theMusic; // Reference to the 
// 
public bool startPlaying;

public BeatScroller theBS;

public static GameManager instance;

public int currentScore;
public int scorePerNote = 100;
public int scorePerGoodNote = 125;
public int scorePerPerfectNote = 150;

public int currentMultiplier;
public int multiplierTracker;
public int[] multiplierThresholds;

public Text scoreText;
public Text multiText;

public float totalNotes;
public float normalHits;    
public float goodHits;
public float perfectHits;
public float missedHits;

public GameObject resultsScreen;
public Text percentHitText, rankText, normalsText, goodsText, perfectsText, missesText, finalScoreText;

private List<NoteData> noteDataLog = new List<NoteData>();
private bool resultsSaved = false;

private struct NoteData
{
    public float timeFromStart;     // When during the song this happened (seconds)
    public string hitQuality;       // "Perfect", "Good", "Normal", or "Miss"
    public float positionOffset;    // How far from perfect (Y position when hit)
}


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
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
        else
        {
            if (!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);

                normalsText.text = "" + normalHits;
                goodsText.text = "" + goodHits;
                perfectsText.text = "" + perfectHits;
                missesText.text = "" + missedHits;

                float totalHits = normalHits + goodHits + perfectHits;
                float percentHit = (totalHits / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";

                if (percentHit > 40)
                {
                    rankVal = "D";
                    if (percentHit > 55)                
                    {
                        rankVal = "C";  
                        if (percentHit > 70)                
                        {
                            rankVal = "B";  
                            if (percentHit > 85)
                            {
                                rankVal = "A";  
                                if (percentHit > 95)
                                {               
                                    rankVal = "S";  
                                }
                            }
                        }
                    }
                }

                rankText.text = rankVal;

                finalScoreText.text = "" + currentScore;

                if (!resultsSaved)
                {
                    SaveDataToCSV(percentHit, rankVal);
                    resultsSaved = true;
                }
            }
        }
        
    }

    public void NoteHit()
    {
        Debug.Log ("Hit on time");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            } 
        }

        multiText.text = "Multiplier: x" + currentMultiplier;

       // currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();

        normalHits++;
        LogNote("Normal", 0f); 
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();

        goodHits++;
        LogNote("Good", 0f);
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();  

        perfectHits++;  
        LogNote("Perfect", 0f);
    }

    public void NoteMissed()    
    {
        Debug.Log ("Missed Note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;

        missedHits++;
        LogNote("Miss", 0f);
    }

    private void LogNote(string quality, float offset)
    {
        noteDataLog.Add(new NoteData
        {
            timeFromStart = theMusic.time,  // Current playback time of the song
            hitQuality = quality,
            positionOffset = offset
        });
    }

    private void SaveDataToCSV(float percentHit, string rank)
    {
        string fileName = $"RhythmData_{System.DateTime.Now:yyyyMMdd_HHmmss}.csv";
        string path = Path.Combine(Application.persistentDataPath, fileName);

        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine("=== SESSION SUMMARY ===");
            writer.WriteLine("FinalScore,TotalNotes,Perfect,Good,Normal,Miss,AccuracyPercent,Rank");
            writer.WriteLine($"{currentScore},{totalNotes},{perfectHits},{goodHits},{normalHits},{missedHits},{percentHit:F2},{rank}");
            writer.WriteLine();

            writer.WriteLine("=== PER-NOTE LOG ===");
            writer.WriteLine("NoteNumber,TimeFromStart_s,HitQuality,PositionOffset");
            for (int i = 0; i < noteDataLog.Count; i++)
            {
                var n = noteDataLog[i];
                writer.WriteLine($"{i + 1},{n.timeFromStart:F3},{n.hitQuality},{n.positionOffset:F3}");
            }
        }
        Debug.Log("Rhythm CSV saved to: " + path);
    }

}
