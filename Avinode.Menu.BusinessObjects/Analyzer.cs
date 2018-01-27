using System.Linq;
using Avinode.Menu.Entities;

namespace Avinode.Menu.BusinessObjects
{
    public class Analyzer 
    {
        private Entities.Menu rootMenu;

        public Analyzer(Entities.Menu rootMenu)
        {
            this.rootMenu = rootMenu;            
        }

        public void MarkAsActive(string activePath)
        {
            rootMenu.ForEach(menuItem => Iterate(menuItem, activePath, 0));
        }

        private bool Iterate(Item menu, string activePath, int tabposition)            
        {
            if (menu == null) return false;
      
            menu.TabPosition = tabposition++;
            bool? childIsActive = false;

            if (menu.SubMenu != null)
                childIsActive = menu.SubMenu?.Any(menuItem => Iterate(menuItem, activePath, tabposition));

            return menu.IsActive = (menu.Path == activePath) || (childIsActive??false);
        }        
    }
}
