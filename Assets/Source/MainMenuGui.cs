using System;
using UnityEngine;

public class MainMenuGui : MonoBehaviour
{
     private void OnGUI()
     {
          GUILayout.BeginScrollView(Vector2.zero);
          GUILayout.Label("Main Menu");
          if (GUILayout.Button("Begin"))
          {
               throw new NotImplementedException();
          }
          GUILayout.EndScrollView();
     }
}
