using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CompleteSkeleton : MonoBehaviour
{
    [SerializeField] private SceneController sc_ctrl;
    [SerializeField] private GameObject[] skel_lights;
    private int nb_bones;
    private int nb_bones_placed = 0;
    void Start()
    {
        nb_bones = transform.childCount;
        foreach (var light in skel_lights)
        {
            light.SetActive(false);
        }
    }

    void Update()
    {
        if (nb_bones_placed == nb_bones)
        {
            sc_ctrl.CompleteSkeleton();
            foreach (var light in skel_lights)
            {
                light.SetActive(true);
            }
        }
            
    }
    public void BonePlaced()
    {
        nb_bones_placed++;
        Debug.Log("nb os bien place : " + nb_bones_placed);
    }
}
