using StoreMSDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreMSDemo.Controllers
{
    public class ItemsController : Controller
    {
        // GET: Items
        public ActionResult DisplayItems()
        {
            List<Item> iList = new List<Item>();
            using (StoreMSEntities db = new StoreMSEntities())
            {
                foreach (var item in db.Items)
                {
                    iList.Add(item);
                }
            }

            return View("DisplayItems", iList);
        }

        public ActionResult NewItem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveItem(Item Model)
        {
            using (StoreMSEntities db = new StoreMSEntities())
            {
                Item objItem = new Item();
                objItem.ItemCode = Model.ItemCode;
                objItem.ItemName = Model.ItemName;
                objItem.ItemType = Model.ItemType;
                objItem.Quantity = Model.Quantity;
                objItem.UserID = Convert.ToInt32(Session["UserID"]);
                objItem.TDate = System.DateTime.Now;

                db.Items.Add(objItem);
                db.SaveChanges();
            }

            return RedirectToAction("DisplayItems", "Items", Model);
        }

        public ActionResult Print(string ItemCode)
        {
            List<Item> iList = new List<Item>();
            using (StoreMSEntities db = new StoreMSEntities())
            {
                foreach (var item in db.Items)
                {
                    iList.Add(item);
                }
            }

            FileManager fileManager = new FileManager();
            string BarCodePath = fileManager.CreateBarCode(ItemCode, 1);

            ViewData["BarCodePath"] = BarCodePath;

            return View();
        }
    }
}