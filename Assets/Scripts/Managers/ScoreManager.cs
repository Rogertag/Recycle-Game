namespace LukeScripts.Game
{
    using UnityEngine;
    using TMPro;

    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        [SerializeField] private TMP_Text scoreText;

        private int score = 0;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            UpdateScoreUI();
        }

        public void AddPoint()
        {
            score++;
            UpdateScoreUI();

            Debug.Log("Score: " + score);
        }

        private void UpdateScoreUI()
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
