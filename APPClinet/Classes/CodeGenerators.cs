using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Classes
{
    public class CodeGenerators
    {
        // onlinestringtools.com/find-string-length
        // strlength.com
        public Random _rnd = null;
        public string GetCode()
        {
            return Guid.NewGuid().ToString();
        }
        public string ActiveCode()
        {
            _rnd = new Random();
            return _rnd.Next(100000, 999999).ToString();
        }
        public string FileCode()
        {
            return Guid.NewGuid().ToString().Replace("-","");
        }
        public string CodeImage()
        {
            _rnd = new Random();
            return _rnd.Next(100000, 999999).ToString() + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
        }
    }
}
