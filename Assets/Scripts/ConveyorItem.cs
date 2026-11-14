
using System;
using UnityEngine;

public class ConveyorItem : MonoBehaviour
{
    public event Action<ConveyorItem> PickedUp;

    private Transform[] path;
    private int targetIndex;
    private float moveSpeed;
    private bool isOnConveyor = true;

    public bool IsOnConveyor => isOnConveyor;

    public void Initialise(Transform[] conveyorPath, float speed)
    {
        path = conveyorPath;
        moveSpeed = speed;
        targetIndex = 1;

        if (path == null || path.Length < 2)
        {
            Debug.LogError("Conveyor item needs at least 2 waypoints.");
            enabled = false;
            return;
        }

        transform.position = path[0].position;
    }

    private void Update()
    {
        if (!isOnConveyor || path == null || path.Length < 2)
            return;

        Transform target = path[targetIndex];

        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            targetIndex = (targetIndex + 1) % path.Length;
        }
    }

    // Call this from the player's interaction script.
    public void PickUp(Transform holdPoint = null)
    {
        if (!isOnConveyor)
            return;

        isOnConveyor = false;

        // Stop further conveyor movement.
        enabled = false;

        // Notify any script listening for pickup events.
        PickedUp?.Invoke(this);

        if (holdPoint != null)
        {
            transform.SetParent(holdPoint);
            transform.localPosition = Vector3.zero;

            Collider2D itemCollider = GetComponent<Collider2D>();

            if (itemCollider != null)
                itemCollider.enabled = false;
        }
    }

    // Useful if the player system handles carrying separately.
    public void RemoveFromConveyor()
    {
        isOnConveyor = false;
        enabled = false;
        PickedUp?.Invoke(this);
    }
}
