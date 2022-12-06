using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBoolean : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string parameterName;

    public void SetBooleanParameter (bool value)
    {
        animator.SetBool(parameterName, value);
    }
}
