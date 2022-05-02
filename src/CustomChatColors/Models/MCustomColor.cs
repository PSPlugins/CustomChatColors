using System.Xml.Serialization;

namespace CustomChatColors.Models
{
    public class MCustomColor
    {
        [XmlAttribute]
        public ulong PlayerId { get; set; }
        
        [XmlAttribute]
        public string HexColorCode { get; set; }
    }
}