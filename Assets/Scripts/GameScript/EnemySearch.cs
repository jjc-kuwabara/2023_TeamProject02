using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class EnemySearch : MonoBehaviour
{
        SphereCollider col;

        public bool playerOn = false;
        public bool search = false;

        void Start()
        {
            col = GetComponent<SphereCollider>();
            search = true;
        }
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                playerOn = true;
                search = false;
                col.radius = 5;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                playerOn = false;
                search = true;
                col.radius = 3;
            }
        }
    }


