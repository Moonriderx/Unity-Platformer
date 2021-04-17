using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Moonrider
{

    // this script will allows us to change materials applied to character faster

    [CustomEditor(typeof(CharacterControl))]
    public class MaterialChanger : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CharacterControl control = (CharacterControl)target;
            if (GUILayout.Button("Change Material")) // if the button is pressed
            {
                control.ChangeMaterial();
            }
        }

    }
}