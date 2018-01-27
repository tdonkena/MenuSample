
namespace Avinode.Menu.Entities
{
    public class Item
    {
        public string DisplayName { get; set; }
        public string Path { get; set; }
        public int TabPosition { get; set; }
        public bool IsActive{ get; set; }
        public Menu SubMenu { get; set; }
    }
}
