using Avinode.Menu.BusinessObjects;
using Avinode.Menu.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class LoaderTests
    {
        [TestMethod]
        public void LoadSchedAeroMenu()
        {
            var loader = new Loader();
            var menu = loader.Load(@"XmlFiles\SchedAero Menu.txt");
            Assert.AreEqual(3, menu.Count);
            AssertItem(menu[0], "Home", "/Default.aspx");
            AssertItem(menu[1], "Trips", "/Requests/Quotes/CreateQuote.aspx", true, 3);
            AssertItem(menu[1].SubMenu[0], "Create Quote", "/Requests/Quotes/CreateQuote.aspx");
            AssertItem(menu[1].SubMenu[1], "Open Quotes", "/Requests/OpenQuotes.aspx");
            AssertItem(menu[1].SubMenu[2], "Scheduled Trips", "/Requests/Trips/ScheduledTrips.aspx");
            AssertItem(menu[2], "Company", "/mvc/company/view", true, 3);
            AssertItem(menu[2].SubMenu[0], "Customers", "/customers/customers.aspx");
            AssertItem(menu[2].SubMenu[1], "Pilots", "/pilots/pilots.aspx");
            AssertItem(menu[2].SubMenu[2], "Aircraft", "/aircraft/Aircraft.aspx");
        }

        [TestMethod]
        public void LoadWyvernMenu()
        {
            var loader = new Loader();
            var menu = loader.Load(@"XmlFiles\Wyvern Menu.txt");
            Assert.AreEqual(3, menu.Count);
            AssertItem(menu[0], "Home", "/mvc/wyvern/home", true, 2);
            AssertItem(menu[0].SubMenu[0], "News", "/mvc/wyvern/home/news");
            AssertItem(menu[0].SubMenu[1], "Directory", "/Directory/Directory.aspx", true, 2);
            AssertItem(menu[0].SubMenu[1].SubMenu[0], "Favorites", "/TWR/Directory.aspx");
            AssertItem(menu[0].SubMenu[1].SubMenu[1], "Search Aircraft", "/TWR/AircraftSearch.aspx");
            AssertItem(menu[1], "PASS", "/PASS/GeneratePASS.aspx", true, 3);
            AssertItem(menu[1].SubMenu[0], "Create New", "/PASS/GeneratePASS.aspx");
            AssertItem(menu[1].SubMenu[1], "Sent Requests", "/PASS/YourPASSReports.aspx");
            AssertItem(menu[1].SubMenu[2], "Received Requests", "/PASS/Pending/PendingRequests.aspx");
            AssertItem(menu[2], "Company", "/mvc/company/view", true, 4);
            AssertItem(menu[2].SubMenu[0], "Users", "/mvc/account/list");
            AssertItem(menu[2].SubMenu[1], "Aircraft", "/aircraft/fleet.aspx");
            AssertItem(menu[2].SubMenu[2], "Insurance", "/insurance/policies.aspx");
            AssertItem(menu[2].SubMenu[3], "Certificate", "/Certificates/Certificates.aspx");
        }

        private void AssertItem(Item menuItem, string displayName, string path, bool hasSubMenus = false, int subMenuCount = 0)
        {
            Assert.AreEqual(displayName, menuItem.DisplayName);
            Assert.AreEqual(path, menuItem.Path);
            Assert.AreEqual(hasSubMenus, menuItem.SubMenu != null);
            if (hasSubMenus)
                Assert.AreEqual(subMenuCount, menuItem.SubMenu.Count);
        }
    }
}