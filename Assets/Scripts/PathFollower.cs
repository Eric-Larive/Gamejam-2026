using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public SceneLoader sceneLoader;
    public Animator animator; 

    private int _currentWaypointIndex = 0;

    private void Update()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[_currentWaypointIndex];
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // Play animation based on direction
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Horizontal movement
            animator.Play(direction.x > 0 ? "Right" : "Left");
        }
        else
        {
            // Vertical movement
            animator.Play(direction.y > 0 ? "Up" : "Down");

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                _currentWaypointIndex++;

                if (_currentWaypointIndex >= waypoints.Length)
                    sceneLoader.LoadNextScene();
            }
        }
    }
}