﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace AthenasSpider.Models
{
   public class CheckoutViewModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email Address")]
            public string EmailAddress { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }


            [Display(Name = "Ship to Recipient")]
            public string ShippingRecipient { get; set; }

            [Required]
            [Display(Name = "Ship to Street Address")]
            public string ShippingAddressLine1 { get; set; }

            [Display(Name = "Ship to Apartment/Unit #")]
            public string ShippingAddressLine2 { get; set; }

            [Required]
            [Display(Name = "Ship to City")]
            public string ShippingCity { get; set; }

            [Required]
            [Display(Name = "Ship to State")]
            public string ShippingState { get; set; }

            [Required]
            [Display(Name = "Ship to Zip Code")]
            public string ShippingPostalCode { get; set; }

            [Required]
            [Display(Name = "Credit Card Number")]
            public string CreditCardNumber { get; set; }

            [Required]
            [Display(Name = "CVV")]
            public string CreditCardVerificationValue { get; set; }

            [Required]
            [Display(Name = "Expiration")]
            public int CreditCardExpirationMonth { get; set; }

            [Required]
            public int CreditCardExpirationYear { get; set; }

            [Required]
            public string CreditCardHolderName { get; set; }

            public Order CurrentBasket { get; set; }
        }
    }