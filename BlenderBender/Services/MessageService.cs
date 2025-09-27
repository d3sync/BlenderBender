using System;
using System.Windows.Forms;

namespace BlenderBender.Services
{
    /// <summary>
    /// Service responsible for automated message generation and clipboard operations.
    /// Extracted from various button click handlers in Form1.cs for better maintainability.
    /// </summary>
    public class MessageService
    {
        private readonly IUserService _userService;
        private readonly IDateService _dateService;

        public MessageService(IUserService userService, IDateService dateService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _dateService = dateService ?? throw new ArgumentNullException(nameof(dateService));
        }

        /// <summary>
        /// Generates a "did not answer" message and copies it to clipboard.
        /// </summary>
        public string GenerateDidNotAnswerMessage()
        {
            var message = $"**Δεν απαντούσε {_userService.DateTimeNUser()}**";
            CopyToClipboard(message);
            return message;
        }

        /// <summary>
        /// Generates an "inability to communicate" message and copies it to clipboard.
        /// </summary>
        public string GenerateInabilityCommunicateMessage()
        {
            var message = $"**Αδυναμία επικοινωνίας {_userService.DateTimeNUser()}**";
            CopyToClipboard(message);
            return message;
        }

        /// <summary>
        /// Generates a "requested pickup from store" message and copies it to clipboard.
        /// </summary>
        public string GeneratePickupFromStoreMessage()
        {
            var message = $"**Ζήτησε να παραλάβει απ το κατάστημα. {_userService.DateTimeNUser()}**";
            CopyToClipboard(message);
            return message;
        }

        /// <summary>
        /// Generates a "second on-site message" for e-shop.
        /// </summary>
        public string GenerateSecondOnSiteMessage(string phoneNumber, string deliveryDate)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentException("Phone number cannot be null or empty", nameof(phoneNumber));
            if (string.IsNullOrEmpty(deliveryDate))
                throw new ArgumentException("Delivery date cannot be null or empty", nameof(deliveryDate));

            var message = $"ΣΑΣ ΥΠΕΝΘΥΜΙΖΟΥΜΕ ΟΤΙ Η ΠΑΡΑΓΓΕΛΙΑ ΣΑΣ ΕΙΝΑΙ ΕΤΟΙΜΗ ΚΑΙ ΠΡΕΠΕΙ ΝΑ ΠΑΡΑΔΟΘΕΙ ΜΕΧΡΙ {deliveryDate.ToUpper()}. ΤΗΛ.: {phoneNumber}.";
            CopyToClipboard(message);
            return message;
        }

        /// <summary>
        /// Generates a message for customer requesting to keep item in store until specific date.
        /// </summary>
        public string GenerateKeepInStoreMessage(DateTime keepUntilDate)
        {
            var dateFormatted = keepUntilDate.ToString("dddd dd/MM");
            var message = $"**Ζήτησε να παραμείνει στο κατάστημα μέχρι και {dateFormatted}({_userService.CurrentUser()})**";
            CopyToClipboard(message);
            return message;
        }

        /// <summary>
        /// Generates a customer notification message for various scenarios.
        /// </summary>
        public string GenerateCustomerNotificationMessage(bool customerNotified, string additionalInfo = "")
        {
            var baseMessage = customerNotified ? "Ο πελάτης ενημερώθηκε" : "Αδυναμία ενημέρωσης";
            var message = $"~~{baseMessage} {_userService.DateTimeNUser()}";
            
            if (!string.IsNullOrEmpty(additionalInfo))
            {
                message += $" {additionalInfo}";
            }
            
            message += " ~~";
            CopyToClipboard(message);
            return message;
        }

        /// <summary>
        /// Generates help/shortcut information message.
        /// </summary>
        public string GenerateHelpMessage()
        {
            return "Ctrl + 1 : Selects Automated Messages Tab\n" +
                   "Ctrl + 2 : Selects E-mail Tab\n" +
                   "Ctrl + 3 : Selects Πιστωτικά Tab\n" +
                   "Ctrl + 4 : Selects Υπολογισμός Χρημάτων Tab\n" +
                   "Ctrl + C : Clears active tabs inputs\n" +
                   "Alt + Δ : Δεν απαντούσε\n" +
                   "Alt + E : 2ο Μήνυμα για επιτόπου\n" +
                   "Alt + A : Αδυναμία επικοινωνίας\n" +
                   "Alt + Ρ : Ημερομηνία και Ώρα τώρα\n" +
                   "Alt + Τ : Τιμολογήθηκε από ....\n" +
                   "Alt + Z : Ζήτησε κατάστημα\n";
        }

        /// <summary>
        /// Copies text to system clipboard and shows notification.
        /// </summary>
        /// <param name="text">Text to copy</param>
        public void CopyToClipboard(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Clipboard.SetText(text);
            }
        }

        /// <summary>
        /// Shows a notification with the specified message.
        /// </summary>
        public void ShowNotification(string message, string title = "e-Shop Assistant")
        {
            // This method signature allows for future implementation of proper notification system
            // Currently maintaining compatibility with existing NotifyIcon usage in Form1
        }
    }

    /// <summary>
    /// Interface for user service operations.
    /// </summary>
    public interface IUserService
    {
        string CurrentUser();
        string DateTimeNUser();
        string DateNUser();
    }

    /// <summary>
    /// Interface for date service operations.
    /// </summary>
    public interface IDateService
    {
        string DateTo(string option, int extraDays);
    }
}