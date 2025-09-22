namespace LabFusion.Menu.Data;

public class BoolElementData : ElementData
{
    private bool _value = false;
    public bool Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;

            OnValueChanged?.Invoke(value);
        }
    }

    public Action<bool> OnValueChanged;
}
