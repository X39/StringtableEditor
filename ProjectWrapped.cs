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
    public class ProjectWrapped : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string callee = "") => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callee));

        public XmlData.Project Wrapped { get; }

        public MainWindowDataContext Owner => this.OwnerWeak.TryGetTarget(out var target) ? target : null;
        private readonly WeakReference<MainWindowDataContext> OwnerWeak;

        public ProjectWrapped(MainWindowDataContext owner) : this(owner, new XmlData.Project()) { }
        public ProjectWrapped(MainWindowDataContext owner, XmlData.Project wrapped)
        {
            this.OwnerWeak = new WeakReference<MainWindowDataContext>(owner);
            this.Wrapped = wrapped;
            this.Items = new ObservableCollection<PackageWrapped>(wrapped.Packages.Select((item) => new PackageWrapped(owner, item)));
            this._IsSelected = false;
            this._IsExpanded = true;
        }

        public ObservableCollection<PackageWrapped> Items { get; }

        public bool IsSelected { get => this._IsSelected; set { this._IsSelected = value; this.RaisePropertyChanged(); } }
        private bool _IsSelected;

        public bool IsExpanded { get => this._IsExpanded; set { this._IsExpanded = value; this.RaisePropertyChanged(); } }
        private bool _IsExpanded;
    }
}
