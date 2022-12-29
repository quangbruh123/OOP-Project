using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace QLBaiDoXe
{
    public class NotifyErrorViewModel1 : ObservableObject, INotifyDataErrorInfo
    {
        //Khởi tạo loại textbox tương ứng trong đây:

       
        private string ngay;
        public string Ngay
        {
            get => ngay;
            set => SetProperty(ref ngay, value);
        }

        private string thang;
        public string Thang
        {
            get => thang;
            set => SetProperty(ref thang, value);
        }

        private string nam;
        public string Nam
        {
            get => nam;
            set => SetProperty(ref nam, value);
        }


        public RelayCommand SubmitCommand { get; }

        public NotifyErrorViewModel1()
        {
            SubmitCommand = new RelayCommand(OnSubmit, CanSubmit);
            //Doing this will cause the errors to show immediately
            Validate(nameof(Ngay));
            Validate(nameof(Thang));
            Validate(nameof(Nam));
        }

        private bool CanSubmit()
        {
            //Link the CanExecute state of the command to the visible errors on the screen. 
            //You can also separate the command from the validation errors and simply change this to match the one in SimpleViewModel.CanSubmit
            return !HasErrors;
        }

        private void OnSubmit()
        {
            Debug.WriteLine("Form Submitted");
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            Validate(e.PropertyName);
        }

        private void Validate(string changedPropertyName)
        {
            //Do whatever validation is needed here
            //You can do validation accross multiple properties as well.
            switch (changedPropertyName)
            {
                case nameof(Ngay):
                    if (string.IsNullOrWhiteSpace(Ngay))
                    {
                        _ValidationErrorsByProperty[nameof(Ngay)] = new List<object> { " Trường dữ liệu bắt buộc." };
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Ngay)));
                    }
                    else if (_ValidationErrorsByProperty.Remove(nameof(Ngay)))
                    {
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Ngay)));
                    }
                    break;

                case nameof(Thang):
                    if (string.IsNullOrWhiteSpace(Thang))
                    {
                        _ValidationErrorsByProperty[nameof(Thang)] = new List<object> { " Trường dữ liệu bắt buộc." };
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Thang)));
                    }
                    else if (_ValidationErrorsByProperty.Remove(nameof(Thang)))
                    {
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Thang)));
                    }
                    break;

                case nameof(Nam):
                    if (string.IsNullOrWhiteSpace(Nam))
                    {
                        _ValidationErrorsByProperty[nameof(Nam)] = new List<object> { " Trường dữ liệu bắt buộc." };
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Nam)));
                    }
                    else if (_ValidationErrorsByProperty.Remove(nameof(Nam)))
                    {
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Nam)));
                    }
                    break;
            }
            SubmitCommand.NotifyCanExecuteChanged();

            //Notify the command that it needs to invoke its CanExecute method (in this case CanSubmit).

            /*if (string.IsNullOrWhiteSpace(changedPropertyName))
            {
                _ValidationErrorsByProperty[nameof(changedPropertyName)] = new List<object> { " Trường dữ liệu bắt buộc." };
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(changedPropertyName)));
            }
            else if (_ValidationErrorsByProperty.Remove(nameof(changedPropertyName)))
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(changedPropertyName)));
            }
            */
        }


        public IEnumerable GetErrors(string propertyName)
        {
            if (_ValidationErrorsByProperty.TryGetValue(propertyName, out List<object> errors))
            {
                return errors;
            }
            return Array.Empty<object>();
        }

        private readonly Dictionary<string, List<object>> _ValidationErrorsByProperty =
            new Dictionary<string, List<object>>();

        public bool HasErrors => _ValidationErrorsByProperty.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}