using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class BallBody
    {
        private readonly Rigidbody2D      rigidbody;
        private readonly CircleCollider2D ballCollider;
        private readonly Transform        spawnPoint;
    
        private readonly float            radius;
    
        public BallBody(Rigidbody2D rigidbody, CircleCollider2D ballCollider, Transform spawnPoint)
        {
            this.rigidbody = rigidbody;
            this.ballCollider = ballCollider;
            this.spawnPoint = spawnPoint;
    
            ConfigureRigidbody();
    
            radius = this.ballCollider.radius * Mathf.Max(this.ballCollider.transform.lossyScale.x, 
                this.ballCollider.transform.lossyScale.y);
        }
    
        public Vector2 Position => rigidbody.position;
        public float   Radius => radius;
    
        public void MoveToSpawnPoint()
        {
            if (spawnPoint == null)
            {
                rigidbody.position = Vector2.zero;
                rigidbody.transform.position = Vector3.zero;
                
                return;
            }
    
            Vector3 position = spawnPoint.position;
            rigidbody.position = new Vector2(position.x, position.y);
            rigidbody.transform.position = position;
        }
    
        public void FollowSpawnPoint()
        {
            if (spawnPoint == null)
            {
                return;
            }
    
            Vector3 position = spawnPoint.position;
            rigidbody.position = new Vector2(position.x, position.y);
            rigidbody.transform.position = position;
        }
    
        public void MoveTo(Vector2 position)
        {
            rigidbody.MovePosition(position);
        }
    
        private void ConfigureRigidbody()
        {
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            rigidbody.simulated = true;
            rigidbody.useFullKinematicContacts = true;
            rigidbody.gravityScale = 0.0f;
            rigidbody.freezeRotation = true;
        }
    }
}