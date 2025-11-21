using UnityEngine;
using Zenject;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace MiniIT.ARKANOID
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class BallView : MonoBehaviour
    {
        private BallMovement   ballMovement;
        private PlayerControls playerControls;
    
        [Inject]
        private void Construct(
            BallConfig ballConfig,
            Collider2D bottomLoseTrigger,
            Transform spawnPoint,
            GameSession gameSession,
            GameplayAudio gameplayAudio)
        {
            Rigidbody2D ballRigidbody = GetComponent<Rigidbody2D>();
            CircleCollider2D ballCollider = GetComponent<CircleCollider2D>();
    
            ballMovement = new BallMovement(ballRigidbody, ballCollider, bottomLoseTrigger, spawnPoint, ballConfig, 
                gameSession, gameplayAudio);
        }
    
        private void OnEnable()
        {
            playerControls ??= new PlayerControls();
            
            playerControls.PlayerInput.LaunchBall.performed += OnLaunchPerformed;
            
            playerControls.Enable();
        }
    
        private void OnDisable()
        {
            if (playerControls != null)
            {
                playerControls.PlayerInput.LaunchBall.performed -= OnLaunchPerformed;
                
                playerControls.Disable();
            }
        }
    
        private void FixedUpdate()
        {
            ballMovement.FixedTick(Time.fixedDeltaTime);
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            ballMovement.OnTriggerEnter(other);
        }
    
        private void OnLaunchPerformed(CallbackContext context)
        {
            if (context.performed)
            {
                ballMovement.Launch();
            }
        }
    }
}