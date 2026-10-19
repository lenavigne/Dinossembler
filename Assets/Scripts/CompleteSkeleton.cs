using UnityEngine;

public class CompleteSkeleton : MonoBehaviour
{
    [SerializeField] private SceneController sc_ctrl;
    private int nb_bones;
    private int nb_bones_placed = 0;
    void Start()
    {
        nb_bones = transform.childCount;
    }

    void Update()
    {
        if (nb_bones_placed == nb_bones)
            sc_ctrl.CompleteSkeleton();
    }
    public void BonePlaced()
    {
        nb_bones_placed++;
        Debug.Log("nb os bien place : " + nb_bones_placed);
    }
}
