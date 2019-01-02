using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StringtableEditor.XmlData
{
    [XmlRoot]
    public class Key : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string callee = "") => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callee));

        [XmlAttribute("ID")]
        public string Identifier { get => this._Identifier; set { this._Identifier = value; this.RaisePropertyChanged(); } }
        private string _Identifier;
        [XmlElement]
        public string English { get => this._English; set { this._English = value; this.RaisePropertyChanged(); } }
        private string _English;
        [XmlElement]
        public string Czech { get => this._Czech; set { this._Czech = value; this.RaisePropertyChanged(); } }
        private string _Czech;
        [XmlElement]
        public string French { get => this._French; set { this._French = value; this.RaisePropertyChanged(); } }
        private string _French;
        [XmlElement]
        public string Spanish { get => this._Spanish; set { this._Spanish = value; this.RaisePropertyChanged(); } }
        private string _Spanish;
        [XmlElement]
        public string Italian { get => this._Italian; set { this._Italian = value; this.RaisePropertyChanged(); } }
        private string _Italian;
        [XmlElement]
        public string Polish { get => this._Polish; set { this._Polish = value; this.RaisePropertyChanged(); } }
        private string _Polish;
        [XmlElement]
        public string Portuguese { get => this._Portuguese; set { this._Portuguese = value; this.RaisePropertyChanged(); } }
        private string _Portuguese;
        [XmlElement]
        public string Russian { get => this._Russian; set { this._Russian = value; this.RaisePropertyChanged(); } }
        private string _Russian;
        [XmlElement]
        public string German { get => this._German; set { this._German = value; this.RaisePropertyChanged(); } }
        private string _German;
        [XmlElement]
        public string Korean { get => this._Korean; set { this._Korean = value; this.RaisePropertyChanged(); } }
        private string _Korean;
        [XmlElement]
        public string Japanese { get => this._Japanese; set { this._Japanese = value; this.RaisePropertyChanged(); } }
        private string _Japanese;
        [XmlElement]
        public string Chinese { get => this._Chinese; set { this._Chinese = value; this.RaisePropertyChanged(); } }
        private string _Chinese;
        [XmlElement]
        public string Chinesesimp { get => this._Chinesesimp; set { this._Chinesesimp = value; this.RaisePropertyChanged(); } }
        private string _Chinesesimp;
        [XmlElement]
        public string Turkish { get => this._Turkish; set { this._Turkish = value; this.RaisePropertyChanged(); } }
        private string _Turkish;
        [XmlElement]
        public string Swedish { get => this._Swedish; set { this._Swedish = value; this.RaisePropertyChanged(); } }
        private string _Swedish;
        [XmlElement]
        public string Slovak { get => this._Slovak; set { this._Slovak = value; this.RaisePropertyChanged(); } }
        private string _Slovak;
        [XmlElement]
        public string SerboCroatian { get => this._SerboCroatian; set { this._SerboCroatian = value; this.RaisePropertyChanged(); } }
        private string _SerboCroatian;
        [XmlElement]
        public string Norwegian { get => this._Norwegian; set { this._Norwegian = value; this.RaisePropertyChanged(); } }
        private string _Norwegian;
        [XmlElement]
        public string Icelandic { get => this._Icelandic; set { this._Icelandic = value; this.RaisePropertyChanged(); } }
        private string _Icelandic;
        [XmlElement]
        public string Hungarian { get => this._Hungarian; set { this._Hungarian = value; this.RaisePropertyChanged(); } }
        private string _Hungarian;
        [XmlElement]
        public string Greek { get => this._Greek; set { this._Greek = value; this.RaisePropertyChanged(); } }
        private string _Greek;
        [XmlElement]
        public string Finnish { get => this._Finnish; set { this._Finnish = value; this.RaisePropertyChanged(); } }
        private string _Finnish;
        [XmlElement]
        public string Dutch { get => this._Dutch; set { this._Dutch = value; this.RaisePropertyChanged(); } }
        private string _Dutch;
    }
}