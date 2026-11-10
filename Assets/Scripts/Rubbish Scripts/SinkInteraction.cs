namespace LukeScripts.Game
{
    using UnityEngine;

    public class SinkInteraction : MonoBehaviour
    {
        private bool playerInRange = false;
        public GameObject minigameUI; // minigame 

        // Update is called once per frame
        void Update()
        {
            if (playerInRange && Input.GetKeyDown(KeyCode.E))
            {
                OpenMinigame();

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                playerInRange = true;
                Debug.Log("Press 'E' to wash item");

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                playerInRange = false;
            }
        }

        void OpenMinigame()
        {
            minigameUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}

