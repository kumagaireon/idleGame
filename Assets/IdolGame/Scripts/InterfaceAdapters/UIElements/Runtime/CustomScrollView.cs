using UnityEngine.UIElements;

namespace IdolGame.UIElements;

[UxmlElement]
public partial class CustomScrollView : ScrollView
{
    [UxmlAttribute("items-template")] 
    public VisualTreeAsset itemsTemplate { get; set; }


    public new class UxmlFactory : UxmlFactory<CustomScrollView, UxmlTraits>
    {
    }

    public new class UxmlTraits : ScrollView.UxmlTraits
    {
        UxmlAssetAttributeDescription<VisualTreeAsset> itemsTemplateAttribute =
            new UxmlAssetAttributeDescription<VisualTreeAsset> { name = "items-template" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var customScrollView = ve as CustomScrollView;
            if (customScrollView != null)
            {
                customScrollView.itemsTemplate = itemsTemplateAttribute.GetValueFromBag(bag, cc);
            }
        }
    }
}