using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
   [SerializeField] private float blockingTime;
   public bool isBlocking;
   private void Update()
   {
      if (Input.GetKey(KeyCode.Mouse1))
      {
         Block(true);
      }

      if (Input.GetKeyUp(KeyCode.Mouse1))
      {
         Block(false);
      }
   }

   private void Block(bool blocking)
   {
      isBlocking = blocking;
   }
}
