using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    public enum AnimatorOwner { Player, Enemy }
    public AnimatorOwner owner;

    public Sprite[] idleFrames;
    public Sprite[] attackFrames;
    public float fps = 8f;

    private Image _image;
    private float _timer;
    private int _currentFrame;
    private bool _isAttacking;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        Sprite[] frames = _isAttacking ? attackFrames : idleFrames;

        _timer += Time.deltaTime;
        if (_timer >= 1f / fps)
        {
            _timer = 0f;
            _currentFrame++;

            if (_currentFrame >= frames.Length)
            {
                _currentFrame = 0;
                if (_isAttacking)
                    _isAttacking = false; 
            }

            _image.sprite = frames[_currentFrame];
        }
    }

    private void OnEnable()
    {
        if (owner == AnimatorOwner.Player)
            CombatManager.OnPlayerAttack += PlayAttack;
        else
            CombatManager.OnEnemyAttack += PlayAttack;
    }

    private void OnDisable()
    {
        if (owner == AnimatorOwner.Player)
            CombatManager.OnPlayerAttack -= PlayAttack;
        else
            CombatManager.OnEnemyAttack -= PlayAttack;
    }

    public void PlayAttack()
    {
        _isAttacking = true;
        _currentFrame = 0;
        _timer = 0f;
    }
}