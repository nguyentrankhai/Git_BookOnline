using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Template.Domain.Highlight
{
    public class HighlightCollection
    {
        private List<Collection> item;
        public List<Collection> Item
        {
            get
            {
                return item;
            }

            set
            {
                item = value;
            }
        }
        public HighlightCollection()
        {
            item = new List<Collection>();
        }
    }
    public class Collection
    {
        private List<Highlight> listTextMarkup;
        public List<Highlight> ListTextMarkup
        {
            get
            {
                return listTextMarkup;
            }

            set
            {
                listTextMarkup = value;
            }
        }
        public string BookName { get; set; }
        public Collection()
        {
            listTextMarkup = new List<Highlight>();
        }
    }
}
