using System;
using Avinode.Menu.BusinessObjects;
using Avinode.Menu.Entities;

namespace MenuSample
{
    class Program
    {
        static void Main(string[] args)
        {            
            var menu = new Loader().Load(args[0]);
            new Analyzer(menu).MarkAsActive(args[1]);
            menu.ForEach(Log); 
        }

        public static void Log(Item item)
        {
            var msg = $"{item.DisplayName},{item.Path}{(item.IsActive ? " ACTIVE" : string.Empty)}";
            for (var i = 0; i < item.TabPosition; i++)            
                msg = $"\t{msg}";
            
            Console.WriteLine($"{msg}");
            
            item.SubMenu?.ForEach(Log);            
        }
    }
}
