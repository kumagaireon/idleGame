using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace IdolGame.UIElements;

[UxmlElement]
public abstract partial class BaseOptionsUIElement : VisualElement
{
    const string OptionUIBaseUssClassName = "options_ui_element__base";

    readonly TextElement optionNameTextElement = new() { name = "option-name" };
    readonly VisualElement optionValueElement = new() { name = "option-value" };

    [UxmlAttribute("text")]
    public string Text
    {
        get => optionNameTextElement.text;
        set => optionNameTextElement.text = value;
    }

    protected BaseOptionsUIElement()
    {
        AddToClassList(OptionUIBaseUssClassName);
        
        Add(optionNameTextElement);
        Add(optionValueElement);
    }

    protected void AddOption(VisualElement optionElement)
    {
        optionValueElement.Add(optionElement);
    }
}

[UxmlElement]
public partial class OptionsDropdownUIElement : BaseOptionsUIElement
{
    readonly DropdownField optionDropdownField = new() { name = "options-dropdown" };
    List<string> choices = new();
    string label = string.Empty;
    int index;

    [UxmlAttribute("choices")]
    public List<string> Choices
    {
        get => choices;
        set
        {
            if (choices == value)
            {
                return;
            }

            choices = value;
            optionDropdownField.choices = value;
        }
    }

    [UxmlAttribute("label")]
    public string Label
    {
        get => label;
        set
        {
            if (label == value)
            {
                return;
            }

            label = value;
            optionDropdownField.label = value;
        }
    }

    [UxmlAttribute("index")]
    public int Index
    {
        get => index;
        set
        {
            if (index == value)
            {
                return;
            }

            index = value;
            optionDropdownField.index = value;
        }
    }

    public OptionsDropdownUIElement()
    {
        AddOption(optionDropdownField);
    }
    
    public void FocusDropdown() => optionDropdownField.Focus();
}

[UxmlElement]
public partial class OptionsButtonUIElement : BaseOptionsUIElement
{
    readonly Button optionButton = new() { name = "options-button" };
    bool enabled;

    [UxmlAttribute("option-enabled")]
    public bool Enabled
    {
        get => enabled;
        set
        {
            if (enabled == value)
            {
                return;
            }

            enabled = value;
            optionButton.text = value ? "ON" : "OFF";
        }
    }

    public OptionsButtonUIElement()
    {
        optionButton.text = enabled ? "ON" : "OFF";
        optionButton.clicked += () => Enabled = !Enabled;
        AddOption(optionButton);
    }
    
    public void FocusButton() => optionButton.Focus();
}

[UxmlElement]
public partial class OptionsSliderUIElement : BaseOptionsUIElement
{
    readonly Slider optionSlider = new() { name = "options-slider" };
    float value;

    [UxmlAttribute("value")]
    public float Value
    {
        get => value;
        set
        {
            if (Mathf.Approximately(this.value, value))
            {
                return;
            }

            this.value = value;
            optionSlider.value = value;
        }
    }

    [UxmlAttribute("low-value")]
    public float LowValue
    {
        get => optionSlider.lowValue;
        set => optionSlider.lowValue = value;
    }
    
    [UxmlAttribute("high-value")]
    public float HighValue
    {
        get => optionSlider.highValue;
        set => optionSlider.highValue = value;
    }
    
    public OptionsSliderUIElement()
    {
        AddOption(optionSlider);
    }
    
    public void FocusSlider() => optionSlider.Focus();
}