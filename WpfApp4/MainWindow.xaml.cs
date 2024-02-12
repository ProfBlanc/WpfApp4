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

namespace WpfApp4
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		List<Student> students;
		public MainWindow()
		{
			InitializeComponent();

			studentDetails.Visibility = Visibility.Hidden;
			studentDetails.Background =  Brushes.Beige;

			listingGrid.Columns.Add(
				new GridViewColumn
				{
					Header = "ID",
					DisplayMemberBinding = new Binding("Id")
				}
				);
			listingGrid.Columns.Add(
				new GridViewColumn
				{
					Header = "First Name",
					DisplayMemberBinding = new Binding("FirstName")
				}
				);
			listingGrid.Columns.Add(
				new GridViewColumn
				{
					Header = "Last Name",
					DisplayMemberBinding = new Binding("LastName")
				}
				);
			listingGrid.Columns.Add(
				new GridViewColumn { 
				Header="Email", DisplayMemberBinding = new Binding("Email") 
				}
				);

			students = new List<Student>() { 
			new Student(123, "John", "Smith", "john@smith.com"),
			new Student(456, "Mary", "Brown", "mary@brown.com"),
			new Student(789, "Liz", "Bot", "liz@bot.com"),
			new Student(234, "Frank", "Paul", "frank@paul.com"),
			};
			foreach(var item in students) {
				listing.Items.Add(item);

			}

		}

		private void listing_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListView listView = sender as ListView;

			if (listView != null)
			{
				int indexSelected = listView.SelectedIndex;
				Student selected = listView.SelectedItem as Student;
				if (selected != null)
				{
					//Console.WriteLine(selected);
					//MessageBox.Show("Student = " + selected.ToString());

					studentDetailsID.Text = selected.Id.ToString();
					studentDetailsFN.Text = selected.FirstName;
					studentDetailsLN.Text = selected.LastName;
					studentDetailsIdEM.Text = selected.Email;

					studentDetails.Visibility = Visibility.Visible;

                }

			}

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string id = TextBoxID.Text;
			string fn = TextBoxFN.Text;
			string ln = TextBoxLN.Text;
			string em = TextBoxEM.Text;

			Student studentToAdd = Student.Validate(id, fn, ln, em);
			if (studentToAdd == null)
			{
				MessageBox.Show("Error! Please fill out all fields!", "Error!",MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else{
				MessageBox.Show("Good job!", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
				TextBoxID.Text = "";
				TextBoxFN.Text = "";
				TextBoxLN.Text = "";
				TextBoxEM.Text = "";

				students.Add(studentToAdd);
				listing.Items.Add(studentToAdd);

				tabs.SelectedIndex = 0;
			}

		}
	}
}