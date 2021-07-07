using System;
using System.Collections.Generic;
using Xamarin.Forms;
using YogaClicks.MenuItems;

namespace YogaClicks
{
    public partial class MainPage : MasterDetailPage
    {
        public List<MasterPageItem> MenuList { get; set; }

        public MainPage()
		{
			InitializeComponent();

            MenuList = new List<MasterPageItem>();
            //this are for android Icons you can download from android asset studio and include in Your Project Resources Folder
            // Creating our pages for menu navigation
            // Here you can define title for item, 
            // icon on the left side, and page that you want to open after selection
            //var page1 = new MasterPageItem() { Title = "FastDelivery", Icon = "ic_local_shipping_black_24dp.png", TargetType = typeof(View1) };
            //var page2 = new Mas   terPageItem() { Title = "Menus", Icon = "ic_restaurant_black_24dp", TargetType = typeof(View2) };
            //var page3 = new MasterPageItem() { Title = "Free Pizza", Icon = "ic_local_pizza_black_24dp.png", TargetType = typeof(View3) };
         

            //Fot Ios icons
            var page1 = new MasterPageItem() { Title = "FastDelivery", Icon = "", TargetType = typeof(Views.AboutUs) };
            var page2 = new MasterPageItem() { Title = "Menus", Icon = "", TargetType = typeof(Views.Styles) };
            var page3 = new MasterPageItem() { Title = "Free Pizza", Icon = "", TargetType = typeof(Views.Shop) };

            // Adding menu items to menuList
            MenuList.Add(page1);
            MenuList.Add(page2);
            MenuList.Add(page3);
   


            // Setting our list to be ItemSource for ListView in MainPage.xaml  
            navigationDrawerList.ItemsSource = MenuList;
            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Views.AboutUs )));
               this.BindingContext = new
            {
                Header = "",
                Image = "http://www3.hilton.com/resources/media/hi/GSPSCHF/en_US/img/shared/full_page_image_gallery/main/HH_food_22_675x359_FitToBoxSmallDimension_Center.jpg",
                //Footer = "         -------- Welcome To HotelPlaza --------           "
                Footer = "Welcome To Hotel Plaza"
            };
        }



        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }
}
