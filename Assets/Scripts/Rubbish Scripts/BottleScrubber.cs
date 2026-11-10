namespace LukeScripts.Game
{
    using UnityEngine;
    using UnityEngine.UI;

    public class BottleScrubber : MonoBehaviour
    {
        public Slider progressBar;
        public float maxCleanliness = 100f;
        private float currentCleanliness = 0f;

        public GameObject minigameUI; // refernce to the sink interaction script

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

            if (progressBar != null)
            {
                progressBar.maxValue = maxCleanliness;
                progressBar.value = currentCleanliness;
            }
        }

        //the scrub event trigger, this is used for when the player has a bottle, while also interacting with the sink
        public void Scrub()
        {
            currentCleanliness += Time.unscaledDeltaTime * 40f; // the scrubing speed fot the player

            if (progressBar != null)
            {
                progressBar.value = currentCleanliness;

            }

            if (currentCleanliness >= maxCleanliness)
            {
                CompleteCleaning();
            }
        }

        void CompleteCleaning()
        {
            Debug.Log("bottle has been cleaned");
            Time.timeScale = 1f;
            minigameUI.SetActive(false); // hide minigame

            //reset for next time
            currentCleanliness = 0f;
            if (progressBar != null) progressBar.value = 0f;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
