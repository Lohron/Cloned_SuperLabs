using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperLabs
{
    public class MapController : MonoBehaviour
    {
        void Start()
        {
            transform.Find("Ceiling").gameObject.SetActive(false);
        }
    }
}