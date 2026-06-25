// StartTrigger.cs
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        RunTimer timer = FindObjectOfType<RunTimer>();
        if (timer != null)
        {
            timer.StartRun();
        }
    }
}

// FinishTrigger.cs
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        RunTimer timer = FindObjectOfType<RunTimer>();
        if (timer != null)
        {
            timer.StopRun();
        }
    }
}
