using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class BallBody
    {
        private readonly Rigidbody2D      _rigidbody;
        private readonly CircleCollider2D _ballCollider;
        private readonly Transform        _spawnPoint;
    
        private readonly float            _radius;
    
        public BallBody(Rigidbody2D rigidbody, CircleCollider2D ballCollider, Transform spawnPoint)
        {
            _rigidbody = rigidbody;
            _ballCollider = ballCollider;
            _spawnPoint = spawnPoint;
    
            ConfigureRigidbody();
    
            _radius = _ballCollider.radius * Mathf.Max(_ballCollider.transform.lossyScale.x, 
                _ballCollider.transform.lossyScale.y);
        }
    
        public Vector2 Position => _rigidbody.position;
        public float   Radius => _radius;
    
        public void MoveToSpawnPoint()
        {
            if (_spawnPoint == null)
            {
                _rigidbody.position = Vector2.zero;
                _rigidbody.transform.position = Vector3.zero;
                
                return;
            }
    
            Vector3 position = _spawnPoint.position;
            _rigidbody.position = new Vector2(position.x, position.y);
            _rigidbody.transform.position = position;
        }
    
        public void FollowSpawnPoint()
        {
            if (_spawnPoint == null)
            {
                return;
            }
    
            Vector3 position = _spawnPoint.position;
            _rigidbody.position = new Vector2(position.x, position.y);
            _rigidbody.transform.position = position;
        }
    
        public void MoveTo(Vector2 position)
        {
            _rigidbody.MovePosition(position);
        }
    
        private void ConfigureRigidbody()
        {
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _rigidbody.simulated = true;
            _rigidbody.useFullKinematicContacts = true;
            _rigidbody.gravityScale = 0.0f;
            _rigidbody.freezeRotation = true;
        }
    }
}