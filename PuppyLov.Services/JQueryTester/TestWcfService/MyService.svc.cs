using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Security;
using TestWcfService.Items;

namespace TestWcfService
{
    //[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MyService : IMyService
    {
        public bool UpdateItems(IEnumerable<StatusItem> items)
        {
            //proceed updates.
            return true;
        }

        public bool Update(StatusItem item)
        {
            //proceed update.
            return true;
        }

        public void Delete(int ID)
        {
            //proceed delete.
        }

        public TestItem GetItemData(string ID)
        {
            int id = 0;
            if (int.TryParse(ID, out id))
            {
                List<TestItem> items = new List<TestItem>();
                for (int i = 0; i <= id; i++)
                {
                    TestItem tmp = new TestItem { Name = "Name-" + i, Color = "#ffff" + i };
                    items.Add(tmp);
                }

                //Return Last (single item)
                return items.Last();
            }
            return null;
        }

        public List<TestItem> GetItems()
        {
            List<TestItem> items = new List<TestItem>();

            for (int i = 0; i <= 10; i++)
            {
                TestItem tmp = new TestItem { Name = "Name-"+i, Color="#ffff"+i };
                items.Add(tmp);
            }

            return items;
        }
    }
}
