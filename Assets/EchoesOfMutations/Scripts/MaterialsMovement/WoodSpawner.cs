using UnityEngine;
using MoreMountains.Feedbacks;

public class WoodSpawner : MonoBehaviour
{
    public MMF_Player WoodMove;

    void Start()
    {
        WoodMove.PlayFeedbacks();
 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
