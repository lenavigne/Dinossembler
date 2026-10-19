using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private int nb_skeletons = 1;
    private int nb_skel_complete = 0;
    private double start_timer;
    [SerializeField] private double chrono = 100f;
    [SerializeField] private string nameScene = "TestGamingScene";

    void Start()
    {
        if(SceneManager.GetActiveScene().name == nameScene)
        {
            start_timer = Time.timeAsDouble;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == nameScene)
        {
            if (Time.timeAsDouble - start_timer >= chrono)
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
    public void CompleteSkeleton()
    {
        nb_skel_complete++;
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
