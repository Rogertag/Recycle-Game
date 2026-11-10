namespace LukeScripts.Game
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        private Rigidbody2D rb;
        private Vector2 movement;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }



        // Update is called once per frame
        private void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // prevents movement from going faster when going diagonal
            movement = movement.normalized;
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

}
