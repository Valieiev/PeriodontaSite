﻿using PeriodontalSite1.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = (" Name is required.")), RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only alphabetic characters are allowed.")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = (" Surname is required.")), RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only alphabetic characters are allowed.")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = ("Patronymic  is required.")), RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only alphabetic characters are allowed.")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

       

        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birth { get; set; }

        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Пол")]
        public string Sex { get; set; }

        [Required]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}