using UnityEngine;
using Zenject;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace MiniIT.ARKANOID
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class BallView : MonoBehaviour
    {
        private BallConfig     _ballConfig; 
        private BallMovement   _ballMovement;
        private PlayerControls _playerControls;
        private Transform      _spawnPoint;
        private Collider2D     _bottomLoseTrigger; 
    
        [Inject]
        private void Construct(BallConfig ballConfig, Collider2D bottomLoseTrigger, Transform spawnPoint,
            GameSession gameSession, GameplayAudio gameplayAudio)
        {
            Rigidbody2D ballRigidbody = GetComponent<Rigidbody2D>();
            CircleCollider2D ballCollider = GetComponent<CircleCollider2D>();
    
            _ballMovement = new BallMovement(ballRigidbody, ballCollider, bottomLoseTrigger, spawnPoint, ballConfig, 
                gameSession, gameplayAudio);
        }
    
        private void OnEnable()
        {
            _playerControls ??= new PlayerControls();
            
            _playerControls.PlayerInput.LaunchBall.performed += OnLaunchPerformed;
            
            _playerControls.Enable();
        }
    
        private void OnDisable()
        {
            if (_playerControls != null)
            {
                _playerControls.PlayerInput.LaunchBall.performed -= OnLaunchPerformed;
                
                _playerControls.Disable();
            }
        }
    
        private void FixedUpdate()
        {
            _ballMovement.FixedTick(Time.fixedDeltaTime);
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            _ballMovement.OnTriggerEnter(other);
        }
    
        private void OnLaunchPerformed(CallbackContext context)
        {
            if (context.performed)
            {
                _ballMovement.Launch();
            }
        }
    }
}