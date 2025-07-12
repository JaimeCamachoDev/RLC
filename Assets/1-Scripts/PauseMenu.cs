using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] TimerDisplay timerDisplay;
    bool isPaused;
    private void Start()
    {
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        Cursor.visible = pauseMenu.activeSelf;
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        timerDisplay.counting = !isPaused;
        pauseMenu.SetActive(isPaused);
        rb.isKinematic = isPaused;
        if (!isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
