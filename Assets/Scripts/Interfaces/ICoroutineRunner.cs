using System.Collections;
using UnityEngine;

namespace ProjectSolitude.Interfaces
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
