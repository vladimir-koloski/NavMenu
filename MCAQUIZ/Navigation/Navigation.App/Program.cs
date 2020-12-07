using Navigation.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Navigation.App
{
    class Program
    {
        public class Item
        {
            public int Id { get; set; }
            public string menuName { get; set; }
            public int? parentID { get; set; }
            public bool isHidden { get; set; }
            public string linkURL { get; set; }
        }
        static void Main(string[] args)
        {            

            string fileFolder = @"C:\Users\User\Desktop\MCA QUIZ\";
            string filePath = fileFolder + "Navigation.csv";

            using (StreamReader sr = new StreamReader(filePath))
            {
                List<Item> items = new List<Item>();
                int index = 0;
                while (!sr.EndOfStream)
                {
                    var dataFile = sr.ReadLine();
                    if (index != 0)
                    {
                        var dataItem = dataFile.Split(';');
                        var id = Convert.ToInt32(dataItem[0]);
                        var menuName = dataItem[1];
                        var parentID = Helper.ConvertToNull(dataItem[2]);
                        var isHidden = Convert.ToBoolean(dataItem[3]);
                        var linkURL = dataItem[4];
                        var item = new Item()
                        {
                            Id = id,
                            menuName = menuName,
                            parentID = parentID,
                            isHidden = isHidden,
                            linkURL = linkURL
                        };
                        items.Add(item);
                    }
                    index++;
                }
                NavigationMenu(items);
            }            
            Console.ReadLine();
        }
        

        public static List<Item> GetItemsByParentId(List<Item> items, int? parentId)
        {
            var parentList = new List<Item>();
            foreach (var item in items)
            {
                if (item.parentID == parentId)
                {
                    parentList.Add(item);
                }
            }
            return parentList.OrderBy(n => n.menuName).ToList();
        }
        public static void NavigationMenu(List<Item> items, int? parentId = null, int level = 0)
        {
            List<Item> itemList = GetItemsByParentId(items, parentId);
            if (itemList.Count > 0)
            {
                foreach (Item item in itemList)
                {
                    if (item.isHidden == true) continue;
                    string menuItemOutput = Helper.AddDots(level) + " " + item.menuName;
                    Console.WriteLine(menuItemOutput);
                    NavigationMenu(items, item.Id, level + 1);
                }
            }
        }
    }
}
