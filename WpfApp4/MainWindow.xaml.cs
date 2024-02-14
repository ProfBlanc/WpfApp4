using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
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
		List<ImageSource> imageSources;
		int selectedStudentIdIndex = -1;


		private ImageSource mapStringToImageSource(String file) {
            foreach (ImageSource item in imageSources) {
                
				if (item.ToString().Contains(file)) {  //@application/package....filename.ext
					return item;
				}
			}

			return null;
		}
		public MainWindow()
		{
			InitializeComponent();

			defaultProfilePhoto = profilePhoto.Source;
			imageSources = new List<ImageSource>() { f1.Source, f2.Source,
			f3.Source, f4.Source, f5.Source, f6.Source };

			studentDetails.Visibility = Visibility.Hidden;
			studentDetails.Background =  Brushes.Beige;

			string str = "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"><Grid><Image Height=\"50\" Width=\"50\" Source=\"{Binding Photo}\" /></Grid></DataTemplate>";
			DataTemplate template = new DataTemplate();
			template = XamlReader.Parse(str) as DataTemplate;


			listingGrid.Columns.Add(
			new GridViewColumn
			{
				Header = "Photo",
				CellTemplate = template
			}
			);

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
					DisplayMemberBinding = new Binding("Status"),
				}
				);

			students = new List<Student>() { 
			new Student(123, "John", "Smith", "john@smith.com", "Male", "Full-Time", "basketball1.png"),
			new Student(456, "Mary", "Brown", "mary@brown.com", "Female", "Full-Time", "basketball2.png"),
			new Student(789, "Liz", "Bot", "liz@bot.com", "Female", "Part-Time", "basketball3.png"),
			new Student(234, "Frank", "Paul", "frank@paul.com", "Male", "Part-Time", "basketball4.png"),
			new Student(579, "Beth", "Ann", "beth@anne.com", "Female", "Full-Time", "basketball5.png"),
			new Student(679, "Conny", "Jet", "conny@jet.com", "Female", "Part-Time", "basketball6.png"),
			};

			AddAllStudentsToListing();
			studentStatus.Items.Add("Part-Time");
			studentStatus.Items.Add("Full-Time");
			studentStatus.SelectedIndex = 0;



		}

		private void AddAllStudentsToListing() {

			foreach (var item in students)
			{
				listing.Items.Add(item);
			}
		}


		private void listing_Click(object sender) {
			ListView listView = sender as ListView;

			if (listView != null)
			{
				selectedStudentIdIndex = listView.SelectedIndex;
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
					studentDetailsPP.Source = mapStringToImageSource(selected.Photo);
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

				gender_Male.IsChecked = false;
				gender_Female.IsChecked = false;
				gender_Undeclared.IsChecked = false;

				studentStatus.SelectedIndex = 0;

				if (!TextBoxID.IsEnabled)
					students[selectedStudentIdIndex] = studentToAdd;
				else
					students.Add(studentToAdd);


				//listing.Items.Add(studentToAdd);
				/* Replace technique
				listing.Items.RemoveAt(selectedStudentIdIndex);
				listing.Items.Insert(selectedStudentIdIndex, studentToAdd);
				*/
				// clear and re-add technique
				listing.Items.Clear();
				AddAllStudentsToListing();

				studentDetailsID.Text = "";
				studentDetailsFN.Text = "";
				studentDetailsLN.Text = "";
				studentDetailsEM.Text = "";
				studentDetailsGE.Text = "";
				studentDetailsST.Text = "";
				studentDetailsPP.Source = defaultProfilePhoto;
				studentDetails.Visibility = Visibility.Hidden;
				TextBoxID.IsEnabled = true;


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

			profilePhoto.Source = studentDetailsPP.Source;
			studentStatus.SelectedIndex = status.Equals("Full-Time") ? 1 : 0;
			Btn_Submit.Content = "Edit";
			tabs.SelectedIndex = 1;
			TextBoxID.IsEnabled = false;


		}


		private void Button_Clear_Click(object sender, RoutedEventArgs e)
		{
			Btn_Submit.Content = "Submit";
			TextBoxID.Text = "";
			TextBoxFN.Text = "";
			TextBoxLN.Text = "";
			TextBoxEM.Text = "";
			gender_Male.IsChecked = false;
			gender_Female.IsChecked = false;
			gender_Undeclared.IsChecked = false;
			studentStatus.SelectedIndex = 0;
			profilePhoto.Source = defaultProfilePhoto;

			//selectedStudentIdIndex = -1;

			TextBoxID.IsEnabled = true;

			
		}

		private void listing_Click_Action(object sender, SelectionChangedEventArgs e)
		{
			listing_Click(sender);
		}

		private void listing_Click_Action(object sender, MouseButtonEventArgs e)
		{
			listing_Click(sender);
		}
	}
}