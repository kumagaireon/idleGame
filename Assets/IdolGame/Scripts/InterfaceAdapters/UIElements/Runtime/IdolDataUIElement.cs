using UnityEngine;
using UnityEngine.UIElements;

namespace IdolGame.UIElements;

[UxmlElement]
public partial class IdolDataUIElement : VisualElement
{
    readonly VisualElement background = new() { name = "background" };
    readonly VisualElement groupLogoElement = new() { name = "group-logo" };
    readonly TextElement groupNameTextElement = new() { name = "group-name-text" };
    readonly VisualElement idolMembersContainer = new() { name = "idol-members-container" };
    public int Index { get; set; }

    public Sprite? GroupLogo
    {
        set => groupLogoElement.style.backgroundImage = Background.FromSprite(value);
    }

    public string? GroupName
    {
        set => groupNameTextElement.text = @$"<style=""bold"">{value}";
    }

    public IdolMembersData[]? Members
    {
        set
        {
            idolMembersContainer.Clear();
            if (value != null)
            {
                foreach (var member in value)
                {
                    var memberElement = new IdolMemberUIElement
                    {
                        MemberName = member.Name.ToString(),
                        MemberImage = Background.FromSprite(Resources.Load<Sprite>(member.ImagePath.ToString()))
                    };
                    idolMembersContainer.Add(memberElement);
                }
            }
        }
    }

}
// 個々のアイドルメンバーを表示するためのUI要素
public class IdolMemberUIElement : VisualElement
{
    readonly VisualElement memberImageElement = new() { name = "member-image" };
    readonly TextElement memberNameTextElement = new() { name = "member-name-text" };

    public string? MemberName
    {
        set => memberNameTextElement.text = @$"<style=""bold"">{value}";
    }

    public Background MemberImage
    {
        set => memberImageElement.style.backgroundImage = value;
    }

    public IdolMemberUIElement()
    {
        Add(memberImageElement);
        Add(memberNameTextElement);
    }
}