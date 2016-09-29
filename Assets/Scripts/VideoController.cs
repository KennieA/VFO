using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VideoController : MonoBehaviour
{

    string url = "https://welfaredenmark.blob.core.windows.net/vfo-recordings-staging/ogv";

    RawImage _player;
    AudioSource _sound;
    MovieTexture video;

    // Use this for initialization
    void Start ()
    {
        _player = GetComponent<RawImage>();
        _sound = GetComponent<AudioSource>();

        StartCoroutine(LoadVideo());
    }

    IEnumerator LoadVideo()
    {
        WWW www = new WWW(url);
        yield return www;

        if (www.error != null)
        {
            Debug.Log("Error: Can't load video");
            yield break;
        }
        else
        {
            video = www.movie;
            _player.texture = video;
            _sound.clip = video.audioClip;
            video.Play();
            _sound.Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && video.isPlaying)
        {
            video.Pause();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !video.isPlaying)
        {
            video.Play();
        }
    }
}
