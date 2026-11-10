namespace LukeScripts.Game
{
    using UnityEngine;

    public class RubbishItem : MonoBehaviour
    {
        [SerializeField] private string rubbishType = "Plastic";

        private RubbishSpawner spawner;

        public string RubbishType => rubbishType;

        public void SetSpawner(RubbishSpawner newSpawner)
        {
            spawner = newSpawner;
        }

        public RubbishSpawner GetSpawner()
        {
            return spawner;
        }

        public void RespawnAtOriginalLocation()
        {
            if (spawner != null)
            {
                spawner.Respawn();
            }
        }
    }
}
