using System;
using System.Collections.Generic;
using UnityEngine;


public class Wall : MonoBehaviour
{
     [SerializeField] private List<Rigidbody> wallPieces = new List<Rigidbody>();

     private void Start()
     {
          GrabRigidbodies();
     }

     private void GrabRigidbodies()
     {
          for(int i=0 ;i<transform.childCount;i++)
          {
               wallPieces.Add(transform.GetChild(i).GetComponent<Rigidbody>());
          }
     }
}
