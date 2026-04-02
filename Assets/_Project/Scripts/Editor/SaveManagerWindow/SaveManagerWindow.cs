#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FiXiK.Editor.DispatcherSaves
{
    public class SaveManagerWindow : EditorWindow
    {
        [SerializeField] private SavesDataConfig _config;

        private string _filePath;
        private bool _fileExists;
        private long _fileSize;
        private DateTime _lastWriteTime;

        private void OnEnable() =>
            UpdateFileInfo();

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Настройки конфига", EditorStyles.boldLabel);
            EditorGUI.BeginChangeCheck();
            SavesDataConfig newConfig = (SavesDataConfig)EditorGUILayout.ObjectField("Конфиг сохранений", _config, typeof(SavesDataConfig), false);

            if (EditorGUI.EndChangeCheck())
            {
                _config = newConfig;
                UpdateFileInfo();
            }

            if (_config != null)
                EditorGUILayout.LabelField("Имя файла (из конфига):", _config.Filename);
            else
                EditorGUILayout.HelpBox("Конфиг не задан. Используется стандартное имя: " + Constants.DefaultSavesFileName, MessageType.Info);

            EditorGUILayout.LabelField("Информация о сохранённом файле", EditorStyles.boldLabel);
            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("Путь:", _filePath);
            EditorGUILayout.LabelField("Существует:", _fileExists ? "Да" : "Нет");

            if (_fileExists)
            {
                EditorGUILayout.LabelField("Размер:", Utils.FormatFileSize(_fileSize));
                EditorGUILayout.LabelField("Последнее изменение:", _lastWriteTime.ToString("dd.MM.yyyy в HHч mmм ssс"));
            }

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Обновить инфу", GUILayout.Height(30)))
                UpdateInfo();

            GUI.enabled = _fileExists;

            if (GUILayout.Button("Открыть файл", GUILayout.Height(30)))
                OpenFile();

            if (GUILayout.Button("Открыть путь к файлу", GUILayout.Height(30)))
                OpenFolder();

            if (GUILayout.Button("Удалить файл сохранений", GUILayout.Height(30)))
                DeleteFile();

            GUI.enabled = true;

            EditorGUILayout.Space(5);

            EditorGUILayout.HelpBox(
                "Используй эти инструменты для управления файлом сохранений игры. Удаление приведёт к обнулению прогресса.",
                MessageType.Info);
        }

        [MenuItem(Constants.ToolsName)]
        private static void ShowWindow()
        {
            SaveManagerWindow window = GetWindow<SaveManagerWindow>();
            window.titleContent = new GUIContent(Constants.WindowName);
            window.Show();
        }

        private string GetSaveFilePath()
        {
            string fileName = _config != null && !string.IsNullOrEmpty(_config.Filename)
                ? _config.Filename
                : Constants.DefaultSavesFileName;

            return Path.Combine(Application.persistentDataPath, fileName);
        }

        private void UpdateFileInfo()
        {
            _filePath = GetSaveFilePath();
            _fileExists = File.Exists(_filePath);

            if (_fileExists)
            {
                FileInfo info = new(_filePath);
                _fileSize = info.Length;
                _lastWriteTime = info.LastWriteTime;
            }
        }

        private void UpdateInfo() =>
            UpdateFileInfo();

        private void OpenFile()
        {
            if (_fileExists == false)
            {
                EditorUtility.DisplayDialog(Constants.MessageFileNotFound, Constants.MessageSaveFileDoesNotExist, Constants.ButtonOk);

                return;
            }

            try
            {
                EditorUtility.OpenWithDefaultApp(_filePath);
            }
            catch (Exception e)
            {
                Debug.LogError($"Не удалось открыть файл: {e.Message}");
                EditorUtility.DisplayDialog("Ошибка", "Не удалось открыть файл. Подробности смотрите в консоли.", Constants.ButtonOk);
            }
        }

        private void OpenFolder()
        {
            if (_fileExists == false)
            {
                string directory = Application.persistentDataPath;

                if (Directory.Exists(directory))
                    EditorUtility.RevealInFinder(directory);
                else
                    EditorUtility.DisplayDialog(Constants.MessageFolderNotFound, Constants.MessageDataPathNotExist, Constants.ButtonOk);

                return;
            }

            try
            {
                EditorUtility.RevealInFinder(_filePath);
            }
            catch (Exception e)
            {
                Debug.LogError($"Не удалось открыть папку: {e.Message}");
                EditorUtility.DisplayDialog("Ошибка", "Не удалось открыть папку. Подробности смотрите в разделе консоль.", Constants.ButtonOk);
            }
        }

        private void DeleteFile()
        {
            if (_fileExists == false)
            {
                EditorUtility.DisplayDialog(Constants.MessageFileNotFound, Constants.MessageSaveFileDoesNotExist, Constants.ButtonOk);

                return;
            }

            bool confirm = EditorUtility.DisplayDialog(
                "Подтвердите удаление",
                "Вы уверены, что хотите удалить сохраненный файл? Это действие невозможно отменить",
                Constants.ButtonYes,
                Constants.ButtonNo);

            if (confirm)
            {
                try
                {
                    File.Delete(_filePath);
                    UpdateFileInfo();
                    Debug.Log("Сохранённый файл удалён");
                }
                catch (Exception e)
                {
                    Debug.LogError($"Не удалось удалить файл: {e.Message}");
                    EditorUtility.DisplayDialog("Ошибка", "Не удалось удалить файл. Подробности смотрите в разделе консоль", Constants.ButtonOk);
                }
            }
        }
    }
}
#endif