using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimelineExtension
{
    public static bool stopPlaying = false;
    private static readonly WaitForEndOfFrame _frameWait = new WaitForEndOfFrame();
    
    public static void ReversePlay(this UnityEngine.Playables.PlayableDirector timeline)
    {
        //
        ExtensionObject.Instance.StartCoroutine(Reverse(timeline));
    }
    
    private static IEnumerator Reverse(UnityEngine.Playables.PlayableDirector timeline)
    {
        UnityEngine.Playables.DirectorUpdateMode defaultUpdateMode = timeline.timeUpdateMode;
        timeline.timeUpdateMode = UnityEngine.Playables.DirectorUpdateMode.Manual;

        if (timeline.time.ApproxEquals(timeline.duration) || timeline.time.ApproxEquals(0))
        {
            timeline.time = timeline.duration;
        }
        
        timeline.Evaluate();

        yield return _frameWait;
        
        // This is where the magic happens: :3
        float dt = (float)timeline.duration;
        while (dt > 0)
        {
            dt -= Time.deltaTime;
            timeline.time = Mathf.Max(dt, 0);
            timeline.Evaluate();
            
            if (stopPlaying)
            {
                stopPlaying = false;
                break;
            }

            yield return _frameWait;
        }

        timeline.initialTime = 5.0f;
        timeline.time = 5.0f;
        timeline.Stop();
        timeline.Evaluate();
        timeline.timeUpdateMode = defaultUpdateMode;
        //timeline.Stop();
    }

    public static bool ApproxEquals(this double num, float other)
    {
        return Mathf.Approximately((float)num, other);
    }
    
    public static bool ApproxEquals(this double num, double other)
    {
        return Mathf.Approximately((float)num, (float)other);
    }
}
