using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApp4
{
    class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

		public string Photo { get; set; }

		public string Gender { get; set; }
		public string Status { get; set; }

		public Student(int id, string firstName, string lastName, string email) { 
            
            Id = id;        
            FirstName = firstName;
            LastName = lastName;    
            Email = email;
        }
		public Student(int id, string firstName, string lastName, string email, string gender, string status, string photo) : this(id, firstName, lastName, email)
		{

            Gender = gender;
            Status = status;
            Photo = photo;
		}
			public static Student Validate(string id, string firstName, string lastName, string email, string gender, string status, string photo)
		{
			if (id.Length > 0 && id.Length < 11 && firstName.Length > 0 && lastName.Length > 0 && email.Length > 0 && status.Length > 0 && photo.Length > 0 && gender.Length > 0)
				return new Student(Convert.ToInt32(id), firstName, lastName, email, gender, status, photo);
			return null;

		}
		public static Student Validate(string id, string firstName, string lastName, string email)  {
                if(id.Length > 0 && id.Length < 11 && firstName.Length > 0 && lastName.Length > 0 && email.Length > 0)
                    return new Student(Convert.ToInt32(id), firstName, lastName, email);
            return null;
        
        }
		public override string ToString()
		{
            return $"ID = {Id}, FirstName = {FirstName}, LastName={LastName}, Email={Email}, Gender={Gender}, Status={Status}, Photo={Photo}";
		}
	}
}
