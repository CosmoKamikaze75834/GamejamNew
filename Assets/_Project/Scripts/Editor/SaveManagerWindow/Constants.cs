#if UNITY_EDITOR
namespace FiXiK.Editor.DispatcherSaves
{
    public static class Constants
    {
        public const string DefaultSavesFileName = "TestSaves.txt";
        public const string WindowName = "Диспетчер сохранений";
        public const string ToolsName = "Tools/" + WindowName;
        public const string MessageFileNotFound = "File not found";
        public const string MessageSaveFileDoesNotExist = "Файл сохранений отсутствует";
        public const string MessageFolderNotFound = "Путь к файлу не найден";
        public const string MessageDataPathNotExist = "Не удалось получить путь к файлу";
        public const string ButtonOk = "Ясненько";
        public const string ButtonYes = "Да";
        public const string ButtonNo = "Нет";
    }
}
#endif