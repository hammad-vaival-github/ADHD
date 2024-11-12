using System;
using System.Collections.Generic;

namespace waats.Classes
{
    public class DataTableAjaxPostModel
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<DataTableColumn> columns { get; set; }
        public DataTableSearch search { get; set; }
        public List<DataTableOrder> order { get; set; }

        /* filters */
        public string Title { get; set; }
        public string TableName { get; set; }


    }
}