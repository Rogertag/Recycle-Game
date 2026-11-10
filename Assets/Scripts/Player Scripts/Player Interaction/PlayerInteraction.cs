namespace LukeScripts.Game
{
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private float interactionRadius = 1f;
        [SerializeField] private Transform carryPoint;
        [SerializeField] private LayerMask interactableLayers;

        private RubbishItem carriedItem;


        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }

        private void Interact()
        {
            // if the player is carrying rubbish, dispose it first
            if (carriedItem != null)
            {
                RecyclingBin bin = FindNearbyBin();

                if (bin != null)
                {
                    bin.TryDeposit(carriedItem);
                    return;
                }

                //Press E away from a bin to drop the chosen item
                DropItem();
                return;
            }

            //otherwise whatever rubbish is in range,. pick it up

            Collider2D hit = Physics2D.OverlapCircle(transform.position, interactionRadius, interactableLayers);

            if (hit == null)
                return;

            RubbishItem item = hit.GetComponent<RubbishItem>();

            if (item != null)
                PickUp(item);
        }

        private void PickUp(RubbishItem item)
        {
            carriedItem = item;

            //Disable the items collider while being carried
            Collider2D itemCollider = item.GetComponent<Collider2D>();

            if (itemCollider != null)
                itemCollider.enabled = false;

            Rigidbody2D itemRb = item.GetComponent<Rigidbody2D>();

            if (itemRb != null)
                itemRb.simulated = false;

            item.transform.SetParent(carryPoint);
            item.transform.localPosition = Vector3.zero;
        }

        private void DropItem()
        {
            carriedItem.transform.SetParent(null);

            carriedItem.transform.position = transform.position + Vector3.down * 0.5f;

            Collider2D itemCollider = carriedItem.GetComponent<Collider2D>();

            if (itemCollider != null)
                itemCollider.enabled = false;

            Rigidbody2D itemRb = carriedItem.GetComponent<Rigidbody2D>();

            if (itemRb != null)
                itemRb.simulated = true;

            carriedItem = null;
        }

        private RecyclingBin FindNearbyBin()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRadius);

            foreach (Collider2D hit in hits)
            {
                RecyclingBin bin = hit.GetComponent<RecyclingBin>();

                if (bin != null)
                    return bin;
            }
            return null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }


    }
}

