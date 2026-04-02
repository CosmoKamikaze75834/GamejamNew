#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class TypeDropdownItem : AdvancedDropdownItem
{
    public Type Type { get; }

    public TypeDropdownItem(string name, Type type) : base(name)
    {
        Type = type;
    }
}

public class ComponentTypeDropdown : AdvancedDropdown
{
    private readonly Action<Type> _onSelected;

    public ComponentTypeDropdown(AdvancedDropdownState state, Action<Type> onSelected) : base(state)
    {
        _onSelected = onSelected;
    }

    protected override AdvancedDropdownItem BuildRoot()
    {
        AdvancedDropdownItem root = new(Constants.Component.Tittle);

        IEnumerable<Type> componentTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly =>
            {
                try { return assembly.GetTypes(); }
                catch { return Type.EmptyTypes; }
            })
            .Where(type => type.IsSubclassOf(typeof(Component)) && type.IsAbstract == false);

        string noneNameSpaceText = Constants.Component.NoneNamespacesText;

        IOrderedEnumerable<IGrouping<string, Type>> groupedTypes = componentTypes
            .GroupBy(type => type.Namespace ?? noneNameSpaceText)
            .OrderBy(group => group.Key == noneNameSpaceText ? 0 : 1)
            .ThenBy(group => group.Key);

        foreach (IGrouping<string, Type> group in groupedTypes)
        {
            AdvancedDropdownItem groupItem = new(group.Key);

            foreach (Type type in group.OrderBy(type => type.Name))
            {
                string uniqueIdentifier = $"{type.FullName}, {type.Assembly.GetName().Name}";
                string typeNamespace = string.IsNullOrEmpty(type.Namespace) ? string.Empty : $"({type.Namespace})";
                string displayName = $"{type.Name} {typeNamespace}";
                TypeDropdownItem typeItem = new(displayName, type);
                groupItem.AddChild(typeItem);
            }

            root.AddChild(groupItem);
        }

        return root;
    }

    protected override void ItemSelected(AdvancedDropdownItem item)
    {
        if (item is TypeDropdownItem typeItem)
            _onSelected?.Invoke(typeItem.Type);
        else
            Debug.LogWarning(string.Format(Constants.Component.MessageTypeWithIdNotFound, item.name));
    }
}
#endif