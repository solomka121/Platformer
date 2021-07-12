using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private SpriteAnimationController _playerAnimator;
    [SerializeField] private LevelObjectView _playerView;
    [SerializeField] private AnimationCfg _playerConfig;
    [SerializeField] private int _animationSpeed = 10;
    
    void Awake()
    {
        _playerConfig = Resources.Load<AnimationCfg>("PlayerAnimCfg");
        _playerAnimator = new SpriteAnimationController(_playerConfig);
        _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Idle, true, _animationSpeed);
    }

    
    void Update()
    {
        _playerAnimator.Update();
    }
}
