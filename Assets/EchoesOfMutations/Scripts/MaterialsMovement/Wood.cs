using UnityEngine;
using MoreMountains.Feedbacks;


public class Wood : MonoBehaviour
{
    public Transform startPoint; 
    public Transform endPoint;   
    private MMF_Player woodMove; 

    private void Awake()
    {
        
        woodMove = GetComponent<MMF_Player>();
    }

    public void Initialize(Transform start, Transform end)
    {
        
        startPoint = start;
        endPoint = end;

        
        if (woodMove != null)
        {
           
            woodMove.PlayFeedbacks();
        }
    }
}