
using System;
using System.Collections;
using System.Reflection;
using _Main._Scripts.SaveLoad;
using _Main._Scripts.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Main._Scripts.Debug
{
    public class DebugScreen : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;
        [SerializeField] private StyleSheet styleSheet;
        private void OnEnable()
        {
            StartCoroutine(Generate());
        }

        private void OnValidate()
        {
            if(Application.isPlaying || !gameObject.activeInHierarchy) return;
            StartCoroutine(Generate());
        }

        private IEnumerator Generate()
        {
            yield return null;
            var root = uiDocument.rootVisualElement;
            root.Clear();
            root.styleSheets.Add(styleSheet);
            var gameData = SaveManager.Load<GameData>();
            
            var menu = Create("root");
          
     

            var closeBtn = Create<Button>("btn");
            var saveBtn = Create<Button>("btn");
            closeBtn.text = "close";
            saveBtn.text = "save";
            var buttonsContainer = Create("buttons-container");
            menu.Add(buttonsContainer);
            // menu.Add(saveBtn);
            buttonsContainer.Add(saveBtn);
            buttonsContainer.Add(closeBtn);

            saveBtn.clicked += () =>
            {
                SaveManager.Save(gameData);
                GameManager.Instance.ReloadScene();
            };
            closeBtn.clicked += () => gameObject.SetActive(false);
            
            var container = Create("container");
            var list = Create<ScrollView>("list-view");
            container.Add(list);
            menu.Add(container);
            root.Add(menu);
            
            foreach (var fieldInfo in typeof(GameData).GetFields())
            {
                var fieldContainer = Create("field-container");
                if (fieldInfo.FieldType == typeof(bool))
                {
                    var label = Create<Label>("field-name");
                    label.text = fieldInfo.Name.ToSentenceCase();
                    var toggle = Create<Toggle>("toggle");
                    toggle.value = (bool)fieldInfo.GetValue(gameData);
                    toggle.RegisterValueChangedCallback((evt) =>
                    {
                        fieldInfo.SetValue(gameData,evt.newValue);
                    });
                    fieldContainer.Add(label);
                    fieldContainer.Add(toggle);
                }

                if (fieldInfo.FieldType == typeof(int))
                {
                    CreateTextField(fieldInfo, gameData, fieldContainer, (val) =>
                    {
                        if (int.TryParse(val,out int result))
                        {
                            fieldInfo.SetValue(gameData,result);
                        }
                    });
                }
                
                if (fieldInfo.FieldType == typeof(float))
                {   
                    CreateTextField(fieldInfo, gameData, fieldContainer, (val) =>
                    {
                        if (float.TryParse(val,out float result))
                        {
                            fieldInfo.SetValue(gameData,result);
                        }
                    });
                }
                list.Add(fieldContainer);
            }
            
            var resetButtonContainer = Create("field-container");
            var buttonLabel = Create<Label>("field-name");
            buttonLabel.text = "Stage Reset";
            var resetButton = Create<Button>("btn");
            resetButton.AddToClassList("reset-btn");
            resetButton.text = "Reset";
            resetButton.clicked += GameManager.Instance.StageReset;
            resetButtonContainer.Add(buttonLabel);
            resetButtonContainer.Add(resetButton);
            list.Add(resetButtonContainer);
        }

        private void CreateTextField(FieldInfo fieldInfo, GameData gameData, VisualElement fieldContainer , Action<string> ValueChanged = null)
        {
            var label = Create<Label>("field-name");
            label.text = fieldInfo.Name.ToSentenceCase();
            var field = Create<TextField>("field");
            field.value = fieldInfo.GetValue(gameData).ToString();
            field[0].AddToClassList("field-input");
            field.RegisterValueChangedCallback(evt =>
            {
                ValueChanged?.Invoke(evt.newValue);
            });
            fieldContainer.Add(label);
            fieldContainer.Add(field);
        }
        VisualElement Create(string className)
        {
            return Create<VisualElement>(className);
        }

        T Create<T>(string className) where T : VisualElement, new()
        {
            var ele = new T();
            ele.AddToClassList(className);
            return ele;
        }
    }
}