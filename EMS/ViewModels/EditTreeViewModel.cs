using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.WpfGraphControl;

namespace EMS.ViewModels
{
    public class EditTreeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public GraphViewer graphViewer = new GraphViewer();

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
            }
        }

        public EditTreeViewModel()
        {
            //GraphViewerPanel = new DockPanel();

        }

        
    }
}
