using System;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationController : IDisposable
{
    public sealed class Animation
    {
        public AnimState Track;
        public List<Sprite> Sprites;
        public bool Loop = true;
        public float Counter = 0;
        public float Speed = 10;
        public bool IsActive = true;

        public void Update()
        {
            if (!IsActive) return;

            Counter += Time.deltaTime * Speed;
            if (Loop)
            {
                while(Counter > Sprites.Count)
                {
                    Counter -= Sprites.Count; 
                }
            }
            else if (Counter > Sprites.Count)
            {
                Counter = Sprites.Count;
                IsActive = false;
            }
        }
    }

    private AnimationCfg _config;
    private Dictionary<SpriteRenderer, Animation> _activeAnimation = new Dictionary<SpriteRenderer, Animation>();

    public SpriteAnimationController(AnimationCfg config)
    {
        _config = config;
    }

    public void StartAnimation(SpriteRenderer renderer, AnimState track, bool loop, float speed)
    {
        if (_activeAnimation.TryGetValue(renderer , out var animation))
        {
            animation.Loop = loop;
            animation.Speed = speed;
            animation.IsActive = true;
            if (animation.Track != track)
            {
                animation.Track = track;
                animation.Sprites = _config.Sequence.Find(sequence => sequence.Track == track).Sprites;
                animation.Counter = 0;
            }
        }
        else
        {
            _activeAnimation.Add(renderer, new Animation()
            {
                Loop = loop,
                Speed = speed,
                Track = track,
                Sprites = _config.Sequence.Find(sequence => sequence.Track == track).Sprites
            });
        }
    }

    public void StopAnimation(SpriteRenderer renderer)
    {
        if (_activeAnimation.ContainsKey(renderer))
        {
            _activeAnimation.Remove(renderer);
        }
    }

    public void Update()
    {
        foreach(var animation in _activeAnimation)
        {
            animation.Value.Update();
            if (animation.Value.Counter < animation.Value.Sprites.Count)
            {
                animation.Key.sprite = animation.Value.Sprites[(int)animation.Value.Counter];
            }
        }
    }
     
    public void Dispose()
    {
        _activeAnimation.Clear();
    }
}
