using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppurpleKinectProto.modules;

namespace AppurpleKinectProto
{
    /// <summary>
    /// Interaction logic for productpage.xaml
    /// </summary>
    public partial class productpage : Page
    {
        

        public productpage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = productObject.product;
            productBase.tempLoader.LoadNextProduct();
        }

    }
}
