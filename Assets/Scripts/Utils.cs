using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Utils {

    public static class Utils {
        public static Vector3 GetRandomDir() {
           
            return new Vector3(
                Random.Range(-1f, 1f),  // X 
                Random.Range(-1f, 1f) // Y
            ).normalized;
        }
    }

}