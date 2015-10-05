using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Quartz.Core
{
    public class q_Helper
    {

        public Hashtable optionsToHashTable(string selectionList)
        {
            Hashtable itemList = new Hashtable();

            string[] stringArray = selectionList.Split(',');

            for (int i = 0; i < stringArray.Length - 1; i++)
            {
                itemList.Add(stringArray[i], stringArray[i + 1]);
                i++;    // increment again to move to the pair
            }

            return itemList;
        }

        public Dictionary<string, string> optionsToDictionary(string selectionList)
        {
            Dictionary<string, string> itemList = new Dictionary<string, string>();

            string[] stringArray = selectionList.Split(',');

            for (int i = 0; i < stringArray.Length - 1; i++)
            {
                itemList.Add(stringArray[i], stringArray[i + 1]);
                i++;    // increment again to move to the pair
            }

            return itemList;
        }

        public ArrayList optionsToArrayList(string selectionList)
        {
            ArrayList itemList = new ArrayList();

            string[] stringArray = selectionList.Split(',');

            for (int i = 0; i < stringArray.Length; i++)
            {
                itemList.Add(stringArray[i]);
            }

            return itemList;
        }

        public string addSpacesAfterCommas(string strInput)
        {
            string strOutput = String.Empty ;

            strOutput = strInput.Replace(",", ", ");

            return strOutput;
        }

        public static string replaceSpecialCharacters(string strInput)
        {
            string strOutput = strInput ;

            // double-quotes
            strOutput = strOutput.Replace("\"", "&#34;");
            strOutput = strOutput.Replace("”", "&#34;");
            strOutput = strOutput.Replace("“", "&#34;");

            // single quotes
            strOutput = strOutput.Replace("\'", "&#39;");
            strOutput = strOutput.Replace("‘", "&#39;");
            strOutput = strOutput.Replace("’", "&#39;");

            return strOutput;
        }
    }
}
