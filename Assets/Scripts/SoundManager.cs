using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    /// <summary>
    /// This class controls the different petitions fo sounds from different user
    /// In this particular game, we have three rockets that wants to make a sound (the same sound). So what we are doing is counting how many rockets are using the sound.
    /// While there is more than one user, we play the sound (a single sound), if the users are zero, we stop it.
    /// </summary>
    public AudioClip sound;
    private AudioSource m_source;
    public static int usersSound;

	// Use this for initialization
	void Start ()
    {
        if(sound)
        {
            m_source = gameObject.GetComponent<AudioSource>();
            m_source.clip = sound;
        }

        usersSound = 0;
	}
	
	public void Play()
    {
        if(usersSound == 0 && m_source.clip == sound)
        {
            m_source.Play();
        }
        usersSound++;
    }

    public void Stop()
    {
        if(usersSound > 0)
        {
            usersSound--;
        }
        if(usersSound == 0 && m_source.clip == sound)
        {
            m_source.Stop();
        }
    }
}
