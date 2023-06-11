using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class FirstSceneManager : MonoBehaviour
    {
        private int lvl;
        private int thisLevel;
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2f);
            thisLevel = DBManager.Instance.GetLevel();
        
            if (thisLevel > 5)
            {
                var randomLevel = Random.Range(1, 5);
                lvl = randomLevel;
            }
            else
            {
                lvl = thisLevel;
            }
        
            SceneManager.LoadScene(lvl);
        }
    }
}