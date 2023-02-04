using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAssignController : MonoBehaviour
{
    public GameObject[] Tasks;

    public IEnumerator updateTask(int indexTask, float dialSpeed)
    {
        yield return new WaitForSeconds(dialSpeed);
        if (indexTask > 1)
        {
            Tasks[indexTask - 2].SetActive(false);
        }
        Tasks[indexTask-1].SetActive(true);
    }
}
