using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIAnimator : MonoBehaviour
{
    [SerializeField] private List<UIAnimationClip> UIAnimationClips = new List<UIAnimationClip>();

    public float PlayAnimation(string id)
    {
        StopAllCoroutines();
        UIAnimationClip clip = UIAnimationClips.Find(x => x.AnimationId == id);
        if (clip == null)
        {
            Debug.LogError("[UIAnimator] [PlayAnimation()] Invalid animation id");
            return 0;
        }
        StartCoroutine(DoPlay(clip.AnimFrames, clip.TargetImage, deltaFrame(clip.AnimationLength, clip.AnimFrames.Count)));
        return clip.AnimationLength;
    }

    private IEnumerator DoPlay(List<Sprite> sprites, Image target, float delayPerFrame)
    {
        WaitForSeconds delay = new WaitForSeconds(delayPerFrame);
        for (int i = 0; i < sprites.Count; i++)
        {
            target.sprite = sprites[i];
            yield return delay;
        }
    }

    private float deltaFrame(float totalLength, int countFrames) => totalLength / countFrames;
}

[Serializable]
public class UIAnimationClip
{
    public string AnimationId;
    public float AnimationLength;
    public Image TargetImage;
    public List<Sprite> AnimFrames = new List<Sprite>();
}