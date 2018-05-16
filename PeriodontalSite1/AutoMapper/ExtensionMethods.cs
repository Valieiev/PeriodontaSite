using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.AutoMapper
{
    public static class ExtensionMethods
    {
        public static TDest Map<TDest>(this object src)
        {
            return (TDest)Mapper.Map(src, src.GetType(), typeof(TDest));
        }
    }
}