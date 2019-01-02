using StringtableEditor.XmlData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StringtableEditor
{
    public class KeyWrapped : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string callee = "") => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callee));

        public XmlData.Key Wrapped { get; }

        public MainWindowDataContext Owner => this.OwnerWeak.TryGetTarget(out var target) ? target : null;
        private readonly WeakReference<MainWindowDataContext> OwnerWeak;

        public KeyWrapped(MainWindowDataContext owner) : this(owner, new XmlData.Key()) { this.Existing = false; }
        public KeyWrapped(MainWindowDataContext owner, XmlData.Key wrapped)
        {
            this.OwnerWeak = new WeakReference<MainWindowDataContext>(owner);
            this.Wrapped = wrapped;
            this.Existing = true;
        }

        public bool Existing { get => this._Existing; set { this._Existing = value; this.RaisePropertyChanged(); } }
        private bool _Existing;

        public void OnSelected()
        {
            if (!this.Existing)
            {
                this.Existing = true;
                this.Owner.SelectedContainer.Items.Add(new KeyWrapped(this.Owner));
            }
        }

        private string GetLanguage(string key)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                return String.Empty;
            }
            var t = typeof(XmlData.Key);
            return t.GetProperty(key).GetValue(this.Wrapped) as string;
        }
        private void SetLanguage(string key, string value)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                return;
            }
            var t = typeof(XmlData.Key);
            t.GetProperty(key).SetValue(this.Wrapped, value);
        }

        public string LanguageA { get => this.GetLanguage(this.Owner.SelectedLanguageA); set { this.SetLanguage(this.Owner.SelectedLanguageA, value); this.RaisePropertyChanged(); if (this.Owner.SelectedLanguageA == this.Owner.SelectedLanguageB) { this.RaisePropertyChanged(nameof(this.LanguageB)); } } }
        public string LanguageB { get => this.GetLanguage(this.Owner.SelectedLanguageB); set { this.SetLanguage(this.Owner.SelectedLanguageB, value); this.RaisePropertyChanged(); if (this.Owner.SelectedLanguageA == this.Owner.SelectedLanguageB) { this.RaisePropertyChanged(nameof(this.LanguageA)); } } }
    }
}
