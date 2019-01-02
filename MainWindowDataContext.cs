using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace StringtableEditor
{
    public class MainWindowDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string callee = "") => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callee));

        public ICommand CmdFileOpen => new RelayCommand(() =>
        {
            var dlg = new OpenFileDialog()
            {
                Filter = "*.xml|*.xml",
                CheckFileExists = true,
                Multiselect = false
            };
            var dlgres = dlg.ShowDialog();
            if (dlgres.HasValue)
            {
                this.LoadXml(dlg.FileName);
            }
        });
        public ICommand CmdFileSaveAs => new RelayCommand(() =>
        {
            var dlg = new SaveFileDialog()
            {
                Filter = "*.xml|*.xml"
            };
            var dlgres = dlg.ShowDialog();
            if (dlgres.HasValue)
            {
                this.SaveXml(dlg.FileName);
            }
        });
        public ICommand CmdFileSave => new RelayCommand(() => this.SaveXml(this.LastFileName));

        public string LastFileName { get => Properties.Settings.Default.LastFilePath; set { Properties.Settings.Default.LastFilePath = value; this.RaisePropertyChanged(); Properties.Settings.Default.Save(); } }

        private static readonly string[] Languages;
        static MainWindowDataContext()
        {
            Languages = new string[] { " ", "English", "Czech", "French", "Spanish", "Italian", "Polish", "Portuguese", "Russian", "German", "Korean", "Japanese", "Chinese", "Chinesesimp", "Turkish", "Swedish", "Slovak", "SerboCroatian", "Norwegian", "Icelandic", "Hungarian", "Greek", "Finnish", "Dutch" };
            Array.Sort(Languages);
            Languages[0] = "Original";
        }
        public IEnumerable<string> AvailableLanguages => Languages;


        public string SelectedLanguageA
        {
            get => Properties.Settings.Default.LastLanguageA;
            set
            {
                Properties.Settings.Default.LastLanguageA = value;
                this.RaisePropertyChanged();
                Properties.Settings.Default.Save();
                if (this.SelectedContainer != null)
                {
                    foreach (var it in this.SelectedContainer.Items)
                    {
                        it.RaisePropertyChanged(nameof(it.LanguageA));
                    }
                }
            }
        }
        public string SelectedLanguageB
        {
            get => Properties.Settings.Default.LastLanguageB;
            set
            {
                Properties.Settings.Default.LastLanguageB = value;
                this.RaisePropertyChanged();
                Properties.Settings.Default.Save();
                if (this.SelectedContainer != null)
                {
                    foreach (var it in this.SelectedContainer.Items)
                    {
                        it.RaisePropertyChanged(nameof(it.LanguageB));
                    }
                }
            }
        }
        public ContainerWrapped SelectedContainer { get => this._SelectedContainer; set { this._SelectedContainer = value; this.RaisePropertyChanged(); } }
        private ContainerWrapped _SelectedContainer;
        public ObservableCollection<ProjectWrapped> TreeViewSource { get => this._TreeViewSource; set { this._TreeViewSource = value; this.RaisePropertyChanged(); } }
        private ObservableCollection<ProjectWrapped> _TreeViewSource;
        public KeyWrapped SelectedEntry { get => this._SelectedEntry; set { this._SelectedEntry = value; value.OnSelected(); this.RaisePropertyChanged(); } }
        private KeyWrapped _SelectedEntry;

        public MainWindowDataContext()
        {
            this.TreeViewSource = new ObservableCollection<ProjectWrapped>
            {
                new ProjectWrapped(this, new XmlData.Project {
                    Name = "Project",
                    Packages = new ObservableCollection<XmlData.Package>
                    {
                        new XmlData.Package
                        {
                            Name = "Package",
                            Containers = new ObservableCollection<XmlData.Container>
                            {
                                new XmlData.Container
                                {
                                    Name = "Container"
                                }
                            }
                        }
                    }
                })
            };
            if (File.Exists(Properties.Settings.Default.LastFilePath))
            {
                this.LoadXml(Properties.Settings.Default.LastFilePath);
            }
        }

        public void LoadXml(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                return;
            }
            this.LastFileName = path;
            try
            {
                using (var stream = File.OpenRead(path))
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    var serializer = new XmlSerializer(typeof(XmlData.Project));
                    var project = serializer.Deserialize(reader) as XmlData.Project;
                    if (project.Packages.Count == 0)
                    {
                        MessageBox.Show("Malformed Project", "Failed to load stringtable.xml");
                        return;
                    }
                    if (project.Packages.Any((it) => it.Containers.Count == 0))
                    {
                        MessageBox.Show("Malformed Package", "Failed to load stringtable.xml");
                        return;
                    }
                    this.SelectedContainer = null;
                    this.TreeViewSource = new ObservableCollection<ProjectWrapped>
                {
                    new ProjectWrapped(this, project)
                };
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed to load stringtable.xml");
            }
        }
        public void SaveXml(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                return;
            }
            this.LastFileName = path;
            try
            {
                using (var stream = File.Open(path, FileMode.Create))
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    var serializer = new XmlSerializer(typeof(XmlData.Project));
                    serializer.Serialize(writer, this.TreeViewSource.First().Wrapped);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed to load stringtable.xml");
            }
        }
    }
}
