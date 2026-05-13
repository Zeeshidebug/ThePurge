using UnityEngine;

public class AttackPointFollowMouse : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distanceFromPlayer = 0.7f;

    private void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - player.position).normalized;

        // POSITION ORBIT
        transform.position = (Vector2)player.position + direction * distanceFromPlayer;

        // ROTATION
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}