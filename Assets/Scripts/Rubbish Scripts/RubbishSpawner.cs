namespace LukeScripts.Game
{
    //using System.Diagnostics.Contracts;
    using UnityEngine;

    public class RubbishSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject rubbishPrefab;

        public void SpawnRubbish()
        {
            if (rubbishPrefab == null)
            {
                Debug.LogWarning("No rubbish prefab assigned!", this);
                return;
            }

            GameObject rubbish = Instantiate(rubbishPrefab, transform.position, Quaternion.identity);

            RubbishItem item = rubbish.GetComponent<RubbishItem>();

            if (item != null)
            {
                item.SetSpawner(this);
            }
            else
            {
                Debug.LogWarning("The rubbish prefab needs a Rubbishitem component!", rubbish);
            }
        }

        public void Respawn()
        {
            SpawnRubbish();
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            SpawnRubbish();
        }


    }
}
