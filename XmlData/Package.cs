using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StringtableEditor.XmlData
{
    [XmlRoot]
    public class Package : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string callee = "") => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callee));

        [XmlAttribute("name")]
        public string Name { get => this._Name; set { this._Name = value; this.RaisePropertyChanged(); } }
        private string _Name;

        [XmlElement("Container")]
        public ObservableCollection<Container> Containers { get; set; }

        public Package()
        {
            this.Containers = new ObservableCollection<Container>();
        }
    }
}