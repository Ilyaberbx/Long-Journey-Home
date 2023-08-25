﻿using System.Collections;
using UnityEngine;

namespace Infrastructure.Interfaces
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine typingCoroutine);
        void StopAllCoroutines();
    }
}
