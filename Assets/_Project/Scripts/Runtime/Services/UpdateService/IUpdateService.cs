using System;

public interface IUpdateService
{
    IUpdateService Subscribe(Action<float> handler, UpdateType updateType);

    IUpdateService Unsubscribe(Action<float> handler, UpdateType updateType);

    IUpdateService DebugPrint();
}