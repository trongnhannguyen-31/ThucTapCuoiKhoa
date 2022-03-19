using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FreshMvvm;

namespace Phoenix.Mobile.Core.Utils
{
    public static class AutoMapperExtension
    {

        public static List<TResult> MapTo<TResult>(this IEnumerable self)
        {
            if (self == null)
            {
                return null;
            }

            var mapper = FreshIOC.Container.Resolve<IMapper>();
            return (List<TResult>)mapper.Map(self, self.GetType(), typeof(List<TResult>));
        }


        public static TResult MapTo<TResult>(this object self) where TResult : class
        {
            if (self == null)
            {
                return null;
            }
            var mapper = FreshIOC.Container.Resolve<IMapper>();
            return (TResult)mapper.Map(self, self.GetType(), typeof(TResult));
        }
        public static TResult MapTo<TResult>(this object self, TResult dest) where TResult : class
        {
            if (self == null)
            {
                return null;
            }
            var mapper = FreshIOC.Container.Resolve<IMapper>();
            return (TResult)mapper.Map(self, dest, self.GetType(), typeof(TResult));
        }
    }
}
