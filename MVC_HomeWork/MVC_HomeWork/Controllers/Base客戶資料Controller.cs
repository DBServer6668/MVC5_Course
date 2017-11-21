using MVC_HomeWork.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_HomeWork.Controllers
{
    public abstract class Base客戶資料Controller : Controller
    {
        protected 客戶資料Entities db = new 客戶資料Entities();
        protected static string _keyword = null, _dropDownList = null;

        public Base客戶資料Controller()
        {

        }

        protected SelectList getdropDownList()
        {
            List<SelectListItem> datalist = new List<SelectListItem>();
            datalist.Add(new SelectListItem
            {
                Text = "顯示全部分類",
                Value = "顯示全部分類"
            });

            datalist.Add(new SelectListItem
            {
                Text = "一般客戶",
                Value = "一般客戶"
            });
            datalist.Add(new SelectListItem
            {
                Text = "付費客戶",
                Value = "付費客戶"
            });
            datalist.Add(new SelectListItem
            {
                Text = "VIP客戶",
                Value = "VIP客戶"
            });

            SelectList categoryList = new SelectList(datalist, "Value", "Text", string.IsNullOrEmpty(_dropDownList) ? "顯示全部分類" : _dropDownList);

            return categoryList;
        }

        protected object getdata()
        {
            var data = db.客戶資料.Where(P => P.是否已刪除 != true);
            if (null != _dropDownList && null != _keyword)
            {
                data = data.Where(P => P.客戶分類.Equals(_dropDownList));
                data = data.Where(P => P.客戶名稱.Contains(_keyword));
            }
            else if (null != _dropDownList)
            {
                data = data.Where(P => P.客戶分類.Equals(_dropDownList));
            }
            else if (null != _keyword)
            {
                data = data.Where(P => P.客戶名稱.Contains(_keyword));
            }

            return data;
        }

        protected object dataStoSort(int? id)
        {
            var data = db.客戶資料.Where(P => P.是否已刪除 != true);
            if (null != _dropDownList && null != _keyword)
            {
                data = data.Where(P => P.客戶分類.Equals(_dropDownList));
                data = data.Where(P => P.客戶名稱.Contains(_keyword));
            }
            else if (null != _dropDownList)
            {
                data = data.Where(P => P.客戶分類.Equals(_dropDownList));
            }
            else if (null != _keyword)
            {
                data = data.Where(P => P.客戶名稱.Contains(_keyword));
            }

            switch (id)
            {
                case 1:
                    data = data.OrderBy(P => P.客戶名稱);
                    break;
                case 2:
                    data = data.OrderBy(P => P.統一編號);
                    break;
                case 3:
                    data = data.OrderBy(P => P.電話);
                    break;
                case 4:
                    data = data.OrderBy(P => P.傳真);
                    break;
                case 5:
                    data = data.OrderBy(P => P.地址);
                    break;
                case 6:
                    data = data.OrderBy(P => P.Email);
                    break;
                case 7:
                    data = data.OrderBy(P => P.客戶分類);
                    break;

                case 11:
                    data = data.OrderByDescending(P => P.客戶名稱);
                    break;
                case 22:
                    data = data.OrderByDescending(P => P.統一編號);
                    break;
                case 33:
                    data = data.OrderByDescending(P => P.電話);
                    break;
                case 44:
                    data = data.OrderByDescending(P => P.傳真);
                    break;
                case 55:
                    data = data.OrderByDescending(P => P.地址);
                    break;
                case 66:
                    data = data.OrderByDescending(P => P.Email);
                    break;
                case 77:
                    data = data.OrderByDescending(P => P.客戶分類);
                    break;
            }

            return data;
        }

        protected JArray GetExportDataWithAllColumns()
        {
            var query = db.客戶資料.Where(P => P.是否已刪除 != true).OrderBy(P => P.Id);
            if (null != _dropDownList)
            {
                query = query.Where(P => P.客戶分類.Equals(_dropDownList)).OrderBy(P => P.Id);
            }

            JArray jObjects = new JArray();
            foreach (var item in query)
            {
                var jo = new JObject
                {
                    {"客戶名稱", item.客戶名稱},
                    {"統一編號", item.統一編號},
                    {"電話", item.電話},
                    {"傳真", item.傳真},
                    {"地址", item.地址},
                    {"Email", item.Email},
                    {"客戶分類", item.客戶分類}
                };
                jObjects.Add(jo);
            }
            return jObjects;
        }

        protected object getStatistics()
        {
            var db = new 客戶資料Entities();
            var result = db.客戶資料.Where(P => P.是否已刪除 != true);

            // LINQ ( C# 3.0 )
            //var data = "SELECT * FROM table WHERE ...";
            var data = from p in result
                       select new 客戶統計()
                       {
                           客戶名稱 = p.客戶名稱,
                           銀行帳戶數量 = p.客戶銀行資訊.Count(),
                           聯絡人數量 = p.客戶聯絡人.Count()
                       };

            return data;
        }

        protected object dataStoSort_Statistics(int? id)
        {
            var db = new 客戶資料Entities();
            var result = db.客戶資料.Where(P => P.是否已刪除 != true);

            // LINQ ( C# 3.0 )
            //var data = "SELECT * FROM table WHERE ...";
            var data = from p in result
                       select new 客戶統計()
                       {
                           客戶名稱 = p.客戶名稱,
                           銀行帳戶數量 = p.客戶銀行資訊.Count(),
                           聯絡人數量 = p.客戶聯絡人.Count()
                       };

            switch (id)
            {
                case 1:
                    data = data.OrderBy(P => P.客戶名稱);
                    break;
                case 2:
                    data = data.OrderBy(P => P.聯絡人數量);
                    break;
                case 3:
                    data = data.OrderBy(P => P.銀行帳戶數量);
                    break;

                case 11:
                    data = data.OrderByDescending(P => P.客戶名稱);
                    break;
                case 22:
                    data = data.OrderByDescending(P => P.聯絡人數量);
                    break;
                case 33:
                    data = data.OrderByDescending(P => P.銀行帳戶數量);
                    break;
            }

            return data;
        }

        protected JArray GetExportDataWithAllColumns_Statistics()
        {
            var db = new 客戶資料Entities();
            var result = db.客戶資料.Where(P => P.是否已刪除 != true);

            // LINQ ( C# 3.0 )
            //var data = "SELECT * FROM table WHERE ...";
            var data = from p in result
                       select new 客戶統計()
                       {
                           客戶名稱 = p.客戶名稱,
                           銀行帳戶數量 = p.客戶銀行資訊.Count(),
                           聯絡人數量 = p.客戶聯絡人.Count()
                       };

            JArray jObjects = new JArray();

            foreach (var item in data)
            {
                var jo = new JObject
                {
                    {"客戶名稱", item.客戶名稱},
                    {"聯絡人數量", item.聯絡人數量},
                    {"銀行帳戶數量", item.銀行帳戶數量}
                };
                jObjects.Add(jo);
            }
            return jObjects;
        }
    }
}