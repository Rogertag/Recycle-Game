namespace LukeScripts.Game
{
    using UnityEngine;

    public class RecyclingBin : MonoBehaviour
    {
        [SerializeField] private string acceptedType = "Plastic";

        public bool TryDeposit(RubbishItem item)
        {
            if (item == null)
                return false;

            if (item.RubbishType != acceptedType)
            {
                Debug.Log("Wrong bin! this bin accepts " + acceptedType);
                return false;
            }

            Debug.Log("Correct recycling");

            RubbishSpawner spawner = item.GetSpawner();

            //give the player 1 point for every successful point
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddPoint();
            }

            // has to spawn another piece of rubbish before it destoys the rubbish
            //RubbishSpawner spawner = item.GetComponent<RubbishSpawner>() != null ? GetSpawner(item) : null;

            Destroy(item.gameObject);

            if (spawner != null)
            {
                spawner.Respawn();
            }
            else
            {
                Debug.LogWarning("This rubbish is not avliable");
            }

            return true;
        }

        private RubbishSpawner GetSpawner(RubbishItem item)
        {
            return item.GetSpawner();
        }

    }
}
