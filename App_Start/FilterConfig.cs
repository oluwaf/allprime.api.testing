﻿using System.Web;
using System.Web.Mvc;

namespace AllPrime.EndPoint
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
