﻿/******************************************************************************\
* Copyright (C) 2012-2016 Leap Motion, Inc. All rights reserved.               *
* Leap Motion proprietary and confidential. Not for distribution.              *
* Use subject to the terms of the Leap Motion SDK Agreement available at       *
* https://developer.leapmotion.com/sdk_agreement, or another agreement         *
* between Leap Motion and you, your company or other organization.             *
\******************************************************************************/

using UnityEngine;
using System.Collections;

public class PhysicalButtonCollisionCallback : MonoBehaviour {

  public PhysicalButton toCall;

  void OnCollisionEnter(Collision coll) {
    toCall.ReceiveCollisionEnter(coll, GetComponentInChildren<Collider>());
  }

}
