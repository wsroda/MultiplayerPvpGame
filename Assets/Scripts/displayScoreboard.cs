using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class displayScoreboard
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                refreshScoreboard();
            }
        }

        private void refreshScoreboard()
        {
            
        }
    }
}