using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TNC.WPF.Event.Infrastucture
{
    public static class CodeGeneration
    {
        public static string Refresh()
        {
            string code = "";
            Random rnd = new Random();
            int n;
            string st = "@#+-#$%^&*!ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
           
                for (int j = 0; j < 8; j++)
                {
                    n = rnd.Next(0, st.Length - 1);
                    code += st.Substring(n, 1);
                }

            return code;
        }
    }
}
