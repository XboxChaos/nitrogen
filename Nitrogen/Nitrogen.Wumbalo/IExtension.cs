using System;
using System.IO;
using System.Windows.Controls;

namespace Nitrogen.Wumbalo
{
    public interface IExtension
    {
        string Title { get; set; }

        object StatusBarContents { get; set; }

        MenuItem[] GetMenuItems();

        Panel CreateView();

        void Open(Stream input);

        Stream Save();
    }
}
