using UnityEngine;

public class RCRSceneManager : MonoBehaviour
{
    [SerializeField] Transform head;
    private bool firstFrame = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (firstFrame) {
            this.transform.position = new Vector3(head.position.x, this.transform.position.y, head.position.z) + new Vector3(head.forward.x, 0, head.forward.z).normalized * 0.6f;
            this.transform.LookAt(new Vector3(head.position.x, this.transform.position.y, head.position.z));
            this.transform.forward *= -1;
            this.transform.Rotate(0, 90 ,0);
            firstFrame = false;
        }
    }
}
