using System.Threading.Tasks;
using Enums;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject usernamePanel;
        [SerializeField]private TMP_InputField usernameInput;
        [SerializeField]private TMP_Text usernameText;


        public async Task WaitAndStart()
        {
            await Task.Yield();
            FirstInGame();
        }

        #region Clicks

        public async void  SaveInputUsernameClick()
        {
            Debug.Log(usernameInput.text);
            await DBManager.Instance.SaveInformation<string>(usernameInput.text, ConstantVariables.Users, 
                ConstantVariables.Username, ChildType.Username);
            usernamePanel.SetActive(false);
            usernameText.text = usernameInput.text;
        }

        public void ChangeUsernameClick()
        {
            usernamePanel.SetActive(true);
        }

        #endregion
    
        private void FirstInGame()
        {
            usernameText.text = DBManager.Instance.GetUsername();
            Debug.Log(DBManager.Instance.GetUsername());
        }
    
    
    }
}