namespace BlazorFill.Components.Dropdown;

public struct DropdownModel
{
    public string Content;
    public bool Html;
    public Action Click;
    public IEnumerable<DropdownModel> Children;
}