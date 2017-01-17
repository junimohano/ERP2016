using System;

namespace Erp2016.Lib
{
    [Serializable]
    public class CListModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
        public bool Selected { get; set; }
    }
}