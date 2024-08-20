using UnityEngine;
using UnityEngine.Video;

public class TestTV : InteractionableObject
{
    private VideoPlayer _videoPlayer;

    protected override void Awake()
    {
        base.Awake();

        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.enabled = false;
    }

    protected override void OverInteractionCount()
    {
        
    }

    protected override void ProgressInteraction()
    {
        if(_videoPlayer != null )
        {
            if(_videoPlayer.enabled == true)
            {
                _videoPlayer.enabled = false;
            }
            else
            {
                _videoPlayer.enabled = true;
            }
        }
    }
}
