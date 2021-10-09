using Stripe;
using CLED.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLED.Data;
using CLED.Areas.Identity.Data;
using Microsoft.Extensions.Logging;
using CLED.Controllers;
using Microsoft.AspNetCore.Identity;

namespace CLED.Payment
{
    public class ProcessPayment
    {
       
        public static async Task<dynamic> PayAsync(PayModel payModel)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51JRlz7JGbBjxQ0BEOsH8h7jkYnXPqiJvRTmNtui7lLIfRXiG8hg83aQlk5IS3n0rcBnAbJh3OSCnwoaec8p6AVpB00fFyMN5vi";

                var options = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = payModel.CardNumder,
                        ExpMonth = payModel.Month,
                        ExpYear = payModel.Year,
                        Cvc = payModel.CVC
                    },
                };

                var serviceToken = new TokenService();
                Token stripeToken = await serviceToken.CreateAsync(options);

                var chargeOptions = new ChargeCreateOptions
                {
                    Amount = 5000,
                    Currency = "usd",
                    Description = "Premium",
                    Source = stripeToken.Id
                };

                var chargeService = new ChargeService();
                Charge charge = await chargeService.CreateAsync(chargeOptions);
                Facture facture = new Facture();
                if (charge.Paid)
                {
                    
                    facture.FactureDate = DateTime.Today.ToShortDateString();
                    facture.userName = payModel.username;
                    facture.Price = 50;
                    facture.FactureStatus = "paid";        
                }
                else
                {
                    facture.FactureDate = DateTime.Today.ToShortDateString();
                    facture.userName = payModel.username;
                    facture.Price = 50;
                    facture.FactureStatus = "Not paid";         
                }
                return facture;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
