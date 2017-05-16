
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Ht.Ihsil.Rgph.App.Superviseur.MVVM
{
    public class MessageBoxViewModel : INotifyPropertyChanged, IMessageBoxViewModel
    {
        private string title;
        private string message;
        private MessageBoxButtons messageBoxButton;
        private MessageBoxIcon messageBoxIcon;
        private Confirmation _confirmation;

        public Confirmation Confirmation
        {
            get { return _confirmation; }
            set
            {
                if (_confirmation != value)
                {
                    _confirmation = value;
                    RaisePropertyChanged(() => Confirmation);
                }
            }
        }

        public MessageBoxButtons MessageBoxButton
        {
            get
            {
                return this.MessageBoxButton;
            }
            set
            {
                if (MessageBoxButton != value)
                {
                    MessageBoxButton = value;
                    RaisePropertyChanged(() => MessageBoxButton);
                }
            }
        }

        public MessageBoxIcon MessageBoxImage
        {
            get
            {
                return this.MessageBoxImage;
            }
            set
            {
                if (this.MessageBoxImage != value)
                {
                    this.MessageBoxImage = value;
                    RaisePropertyChanged(() => MessageBoxImage);
                }
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (title != value)
                {
                    title = value;
                    RaisePropertyChanged(() => Title);
                }
            }
        }

        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                if (message != value)
                {
                    message = value;
                    RaisePropertyChanged(() => Message);
                }
            }
        }
        public string DisplayIcon
        {
            get
            {
                switch (this.MessageBoxImage)
                {
                    case MessageBoxIcon.Information:
                        return @"/images/Information_48.png";
                    case MessageBoxIcon.Error:
                        return @"/images/Error_48.png";
                    default: 
                        return @"/images/Information_48.png";
                }
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs((propertyName)));
            }
        }
        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            RaisePropertyChanged(propertyName);
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

}
