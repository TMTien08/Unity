using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    UIDocument uiDocument;
    private Button m_HaiNguoi;
    private Button m_DieuKhienMayBay;

    void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        m_HaiNguoi = uiDocument.rootVisualElement.Q<Button>("HaiNguoi");
        m_DieuKhienMayBay = uiDocument.rootVisualElement.Q<Button>("DieuKhienMayBay");
        m_HaiNguoi.clicked += () => LoadScene(1);
        m_DieuKhienMayBay.clicked += () => LoadScene(2);
    }
    void LoadScene(int SceneNum)
    {
        SceneManager.LoadScene(SceneNum);
    }

}
