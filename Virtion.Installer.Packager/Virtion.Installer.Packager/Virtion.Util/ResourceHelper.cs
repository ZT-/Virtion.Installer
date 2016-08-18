using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Virtion.Util
{
    class ResourceHelper
    {
        public static BitmapImage GetImage(string uri)
        {
            Uri fullUri = new Uri("pack://application:,,,/Virtion.Installer.Packager;component/" + uri, UriKind.Absolute);
            return new BitmapImage(fullUri);
        }

        public static ImageSource LoadIcon(string name)
        {
            Uri uri = new Uri("pack://application:,,,/Virtion.Installer.Packager;component/Resource/" + name, UriKind.Absolute);
            var frame = BitmapFrame.Create(uri);
            return frame;
        }

    }
}
