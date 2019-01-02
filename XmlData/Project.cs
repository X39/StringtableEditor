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
    public class Project : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string callee = "") => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callee));

        [XmlAttribute("name")]
        public string Name { get => this._Name; set { this._Name = value; this.RaisePropertyChanged(); } }
        private string _Name;

        [XmlElement("Package")]
        public ObservableCollection<Package> Packages { get; set; }

        public Project()
        {
            this.Packages = new ObservableCollection<Package>();
        }
    }
}
