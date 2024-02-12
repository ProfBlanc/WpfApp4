using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
    class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public Student(int id, string firstName, string lastName, string email) { 
            
            Id = id;        
            FirstName = firstName;
            LastName = lastName;    
            Email = email;
        }
        public static Student Validate(string id, string firstName, string lastName, string email)  {
                if(id.Length > 0 && firstName.Length > 0 && lastName.Length > 0 && email.Length > 0)
                    return new Student(Convert.ToInt32(id), firstName, lastName, email);
            return null;
        
        }
		public override string ToString()
		{
            return $"ID = {Id}, FirstName = {FirstName}, LastName={LastName}, Email={Email}";
		}
	}
}
