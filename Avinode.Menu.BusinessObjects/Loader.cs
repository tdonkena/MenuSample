using System.Xml;
using Avinode.Menu.Entities;

namespace Avinode.Menu.BusinessObjects
{
    public class Loader
    {
        private const string MENU_NODE = "menu";
        private const string ITEM_NODE = "item";
        private const string DISPLAYNAME_NODE = "displayName";
        private const string PATH_NODE = "path";
        private const string VALUE_ATTRIBUTE = "value";
        private const string SUBMENU_NODE = "subMenu";

        public Entities.Menu Load(string menuXmlPath)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(menuXmlPath);
            var menuNode = xmlDoc.GetElementsByTagName(MENU_NODE);
            return (menuNode.Count == 0) ? null : Parse(xmlDoc.GetElementsByTagName(MENU_NODE)[0].SelectNodes(ITEM_NODE));
        }

        private Entities.Menu Parse(XmlNodeList nodelist)
        {
            if (nodelist == null) return null;

            var menu = new Entities.Menu();

            foreach (XmlNode node in nodelist)                
            {
                var item = new Item { DisplayName = node.SelectSingleNode(DISPLAYNAME_NODE).InnerText, Path = node.SelectSingleNode(PATH_NODE).Attributes[VALUE_ATTRIBUTE].Value };                
                var subMenu = node.SelectNodes(SUBMENU_NODE);
                
                if (subMenu != null && subMenu.Count > 0)
                item.SubMenu = Parse(subMenu[0].SelectNodes(ITEM_NODE));
                menu.Add(item);
            }

            return menu;
        }
    }    
}

