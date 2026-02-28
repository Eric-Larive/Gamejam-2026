using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public GameObject[] toShow;

    private int _currentWaypointIndex = 0;
    private bool _reachDestination = false;

    private void Update()
    {
        if (_reachDestination) return;
        Transform target = waypoints[_currentWaypointIndex];
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            _currentWaypointIndex++;

            if (_currentWaypointIndex >= waypoints.Length)
            {
                foreach (var gameObjects in toShow)
                {
                    gameObjects.SetActive(true);
                }
                _reachDestination = true;
            }
        }
    }
}