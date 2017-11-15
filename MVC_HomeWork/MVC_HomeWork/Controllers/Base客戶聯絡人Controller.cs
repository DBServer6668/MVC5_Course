using MVC_HomeWork.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_HomeWork.Controllers
{
    public abstract class Base客戶聯絡人Controller : Controller
    {
        protected 客戶資料Entities db = new 客戶資料Entities();
        protected static string _keyword = null, _dropDownList = null;

        public Base客戶聯絡人Controller()
        {

        }

        protected SelectList getdropDownList()
        {
            //var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);
            var categories = db.客戶聯絡人.Where(P => P.是否已刪除 != true).OrderBy(P=>P.職稱);
            var data = from P in categories group P by P.職稱 into g select g.FirstOrDefault();

            List<SelectListItem> datalist = new List<SelectListItem>();
            datalist.Add(new SelectListItem
            {
                Text = "顯示全部分類",
                Value = "顯示全部分類"
            });

            foreach (var ss in data)
            {
                datalist.Add(new SelectListItem
                {
                    Text = ss.職稱,
                    Value = ss.職稱
                });
            }
            SelectList categoryList = new SelectList(datalist, "Value", "Text", string.IsNullOrEmpty(_dropDownList) ? "顯示全部分類" : _dropDownList);

            return categoryList;
        }

        protected object getdata()
        {
            var data_客戶聯絡人 = db.客戶聯絡人.Where(P => P.是否已刪除 != true);
            if (null != _dropDownList && null != _keyword)
            {
                data_客戶聯絡人 = data_客戶聯絡人.Where(P => P.職稱.Equals(_dropDownList));
                data_客戶聯絡人 = data_客戶聯絡人.Where(P => P.姓名.Contains(_keyword));
            }
            else if (null != _dropDownList)
            {
                data_客戶聯絡人 = data_客戶聯絡人.Where(P => P.職稱.Equals(_dropDownList));
            }
            else if (null != _keyword)
            {
                data_客戶聯絡人 = data_客戶聯絡人.Where(P => P.姓名.Contains(_keyword));
            }

            return data_客戶聯絡人;
        }

        protected object dataStoSort(int? id)
        {
            var data_客戶聯絡人 = db.客戶聯絡人.Where(P => P.是否已刪除 != true);
            if (null != _dropDownList && null != _keyword)
            {
                data_客戶聯絡人 = data_客戶聯絡人.Where(P => P.職稱.Equals(_dropDownList));
                data_客戶聯絡人 = data_客戶聯絡人.Where(P => P.姓名.Contains(_keyword));
            }
            else if (null != _dropDownList)
            {
                data_客戶聯絡人 = data_客戶聯絡人.Where(P => P.職稱.Equals(_dropDownList));
            }
            else if (null != _keyword)
            {
                data_客戶聯絡人 = data_客戶聯絡人.Where(P => P.姓名.Contains(_keyword));
            }

            switch (id)
            {
                case 1:
                    data_客戶聯絡人 = data_客戶聯絡人.OrderBy(P => P.職稱);
                    break;
                case 2:
                    data_客戶聯絡人 = data_客戶聯絡人.OrderBy(P => P.姓名);
                    break;
                case 3:
                    data_客戶聯絡人 = data_客戶聯絡人.OrderBy(P => P.Email);
                    break;
                case 4:
                    data_客戶聯絡人 = data_客戶聯絡人.OrderBy(P => P.手機);
                    break;
                case 5:
                    data_客戶聯絡人 = data_客戶聯絡人.OrderBy(P => P.電話);
                    break;
                case 6:
                    data_客戶聯絡人 = data_客戶聯絡人.OrderBy(P => P.客戶資料.客戶名稱);
                    break;

                case 11:
                    data_客戶聯絡人 = data_客戶聯絡人.OrderByDescending(P => P.職稱);
                    break;
                case 22:
                    data_客戶聯絡人 = data_客戶聯絡人.OrderByDescending(P => P.姓名);
                    break;
                case 33:
                    data_客戶聯絡人 = data_客戶聯絡人.OrderByDescending(P => P.Email);
                    break;
                case 44:
                    data_客戶聯絡人 = data_客戶聯絡人.OrderByDescending(P => P.手機);
                    break;
                case 55:
                    data_客戶聯絡人 = data_客戶聯絡人.OrderByDescending(P => P.電話);
                    break;
                case 66:
                    data_客戶聯絡人 = data_客戶聯絡人.OrderByDescending(P => P.客戶資料.客戶名稱);
                    break;
            }

            return data_客戶聯絡人;
        }

        protected JArray GetExportDataWithAllColumns()
        {
            var query = db.客戶聯絡人.Where(P => P.是否已刪除 != true).OrderBy(x => x.Id);

            JArray jObjects = new JArray();

            foreach (var item in query)
            {
                var jo = new JObject
                {
                    {"職稱", item.職稱},
                    {"姓名", item.姓名},
                    {"Email", item.Email},
                    {"手機", item.手機},
                    {"電話", item.電話},
                    {"客戶名稱", item.客戶資料.客戶名稱}
                };
                jObjects.Add(jo);
            }
            return jObjects;
        }
    }
}