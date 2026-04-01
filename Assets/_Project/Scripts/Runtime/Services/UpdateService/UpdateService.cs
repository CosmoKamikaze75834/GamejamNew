using System;
using System.Collections.Generic;
using UnityEngine;

namespace CustomUpdateService
{
    public class UpdateService : MonoBehaviour, IUpdateService
    {
        private const string Mark = "[UpdateService]";

#if UNITY_EDITOR
        [Header("Debug")]
        [SerializeField] private bool _logSubscribers;
#endif

        private readonly Dictionary<UpdateType, List<Action<float>>> _handlers = new()
        {
            [UpdateType.Update] = new List<Action<float>>(),
            [UpdateType.FixedUpdate] = new List<Action<float>>(),
            [UpdateType.LateUpdate] = new List<Action<float>>()
        };

        private void Update() =>
            InvokeHandlers(UpdateType.Update, Time.deltaTime);

        private void FixedUpdate() =>
            InvokeHandlers(UpdateType.FixedUpdate, Time.fixedDeltaTime);

        private void LateUpdate() =>
            InvokeHandlers(UpdateType.LateUpdate, Time.deltaTime);

        public IUpdateService Subscribe(Action<float> handler, UpdateType updateType)
        {
            if (TryGetHandlerList(updateType, out var list))
                AddHandler(list, handler, updateType);

            return this;
        }

        public IUpdateService Unsubscribe(Action<float> handler, UpdateType updateType)
        {
            if (TryGetHandlerList(updateType, out var list))
                RemoveHandler(list, handler, updateType);

            return this;
        }

        public IUpdateService DebugPrint()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"Update handlers ({_handlers[UpdateType.Update].Count}):");
            AppendHandlers(sb, _handlers[UpdateType.Update]);
            sb.AppendLine($"FixedUpdate handlers ({_handlers[UpdateType.FixedUpdate].Count}):");
            AppendHandlers(sb, _handlers[UpdateType.FixedUpdate]);
            sb.AppendLine($"LateUpdate handlers ({_handlers[UpdateType.LateUpdate].Count}):");
            AppendHandlers(sb, _handlers[UpdateType.LateUpdate]);

            Debug.Log(sb.ToString());

            return this;
        }

        private bool TryGetHandlerList(UpdateType updateType, out List<Action<float>> list)
        {
            if (_handlers.TryGetValue(updateType, out list))
                return true;

            Debug.LogError($"{Mark} Неподдерживаемый тип обновления: {updateType}");
            return false;
        }

        private void AddHandler(List<Action<float>> handlers, Action<float> handler, UpdateType updateType)
        {
            if (handler == null)
            {
                Debug.LogError($"{Mark} Попытка подписать null-делегат");
                return;
            }

            if (handlers.Contains(handler))
            {
#if UNITY_EDITOR
                Debug.LogWarning($"{Mark} Подписчик \"{handler.Target}.{handler.Method.Name}\" уже подписан на {updateType}");
#endif
                return;
            }

            handlers.Add(handler);
#if UNITY_EDITOR
            if (_logSubscribers)
                Debug.Log($"{Mark} [{updateType}] Добавлен подписчик: \"{handler.Target}\". Всего: {handlers.Count}");
#endif
        }

        private void RemoveHandler(List<Action<float>> handlers, Action<float> handler, UpdateType updateType)
        {
            if (handlers.Remove(handler))
            {
#if UNITY_EDITOR
                if (_logSubscribers)
                    Debug.Log($"{Mark} [{updateType}] Удалён подписчик: \"{handler.Target}\". Осталось: {handlers.Count}");
#endif
            }
        }

        private void InvokeHandlers(UpdateType type, float delta)
        {
            List<Action<float>> list = _handlers[type];

            for (int i = list.Count - 1; i >= 0; i--)
            {
                Action<float> handler = list[i];

                if (handler == null)
                {
                    list.RemoveAt(i);

                    continue;
                }

                if (handler.Target is UnityEngine.Object unityObj && unityObj == null)
                {
                    Debug.LogError($"{Mark} Подписчик {handler.Target} был уничтожен, но не отписался!");
                    list.RemoveAt(i);

                    continue;
                }

                handler.Invoke(delta);
            }
        }

        private void AppendHandlers(System.Text.StringBuilder sb, List<Action<float>> handlers)
        {
            foreach (Action<float> h in handlers)
            {
                string target = h.Target?.ToString() ?? "static";
                string method = h.Method.Name;
                sb.AppendLine($"  - {target}.{method}");
            }
        }
    }
}