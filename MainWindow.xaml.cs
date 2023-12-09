using FinalTask.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CloudIpspSDK;
using CloudIpspSDK.Checkout;

namespace FinalTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppDbContext _db;
        private ObservableCollection<Item> _items;
        private List<Item> CartToCheckout = new List<Item>();
        public int CartSum = 0;
        public int CartItemsTotal = 0;

        public MainWindow()
        {
            InitializeComponent();
            _db = new AppDbContext();
            LoadItems();
            ItemsListBox.ItemsSource = _items;
        }

        private void LoadItems()
        {
            _items = new ObservableCollection<Item>(_db.items.ToList());
            ItemsListBox.ItemsSource = _items;
        }

        private void AddToCartBtn_Click(object sender, RoutedEventArgs e)
        {
            Button addButton = (Button)sender;
            Item item = (Item)addButton.DataContext;

            if (item != null)
            {
                CartToCheckout.Add(item);
                UpdateCartInfo();
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }
        }

        private void UpdateCartInfo()
        {
            CartSum = CartToCheckout.Sum(x => x.Price);
            CartItemsTotal = CartToCheckout.Count;
            CartInfoBlock.Text = $"Items are in your cart\nTotal Items: {CartItemsTotal}\nTotal Price: {CartSum}$";
        }

        private void CartPayBtn_Click(object sender, RoutedEventArgs e)
        {
            Config.MerchantId = 1396424;
            Config.SecretKey = "test";

            var req = new CheckoutRequest
            {
                order_id = Guid.NewGuid().ToString("N"),
                amount = CartSum * 100,
                order_desc = "checkout json demo",
                currency = "USD"
            };
            var resp = new Url().Post(req);
            string url = "/";
            if (resp.Error == null)
            {
                url = resp.checkout_url;
                System.Diagnostics.Process.Start(url);
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }

        }
    }
}
