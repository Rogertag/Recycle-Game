
using UnityEngine;

public class ConveyorController : MonoBehaviour
{
    [Header("Item Spawning")]
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxItems = 8;

    [Header("Conveyor Movement")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float itemSpeed = 2f;

    private float spawnTimer;
    private int activeItems;

    private void Start()
    {
        if (waypoints == null || waypoints.Length < 2)
        {
            Debug.LogError("Assign at least 2 conveyor waypoints.");
            enabled = false;
            return;
        }

        if (itemPrefabs == null || itemPrefabs.Length == 0)
        {
            Debug.LogError("Assign at least 1 item prefab.");

            enabled = false;
        }
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && activeItems < maxItems)
        {
            SpawnItem();
            spawnTimer = 0f;
        }
    }

    private void SpawnItem()
    {
        int randomIndex = Random.Range(0, itemPrefabs.Length);

        GameObject prefab = itemPrefabs[randomIndex];

        if (prefab == null)
            return;

        GameObject newItem = Instantiate(prefab, waypoints[0].position, Quaternion.identity);

        ConveyorItem conveyorItem = newItem.GetComponent<ConveyorItem>();

        if (conveyorItem == null)
        {
            Debug.LogError(prefab.name + " is missing ConveyorItem.");
            Destroy(newItem);
            return;
        }

        conveyorItem.Initialise(waypoints, itemSpeed);
        activeItems++;

        conveyorItem.PickedUp += OnItemRemoved;
    }

    private void OnItemRemoved(ConveyorItem item)
    {
        activeItems = Mathf.Max(0, activeItems - 1);
        item.PickedUp -= OnItemRemoved;
    }
}

