using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Journal_Recorder
{
    public class InvItemDB
    {
        private string Path;
        public InvItemDB(string path) {
            Path = @"..\..\"+path+".xml";
        }
        public void setPath(string path)
        {
            this.Path = @"..\..\" + path+".xml";
        }
        public string getPath()
        {
            return Path;
        }
        public List<JournalRecord> GetItems(bool firstFile)
        {
            // create the list
            List<JournalRecord> items = new List<JournalRecord>();

            // create the XmlReaderSettings object
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            // create the XmlReader object
            
            XmlReader xmlIn = XmlReader.Create(Path, settings);

            // read past all nodes to the first Book node
            if (xmlIn.ReadToDescendant("Journal"))
            {
                // create one Product object for each Product node
                do
                {
                    JournalRecord list = new JournalRecord();
                    xmlIn.ReadStartElement("Journal");
                    if(firstFile)
                        list.category = xmlIn.ReadElementContentAsString();
                    if(!firstFile)
                    {
                        list.date = xmlIn.ReadElementContentAsString();
                        list.journal= xmlIn.ReadElementContentAsString();
                        
                    }
                    items.Add(list);
                }
                while (xmlIn.ReadToNextSibling("Journal"));
            }
            
            // close the XmlReader object
            xmlIn.Close();

            return items;
        }

        public void SaveItems(List<JournalRecord> items, bool firstFile)
        {
            // create the XmlWriterSettings object
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");

            // create the XmlWriter object
            XmlWriter xmlOut = XmlWriter.Create(Path, settings);

            // write the start of the document
            xmlOut.WriteStartDocument();
            xmlOut.WriteStartElement("JournalRecord");

            // write each product object to the xml file
            foreach (JournalRecord item in items)
            {
                xmlOut.WriteStartElement("Journal");
                if(firstFile)
                    xmlOut.WriteElementString("Category", item.category);
                else if(!firstFile)
                {
                    xmlOut.WriteElementString("Date", item.date);
                    xmlOut.WriteElementString("Journal", item.journal);
                }
                
                xmlOut.WriteEndElement();
            }

            // write the end tag for the root element
            xmlOut.WriteEndElement();

            // close the xmlWriter object
            xmlOut.Close();
        }
    }
}
