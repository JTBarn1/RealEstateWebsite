using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace RealEstateLibrary
{
    internal class EmailSender
    {
        public void SendAcceptEmail(string sender, string receiver, string firstName, string streetName)
        {

            Email email = new Email();
            string subject = "Offer Acceptance";
            string body = $"Hi {firstName},\n" +
            $"I’m pleased to inform you that your offer on {streetName} has been accepted! Congratulations on this exciting step towards your new home.";

            email.SendMail(receiver, sender, subject, body);
        }
        public void SendRejectEmail(string sender, string receiver, string firstName, string streetName)
        {
            

            Email email = new Email();
            string subject = "Offer Rejection";
            string body = $"Hi {firstName},\n" +
            $"I regret to inform you that your offer on {streetName} has been rejected. Thank you for your interest.";

            email.SendMail(receiver, sender, subject, body);
        }

        internal void SendOfferNotice(string workEmail, string address)
        {
            Email email = new Email();
            string subject = "Offer Notice";
            string body = $"A new Offer had been placed on {address}";
            

            email.SendMail(workEmail, workEmail, subject, body);
        }
    }
}
