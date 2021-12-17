using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FoodPicker_WPF
{
    public partial class MainWindow : Window
    {

        public static List<string> foodItemList = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            WriteAndReadFile.ReadFile(FoodListBox);
            GoogleMapLoad();

        }

        private void GoogleMapLoad()
        {
            GoogleMapProvider.Instance.ApiKey = "AIzaSyCXJrDpszuNQfMEXKIifx5zYzhSq3Irpyg";

            // config map
            MapControl.MapProvider = GMapProviders.OpenStreetMap;
            MapControl.Position = new PointLatLng(37.21051768521115, 127.08985487358365);
            MapControl.MinZoom = 2;
            MapControl.MaxZoom = 17;
            MapControl.Zoom = 13;
            MapControl.ShowCenter = false;
            MapControl.DragButton = MouseButton.Left;
            MapControl.Position = new PointLatLng(37.21051768521115, 127.08985487358365);
            
            MapControl.MouseLeftButtonDown += new MouseButtonEventHandler(mapControl_MouseLeftButtonDown);
        }
        void mapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPoint = e.GetPosition(MapControl);
            PointLatLng point = MapControl.FromLocalToLatLng((int)clickPoint.X, (int)clickPoint.Y);
            GMapMarker marker = new GMapMarker(point);
            marker.Shape = new Ellipse
            {
                Width = 10,
                Height = 10,
                Stroke = Brushes.Black,
                StrokeThickness = 1.5
            };
            MapControl.Markers.Add(marker);
        }
        // saves data accordingly.
        protected override void OnClosing(CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to save your changes before exiting?", "Save and exit", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("Saved...Closing...", "Save and exit");
                    WriteAndReadFile.WriteFile();
                    e.Cancel = false;
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("Closing...", "Save and exit");
                    e.Cancel = false;
                    break;
                case MessageBoxResult.Cancel:
                    MessageBox.Show("Going back...", "Save and exit");
                    e.Cancel = true;
                    break;

            }

        }

        //밥밥밥 골라라밥
        private void PickButton_Click(object sender, RoutedEventArgs e)
        {
            if (foodItemList.Count != 0)
            {
                Random rndNum = new Random();
                int randomNumber = rndNum.Next(0, foodItemList.Count());

                OutputTextBox.Text = foodItemList[randomNumber].ToString();
            }
            else
            {
                MessageBox.Show(" 이상한데유", "Error");
            }

        }

        //  Food add
        private void AddFoodButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate.InputValidationCheck(InputTextBox.Text) == true)
            {
                foodItemList.Add(InputTextBox.Text.Trim());
                FoodListBox.Items.Add(InputTextBox.Text.Trim());

                MessageBox.Show(InputTextBox.Text.Trim() + " has been added.", "Added");
            }

            InputTextBox.Text = "";

        }

        // Food Delete
        private void DeleteFoodButton_Click(object sender, RoutedEventArgs e)
        {
            if (foodItemList.Contains(InputTextBox.Text))
            {
                foodItemList.Remove(InputTextBox.Text);
                FoodListBox.Items.Remove(InputTextBox.Text);

                MessageBox.Show(InputTextBox.Text + " has been deleted.", "Deleted");
            }
            else
            {
                MessageBox.Show("That food item is has not been added yet.", "Error");
            }

            InputTextBox.Text = "";

        }



    }
}
