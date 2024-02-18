using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coreConsoleProject
{
    enum IndexNames: int
    {
        Zero = 0,
        One,
        Two,
        Three,
        Four,
        Five,
        Ten = 10,
        Fifteen = 15
    }

    internal class Indexer
    {
        // Indexer
        // Makes object indexable
        // string is value type and return type
       public string this[int index]
       {
        get {
            if (index < 0 || index >= strArr.Length)
                throw new ArgumentOutOfRangeException("Index out of range");
            return strArr[index];
        }
        set {
            if (index < 0 || index >= strArr.Length)
                throw new ArgumentOutOfRangeException("Index out of range");
            strArr[index] = value;
        }
       }

       private string[] strArr = new string[10];
    }


}
