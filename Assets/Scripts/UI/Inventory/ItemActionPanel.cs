using Logic.Inventory.Actions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class ItemActionPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonPrefab;

        public void AddButton(string name, IAction listener)
        {
            GameObject button = Instantiate(_buttonPrefab, transform);
            button.GetComponent<Button>().onClick.AddListener(listener.ExecuteAction);
            button.GetComponentInChildren<TMP_Text>().text = name;
        }

        public void Toggle(bool value)
        {
            if (value)
                ClearUp();
            
            gameObject.SetActive(value);
        }

        private void ClearUp()
        {
            foreach (Transform child  in transform) 
                Destroy(child.gameObject);
        }
    }
}