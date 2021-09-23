using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
   [SerializeField] private float blockingTime;
   [SerializeField ]private float _currentBlockingTime;
   public bool isBlocking;

   private void Start()
   {
      _currentBlockingTime = blockingTime;
   }

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

      if (isBlocking)
      {
         if (_currentBlockingTime > 0)
         {
            _currentBlockingTime -= Time.deltaTime;
         }
         else
         {
            Block(false);
         }
         
      }
      else
      {
         if (_currentBlockingTime < blockingTime)
         {
            _currentBlockingTime += Time.deltaTime;
         }
      }
      
   }

   private void Block(bool blocking)
   {
      isBlocking = blocking;
   }
}
