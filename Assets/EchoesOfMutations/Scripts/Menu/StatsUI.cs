using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bulletsTxt;
    void Start()
    {
        
    }

    
    void Update()
    {
        UpdateBullets();
    }

    public void UpdateBullets()
    {
        bulletsTxt.text = "Bullets : " + GameManager.Instance.gun.maxbulletsCapacity;
    }
}
