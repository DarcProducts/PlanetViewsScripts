using System.Collections.Generic;
using UnityEngine;

public class darcSoundController : MonoBehaviour
{
    public static darcSoundController Instance { get; private set; }
    [SerializeField] AudioSource _mainMusicOption1;
    [SerializeField] AudioSource _mainMusicOption2;
    [SerializeField] AudioSource _mainMusicOption3;
    AudioSource currentAudioPlaying = null;
    [SerializeField] AudioSource _highlightButtonPress;

    List<AudioSource> mainMusicList = new List<AudioSource>();

    void Awake() => Instance = this;

    void Start()
    {
        AddMusicToList();
        PlayRandomSongThenLoop();
    }

    void AddMusicToList()
    {
        if (_mainMusicOption1 != null)
            mainMusicList.Add(_mainMusicOption1);
        if (_mainMusicOption2 != null)
            mainMusicList.Add(_mainMusicOption2);
        if (_mainMusicOption3 != null)
            mainMusicList.Add(_mainMusicOption3);
    }

    public void PlayRandomSongThenLoop()
    {
        currentAudioPlaying = null;
        if (currentAudioPlaying == null || !currentAudioPlaying.isPlaying)
        {
            int num = Random.Range(0, 3);
            if (num == 0 && _mainMusicOption1 != null)
            {
                _mainMusicOption1.Play();
                currentAudioPlaying = _mainMusicOption1;
            }
            else if (num == 1 && _mainMusicOption2 != null)
            {
                _mainMusicOption2.Play();
                currentAudioPlaying = _mainMusicOption2;
            }
            else if (num == 2 && _mainMusicOption3 != null)
            {
                _mainMusicOption3.Play();
                currentAudioPlaying = _mainMusicOption3;
            }
        }
    }

    public void PlayAllMainMusic()
    {

    }
    public AudioSource GetCurrentMainMusic()
    {
        if (currentAudioPlaying != null)
            return currentAudioPlaying;
        else return null;
    }
    public void PlayHighlightButtonPress()
    {
        if (_highlightButtonPress != null)
            _highlightButtonPress.Play();
    }
}
