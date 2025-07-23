using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book
{
    public class SingletonStorage
    {
        private static SingletonStorage _instance { get; set; }
        public static SingletonStorage Instance
        {
            get
            {
                return _instance ?? (_instance = new SingletonStorage());
            }
        }
    }
}
