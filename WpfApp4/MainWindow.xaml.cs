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

		ImageSource defaultProfilePhoto;

		public MainWindow()
		{
			InitializeComponent();

			defaultProfilePhoto = profilePhoto.Source;

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

			listingGrid.Columns.Add(
				new GridViewColumn
				{
					Header = "Gender",
					DisplayMemberBinding = new Binding("Gender")
				}
				);
			listingGrid.Columns.Add(
				new GridViewColumn
				{
					Header = "Status",
					DisplayMemberBinding = new Binding("Status")
				}
				);


			students = new List<Student>() { 
			new Student(123, "John", "Smith", "john@smith.com", "Male", "Full-Time", "basketball1.png"),
			new Student(456, "Mary", "Brown", "mary@brown.com", "Female", "Full-Time", "basketball2.png"),
			new Student(789, "Liz", "Bot", "liz@bot.com", "Female", "Part-Time", "basketball3.png"),
			new Student(234, "Frank", "Paul", "frank@paul.com", "Male", "Part-Time", "basketball2.png"),
			};
			foreach(var item in students) {
				listing.Items.Add(item);

			}


			studentStatus.Items.Add("Part-Time");
			studentStatus.Items.Add("Full-Time");



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
					studentDetailsEM.Text = selected.Email;
					studentDetailsGE.Text = selected.Gender;
					studentDetailsST.Text = selected.Status;


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
			
			string status = studentStatus.SelectedIndex >= 0 ? studentStatus.SelectedValue.ToString() : "";

			string gender = gender_Undeclared.Content.ToString();

			if ((bool)gender_Male.IsChecked)
				gender = gender_Male.Content.ToString();
			else if ((bool)gender_Female.IsChecked)
				gender = gender_Female.Content.ToString();

			string photo = profilePhoto.Source.ToString();

			Student studentToAdd = Student.Validate(id, fn, ln, em, gender, status, photo);
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

		private void onImageClick(object sender, MouseButtonEventArgs e)
		{
			profilePhoto.Source = (sender as Image).Source;

		}

		private void Button_Reset_Clicked(object sender, RoutedEventArgs e)
		{
			profilePhoto.Source = defaultProfilePhoto;
			
		}

		private void Edit_Student(object sender, RoutedEventArgs e)
		{
			// populate the form with the selected student

			TextBoxID.Text = studentDetailsID.Text;
			TextBoxFN.Text = studentDetailsFN.Text;
			TextBoxLN.Text = studentDetailsLN.Text;
			TextBoxEM.Text = studentDetailsEM.Text;

			string gender = studentDetailsGE.Text;
			string status = studentDetailsST.Text;
			switch(gender)
			{
				case "Male":gender_Male.IsChecked = true; break;
				case "Female":gender_Female.IsChecked = true; break;
				default: gender_Undeclared.IsChecked = true; break;
			}
			studentStatus.SelectedIndex = status.Equals("Full-Time") ? 1 : 0;

			tabs.SelectedIndex = 1;


		}
	}
}