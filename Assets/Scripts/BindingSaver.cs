using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class BindingSaver : MonoBehaviour
{   
        public InputActionAsset controls;

        void Start ()
        {
            DontDestroyOnLoad(this.gameObject);
            LoadControlOverrides();
        }
        
        void OnApplicationQuit ()
        {
            StoreControlOverrides();
        }
 
        /// Private wrapper class for json serialization of the overrides
        [System.Serializable]
        class BindingWrapperClass {
            public List<BindingSerializable> bindingList = new List<BindingSerializable> ();
        }
 
        /// internal struct to store an id overridepath pair for a list
        [System.Serializable]
        private struct BindingSerializable {
            public string id;
            public string path;
 
            public BindingSerializable(string bindingId, string bindingPath ) {
                id = bindingId;
                path = bindingPath;
            }
        }
 
        /// stores the active control overrides to player prefs
        public void StoreControlOverrides () {
            //saving
            BindingWrapperClass bindingList = new BindingWrapperClass ();
            foreach ( var map in controls.actionMaps ) {
                foreach ( var binding in map.bindings ) {
                    if ( !string.IsNullOrEmpty ( binding.overridePath ) ) {
                        bindingList.bindingList.Add ( new BindingSerializable ( binding.id.ToString (), binding.overridePath ) );
                    }
                }
            }
            PlayerPrefs.SetString( "ControlOverrides", JsonUtility.ToJson ( bindingList ) );
            PlayerPrefs.Save ();
        }
 
        /// Loads control overrides from playerprefs
        public void LoadControlOverrides () {
            if (PlayerPrefs.HasKey( "ControlOverrides") ) {
                BindingWrapperClass bindingList = JsonUtility.FromJson ( PlayerPrefs.GetString ( "ControlOverrides" ), typeof ( BindingWrapperClass ) ) as BindingWrapperClass;
                //create a dictionary to easier check for existing overrides
                Dictionary<System.Guid, string> overrides = new Dictionary<System.Guid, string> ();
                foreach ( var item in bindingList.bindingList) {
                    overrides.Add ( new System.Guid ( item.id ), item.path );
                }
                //walk through action maps check dictionary for overrides
                foreach ( var map in controls.actionMaps ) {
                    var bindings = map.bindings;
                    for ( var i = 0; i < bindings.Count; ++i ) {
                        if ( overrides.TryGetValue ( bindings[i].id, out string overridePath ) ) {
                            //if there is an override apply it
                            map.ApplyBindingOverride ( i, new InputBinding { overridePath = overridePath } );
                        }
                    }
                }
            }
        }
}
