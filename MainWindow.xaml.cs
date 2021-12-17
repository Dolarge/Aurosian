using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;


namespace FoodPicker_WPF
{
    public partial class MainWindow : Window
    {

        public static List<string> foodItemList = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            WriteAndReadFile.ReadFile(FoodListBox);

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
