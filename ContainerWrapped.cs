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
    public class ContainerWrapped : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string callee = "") => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callee));

        public XmlData.Container Wrapped { get; }

        public MainWindowDataContext Owner => this.OwnerWeak.TryGetTarget(out var target) ? target : null;
        private readonly WeakReference<MainWindowDataContext> OwnerWeak;

        public ContainerWrapped(MainWindowDataContext owner) : this(owner, new XmlData.Container()) { }
        public ContainerWrapped(MainWindowDataContext owner, XmlData.Container wrapped)
        {
            this.OwnerWeak = new WeakReference<MainWindowDataContext>(owner);
            this.Wrapped = wrapped;
            this.Items = new ObservableCollection<KeyWrapped>(wrapped.Keys.Select((item) => new KeyWrapped(owner, item)))
            {
                new KeyWrapped(owner)
            };
            this._IsSelected = false;
            this._IsExpanded = true;
        }

        public ObservableCollection<KeyWrapped> Items { get; }

        public bool IsSelected { get => this._IsSelected; set { this._IsSelected = value; this.RaisePropertyChanged(); if (value) { this.Owner.SelectedContainer = this; } } }
        private bool _IsSelected;

        public bool IsExpanded { get => this._IsExpanded; set { this._IsExpanded = value; this.RaisePropertyChanged(); } }
        private bool _IsExpanded;
    }
}
