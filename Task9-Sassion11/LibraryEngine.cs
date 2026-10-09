using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9_Sassion11
{
    public delegate string BookDelegate(Book B); 
    public class LibraryEngine 
    { 
        public static void ProcessBooks(List<Book> BList, BookDelegate fPtr) 
        { 
            foreach (Book B in BList) 
            { 
                Console.WriteLine(fPtr(B)); 
            } 
        } 
    }
}

