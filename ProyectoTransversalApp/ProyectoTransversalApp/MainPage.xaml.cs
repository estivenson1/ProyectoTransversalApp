using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoTransversalApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ContactViewModel();
        }
    }
    public class ContactViewModel
    {
        public Command SmsCommand { get; }
        public Command EmailCommand { get; }
        public Command PhoneCommand { get; }
        public Command NavigateCommand { get; }
        public Contact Contact { get; }
        public ContactViewModel(Contact contact)
        {
            Contact = contact;
        }
        public ContactViewModel()
        {
            Contact = new Contact
            {
                Name = "Estivenson Ortega Villar",
                Address = "Centro comercial caribe plaza",
                City = "Cartagena",
                State = "WA",
                ZipCode = "98052",
                Email = "eortega@facturecolombia.com",
                PhoneNumber = "3017915843"
            };

            SmsCommand = new Command(async () => await ExecuteSmsCommand());
            EmailCommand = new Command(async () => await ExecuteEmailCommand());
            PhoneCommand = new Command(ExecutePhoneCommand);
            NavigateCommand = new Command(async () => await ExecuteNavigateCommand());
        }

        async Task ExecuteSmsCommand()
        {
            try
            {
                await Sms.ComposeAsync(new SmsMessage(string.Empty, Contact.PhoneNumber));
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        async Task ExecuteEmailCommand()
        {
            try
            {
                string subject = "Envío dee SMS";
                string body = "Hola buenas tades este es el correo mire";
                await Email.ComposeAsync(subject, body, Contact.Email);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        void ExecutePhoneCommand()
        {
            try
            {
                PhoneDialer.Open(Contact.PhoneNumber);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        async Task ExecuteNavigateCommand()
        {
            try
            {
                await Map.OpenAsync(new Placemark
                {
                    Thoroughfare = Contact.Address,
                    Locality = Contact.City,
                    AdminArea = Contact.State,
                    PostalCode = Contact.ZipCode
                });
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        void ProcessException(Exception ex)
        {
            if (ex != null)
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
    }

}
