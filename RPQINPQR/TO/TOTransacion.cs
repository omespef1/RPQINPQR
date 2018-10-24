using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPQINPQR.TO
{
    public class TOTransaction
    {
        public int retorno { get; set; }
        public string txtRetorno { get; set; }
    }
    public class TOTransaction<T> where T:class
    {
        public int retorno { get; set; }
        public string txtRetorno { get; set; }
        public T objTransaction { get; set; }
    }
}