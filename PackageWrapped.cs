using StringtableEditor.XmlData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StringtableEditor
{
    public class PackageWrapped : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string callee = "") => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callee));

        public XmlData.Package Wrapped { get; }

        public MainWindowDataContext Owner => this.OwnerWeak.TryGetTarget(out var target) ? target : null;
        private readonly WeakReference<MainWindowDataContext> OwnerWeak;

        public PackageWrapped(MainWindowDataContext owner) : this(owner, new XmlData.Package()) { }
        public PackageWrapped(MainWindowDataContext owner, XmlData.Package wrapped)
        {
            this.OwnerWeak = new WeakReference<MainWindowDataContext>(owner);
            this.Wrapped = wrapped;
            this.Items = new ObservableCollection<ContainerWrapped>(wrapped.Containers.Select((item) => new ContainerWrapped(owner, item)));
            this._IsSelected = false;
            this._IsExpanded = true;
        }

        public ObservableCollection<ContainerWrapped> Items { get; }

        public bool IsSelected { get => this._IsSelected; set { this._IsSelected = value; this.RaisePropertyChanged(); } }
        private bool _IsSelected;

        public bool IsExpanded { get => this._IsExpanded; set { this._IsExpanded = value; this.RaisePropertyChanged(); } }
        private bool _IsExpanded;
    }
}
