using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTriggerRandomTime : MonoBehaviour
{
    [SerializeField] private bool isLoop;
    [SerializeField] private float timeRange;
    [SerializeField] private float timeRandomRange;
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerName;

    void Start()
    {
        StartCoroutine(SetTriggerCoroutine());
    }

    IEnumerator SetTriggerCoroutine()
    {
        do {
            animator.SetTrigger(triggerName);
            float delay = getRandomTime();
            yield return new WaitForSeconds(delay);
        } while (isLoop);
    }

    float getRandomTime()
    {
        return (timeRange + Random.Range(-timeRandomRange,timeRandomRange));
    }
}
