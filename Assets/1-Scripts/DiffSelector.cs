using UnityEngine;

public class DiffSelector : MonoBehaviour
{
    [SerializeField] GameObject chekpoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (chekpoints != null)
        {
            if (PlayerPrefs.GetInt("CheckpointsOn") == 0)
            {
                chekpoints.SetActive(false);
            }
        }
    }
    public void SetDiffLevel(int value)
    {
        PlayerPrefs.SetInt("CheckpointsOn", value);
    }
}
