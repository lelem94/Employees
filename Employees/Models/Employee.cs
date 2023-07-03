using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Employees.Models
{
    public class Employee
    {
        public int ID { get; set; } // ID univoco

        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "Il campo nome è obbligatorio")]
        public string FirstName { get; set; } // Nome

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "Il campo cognome è obbligatorio")]
        public string LastName { get; set; } // Cognome

        [Display(Name = "Age")]
        [Required(ErrorMessage = "Inserisci la tua età")]
        public int Age { get; set; } // Età

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Indirizzo email obbligatorio")]
        public string Email { get; set; } // Indirizzo email

        [Display(Name = "Gender")]
        public string Gender { get; set; } // Genere

        [Display(Name = "JobTitle")]
        public string JobTitle { get; set; } // Titolo professionale

        [Display(Name = "Salary")]
        public double Salary { get; set; } // Stipendio

        [Display(Name = "HireDate")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; } // Data di assunzione

        [Display(Name = "Department")]
        public string Department { get; set; } // Ufficio

    }
}