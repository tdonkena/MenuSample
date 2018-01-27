using System.Collections.Generic;
using Avinode.Menu.BusinessObjects;
using Avinode.Menu.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class AnalyzerTests
    {
        private Menu GetSchedAeroMenu()
        {
            return new Menu{
                new Item{DisplayName = "Home", Path = "/Default.aspx"},
                new Item
                {
                    DisplayName = "Trips",
                    Path = "/Requests/Quotes/CreateQuote.aspx",
                    SubMenu = new Menu
                    {
                        new Item{DisplayName = "Create Quote", Path = "/Requests/Quotes/CreateQuote.aspx"},
                        new Item{DisplayName = "Open Quotes", Path = "/Requests/OpenQuotes.aspx"},
                        new Item{DisplayName = "Scheduled Trips", Path = "/Requests/Trips/ScheduledTrips.aspx"}
                    }
                },
                new Item
                {
                    DisplayName = "Company",
                    Path = "/mvc/company/view",
                    SubMenu = new Menu
                    {
                        new Item{DisplayName = "Customers", Path = "/customers/customers.aspx"},
                        new Item{DisplayName = "Pilots", Path = "/pilots/pilots.aspx"},
                        new Item{DisplayName = "Aircraft", Path = "/aircraft/Aircraft.aspx"}
                    }
                }
            };
        }

        private Menu GetWyvernMenu()
        {
            return new Menu{
                new Item
                {
                    DisplayName = "Home",
                    Path = "/mvc/wyvern/home",
                    SubMenu = new Menu
                    {
                        new Item{DisplayName = "News", Path = "/mvc/wyvern/home/news"},
                        new Item
                        {
                            DisplayName = "Directory",
                            Path = "/Directory/Directory.aspx",
                            SubMenu = new Menu
                            {
                                new Item{DisplayName = "Favorites", Path = "/TWR/Directory.aspx"},
                                new Item{DisplayName = "Search Aircraft", Path = "/TWR/AircraftSearch.aspx"}
                            }
                        }
                    }
                },
                new Item
                {
                    DisplayName = "PASS",
                    Path = "/PASS/GeneratePASS.aspx",
                    SubMenu = new Menu
                    {
                        new Item{DisplayName = "Create New", Path = "/PASS/GeneratePASS.aspx"},
                        new Item{DisplayName = "Sent Requests", Path = "/PASS/YourPASSReports.aspx"},
                        new Item{DisplayName = "Received Requests", Path = "/PASS/Pending/PendingRequests.aspx"}
                    }
                },
                new Item
                {
                    DisplayName = "Company",
                    Path = "/mvc/company/view",
                    SubMenu = new Menu
                    {
                        new Item{DisplayName = "Users", Path = "/mvc/account/list"},
                        new Item{DisplayName = "Aircraft", Path = "/aircraft/fleet.aspx"},
                        new Item{DisplayName = "Insurance", Path = "/insurance/policies.aspx"},
                        new Item{DisplayName = "Certificate", Path = "/Certificates/Certificates.aspx"}

                    }
                }
            };
        }

        [TestMethod]
        public void NoActiveNodesInWyvernMenu()
        {
            var menu = GetWyvernMenu();
            new Analyzer(menu).MarkAsActive(string.Empty);
            menu.ForEach(item => IsActive(item, new List<string>()));
        }

        [TestMethod]
        public void FirstLevelNodeIsActiveInWyvernMenu()
        {
            var menu = GetWyvernMenu();
            new Analyzer(menu).MarkAsActive("/mvc/company/view");
            menu.ForEach(item => IsActive(item, new List<string>{"Company"}));
        }

        [TestMethod]
        public void SecondLevelNodeIsActiveInWyvernoMenu()
        {
            var menu = GetWyvernMenu();
            new Analyzer(menu).MarkAsActive("/insurance/policies.aspx");
            menu.ForEach(item => IsActive(item, new List<string> { "Company", "Insurance" }));
        }

        [TestMethod]
        public void ThirdLevelNodeIsActiveInWyvernoMenu()
        {
            var menu = GetWyvernMenu();
            new Analyzer(menu).MarkAsActive("/TWR/AircraftSearch.aspx");
            menu.ForEach(item => IsActive(item, new List<string> { "Home", "Directory", "Search Aircraft" }));
        }

        [TestMethod]
        public void NoActiveNodesInSchedAeroMenu()
        {
            var menu = GetSchedAeroMenu();
            new Analyzer(menu).MarkAsActive(string.Empty);
            menu.ForEach(item => IsActive(item, new List<string>()));
        }

        [TestMethod]
        public void FirstLevelNodeIsActiveInSchedAeroMenu()
        {
            var menu = GetSchedAeroMenu();
            new Analyzer(menu).MarkAsActive("/Default.aspx");
            menu.ForEach(item => IsActive(item, new List<string> { "Home" }));
        }

        [TestMethod]
        public void SecondLevelNodeIsActiveInSchedAeroMenu()
        {
            var menu = GetSchedAeroMenu();
            new Analyzer(menu).MarkAsActive("/Requests/OpenQuotes.aspx");
            menu.ForEach(item => IsActive(item, new List<string> { "Trips", "Open Quotes" }));
        }

        private void IsActive(Item item, List<string> activeItems)
        {
            Assert.AreEqual(activeItems.Contains(item.DisplayName), item.IsActive, $"Failed for {item.DisplayName} {item.Path}");
            item.SubMenu?.ForEach(subItem => IsActive(subItem, activeItems));
        }
    }
}
