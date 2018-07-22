using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class FrequentlyUsed : IDisposable
    {
        //IDisposable//
        public void Dispose()
        {

        }//adds Dispose Capabilitie
        ~FrequentlyUsed()
        {

        }//Clean up unmanaged resources in case users of your library don't call 'Dispose'.

        //Basic//
        public string lol()
        {
            return "lol";
        }
        //Complex//
    }
}
