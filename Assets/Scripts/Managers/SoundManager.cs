using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip playerJump;
    public static AudioClip playerDash;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        playerJump = Resources.Load<AudioClip>("Jump");
        playerDash = Resources.Load<AudioClip>("Dash");


        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Jump":
                audioSrc.PlayOneShot(playerJump);
                break;
            case "Dash":
                audioSrc.PlayOneShot(playerDash);
                break;
        }
    }
}
