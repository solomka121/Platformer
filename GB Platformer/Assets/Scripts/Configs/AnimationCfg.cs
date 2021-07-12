using System;
using System.Collections.Generic;
using UnityEngine;

public enum AnimState 
{ 
    Idle = 0 ,
    Run = 1
}

[CreateAssetMenu(fileName = "SpriteAnimCfg" , menuName = "Configs/Animation")]
public class AnimationCfg : ScriptableObject
{
    [Serializable]
    public sealed class SpriteSequence
    {
        public AnimState Track;
        public List<Sprite> Sprites = new List<Sprite>();
    }

    public List<SpriteSequence> Sequence = new List<SpriteSequence>();
}
