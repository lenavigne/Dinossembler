using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private int nb_skeletons = 1;
    private int nb_skel_complete = 0;
    private double time_elasped = 0;
    [SerializeField] private double alloted_time = 100f;
    [SerializeField] private string nameMainScene = "MainScene";
    [SerializeField] private TextMeshProUGUI textTimer;
    [SerializeField] private TextMeshProUGUI textSkeletonsCounter;

    void Start()
    {
        if(SceneManager.GetActiveScene().name == nameMainScene)
        {
            DisplayTimer();
            DisplaySkeletonCounter();
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == nameMainScene)
        {
            time_elasped += Time.deltaTime;
            DisplayTimer();
            if (time_elasped >= alloted_time)
            {
                ChangeSceneByName("EndLooseScene");
                
            }
            else if (nb_skel_complete == nb_skeletons)
            {
                ChangeSceneByName("EndWinScene");
            }

        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    void DisplayTimer()
    {
        double timeRemain = alloted_time - time_elasped;
        float min = (int)(timeRemain / 60);
        float sec = (int)(timeRemain % 60);
        double milsec = (timeRemain % 1) * 100;
        textTimer.text = string.Format("{0:00}:{1:00}.{2:00}", min, sec, milsec);
    }
    public void CompleteSkeleton()
    {
        nb_skel_complete++;
        DisplaySkeletonCounter();
    }
    void DisplaySkeletonCounter()
    {
        textSkeletonsCounter.text = "Skeletons : " + nb_skel_complete + "/" + nb_skeletons;
    }
    public void ChangeSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
